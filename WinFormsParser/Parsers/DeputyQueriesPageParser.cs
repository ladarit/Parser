using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class DeputyQueriesPageParser : DeputyPageParser
    {
        public override Page<List<string>> CreatePage(string html, string[] satellitePage)
        {
            var doc = Converter.ConvertToHtmlDocument(html);
            var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            var trCollect = doc.DocumentNode.SelectNodes("//table[@class='Centered THEAD10']/tr").ToList();
            trCollect.RemoveRange(0, 2);
            trCollect.Remove(trCollect.Last());
            var trToRemove = trCollect.Where(tr => String.IsNullOrEmpty(tr.SelectNodes("td")[1].InnerText.RemoveOddSpaces())).ToList();
            trToRemove.ForEach(tr => trCollect.Remove(tr));
            List<string> linkCollect = new List<string>();
            foreach (var tr in trCollect)
            {
                var link = satellitePage[2].Split(new[] { "zweb2/" }, StringSplitOptions.None)[0] + "zweb2/" + tr.SelectNodes("td")[1].SelectSingleNode("div/b/a").Attributes["href"].Value;
                linkCollect.Add(link);
            }
            foreach (var link in linkCollect)
            {
                var responce = HtmlProvider.GetResponse<string>(link);
                if (responce.Error != null)
                    throw new Exception(responce.Error.ErrorMsg);
                var document = Converter.ConvertToHtmlDocument(responce.ReceivedData);
                ParseQueryPage(ref page, document);
            }
            return page;
        }

        private void ParseQueryPage(ref Page<List<string>> page, HtmlDocument doc)
        {
            var name = doc.DocumentNode.SelectSingleNode("//table[@class='Centered']").SelectNodes("tr").First().SelectNodes("td")[1].InnerText.RemoveOddSpaces();
            var tableRows = doc.DocumentNode.SelectNodes("//table[@class='THEAD02 Centered']/tr");

            var headers = tableRows.FindFirst("tr").SelectNodes("td").ToList();
            tableRows.Remove(tableRows.First());
            tableRows.Remove(tableRows.Last());
            if (!headers.Any()) return;
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");
                if (!(cells != null && cells.Any())) continue;
                var text = new List<string>();
                for (int i = 0; i < cells.Count; i++)
                {
                    if (!Regex.IsMatch(cells[i].InnerText.RemoveOddSpaces(), @"^\s+") && !string.IsNullOrEmpty(cells[i].InnerText.RemoveOddSpaces()))
                        text.Add(headers[i].InnerText.RemoveColon().RemoveOddSpaces() + ": " + cells[i].InnerText.RemoveOddSpaces());
                }
                var pageDetailsRow = new Record<List<string>>
                {
                    Name = name,
                    Value = new List<string>()
                };
                pageDetailsRow.Value.AddRange(text);
                page.PageDetails.Add(pageDetailsRow);
            }
        }
    }
}
