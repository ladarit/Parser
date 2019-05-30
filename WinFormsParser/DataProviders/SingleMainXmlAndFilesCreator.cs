using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Parsers;
using HtmlAgilityPack;

namespace GovernmentParse.DataProviders
{
    public class SingleMainXmlAndFilesCreator : TransitionalFilesProvider
    {
        private readonly Dictionary<string, Func<XmlDocument, Page<List<string>>, List<string>, List<XmlElement>>> _controlNameToRootInstance;

        private readonly Dictionary<string, Func<object, bool, string[], string, Page<List<string>>>> _controlNameToParseInstance;

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SingleMainXmlAndFilesCreator()
        {
            _controlNameToParseInstance = new Dictionary<string, Func<object, bool, string[], string, Page<List<string>>>>
            {
                { @"^SaveLawsBtn|^SaveByLowNameBtn|^SaveLawsByDatePickerBtn", (receivedData, checkBox, urlsArrays, text) => new LowsPageParser().ParseDetails((string)receivedData, checkBox, urlsArrays, text) },
                { @"^SavePlenarySessionCalendarPlanBtn",(receivedData, checkBox, urlsArrays, text) => new PlanarySessionPageParser().ParseDetails((string)receivedData, checkBox, urlsArrays, text) },
                { @"^SavePlenarySessionDatesBtn", (receivedData, checkBox, urlsArrays, text) => new PlanarySessionsDatesPageParser().ParseDetails((HtmlNode)receivedData, checkBox, urlsArrays, text) },
                { @"^SaveСommitteesBtn", (receivedData, checkBox, urlsArrays, text) => new CommitteePageParser().ParseDetails((string)receivedData, checkBox, urlsArrays, text) },
                { @"^SaveFractionsBtn", (receivedData, checkBox, urlsArrays, text) => new FractionPageParser().ParseDetails((string)receivedData, checkBox, urlsArrays, text) }
            };

            _controlNameToRootInstance = new Dictionary<string, Func<XmlDocument, Page<List<string>>, List<string>, List<XmlElement>>>
            {
                { @"^SaveLawsBtn|^SaveByLowNameBtn|^SaveLawsByDatePickerBtn", (document, page, cells) => new LowsPageParser().FillRootElement(ref document, page.PageDetails, cells) },
                { @"^SavePlenarySessionCalendarPlanBtn", (document, page, cells) => new PlanarySessionPageParser().FillRootElement(ref document, page.PageDetails, cells) },
                { @"^SavePlenarySessionDatesBtn", (document, page, cells) => new PlanarySessionsDatesPageParser().FillRootElement(ref document, page.PageDetails, cells) },
                { @"^SaveСommitteesBtn", (document, page, cells) => new CommitteePageParser().FillRootElement(ref document, page.PageDetails) },
                { @"^SaveFractionsBtn", (document, page, cells) => new FractionPageParser().FillRootElement(ref document, page.PageDetails, cells) }
            };
        }

        public override ParseResult<XmlElement> GetBlankFiles(HtmlNode row, string[] satellitePage, string controlName, bool checkBoxOption = false)
        {
            XmlDocument doc = new XmlDocument();           
            Page<List<string>> pageFromRow;
            var rowParseResult = new ParseResult<XmlElement>
            {
                XmlDocuments = new List<XmlElement>(),
                Files = new List<FileModel>()
            };
            var listOfCells = new List<string>();
            switch (controlName)
            {
                case "SavePlenarySessionCalendarPlanBtn":
                    pageFromRow = ParseSessionsPlanRow(row, satellitePage, controlName, checkBoxOption);
                    break;
                case "SavePlenarySessionDatesBtn":
                    pageFromRow = ParseSessionsDatesRow(row, satellitePage, controlName, checkBoxOption);
                    break;
                default:
                    pageFromRow = ParseBaseRow(row, satellitePage, controlName, ref listOfCells, checkBoxOption);
                    break;
            }

            if (pageFromRow.Error != null)
                return new ParseResult<XmlElement> { Error = pageFromRow.Error };

            if (pageFromRow.Files != null)
                rowParseResult.Files.AddRange(pageFromRow.Files);
           
            var filesCollection = _controlNameToRootInstance.FirstOrDefault(r => Regex.IsMatch(controlName, r.Key)).Value(doc, pageFromRow, listOfCells);
            rowParseResult.XmlDocuments.AddRange(filesCollection);
            return rowParseResult;
        }

        /// <summary>
        /// метод возвращает информацию про законопроекты, либо комитеты, либо фракции
        /// </summary>
        /// <param name="row">строка для парсинга из главной страницы</param>
        /// <param name="satellitePages">дополнительные url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="listOfCells">ячейки строки</param>
        /// <param name="checkBoxOption">значение какого-либо чек-бокса</param>
        private Page<List<string>> ParseBaseRow(HtmlNode row, string[] satellitePages, string controlName, ref List<string> listOfCells, bool checkBoxOption = false)
        {
            try
            {
                Page<List<string>> pageDetailsList = new Page<List<string>>();
                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                {
                    if (cell.InnerHtml.Contains("href"))
                    {
                        var link = cell.SelectSingleNode("a").Attributes["href"].Value;
                        if (!link.ContainsAny("http://zakon.rada.gov.ua/go", "/pls/radan_gs09/ns_zal_frack"))
                        {
                            var fullLink = link.Contains("http") ? link : string.Format(satellitePages[1] + "{0}", link);

                            string[] urlsArray = new string[satellitePages.Length + 1];
                            satellitePages.CopyTo(urlsArray, 0);
                            urlsArray[urlsArray.Length - 1] = fullLink;

                            var responce = controlName.Equals("SaveFractionsBtn")
                                ? HtmlProvider.GetResponse<string>(fullLink, link.Contains("http"))
                                : HtmlProvider.GetResponse<string>(fullLink);

                            pageDetailsList = _controlNameToParseInstance.FirstOrDefault(r => Regex.IsMatch(controlName, r.Key))
                                                                         .Value(responce.ReceivedData, checkBoxOption, urlsArray, cell.InnerText);
                        }
                    }
                    cell.InnerHtml = cell.InnerHtml.Replace("\n", " ");
                    listOfCells.Add(cell.InnerText);
                }
                return pageDetailsList;
            }
            catch (Exception e)
            {
                _log.Error($"ParseBaseRow.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new Page<List<string>> { Error = new ErrorModel { ErrorMsg = "Помилка при пошуку інформації" }};
            }
        }

        /// <summary>
        /// метод возвращает информацию про планируемые дневные расписания пленарных заседаний
        /// </summary>
        /// <param name="row">строка для парсинга из главной страницы</param>
        /// <param name="satellitePages">дополнительный url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="checkBoxOption">значение какого-либо чек-бокса</param>
        private Page<List<string>> ParseSessionsPlanRow(HtmlNode row, string[] satellitePages, string controlName, bool checkBoxOption = false)
        {
            try
            {
                var responce = HtmlProvider.GetResponse<string>(satellitePages[1] + row.Attributes["href"].Value);
                return HtmlProvider.Is404Except(responce) ?
                    new Page<List<string>>()
                    : _controlNameToParseInstance.FirstOrDefault(r => Regex.IsMatch(controlName, r.Key)).Value(responce.ReceivedData, checkBoxOption, satellitePages, string.Empty);
            }
            catch (Exception e)
            {
                _log.Error($"ParseSessionsPlanRow.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new Page<List<string>> { Error = new ErrorModel {ErrorMsg = "Помилка при пошуку денного розкладу пленарних засідань" }};
            }
        }

        /// <summary>
        /// метод возвращает информацию про сесионный график пленарных заседаний
        /// </summary>
        /// <param name="data">информация для парсинга</param>
        /// <param name="satellitePages">дополнительный url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="checkBoxOption">значение какого-либо чек-бокса</param>
        private Page<List<string>> ParseSessionsDatesRow(HtmlNode data, string[] satellitePages, string controlName, bool checkBoxOption = false)
        {
            try
            {
                return _controlNameToParseInstance.FirstOrDefault(r => Regex.IsMatch(controlName, r.Key)).Value(data, checkBoxOption, satellitePages, string.Empty);
            }
            catch (Exception e)
            {
                _log.Error($"ParseSessionsDatesRow.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new Page<List<string>> { Error = new ErrorModel { ErrorMsg = "Помилка при пошуку сессійного розкладу пленарних засідань"} };
            }
        }
    }
}
