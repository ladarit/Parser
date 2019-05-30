using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class DeputyVotePageParser : DeputyPageParser
    {
        public override List<List<HtmlNode>> ParsePage(string html)
        {
            var doc = Converter.ConvertToHtmlDocument(html);
            var liCollect = doc.DocumentNode.SelectSingleNode("//ul[@class='pd']").SelectNodes("li").ToList();
            var firstLi = liCollect.First().Clone();
            //разделяем коллекцию по датам голосований
            List<List<HtmlNode>> daysCollect = new List<List<HtmlNode>> { new List<HtmlNode> { firstLi } };
            liCollect.RemoveAt(0);
            var days = liCollect.Where(li => Regex.IsMatch(li.InnerText.RemoveOddSpaces(), @"\d+\.\d+\.\d+") && li.ChildNodes.Count == 1).ToList();
            for (int i = 0; i < days.Count; i++)
            {
                var dayRows = liCollect.TakeWhile(tr => !tr.Equals(days[i])).ToList();
                liCollect.RemoveRange(0, dayRows.Count);
                if (i == 0)
                    daysCollect[0].AddRange(dayRows);
                else
                    daysCollect.Add(dayRows);
            }
            daysCollect.Add(liCollect);
            //к каждому голосованию добавляем день
            for (int i = 0; i < daysCollect.Count; i++)
            for (int j = 1; j < daysCollect[i].Count; j++)
                daysCollect[i][j].AppendChild(daysCollect[i][0].FirstChild);

            return daysCollect;
        }

        public override Page<List<string>> CreatePage(List<HtmlNode> day)
        {
            var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            foreach (var voting in day.Where(l => l.ChildNodes.Count > 1))
            {
                var row = new Record<List<string>> { Value = new List<string>(), Name = "Голосування" };
                var votingDetails = voting.FirstChild.InnerText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var vortingName = "Назва голосування: " + votingDetails[1].RemoveOddSpaces();
                var solution = "Рішення: " + votingDetails[2].RemoveOddSpaces();
                var votingDate = "Дата голосування: " + voting.LastChild.InnerText.RemoveOddSpaces();
                row.Value.AddMany(votingDate, vortingName, solution);
                page.PageDetails.Add(row);
            }
            return page;
        }
    }
}
