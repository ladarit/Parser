using System.Collections.Generic;
using System.Linq;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.Parsers
{
    public class DeputySpeechesPageParser : DeputyPageParser
    {
        public override List<List<HtmlNode>> ParsePage(string html)
        {
            var doc = Converter.ConvertToHtmlDocument(html);
            var liCollect = doc.DocumentNode.SelectSingleNode("//ul[@class='pd']").SelectNodes("li").ToList();
            //объединяем li по два: верхний и нижний
            for (int i = 0; i < liCollect.Count; i++)
            {
                if (i % 2 != 0 || i == liCollect.Count - 1) continue;
                var divChilds = liCollect[i].SelectSingleNode("div[@class='strpit']").SelectNodes("div");
                foreach (var child in divChilds)
                    liCollect[i + 1].SelectSingleNode("div[@class='block_pd']").AppendChild(child.Clone());
            }
            var liToRemove = liCollect.Where(li => liCollect.FindIndex(elem => elem.Equals(li)) % 2 == 0).ToList();
            liToRemove.ForEach(li => liCollect.Remove(li));
            //разделяем коллекцию по датам голосований
            var firstLi = liCollect.First().Clone();
            List<List<HtmlNode>> daysCollect = new List<List<HtmlNode>> { new List<HtmlNode> { firstLi } };
            liCollect.RemoveAt(0);
            var firstdayequalrows = liCollect.TakeWhile(tr => tr.SelectNodes("div/div")[1].InnerText.RemoveOddSpaces().Split(' ')[0]
                .Equals(firstLi.SelectNodes("div/div")[1].InnerText.RemoveOddSpaces().Split(' ')[0])).ToList();
            if (firstdayequalrows.Any())
            {
                daysCollect[0].AddRange(firstdayequalrows);
                liCollect.RemoveRange(0, firstdayequalrows.Count);
            }
            var liCount = liCollect.Count;
            for (int i = 0; i < liCount; i++)
            {
                if (!liCollect.Any()) break;
                var dayRows = liCollect.TakeWhile(tr => tr.SelectNodes("div/div")[1].InnerText.RemoveOddSpaces().Split(' ')[0]
                    .Equals(liCollect[0].SelectNodes("div/div")[1].InnerText.RemoveOddSpaces().Split(' ')[0])).ToList();
                if (!dayRows.Any())
                {
                    daysCollect.Add(new List<HtmlNode> { liCollect[i] });
                    liCollect.RemoveAt(i);
                }
                else
                {
                    liCollect.RemoveRange(0, dayRows.Count);
                    daysCollect.Add(dayRows);
                }

            }
            return daysCollect;
        }

        public override Page<List<string>> CreatePage(List<HtmlNode> day)
        {
            var page = new Page<List<string>> { PageDetails = new List<Record<List<string>>>() };
            foreach (var record in day)
            {
                var divCollect = record.SelectNodes("div/div").ToList();
                divCollect.RemoveAt(0);
                var row = new Record<List<string>> { Value = new List<string>(), Name = "Виступи" };
                var speechDate = "Дата виступу: " + divCollect[0].InnerText.RemoveOddSpaces().Split(' ')[0];
                var speechTime = "Час виступу: " + divCollect[0].InnerText.RemoveOddSpaces().Split(' ')[1];
                var speechPlace = "Місце виступу: " + divCollect[1].InnerText.RemoveOddSpaces();
                var speechLawNumber = "№ реєстр.проекту: " + divCollect[2].InnerText.RemoveOddSpaces();
                var speechThemeInitiator = "Ініціатор теми виступу: " + divCollect[3].InnerText.RemoveOddSpaces();
                var speechTheme = "Тема виступу: " + divCollect[4].InnerText.RemoveOddSpaces();
                row.Value.AddMany(speechDate, speechTime, speechPlace, speechLawNumber, speechThemeInitiator, speechTheme);
                page.PageDetails.Add(row);
            }
            return page;
        }
    }
}
