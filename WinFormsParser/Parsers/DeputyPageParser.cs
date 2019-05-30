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
    public class DeputyPageParser : PageParser<List<string>>
    {
        /// <summary>
        /// метод формирует коллекцию записей со страницы депутата
        /// </summary>
        /// <param name="html">разметка страницы депутата</param>
        /// <param name="checkBoxOption">значение какого-либо чек-бокса</param>
        /// <param name="urlCollect">дополн. url</param>
        /// <param name="deputyPagelink">имя контрола</param>
        /// <returns>возвращает коллекцию записей со страницы депутата</returns>
        public override Page<List<string>> ParseDetails(string html, bool checkBoxOption = false, string[] urlCollect = null, string deputyPagelink = null)
        {
            var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            var document = Converter.ConvertToHtmlDocument(html);

            var generalInfoTrCollection = document.DocumentNode.SelectNodes("//table[@class='simple_info']/tr");

            ParseGeneralWorkInfo(ref page, generalInfoTrCollection[0], urlCollect, deputyPagelink, checkBoxOption);
            ParseBirthDayInfo(ref page, generalInfoTrCollection[1]);
            ParseGeneralAtTimeOfElectionInfo(ref page, generalInfoTrCollection.Skip(2).ToList());

            //пасринг переход по фракциям, должности на протяжении созыва
            var linkCollect = document.DocumentNode.SelectSingleNode("//div[@class='topTitle']").SelectNodes("a");
            foreach (var link in linkCollect)
            {
                var linkValue = link.Attributes["href"].Value;
                if (Regex.IsMatch(linkValue, @"p_(ex)*deputat(_fr_changes|_work_history)"))
                {
                    var transitionsPage = ParseBaseLinks(linkValue);
                    if (transitionsPage.PageDetails.Any())
                        transitionsPage.PageDetails.ForEach(p => page.PageDetails.Add(p));
                }
            }
            return page;
        }

        /// <summary>
        /// метод добавляет в коллекцию записей экземпляра Page общее инфо о рабочей деятельности депутата 
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="tr">строка таблицы информации о депутате</param>
        /// <param name="urlCollect">дополн. url</param>
        /// <param name="deputyPagelink"></param>
        /// <param name="checkBoxOption">значение какого-либо чек-бокса</param>
        private void ParseGeneralWorkInfo(ref Page<List<string>> page, HtmlNode tr, string[] urlCollect, string deputyPagelink, bool checkBoxOption = false)
        {
            //определяем имя депутата и добавляем его в коллекцию записей
            var name = tr.SelectNodes("td")?.FindFirst("h2")?.InnerText.RemoveOddSpaces();
            AddSingleValueDataToCollection(ref page, "Ім'я", name);

            //определяем номер созыва депутата и добавляем его в коллекцию записей
            var convocation = RomanArabicNumerals.ToRoman(int.Parse(urlCollect.First().Split('=').Last()) - 1) + " скликання";
            AddSingleValueDataToCollection(ref page, "Скликання", convocation);

            //добавляем url на страницу депутата
            AddSingleValueDataToCollection(ref page, "Посилання", deputyPagelink);

            //добавляем в коллекцию файлов фото депутата
            if(checkBoxOption)
                GetDeputyPhoto(ref page, tr, name, convocation);

            //добавляем в коллекцию инфо о депутате
            if (deputyPagelink.Contains("expage"))
                ParseExDeputyInfo(ref page, tr);
            else
                ParseIncumbentInfo(ref page, tr);
        }

        /// <summary>
        /// метод добавляет в коллекцию записей экземпляра Page информацию для действующего депутата
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="tr">строка таблицы информации о депутате</param>
        private void ParseIncumbentInfo(ref Page<List<string>> page, HtmlNode tr)
        {
            var generalInfo = tr.SelectNodes("td")[1].SelectSingleNode("div[@class='mp-general-info']");
            var post = generalInfo.SelectNodes("following-sibling::text()").FirstOrDefault(x => Regex.IsMatch(x.InnerText, @"\w+"));

            foreach (var dt in generalInfo.SelectSingleNode("dl").SelectNodes("dt"))
                AddSingleValueDataToCollection(ref page, dt.InnerText,dt.SelectSingleNode("following-sibling::dd[1]").InnerText);

            if (string.IsNullOrEmpty(post?.InnerText) || Regex.IsMatch(post.InnerText, @"^На пленарних засіданнях")) return;

            var fraction = post.InnerText.RemoveOddSpaces();

            AddSingleValueDataToCollection(ref page, "Фракція:", DeputyElementNames.FirstOrDefault(r => fraction.Contains(r.Key, StringComparison.OrdinalIgnoreCase)).Value);

            AddSingleValueDataToCollection(ref page, "Посада у фракції:", fraction);
        }

        /// <summary>
        /// метод добавляет в коллекцию записей экземпляра Page информацию про экс-депутата
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="tr">строка таблицы информации о депутате</param>
        private void ParseExDeputyInfo(ref Page<List<string>> page, HtmlNode tr)
        {
            var generalInfo = tr.SelectNodes("td")?.FindFirst("h2").SelectNodes("following-sibling::text()")
                              .Where(x => Regex.IsMatch(x.InnerText.RemoveOddSpaces(), @"\w+")).ToList();

            if (generalInfo == null || !generalInfo.Any()) return;

            var info = string.Empty;
            foreach (var textNode in generalInfo)
            {
                var text = textNode.InnerText.RemoveOddSpaces();
                if (Regex.IsMatch(text, @"^Дата (набуття|припинення)"))
                {
                    var values = text.Split(':');
                    AddSingleValueDataToCollection(ref page, values[0] + ":", values[1]);
                }
                else
                    info += text + ". ";
            }
            if(!string.IsNullOrEmpty(info))
                AddSingleValueDataToCollection(ref page, "Відомості про партійність:", info);
        }

        /// <summary>
        /// метод добавляет в коолекцию записей экземпляра Page инфо о дате рождения депутата
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="tr">строка таблицы информации о депутате</param>
        private void ParseBirthDayInfo(ref Page<List<string>> page, HtmlNode tr)
        {
            var additInfo = tr.SelectNodes("td").First().InnerText;
            var additInfoValue = tr.SelectNodes("td").Last().InnerText.GetDateFromVerbal();
            AddSingleValueDataToCollection(ref page, additInfo, additInfoValue);
        }

        /// <summary>
        /// метод добавляет в коллекцию записей экземпляра Page инфо о депутате на момент избрания
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="trCollect">коллекция строк таблицы информации о депутате</param>
        private void ParseGeneralAtTimeOfElectionInfo(ref Page<List<string>> page, List<HtmlNode> trCollect)
        {
            var info = string.Empty;
            foreach (var tr in trCollect)
            {
                var additInfo = tr.SelectNodes("td").First().InnerText.RemoveOddSpaces();
                if (additInfo.Contains("Відомості на момент обрання:"))
                    additInfo = string.Empty;
                var additInfoValue = tr.SelectNodes("td").Last().InnerText;
                info += additInfo + " " + additInfoValue + ". ";
            }
            if (string.IsNullOrEmpty(info)) return;
            info = Regex.Replace(info.RemoveOddSpaces(), @"\.$", string.Empty).Replace(",", ", ");
            AddSingleValueDataToCollection(ref page, "Відомості на момент обрання:", info);
        }

        /// <summary>
        /// метод добавляет в коллекцию Files экземпляра Page файл фото депутата
        /// </summary>
        /// <param name="page">коллекция записей</param>
        /// <param name="tr">строка таблицы информации о депутате</param>
        /// <param name="name">имя депутата</param>
        /// <param name="convocation">строка с номером созыва</param>
        private void GetDeputyPhoto(ref Page<List<string>> page, HtmlNode tr, string name, string convocation)
        {
            var photo = tr.SelectNodes("td")?.FindFirst("img")?.Attributes["src"].Value;
            if (photo == null) return;
            var response = HtmlProvider.GetResponse<byte[]>(photo);
            if (response.Error != null)
                throw FormattedExceptionCreator.CreateExc(response.Error);
            page.Files = new List<FileModel> { new FileCreator().CreateFile(response.ReceivedData, response.FileType, "Депутат#Фото#" + name + "#" + convocation + "#") };
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа законопроекта данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="deputyPageList">коллекция строк страницы законопроекта из метода ParseDetails</param>
        /// <param name="listOfCells">коллекция ячеек строки законопроекта со страницы с перечнем законопроектов</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> deputyPageList, List<string> listOfCells = null)
        {
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, DeputyRecordNamesComparator);

            if (deputyPageList != null)
            {
                foreach (var pageRow in deputyPageList)
                {
                    var dictElement = DeputyRecordNamesComparator.FirstOrDefault(t => t.Key.Equals(pageRow.Name, StringComparison.OrdinalIgnoreCase)).Value;
                    if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                    {
                        var element = doc.CreateElement(dictElement.ElementName);
                        AddDataToBlock(ref element, ref blocksCollection, dictElement.BlockName, pageRow.GeneralSign, doc, pageRow, dictElement.IsLiElement);
                    }
                }
            }
            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes))
                root.AppendChild(xmlElement);
            rootCollect.Add(root);

            return rootCollect;
        }

        /// <summary>
        /// метод возращает инофрмацию о переходах депутата между фракциями, о должностях
        /// </summary>
        /// <param name="link">часть url для парсинга</param>
        private Page<List<string>> ParseBaseLinks(string link)
        {
            var record = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            var response = HtmlProvider.GetResponse<string>(link.CleanUrl());

            //находим в разметке некорректные tr теги и исправляем
            response.ReceivedData = FixFractionTransitTableHtmlMurkup(response.ReceivedData);

            HtmlDocument page = Converter.ConvertToHtmlDocument(response.ReceivedData);
            var tableRows = page.DocumentNode.SelectNodes("//table[@class='striped Centered']/tr");
            var headers = tableRows?.FindFirst("tr").SelectNodes("th").ToList();

            if (tableRows == null || !tableRows.Any() || !headers.Any()) return record;

            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");
                if (!(cells != null && cells.Any())) continue;
                var text = new List<string>();
                for (int i = 0; i < cells.Count; i++)
                    if (cells[i].InnerText.RemoveOddSpaces() != "")
                        text.Add(headers[i].InnerText.RemoveOddSpaces() + ": " + cells[i].InnerText.RemoveOddSpaces());
                var pageRow = new Record<List<string>>
                {
                    Name = link.Contains("fr_changes") ? "Переходи по фракціях" : "Посади протягом скликання",
                    Value = new List<string>()
                };
                pageRow.Value.AddRange(text);
                record.PageDetails.Add(pageRow);
            }
            return record;
        }

        /// <summary>
        /// метод добавляет данные в блок
        /// </summary>
        /// <param name="element">элемент, данные из которого будут добавлены в блок</param>
        /// <param name="blocksCollection">коллекция блоков</param>
        /// <param name="blockName">имя блока, в который надо добавить данные</param>
        /// <param name="blockComment"></param>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="pageDetailsRow">запись, данные которой будут добавлены в element</param>
        /// <param name="isLiElement">является ли данный элемент списочным</param>
        private void AddDataToBlock(ref XmlElement element, ref List<XmlElement> blocksCollection, string blockName, string blockComment = null,
                                    XmlDocument doc = null, Record<List<string>> pageDetailsRow = null, bool isLiElement = false)
        {
            var block = !string.IsNullOrEmpty(blockComment)
                ? blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == blockName && b.GetAttribute("comment") == blockComment)
                : blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == blockName);
            if (block == null) return;

            //если запись НЕ равна null, то работаем с записью (если запись равна null => данные для блока уже есть в element, поэтому просто добавляем элемент в блок)
            if (doc != null && pageDetailsRow != null)
            {
                //если запись списочная
                if (isLiElement)
                {
                    var row = doc.CreateElement("row");
                    row = AddDataToRow(row, doc, pageDetailsRow, blockName);
                    block.AppendChild(row.Clone());
                    return;
                }
                //если запись НЕ списочная
                var value = pageDetailsRow.Value.First();
                element.InnerText = value.FormatToDateTimeOrText().RemoveOddSpaces();
                element.SetAttribute("comment", pageDetailsRow.Name.RemoveColon());
            }
            if (blockName == "GeneralInfo") element.SetAttribute("type", "record");
            block.AppendChild(element);
        }

        /// <summary>
        /// метод наполняет информацией строки разделов, которые состоят из нескольких полей 
        /// </summary>
        /// <param name="row">строка для наполения</param>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="pageDetailsRow">запись, данные которой будут добавлены в строку</param>
        /// <param name="blockName">имя блока</param>
        /// <returns></returns>
        private XmlElement AddDataToRow(XmlElement row, XmlDocument doc, Record<List<string>> pageDetailsRow, string blockName)
        {
            foreach (var innerRow in pageDetailsRow.Value)
            {
                List<string> list = new List<string> { innerRow };
                foreach (var text in list.Where(t => t.Contains(": ")))
                {
                    var newElem = CreateElement(text, doc, list, blockName);
                    row.AppendChild(newElem);
                }
            }
            return row;
        }

        /// <summary>
        /// метод возвращает новый xml элемент, который впоследствии записывается в элемент row
        /// </summary>
        /// <param name="text">текст нового элемента</param>
        /// <param name="doc">xml документ</param>
        /// <param name="list">коллекция значений, содержащая text, для определения имени нового элемента</param>
        /// <param name="blockName">имя блока, которому принадлжеит элемент</param>
        private XmlElement CreateElement(string text, XmlDocument doc, List<string> list, string blockName)
        {
            var newElemName = DeputyElementNames.FirstOrDefault(e => text.Contains(e.Key)).Value;
            var newElem = doc.CreateElement((list.Count > 1 ? list[0] : blockName) + "_" + newElemName);
            var delimiter = text.IndexOf(':');
            var comment = text.Substring(0, delimiter);
            var value = text.Substring(delimiter + 1);
            newElem.SetAttribute("comment", comment);
            newElem.InnerText = value.FormatToDateTimeOrText().RemoveOddSpaces();
            return newElem;
        }

        /// <summary>
        /// метод исправляет некорректные tr теги таблиц в разметке
        /// </summary>
        /// <param name="recievedData">разметка</param>
        /// <returns>исправленная разметка</returns>
        private string FixFractionTransitTableHtmlMurkup(string recievedData)
        {
            recievedData = recievedData.RemoveOddSpaces();
            var table = Regex.Match(recievedData, @"(<table).*(</table>)");
            var badTrCollection = Regex.Matches(table.ToString(), @"<tr>((?!</tr>).)+?<tr>").Cast<Match>().ToList();
            foreach (var badTr in badTrCollection)
            {
                var correctTr = Regex.Replace(badTr.ToString(), @"<tr>$", "</tr>");
                recievedData = recievedData.Replace(badTr.ToString(), correctTr);
            }
            return recievedData;
        }

        public virtual List<List<HtmlNode>> ParsePage(string html)
        {
            return null;
        }

        public virtual Page<List<string>> CreatePage(List<HtmlNode> day)
        {
            return null;
        }

        public virtual Page<List<string>> CreatePage(string html, string[] satellitePage)
        {
            return null;
        }
    }
}
