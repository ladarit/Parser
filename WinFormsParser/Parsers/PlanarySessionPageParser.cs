using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    class PlanarySessionPageParser : PageParser<List<string>>
    {
        private string _sessionWeekName = string.Empty;

        /// <summary>
        /// метод формирует коллекцию записей со страницы недельного расписания пленарного заседания
        /// </summary>
        /// <param name="html">разметка страницы недельного расписания пленарного заседания</param>
        /// <param name="checkBox">значение какого-либо чек-бокса</param>
        /// <param name="satellitePages">дополнительные url для парсинга</param>
        /// <param name="additParams">доп. параметры</param>
        /// <returns>возвращает коллекцию записей страницы недельного расписания пленарного заседания</returns>
        public override Page<List<string>> ParseDetails(string html, bool checkBox = false, string[] satellitePages = null, string additParams = null)
        {
            var sessionDetails = new Page<List<string>>
            {
                PageDetails = new List<Record<List<string>>>(),
                Files = new List<FileModel>()
            };

            //создаем xml документ страницы недельного расписания пленарного заседания
            var document = Converter.ConvertToHtmlDocument(html);
            _sessionWeekName = document.DocumentNode.SelectNodes("//h3").Last()?.InnerText.Replace("на ", "").RemoveOddSpaces();
            var pattern = new Regex(@"\d+\s+.+\s+\(\w+'*\w+\)");
            var tableRows = document.DocumentNode.SelectNodes("//table[@class='MsoNormalTable']")[2].SelectNodes("tr").ToList();
            var tableHeaders = document.DocumentNode.SelectNodes("//table[@class='MsoNormalTable']")[1].SelectNodes("tr/td").ToList();
            //находим строки, которые нужно удалить и удаляем
            var rowsForRemove = tableRows.Where(r =>
            {
                var td = r.SelectNodes("td")[2];
                var styleSign = td.SelectSingleNode("p")?.Attributes["align"]?.Value;
                if (styleSign == null || !styleSign.Equals("center")) return false;
                var text = td.InnerText.RemoveOddSpaces();
                return text.Equals("* * *") || !Regex.IsMatch(text, $@"{pattern}|читання$");
            }).ToList();
            rowsForRemove.ForEach(r => tableRows.Remove(r));

            //объединяем строки дней недели с текстом содержащим "читання" со следующими строками
            var rowsForConcat = tableRows.Where(r => Regex.IsMatch(r.SelectNodes("td")[2].InnerText.RemoveOddSpaces(), @"читання$")).ToList();
            rowsForConcat.ForEach(r =>
            {
                var index = tableRows.FindIndex(t => t.Equals(r));
                tableRows[index + 1].AppendChild(r.SelectNodes("td")[2].Clone());
            });
            rowsForConcat.ForEach(r => tableRows.Remove(r));

            //создаем коллекцию дней текущей недели
            var firstTr = tableRows[0].Clone();
            List<List<HtmlNode>> daysCollect = new List<List<HtmlNode>> { new List<HtmlNode> { firstTr } };
            tableRows.RemoveAt(0);
            var days = tableRows.Where(tr =>
            {
                var styleSign = tr.SelectNodes("td")[2].SelectSingleNode("p")?.Attributes["align"]?.Value;
                return styleSign != null && styleSign.Equals("center") && pattern.IsMatch(tr.SelectNodes("td")[2].InnerText.RemoveOddSpaces());
            }).ToList();
            for (int i = 0; i < days.Count; i++)
            {
                var dayRows = tableRows.TakeWhile(tr => !tr.Equals(days[i])).ToList();
                tableRows.RemoveRange(0, dayRows.Count);
                if (i == 0)
                    daysCollect[0].AddRange(dayRows);
                else
                    daysCollect.Add(dayRows);
            }
            daysCollect.Add(tableRows);

            //заполянем коллекцию, возвращаемую методом
            foreach (var day in daysCollect)
            {
                var tmp = day[0].InnerText.RemoveOddSpaces().Split(' ');
                var dayNumd = Int32.Parse(tmp[0]) > 10 ? tmp[0] : "0" + tmp[0];
                var month = MonthsComparator.FirstOrDefault(k => tmp[1].Contains(k.Key)).Value;
                var fullDate = "Дата:" + dayNumd + "." + month + "." + _sessionWeekName?.Split(' ')[2];
                var dayName = "Назва дня:" + Regex.Replace(tmp[2], @"[()]", "");
                day.RemoveAt(0);
                foreach (HtmlNode t in day)
                {
                    var dayDetails = new Record<List<string>> { Value = new List<string>() };
                    var tdCollect = t.SelectNodes("td");
                    for (int j = 0; j < tdCollect.Count; j++)
                    {
                        var text = tdCollect[j].InnerText.RemoveOddSpaces();
                        if (string.IsNullOrEmpty(text)) continue;
                        if (j < 4)
                            dayDetails.Value.Add((Regex.IsMatch(text, @"[.]") && j == 0 ? "Година" : tableHeaders[j].InnerText.RemoveOddSpaces()) + ":" + text);
                        if (j == 4)
                            dayDetails.Value.Add("Номер читання: " + text);
                    }
                    if (dayDetails.Value.Any())
                    {
                        dayDetails.GeneralSign = "Week";
                        dayDetails.Value.Insert(0, "Пленарний тиждень:" + _sessionWeekName);
                        dayDetails.Value.Insert(1, fullDate);
                        dayDetails.Value.Insert(2, dayName);
                        sessionDetails.PageDetails.Add(dayDetails);
                    }
                }
            }
            return sessionDetails;
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа недельного расписания пленарного заседания данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ недельного расписания пленарного заседания</param>
        /// <param name="sessionPageList">коллекция строк страницы недельного расписания пленарного заседания из метода ParseDetails</param>
        /// <param name="listOfCells">для данной реализации метода параметр пустой</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> sessionPageList, List<string> listOfCells = null)
        {
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, SessionPlanRecordNamesComparator);

            if (listOfCells != null && sessionPageList != null)
            {
                foreach (var row in sessionPageList)
                {
                    var dictElement = SessionPlanRecordNamesComparator.FirstOrDefault(t => t.Value.BlockName == row.GeneralSign).Value;
                    if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                    {
                        var element = doc.CreateElement(dictElement.ElementName);
                        AddDataToBlock(ref blocksCollection, element, dictElement.BlockName, SessionPlanRecordNamesComparator, dictElement.IsLiElement, doc, row);
                    }
                }
                listOfCells.Clear();
            }

            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes))
                root.AppendChild(xmlElement);
            rootCollect.Add(root);
            return rootCollect;
        }

        /// <summary>
        /// метод добавляет данные в блок
        /// </summary>
        /// <param name="blocksCollection">коллекция блоков</param>
        /// <param name="element"></param>
        /// <param name="blockName">имя блока, в который надо добавить данные</param>
        /// <param name="isLiElement"></param>
        /// <param name="doc">xml документ страницы недельного расписания пленарного заседания</param>
        /// <param name="row">запись, данные которой будут добавлены в element</param>
        /// <param name="dictionary"></param>
        protected void AddDataToBlock(ref List<XmlElement> blocksCollection, XmlElement element, string blockName, Dictionary<string, RecordOptions> dictionary,
                                      bool isLiElement, XmlDocument doc = null, Record<List<string>> row = null)
        {
            var block = blocksCollection.FirstOrDefault(b => b.GetAttribute("name") == blockName);
            if (block == null) return;

            if (doc != null && row != null)
            {
                if (isLiElement)
                {
                    var rowElem = doc.CreateElement("row");
                    if (row.Value != null && row.Value.Any())
                        foreach (var value in row.Value)
                        {
                            var names = value.Split(':');
                            var record = doc.CreateElement(dictionary.FirstOrDefault(n => n.Key.Equals(names[0])).Value.ElementName);
                            record.SetAttribute("comment", names[0].RemoveOddSpaces());
                            record.InnerText = names[1].RemoveOddSpaces();
                            rowElem.AppendChild(record);
                        }
                    block.AppendChild(rowElem);
                    return;
                }
                element.InnerText = row.Value.First().RemoveOddSpaces();
                element.SetAttribute("type", "record");
                block.AppendChild(element);
            }
        }
    }
}
