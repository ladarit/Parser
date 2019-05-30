using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class LowsPageParser : PageParser<List<string>>
    {
        private string LowNumber { get; set; }

        /// <summary>
        /// метод создает блоки для корневого элемента xml документа
        /// </summary>
        /// <param name="doc">xml документ законопроекта (пустой)</param>
        /// <param name="dictionary"></param>
        /// <returns>коллекция блоков</returns>
        public override List<XmlElement> CreateBlocks(XmlDocument doc, Dictionary<string, RecordOptions> dictionary)
        {
            List<XmlElement> blockCollection = new List<XmlElement>();
            foreach (var elem in dictionary.Select(e => e.Value.BlockName).Distinct().ToList())
            {
                XmlElement block = doc.CreateElement("block");
                block.SetAttribute("type", dictionary.FirstOrDefault(e => e.Value.BlockName == elem).Value.IsLiElement ? "list" : "record");
                block.SetAttribute("name", elem);
                if (elem == "LawInfo")
                    block.SetAttribute("comment", "Інформація про Законопроект");
                blockCollection.Add(block);
            }
            return blockCollection;
        }

        /// <summary>
        /// метод формирует коллекцию записей со страницы законопроекта
        /// </summary>
        /// <param name="html">разметка страницы законопроекта</param>
        /// <param name="checkbox">значение какого-либо чек-бокса</param>
        /// <param name="satellitePages">дополнительные url для парсинга</param>
        /// <param name="additParams">доп. параметры</param>
        /// <returns>возвращает коллекцию записей страницы законопроекта</returns>
        public override Page<List<string>> ParseDetails(string html, bool checkbox = false, string[] satellitePages = null, string additParams = null)
        {
            var lowDetails = new Page<List<string>>
            {
                PageDetails = new List<Record<List<string>>>(),
                Files = new List<FileModel>()
            };
            var listOfBlocksWithMultRecInRow = new List<Record<List<string>>>();

            //создаем xml документ страницы законопроекта и находим все ноды с именем "dl"
            var document = Converter.ConvertToHtmlDocument(html);
            var collectionOfDefinitions = document.DocumentNode.SelectSingleNode("//div[@class='zp-info']").SelectSingleNode("dl");
            collectionOfDefinitions.InnerHtml = collectionOfDefinitions.InnerHtml.Replace("\n", "");

            //проходим по каждому дочернему ноду "dt" нода "dl"
            foreach (var row in collectionOfDefinitions.SelectNodes("dt"))
            {
                var pageDetailsRow = new Record<List<string>>
                {
                    Name = row.InnerText == "Ініціатор(и) законопроекту:"
                        ? "Ініціатор законопроекту"
                        : row.InnerText.RemoveColon(),
                    Value = new List<string>()
                };

                //выбираем ноды "li" в элементе "dd", соответствующем текущему элементу "dt"
                var currentRowValuesInLi = row.NextSibling.SelectNodes("li");

                //если такие ноды есть
                if (currentRowValuesInLi != null)
                {
                    //var sortedCollection = currentRowValuesInLi.OrderBy(e => e.InnerHtml.RemoveOddSpaces()).ToList();
                    LowNumber = lowDetails.PageDetails[0].Value[0].Split(new[] {"від"}, StringSplitOptions.None)[0].Replace("/", "_").Trim();
                    foreach (var item in currentRowValuesInLi)
                    {
                        var fileName = LowNumber + "#";
                        //если несколько записей в будущем row
                        if (LowTableElemTypesByMultiRecordInRow.Where(t => t.Key == pageDetailsRow.Name).Select(t => t.Value).FirstOrDefault())
                        {
                            fileName = fileName + "Связь#" + item.InnerText.RemoveOddSpaces();
                            var file = CreateFile(item, fileName, satellitePages?[1]);
                            if (file.Error != null)
                                return new Page<List<string>> {Error = file.Error};
                            if (file.Content != null && !string.IsNullOrEmpty(file.FileName))
                                lowDetails.Files.Add(file);
                            var defRow = GetValuesForMultiplieRecordsInRow(item, satellitePages?[1]);
                            if (defRow.Error != null)
                                return new Page<List<string>> {Error = defRow.Error};
                            if (defRow.Value.Any())
                                listOfBlocksWithMultRecInRow.Add(defRow);
                        }
                        else
                        {
                            if (pageDetailsRow.Name == "Текст законопроекту та супровідні документи")
                            {
                                fileName = LowNumber + "#" + item.InnerText.RemoveOddSpaces();
                                var file = CreateFile(item, fileName, satellitePages?[1]);
                                if (file.Error != null)
                                    return new Page<List<string>> {Error = file.Error};
                                if (file.Content == null || string.IsNullOrEmpty(file.FileName)) continue;
                                lowDetails.Files.Add(file);
                            }
                            else
                                pageDetailsRow.Value.Add(item.InnerText);
                        }
                    }
                }
                //если таких нодов нет, то берем внутренний текст элемента "dd"
                else
                {
                    if (pageDetailsRow.Name.ContainsAny("Включено до порядку денного", "Номер, дата акту"))
                        pageDetailsRow = SplitStringByDateAndNumber(ref lowDetails, pageDetailsRow, row);
                    else
                        pageDetailsRow.Value.Add(row.NextSibling.InnerText);
                }

                //кладем данные из текущей строки в коллекцию
                if (listOfBlocksWithMultRecInRow.Any())
                {
                    listOfBlocksWithMultRecInRow.ForEach(b => lowDetails.PageDetails.Add(b));
                    listOfBlocksWithMultRecInRow.Clear();
                }
                else
                    lowDetails.PageDetails.Add(pageDetailsRow);
            }

            //добавляем адрес страницы в коллекцию
            lowDetails.PageDetails.Add(new Record<List<string>>
            {
                Name = "Адреса веб-сторінки",
                Value = new List<string> { satellitePages?.Last() }
            });

            //работа с нижними вкладками
            foreach (var link in new[] { "#flow_tab", "#kom_processing_tab", "#ui-tabs-2" })
            {
                ParseBottomTab(ref lowDetails, satellitePages?.Last(), link);
                if (lowDetails.Error != null) break;
            }

            //проверяем имена файлов и меняем их, если они совпадают
            ChangeFilesIdenticalNames(lowDetails.Files);

            return lowDetails;
        }

        /// <summary>
        /// метод добавляет одну часть разбитой строки в коллекцию и возвращает другую часть строки
        /// </summary>
        /// <param name="lowDetails"></param>
        /// <param name="pageDetailsRow"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private Record<List<string>> SplitStringByDateAndNumber(ref Page<List<string>> lowDetails, Record<List<string>> pageDetailsRow, HtmlNode row)
        {
            List<string> listOfNames = null;
            if (pageDetailsRow.Name.Equals("Включено до порядку денного"))
                listOfNames = new List<string> { pageDetailsRow.Name + " за датою", pageDetailsRow.Name + " за номером" };
            if (pageDetailsRow.Name.Equals("Номер, дата акту"))
                listOfNames = new List<string> { "Дата акту", "Номер акту" };

            var text = row.NextSibling.InnerText.Split(new[] { "від" }, StringSplitOptions.None);
            lowDetails.PageDetails.Add(new Record<List<string>>
            {
                Name = listOfNames?.First(),
                Value = new List<string> { text[1].FormatToDateTimeOrText().Trim() }
            });
            pageDetailsRow.Name = listOfNames?.Last();
            pageDetailsRow.Value.Add(text[0].Trim());
            return pageDetailsRow;
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа законопроекта данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="lowPageDetails">коллекция строк страницы законопроекта из метода ParseDetails</param>
        /// <param name="listOfCells">коллекция ячеек строки законопроекта со страницы с перечнем законопроектов</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> lowPageDetails, List<string> listOfCells = null)
        {
            var lowNumber = string.Empty;
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, LowRecordNamesComparator);

            if (listOfCells != null && lowPageDetails != null)
            {
                lowNumber = listOfCells[0].Trim();
                //Создаем опциональные блоки c комментариями
                foreach (var record in lowPageDetails.Where(r => !string.IsNullOrEmpty(r.GeneralSign) || !string.IsNullOrEmpty(r.ParentBlockGeneralSign)))
                {
                    XmlElement block = doc.CreateElement("block");
                    block.SetAttribute("type", "list");
                    block.SetAttribute("name", record.Id);
                    if (!string.IsNullOrEmpty(record.GeneralSign))
                    {
                        block.SetAttribute("comment", record.GeneralSign);
                    }
                    blocksCollection.Add(block);
                }

                XmlElement element = doc.CreateElement("LowNumber");
                element.InnerText = listOfCells[0].Trim();
                AddDataToBlock(ref element, ref blocksCollection, "LawInfo");

                element = doc.CreateElement("RegistrationDate");
                element.InnerText = listOfCells[1].Trim();
                AddDataToBlock(ref element, ref blocksCollection, "LawInfo");

                element = doc.CreateElement("LowName");
                element.InnerText = listOfCells[2].Trim();
                AddDataToBlock(ref element, ref blocksCollection, "LawInfo");

                foreach (var pageDetailsRow in lowPageDetails)
                {
                    var dictElement = LowRecordNamesComparator.FirstOrDefault(t => t.Key == pageDetailsRow.Name).Value;
                    if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                        element = doc.CreateElement(dictElement.ElementName);
                    AddDataToBlock(ref element, ref blocksCollection, dictElement.BlockName, pageDetailsRow.GeneralSign, doc, pageDetailsRow, dictElement.IsLiElement, pageDetailsRow.ParentBlockGeneralSign);
                }
                listOfCells.Clear();
            }

            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes && !new[] {"NamedVotingResults", "FractionResult", "DeputyResult"}.Contains(b.GetAttribute("name"))))
                root.AppendChild(xmlElement);
            rootCollect.Add(root);
            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes && b.GetAttribute("name") == "NamedVotingResults"))
            {
                XmlElement additionalRoot = doc.CreateElement("document");

                XmlElement block = doc.CreateElement("block");
                block.SetAttribute("type", "record");
                block.SetAttribute("name", "VoteInfo");
                block.SetAttribute("comment", "Інформація щодо голосування за Законопроект");

                XmlElement element = doc.CreateElement("VoteInfo_LawNumber");
                element.SetAttribute("type", "record");
                element.InnerText = lowNumber;
                block.AppendChild(element.Clone());

                element = doc.CreateElement("VoteInfo_LawDate");
                element.SetAttribute("type", "record");
                element.InnerText = xmlElement.GetAttribute("comment").Split(':').Last().Trim();
                block.AppendChild(element.Clone());

                xmlElement.InsertBefore(block, xmlElement.FirstChild);
                var childNodes = xmlElement.ChildNodes;
                for (int i = 0; i < childNodes.Count; i++)
                    additionalRoot.AppendChild(childNodes[i].Clone());
                rootCollect.Add(additionalRoot);
            }
            return rootCollect;
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
                                    XmlDocument doc = null, Record<List<string>> pageDetailsRow = null, bool isLiElement = false, string parentBlockGeneralSign = null)
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
                    var dictElement = LowRecordNamesComparator.FirstOrDefault(t => t.Key == pageDetailsRow.Name).Value;
                    if (dictElement.IsMultiRecordInRow)
                    {
                        if (pageDetailsRow.Name == "Пофракційні результати голосування" || pageDetailsRow.Name == "Депутатські результати голосування")
                        {
                            FillOptionalBlocks(ref blocksCollection, doc, pageDetailsRow, blockName, dictElement.DefRowName);
                            if (!string.IsNullOrEmpty(parentBlockGeneralSign))
                            {
                                var parentBlock = blocksCollection.FirstOrDefault(b => b.GetAttribute("comment") == parentBlockGeneralSign);
                                if (parentBlock != null)
                                {
                                    parentBlock.AppendChild(block.Clone());
                                    block.IsEmpty = true;
                                }
                            }
                        }
                        else
                        {
                            row = AddDataToRow(row, doc, pageDetailsRow, blockName, dictElement.DefRowName);
                            block.AppendChild(row.Clone());
                        }
                    }
                    else
                        foreach (var innerRow in pageDetailsRow.Value)
                        {
                            element.InnerText = innerRow.RemoveOddSpaces();
                            element.SetAttribute("comment", pageDetailsRow.Name);
                            row.AppendChild(element);
                            block.AppendChild(row.Clone());
                        }
                    return;
                }
                //если запись НЕ списочная
                element.InnerText = pageDetailsRow.Value.First().RemoveOddSpaces();
                if (blockName != "LawInfo") element.SetAttribute("comment", pageDetailsRow.Name);
            }
            if (blockName == "LawInfo") element.SetAttribute("type", "record");
            block.AppendChild(element);
        }

        /// <summary>
        /// метод наполняет информацией строки разделов, которые состоят из нескольких полей 
        /// </summary>
        /// <param name="row">строка для наполения</param>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="pageDetailsRow">запись, данные которой будут добавлены в строку</param>
        /// <param name="blockName">имя блока</param>
        /// <param name="defName">дефолтное имя для поля строки</param>
        /// <returns></returns>
        private XmlElement AddDataToRow(XmlElement row, XmlDocument doc, Record<List<string>> pageDetailsRow, string blockName, string defName)
        {
            foreach (var innerRow in pageDetailsRow.Value)
            {
                List<string> list = new List<string> { innerRow };
                foreach (var text in list.Where(t => t.Contains(": ")))
                {
                    var newElem = CreateElement(text, doc, list, defName, blockName);
                    row.AppendChild(newElem);
                }
            }
            return row;
        }

        /// <summary>
        /// метод наполняет информацией опциональные блоки (т.е. блоки, которые могут отсутствовать в документе)
        /// </summary>
        /// <param name="blocksCollection">коллекция блоков</param>
        /// <param name="doc">xml документ</param>
        /// <param name="pageDetailsRow">коллекция записей</param>
        /// <param name="blockName">имя опционального блока</param>
        /// <param name="defName">имя по умолчанию для элемента</param>
        private void FillOptionalBlocks(ref List<XmlElement> blocksCollection, XmlDocument doc, Record<List<string>> pageDetailsRow, string blockName, string defName)
        {
            foreach (var innerRow in pageDetailsRow.Value)
            {
                XmlElement row = doc.CreateElement("row");
                List<string> list = innerRow.Split(new[] { '\n' }, StringSplitOptions.None).ToList();
                var tmpBlock = blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == list[0]);
                if (tmpBlock == null) return;
                foreach (var text in list.Where(t => t.Contains(": ")))
                {
                    var newElem = CreateElement(text, doc, list, defName, blockName);
                    row.AppendChild(newElem);
                    tmpBlock.AppendChild(row);
                }
            }
        }

        /// <summary>
        /// метод возвращает новый xml элемент, который впоследствии записывается в элемент row
        /// </summary>
        /// <param name="text">текст нового элемента</param>
        /// <param name="doc">xml документ</param>
        /// <param name="list">коллекция значений, содержащая text, для определения имени нового элемента</param>
        /// <param name="defName">имя по умолчанию для элемента</param>
        /// <param name="blockName">имя блока, которому принадлжеит элемент</param>
        /// <returns></returns>
        private XmlElement CreateElement(string text, XmlDocument doc, List<string> list, string defName, string blockName)
        {
            var newElemName = LowElementsNames.FirstOrDefault(e => text.Contains(e.Key)).Value ??
                              (LowElementsNames.FirstOrDefault(e => Regex.IsMatch(text, e.Key)).Value ?? defName);
            var newElem = doc.CreateElement((list.Count > 1 ? list[0] : blockName) + "_" + newElemName);
            var splitResult = text.Split(':').ToList().Select(t => t.RemoveOddSpaces()).ToList();
            newElem.SetAttribute("comment", splitResult[0]);
            if (newElemName.Equals("URL"))
            {
                newElem.InnerText = splitResult[1] + ":" + splitResult[2];
            }
            else
            {
                newElem.InnerText = splitResult[1];
            }
            return newElem;
        }

        /// <summary>
        /// метод возвращает данные для раздела "Документи, пов'язані із роботою"
        /// </summary>
        /// <param name="liNode">"li" нод</param>
        /// <param name="conclusionPage">первая часть(неизменная) url страницы с выводами комитета о закопроекте</param>
        /// <returns>возвращает данные из переданого в него li нода</returns>
        private Record<List<string>> GetValuesForMultiplieRecordsInRow(HtmlNode liNode, string conclusionPage)
        {
            var definitionRow = new Record<List<string>>
            {
                Name = "Документи, пов'язані із роботою",
                Value = new List<string>()
            };
            var docToWork = liNode.SelectSingleNode("a[@target='_blank']");
            if (docToWork == null) return definitionRow;

            var docToWorkLink = docToWork.GetAttributeValue("href", string.Empty);
            var index = docToWorkLink.IndexOf("pcaption=", StringComparison.Ordinal) + 9;
            var docToWorkLinkDecoded = HttpUtility.UrlEncode(docToWorkLink.Substring(index), Encoding.GetEncoding("windows-1251"));
            docToWorkLink = docToWorkLink.Substring(0, index) + docToWorkLinkDecoded;
            var response = HtmlProvider.GetResponse<string>(conclusionPage + docToWorkLink);

            if (response.Error != null)
                return new Record<List<string>> { Error = response.Error };

            if (!string.IsNullOrEmpty(response.ReceivedData))
            {
                HtmlDocument page = Converter.ConvertToHtmlDocument(response.ReceivedData);
                var pageInnerText = page.DocumentNode.SelectSingleNode("//body").InnerText;
                if (pageInnerText != null)
                {
                    var textValues = new List<string>();
                    pageInnerText = pageInnerText.Replace("\n", "");
                    var shortTextIndex = pageInnerText.IndexOf("Скорочений текст", StringComparison.Ordinal);
                    if (shortTextIndex > -1)
                    {
                        var conclusion = liNode.InnerText.RemoveOddSpaces();
                        var date = Regex.Match(conclusion, @"(\d+\.\d+\.\d+)$").Value;
                        //добавляем дату
                        if (!string.IsNullOrEmpty(date))
                            textValues.Add("Дата: " + date.RemoveOddSpaces());
                        //добавляем висновок
                        textValues.Add("Назва: " + conclusion);
                        //добавляем Скорочений текст
                        var formalizedTextIndex = pageInnerText.IndexOf("Формалізований текст", StringComparison.Ordinal);
                        if (formalizedTextIndex > -1)
                            textValues.Add(pageInnerText.Substring(shortTextIndex, formalizedTextIndex - shortTextIndex));
                        //добавляем Формалізований текст
                        textValues.Add(pageInnerText.Substring(formalizedTextIndex));
                    }
                    definitionRow.Value.AddRange(textValues);
                }
            }
            return definitionRow;
        }

        /// <summary>
        /// метод получает информацию из нижних вкладок страницы законопроекта
        /// </summary>
        /// <param name="lowDetails">коллекция полей страницы законопроекта</param>
        /// <param name="fullLink">ссылка на страницу законопроекта</param>
        /// <param name="link">ссылка на вкладку</param>
        private void ParseBottomTab(ref Page<List<string>> lowDetails, string fullLink, string link)
        {
            var response = HtmlProvider.GetResponse<string>(fullLink + link);
            if (response.Error != null)
            {
                lowDetails.Error = response.Error;
                return;
            }
            if (!string.IsNullOrEmpty(response.ReceivedData))
                link = link.Substring(1);
            HtmlDocument page = Converter.ConvertToHtmlDocument(response.ReceivedData);
            if (link == "flow_tab" || link == "kom_processing_tab")
                ParseBaseBottomTab(ref lowDetails, page, link);
            if (link == "ui-tabs-2")
                ParseVotesTab(ref lowDetails, page, fullLink);
        }

        /// <summary>
        /// метод возвращает информацию из нижней вкладки со страницы законопроекта
        /// </summary>
        /// <param name="lowDetails">коллекция полей страницы законопроекта</param>
        /// <param name="page">страница для парсинга, содержащая информацию во вкладке</param>
        /// <param name="link">укороченная ссылка на вкладку</param>
        private void ParseBaseBottomTab(ref Page<List<string>> lowDetails, HtmlDocument page, string link)
        {
            var tableRows = page.DocumentNode.SelectNodes($"//div[@id='{link}']//table[@class='striped Centered']//tr");
            if (!(tableRows != null && tableRows.Any())) return;
            var headers = tableRows.FindFirst("tr").SelectNodes("th").ToList();
            if(link.Equals("flow_tab"))
                lowDetails.PageDetails.Add(new Record<List<string>>
                {
                    Name = "Проходження законопроекту",
                    Value = new List<string> { headers[1].InnerText.RemoveOddSpaces() }
                });
            if (!headers.Any()) return;
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");
                if (!(cells != null && cells.Any())) continue;
                var text = new List<string>();
                for (int i = 0; i < cells.Count; i++)
                {
                    if (!Regex.IsMatch(cells[i].InnerText, @"^\s+") && cells[i].InnerText != "")
                        text.Add(headers[i].InnerText.RemoveColon() + ": " + cells[i].InnerText);
                }
                var pageDetailsRow = new Record<List<string>>
                {
                    Name = link == "flow_tab" ? "Проходження" : "Опрацювання комітетами",
                    Value = new List<string>()
                };
                pageDetailsRow.Value.AddRange(text);
                lowDetails.PageDetails.Add(pageDetailsRow);
                if (link.Equals("flow_tab")) break; //если это вкладка проходження, то берем только первую строку таблицы во вкладке
            }
        }

        /// <summary>
        /// метод возвращает информацию со вкладки голосований
        /// </summary>
        /// <param name="lowDetails">коллекция полей страницы законопроекта</param>
        /// <param name="page">страница для парсинга, содержащая информацию во вкладке</param>
        /// <param name="fullLink">полная ссылка на вкладку</param>
        private void ParseVotesTab(ref Page<List<string>> lowDetails, HtmlDocument page, string fullLink)
        {
            var liElem = page.DocumentNode.SelectNodes("//div[@class='tabs_block']//ul//li").FirstOrDefault(li => li.InnerText == "Результати голосування");
            var href = liElem?.SelectSingleNode("a").Attributes["href"].Value;
            if (string.IsNullOrEmpty(href)) return;

            href = fullLink.Split(new[] { "/pls" }, StringSplitOptions.None)[0] + href;
            var response = HtmlProvider.GetResponse<string>(href);
            if (response.Error != null || string.IsNullOrEmpty(response.ReceivedData))
            {
                lowDetails.Error = response.Error ?? new ErrorModel {ErrorMsg = "Порожній контент"};
                return;
            }
            page = Converter.ConvertToHtmlDocument(response.ReceivedData);
            var liNodes = page.DocumentNode.SelectNodes("//form[@id='forma_sel']//li");
            if (liNodes == null) return;
            IEnumerable<HtmlNode> liNodesSmall = null;
            if (liNodes.Count > 21)
            {
                liNodesSmall = liNodes.Skip(Math.Max(0, liNodes.Count - 19));
                liNodes = null;
            }
            foreach (var li in liNodes ?? liNodesSmall)
            {
                if(li.SelectSingleNode("div//div[@class='nomer']") != null) continue;

                var voteDetails = li.SelectSingleNode("div//div[@class='fr_nazva']");
                var voteResults = voteDetails.SelectSingleNode("p").InnerText.RemoveOddSpaces().RemoveColon();
                var voteDate = "Дата голосування: " + li.SelectSingleNode("div//div[@class='fr_data']").InnerText.RemoveOddSpaces().RemoveColon().Replace(":", "_");
                var voteName = "Найменування голосування: " + voteDetails.SelectSingleNode("a").InnerText.RemoveOddSpaces().RemoveColon();
                var sulotion = "Рішення: " + voteResults.Substring(voteResults.IndexOf("Рішення", StringComparison.Ordinal));

                var pageDetailsRow = FillVotesResultForLawXml(voteResults);
                pageDetailsRow.Id = "VotingResults";
                pageDetailsRow.Name = "Результати голосування";
                pageDetailsRow.Value.AddMany(voteDate, voteName, sulotion);
                //добавляем поименные голосования
                var namedVoteResultsLink = voteDetails.SelectSingleNode(".//a").Attributes["href"].Value;
                if (!string.IsNullOrEmpty(namedVoteResultsLink))
                {
                    var namedVoteResultsFullLink = fullLink.Split(new[] { "/pls" }, StringSplitOptions.None)[0] + namedVoteResultsLink;
                    var voteFileLink = "URL голосування: " + namedVoteResultsFullLink;
                    pageDetailsRow.Value.Add(voteFileLink);

                    response = HtmlProvider.GetResponse<string>(namedVoteResultsFullLink);
                    if (response.Error != null)
                        throw new Exception(response.Error.ErrorMsg);
                    var votePage = Converter.ConvertToHtmlDocument(response.ReceivedData);
                    var generalSign = li.SelectSingleNode("//div[@class='block_pd']/div[@class='nomer']/a").InnerText.RemoveOddSpaces() + "#" + voteDate;
                    lowDetails.PageDetails.Add(new Record<List<string>>
                    {
                        Id = "NamedVotingResults",
                        Name = "Поіменні результати голосування",
                        GeneralSign = generalSign,
                        Value = new List<string>()
                    });
                    var namedVotesResults = new List<Record<List<string>>>
                    {
                        new Record<List<string>> { Value = new List<string>(), Id = "FractionResult", ParentBlockGeneralSign = generalSign },
                        new Record<List<string>> { Value = new List<string>(), Id = "DeputyResult", ParentBlockGeneralSign = generalSign }
                    };
                    ParseVotesByFractions(ref namedVotesResults, votePage);
                    namedVotesResults[0].Name = "Пофракційні результати голосування";
                    namedVotesResults[1].Name = "Депутатські результати голосування";
                    lowDetails.PageDetails.AddRange(namedVotesResults);
                    //добавляем rtf файл голосования
                    var file = CreateFile(votePage.DocumentNode.SelectSingleNode("//li[@id='01']/div[@class='vid_pr']"), "Голосування#Документ#" + LowNumber + "#" + voteDate.Replace(":","_") + "#");
                    if (file.Error != null)
                        throw new Exception(file.Error.ErrorMsg);
                    if (file.Content != null && !string.IsNullOrEmpty(file.FileName))
                        lowDetails.Files.Add(file);
                }
                lowDetails.PageDetails.Add(pageDetailsRow);
            }
        }

        /// <summary>
        /// метод возвращет информацию поименного голосования (по фракциям)
        /// </summary>
        /// <param name="namedvotesResults"></param>
        /// <param name="page">страница с результатами голосования</param>
        public void ParseVotesByFractions(ref List<Record<List<string>>> namedvotesResults, HtmlDocument page)
        {
            var liNodes = page.DocumentNode.SelectNodes("//div[@id='PlsqlBody']/ul/li[@style='border: 1px solid #DCDCDC;']/ul[@class='fr']/li");
            if (liNodes == null)
                throw new Exception("Не вдається знайти блоки поіменного голосування");
            foreach (var li in liNodes)
            {
                var fractionInfo = li.FirstChild;
                var fractionName = fractionInfo.SelectSingleNode("./center/b").InnerText.RemoveOddSpaces();
                var fractionCountAndVotes = fractionInfo.InnerText.Split(new[] { fractionName }, StringSplitOptions.RemoveEmptyEntries)[0]
                                                                        .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //var fractionNameTranslit = LowRecordNamesComparator.FirstOrDefault(t => t.Key == fractionName).Value.BlockName + "\n";
                fractionName += "\n";
                var generalFractionVotes = "FractionResult\nНазва фракції: " + fractionName + "Кількість депутатів: " + Regex.Match(fractionCountAndVotes[0], @"\d+").Value + "\n";
                generalFractionVotes += FillVotesResult(fractionCountAndVotes[1]);
                namedvotesResults[0].Value.Add(generalFractionVotes);
                foreach (var dep in li.SelectNodes("./ul/li"))
                {
                    var str = dep.InnerText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    namedvotesResults[1].Value.Add("DeputyResult\nНазва фракції: " + fractionName + "Ім'я: " + str[0] + "\nРішення: " + str[1]);
                }
            }
        }

        /// <summary>
        /// метод возвращает экземпляр Record, содержащий общее инфо о голосовании
        /// </summary>
        /// <param name="voteResults">строка для парсинга, содержащая общее ифно о голосовании</param>
        /// <param name="fractionNameTranslit">название фракции</param>
        private Record<List<string>> FillVotesResultForLawXml(string voteResults, string fractionNameTranslit = null)
        {
            var voteResultsArray = Regex.Split(voteResults, @"\s+");
            var pageDetails = new Record<List<string>>
            {
                Value = new List<string>
                {
                    fractionNameTranslit + "За: " + Regex.Match(voteResultsArray[0], @"\d+").Value,
                    fractionNameTranslit + "Проти: " + Regex.Match(voteResultsArray[1], @"\d+").Value,
                    fractionNameTranslit + "Утрималось: " + Regex.Match(voteResultsArray[2], @"\d+").Value,
                    fractionNameTranslit + "Не голосувало: " + Regex.Match(voteResultsArray[4], @"\d+").Value
                }
            };
            if (voteResultsArray[5].Contains("Відсутні:"))
                pageDetails.Value.Add(fractionNameTranslit + "Відсутньо: " + Regex.Match(voteResultsArray[5], @"\d+").Value);
            return pageDetails;
        }

        /// <summary>
        /// метод возвращает экземпляр Record, содержащий общее инфо о голосовании
        /// </summary>
        /// <param name="voteResults">строка для парсинга, содержащая общее ифно о голосовании</param>
        private string FillVotesResult(string voteResults)
        {
            var voteResultsArray = Regex.Split(voteResults, @"\s+");
            var list = new List<string>
            {
                "За: " + Regex.Match(voteResultsArray[0], @"\d+").Value,
                "Проти: " + Regex.Match(voteResultsArray[1], @"\d+").Value,
                "Утрималось: " + Regex.Match(voteResultsArray[2], @"\d+").Value,
                "Не голосувало: " + Regex.Match(voteResultsArray[4], @"\d+").Value
            };
            if (voteResultsArray[5].Contains("Відсутні:"))
                list.Add("Відсутньо: " + Regex.Match(voteResultsArray[5], @"\d+").Value);
            return list.Aggregate((current, next) => current + "\n" + next);
        }

        /// <summary>
        /// метод возвращает экземпляр FileModel
        /// </summary>
        /// <param name="item">объект для парсинга</param>
        /// <param name="lowName">имя законопроекта, необходимое для имени файла</param>
        /// <param name="partOfPageUrl">часть url для скачивания файла</param>
        private FileModel CreateFile(HtmlNode item, string lowName, string partOfPageUrl = null)
        {
            try
            {
                var href = item.SelectSingleNode("a[not(@target)]")?.Attributes["href"].Value;
                if (href == null) return new FileModel();
                var response = HtmlProvider.GetResponse<byte[]>(partOfPageUrl + href);
                if (response.Error != null)
                    throw FormattedExceptionCreator.CreateExc(response.Error);
                var file = new FileCreator().CreateFile(response.ReceivedData, response.FileType, lowName);
                if (file.Error != null)
                    throw new Exception(file.Error.ErrorMsg);
                return file;
            }
            catch (Exception e)
            {
                return new FileModel {
                    Error = e.Data.Values.Count > 0
                        ? e.Data.Values.OfType<ErrorModel>().First()
                        : new ErrorModel {ErrorMsg = e.Message}
                };
            }
        }

        /// <summary>
        /// метод изменяет имена у файлов с одинаковым названием
        /// </summary>
        /// <param name="files">коллекция файлов</param>
        private void ChangeFilesIdenticalNames(List<FileModel> files)
        {
            var filesWithSameName = files.GroupBy(f=>f.FileName).Where(grp => grp.Skip(1).Any()).SelectMany(g=>g).ToList();
            for (int i = 0; i < filesWithSameName.Count; i++)
                filesWithSameName[i].FileName += " №" + (i + 1).GetBitNumber();
        }
    }
}
