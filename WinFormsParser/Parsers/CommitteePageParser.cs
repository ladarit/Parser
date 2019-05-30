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
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace GovernmentParse.Parsers
{
    public class CommitteePageParser : PageParser<List<string>>
    {
        /// <summary>
        /// метод формирует коллекцию записей со страницы комитета
        /// </summary>
        /// <param name="html">разметка страницы комитета</param>
        /// <param name="checkBox">значение какого-либо чек-бокса</param>
        /// <param name="urlCollect">дополн. url</param>
        /// <param name="committeeName">название комитета</param>
        /// <returns>возвращает коллекцию записей страницы комитета</returns>
        public override Page<List<string>> ParseDetails(string html, bool checkBox = false, string[] urlCollect = null, string committeeName = null)
        {
            var committeeDetails = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            HtmlDocument document = Converter.ConvertToHtmlDocument(html);

            //добавляем в коллекцию записей название комитета
            if (committeeName != null)
                AddSingleValueDataToCollection(ref committeeDetails, "Назва комітету", committeeName, "CommitteeInfo");

            //добавляем в коллекцию записей номер созыва комитета
            var header = document.DocumentNode.SelectSingleNode("//h3/span").InnerText.RemoveOddSpaces();
            var convocation = Regex.Match(header, @"\s*(X?)(I{1,3}V?|I?XI{0,3}|VI{0,3})\s+").Value + " скликання";
            AddSingleValueDataToCollection(ref committeeDetails, "Скликання", convocation, "CommitteeInfo");

            //добавляем в коллекцию записей url страницы комитета
            AddSingleValueDataToCollection(ref committeeDetails, "Посилання", urlCollect?.Last(), "CommitteeInfo");

            HtmlNodeCollection tables = document.DocumentNode.SelectNodes("//table[@class='simple_info']");
            if (tables != null)
                for (int i = 0; i < tables.Count; i++)
                {
                    foreach (HtmlNode row in tables[i].SelectNodes("tr"))
                    {
                        var tdCells = row.SelectNodes("td");
                        if (!tdCells.Any()) continue;
                        var pageDetails = new Record<List<string>> { Value = new List<string>() };
                        if (i == 0)
                            pageDetails = ParseBaseComInfo(ref committeeDetails, urlCollect?[1], tdCells);

                        #region В ДАННЫЙ МОМЕНТ Разделы "главы комитета" и "секретариат" не нужны заказчику
                        //if (i == 1)
                        //    pageDetails = ParseComChairmans(tdCells);
                        //if (i == 2)
                        //    pageDetails = ParseSecretariate(tdCells);
                        #endregion

                        if (!String.IsNullOrEmpty(pageDetails?.Name) && pageDetails.Value.Any())
                            committeeDetails.PageDetails.Add(pageDetails);
                    }
                }
            else
            {
                HtmlNode ulElement = document.DocumentNode.SelectSingleNode("//ul[@class='level1']");
                if (ulElement != null)
                {
                    foreach (var liElement in ulElement.SelectNodes("li"))
                        committeeDetails.PageDetails.Add(new Record<List<string>>
                        {
                            Value = new List<string> { "Ім'я:" + liElement.InnerText },
                            GeneralSign = "Participants"
                        });
                }
            }
            return committeeDetails;
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа комитета данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ комитета</param>
        /// <param name="committeePageDetails">коллекция строк страницы комитета из метода ParseDetails</param>
        /// <param name="listOfCells">для данной реализации метода параметр пустой</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> committeePageDetails, List<string> listOfCells = null)
        {
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, CommitteeRecordNamesComparator);

            if (committeePageDetails == null)
            {
                rootCollect.Add(root); return rootCollect;
            }

            //создаем раздел "инфо о комитете"
            foreach (var row in committeePageDetails)
            {
                var dictElement = row.GeneralSign == "CommitteeInfo" ?
                    CommitteeRecordNamesComparator.FirstOrDefault(t => t.Key == row.Name).Value :
                    CommitteeRecordNamesComparator.FirstOrDefault(t => t.Value.BlockName == row.GeneralSign).Value;
                if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                {
                    XmlElement element = doc.CreateElement(dictElement.ElementName);
                    AddDataToBlock(ref element, ref blocksCollection, dictElement.BlockName, doc, row);
                }
            }

            #region В ДАННЫЙ МОМЕНТ Разделы "главы комитета" и "секретариат" не нужны заказчику
            ////создаем раздел "участники комитета"
            //var participantRecords = committeePageDetails.Where(t => t.GeneralSign == "Participants").ToList();
            //if (participantRecords.Any())
            //{
            //    var dictElement = CommitteeRecordNamesComparator.FirstOrDefault(t => t.Key == "Participants").Value;
            //    XmlElement element = doc.CreateElement(dictElement.ElementName);

            //    var sameParticipants = participantRecords.GroupBy(t => t.Name).Select(x => x);
            //    foreach (var partip in sameParticipants)
            //    {
            //        var participant = partip.ToList();
            //        if (participant.Count > 1)
            //        {
            //            participant.First().Value.Add(participant.Last().Value.First());
            //            participant.Remove(participant.Last());

            //            committeePageDetails.Remove(participant.Last());
            //            committeePageDetails.Where(t => t.Name == participant.First().Name).Select(c =>
            //            {
            //                c.Value = participant.First().Value;
            //                return c;
            //            });
            //        }
            //        AddDataToBlock(ref element, ref blocksCollection, dictElement.BlockName, doc, participant.First());
            //    }
            //}
            #endregion

            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes))
                root.AppendChild(xmlElement);

            rootCollect.Add(root);
            return rootCollect;
        }

        /// <summary>
        /// метод добавляет данные в блок
        /// </summary>
        /// <param name="element">элемент, данные из которого будут добавлены в блок</param>
        /// <param name="blocksCollection">коллекция блоков</param>
        /// <param name="blockName">имя блока, в который надо добавить данные</param>
        /// <param name="doc">xml документ законопроекта</param>
        /// <param name="row">запись, данные которой будут добавлены в element</param>
        private void AddDataToBlock(ref XmlElement element, ref List<XmlElement> blocksCollection, string blockName, XmlDocument doc = null, Record<List<string>> row = null)
        {
            var block = blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == blockName);
            if (block == null) return;

            if (doc != null && row != null)
            {
                if (row.GeneralSign == "Participants" ||row.GeneralSign == "CommitteeWork")
                {
                    var rowElem = doc.CreateElement("row");
                    if (row.Value != null && row.Value.Any())
                        foreach (var value in row.Value)
                        {
                            var names = value.Split(':');
                            var record = doc.CreateElement(CommitteeElementsNames.FirstOrDefault(n => n.Key == names[0]).Value);
                            record.SetAttribute("comment", names[0].RemoveOddSpaces());
                            record.InnerText = names[1].RemoveOddSpaces();
                            if(row.GeneralSign == "CommitteeWork")
                                block.AppendChild(record);
                            else
                                rowElem.AppendChild(record);
                        }
                    if (row.GeneralSign != "CommitteeWork")
                        block.AppendChild(rowElem);
                }
                else
                {
                    element.InnerText = row.Value.First().FormatToDateTimeOrText();
                    element.SetAttribute("type", "record");
                    block.AppendChild(element);
                }
            }
        }

        /// <summary>
        /// метод возвращает общую информацию о комитете
        /// </summary>
        /// <param name="committeeDetails">коллекция строк страницы комитета из метода ParseDetails</param>
        /// <param name="partOfLowPageUrl">часть url для парсинга участников комитета</param>
        /// <param name="cells">коллекция ячеек с инфо о комитете</param>
        /// <returns></returns>
        private Record<List<string>> ParseBaseComInfo(ref Page<List<string>> committeeDetails, string partOfLowPageUrl, HtmlNodeCollection cells)
        {
            var pageDetails = new Record<List<string>> { Value = new List<string>() };
            var href = cells[0].SelectSingleNode("a")?.Attributes["href"].Value;
            if (href != null)
            {
                if (href.Contains("p_komity_list?", StringComparison.OrdinalIgnoreCase))
                    ParseParticipantsPage(ref committeeDetails, partOfLowPageUrl + href);
                else if(cells[0].InnerText.Contains("Веб-сайт Комітету"))
                    AddSingleValueDataToCollection(ref committeeDetails, "Веб-сайт Комітету", href, "CommitteeInfo");
            }
            else
            {
                pageDetails.GeneralSign = "CommitteeInfo";
                pageDetails.Name = cells[0].InnerText.RemoveOddSpaces().RemoveColon();
                if (pageDetails.Name.Contains("Підстава для створення", StringComparison.OrdinalIgnoreCase))
                {
                    var text = cells[1].InnerText.RemoveOddSpaces().Split(new[] { "від" }, StringSplitOptions.None);
                    AddSingleValueDataToCollection(ref committeeDetails, pageDetails.Name, text[0].Trim(), "CommitteeInfo");
                    AddSingleValueDataToCollection(ref committeeDetails, pageDetails.Name + " за датою", text[1].FormatToDateTimeOrText().Trim(), "CommitteeInfo");
                }
                else
                    pageDetails.Value.Add(cells.Count > 1
                        ? pageDetails.Name.Contains("Кількісний склад") ? cells[1].InnerText.RemoveOddSpaces().Split(',')[0] : cells[1].InnerText.RemoveOddSpaces()
                        : cells[0].SelectSingleNode("a")?.Attributes["href"].Value);
            }
            return pageDetails;
        }

        /// <summary>
        /// метод парсит страницу участников комитета
        /// </summary>
        /// <param name="committeeDetails"></param>
        /// <param name="href"></param>
        private void ParseParticipantsPage(ref Page<List<string>> committeeDetails, string href)
        {
            var response = HtmlProvider.GetResponse<string>(href);

            if (response.Error != null) throw new Exception("Не вдається знайти сторінку з переліком членів комітету");

            HtmlDocument page = Converter.ConvertToHtmlDocument(response.ReceivedData);
            var table = page.DocumentNode.SelectSingleNode("//table[@class='striped Centered']");
            if (table == null) return;
            foreach (HtmlNode row in table.SelectNodes("tr"))
            {
                var data = row.SelectNodes("td");
                if (data.Count > 1 && !String.IsNullOrEmpty(data[0].InnerText) && !String.IsNullOrEmpty(data[1].InnerText))
                    committeeDetails.PageDetails.Add(new Record<List<string>>
                    {
                        Value = new List<string> { "Ім'я:" + data[0].InnerText.RemoveOddSpaces().RemoveColon(), "Посада:" + data[1].InnerText.RemoveOddSpaces() },
                        GeneralSign = "Participants"
                    });
            }
        }

        #region методы для глав комитета и секретариата
        //private Record<List<string>> ParseComChairmans(HtmlNodeCollection cells)
        //{
        //    return new Record<List<string>>
        //    {
        //        Value = new List<string> { cells[1].InnerText.RemoveOddSpaces() },
        //        Name = "Ім'я:" + cells[0].InnerText.RemoveOddSpaces().RemoveColon(),
        //        GeneralSign = "Participants"
        //    };
        //}

        //private Record<List<string>> ParseSecretariate(HtmlNodeCollection cells)
        //{
        //    var pageDetails = new Record<List<string>>
        //    {
        //        Value = new List<string>(),
        //        Name = "Ім'я:" + cells[0].InnerText.RemoveOddSpaces().RemoveColon(),
        //        GeneralSign = "Секретаріат"
        //    };
        //    var secretaryRecordValues = cells[1].InnerText?.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //    if (secretaryRecordValues != null)
        //    {
        //        foreach (var val in secretaryRecordValues)
        //        {
        //            var value = CommitteeElementsNames.FirstOrDefault(e => val.Contains(e.Key)).Key;
        //            pageDetails.Value.AddRange(secretaryRecordValues);
        //        }
        //    }
        //    return pageDetails;
        //}
        #endregion

        #region В ДАННЫЙ МОМЕНТ формирование расписания работы коммитетов не нужно заказчику
        //public List<List<HtmlNode>> ParseCommiteeWorkPlan(string html)
        //{
        //    try
        //    {
        //        var doc = Converter.ConvertToHtmlDocument(html);
        //        var committeeWorkPages = new List<List<HtmlNode>>();

        //        //находим срок проведения заседания
        //        var workDates = doc.DocumentNode.SelectNodes("//p").FirstOrDefault(p => Regex.IsMatch(p.InnerText.RemoveOddSpaces(), @"^на \d+.* року"))?.InnerText.RemoveOddSpaces();

        //        //получаем коллецию строк
        //        var pCollect = GetCollectionOfRecords(ref doc);

        //        //находим и исправляем неверные адреса
        //        CorrectBadAdresses(ref pCollect);

        //        //находим заголовки названий комитетов
        //        var committees = doc.DocumentNode.SelectNodes("//*[@align='center']").Where(p => Regex.IsMatch(p.InnerText.RemoveOddSpaces(), @"^(КОМІТЕТ|СПЕЦІАЛЬНА КОНТРОЛЬНА)")).ToList();

        //        //создаем буферный элемент
        //        var span = doc.CreateElement("span");
        //        //формируем коллекцию для каждого комитета
        //        foreach (var t in committees)
        //        {
        //            var startIndex = pCollect.FindIndex(p => p.Equals(t)) + 1;
        //            var comInfo = pCollect.Skip(startIndex).TakeWhile(p => !Regex.IsMatch(p.InnerText.RemoveOddSpaces(), "^(КОМІТЕТ|СПЕЦІАЛЬНА КОНТРОЛЬНА)")).ToList();
        //            if (!Regex.IsMatch(comInfo.First().InnerText.RemoveOddSpaces(), @"^\(.*\)$"))
        //            {
        //                var comName = comInfo.TakeWhile(c => !Regex.IsMatch(c.InnerText.RemoveOddSpaces(), @"^\(.*\)$")).ToList();
        //                comName.ForEach(n => { t.SelectNodes("b/span").First().InnerHtml += " " + n.InnerText; comInfo.Remove(n); });
        //            }
        //            comInfo.ForEach(n =>
        //            {
        //                if (!Regex.IsMatch(n.InnerText.RemoveOddSpaces(), @"^(M{0,4})(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(ІX|ІV|V?І{0,3}|(I){0,3}|ІI|IІ).\s*Законопроекти"))
        //                {
        //                    span.InnerHtml = n.InnerHtml;
        //                    var bNode = t.SelectSingleNode("b");
        //                    if (bNode == null)
        //                    {
        //                        var b = doc.CreateElement("b");
        //                        b.AppendChildren(t.ChildNodes);
        //                        t.RemoveAllChildren();
        //                        t.AppendChild(b);
        //                    }
        //                    t.SelectSingleNode("b").AppendChild(span.Clone());
        //                }
        //            });
        //        }

        //        //разбиваем коллекцию коммитетов по дням их заседаний
        //        var pattern = @"^\d+-*\d*\s*\w*$";
        //        committees = CreateCommiteesBySign(pattern, committees, doc);

        //        //переводим даты в числовой вид и разбиваем двойные даты, если они присутствуют
        //        ChangeDatesToNumerals(ref committees, pattern, span, workDates);

        //        //разбиваем коллекцию коммитетов по количеству адресов в одном комитете
        //        pattern = @"^\(.*\)$";
        //        committees = CreateCommiteesBySign(pattern, committees, doc);

        //        //разбиваем коллекцию коммитетов по количеству строк со временем
        //        pattern = @"[оО]+\s*\d+(.|:|\s*год\.\s*)\d+\s*(хв\.)?\s*$";
        //        committees = CreateCommiteesByTime(pattern, committees, doc);

        //        //формируем для каждого комитета коллекцию, соответствующую будущему файлу
        //        foreach (var commit in committees)
        //        {
        //            var allNodes = commit.SelectNodes("b/span").Where(s => !string.IsNullOrEmpty(s.InnerText.RemoveOddSpaces())).ToList();
        //            //находим делим тематики заседаний по признаку: связь с законами или с общими вопросами
        //            pattern = @"^(КОМІТЕТ|СПЕЦІАЛЬНА КОНТРОЛЬНА|\(.*\)$|(Початкова|Кінцева) дата)";
        //            var themesOfCommitWork = allNodes.Where(s => !Regex.IsMatch(s.InnerText.RemoveOddSpaces(), pattern)).ToList();
        //            var comInfo = allNodes.Except(themesOfCommitWork).ToList();
        //            var lowThemes = themesOfCommitWork.TakeWhile(s => !Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^Інші\s*питання")).ToList();
        //            var questThemes = themesOfCommitWork.Except(lowThemes).ToList();
        //            if (questThemes.Any() && questThemes.Count > 1)
        //            {
        //                allNodes.Remove(questThemes[0]);
        //                questThemes.RemoveAt(0);
        //            }

        //            //среди тем с общими вопросами находим тематики, которые имеют порядковый номер и объединяем каждую из них с главной тематикой и добавляем в итоговую коллекцию
        //            if (lowThemes.Any())
        //                UnionChildElementsWithParentElem(ref allNodes, ref lowThemes, "lowTheme");
        //            if (questThemes.Any())
        //                UnionChildElementsWithParentElem(ref allNodes, ref questThemes, "questTheme");

        //            var counter = 1;
        //            foreach (var theme in lowThemes)
        //            {
        //                pattern = @"\(.*р\.\s*№+.*\)|р\.\s*№+.*\,?";
        //                theme.InnerHtml = "Тема питання: " + theme.InnerHtml.Replace("\n", " ").Replace(":", "-").RemoveOddSpaces();
        //                var innerText = theme.InnerText.Replace("\n", " ").RemoveOddSpaces();
        //                //если в строке есть номера законопроектов
        //                if (Regex.IsMatch(innerText, pattern))
        //                {
        //                    innerText = Regex.Match(innerText, @"р\.\s*№+.*\,?").Value;
        //                    innerText = Regex.Replace(innerText, @"р\.\s*№+", "р.№");
        //                    innerText = innerText.Substring(innerText.IndexOf("№", StringComparison.Ordinal) + 1);
        //                    var mathes = Regex.Matches(innerText, @"\d+-?\/?[\wа-яєії]+");
        //                    foreach (var t in mathes)
        //                        if (Regex.IsMatch(t.ToString().Trim(), @"^\d+"))
        //                            FillCommitteeWorkCollection(ref committeeWorkPages, ref counter, doc, comInfo, "Номер законопроекта:", t.ToString().Replace(")", ""), theme);
        //                }
        //                else
        //                    FillCommitteeWorkCollection(ref committeeWorkPages, ref counter, doc, comInfo, "Номер законопроекта:", "відсутній", theme);
        //            }
        //            foreach (var theme in questThemes)
        //            {
        //                theme.InnerHtml = "Тема питання: " + theme.InnerText.Replace("\n", " ").Replace(":", "-").RemoveOddSpaces();
        //                FillCommitteeWorkCollection(ref committeeWorkPages, ref counter, doc, comInfo, "Інше питання:", "інші питання", theme);
        //            }
        //        }
        //        return committeeWorkPages;
        //    }
        //    catch (Exception e)
        //    {
        //        EventsLogger.Logger("ERROR! ParseCommiteeWorkPlan", e.Message + " StackTrace:" + e.StackTrace, "error");
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// метод наполняет итоговую коллекцию перед созданием файлов
        ///// </summary>
        ///// <param name="committeeWorkPages"></param>
        ///// <param name="counter"></param>
        ///// <param name="doc"></param>
        ///// <param name="comInfo"></param>
        ///// <param name="textBeforeColumn"></param>
        ///// <param name="textAfterColumn"></param>
        ///// <param name="theme"></param>
        //private void FillCommitteeWorkCollection(ref List<List<HtmlNode>> committeeWorkPages, ref int counter, HtmlDocument doc,
        //                                         List<HtmlNode> comInfo, string textBeforeColumn, string textAfterColumn, HtmlNode theme)
        //{
        //    //добавляем уникальную информацию
        //    var elem = doc.CreateElement("span");
        //    elem.InnerHtml = textBeforeColumn + textAfterColumn;
        //    var page = new List<HtmlNode> { elem.Clone(), theme };
        //    //добавляем каунтер файла
        //    elem.InnerHtml = counter.ToString();
        //    page.Add(elem.Clone());
        //    //добавляем общую информацию
        //    page.AddRange(comInfo);
        //    committeeWorkPages.Add(page);
        //    counter++;
        //}

        ///// <summary>
        ///// метод объединяет строки, имеющие порядковый номер с их главной строкой
        ///// </summary>
        ///// <param name="allNodes"></param>
        ///// <param name="themes"></param>
        ///// <param name="theme"></param>
        //private void UnionChildElementsWithParentElem(ref List<HtmlNode> allNodes, ref List<HtmlNode> themes, string theme)
        //{
        //    var pattern = @"^(\d+\s?\)|-)|;$";
        //    var headerThemes = themes.Where(s => Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @":$|^Законопроекти")).ToList();
        //    if (!headerThemes.Any()) return;
        //    foreach (var headerTheme in headerThemes)
        //    {
        //        var index = themes.FindIndex(t => t.Equals(headerTheme)) + 1;
        //        var childThemes = themes.Skip(index).TakeWhile(h => Regex.IsMatch(h.InnerText.RemoveOddSpaces(), theme.Equals("questTheme") ? pattern : @"\(.*№+.*\)")).ToList();

        //        if (childThemes.Any() && Regex.IsMatch(childThemes.Last().InnerText.RemoveOddSpaces(), @";$"))
        //        {
        //            var innerIndex = themes.FindIndex(t => t.Equals(childThemes.Last()));
        //            if (innerIndex > -1 && innerIndex < themes.Count)
        //            {
        //                var nextElem = themes[themes.FindIndex(t => t.Equals(childThemes.Last())) + 1];
        //                if (nextElem != null && Regex.IsMatch(nextElem.InnerText.RemoveOddSpaces(), @"\.$"))
        //                    childThemes.Add(nextElem);
        //            }
        //        }

        //        foreach (HtmlNode t in childThemes)
        //            t.InnerHtml = headerTheme.InnerText.RemoveOddSpaces().RemoveColon() + " - " + Regex.Replace(t.InnerText.Replace("\n", " ").RemoveOddSpaces(), pattern, " ");

        //        if (theme.Equals("lowTheme") && Regex.IsMatch(headerTheme.InnerText.RemoveOddSpaces(), @"\(.*№+.*\)")) continue;
        //        themes.Remove(headerTheme);
        //        allNodes.Remove(headerTheme);
        //    }
        //}

        ///// <summary>
        ///// метод возвращает коллекцию строк, удаляя пустые и невидимые элементы
        ///// </summary>
        ///// <param name="doc"></param>
        //private List<HtmlNode> GetCollectionOfRecords(ref HtmlDocument doc)
        //{
        //    try
        //    {
        //        //удаляем невидимые элементы из документа
        //        var pHasEmptyNodes = doc.DocumentNode.SelectNodes("html/body/div/p").Where(n => n.InnerHtml.Contains("display:none")).ToList();
        //        for (var i = 0; i < pHasEmptyNodes.Count; i++)
        //            pHasEmptyNodes[i] = DeleteHiddenElements(pHasEmptyNodes[i]);

        //        //получаем коллекцию строк документа, исключая строки, состоящие из нижних подчеркиваний
        //        var pCollect = doc.DocumentNode.SelectSingleNode("//div[@class='WordSection1']").ChildNodes
        //            .Where(p => !Regex.IsMatch(p.InnerText.RemoveOddSpaces(), @"^\s*_+.+_+$|^\s*$")).ToList();

        //        //удаляем первые 4 элемента коллекции
        //        pCollect.RemoveRange(0, 4);
        //        //удаляем последние 2 элемента коллекции
        //        pCollect.RemoveRange(pCollect.IndexOf(pCollect.Last()) - 1, 2);

        //        for (var i = 0; i < pCollect.Count; i++)
        //        {
        //            //находим и удаляем пустые ноды
        //            var emptyNodes = pCollect[i].SelectNodes("*").Where(n => string.IsNullOrEmpty(n.InnerText.RemoveOddSpaces())).ToList();
        //            emptyNodes.ForEach(e => pCollect[i].RemoveChild(e));

        //            //pCollect[i] = DivideJoinedRecords(pCollect[i]);

        //            //находим и разъединяем обединенные строки, имеющие перенос <br> 
        //            var style = pCollect[i].Attributes["style"]?.Value;
        //            if (string.IsNullOrEmpty(style) || !pCollect[i].Attributes["style"].Value.Contains("text-align:center") 
        //                || Regex.IsMatch(pCollect[i].InnerText.RemoveOddSpaces(), @"\(.*\)$"))
        //                continue;
        //            var bElems = pCollect[i].SelectNodes("b")?.ToList();
        //            if (bElems == null || bElems.Count <= 1) continue;
        //            for (var j = 1; j < bElems.Count; j++)
        //            {
        //                if (!bElems[j].InnerHtml.Contains("<br>")) continue;
        //                var pElem = pCollect[i].Clone();
        //                pElem.RemoveChild(pElem.FirstChild);
        //                pCollect[i].RemoveChild(bElems[j]);
        //                pCollect.Insert(i + 1, pElem);
        //            }
        //        }
        //        return pCollect;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        ////private HtmlNode DivideJoinedRecords(HtmlNode pElement)
        ////{
        ////    var a = pElement.Attributes["align"]?.Value; 
        ////    if (!pElement.HasChildNodes || pElement.Attributes["align"] != null) return pElement;
        ////    if (Regex.IsMatch(pElement.InnerText.RemoveOddSpaces(), @"Проект.*\.\s*Проект"))
        ////    {
        ////        var recordsToSplit = pElement.ChildNodes;
        ////    }
        ////    //var childs = pElement.ChildNodes;
        ////    //if (childs.Count > 2)
        ////    //{
        ////    //    var countOfRecords = childs.Where(c => Regex.IsMatch(c.InnerText.RemoveOddSpaces(), @"^Проект Закону")).ToList();
        ////    //    if (countOfRecords.Count > 1)
        ////    //    {

        ////    //    }
        ////    //}
        ////    return pElement;
        ////}

        ///// <summary>
        ///// метод формирует количество коммитетов по паттерну регулярного выражения
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <param name="committeesCollect"></param>
        ///// <param name="doc"></param>
        ///// <returns></returns>
        //private List<HtmlNode> CreateCommiteesBySign(string pattern, List<HtmlNode> committeesCollect, HtmlDocument doc)
        //{
        //    try
        //    {
        //        var committees = committeesCollect;
        //        for (int i = 0; i < committees.Count; i++)
        //        {
        //            var spanCollect = committees[i].SelectNodes("b/span");
        //            var daysCount = FindDaysElements(pattern, spanCollect);
        //            if (daysCount.Count <= 1) continue;
        //            for (int j = 1; j < daysCount.Count; j++)
        //            {
        //                var startIndex = spanCollect.IndexOf(daysCount[j]) + 1;
        //                //создаем буферный элемент
        //                var pElem = doc.CreateElement("p");
        //                pElem.AppendChild(doc.CreateElement("b"));
        //                var tmpComitDay = spanCollect.Skip(startIndex).TakeWhile(p => !Regex.IsMatch(p.InnerText.RemoveOddSpaces(), pattern)).ToList();
        //                tmpComitDay.Insert(0, spanCollect.First());
        //                if (pattern.Equals(@"^\d+-*\d*\s*\w*$"))
        //                {
        //                    if (tmpComitDay.FirstOrDefault(e => Regex.IsMatch(e.InnerText.RemoveOddSpaces(), @"^\(.*\)$")) == null &&
        //                        Regex.IsMatch(spanCollect[startIndex - 2].InnerText.RemoveOddSpaces(), @"^\(.*\)$"))
        //                        tmpComitDay.Insert(1, spanCollect[startIndex - 2]);
        //                }
        //                tmpComitDay.Insert(1, daysCount[j]);
        //                if (pattern.Equals(@"^\(.*\)$"))
        //                {
        //                    var dates = spanCollect.Where(s => Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^(Початкова|Кінцева) дата")).ToList();
        //                    if (dates.Any())
        //                    {
        //                        List<HtmlNode> datesClone = new List<HtmlNode>();
        //                        dates.ForEach(d => datesClone.Add(d.Clone()));
        //                        tmpComitDay.InsertRange(2, datesClone);
        //                    }
        //                }
        //                tmpComitDay.ForEach(s =>
        //                {
        //                    pElem.SelectSingleNode("b").AppendChild(s.Clone());
        //                    if (!Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^(КОМІТЕТ|СПЕЦІАЛЬНА КОНТРОЛЬНА|(Початкова|Кінцева) дата)"))
        //                        committees[i].SelectSingleNode("b").RemoveChild(s);
        //                });
        //                committees.Insert(i+1, pElem);
        //            }
        //        }
        //        return committees;
        //    }
        //    catch (Exception e)
        //    {
        //        EventsLogger.Logger("ERROR! CreateCommiteesBySign", e.Message + " StackTrace:" + e.StackTrace, "error");
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// метод метод формирует количество коммитетов по количеству вхождений строки со временем
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <param name="committeesCollect"></param>
        ///// <param name="doc"></param>
        ///// <returns></returns>
        //private List<HtmlNode> CreateCommiteesByTime(string pattern, List<HtmlNode> committeesCollect, HtmlDocument doc)
        //{
        //    var committees = committeesCollect;
        //    for (int i = 0; i < committees.Count; i++)
        //    {
        //        var spanCollect = committees[i].SelectNodes("b/span");
        //        var daysCount = FindDaysElements(pattern, spanCollect);
        //        if(!daysCount.Any()) continue;
        //        foreach (HtmlNode t in daysCount)
        //        {
        //            var startIndex = spanCollect.IndexOf(t) + 1;
        //            //создаем буферный элемент
        //            var pElem = doc.CreateElement("p");
        //            pElem.AppendChild(doc.CreateElement("b"));
        //            var tmpComitDay = spanCollect.Skip(startIndex).TakeWhile(p => !Regex.IsMatch(p.InnerText.RemoveOddSpaces(), pattern)).ToList();
        //            tmpComitDay.Insert(0, spanCollect.First());
        //            tmpComitDay.Insert(1, spanCollect.First(s => Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^\(.*\)$")).Clone());
        //            tmpComitDay[1].InnerHtml = Regex.Replace(tmpComitDay[1].InnerHtml, @"[оО]+\s*\d+(.|:|\s*год\.\s*)\d+\s*(хв\.)?\s*", t.InnerText);
        //            var dates = spanCollect.Where(s => Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^(Початкова|Кінцева) дата")).ToList();
        //            if (dates.Any())
        //            {
        //                List<HtmlNode> datesClone = new List<HtmlNode>();
        //                dates.ForEach(d => datesClone.Add(d.Clone()));
        //                tmpComitDay.InsertRange(2, datesClone);
        //            }
        //            tmpComitDay.ForEach(s =>
        //            {
        //                pElem.SelectSingleNode("b").AppendChild(s.Clone());
        //                if (!Regex.IsMatch(s.InnerText.RemoveOddSpaces(), @"^(КОМІТЕТ|СПЕЦІАЛЬНА КОНТРОЛЬНА|(Початкова|Кінцева) дата|\(.*\)$)"))
        //                    committees[i].SelectSingleNode("b").RemoveChild(s);
        //            });
        //            committees[i].SelectSingleNode("b").RemoveChild(t);
        //            committees.Insert(i + 1, pElem);
        //        }
        //    }
        //    return committees;
        //}

        ///// <summary>
        ///// метод переводит даты заседаний комитетов в числовой вид и разбивает двойные даты, если они присутствуют
        ///// </summary>
        ///// <param name="committees"></param>
        ///// <param name="pattern"></param>
        ///// <param name="span"></param>
        ///// <param name="workDates"></param>
        //private void ChangeDatesToNumerals(ref List<HtmlNode> committees, string pattern, HtmlNode span, string workDates)
        //{
        //    var dateArray = workDates.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //    var yearFormdateArray = dateArray.LastOrDefault(e => Regex.IsMatch(e, @"\d+"));
        //    var wordsFormdateArray = dateArray.Where(e => Regex.IsMatch(e, @"\w[а-яєіїА-ЯЄІЇ]+")).ToList();
        //    var endOfWorkDates = "." + MonthsComparator.FirstOrDefault(k => wordsFormdateArray[1].Contains(k.Key)).Value + "." + yearFormdateArray;
        //    foreach (var commit in committees)
        //    {
        //        var spanCollect = commit.SelectNodes("b/span");
        //        var daysCount = FindDaysElements(pattern, spanCollect);
        //        foreach (var day in daysCount)
        //        {
        //            var startDate = "Початкова дата:";
        //            var textDate = day.InnerText.Replace("\n", " ").RemoveOddSpaces();
        //            var date = new string[2];
        //            if (Regex.IsMatch(textDate, @"^\d+\w[а-яєіїА-ЯЄІЇ]+"))
        //            {
        //                var stackofDate = new Stack<char>();
        //                var stackofMonth = new Stack<char>();
        //                for (var i = textDate.Length - 1; i >= 0; i--)
        //                {
        //                    if (char.IsNumber(textDate[i]))
        //                        stackofDate.Push(textDate[i]);
        //                    else
        //                        stackofMonth.Push(textDate[i]);
        //                }
        //                date[0] = new string(stackofDate.ToArray());
        //                date[1] = new string(stackofMonth.ToArray());
        //            }
        //            else
        //                date = textDate.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //            var dateElem = commit.SelectNodes("b/span").FirstOrDefault(s => s.Equals(day));
        //            if (dateElem == null) continue;
        //            //если двойная дата
        //            if (Regex.IsMatch(date[0], @"^\d+-\d+$"))
        //            {
        //                var datesArray = date[0].Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
        //                for (int i = 0; i < datesArray.Length; i++)
        //                {
        //                    if (Int32.Parse(datesArray[i]) < 10)
        //                        datesArray[i] = "0" + datesArray[i];
        //                }
        //                dateElem.InnerHtml = startDate + datesArray[0] + endOfWorkDates;
        //                span.InnerHtml = "Кінцева дата:" + datesArray[1] + endOfWorkDates;
        //                commit.SelectSingleNode("b").AppendChild(span.Clone());
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    date[0] = Int32.Parse(date[0]) < 10 ? "0" + date[0] : date[0];
        //                    dateElem.InnerHtml = startDate + date[0] + endOfWorkDates;

        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e);
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// метод возвращает дочерние элементы нода комитета, соответствующие переданному шаблону
        ///// </summary>
        ///// <param name="pattern"></param>
        ///// <param name="spanCollect"></param>
        ///// <returns></returns>
        //private List<HtmlNode> FindDaysElements(string pattern, HtmlNodeCollection spanCollect)
        //{
        //    return spanCollect.Where(p => Regex.IsMatch(p.InnerText.RemoveOddSpaces(), pattern)).ToList();
        //}

        ///// <summary>
        ///// метод удаляет невидимые элементы
        ///// </summary>
        ///// <param name="element"></param>
        ///// <returns></returns>
        //private HtmlNode DeleteHiddenElements(HtmlNode element)
        //{
        //    if (!element.HasChildNodes || !element.InnerHtml.Contains("display:none"))
        //        return element;

        //    var node = element.SelectNodes("*")
        //                    .Where(n => n.HasAttributes && n.Attributes["style"] != null
        //                            && n.Attributes["style"].Value.Contains("display:none") 
        //                            || n.InnerHtml.Contains("display:none")).ToList();

        //    if (!node.Any())
        //    {
        //        var subNodes = element.ChildNodes;
        //        foreach (var subNode in subNodes)
        //            DeleteHiddenElements(subNode);
        //    }
        //    foreach (HtmlNode n in node)
        //    {
        //        if (n.InnerHtml.Contains("display:none"))
        //            DeleteHiddenElements(n);
        //        else
        //            n.Remove();
        //    }
        //    return element;
        //}

        ///// <summary>
        ///// метод исправляет кривые адреса
        ///// </summary>
        ///// <param name="pCollect"></param>
        //private void CorrectBadAdresses(ref List<HtmlNode> pCollect)
        //{
        //    var adresses = pCollect.Where(p => p.HasAttributes && p.Attributes["style"] != null && p.Attributes["style"].Value.Contains("text-align:center")).ToList();
        //    var badAdresses = adresses.Where(p => Regex.IsMatch(p.InnerText.RemoveOddSpaces(), @"^([^\(].*\))|(.*\)(\.|,|;))$")).ToList();
        //    foreach (HtmlNode t in badAdresses)
        //    {
        //        var splitedAdrees = t.InnerText.RemoveOddSpaces().Split(' ');
        //        var innerHtmlBegin = t.InnerHtml.Split(new[] { splitedAdrees.First() }, StringSplitOptions.None).First();
        //        var innerHtmlEnd = t.InnerHtml.Split(new[] { splitedAdrees.Last() }, StringSplitOptions.None).Last();
        //        var rightAdress = (!splitedAdrees.First().Contains("(") ? "(" + t.InnerText : t.InnerText).RemoveOddSpaces();
        //        rightAdress = rightAdress.Substring(0, rightAdress.LastIndexOf(")", StringComparison.Ordinal) + 1);
        //        t.InnerHtml = innerHtmlBegin + rightAdress + innerHtmlEnd;
        //    }
        //}

        ///// <summary>
        ///// метод возвращает экземпляр класса Page для одной тематики заседания
        ///// </summary>
        ///// <param name="collection"></param>
        ///// <returns></returns>
        //public Page<List<string>> CreateCommitteeWorkPage(List<HtmlNode> collection)
        //{
        //    try
        //    {
        //        var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
        //        var row = new Record<List<string>> { Value = new List<string>(), GeneralSign = "CommitteeWork" };
        //        var textnumb = collection.First(c => Regex.IsMatch(c.InnerText, @"^\d+$")).InnerText;
        //        var numb = Int32.Parse(textnumb);
        //        if (numb >= 10 && numb < 100)
        //            textnumb = "00" + textnumb;
        //        else if (numb >= 100 && numb < 1000)
        //            textnumb = "0" + textnumb;
        //        else if (numb < 10)
        //            textnumb = "000" + textnumb;
        //        var number = "Номер файлу:" + textnumb;
        //        var theme = "Тема засідання:" + collection.First(c => Regex.IsMatch(c.InnerText, @"^Тема питання")).InnerText.Split(':')[1].RemoveOddSpaces();
        //        var lowNumber = collection.First(c => Regex.IsMatch(c.InnerText, @"^(Номер законопроекта|Інше питання):")).InnerText;
        //        var comName = "Назва комітету:" + collection.First(c => Regex.IsMatch(c.InnerText, @"^(КОМІТЕТ|СПЕЦІАЛЬНА)")).InnerText.Replace("\n", " ").RemoveOddSpaces().FirstCharToUpper();
        //        var adresstext = collection.First(c => Regex.IsMatch(c.InnerText.RemoveOddSpaces(), @"^\(.*\)$")).InnerText.Replace("\n", " ").RemoveOddSpaces();
        //        var adress = "Адреса:" + Regex.Replace(adresstext, @"\(|\)", "").Replace("/", "_");
        //        var startDate = collection.First(c => Regex.IsMatch(c.InnerText, @"^Початкова дата"))?.InnerText;
        //        var endDate = collection.FirstOrDefault(c => Regex.IsMatch(c.InnerText, @"^Кінцева дата"))?.InnerText;

        //        row.Value.AddMany(comName, startDate, number, theme, lowNumber, adress);
        //        if (!string.IsNullOrEmpty(endDate))
        //            row.Value.Add(endDate);
        //        page.PageDetails.Add(row);
        //        return page;

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}
        #endregion
    }
}
