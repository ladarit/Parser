using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using GovernmentParse.Controls;
using GovernmentParse.DataProviders;

namespace GovernmentParse
{
    public partial class ModernForm : Form
    {
        private UrlsCollection _urls;

        private readonly ResourceReader _resourceReader;

        private readonly ProgressReporter _progressReporter;

        private CancellationTokenSource _cancellationTokenSource;

        //private ErrorModel _error;

        private bool NewReportIndicator { get; set; }

        private bool LimitWarning { get; set; }

        #region управление окном
        // ReSharper disable once InconsistentNaming
        public const int WM_NCLBUTTONDOWN = 0xA1;

        // ReSharper disable once InconsistentNaming
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Rectangle _fullWindowRectangle;

        private Point _previousPosition;

        private readonly Size _windowStartSize;

        private readonly Control.ControlCollection _workAreaPanels;

        private double _widthCoef;

        private double _heightCoef;

        bool _isEnlarge;

        private string _selectedTabName;
        #endregion

        #region индикаторы состояния сохранения данных
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
        #endregion

        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ModernForm()
        {
            InitializeComponent();
            Text = string.Empty;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            SidePanelSlider.Height = LawsTabButton.Height;
            SidePanelSlider.Top = LawsTabButton.Top;
            LawsControl.BringToFront();
            _fullWindowRectangle = Screen.PrimaryScreen.WorkingArea;
            _windowStartSize = Size;
            _workAreaPanels = WorkAreaPanel.Controls;

            _resourceReader = new ResourceReader();
            _progressReporter = new ProgressReporter();
            CancelBtn.Enabled = false;
            LimitWarning = false;
            _selectedTabName = string.Empty;
            CancelBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            //устанавливаем шрифт лейблов
            HeaderText.ForeColor = Color.White;
            HeaderText.Font = FontProvider.SetRobotoFont(13.0F, FontStyle.Regular);

            //подписываем методы на события контролов
            LawsControl.SaveLawsEvent += UpdateFiles;
            LawsControl.ChangeLimitWarningEvent += ChangeLimitWarning;
            CommitteesControl.SaveCommitteesEvent += UpdateFiles;
            PlanarySessionsControl.SaveSessionsEvent += UpdateFiles;
            DeputiesControl.SaveDeputiesEvent += UpdateFiles;
            FractionsControl.SaveFractionsEvent += UpdateFiles;
            DeputiesControl.PhotoDownloadEvent += ChangePhotoDownloadSign;

			//отключаем вкладки депутатов, фракций, коммитетов
	        DeputiesTabButton.Enabled = false;
	        FractionsTabButton.Enabled = false;
	        CommitteesTabButton.Enabled = false;

        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// метод обрабатывает нажатие на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="urls"></param>
        public async void UpdateFiles(object sender, UrlsCollection urls)
        {
            try
            {
                WorkStart();
                _urls = urls;
                var controlName = ((Button)sender).Name;
                if (!controlName.ContainsAny("SaveLawsByDatePickerBtn", "SaveByLowNameBtn"))
                    if (!CheckForRetention(controlName)) return;

                _log.Info("\n\n\nSTART Work");

                ParseResult<FileModel> parseResult;
                
                //поиск новых файлов
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
                else if (controlName.Equals("SaveAllLawsFromCurrentConvocationBtn"))
                {
                    await UpdateAllLawsFromCurrentConvocation(controlName);
                    return;
                }
                else
                    parseResult = await SearchFiles(controlName);

                if (parseResult.Error != null || !parseResult.XmlDocuments.Any() && !parseResult.XmlDocuments.Any()) return;

                //сравнение
                var compareWithSavedResult = await CompareFilesWithSaved(parseResult, _urls);
                if (!compareWithSavedResult.Files.Any() || compareWithSavedResult.Error != null)
                {
                    SetProgressBarsToMax();
                    MessageBox.Show(_resourceReader.GetString(!compareWithSavedResult.Files.Any() ? "NothingToSave" : compareWithSavedResult.Error.ErrorMsg));
                    NewReportIndicator = false;
                    return;
                }

                //запись
                var saveResult = await SaveFiles(compareWithSavedResult, _urls);
                if (saveResult.Error != null) return;

                _log.Info("COMPLETE Work, Start Create Report");

                await CreateReport(saveResult, controlName);

                _log.Info("COMPLETE create report");
            }
            catch (Exception ex)
            {
                //_error = new ErrorModel
                //{
                //    ErrorMsg = ex.Message,
                //    ControlName = ((Button) sender).Name,
                //    Operation = "UpdateFilesBtnFromModernForm"
                //};
                _log.Error($"CreateAndFillRootElement.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
            }
            finally
            {
                ////запись ошибки в БД
                //if (!string.IsNullOrEmpty(_error?.ErrorMsg))
                //    await SaveErrorMsgToDataBase(_error);
                WorkComplete();
            }
        }

        #region метод отправки в базу отчета об ошибке
        //private async Task SaveErrorMsgToDataBase(ErrorModel error)
        //{
        //    var errorSaver = new ErrorSaver();
        //    var saveErrResult = Task.Run(() => errorSaver.SendErrorMsgToDataBase(error));

        //    await _progressReporter.RegisterContinuation(saveErrResult, () =>
        //    {
        //        if (saveErrResult.Result.Error != null)
        //            MessageBox.Show(_resourceReader.GetString("CantSaveError"));
        //    });
        //}
        #endregion

        #region методы для обновления данных путем сравнения с существующими в базе (без скачивания xml)
        private async Task<ParseResult<FileModel>> SearchFiles(string controlName, TableInfo table = null)
        {
            _progressReporter.ReportProgress(() => { CancelBtn.Enabled = true; });

            SearchProgressBar.Value = 0;

            _cancellationTokenSource = new CancellationTokenSource();
            var parseResult = Task.Run(() =>
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                return new FilesProvider().GetFilesFromSite(_urls, LimitWarning, controlName, _cancellationTokenSource, _progressReporter, SearchProgressBar, IsPhotoDownload, table);
            }, _cancellationTokenSource.Token);

            await _progressReporter.RegisterContinuation(parseResult, () =>
            {
                if (parseResult.IsCanceled)
                    MessageBox.Show(_resourceReader.GetString("SearchStopped"));

                if (parseResult.Result.Error != null)
                {
                    //_error = parseResult.Result.Error;
                    MessageBox.Show(parseResult.Result.Error.ErrorMsg);
                    return;
                }

                if (!parseResult.Result.XmlDocuments.Any() && !parseResult.Result.XmlDocuments.Any())
                {
                    SetProgressBarsToMax();
                    MessageBox.Show(_resourceReader.GetString("ZeroCountWarning"));
                }
            });

            _progressReporter.ReportProgress(() => { SearchProgressBar.Value = SearchProgressBar.Maximum; });
            _progressReporter.ReportProgress(() => { CancelBtn.Enabled = false; });

            return parseResult.Result;
        }

        private async Task UpdateAllLawsFromCurrentConvocation(string controlName, TableInfo table = null)
        {
            _progressReporter.ReportProgress(() => { CancelBtn.Enabled = true; });

            SearchProgressBar.Value = 0;

            _cancellationTokenSource = new CancellationTokenSource();

            var result = await Task.Run(() =>
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                return new FilesProvider().GetFilesFromSite(_urls, LimitWarning, controlName, _cancellationTokenSource, _progressReporter, SearchProgressBar, IsPhotoDownload, table);
            }, _cancellationTokenSource.Token);

            MessageBox.Show(_resourceReader.GetString("SuccessSave"));
        }

        private async Task<FilesToSave> CompareFilesWithSaved(ParseResult<FileModel> files, UrlsCollection urls)
        {
            var fileCompare = new FilesCompare();
            var compareResult = Task.Run(() => fileCompare.GetFilesNeedToSaveFromDataBase(files, urls));

            await _progressReporter.RegisterContinuation(compareResult, () =>
            {
                _progressReporter.ReportProgress(() => { CompareProgressBar.Value = CompareProgressBar.Maximum; });

                if (compareResult.Result.Error != null)
                {
                    //_error = new BaseModel
                    //{
                    //    Error = compareResult.Result.Error,
                    //    Operation = compareResult.Result.Operation
                    //};
                    MessageBox.Show(_resourceReader.GetString("CompareError"));
                }
            });
            return compareResult.Result;
        }

        private async Task<SavedFiles> SaveFiles(FilesToSave files, UrlsCollection urls)
        {
            var filesUpload = new FilesUpload();
            var saveResult = Task.Run(() => filesUpload.UploadFilesToDataBase(files, urls, _progressReporter, SaveProgressBar));

            await saveResult.ContinueWith(_ =>
            {
                if (saveResult.Result.Error != null)
                {
                    //_error = saveResult.Result.Error;
                    MessageBox.Show(_resourceReader.GetString("SaveError"));
                }
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
                    _progressReporter.ReportProgress(() => NewReportIndicator = ReportControl.ChangeTextBoxValue(report.Result));
                    MessageBox.Show(_resourceReader.GetString("SuccessSave"));
                    GetofSetSavedState(controlName, true, true);
                }
            });
        }
        #endregion

        #region методы для обновления и загрузки новых законопроектов путем скачивания и сравнения xml с API Верховной Рады
        private async Task<DownloadResult<XmlDocument>> DownloadXmlFiles(string controlName)
        {
            var downloadResult = Task.Run(() => new XmlDownloader().GetXmlDocuments(_urls, controlName));
            await _progressReporter.RegisterContinuation(downloadResult, () =>
                {
                    if (downloadResult.Result.Error != null)
                    {
                        //_error = downloadResult.Result.Error;
                        MessageBox.Show(_resourceReader.GetString("DownloadForCompareError"));
                    }
                }
            );
            return downloadResult.Result;
        }

        private async Task<CompareXmlFilesResult> CompareXmlFiles(List<XmlDocument> files)
        {
            var compareResult = Task.Run(() => new XmlComparer().CompareFiles(files));
            await _progressReporter.RegisterContinuation(compareResult, () =>
                {
                    if (compareResult.Result.Error != null)
                    {
                        //_error = compareResult.Result.Error;
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
                    if (createResult.Result.Error != null)
                    {
                        //_error = createResult.Result.Error;
                        MessageBox.Show(_resourceReader.GetString("CreateLawsTableError"));
                    }
                }
            );
            return createResult.Result;
        }
        #endregion

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
            ChangeButtonsStatus(false);
            ReportTabButton.BackColor = Color.FromArgb(41, 39, 35);
            ReportTabButton.Image = Properties.Resources.Save_48px;
            NewReportIndicator = ReportControl.ChangeTextBoxValue(string.Empty);
        }

        /// <summary>
        /// метод изменяет досутпность кнопок после окончания работы
        /// </summary>
        private void WorkComplete()
        {
            //меняем стиль кнопки отчета, если есть отчет
            if (NewReportIndicator && !_selectedTabName.Equals("ReportTabButton"))
            {
                ReportTabButton.BackColor = Color.DarkGreen;
                ReportTabButton.Image = Properties.Resources.High_Priority_48px;
            }

            //обнуляем прогрессбары
            _progressReporter.ReportProgress(() =>
            {
                SearchProgressBar.Value = 0;
                CompareProgressBar.Value = 0;
                SaveProgressBar.Value = 0;
            });

            //_error = null;
            ChangeButtonsStatus(true);
            LimitWarning = false;
        }

        private void SetProgressBarsToMax()
        {
            _progressReporter.ReportProgress(() =>
            {
                if(SearchProgressBar.Value != SearchProgressBar.Maximum)
                    SearchProgressBar.Value = SearchProgressBar.Maximum;

                if (CompareProgressBar.Value != CompareProgressBar.Maximum)
                    CompareProgressBar.Value = CompareProgressBar.Maximum;

                if (SaveProgressBar.Value != SaveProgressBar.Maximum)
                    SaveProgressBar.Value = SaveProgressBar.Maximum;
            });
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
        /// метод меняет значение поля, отвечающего за выдачу предупреждения о количестве записей
        /// </summary>
        private void ChangeLimitWarning(bool state)
        {
            LimitWarning = state;
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
            LawsControl.ChangeButtonStyleAndState(value);
            CommitteesControl.ChangeButtonStyleAndState(value);
            PlanarySessionsControl.ChangeButtonStyleAndState(value);
            DeputiesControl.ChangeButtonStyleAndState(value);
            FractionsControl.ChangeButtonStyleAndState(value);
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

        private void ChangePhotoDownloadSign(object sender, bool state)
        {
            IsPhotoDownload = state;
        }

        #region методы вкладок левого сайдбара
        private void LawsTabButton_Click(object sender, EventArgs e)
        {
            BringToFrontWorkArea();
            HeaderText.Text = _resourceReader.GetString("LawsHeader");
            MoveSliderPanel((Control)sender);
            LawsControl.BringToFront();
            cursor.BringToFront();
        }

        private void CommitteesTabButton_Click(object sender, EventArgs e)
        {
            BringToFrontWorkArea();
            HeaderText.Text = _resourceReader.GetString("CommitteeHeader");
            MoveSliderPanel((Control)sender);
            CommitteesControl.BringToFront();
            cursor.BringToFront();
        }

        private void PlanarySessionsTabButton_Click(object sender, EventArgs e)
        {
            BringToFrontWorkArea();
            HeaderText.Text = _resourceReader.GetString("SessionsHeader");
            MoveSliderPanel((Control)sender);
            PlanarySessionsControl.BringToFront();
            cursor.BringToFront();
        }

        private void DeputiesTabButton_Click(object sender, EventArgs e)
        {
            BringToFrontWorkArea();
            HeaderText.Text = _resourceReader.GetString("DeputiesHeader");
            MoveSliderPanel((Control)sender);
            DeputiesControl.BringToFront();
            cursor.BringToFront();
        }

        private void FractionsTabButton_Click(object sender, EventArgs e)
        {
            BringToFrontWorkArea();
            HeaderText.Text = _resourceReader.GetString("FractionsHeader");
            MoveSliderPanel((Control)sender);
            FractionsControl.BringToFront();
            cursor.BringToFront();
        }

        private void ReportTabButton_Click(object sender, EventArgs e)
        {
            HeaderText.Text = _resourceReader.GetString("ReportHeader");
            MoveSliderPanel((Control)sender);
            BringToFrontReportArea();
            if (ReportTabButton.BackColor == Color.DarkGreen)
            {
                ReportTabButton.BackColor = Color.FromArgb(41, 39, 35);
                ReportTabButton.Image = Properties.Resources.Save_48px;
            }
            cursor.SendToBack();
        }

        private void BringToFrontWorkArea()
        {
            WorkAreaPanel.BringToFront();
            BottomPanel.BringToFront();
            ReportControl.SendToBack();
        }

        private void BringToFrontReportArea()
        {
            WorkAreaPanel.SendToBack();
            BottomPanel.SendToBack();
            ReportControl.BringToFront();
        }

        #endregion

        #region методы управления окном
        private void MinimizeWindowBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MaximizeWindowBtn_Click(object sender, EventArgs e)
        {
            Hide();

            if (Size != _fullWindowRectangle.Size)
            {
                _isEnlarge = true;
                _widthCoef = (double)_fullWindowRectangle.Width / Size.Width;
                _heightCoef = (double)_fullWindowRectangle.Height / Size.Height;
                _previousPosition = Location;
                Location = new Point(0, 0);
                Size = _fullWindowRectangle.Size;
            }
            else
            {
                _isEnlarge = false;
                Location = _previousPosition;
                Size = _windowStartSize;
            }

            foreach (Control control in _workAreaPanels)
                ((BaseWorkAreaControl)control).SetElementSize(_widthCoef, _heightCoef, _isEnlarge);

            if (!string.IsNullOrEmpty(_selectedTabName))
            {
                var activeTabBtn = Controls.Find(_selectedTabName, true).First();
                MoveSliderPanel(activeTabBtn);
            }
            cursor.Location = new Point(CompareProgressBar.Left + 243, cursor.Location.Y);

            Show();
        }

        private void MoveSliderPanel(Control button)
        {
            _selectedTabName = button.Name;
            SidePanelSlider.Height = button.Height;
            SidePanelSlider.Top = button.Top;
        }

        private void CloseWindowBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}
