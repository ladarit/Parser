using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.DataProviders
{
    public class FilesCompare
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private FilesToSave _filesNeedToSave = new FilesToSave { Files = new List<FileModel>() };


        public FilesToSave GetFilesNeedToSaveFromDataBase(ParseResult<FileModel> parseResult, UrlsCollection urls)
        {
            try
            {
                _log.Info("START GetAndParseDataFromSite");

                if (parseResult.Error != null)
                {
                    _log.Error($"GetAndParseDataFromSite.\n{parseResult.Error}");
                    return new FilesToSave {Error = parseResult.Error};
                }

                _log.Info("COMPLETE GetAndParseDataFromSite. START Create tomporary collection of files names & hashes");

                var netHelper = new NetHelper();
                //создаем коллекцию имен и хешей файлов из parseResult
                var filesNamesAndHashes = new CollectionBeforeCompare
                {
                    Hash = new Dictionary<string, string>(),
                    Ip = netHelper.GetHostIp(),
                    Terminal = netHelper.GetHostName()
                };

                foreach (var file in parseResult.XmlDocuments)
                    filesNamesAndHashes.Hash.Add(file.FileName, file.Md5Hash);
                if (parseResult.Files.Any())
                    foreach (var file in parseResult.Files)
                    {
                        if (file.Content.LongLength < 100000000) //не загружаем файлы более 100mb
                            filesNamesAndHashes.Hash.Add(file.FileName, file.Md5Hash);
                    }

                _log.Info($"COMPLETE Create tomporary collection of files names & hashes, xml count: {parseResult.XmlDocuments.Count}, " +
                          $" files count: {parseResult.Files.Count}. START sending temp collection to api");

                //передаем коллекцию в апи и получем список файлов, которые надо сохранить
                var filesNamesFromApi = UploadFilesNamesAndHashes(filesNamesAndHashes, urls.ApiCompareAdress);

                if (filesNamesFromApi.Error != null)
                {
                    _log.Error($"ERROR Get files from api {filesNamesFromApi.Error.ErrorMsg}");
                    return new FilesToSave {Error = parseResult.Error};
                }

                _log.Info($"COMPLETE Receive temp collection from api. Files to save count: {filesNamesFromApi.File.Count}, new cardcounter: {filesNamesFromApi.NewCardCounter}");

                if (!(filesNamesFromApi.File.Any() && filesNamesFromApi.NewCardCounter > 0))
                    return new FilesToSave {Files = new List<FileModel>()};

                _log.Info("START fill collection to send to api");

                //выбираем из имеющейся коллекции файлов файлы, полученные из api
                foreach (var fileName in filesNamesFromApi.File)
                {
                    var fileModel = Regex.IsMatch(fileName, @"^(\d+|Голосування#Документ#|Депутат#Фото#)")
                        ? parseResult.Files.FirstOrDefault(f => fileName.Contains(f.FileName))
                        : parseResult.XmlDocuments.FirstOrDefault(f => fileName.Contains(f.FileName));
                    if (fileModel == null) continue;
                    fileModel.FileName = fileName + fileModel.FileType;
                    fileModel.NewCardCounter = filesNamesFromApi.NewCardCounter;
                    _filesNeedToSave.Files.Add(fileModel);
                }
                return _filesNeedToSave;
            }
            catch (Exception e)
            {
                _log.Error($"ERROR GetFilesNeedToSaveFromDataBase.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new FilesToSave {Error = new ErrorModel {ErrorMsg = e.Message, Operation = "GetFilesNeedToSaveFromDataBase" } };
            }
            finally
            {
                parseResult.Files.ClearCollection(_filesNeedToSave);
                parseResult.XmlDocuments.ClearCollection(_filesNeedToSave);
                GarbageCleaner.ClearGarbage();
            }
        }

        /// <summary>
        /// метод отправляет в апи коллекцию имен и хешей файлов и возвращает нужные для сохранения файлы и каунтер новой карточки
        /// </summary>
        /// <param name="files"></param>
        /// <param name="apiAdress"></param>
        /// <returns></returns>
        private CollectionAfterCompare UploadFilesNamesAndHashes(CollectionBeforeCompare files, string apiAdress)
        {
            return new ApiHandler(new NetworkCredential()).CompareFiles(apiAdress, files);
        }
    }
}
