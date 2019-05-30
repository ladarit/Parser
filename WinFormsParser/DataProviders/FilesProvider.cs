using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using GovernmentParse.Controls;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Parsers;
using GovernmentParse.Services;
using HtmlAgilityPack;

namespace GovernmentParse.DataProviders
{
    public class FilesProvider
    {
        private bool CheckBoxValue { get; set; }

        private readonly object _localLockObject = new object();

        private readonly int _convocationNumber = ConvocationDeterminant.DetermineConvocation();

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UrlsCollection _urls = new IniReader().AutoReadIni();


        /// <summary>
        /// метод возвращает коллекцию xml и rtf, doc, img в виде экземпляров FileModel (тело файлов в byte[]) с сайта Верховной Рады
        /// </summary>
        /// <param name="urls">url коллекция</param>
        /// <param name="limitWarning">предупреждение о лимите для поиска законов по временному периоду</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="cancelTokenSource">источник токена отмены</param>
        /// <param name="progressReporter">экзмепляр ProgressReporter для доступа к UI</param>
        /// <param name="progressBar">ссылка на progressBar из UI</param>
        /// <param name="checkBoxValue">значение какого-либо чек-бокса</param>
        /// <param name="table">таблица с данными (если null, то выполняется её поиск на странице)</param>
        public ParseResult<FileModel> GetFilesFromSite(UrlsCollection urls, bool limitWarning, string controlName, CancellationTokenSource cancelTokenSource = null,
                                                      ProgressReporter progressReporter = null, SmoothProgressBar progressBar = null, bool checkBoxValue = false, TableInfo table = null)
        {
            CheckBoxValue = checkBoxValue;
            var urlsCollect = new UrlHandler().SetUrlForParse(urls, controlName).ToArray();
            urlsCollect = DetermineUrlForHiddenFormWork(urlsCollect, controlName);
            if (table != null)
                return ParseExistingTable(table, urlsCollect, controlName);

            var html = HtmlProvider.GetResponse<string>(urlsCollect[0], controlName.Equals("SavePlenarySessionDatesBtn"));
            if (html.Error != null)
                return new ParseResult<FileModel> { Error = new ErrorModel { ErrorMsg = html.Error.ErrorMsg, ControlName = controlName, Operation = $"GetResponse from {urlsCollect[0]}" } };

            var primaryTable = GetTable(html.ReceivedData, urlsCollect, controlName, limitWarning);
            var additionalTable = GetAdditionalTable(controlName, urlsCollect);
            if (primaryTable.Error != null || additionalTable.Error != null)
            {
                var error = primaryTable.Error ?? additionalTable.Error;
                return new ParseResult<FileModel> { Error = new ErrorModel { ErrorMsg = error.ErrorMsg, ControlName = controlName, Operation = error.Operation } };
            }

            primaryTable = MergeTables(primaryTable, additionalTable);

            if (controlName.Equals("SaveAllLawsFromCurrentConvocationBtn"))
            {
                return primaryTable.Rows.Any() ?
                    SeacrhAndUpdateFiles(primaryTable, urlsCollect, controlName, cancelTokenSource, progressReporter, progressBar)
                    : new ParseResult<FileModel> { XmlDocuments = new List<FileModel>(), Files = new List<FileModel>() };
            }
            else
            {
                return primaryTable.Rows.Any() ?
                    SearchFiles(primaryTable, urlsCollect, controlName, cancelTokenSource, progressReporter, progressBar)
                    : new ParseResult<FileModel> { XmlDocuments = new List<FileModel>(), Files = new List<FileModel>() };
            }
        }

        /// <summary>
        /// метод устанавливает номер текущего созыва ВР в ссылку для парсинга (для работы из сервиса)
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        private string[] DetermineUrlForHiddenFormWork(string[] urls, string controlName)
        {
            if (controlName.ContainsAny("SaveDeputyBtn", "SaveСommitteesBtn", "SaveFractionsBtn") && !Regex.IsMatch(urls[0], @"\d+$"))
                urls[0] += _convocationNumber;
            return urls;
        }

        /// <summary>
        /// метод возвращает коллекцию xml и rtf, doc, img в виде экземпляров FileModel (тело файлов в byte[]) с сайта Верховной Рады
        /// </summary>
        /// <param name="satellitePages">url коллекция</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="table">таблица с данными</param>
        private ParseResult<FileModel> ParseExistingTable(TableInfo table, string[] satellitePages, string controlName = null)
        {
            return SearchFiles(table, satellitePages, controlName);
        }

        /// <summary>
        /// метод парсит страницу со списком законопроектов, комитетов, депутатов, пленарных заседаний
        /// </summary>
        /// <param name="html">строка для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="satellitePage">дополнительный url для парсинга</param>
        /// <param name="limitWarning">ограничение на количество загружаемых файлов</param>
        private TableInfo GetTable(string html, string[] satellitePage, string controlName = null, bool limitWarning = false)
        {
            var document = Converter.ConvertToHtmlDocument(html);
            var docNode = document.DocumentNode;
            var tableParser = new TableDataParser();
            //находим таблицу
            var tableInfo = tableParser.GetTable(docNode, controlName);

            //если данных нет либо записей больше 100 (для поиска законопроектов по датам)
            var error = tableParser.CheckDataCorrectness(tableInfo.Table, limitWarning);
            if (error != null)
                return new TableInfo { Error = new ErrorModel { ErrorMsg = error.ErrorMsg, Operation = error.Operation, ControlName = controlName } };

            //находим строки таблицы
            tableInfo.Rows = tableParser.GetRows(tableInfo.Table, controlName, satellitePage[0]);
            return tableInfo;
        }

        /// <summary>
        /// метод выполняет итеративный поиск и создание файлов в строках переданной таблицы
        /// </summary>
        /// <param name="tableInfo">таблица со строками</param>
        /// <param name="satellitePage">дополнительный url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="cancelTokenSource">источник токена отмены</param>
        /// <param name="progressReporter">экзмепляр ProgressReporter для доступа к UI</param>
        /// <param name="progressBar">ссылка на progressBar из UI</param>
        private ParseResult<FileModel> SearchFiles(TableInfo tableInfo, string[] satellitePage, string controlName = null,
            CancellationTokenSource cancelTokenSource = null, ProgressReporter progressReporter = null, SmoothProgressBar progressBar = null)
        {
            //устанавливаем макс значение прогрессбара
            if (progressBar != null)
                progressReporter?.ReportProgress(() => progressBar.Maximum = tableInfo.Rows.Count);

            var collection = new List<FileModel>();
            var files = new List<FileModel>();

            var watch = new Stopwatch();
            watch.Start();

            _log.Info("start parse, rows to parse count " + tableInfo.Rows.Count);

#if Debug_sync
            try
            {
                foreach (HtmlNode row in tableInfo.Rows)
                {
                    if (cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested)
                        cancelTokenSource.Token.ThrowIfCancellationRequested();

                    var parseResult = GetFiles(controlName, row, satellitePage);

                    if (parseResult.XmlDocuments == null || !parseResult.XmlDocuments.Any() || parseResult.Error != null)
                        throw FormattedExceptionCreator.CreateExc(parseResult.Error, "Xml документ дорівнює null", controlName);

                    foreach (var root in parseResult.XmlDocuments.Where(r => r.HasChildNodes))
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlNode tmp = doc.ImportNode(root, true);
                        doc.AppendChild(tmp);
                        var file = CreateFileFromXml(doc);
                        if (file.Error != null)
                            throw FormattedExceptionCreator.CreateExc(file.Error, null, controlName);
                        collection.Add(file);
                    }

                    if (parseResult.Files.Any())
                        parseResult.Files.ForEach(f => files.Add(f));

                    //обновляем значение прогрессбара
                    if (progressBar != null)
                        progressReporter?.ReportProgress(() =>
                        {
                            if (progressBar.Value < progressBar.Maximum)
                                progressBar.Value += 1;
                        });
                }
            }
            catch (Exception ex)
            {
                collection.ClearCollection();
                files.ClearCollection();
                GarbageCleaner.ClearGarbage();
                //_log.Error($"CreateAndFillRootElement.\n{exception.ErrorMsg}\nStackTrace:{ex.StackTrace}");
                return cancelTokenSource.Token.IsCancellationRequested
                    ? new ParseResult<FileModel> { Error = new ErrorModel { ErrorMsg = "Пошук був припинений", Operation = "CancelSearch", ControlName = string.Empty } }
                    : new ParseResult<FileModel> { Error = ex.Data.Values.OfType<ErrorModel>().First() };
            }
#else
            try
            {
                Parallel.ForEach<HtmlNode, ParseResult<FileModel>>(
                    tableInfo.Rows,
                    new ParallelOptions { MaxDegreeOfParallelism = 10 },
                    () => new ParseResult<FileModel> { Files = new List<FileModel>(), XmlDocuments = new List<FileModel>() },
                    (row, state, localList) =>
                    {
                        if (cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested)
                        {
                            state.Stop();
                            cancelTokenSource.Token.ThrowIfCancellationRequested();
                        }

                        var parseResult = GetFiles(controlName, row, satellitePage);
                        if (parseResult.XmlDocuments == null || !parseResult.XmlDocuments.Any() || parseResult.Error != null)
                        {
                            state.Stop();
                            throw FormattedExceptionCreator.CreateExc(parseResult.Error, "Xml документ дорівнює null", controlName);
                        }
                        foreach (var root in parseResult.XmlDocuments.Where(r => r.HasChildNodes))
                        {
                            XmlDocument doc = new XmlDocument();
                            XmlNode tmp = doc.ImportNode(root, true);
                            doc.AppendChild(tmp);
                            var file = CreateFileFromXml(doc);
                            if (file.Error != null)
                            {
                                state.Stop();
                                throw FormattedExceptionCreator.CreateExc(file.Error, null, controlName);
                            }
                            localList.XmlDocuments.Add(file);
                        }
                        if (parseResult.Files.Any())
                            parseResult.Files.ForEach(f => { localList.Files.Add(f); });

                        //обновляем значение прогрессбара
                        if (progressBar != null)
                            progressReporter?.ReportProgress(() =>
                            {
                                if (progressBar.Value < progressBar.Maximum)
                                    progressBar.Value += 1;
                            });

                        return localList;
                    },
                    (finalResult) =>
                    {
                        lock (_localLockObject)
                        {
                            if (finalResult.XmlDocuments.Any())
                                collection.AddRange(finalResult.XmlDocuments);
                            if (finalResult.Files.Any())
                                files.AddRange(finalResult.Files);
                        }
                    }
                );
            }
            catch (AggregateException aex)
            {
                collection.ClearCollection();
                files.ClearCollection();
                GarbageCleaner.ClearGarbage();
                foreach (var ex in aex.InnerExceptions)
                {
                    if (!(cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested))
                    {
                        var except = ex.Data.Values.OfType<ErrorModel>().First();
                        _log.Error($"CreateAndFillRootElement.\n{except.ErrorMsg}\nStackTrace:{except.Operation}");
                    }
                    else
                        _log.Error($"CreateAndFillRootElement.\n{ex.Message}");
                }

                return cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested
                    ? new ParseResult<FileModel> { Error = new ErrorModel { ErrorMsg = "Пошук був припинений", Operation = "CancelSearch", ControlName = string.Empty } }
                    : new ParseResult<FileModel> { Error = aex.InnerExceptions[0].Data.Values.OfType<ErrorModel>().First() };
            }
#endif

            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            _log.Info($"complete parse. Parsing time {ts}");

            return new ParseResult<FileModel> { XmlDocuments = collection, Files = files };
        }

        /// <summary>
        /// метод выполняет итеративный поиск и создание файлов в строках переданной таблицы
        /// </summary>
        /// <param name="tableInfo">таблица со строками</param>
        /// <param name="satellitePage">дополнительный url для парсинга</param>
        /// <param name="controlName">имя контрола</param>
        /// <param name="cancelTokenSource">источник токена отмены</param>
        /// <param name="progressReporter">экзмепляр ProgressReporter для доступа к UI</param>
        /// <param name="progressBar">ссылка на progressBar из UI</param>
        private ParseResult<FileModel> SeacrhAndUpdateFiles(TableInfo tableInfo, string[] satellitePage, string controlName = null,
            CancellationTokenSource cancelTokenSource = null, ProgressReporter progressReporter = null, SmoothProgressBar progressBar = null)
        {
            //устанавливаем макс значение прогрессбара
            if (progressBar != null)
                progressReporter?.ReportProgress(() => progressBar.Maximum = tableInfo.Rows.Count);

            var watch = new Stopwatch();
            watch.Start();

            _log.Info("start parse, rows to parse count " + tableInfo.Rows.Count);

            var arrayCount = (int)Math.Ceiling(tableInfo.Rows.Count / (decimal)10);
            List<HtmlNode>[] array = new List<HtmlNode>[arrayCount];
            if (tableInfo.Rows.Count > 10)
            {
                for (int i = 0; i < arrayCount; i++)
                {
                    array[i] = tableInfo.Rows.Take(10).ToList();
                    tableInfo.Rows.RemoveRange(0, array[i].Count);
                }
            }
            else
                array[0] = tableInfo.Rows;

            for (int i = 0; i < array.Length; i++)
            {
                var collection = new List<FileModel>();
                var files = new List<FileModel>();
#if Debug_sync
                try
                {
                    foreach (HtmlNode row in array[i])
                    {
                        if (cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested)
                            cancelTokenSource.Token.ThrowIfCancellationRequested();

                        if (LawsDonwloadOptions.LogBasedDownloadEnabled)
                        {
                            var lawNumber = "Законопроект#" + row.ChildNodes.FirstOrDefault(n => n.Name == "td")?.InnerText.Replace("/", "_").Trim() + "#";
                            if (Logger.DonloadedLaws.Contains(lawNumber))
                            {
                                continue;
                            }
                        }

                        var parseResult = GetFiles(controlName, row, satellitePage);

                        if (parseResult.XmlDocuments == null || !parseResult.XmlDocuments.Any() ||
                            parseResult.Error != null)
                            throw FormattedExceptionCreator.CreateExc(parseResult.Error, "Xml документ дорівнює null",
                                controlName);

                        foreach (var root in parseResult.XmlDocuments.Where(r => r.HasChildNodes))
                        {
                            XmlDocument doc = new XmlDocument();
                            XmlNode tmp = doc.ImportNode(root, true);
                            doc.AppendChild(tmp);
                            var file = CreateFileFromXml(doc);
                            if (file.Error != null)
                                throw FormattedExceptionCreator.CreateExc(file.Error, null, controlName);
                            collection.Add(file);
                        }

                        if (parseResult.Files.Any())
                            parseResult.Files.ForEach(f => files.Add(f));

                        //обновляем значение прогрессбара
                        if (progressBar != null)
                            progressReporter?.ReportProgress(() =>
                            {
                                if (progressBar.Value < progressBar.Maximum)
                                    progressBar.Value += 1;
                            });
                    }
                    var downloadedFiles = new ParseResult<FileModel> { XmlDocuments = collection, Files = files };
                    var filesForSave = new FilesCompare().GetFilesNeedToSaveFromDataBase(downloadedFiles, _urls);
                    var savedFiles = new FilesUpload().UploadFilesToDataBase(filesForSave, _urls, null, null, true);
                }
                catch (Exception ex)
                {
                    collection.ClearCollection();
                    files.ClearCollection();
                    GarbageCleaner.ClearGarbage();
                    //_log.Error($"CreateAndFillRootElement.\n{exception.ErrorMsg}\nStackTrace:{ex.StackTrace}");
                    return cancelTokenSource.Token.IsCancellationRequested
                        ? new ParseResult<FileModel>
                        {
                            Error = new ErrorModel
                            {
                                ErrorMsg = "Пошук був припинений",
                                Operation = "CancelSearch",
                                ControlName = string.Empty
                            }
                        }
                        : new ParseResult<FileModel> {Error = ex.Data.Values.OfType<ErrorModel>().First()};
                }
#else
                try
                {
                    Parallel.ForEach<HtmlNode, ParseResult<FileModel>>(
                        array[i],
                        new ParallelOptions { MaxDegreeOfParallelism = 10 },
                        () => new ParseResult<FileModel> { Files = new List<FileModel>(), XmlDocuments = new List<FileModel>() },
                        (row, state, localList) =>
                        {
                            if (cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested)
                            {
                                state.Stop();
                                cancelTokenSource.Token.ThrowIfCancellationRequested();
                            }

                            if (!string.IsNullOrEmpty(controlName) && controlName.Equals("SaveLawsByDatePickerBtn") && satellitePage[0].Contains("20000") && LawsDonwloadOptions.LogBasedDownloadEnabled)
                            {
                                var lawNumber = "Законопроект#" + row.ChildNodes.FirstOrDefault(n => n.Name == "td")?.InnerText.Replace("/", "_").Trim() + "#";
                                if (Logger.DonloadedLaws.Contains(lawNumber))
                                {
                                    return null;
                                }
                            }

                            var parseResult = GetFiles(controlName, row, satellitePage);
                            if (parseResult.XmlDocuments == null || !parseResult.XmlDocuments.Any() || parseResult.Error != null)
                            {
                                state.Stop();
                                throw FormattedExceptionCreator.CreateExc(parseResult.Error, "Xml документ дорівнює null", controlName);
                            }
                            foreach (var root in parseResult.XmlDocuments.Where(r => r.HasChildNodes))
                            {
                                XmlDocument doc = new XmlDocument();
                                XmlNode tmp = doc.ImportNode(root, true);
                                doc.AppendChild(tmp);
                                var file = CreateFileFromXml(doc);
                                if (file.Error != null)
                                {
                                    state.Stop();
                                    throw FormattedExceptionCreator.CreateExc(file.Error, null, controlName);
                                }
                                localList.XmlDocuments.Add(file);
                            }
                            if (parseResult.Files.Any())
                                parseResult.Files.ForEach(f => { localList.Files.Add(f); });

                            //обновляем значение прогрессбара
                            if (progressBar != null)
                                progressReporter?.ReportProgress(() =>
                                {
                                    if (progressBar.Value < progressBar.Maximum)
                                        progressBar.Value += 1;
                                });

                            return localList;
                        },
                        (finalResult) =>
                        {
                            lock (_localLockObject)
                            {
                                if (finalResult.XmlDocuments.Any())
                                    collection.AddRange(finalResult.XmlDocuments);
                                if (finalResult.Files.Any())
                                    files.AddRange(finalResult.Files);
                            }
                        }
                    );
                    var downloadedFiles = new ParseResult<FileModel> { XmlDocuments = collection, Files = files };
                    var filesForSave = new FilesCompare().GetFilesNeedToSaveFromDataBase(downloadedFiles, _urls);
                    var savedFiles = new FilesUpload().UploadFilesToDataBase(filesForSave, _urls, null, null, true);
                }
                catch (AggregateException aex)
                {
                    collection.ClearCollection();
                    files.ClearCollection();
                    GarbageCleaner.ClearGarbage();
                    foreach (var ex in aex.InnerExceptions)
                    {
                        if (!(cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested))
                        {
                            var except = ex.Data.Values.OfType<ErrorModel>().First();
                            _log.Error($"CreateAndFillRootElement.\n{except.ErrorMsg}\nStackTrace:{except.Operation}");
                        }
                        else
                            _log.Error($"CreateAndFillRootElement.\n{ex.Message}");
                    }

                    return cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested
                        ? new ParseResult<FileModel> { Error = new ErrorModel { ErrorMsg = "Пошук був припинений", Operation = "CancelSearch", ControlName = string.Empty } }
                        : new ParseResult<FileModel> { Error = aex.InnerExceptions[0].Data.Values.OfType<ErrorModel>().First() };
                }
#endif
            }

            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            _log.Info($"complete saving. Parsing time {ts}");

            return new ParseResult<FileModel> { XmlDocuments = null, Files = null };
        }

        /// <summary>
        /// метод возвращает колелкцию строк с дополнительной страницы
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="urlsCollect"></param>
        /// <returns></returns>
        private TableInfo GetAdditionalTable(string controlName, string[] urlsCollect)
        {
            TableInfo tableInfo = new TableInfo { Rows = new List<HtmlNode>() };
            if (controlName.Equals("SaveDeputyBtn") && urlsCollect[0].EndsWith(_convocationNumber.ToString()))
            {
                var html = HtmlProvider.GetResponse<string>(urlsCollect[0] + "&pid_id = -3");
                if (html.Error != null)
                    return new TableInfo { Error = new ErrorModel { ErrorMsg = html.Error.ErrorMsg, Operation = html.Error.Operation, ControlName = controlName } };
                tableInfo = GetTable(html.ReceivedData, urlsCollect, controlName);
                if (tableInfo.Error != null)
                    return new TableInfo { Error = new ErrorModel { ErrorMsg = tableInfo.Error.ErrorMsg, Operation = tableInfo.Error.Operation, ControlName = controlName } };
            }
            return tableInfo;
        }

        /// <summary>
        /// метод соединяет две таблицы
        /// </summary>
        /// <param name="primaryTable"></param>
        /// <param name="subordinateTable"></param>
        /// <returns></returns>
        private TableInfo MergeTables(TableInfo primaryTable, TableInfo subordinateTable)
        {
            if (subordinateTable.Rows == null || !subordinateTable.Rows.Any()) return primaryTable;
            primaryTable.Rows.AddRange(new List<HtmlNode>(subordinateTable.Rows.Select(row => row.Clone())));
            subordinateTable.Rows.Clear();
            subordinateTable = null;
            return primaryTable;
        }

        /// <summary>
        /// метод возвращает промежуточные документы xml и итоговые файлы других расширений
        /// </summary>
        /// <param name="controlName">имя контрола</param>
        /// <param name="row">строка для парсинга</param>
        /// <param name="satellitePage">коллекция вспомогательных url</param>
        ///// <param name="tableName">имя таблицы</param>
        private ParseResult<XmlElement> GetFiles(string controlName, HtmlNode row, string[] satellitePage)
        {
            switch (controlName)
            {
                case "SaveDeputyBtn":
                case "SaveDepVotingBtn":
                case "SaveDepSpeechesBtn":
                case "SaveDepQueriesBtn":
                case "SaveDepLawActivityBtn":
                case "SaveСommitteesWorkBtn":
                    return new MultiMainXmlAndFilesCreator().GetBlankFiles(row, satellitePage, controlName, CheckBoxValue);
                default:
                    return new SingleMainXmlAndFilesCreator().GetBlankFiles(row, satellitePage, controlName, CheckBoxValue);
            }
        }

        /// <summary>
        /// метод созадет модель файла из xml-документа
        /// </summary>
        /// <param name="doc">xml-документ</param>
        /// <returns></returns>
        private FileModel CreateFileFromXml(XmlDocument doc)
        {
            try
            {
                var blocksCollect = doc.GetElementsByTagName("block");
                var firstPartOfName = DictionaryInitializer.FirstPartOfFileName.FirstOrDefault(n => n.Key == blocksCollect.Item(0)?.Attributes?[1].Value).Value;

                if (firstPartOfName.Equals("Засідання"))
                    blocksCollect = doc.GetElementsByTagName("row");

                var secondPartOfName = string.Empty;

                if (blocksCollect.Item(0)?.Attributes?[1].Value == "VoteInfo")
                {
                    secondPartOfName = blocksCollect.Item(0)?.FirstChild.InnerText + "#Дата голосування_ " + blocksCollect.Item(0)?.LastChild.InnerText;
                }
                else
                {
                    secondPartOfName = DictionaryInitializer.SecondPartOfFileName.FirstOrDefault(r => Regex.IsMatch(firstPartOfName, r.Key)).Value(blocksCollect) ?? string.Empty;
                }
                
                firstPartOfName += "#" + new Regex("[/:\"]").Replace(secondPartOfName, "_") + "#";

                if (firstPartOfName.Contains("Комітет#Графік"))
                    firstPartOfName += blocksCollect.Item(0)?.ChildNodes[1].InnerText
                                       + "#" + blocksCollect.Item(0)?.ChildNodes[5].InnerText
                                       + "#" + blocksCollect.Item(0)?.ChildNodes[2].InnerText + "#";

                return new FileCreator().CreateFile(doc, ".xml", firstPartOfName);
            }
            catch (Exception e)
            {
                _log.Error($"CreateFileFromXml.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new FileModel { Error = new ErrorModel { ErrorMsg = "Не вдається визначити ім'я файлу", Operation = "CreateFileFromXml" } };
            }
            finally
            {
                // ReSharper disable once RedundantAssignment
                doc = null;
            }
        }
    }
}
