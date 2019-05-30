using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse.Controls
{
    public partial class DeputiesControl : BaseWorkAreaControl
    {
        public delegate void SaveBtnPress(object sender, UrlsCollection urls);

        public event SaveBtnPress SaveDeputiesEvent;

        public delegate void PhotoDownload(object sender, bool state);

        public event PhotoDownload PhotoDownloadEvent;

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        public DeputiesControl()
        {
            InitializeComponent();
            _ini = new IniReader();
	        ConvocationDeputiesComboBox.DataSource = new List<string>(); //FillConvocationList(5);
            //задаем шрифт для лейблов
            SaveDeputiesLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            ConvocationDeputiesLabel.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            PhotoDownloadCheckBox.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            //устанавливаем шрифт для комбобокса
            ConvocationDeputiesComboBox.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            //задаем шрифт для кнопок
            SaveDeputyBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
        }

        /// <summary>
        /// метод запускает парсинг депутатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDeputyBtn_Click(object sender, EventArgs e)
        {
            SpotConvocationLinkForDeputyParse();
            SaveDeputiesEvent?.Invoke(sender, _urls);
        }

        ///// <summary>
        ///// метод запускает парсинг депутатских голосований
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SaveDepVotingBtn_Click(object sender, EventArgs e)
        //{
        //    SpotConvocationLinkForDeputyParse();
        //    _urls.DeputyVotePage = SpotDefaultDateTimePeriod(_urls.DeputyVotePage);
        //    _urls.DeputyVotePage = ChangeLinkIfArchive(_urls.DeputyVotePage);
        //    UpdateFiles(sender);
        //}

        ///// <summary>
        ///// метод запускает парсинг депутатских выступлений
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SaveDepSpeechesBtn_Click(object sender, EventArgs e)
        //{
        //    SpotConvocationLinkForDeputyParse();
        //    _urls.DeputySpeechPage = SpotDefaultDateTimePeriod(_urls.DeputySpeechPage);
        //    _urls.DeputySpeechPage = ChangeLinkIfArchive(_urls.DeputySpeechPage);
        //    UpdateFiles(sender);
        //}

        ///// <summary>
        ///// метод запускает парсинг депутатских запросов
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SaveDepQueriesBtn_Click(object sender, EventArgs e)
        //{
        //    SpotConvocationLinkForDeputyParse();
        //    _urls.DeputyQueriesPage = ChangeConvocationInLink(_urls.DeputyQueriesPage);
        //    UpdateFiles(sender);
        //}

        ///// <summary>
        ///// метод запускает парсинг депутатской законотвроч. активности
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SaveDepLawActivityBtn_Click(object sender, EventArgs e)
        //{
        //    SpotConvocationLinkForDeputyParse();
        //    _urls.DeputyLawActivityPage = ChangeConvocationInLink(_urls.DeputyLawActivityPage);
        //    UpdateFiles(sender);
        //}

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

        ///// <summary>
        ///// метод изменяет значения DateTimePicker-ов DepVotingDateTimePickStart, DepVotingDateTimePickEnd в зависимости от номера выбранного созыва
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ConvocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var datesList = DictionaryInitializer.ConvocationDates.First(c => c.Key.Equals(ConvocationComboBox.Text)).Value;

        //    _progressReporter?.ReportProgress(() =>
        //    {
        //        DepVotingDateTimePickStart.Value = datesList.First();
        //        DepVotingDateTimePickStart.Refresh();
        //        DepVotingDateTimePickEnd.Value = datesList.Last();
        //        DepVotingDateTimePickEnd.Refresh();
        //    });
        //}

        ///// <summary>
        ///// метод определяет номер созыва, использующийся в url Верховной Рады согласно выбранного в ConvocationComboBox созыва
        ///// </summary>
        ///// <returns>возвращает номер страницы созыва, использующийся в url страниц Верховной Рады</returns>
        //private int SpotConvocationLink()
        //{
        //    _urls = _ini.AutoReadIni();
        //    var text = ConvocationComboBox.Text.Split(' ').First();
        //    return RomanArabicNumerals.ToArabic(text) + 1;
        //}

        ///// <summary>
        ///// метод меняет в ссылке даты на указанные в DepVotingDateTimePickStart и DepVotingDateTimePickEnd
        ///// </summary>
        ///// <param name="url">url для корректировки</param>
        //private string SpotDefaultDateTimePeriod(string url)
        //{
        //    url = url.Replace("StartDateToReplace", DepVotingDateTimePickStart.Value.ToShortDateString());
        //    return url.Replace("EndDateToReplace", DepVotingDateTimePickEnd.Value.ToShortDateString());
        //}

        ///// <summary>
        ///// устанавливает в переданный url номер страницы созыва, использующийся в url страниц Верховной Рады
        ///// </summary>
        ///// <param name="url">url для корректировки</param>
        //private string ChangeConvocationInLink(string url)
        //{
        //    var pageNumber = SpotConvocationLinkForDeputyParse();
        //    return url.Replace("ConvocationToReplace", pageNumber.ToString());
        //}

        ///// <summary>
        ///// метод добавляет в ссылку указание на принадлежность архив даты на указанные в DepVotingDateTimePickStart и DepVotingDateTimePickEnd
        ///// </summary>
        ///// <param name="url">url для корректировки</param>
        //private string ChangeLinkIfArchive(string url)
        //{
        //    return !ConvocationDeputiesComboBox.Text.Equals(ConvocationList.First()) ? url.Insert(url.IndexOf("dep_", StringComparison.Ordinal), "arh_") : url;
        //}

        /// <summary>
        /// метод определяет номер созыва, использующийся в url Верховной Рады согласно выбранного в ConvocationComboBox созыва
        /// </summary>
        /// <returns>возвращает номер страницы созыва, использующийся в url страниц Верховной Рады</returns>
        private int SpotConvocationLink()
        {
            _urls = _ini.AutoReadIni();
            var text = ConvocationDeputiesComboBox.Text.Split(' ').First();
            return RomanArabicNumerals.ToArabic(text) + 1;
        }

        public void ChangeButtonStyleAndState(bool state)
        {
            ChangeButtonStyleAndState(this, state);
        }

        private void PhotoDownloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PhotoDownloadEvent?.Invoke(sender, PhotoDownloadCheckBox.Checked);
        }

        public void MoveSlider(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if (panel.Name.Contains("EmptyPanel"))
                return;
            Slider.Parent = panel.Parent;
            Slider.Top = panel.Top;
            Slider.Height = panel.Height;
            //сюда надо подставлять панели, расположенные справа
            //Slider.Left = panel.Name.ContainsAny("SaveLawsByDatePickerPanel", "UpdateLawsPanel") ?
            //    panel.Right : panel.Left - 10;
        }

        public override void SetElementSize(double widthCoef, double heightCoef, bool isEnlarge = false)
        {
            base.SetElementSize(widthCoef, heightCoef, isEnlarge);

            //устанавливаем шрифт и размер для лейблов
            SaveDeputiesLabel.Width = Calculate(SaveDeputiesLabel.Width, widthCoef);
            SaveDeputiesLabel.Height = Calculate(SaveDeputiesLabel.Height, heightCoef);
            SaveDeputiesLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);


            ConvocationDeputiesLabel.Width = Calculate(ConvocationDeputiesLabel.Width, widthCoef);
            ConvocationDeputiesLabel.Height = Calculate(ConvocationDeputiesLabel.Height, heightCoef);
            ConvocationDeputiesLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);
            ConvocationDeputiesLabel.Location = new Point(ConvocationDeputiesLabel.Location.X,
                CalcVerticalDistanceBetweenControls(ConvocationDeputiesComboBox, ConvocationDeputiesLabel, 10));

            //устанавливаем шрифт и размер  для чекбокса
            PhotoDownloadCheckBox.Width = Calculate(PhotoDownloadCheckBox.Width, widthCoef);
            PhotoDownloadCheckBox.Height = Calculate(PhotoDownloadCheckBox.Height, heightCoef);
            PhotoDownloadCheckBox.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);

            //устанавливаем шрифт и размер  для комбобокса
            ConvocationDeputiesComboBox.Width = Calculate(ConvocationDeputiesComboBox.Width, widthCoef);
            ConvocationDeputiesComboBox.Height = Calculate(ConvocationDeputiesComboBox.Height, heightCoef);
            ConvocationDeputiesComboBox.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);

            //устанавливаем шрифт для кнопок
            SaveDeputyBtn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);

            //устанавливаем размер и положение для пиктограммы
            SaveDeputiesPicBox.Width = Calculate(SaveDeputiesPicBox.Width, widthCoef);
            SaveDeputiesPicBox.Height = Calculate(SaveDeputiesPicBox.Height, heightCoef);
            var newImgLocation = SaveDeputiesPanel.Width - SaveDeputiesPicBox.Width;
            SaveDeputiesPicBox.Location = new Point(newImgLocation, SaveDeputiesPicBox.Location.Y);
        }

        private void SaveDeputiesPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
