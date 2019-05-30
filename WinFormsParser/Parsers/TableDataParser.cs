using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class TableDataParser
    {
        public readonly ResourceReader ResourceReader;

        public TableDataParser()
        {
            ResourceReader = new ResourceReader();
        }

        /// <summary>
        /// метод возвращает таблицу со строками для парсинга
        /// </summary>
        /// <param name="docNode">root-элемент документа</param>
        /// <param name="controlName">имя контрола</param>
        public TableInfo GetTable(HtmlNode docNode, string controlName = null)
        {
            TableInfo tableInfo = new TableInfo();
            if (!string.IsNullOrEmpty(controlName))
            {
                if (controlName.Equals("SavePlenarySessionDatesBtn"))
                    tableInfo.Table = docNode.SelectSingleNode("//div[@class='white3-panel1']");
                else
                    tableInfo.Table = Regex.IsMatch(controlName, "^SaveDep")
                        ? docNode.SelectNodes("//ul").FirstOrDefault(n => n.Attributes["class"].Value.Contains("search-filter-results"))
                        : docNode.SelectSingleNode("//table[@class='striped Centered']|//table[@class='Otable']");
            }
            else //todo переписать для парсинга разных сущностей а не только законопроектов // имя контрола пустое в случае запуска без интерфейса (silent-режим)
                tableInfo.Table = docNode.SelectSingleNode("//table[@class='striped Centered']|//table[@class='Otable']");

            return tableInfo;
        }

        /// <summary>
        /// метод возвращает имя таблицы
        /// </summary>
        /// <param name="docNode">root-элемент документа</param>
        /// <param name="controlName">имя контрола</param>
        public string GetTableName(HtmlNode docNode, string controlName)
        {
            var tableName = controlName.Equals("SavePlenarySessionDatesBtn")
                ? docNode.SelectSingleNode("//div[@class='panel-head clr']//h3").InnerText
                : docNode.SelectSingleNode("//div[@class='information_block_ins']//b")?.InnerText;

            if (string.IsNullOrEmpty(tableName))
                tableName = docNode.SelectSingleNode("//h3[@align='center']")?.InnerText.RemoveOddSpaces();

            return tableName;
        }

        /// <summary>
        /// метод возвращает строки таблицы для парсинга
        /// </summary>
        /// <param name="table">таблица</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="url"></param>
        public List<HtmlNode> GetRows(HtmlNode table, string controlName, string url = null)
        {
            if (string.IsNullOrEmpty(controlName))
                return table.SelectNodes("tr|li").ToList();

            if (controlName.Equals("SavePlenarySessionDatesBtn"))
                return new List<HtmlNode> { table };

            var rowCollect = controlName.ContainsAny("SavePlenarySessionCalendarPlanBtn", "SaveСommitteesWorkBtn")
                ? table.SelectNodes("//a").Where(a => new Regex("^(WR|RK)").IsMatch(a.Attributes["href"].Value)).ToList()
                : table.SelectNodes("tr|li").ToList();

            //нет необходимости в удалении лишних поисковых результатов, поскольку поиск производится по точному совпадению
            //if (controlName.Equals("SaveByLowNameBtn"))
            //    rowCollect = RemoveUnsuitableRows(rowCollect, url);

            return rowCollect;
        }

        /// <summary>
        /// метод возвращает строки таблицы для парсинга по коллекции xPathList
        /// </summary>
        /// <param name="table">таблица</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="xPathList">коллекция xPathList</param>
        public List<HtmlNode> GetRowsByXpath(HtmlNode table, string controlName, List<string> xPathList)
        {
            var rowCollect = GetRows(table, controlName);
            return rowCollect.Select(r=>r).Where(r=> xPathList.Any(x => x.Equals(r.XPath))).ToList();
        }

        /// <summary>
        /// метод удаляет из коллекции строк строки, названия которых не совпадают с искомыми
        /// </summary>
        /// <param name="rowCollect">коллекция строк</param>
        /// <param name="url"></param>
        private List<HtmlNode> RemoveUnsuitableRows(List<HtmlNode> rowCollect, string url)
        {
            //создаем коллекцию номеров законов
            url = url.Split(new[] { "num=" }, StringSplitOptions.None)[1];
            var laws = url.Split(new[] { "%2c" }, StringSplitOptions.RemoveEmptyEntries);
            //декодируем номера законов
            for (int i = 0; i < laws.Length; i++)
                laws[i] = HttpUtility.UrlDecode(laws[i], Encoding.GetEncoding("windows-1251"));

            //формируем коллекцию искомых законопроектов, выбирая нужные среди найденных
            List<HtmlNode> wantedLawsCollect = new List<HtmlNode> { rowCollect.First() };
            for (int i = 1; i < rowCollect.Count; i++)
            {
                var foundedLaw = rowCollect[i].SelectNodes("td").First();
                var wantedLaw = laws.FirstOrDefault(l => l.Equals(foundedLaw.InnerText.RemoveOddSpaces()));
                if (wantedLaw != null)
                    wantedLawsCollect.Add(rowCollect[i]);
            }
            return wantedLawsCollect;
        }

        /// <summary>
        /// метод возвращает строки таблицы для парсинга
        /// </summary>
        /// <param name="table">таблица</param>
        /// <param name="limitWarning">bool-признак предупреждения о лимите на количество строк</param>
        public ErrorModel CheckDataCorrectness(HtmlNode table, bool limitWarning)
        {
            var rowCount = table?.SelectNodes("tr|li")?.Count ?? 0;
            return table == null || rowCount > 100 && limitWarning
                ? new ErrorModel
                {
                    ErrorMsg = table == null
                        ? ResourceReader.GetString("ZeroCountWarning")
                        : ResourceReader.GetString("CountWarning") + rowCount,
                    Operation = "CheckDataCorrectness"
                }
                : null;
        }
    }
}
