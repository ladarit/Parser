using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse.DataProviders
{
    public class XmlDownloader
    {
        private bool _isLatestXmlLoaded;

        private bool _isOlderXmlValid;

        private string SavePath { get; }

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public XmlDownloader()
        {
            SavePath = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// метод загружает Xml файлы с перечнем законопроектом
        /// </summary>
        /// <param name="urls">url для скачивания xml, содержащиего ссылки на нужные xml файлы</param>
        /// <param name="controlName">имя контрола</param>
        /// <returns>возращает адреса файлов на жестком диске</returns>
        public DownloadResult<XmlDocument> GetXmlDocuments(UrlsCollection urls, string controlName)
        {
            var urlsCollect = new UrlHandler().SetUrlForParse(urls, controlName);

            var orgXmlDownloadResult = DownloadXml(urlsCollect[0], "tmpXml.xml", false);
            if (orgXmlDownloadResult.Error != null)
                return new DownloadResult<XmlDocument> { Error = new ErrorModel {ErrorMsg = orgXmlDownloadResult.Error.ErrorMsg, Operation = orgXmlDownloadResult.Error.Operation, ControlName = controlName } };

            var links = GetXmlLinks(orgXmlDownloadResult.ReceivedData);
            if (links == null || !links.Any())
                return new DownloadResult<XmlDocument> { Error = new ErrorModel { ErrorMsg = "Не вдається визначити посилання на xml файли", ControlName = controlName} };

            var downloadResult = new DownloadResult<XmlDocument> { RecievedData = new List<XmlDocument>() };

            //загружаем xml
            while (links.Count > 0)
            {
                if (_isOlderXmlValid || downloadResult.RecievedData.Count == 2) break;
                var file = DownloadXml(links.First(), links.First().Split('/').Last(), true, @"(<\/bill><\/bills>$)");
                if (file.Error != null)
                {
                    ClearDownloadFiles(downloadResult);
                    return new DownloadResult<XmlDocument> { Error = file.Error };
                }
                if (file.ReceivedData != null)
                    downloadResult.RecievedData.Add(file.ReceivedData);
                if (downloadResult.RecievedData.Count == 1)
                    _isLatestXmlLoaded = true;
                links.Remove(links.First());
            }

            return downloadResult.RecievedData.Count == 2 ?
                downloadResult :
                new DownloadResult<XmlDocument>
                {
                    Error = new ErrorModel
                    {
                        ErrorMsg = $"Кількість файлів для порівняння дорівнює {downloadResult.RecievedData.Count}",
                        Operation = "GetXmlDocuments",
                        ControlName = controlName
                    }
                };
        }

        /// <summary>
        /// метод возращает ссылки на xml для сравнения законопроектов из скачанного xml
        /// </summary>
        /// <param name="xmlDoc">xml документ</param>
        private List<string> GetXmlLinks(XmlDocument xmlDoc)
        {
            try
            {
                var nodesWithLinks = xmlDoc.SelectNodes("/ogd/data/item[@type='file']");

                if (nodesWithLinks == null) throw new Exception();

                var nodesWithXmlLinks = nodesWithLinks.Cast<XmlNode>().Where(n => !Regex.IsMatch(n.FirstChild.InnerText, @"(.zip|.json)"))
                    .OrderByDescending(n => n.ChildNodes[1].InnerText).ToList();

                var xmlLinks = new List<string>();
                nodesWithXmlLinks.ForEach(n => xmlLinks.Add(n.FirstChild.InnerText));
                return xmlLinks;
            }
            catch (Exception ex)
            {
                _log.Error($"GetXmlLinks.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// метод загружает Xml
        /// </summary>
        /// <param name="url">url для загрузки файла</param>
        /// <param name="fileName">имя файла</param>
        /// <param name="isValidate">надо ли валидировать xml</param>
        /// <param name="regSearchTerm">условие поиска в окончании xml файла для проверки закр. тегов на валидность</param>
        /// <returns></returns>
        private ResponceFromUrl<XmlDocument> DownloadXml(string url, string fileName, bool isValidate, string regSearchTerm = null)
        {
            try
            {
                var result = DonwloadFileToHardDrive(url, fileName);
                if (result.Error != null)
                    return new ResponceFromUrl<XmlDocument> { Error = result.Error };

                if (isValidate)
                {
                    var xmlValidationResult = ValidateXml(result.ReceivedData, regSearchTerm);
                    if (xmlValidationResult.Error != null)
                        return new ResponceFromUrl<XmlDocument> { Error = xmlValidationResult.Error };

                    if (!xmlValidationResult.IsXmlValid)
                    {
                        var repairRes = RepairXmlByDateSign(result.ReceivedData);
                        if (repairRes.Error != null)
                            return new ResponceFromUrl<XmlDocument> { Error = repairRes.Error };
                        if (!repairRes.IsSuccess)
                            return new ResponceFromUrl<XmlDocument> { ReceivedData = null };
                    }
                }

                var loadXmlResult = LoadXmlFileToDoc(result.ReceivedData);
                if (loadXmlResult.Error != null)
                    return new ResponceFromUrl<XmlDocument> { Error = loadXmlResult.Error };

                if (_isLatestXmlLoaded) _isOlderXmlValid = true;

                return new ResponceFromUrl<XmlDocument> { ReceivedData = loadXmlResult.ReceivedData };

            }
            catch (Exception e)
            {
                _log.Error($"DownloadXml.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new ResponceFromUrl<XmlDocument> { Error = new ErrorModel { ErrorMsg = e.Message, Operation = "DownloadXml" } };
            }
            finally
            {
                var filePath = SavePath + fileName;
                if (File.Exists(filePath)) File.Delete(filePath);
            }
        }

        /// <summary>
        /// метод загружает в папку расположения программы xml файл, читает его содержимое и возвращает в виде XmlDocument, удаляет файл
        /// </summary>
        /// <param name="url">url для скачивания</param>
        /// <param name="fileName">имя файла</param>
        private ResponceFromUrl<string> DonwloadFileToHardDrive(string url, string fileName)
        {
            try
            {
                return HtmlProvider.DownloadFileToLocalDisk(url, SavePath + fileName);
            }
            catch (Exception e)
            {
                _log.Error($"DonwloadFileToHardDrive.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new ResponceFromUrl<string> { Error = new ErrorModel { ErrorMsg = e.Message } };
            }
        }

        /// <summary>
        /// метод исправляет xml файл, если он является новейшим, в противном случае возвращает false как рез-т исправления
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        /// <returns></returns>
        private XmlRepairResult RepairXmlByDateSign(string filePath)
        {
            return _isLatestXmlLoaded ? new XmlRepairResult { IsSuccess = false } : XmlRepairer.RepairXmlFile(filePath);
        }

        /// <summary>
        ///проверяем файл на корректное закрытие заврешающих тегов
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        /// <param name="regSearchTerm">условие поиска в окончании xml файла для проверки закр. тегов на валидность</param>
        private XmlValidationModel ValidateXml(string filePath, string regSearchTerm)
        {
            return XmlValidator.IsXmlEndedCorrect(filePath, regSearchTerm);
        }

        /// <summary>
        /// загружаем файл в XmlDocument
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        /// <returns></returns>
        private ResponceFromUrl<XmlDocument> LoadXmlFileToDoc(string filePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                return new ResponceFromUrl<XmlDocument> { ReceivedData = xmlDoc };
            }
            catch (Exception e)
            {
                _log.Error($"LoadXmlFileToDoc.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new ResponceFromUrl<XmlDocument> { Error = new ErrorModel { ErrorMsg = e.Message, Operation = "LoadXmlFileToDoc" } };
            }
        }

        /// <summary>
        /// очистка памяти в случае ошибки
        /// </summary>
        /// <param name="downloadResult"></param>
        private void ClearDownloadFiles(DownloadResult<XmlDocument> downloadResult)
        {
            if (downloadResult.RecievedData != null)
            {
                foreach (var xml in downloadResult.RecievedData)
                    xml?.RemoveAll();
                downloadResult.RecievedData.Clear();
                downloadResult.RecievedData = null;
                GarbageCleaner.ClearGarbage();
            }
        }
    }
}
