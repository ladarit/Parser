using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    class FractionPageParser : PageParser<List<string>>
    {
        /// <summary>
        /// метод формирует коллекцию записей со страницы фракции
        /// </summary>
        /// <param name="html">разметка страницы фракции</param>
        /// <param name="checkBox">значение какого-либо чек-бокса</param>
        /// <param name="urlCollect">дополнительные url</param>
        /// <param name="fractionName">доп. параметры</param>
        /// <returns>возвращает коллекцию записей страницы фракции</returns>
        public override Page<List<string>> ParseDetails(string html, bool checkBox = false, string[] urlCollect = null, string fractionName = null)
        {
            var fractionDetails = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            HtmlDocument document = Converter.ConvertToHtmlDocument(html);

            if (string.IsNullOrEmpty(fractionName) || fractionName.Contains("Схема розміщення депутатських фракцій"))
                return new Page<List<string>>();

            //добавляем в коллекцию записей название фракции
            AddSingleValueDataToCollection(ref fractionDetails, "Назва фракції", fractionName, "FractionGeneralInfo");

            //добавляем в коллекцию записей номер созыва фракции
            var header = document.DocumentNode.SelectSingleNode("//h3/span").InnerText.RemoveOddSpaces();
            var convocation = Regex.Match(header, @"\s*(X?)(I{1,3}V?|I?XI{0,3}|VI{0,3})\s+").Value + " скликання";
            AddSingleValueDataToCollection(ref fractionDetails, "Скликання", convocation, "FractionGeneralInfo");

            //добавляем в коллекцию записей url страницы фракции
            AddSingleValueDataToCollection(ref fractionDetails, "Посилання", urlCollect?.Last(), "FractionGeneralInfo");

            if (!fractionName.Contains("Народні депутати, які не входять до складу жодної фракції"))
            {
                //находим <p> без ссылок
                var pNodes = document.DocumentNode.SelectNodes("//p").Where(p => p.InnerText.ContainsAny("Дата створення", "Кількісний склад")).ToList();
                foreach (var node in pNodes)
                {
                    var pageDetails = ParseBaseComInfo(node);
                    fractionDetails.PageDetails.Add(pageDetails);
                }
                //находим <p> с ссылками
                var pNodesWithlink = document.DocumentNode.SelectNodes("//p[@class='topTitle']");
                foreach (var node in pNodesWithlink)
                {
                    var link = node.SelectSingleNode("a")?.Attributes["href"].Value;
                    if (link == null)
                        throw new Exception("Не вдається знайти сторінку з переліком членів фракції");

                    var responce = HtmlProvider.GetResponse<string>(link);
                    if (responce.Error != null)
                        throw new Exception(responce.Error.ErrorMsg);

                    HtmlDocument page = Converter.ConvertToHtmlDocument(responce.ReceivedData);
                    var tableRows = page.DocumentNode.SelectNodes("//table[@class='striped Centered']//tr");
                    if (!(tableRows != null && tableRows.Any())) continue;
                    if (link.Contains("p_fraction_list"))
                        ParseParticipantsPage(ref fractionDetails, tableRows);
                    else
                        ParseDynamicOfTransitionsPage(ref fractionDetails, tableRows);
                }
            }
            else
            {
                var liNodes = document.DocumentNode.SelectNodes("//ul[@class='level1']/li");
                if (liNodes != null && liNodes.Any()) 
                    ParseParticipantsPage(ref fractionDetails, liNodes);
            }
            return fractionDetails;
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа законопроекта данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="farctionDetailsList">коллекция строк страницы законопроекта из метода ParseDetails</param>
        /// <param name="listOfCells">коллекция ячеек строки законопроекта со страницы с перечнем законопроектов</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> farctionDetailsList, List<string> listOfCells = null)
        {
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, FractionsRecordNamesComparator);

            if (farctionDetailsList == null)
            {
                rootCollect.Add(root); return rootCollect;
            }

            if (listOfCells != null)
            {
                foreach (var row in farctionDetailsList)
                {
                    var dictElement = row.GeneralSign == "FractionGeneralInfo"
                        ? FractionsRecordNamesComparator.FirstOrDefault(t => t.Key == row.Name).Value
                        : FractionsRecordNamesComparator.FirstOrDefault(t => t.Value.BlockName == row.GeneralSign)
                            .Value;
                    if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                    {
                        XmlElement element = doc.CreateElement(dictElement.ElementName);
                        AddDataToBlock(ref element, ref blocksCollection, dictElement.BlockName, doc, row);
                    }
                }
            }

            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes))
                root.AppendChild(xmlElement);
            rootCollect.Add(root);

            return rootCollect;
        }

        private void AddDataToBlock(ref XmlElement element, ref List<XmlElement> blocksCollection, string blockName, XmlDocument doc = null, Record<List<string>> row = null)
        {
            var block = blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == blockName);
            if (block == null) return;

            if (doc != null && row != null)
                if (row.GeneralSign == "Participants" || row.GeneralSign == "DynamicOfTransitions")
                {
                    var rowElem = doc.CreateElement("row");
                    if (row.Value != null && row.Value.Any())
                        foreach (var value in row.Value)
                        {
                            var names = value.Split(':');
                            var record = doc.CreateElement(FractionElementsNames.FirstOrDefault(n => n.Key == names[0]).Value);
                            record.SetAttribute("comment", names[0].RemoveOddSpaces());
                            record.InnerText = names[1].FormatToDateTimeOrText().RemoveOddSpaces();
                            rowElem.AppendChild(record);
                        }
                    block.AppendChild(rowElem);
                }
                else
                {
                    element.InnerText = row.Value.First().FormatToDateTimeOrText();
                    element.SetAttribute("type", "record");
                    block.AppendChild(element);
                }
        }

        private Record<List<string>> ParseBaseComInfo(HtmlNode node)
        {
            var pageDetails = new Record<List<string>> { Value = new List<string>() };
            var text = node.InnerText.RemoveOddSpaces().Split(':');
            pageDetails.GeneralSign = "FractionGeneralInfo";
            pageDetails.Name = text[0];
            pageDetails.Value.Add(text[1].Contains(", станом на") ? text[1].Split(',')[0] : text[1]);
            return pageDetails;
        }

        private void ParseParticipantsPage(ref Page<List<string>> fractionDetails, HtmlNodeCollection tableRows)
        {
            var sortedCollection = tableRows.OrderBy(e => e.InnerHtml.RemoveOddSpaces()).ToList();
            foreach (HtmlNode row in sortedCollection)
            {
                if (row.Name.Equals("li"))
                {
                    fractionDetails.PageDetails.Add(new Record<List<string>>
                    {
                        Name = "Склад фракції",
                        Value = new List<string> { "Ім'я:" + row.InnerText.RemoveOddSpaces().RemoveColon() },
                        GeneralSign = "Participants"
                    });
                    continue;
                }
                var data = row.SelectNodes("td|li");
                if (data.Count > 1 && !String.IsNullOrEmpty(data[0].InnerText) && !String.IsNullOrEmpty(data[1].InnerText))
                    fractionDetails.PageDetails.Add(new Record<List<string>>
                    {
                        Name = "Склад фракції",
                        Value = new List<string> { "Ім'я:" + data[0].InnerText.RemoveOddSpaces().RemoveColon(), "Посада:" + data[1].InnerText.RemoveOddSpaces() },
                        GeneralSign = "Participants"
                    });
            }
        }

        private void ParseDynamicOfTransitionsPage(ref Page<List<string>> lowDetails, HtmlNodeCollection tableRows)
        {
            var headers = tableRows.FindFirst("tr").SelectNodes("th").ToList();
            if (!headers.Any()) return;
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");
                if (!(cells != null && cells.Any())) continue;
                var text = new List<string>();
                for (int i = 0; i < cells.Count; i++)
                {
                    if (Regex.IsMatch(cells[i].InnerText, @"\w+") && cells[i].InnerText != "")
                        text.Add(headers[i].InnerText.RemoveColon() + ":" + cells[i].InnerText.RemoveOddSpaces());
                }
                var pageDetailsRow = new Record<List<string>>
                {
                    Name = "Динаміка переходів",
                    Value = new List<string>(),
                    GeneralSign = "DynamicOfTransitions"
                };
                pageDetailsRow.Value.AddRange(text);
                lowDetails.PageDetails.Add(pageDetailsRow);
            }
        }
    }
}

