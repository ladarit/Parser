using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class DeputyLawActivityPageParser : DeputyPageParser
    {
        public override Page<List<string>> CreatePage(string html, string[] satellitePage)
        {
            var doc = Converter.ConvertToHtmlDocument(html);
            var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            var tables = doc.DocumentNode.SelectNodes("//table[@class='striped Centered']");
            foreach (var table in tables)
                ParseLawCreatingPageTables(ref page, table);
            return page;
        }

        private void ParseLawCreatingPageTables(ref Page<List<string>> page, HtmlNode table)
        {
            var tableName = table.PreviousSibling.PreviousSibling.InnerText.RemoveOddSpaces().Split('(')[0].Trim();
            var headers = table.SelectNodes("tr").First().SelectNodes("th").ToList();
            if (!headers.Any()) return;
            var tableRows = table.SelectNodes("tr");
            tableRows.Remove(tableRows.First());
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
                    Name = tableName,
                    Value = new List<string>()
                };
                pageDetailsRow.Value.AddRange(text);
                page.PageDetails.Add(pageDetailsRow);
            }
        }
    }
}
