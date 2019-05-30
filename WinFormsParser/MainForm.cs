using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;
using GovernmentParse.DataProviders;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse
{
    public partial class MainForm : Form
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        private readonly ResourceReader _resourceReader;

        private readonly ProgressReporter _progressReporter;

        private CancellationTokenSource _cancellationTokenSource;

        private List<string> ConvocationList { get; }

        private bool LimitWarning { get; set; }

        private bool LowsIsSaved { get; set; }

        private bool СommitteesIsSaved { get; set; }

        private bool СommitteesWorkIsSaved { get; set; }

        private bool PlenarySessionIsSaved { get; set; }

        private bool DeputyIsSaved { get; set; }

        private bool DepSpeechesIsSaved { get; set; }

        private bool DepVotingIsSaved { get; set; }

        private bool DepQueriesIsSaved { get; set; }

        private bool DepLawActivityIsSaved { get; set; }

        private bool FractionsIsSaved { get; set; }

        private bool IsPhotoDownload { get; set; }

        public MainForm()
        {
            InitializeComponent();
            _ini = new IniReader();
            _resourceReader = new ResourceReader();
            _progressReporter = new ProgressReporter();
            saveReportBtn.Enabled = false;
            CancelBtn.Enabled = false;
            LimitWarning = false;
            TextBoxInformant.ReadOnly = true;
            TextBoxInformant.BackColor = SystemColors.Window;
            TextBoxInformant.Text = _resourceReader.GetString("StartNotification");
            SaveLawsBtn.Text = SaveLawsBtn.Text.Insert(SaveLawsBtn.Text.Length, "\n" + DateTime.Today.AddDays(-7).ToShortDateString() + " - " + DateTime.Today.ToShortDateString());
            SaveСommitteesWorkBtn.Enabled = false;
            ConvocationList = new List<string> { "VIII скликання", "VII скликання", "VI скликання", "V скликання", "IV скликання" };
            ConvocationComboBox.DataSource = ConvocationList;
        }

        /// <summary>
        /// метод обрабатывает нажатие на кнопку
        /// </summary>
        /// <param name="sender"></param>
        private async void UpdateFiles(object sender)
        {
            try
            {
                WorkStart();

                var controlName = ((Button) sender).Name;
                if (!controlName.ContainsAny("SaveLawsByDatePickerBtn", "SaveByLowNameBtn"))
                    if (!CheckForRetention(controlName)) return;

                ParseResult<FileModel> parseResult;

                if (controlName.Equals("UpdateLaws"))
                {
                    var downloadResult = await DownloadXmlFiles(controlName);
                    if (downloadResult.Error != null) return;

                    var compareResult = await CompareXmlFiles(downloadResult.RecievedData);
                    if (compareResult.Error != null) return;

                    var createResult = await CreateLawsTable(compareResult.List);
                    if (createResult.Error != null) return;

                    parseResult = await SearchFiles("SaveLawsBtn", createResult);
                }
                else
                    parseResult = await SearchFiles(controlName);
                if (parseResult.Error != null) return;

                var compareWithSavedResult = await CompareFilesWithSaved(parseResult, _urls);
                if (compareWithSavedResult.Error != null) return;

                var saveResult = await SaveFiles(compareWithSavedResult, _urls);
                if (saveResult.Error != null) return;

                await CreateReport(saveResult, controlName);
            }
            catch (Exception ex)
            {
                _log.Error($"CreateFileFromXml.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
            }
            finally
            {
                WorkComplete();
            }
        }

        #region методы для обновления данных путем сравнения с существующими в базе
        private async Task<ParseResult<FileModel>> SearchFiles(string controlName, TableInfo table = null)
        {
            progressBar.Value = 0;
            progressBar.Step = 1;
            _cancellationTokenSource = new CancellationTokenSource();
            var parseResult = Task.Run(() =>
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                return new ParseResult<FileModel>();//FilesProvider().GetFilesFromSite(_urls, LimitWarning, controlName, _cancellationTokenSource, _progressReporter, progressBar, IsPhotoDownload, table);
            }, _cancellationTokenSource.Token);

            await _progressReporter.RegisterContinuation(parseResult, () =>
            {
                if (parseResult.IsCanceled)
                    MessageBox.Show(_resourceReader.GetString("SearchStopped"));

                if (parseResult.Result.Error != null)
                    MessageBox.Show(parseResult.Result.Error.ErrorMsg);
            });
            return parseResult.Result;
        }

        private async Task<FilesToSave> CompareFilesWithSaved(ParseResult<FileModel> files, UrlsCollection urls)
        {
            var fileCompare = new FilesCompare();
            var compareResult = Task.Run(() => fileCompare.GetFilesNeedToSaveFromDataBase(files, urls));

            await _progressReporter.RegisterContinuation(compareResult, () =>
            {
                if (compareResult.Result.Error != null)
                    MessageBox.Show(_resourceReader.GetString("CompareError"));
            });
            return compareResult.Result;
        }

        private async Task<SavedFiles> SaveFiles(FilesToSave files, UrlsCollection urls)
        {
            var filesUpload = new FilesUpload();
            var saveResult = Task.Run(() => filesUpload.UploadFilesToDataBase(files, urls));

            await saveResult.ContinueWith(_ =>
            {
                if (saveResult.Result.Error != null)
                    MessageBox.Show(_resourceReader.GetString("SaveError"));
            });
            return saveResult.Result;
        }

        private async Task CreateReport(SavedFiles savedFiles, string controlName)
        {
            var report = Task.Run(() => ReportCreator.CreateReport(savedFiles));

            await _progressReporter.RegisterContinuation(report, () =>
            {
                if (!string.IsNullOrEmpty(report.Result))
                {
                    _progressReporter.ReportProgress(() =>
                        {
                            TextBoxInformant.Text = report.Result;
                            TextBoxInformant.Refresh();
                        }
                    );
                    MessageBox.Show(_resourceReader.GetString("SuccessSave"));
                    GetofSetSavedState(controlName, true, true);
                }
                else
                    MessageBox.Show(_resourceReader.GetString("NothingToSave"));
            });
        }
        #endregion

        #region методы для обновления и загрузки новых законопроектов путем скачивания и сравнения xml с API Верховной Рады
        private async Task<DownloadResult<XmlDocument>> DownloadXmlFiles(string controlName)
        {
            var downloadResult = Task.Run(() => new XmlDownloader().GetXmlDocuments(_urls, controlName));
            await _progressReporter.RegisterContinuation(downloadResult, () =>
                {
                    if (downloadResult.Result.Error != null) return;
                        MessageBox.Show(_resourceReader.GetString("DownloadForCompareError"));
                }
            );
            return downloadResult.Result;
        }

        private async Task<CompareXmlFilesResult> CompareXmlFiles(List<XmlDocument> files)
        {
            var compareResult = Task.Run(() => new XmlComparer().CompareFiles(files));
            await _progressReporter.RegisterContinuation(compareResult, () =>
                {
                    if (compareResult.Result.Error != null) return;
                    {
                        MessageBox.Show(_resourceReader.GetString("CompareDownloadedError"));
                    }
                }
            );
            return compareResult.Result;
        }

        private async Task<TableInfo> CreateLawsTable(List<TableRow> laws)
        {
            var createResult = Task.Run(() => new LawsTableCreator().CreateLawsTable(laws));
            await _progressReporter.RegisterContinuation(createResult, () =>
                {
                    if (createResult.Result.Error != null) return;
                        MessageBox.Show(_resourceReader.GetString("CreateLawsTableError"));
                }
            );
            return createResult.Result;
        }
        #endregion

        #region законы
        /// <summary>
        /// метод запускает парсинг законопроектов, принятых за текущую неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLawsBtn_Click(object sender, EventArgs e)
        {
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг законопроектов за выбранный период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLawsByDatePickerBtn_Click(object sender, EventArgs e)
        {
            ChangeLimitWarning(true);
            if (startDate.Value.Date > endDate.Value.Date)
            {
                MessageBox.Show(_resourceReader.GetString("DatePickerWarning"));
                return;
            }

            _urls = _ini.AutoReadIni();
            string[] url = _urls.LawsByDatePage.Split('&');
            url[0] += startDate.Value.ToShortDateString() + "&";
            url[1] += endDate.Value.ToShortDateString() + "&";
            _urls.LawsByDatePage = string.Empty;
            foreach (var str in url)
                _urls.LawsByDatePage += str;

            UpdateFiles(sender);
        }

        private void SaveByLowNameBtn_Click(object sender, EventArgs e)
        {
            TextBoxInformant.Clear();
            if (string.IsNullOrWhiteSpace(SearchLowTextBox.Text))
            {
                MessageBox.Show(_resourceReader.GetString("InvalidSearchLowTextBoxValue"));
                return;
            }
            var lowNums = InputValidator.Сorrect(SearchLowTextBox.Text);
            lowNums = HttpUtility.UrlEncode(lowNums, Encoding.GetEncoding("windows-1251"));
            _urls = _ini.AutoReadIni();
            _urls.LawByNamePage += lowNums;

            UpdateFiles(sender);
        }

        private void UpdateLaws_Click(object sender, EventArgs e)
        {
            UpdateFiles(sender);
        }
        #endregion

        #region комитеты
        /// <summary>
        /// метод запускает парсинг комитетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveСommitteesBtn_Click(object sender, EventArgs e)
        {
            var pageNumber = SpotConvocationLink();
            if (pageNumber < 7)
            {
                MessageBox.Show(_resourceReader.GetString("ArchiveOfCommitteeDoesNotExist")); return;
            }
            if (!ConvocationComboBox.Text.Equals(ConvocationList.First()))
                _urls.CommitteesPage += "?skl=" + pageNumber;
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг еженедельного плана работы комитетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveСommitteesWorkBtn_Click(object sender, EventArgs e)
        {
            UpdateFiles(sender);
        }
        #endregion

        #region пленарые заседания
        /// <summary>
        /// метод запускает парсинг графика проведения пленарных заседаний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlenarySessionCalendarPlanBtn_Click(object sender, EventArgs e)
        {
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг плана работы комитетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlenarySessionDatesBtn_Click(object sender, EventArgs e)
        {
            UpdateFiles(sender);
        }
        #endregion

        #region депутаты
        /// <summary>
        /// метод запускает парсинг депутатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDeputyBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг депутатских голосований
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDepVotingBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            _urls.DeputyVotePage = SpotDefaultDateTimePeriod(_urls.DeputyVotePage);
            _urls.DeputyVotePage = ChangeLinkIfArchive(_urls.DeputyVotePage);
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг депутатских выступлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDepSpeechesBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            _urls.DeputySpeechPage = SpotDefaultDateTimePeriod(_urls.DeputySpeechPage);
            _urls.DeputySpeechPage = ChangeLinkIfArchive(_urls.DeputySpeechPage);
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг депутатских запросов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDepQueriesBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            _urls.DeputyQueriesPage = ChangeConvocationInLink(_urls.DeputyQueriesPage);
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод запускает парсинг депутатской законотвроч. активности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDepLawActivityBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            _urls.DeputyLawActivityPage = ChangeConvocationInLink(_urls.DeputyLawActivityPage);
            UpdateFiles(sender);
        }

        /// <summary>
        /// метод меняет в ссылке-заготовке на страницу депутата номер созыва согласно выбранного в ConvocationComboBox созыва
        /// </summary>
        /// <returns>возвращает номер страницы созыва, использующийся в url страниц Верховной Рады</returns>
        private int SpotConvocationLinkForDeputyParse()
        {
            var pageNumber = SpotConvocationLink();
            _urls.DeputiesPage += pageNumber;
            return pageNumber;
        }

        /// <summary>
        /// метод меняет в ссылке даты на указанные в DepVotingDateTimePickStart и DepVotingDateTimePickEnd
        /// </summary>
        /// <param name="url">url для корректировки</param>
        private string SpotDefaultDateTimePeriod(string url)
        {
            url = url.Replace("StartDateToReplace", DepVotingDateTimePickStart.Value.ToShortDateString());
            return url.Replace("EndDateToReplace", DepVotingDateTimePickEnd.Value.ToShortDateString());
        }

        /// <summary>
        /// устанавливает в переданный url номер страницы созыва, использующийся в url страниц Верховной Рады
        /// </summary>
        /// <param name="url">url для корректировки</param>
        private string ChangeConvocationInLink(string url)
        {
            var pageNumber = SpotConvocationLinkForDeputyParse();
            return url.Replace("ConvocationToReplace", pageNumber.ToString());
        }

        /// <summary>
        /// метод добавляет в ссылку указание на принадлежность архив даты на указанные в DepVotingDateTimePickStart и DepVotingDateTimePickEnd
        /// </summary>
        /// <param name="url">url для корректировки</param>
        private string ChangeLinkIfArchive(string url)
        {
            return !ConvocationComboBox.Text.Equals(ConvocationList.First()) ? url.Insert(url.IndexOf("dep_", StringComparison.Ordinal), "arh_") : url;
        }
        #endregion

        #region фракции
        /// <summary>
        /// метод запускает парсинг фракций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFractionsBtn_Click(object sender, EventArgs e)
        {
            var pageNumber = SpotConvocationLink();
            if (pageNumber < 7)
            {
                MessageBox.Show(_resourceReader.GetString("ArchiveOfFractionsDoesNotExist")); return;
            }
            if (!ConvocationComboBox.Text.Equals(ConvocationList.First()))
                _urls.FractionsMainPage += "?skl=" + pageNumber;
            UpdateFiles(sender);
        }
        #endregion
        
        /// <summary>
        /// метод позволяет сохранить отчет о парсинге
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveReportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog { FileName = "report.txt" };
            if (savefile.ShowDialog() == DialogResult.OK)
                File.WriteAllText(savefile.FileName, TextBoxInformant.Text);
        }

        /// <summary>
        /// метод отменяет выполнение любой операции, кроме обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// метод изменяет досутпность кнопок перед началом работы, очищает текстовое поле
        /// </summary>
        private void WorkStart()
        {
            ChangeAccessToSaveReportBtn(false);
            ChangeButtonsStatus(false);
            TextBoxInformant.Text = string.Empty;
            if (_urls == null) _urls = _ini.AutoReadIni();
        }

        /// <summary>
        /// метод изменяет досутпность кнопок после окончания работы
        /// </summary>
        private void WorkComplete()
        {
            ChangeButtonsStatus(true);
            ChangeAccessToSaveReportBtn(!string.IsNullOrEmpty(TextBoxInformant.Text));
            _urls = null;
        }

        /// <summary>
        /// метод выдает предупреждение при попытке запустить повторный парсинг
        /// </summary>
        /// <returns></returns>
        private bool CheckForRetention(string controlName)
        {
            if (!GetofSetSavedState(controlName)) return true;
            var message = DictionaryInitializer.BtnMessageDictionary.First(m => m.Key.Equals(controlName)).Value;
            message = _resourceReader.GetString(message) + _resourceReader.GetString("RetentionMessage");
            var result = MessageBox.Show(message, _resourceReader.GetString("RetentionHeader"), MessageBoxButtons.YesNo);
            return result != DialogResult.No;
        }

        /// <summary>
        /// метод блокирует кнопку сохранения отчета
        /// </summary>
        private void ChangeAccessToSaveReportBtn(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(ChangeAccessToSaveReportBtn), value);
                return;
            }
            saveReportBtn.Enabled = value;
        }

        /// <summary>
        /// метод меняет значение поля, отвечающего за выдачу предупреждения о количестве записей
        /// </summary>
        private void ChangeLimitWarning(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(ChangeLimitWarning), value);
                return;
            }
            LimitWarning = value;
        }

        /// <summary>
        /// метод блокирует кнопки парсинга
        /// </summary>
        /// <param name="value"></param>
        private void ChangeButtonsStatus(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(ChangeButtonsStatus), value);
                return;
            }
            SaveFractionsBtn.Enabled = value;
            SavePlenarySessionCalendarPlanBtn.Enabled = value;
            SavePlenarySessionDatesBtn.Enabled = value;
            SaveСommitteesBtn.Enabled = value;
            SaveLawsBtn.Enabled = value;
            SaveLawsByDatePickerBtn.Enabled = value;
            SaveByLowNameBtn.Enabled = value;
            SaveDeputyBtn.Enabled = value;
            SaveDepLawActivityBtn.Enabled = value;
            SaveDepVotingBtn.Enabled = value;
            SaveDepSpeechesBtn.Enabled = value;
            SaveDepQueriesBtn.Enabled = value;
            UpdateLaws.Enabled = value;
            CancelBtn.Enabled = !value;
        }

        private bool GetofSetSavedState(string controlName, bool setValue = false, bool newValue = false)
        {
            switch (controlName)
            {
                case "SaveLawsBtn":
                    if (setValue) LowsIsSaved = newValue;
                    return LowsIsSaved;
                case "SaveСommitteesBtn":
                    if (setValue) СommitteesIsSaved = newValue;
                    return СommitteesIsSaved;
                case "SaveСommitteesWorkBtn":
                    if (setValue) СommitteesWorkIsSaved = newValue;
                    return СommitteesWorkIsSaved;
                case "SavePlenarySessionCalendarPlanBtn":
                    if (setValue) PlenarySessionIsSaved = newValue;
                    return PlenarySessionIsSaved;
                case "SaveDeputyBtn":
                    if (setValue) DeputyIsSaved = newValue;
                    return DeputyIsSaved;
                case "SaveDepVotingBtn":
                    if (setValue) DepVotingIsSaved = newValue;
                    return DepVotingIsSaved;
                case "SaveDepQueriesBtn":
                    if (setValue) DepQueriesIsSaved = newValue;
                    return DepQueriesIsSaved;
                case "SaveDepSpeechesBtn":
                    if (setValue) DepSpeechesIsSaved = newValue;
                    return DepSpeechesIsSaved;
                case "SaveDepLawActivityBtn":
                    if (setValue) DepLawActivityIsSaved = newValue;
                    return DepLawActivityIsSaved;
                case "SaveFractionsBtn":
                    if (setValue) FractionsIsSaved = newValue;
                    return FractionsIsSaved;
                default:
                    return false;
            }
        }

        /// <summary>
        /// метод изменяет значения DateTimePicker-ов DepVotingDateTimePickStart, DepVotingDateTimePickEnd в зависимости от номера выбранного созыва
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var datesList = DictionaryInitializer.ConvocationDates.First(c => c.Key.Equals(ConvocationComboBox.Text)).Value;

            _progressReporter?.ReportProgress(() =>
            {
                DepVotingDateTimePickStart.Value = datesList.First();
                DepVotingDateTimePickStart.Refresh();
                DepVotingDateTimePickEnd.Value = datesList.Last();
                DepVotingDateTimePickEnd.Refresh();
            });
        }

        private void PhotoDownloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsPhotoDownload = PhotoDownloadCheckBox.Checked;
        }

        /// <summary>
        /// метод определяет номер созыва, использующийся в url Верховной Рады согласно выбранного в ConvocationComboBox созыва
        /// </summary>
        /// <returns>возвращает номер страницы созыва, использующийся в url страниц Верховной Рады</returns>
        private int SpotConvocationLink()
        {
            _urls = _ini.AutoReadIni();
            var text = ConvocationComboBox.Text.Split(' ').First();
            return RomanArabicNumerals.ToArabic(text) + 1;
        }
    }
}
