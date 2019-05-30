using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public class IniReader
    {
        readonly IniFiles _ini = new IniFiles("config.ini");

        private static Dictionary<string, string> _urls;

        public IniReader()
        {
#if SKMU_server_ANYCPU
            var _partOfUrl = "askod.kmu.gov.ua/VRUApi";
#else
            var _partOfUrl = "app-client/vruApi";
#endif

            _urls = new Dictionary<string, string>
            {
                { "ApiCompareAdress", $"http://{_partOfUrl}/api/GovernSiteFiles/CompareFiles/"},
                { "ApiSaveFileAdress", $"http://{_partOfUrl}/api/GovernSiteFiles/SaveFile/"},
                { "ApiUpdateTableAdress", $"http://{_partOfUrl}/api/GovernSiteFiles/UpdateNoticeColumn/"},
                { "ApiSaveErrorMsg", $"http://{_partOfUrl}/api/GovernSiteFiles/SaveErrorMessage/"},

                {"XmlFilesApiAdress", "http://data.rada.gov.ua/open/data/bills-skl8.xml" },

                { "LawsTablePage", "http://w1.c1.rada.gov.ua/pls/zweb2/webproc555"},
                { "LawPage", "http://w1.c1.rada.gov.ua/pls/zweb2/"},
                { "LawsByDatePage", "http://w1.c1.rada.gov.ua/pls/zweb2/webproc2_5_1_J?date1=&date2=&zp_cnt=10000"},
                { "LawByNamePage", "http://w1.c1.rada.gov.ua/pls/zweb2/webproc2_5_1_J?ses=10009&num_s=3&num="},
                { "LawsByLastConvocation", "http://w1.c1.rada.gov.ua/pls/zweb2/webproc2_5_1_J?ses=10009&num_s=2&num=&date1=&date2=&name_zp=&out_type=&id=&page=1&zp_cnt=20000"},

                { "CommitteesPage", "http://w1.c1.rada.gov.ua/pls/site2/p_komitis?skl="},
                { "CommitteeDetailsPage", "http://w1.c1.rada.gov.ua/pls/site2/"},
                { "CommitteesWorkPage", "http://static.rada.gov.ua/zakon/new/RK/index.htm"},
                { "CommitteesCalendarWeekPage", "http://static.rada.gov.ua/zakon/new/RK/"},

                { "SessionPage", "http://static.rada.gov.ua/zakon/new/WR/index.htm"},
                { "SessionCalendarWeekPage", "http://static.rada.gov.ua/zakon/new/WR/"},
                { "SessionDatesPage", "http://iportal.rada.gov.ua/meeting/awt"},

                { "DeputiesPage", "http://w1.c1.rada.gov.ua/pls/site2/fetch_mps?skl_id="},
                { "DeputyPage", "http://itd.rada.gov.ua/mps/info/page/"},
                { "DeputyVotePage", "http://w1.c1.rada.gov.ua/pls/radan_gs09/ns_dep_gol_list?startDate=StartDateToReplace&endDate=EndDateToReplace&kod=CodeToReplace&nom_str=0"},
                { "DeputySpeechPage", "http://w1.c1.rada.gov.ua/pls/radan_gs09/ns_dep_vistup_list?startDate=StartDateToReplace&endDate=EndDateToReplace&kod=CodeToReplace&nom_str=0"},
                { "DeputyQueriesPage", "http://w1.c1.rada.gov.ua/pls/zweb2/wcadr42d?sklikannja=ConvocationToReplace&kod8011=CodeToReplace"},
                { "DeputyLawActivityPage", "http://w1.c1.rada.gov.ua/pls/pt2/reports.dep2?PERSON=CodeToReplace&SKL=ConvocationToReplace"},

                { "Blank", "startDate=StartDateToReplace&endDate=EndDateToReplace&kod=CodeToReplace&nom_str=0" },

                { "FractionsMainPage", "http://w1.c1.rada.gov.ua/pls/site2/p_fractions?skl="},
                { "FractionWithoutNamePage", "http://w1.c1.rada.gov.ua/pls/site2/"}
            };
        }

        public UrlsCollection AutoReadIni()
        {
            var urlCollection = new UrlsCollection();
            var properties = urlCollection.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var adress = _urls.First(k => k.Key.Contains(prop.Name, StringComparison.OrdinalIgnoreCase));

                if (_ini.KeyExists("SettingForm1", adress.Key))
                    _urls[adress.Key] = _ini.ReadIni("SettingForm1", adress.Key);
                else
                    _ini.Write("SettingForm1", adress.Key, adress.Value);

                prop.SetValue(urlCollection, _urls[adress.Key]);
            }
            return urlCollection;
        }
    }
}
