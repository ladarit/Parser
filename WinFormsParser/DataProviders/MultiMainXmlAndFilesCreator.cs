using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Parsers;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.DataProviders
{
    public class MultiMainXmlAndFilesCreator : TransitionalFilesProvider
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// метод возвращает экземпляр ParseResult 
        /// </summary>
        /// <param name="row">строка таблицы депутатов</param>
        /// <param name="satellitePage">доп. url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="isSavePhoto">значение чек-бокса сохранения фото</param>
        public override ParseResult<XmlElement> GetBlankFiles(HtmlNode row, string[] satellitePage, string controlName, bool isSavePhoto = false)
        {
            XmlDocument doc = new XmlDocument();
            Page<List<string>> pageFromRow = new Page<List<string>>();
            var rowParseResult = new ParseResult<XmlElement>
            {
                XmlDocuments = new List<XmlElement>(),
                Files = pageFromRow.Files ?? new List<FileModel>()
            };
            List<Page<List<string>>> listOfpages = new List<Page<List<string>>>();
            object instance = new object();
            switch (controlName)
            {
                case "SaveDeputyBtn":
                case "SaveDepVotingBtn":
                case "SaveDepSpeechesBtn":
                case "SaveDepQueriesBtn":
                case "SaveDepLawActivityBtn":
                    listOfpages = ParseDeputieInfo(row, satellitePage, controlName, isSavePhoto);
                    instance = new DeputyPageParser();
                    break;
                    #region В ДАННЫЙ МОМЕНТ формирование расписания работы коммитетов не нужно заказчику
                    //case "SaveСommitteesWorkBtn":
                    //    listOfpages = ParseCommitteeWorkRow(row, satellitePage);
                    //    instance = new CommitteePageParser();
                    //    break;
                    #endregion
            }
            if (listOfpages[0].Error != null)
                return new ParseResult<XmlElement> { Error = listOfpages[0].Error };

            foreach (var page in listOfpages)
            {
                rowParseResult.XmlDocuments.AddRange(((PageParser<List<string>>) instance).FillRootElement(ref doc, page.PageDetails));
                if (page.Files != null)
                    rowParseResult.Files.AddRange(page.Files);
            }

            return rowParseResult;
        }

        /// <summary>
        /// метод возвращает информацию про депутатов
        /// </summary>
        /// <param name="row">строка для парсинга из главной страницы</param>
        /// <param name="satellitePage">дополнительный url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="isSavePhoto">значение чек-бокса сохранения фото</param>
        private List<Page<List<string>>> ParseDeputieInfo(HtmlNode row, string[] satellitePage, string controlName, bool isSavePhoto)
        {
            try
            {
                //находим ссылку на страницу депутата
                var linkElem = row.SelectSingleNode("p/a");
                var deputyPagelink = linkElem.Attributes["href"].Value;

                //находим имя депутата
                var deputyName = linkElem.InnerText.RemoveOddSpaces();
               
                switch (controlName)
                {
                    case "SaveDepVotingBtn":
                        return CreatePagesCollection(new DeputyVotePageParser(), deputyPagelink, satellitePage[2], @"ns_(arh_)*dep\?vid=1", "Ім'я для голосування", deputyName);
                    case "SaveDepSpeechesBtn":
                        return CreatePagesCollection(new DeputySpeechesPageParser(), deputyPagelink, satellitePage[2], @"ns_(arh_)*dep\?vid=4", "Ім'я для виступу", deputyName);
                    case "SaveDepQueriesBtn":
                        return CreateSinglePage(new DeputyQueriesPageParser(), deputyPagelink, satellitePage, "Ім'я для запиту", deputyName);
                    case "SaveDepLawActivityBtn":
                        return CreateSinglePage(new DeputyLawActivityPageParser(), deputyPagelink, satellitePage, "Ім'я для законтв. діяльності", deputyName);
                    default:
                        var responce = HtmlProvider.GetResponse<string>(deputyPagelink, useUtf8Encoding: true);
                        if (responce.Error != null)
                            return new List<Page<List<string>>> { new Page<List<string>> { Error = responce.Error } };
                        var pageDetailsList = new DeputyPageParser().ParseDetails(responce.ReceivedData, isSavePhoto, satellitePage, deputyPagelink);
                        return new List<Page<List<string>>> { pageDetailsList };
                }
            }
            catch (Exception e)
            {
                _log.Error($"ParseDeputieInfo.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new List<Page<List<string>>> { new Page<List<string>> { Error = e.Data.Values.Count > 0 ?
                        e.Data.Values.OfType<ErrorModel>().First() 
                        : new ErrorModel {ErrorMsg = "Помилка при пошуку інформації про депутатів" } } };
            }
        }

        /// <summary>
        /// метод создает коллекцию экземпляров Page со страницы депутата (для депутатских голосований, выступлений)
        /// </summary>
        /// <param name="instance">экземпляр DeputyPageParser</param>
        /// <param name="deputyPagelink">url страницы депутата</param>
        /// <param name="blankLink">ссылка-заготовка</param>
        /// <param name="searchArea">часть url для поиска нужного url на странице депутата</param>
        /// <param name="blockName">имя блока, необходимое для определения названия файла</param>
        /// <param name="deputyName">имя депутата</param>
        /// <returns>коллекция экземпляров Page в коллекции List</returns>
        private List<Page<List<string>>> CreatePagesCollection(DeputyPageParser instance, string deputyPagelink, string blankLink, string searchArea, string blockName, string deputyName)
        {
            //получаем данные со страницы депутата
            var responce = HtmlProvider.GetResponse<string>(deputyPagelink, useUtf8Encoding: true);
            if (responce.Error != null)
                throw new Exception(responce.Error.ErrorMsg);

            //находим url, содержащий searchArea, на странице депутата
            var document = Converter.ConvertToHtmlDocument(responce.ReceivedData);
            var linksCollect = document.DocumentNode.SelectSingleNode("//div[@class='topTitle']").SelectNodes("a");
            var linkContainingCode = linksCollect?.FirstOrDefault(l => Regex.IsMatch(l.Attributes["href"].Value, searchArea))?.Attributes["href"].Value;
            
            //заменяем код депутата в ссылке-заготовке
            var link = blankLink.Replace("CodeToReplace", linkContainingCode?.Split('=').Last());

            //получаем по ссылке данные
            responce = HtmlProvider.GetResponse<string>(link);

            //получаем коллекцию, количество объектов которой равняется количеству дней, в которые депутат выступал, голосовал
            var daysCollect = instance.ParsePage(responce.ReceivedData);

            //создаем для каждого объекта коллекции экземпляр Page
            var listOfPages = new List<Page<List<string>>>();
            foreach (var day in daysCollect)
            {
                var votingPage = instance.CreatePage(day);
                //добавляем в Page запись с названием блока и именем депутата для определения имени файла
                votingPage.PageDetails.Insert(0, new Record<List<string>> { Name = blockName, Value = new List<string> { deputyName } });
                listOfPages.Add(votingPage);
            }
            return listOfPages;
        }

        /// <summary>
        /// метод создает экземпляр Page из данных со страницы депутата (для депутатских запросов, законотв. активности)
        /// </summary>
        /// <param name="instance">экземпляр DeputyPageParser</param>
        /// <param name="link">url страницы депутата</param>
        /// <param name="satellitePage">коллекция доп. url для парсинга</param>
        /// <param name="blockName">имя блока, необходимое для определения названия файла</param>
        /// <param name="deputyName">имя депутата</param>
        /// <returns>один экземпляр Page в коллекции List</returns>
        private List<Page<List<string>>> CreateSinglePage(DeputyPageParser instance, string link, string[] satellitePage, string blockName, string deputyName)
        {
            //находим ссылку на нужную страницу для парсинга и заменяем в ней код депутата на нужный
            var links = link.Split('/');
            link = satellitePage[2].Replace("CodeToReplace", links.Contains("expage") ? links[links.Length - 2] : links.Last());
            
            //получаем по ссылке данные
            var responce = HtmlProvider.GetResponse<string>(link);

            //получаем экземпляр Page с данными
            var pageDetailsList = instance.CreatePage(responce?.ReceivedData, satellitePage);

            //добавляем в Page запись с названием блока и именем депутата для определения имени файла
            pageDetailsList.PageDetails.Insert(0, new Record<List<string>> { Name = blockName, Value = new List<string> { deputyName } });

            return new List<Page<List<string>>> {pageDetailsList};
        }

        #region В ДАННЫЙ МОМЕНТ формирование расписания работы коммитетов не нужно заказчику
        ///// <summary>
        ///// метод возвращает информацию про план заседаний комитетов
        ///// </summary>
        ///// <param name="row">строка для парсинга из главной страницы</param>
        ///// <param name="satellitePage">дополнительный url для парсинга</param>
        ///// <returns></returns>
        //private List<Page<List<string>>> ParseCommitteeWorkRow(HtmlNode row, string[] satellitePage)
        //{
        //    try
        //    {
        //        var listofPages = new List<Page<List<string>>>();
        //        var responce = HtmlProvider.GetResponse<string>(satellitePage[1] + row.Attributes["href"].Value);
        //        var listOfPages = new CommitteePageParser().ParseCommiteeWorkPlan(responce.ReceivedData);
        //        foreach (var page in listOfPages)
        //        {
        //            var workPage = new CommitteePageParser().CreateCommitteeWorkPage(page);
        //            //votingPage.PageDetails.Insert(0, new Record<List<string>> { Name = blockName, Value = new List<string> { deputyName } });
        //            listofPages.Add(workPage);
        //        }
        //        return listofPages;
        //    }
        //    catch (Exception e)
        //    {
        //        _log.Error($"ParseCommitteeWorkPage.\n{e.Message}\nStackTrace:{e.StackTrace}");
        //        return new List<Page<List<string>>> { new Page<List<string>> { Error = "Помилка при пошуку інформації про засідання комітетів" } };
        //    }
        //}
        #endregion
    }
}
