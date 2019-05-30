using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public class XmlComparer
    {
        private XmlNamespaceManager NsManager { get; set; }

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// метод сравнивает xml файлы
        /// </summary>
        /// <param name="files">xml файлы</param>
        /// <returns>возвращает коллекцию номеров законопроектов</returns>
        public CompareXmlFilesResult CompareFiles(List<XmlDocument> files)
        {
            try
            {
                var latestFile = files.First();
                var earlierFile = files.Last();

                NsManager = new XmlNamespaceManager(latestFile.NameTable);
                NsManager.AddNamespace("x", "http://static.rada.gov.ua/site/bills/opendata/bills.xsd");

                var latestLawsList = GetLawsList(latestFile);
                if (latestLawsList == null || !latestLawsList.Any())
                    return new CompareXmlFilesResult { Error = new ErrorModel { ErrorMsg = "Не знайдено законопроектів у новому xml" }, Operation = "CompareFiles", ControlName = "UpdateLaws" };

                var earlierLawsList = GetLawsList(earlierFile);
                if (earlierLawsList == null || !earlierLawsList.Any())
                    return new CompareXmlFilesResult { Error = new ErrorModel { ErrorMsg = "Не знайдено законопроектів у попередньому xml" }, Operation = "CompareFiles", ControlName = "UpdateLaws" };

                var updatedLawsNumbers = latestLawsList.Select(p => p.InnerText).Except(earlierLawsList.Select(p => p.InnerText)).Select(n => n).ToList();

                return GetlawsShortInfo(updatedLawsNumbers, latestLawsList);
            }
            catch (Exception ex)
            {
                _log.Error($"CompareFiles.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
                return new CompareXmlFilesResult {Error = new ErrorModel {ErrorMsg = ex.Message}, Operation = "CompareFiles", ControlName = "UpdateLaws" };
            }
            finally
            {
                files.First().RemoveAll();
                files.Last().RemoveAll();
                files.Clear();
                files = null;
            }
        }

        /// <summary>
        /// метод возвращает коллекцию законов
        /// </summary>
        /// <param name="doc">xml документ</param>
        private List<XmlNode> GetLawsList(XmlDocument doc)
        {
            try
            {
                return doc.SelectNodes("//x:bill", NsManager)?.Cast<XmlNode>().ToList();
            }
            catch (Exception ex)
            {
                _log.Error($"GetLawsList.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
                return null;
            }
        }

        private CompareXmlFilesResult GetlawsShortInfo(List<string> laws, List<XmlNode> newLawsList)
        {
            List<TableRow> updatedlawsInfo = new List<TableRow>(); 
            CultureInfo ci = new CultureInfo("uk-UA");
            laws.ForEach(l =>
            {
                var law = newLawsList.FirstOrDefault(r => r.InnerText.Equals(l));
                updatedlawsInfo.Add(new TableRow
                {
                    Cells = new List<string>
                    {
                        law?.SelectSingleNode("x:uri", NsManager)?.InnerText.RemoveOddSpaces(),
                        law?.SelectSingleNode("x:number", NsManager)?.InnerText.RemoveOddSpaces(),
                        DateTime.Parse(law?.SelectSingleNode("x:registrationDate", NsManager)?.InnerText.RemoveOddSpaces()).ToString(ci.DateTimeFormat.ShortDatePattern),
                        law?.SelectSingleNode("x:type", NsManager)?.InnerText + " " + law?.SelectSingleNode("x:title", NsManager)?.InnerText
                    }
                });
            });
            return new CompareXmlFilesResult {List = updatedlawsInfo};
        }
    }
}
