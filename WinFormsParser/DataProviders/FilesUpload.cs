using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using GovernmentParse.Controls;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.DataProviders
{
    public class FilesUpload
    {
        private readonly SavedFiles _savedFiles;

        private static readonly object ThreadLock = new object();

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public FilesUpload()
        {
            _savedFiles = new SavedFiles { SavedFilesInfo = new List<SavedFileInfo>() };
        }

        public SavedFiles UploadFilesToDataBase(FilesToSave filesToSave, UrlsCollection urls, ProgressReporter progressReporter = null, SmoothProgressBar saveProgressBar = null, bool logProgress = false)
        {
            try
            {
                if (!filesToSave.Files.Any()) return new SavedFiles { SavedFilesInfo = new List<SavedFileInfo>() };

                _log.Info("START send files to api");

                if (saveProgressBar != null)
                    saveProgressBar.Maximum = filesToSave.Files.Count;

#if SKMU_server_ANYCPU
                //синхронная отправка в api
                foreach (var file in filesToSave.Files)
                {
                    if (saveProgressBar != null)
                        progressReporter?.ReportProgress(() =>
                        {
                            if (saveProgressBar.Value < saveProgressBar.Maximum)
                                saveProgressBar.Value += 1;
                        });
                    var isSuccess = WrapperSyncUploadFile(urls.ApiSaveFileAdress, file, logProgress);
                    if (!isSuccess)
                        break;
                }
#else
                //асинхронно отправляем каждый файл в api для сохранения //new ParallelOptions { MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 1.0)) },
                Parallel.ForEach(filesToSave.Files, (file, state) =>
                {
                    if (saveProgressBar != null)
                        progressReporter?.ReportProgress(() =>
                        {
                            if (saveProgressBar.Value < saveProgressBar.Maximum)
                                saveProgressBar.Value += 1;
                        });
                    UploadFile(urls.ApiSaveFileAdress, file, state);
                });
#endif
                _log.Info("COMPLETE send files to api");

                //обновляем колонку Notice в таблице SaveFiles
                var cardId = new CardId { Counter = (long)filesToSave.Files.First().NewCardCounter };
                UpdateNoticeColumn(urls.ApiUpdateTableAdress, cardId);

                return _savedFiles;
            }
            catch (Exception e)
            {
                _log.Error($"UploadFilesToDataBase.\n{e.Message}\nStackTrace:{e.StackTrace}");
                _savedFiles.Error = new ErrorModel { ErrorMsg = e.Message, Operation = "UploadFilesToDataBase" };
                return _savedFiles;
            }
            finally
            {
                filesToSave.Files.ClearCollection();
                GarbageCleaner.ClearGarbage();
            }
        }

        private SavedFileInfo SyncUploadFile(string apiAdress, FileModel file, bool logProgress = false)
        {
            return new ApiHandler(new NetworkCredential()).Upload(apiAdress, file, logProgress);
        }

        private bool WrapperSyncUploadFile(string apiAdress, FileModel file, bool logProgress = false)
        {
            SavedFileInfo responce;
            var counter = 0;
            do
            {
                if (counter == 0)
                    responce = SyncUploadFile(apiAdress, file, logProgress);
                else
                {
                    _log.Info($"Can`t send file {file.FileName}, delay 30 sec and retry {counter}(st/nd) time");
                    var task = Task.Delay(30000).ContinueWith(_ => SyncUploadFile(apiAdress, file, logProgress));
                    responce = task.Result;
                    if (responce.Error == null)
                        _log.Info($"SUCCESS send file {file.FileName} on {counter}(st/nd) retry");
                }
                counter++;
            }
            while (counter <= 10 && responce.Error != null);

            lock (ThreadLock)
            {
                _savedFiles.SavedFilesInfo.Add(responce);
            }
            if (responce.Error != null)
            {
                _log.Error($"SYNC UploadFile {file.FileName} error.\n{responce.Error.ErrorMsg}");
                return false;
            }
            return true;
        }

        private void UploadFile(string apiAdress, FileModel file, ParallelLoopState state)
        {
            var responce = new ApiHandler(new NetworkCredential()).Upload(apiAdress, file);
            lock (ThreadLock)
            {
                _savedFiles.SavedFilesInfo.Add(responce);
            }
            if (responce.Error != null)
            {
                _log.Error($"ASYNC UploadFile error.\n{responce.Error}");
                state.Break();
            }
        }

        private void UpdateNoticeColumn(string url, CardId carId)
        {
            _log.Info("START update Notice column");
            var updateColumnResult = new ApiHandler(new NetworkCredential()).UpdateNoticeColumn(url, carId);
            if (updateColumnResult.Error != null)
                _log.Error($"ASYNC updateNoticeColumn error.\n{updateColumnResult.Error}");
            else
                _log.Info("END update Notice column");
        }
    }
}
