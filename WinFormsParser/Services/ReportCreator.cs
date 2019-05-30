using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public static class ReportCreator
    {
        private static readonly log4net.ILog Log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ResourceReader ResourceReader;

        static ReportCreator()
        {
            ResourceReader = new ResourceReader();
        }

        public static void CreateReportForSilentMode(SavedFiles responce)
        {
            var report = new StringBuilder();
            foreach (var answer in responce.SavedFilesInfo)
            {
                if (!String.IsNullOrEmpty(answer.FileName) && answer.FileId != 0 && answer.Error == null)
                    report.Append($"Название файла: {answer.FileName}, номер файла: {answer.FileId} \n");
                else
                    report.Append(answer.Error);
            }
            var directory = Directory.CreateDirectory(Application.StartupPath + "\\Reports");
            var reportName = "\\Report " + $"{DateTime.Now:dd.MM.yyyy._HH.mm}.txt";
            File.WriteAllText(directory.FullName + reportName, report.ToString());
        }

        public static string CreateReport(SavedFiles responce)
        {
            try
            {
                var report = new StringBuilder();
                if (!responce.SavedFilesInfo.Any()) return string.Empty;
                foreach (var answer in responce.SavedFilesInfo)
                {
                    if (!string.IsNullOrEmpty(answer.FileName) && answer.FileId != 0)
                    {
                        var splitOption = answer.FileName.Contains("рев.") ? "рев." : DictionaryInitializer.FileNamesComparator.FirstOrDefault(f => answer.FileName.Contains(f.Key)).Value;
                        var name = answer.FileName.Remove(answer.FileName.IndexOf(splitOption, StringComparison.Ordinal)).Split('#');

                        if (name[0].Contains("Депутат")) name[0] += "#" + name[1];
                        if (name[1].ContainsAny("Связь", "Інфо", "Фото")) name[1] = name[2];
                        if (name[0].Contains("Голосування"))
                        {
                            if (answer.FileName.Contains("Документ", StringComparison.OrdinalIgnoreCase))
                            {
                                name[0] += "#Документ";
                                name[1] = name[3];
                            }
                            else
                            {
                                name[0] += "#" + name[1];
                                name[1] = name[2];
                            }

                            name[1] = name[1].Replace("_", ":");
                        }

                        var strName = splitOption.Equals("рев.")
                            ? DictionaryInitializer.FileNamesComparatorRevis.FirstOrDefault(f => Regex.IsMatch(name[0], f.Key)).Value
                            : DictionaryInitializer.FileNamesComparator.FirstOrDefault(f => Regex.IsMatch(name[0], f.Key)).Value;

                        if (!string.IsNullOrEmpty(strName))
                            report.Append($"{ResourceReader.GetString(strName)} {name[1]} \n");
                    }
                }
                return report.ToString();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return string.Empty;
            }
        }
    }
}
