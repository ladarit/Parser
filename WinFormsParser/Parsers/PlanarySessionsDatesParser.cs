using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    class PlanarySessionsDatesPageParser : PlanarySessionPageParser
    {
        private List<DateTime> _sessionPeriod;

        /// <summary>
        /// метод формирует коллекцию записей со страницы графика пленарных заседаний текущей сессии
        /// </summary>
        /// <param name="html">объект страницы графика пленарных заседаний</param>
        /// <param name="checkbox">значение какого-либо чек-бокса</param>
        /// <param name="satellitePages">дополнительные url для парсинга</param>
        /// <param name="additParams">доп. параметры</param>
        /// <returns>возвращает коллекцию записей страницы графика пленарных заседаний</returns>
        public Page<List<string>> ParseDetails(HtmlNode html, bool checkbox = false, string[] satellitePages = null, string additParams = null)
        {
            var sessionDetails = new Page<List<string>>
            {
                PageDetails = new List<Record<List<string>>>(),
                Files = new List<FileModel>()
            };

            var header = html.SelectSingleNode("//div[@class='b_calendar']/h3").InnerText.RemoveOddSpaces();
            var convocation = Regex.Match(header, @"\s+(І?X|ІV|V?I{0,3})\s+").Value + " скликання";
            var sessionVerbalNumber = header.Split(new[] {" "}, StringSplitOptions.None)[0];
            var sessionDigitNumber = VerbalToNumbersComparator.FirstOrDefault(k => Regex.IsMatch(sessionVerbalNumber, k.Key)).Value;

            AddSingleValueDataToCollection(ref sessionDetails, "Номер сесії", sessionDigitNumber + " сесія");
            AddSingleValueDataToCollection(ref sessionDetails, "Номер скликання", convocation);

            //определяем временной промежуток проведения сессии в цифровом виде
            var monthsPeriod = html.SelectNodes("//ul[@class='calendar_list']/li").ToList();
            IdentifySessionPeriod(header, monthsPeriod);

            foreach (var month in monthsPeriod)
            {
                var monthName = month.SelectSingleNode("h5").InnerText;
                var tableRows = month.SelectSingleNode("table").SelectNodes("tr");
                var headers = tableRows.FindFirst("tr").SelectNodes("th").ToList();
                if (!headers.Any())
                    throw new Exception();
                foreach (var row in tableRows)
                {
                    var cells = row.SelectNodes("td");
                    if (!(cells != null && cells.Any())) continue;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        if(cells[i].HasAttributes && cells[i].Attributes["class"].Value.Contains("yellow"))
                        if (!string.IsNullOrWhiteSpace(cells[i].InnerText.RemoveOddSpaces()))
                        {
                            var pageDetailsRow = new Record<List<string>>
                            {
                                GeneralSign = "SessionDates",
                                Value = new List<string>
                                {
                                    "Дата: " + GetDate(cells[i].InnerText.RemoveOddSpaces(), monthName, _sessionPeriod),
                                    "Назва дня: " + WeekDaysComparator.FirstOrDefault(k=> k.Key.Equals(headers[i].InnerText.RemoveOddSpaces(), StringComparison.OrdinalIgnoreCase)).Value
                                }
                            };
                            sessionDetails.PageDetails.Add(pageDetailsRow);
                        }
                    }
                }
            }
            return sessionDetails;
        }

        /// <summary>
        /// метод наполняет корневой элемент xml документа графика пленарных заседаний данными из метода ParseDetails
        /// </summary>
        /// <param name="doc">xml документ графика пленарных заседаний</param>
        /// <param name="sessionPageList">коллекция строк страницы графика пленарных заседаний из метода ParseDetails</param>
        /// <param name="listOfCells">для данной реализации метода параметр пустой</param>
        /// <returns>возвращает корневой элемент xml документа</returns>
        public override List<XmlElement> FillRootElement(ref XmlDocument doc, List<Record<List<string>>> sessionPageList, List<string> listOfCells = null)
        {
            //Создаем корневой элемент будущего документа
            XmlElement root = doc.CreateElement("document");
            List<XmlElement> rootCollect = new List<XmlElement>();

            //Создаем блоки для всех записей будущего документа
            List<XmlElement> blocksCollection = CreateBlocks(doc, SessionDatesRecordNamesComparator);
            XmlElement element = doc.CreateElement("element");
            if (listOfCells != null && sessionPageList != null)
            {
                foreach (var row in sessionPageList)
                {
                    var dictElement = string.IsNullOrEmpty(row.GeneralSign)
                        ? SessionDatesRecordNamesComparator.FirstOrDefault(t => t.Key == row.Name).Value 
                        : SessionDatesRecordNamesComparator.FirstOrDefault(t => t.Value.BlockName == row.GeneralSign).Value;
                    if (!string.IsNullOrEmpty(dictElement.ElementName) && !string.IsNullOrEmpty(dictElement.BlockName))
                        element = doc.CreateElement(dictElement.ElementName);
                    AddDataToBlock(ref blocksCollection, element, dictElement.BlockName, SessionDatesRecordNamesComparator, dictElement.IsLiElement, doc, row);
                    
                }
                listOfCells.Clear();
            }

            foreach (var xmlElement in blocksCollection.Where(b => b.HasChildNodes))
                root.AppendChild(xmlElement);
            rootCollect.Add(root);
            return rootCollect;
        }

        /// <summary>
        /// метод возвращает коллекцию месяцев проведения сессии
        /// </summary>
        /// <param name="startMonth">начальный месяц</param>
        /// <param name="endMonth">конечный месяц</param>
        /// <param name="years">годы начала и конца сессии</param>
        private List<DateTime> GetDateTimePeriod(string startMonth, string endMonth, MatchCollection years)
        {
            var startMonthDigit = MonthsComparator.FirstOrDefault(k => startMonth.Contains(k.Key, StringComparison.OrdinalIgnoreCase)).Value;
            var endMonthDigit = MonthsComparator.FirstOrDefault(k => endMonth.Contains(k.Key, StringComparison.OrdinalIgnoreCase)).Value;
            var startDate = new DateTime(int.Parse(years[0].ToString()), int.Parse(startMonthDigit), 01);
            var endDate = new DateTime(int.Parse(years[years.Count - 1].ToString()), int.Parse(endMonthDigit), 01);
            List<DateTime> monthList = new List<DateTime>();
            while (startDate <= endDate)
            {
                monthList.Add(startDate);
                startDate = startDate.AddMonths(1);
            }
            return monthList;
        }

        /// <summary>
        /// метод возвращает дату переданного дня проведения заседания в цифровом виде
        /// </summary>
        /// <param name="dayNumder">номер дня</param>
        /// <param name="currentMonth">текущий месяц в вербальном выражении</param>
        /// <param name="monthList">список месяцев проведения сессии, нужный для определеня года текущего номера дня</param>
        private string GetDate(string dayNumder, string currentMonth, List<DateTime> monthList)
        {
            CultureInfo ci = new CultureInfo("uk-UA");
            var currentMonthDigit = MonthsComparator.FirstOrDefault(k => currentMonth.Contains(k.Key, StringComparison.OrdinalIgnoreCase)).Value;
            var date = monthList.FirstOrDefault(m => m.Month == int.Parse(currentMonthDigit));
            return new DateTime(date.Year, date.Month, int.Parse(dayNumder)).ToString(ci.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// метод определяет временной промежуток проведения сессии
        /// </summary>
        /// <param name="header">строка для парсинга</param>
        /// <param name="monthsPeriod">коллекция месяцев проведения сессии, содержащих инфрмацию для парсинга</param>
        private void IdentifySessionPeriod(string header, List<HtmlNode> monthsPeriod)
        {
            var years = Regex.Matches(Regex.Match(header, @"\(.*\)").ToString(), @"\d+");
            var startMonth = monthsPeriod.First().SelectSingleNode("h5").InnerText;
            var endMonth = monthsPeriod.Last().SelectSingleNode("h5").InnerText;
            _sessionPeriod = GetDateTimePeriod(startMonth, endMonth, years);
        }
    }
}
