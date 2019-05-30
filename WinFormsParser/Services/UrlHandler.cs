using System.Collections.Generic;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public class UrlHandler
    {
        public List<string> SetUrlForParse(UrlsCollection urls, string controlName = null)
        {
            var url = new List<string>();
            switch (controlName)
            {
                case "SaveLawsBtn":
                case null:
                    url.Add(urls.LawsTablePage);
                    url.Add(urls.LawPage);
                    break;
                case "SaveLawsByDatePickerBtn":
                    url.Add(urls.LawsByDatePage);
                    url.Add(urls.LawPage);
                    break;
                case "SaveAllLawsFromCurrentConvocationBtn":
                    url.Add(urls.LawsByLastConvocation);
                    url.Add(urls.LawPage);
                    break;
                case "SaveByLowNameBtn":
                    url.Add(urls.LawByNamePage);
                    url.Add(urls.LawPage);
                    break;
                case "SaveСommitteesBtn":
                    url.Add(urls.CommitteesPage);
                    url.Add(urls.CommitteeDetailsPage);
                    break;
                case "SaveСommitteesWorkBtn":
                    url.Add(urls.CommitteesWorkPage);
                    url.Add(urls.CommitteesCalendarWeekPage);
                    break;
                case "SavePlenarySessionCalendarPlanBtn":
                    url.Add(urls.SessionPage);
                    url.Add(urls.SessionCalendarWeekPage);
                    break;
                case "SavePlenarySessionDatesBtn":
                    url.Add(urls.SessionDatesPage);
                    url.Add(urls.SessionDatesPage);
                    break;
                case "SaveDeputyBtn":
                    url.Add(urls.DeputiesPage);
                    url.Add(urls.DeputyPage);
                    break;
                case "SaveDepVotingBtn":
                    url.Add(urls.DeputiesPage);
                    url.Add(urls.DeputyPage);
                    url.Add(urls.DeputyVotePage);
                    break;
                case "SaveDepQueriesBtn":
                    url.Add(urls.DeputiesPage);
                    url.Add(urls.DeputyPage);
                    url.Add(urls.DeputyQueriesPage);
                    break;
                case "SaveDepSpeechesBtn":
                    url.Add(urls.DeputiesPage);
                    url.Add(urls.DeputyPage);
                    url.Add(urls.DeputySpeechPage);
                    break;
                case "SaveDepLawActivityBtn":
                    url.Add(urls.DeputiesPage);
                    url.Add(urls.DeputyPage);
                    url.Add(urls.DeputyLawActivityPage);
                    break;
                case "SaveFractionsBtn":
                    url.Add(urls.FractionsMainPage);
                    url.Add(urls.FractionWithoutNamePage);
                    break;
                case "UpdateLaws":
                    url.Add(urls.XmlFilesApiAdress);
                    break;
            }
            return url;
        }
    }
}
