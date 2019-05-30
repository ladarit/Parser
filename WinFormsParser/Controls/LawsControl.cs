using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;
using System.Windows.Forms;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;
using MetroFramework.Controls;

namespace GovernmentParse.Controls
{
    public partial class LawsControl : BaseWorkAreaControl
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void SaveBtnPress(object sender, UrlsCollection urls);

        public event SaveBtnPress SaveLawsEvent;

        public delegate void LimitWarningDel(bool state);

        public event LimitWarningDel ChangeLimitWarningEvent;

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        public delegate void OnFocus();

        public delegate void OutFocus();

        private const string Password = "fcrjl";

        public LawsControl()
        {
            InitializeComponent();
            _ini = new IniReader();
            CultureInfo ci = new CultureInfo("uk-UA");
            SaveLastWeekLawsLabel.Text = SaveLastWeekLawsLabel.Text.Insert(SaveLastWeekLawsLabel.Text.Length, DateTime.Today.AddDays(-7).ToString(ci.DateTimeFormat.ShortDatePattern) + " - " + DateTime.Today.ToString(ci.DateTimeFormat.ShortDatePattern));
            //задаем шрифт для лейблов
            SaveLastWeekLawsLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            SaveLawsByNamesLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            UpdateLawsByDayLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            SaveLawsByDatesLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            StartDatePickerLabel.Font = FontProvider.SetRobotoFont(10.0F, FontStyle.Regular);
            EndDatePickerLabel.Font = FontProvider.SetRobotoFont(10.0F, FontStyle.Regular);
            SearchLowTextBox.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            SaveAllLawsFromCurrentConvocationBtn.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            SaveAllLawsFromCurrentConvocationBtn.Visible = false;
            //задаем шрифт для кнопок
            UpdateLaws.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
            SaveByLowNameBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
            SaveLawsBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
            SaveLawsByDatePickerBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
            //задаем шрифт для дейтпикеров
            startDate.Font = FontProvider.SetRobotoFont(10.0F, FontStyle.Regular);
            endDate.Font = FontProvider.SetRobotoFont(10.0F, FontStyle.Regular);

            SearchLowTextBox.GotFocus += (sender, args) => { panel2.BackColor = Color.FromArgb(15, 20, 28); };
            SearchLowTextBox.LostFocus += (sender, args) => { panel2.BackColor = Color.FromArgb(80, 90, 104); };
            SaveAllLawsCheckBox.Enabled = true;
        }

        #region законы
        /// <summary>
        /// метод запускает парсинг законопроектов, принятых за текущую неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveLawsBtn_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            SaveLawsEvent?.Invoke(sender, _urls);
        }
        
        /// <summary>
        /// метод запускает парсинг законопроектов за выбранный период
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLawsByDatePickerBtn_Click(object sender, EventArgs e)
        {
            ChangeLimitWarningEvent?.Invoke(true);
            if (startDate.Value.Date > endDate.Value.Date)
            {
                MessageBox.Show(ResourceReader.GetString("DatePickerWarning"));
                return;
            }

            _log.Info($"START SaveByDateTimePickerPeriod start: {startDate.Value.Date} end: {endDate.Value.Date}");
            CultureInfo ci = new CultureInfo("uk-UA");
            _urls = _ini.AutoReadIni();
            string[] url = _urls.LawsByDatePage.Split('&');
            url[0] += startDate.Value.ToString(ci.DateTimeFormat.ShortDatePattern) + "&";
            url[1] += endDate.Value.ToString(ci.DateTimeFormat.ShortDatePattern) + "&";
            _urls.LawsByDatePage = string.Empty;
            foreach (var str in url)
                _urls.LawsByDatePage += str;
            SaveLawsEvent?.Invoke(sender, _urls);
        }

        private void SaveAllLawsFromCurrentConvocationBtn_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            _log.Info("START SaveAllLawsFromCurrentConvocation");
            _urls.LawsByDatePage = _urls.LawsByLastConvocation;
            SaveLawsEvent?.Invoke(sender, _urls);
        }

        private void SaveByLowNameBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchLowTextBox.Text))
            {
                MessageBox.Show(ResourceReader.GetString("InvalidSearchLowTextBoxValue"));
                return;
            }
            var lowNums = InputValidator.Сorrect(SearchLowTextBox.Text);
            lowNums = HttpUtility.UrlEncode(lowNums, Encoding.GetEncoding("windows-1251"));
            _urls = _ini.AutoReadIni();
            if (!string.IsNullOrEmpty(lowNums) && !lowNums.Contains("%2c"))
                _urls.LawByNamePage = _urls.LawByNamePage.Replace("num_s=3", "num_s=1") + lowNums;
            else
                _urls.LawByNamePage += lowNums + "&zp_cnt=-1";

            SaveLawsEvent?.Invoke(sender, _urls);
        }

        private void UpdateLaws_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            SaveLawsEvent?.Invoke(sender, _urls);
        }
        #endregion

        public void ChangeButtonStyleAndState(bool state)
        {
            ChangeButtonStyleAndState(this, state);
        }

        public void MoveSlider(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if (panel.Name.Contains("EmptyPanel"))
                return;
            Slider.Parent = panel.Parent;
            Slider.Top = panel.Top;
            Slider.Height = panel.Height;
            Slider.Left = panel.Name.ContainsAny("SaveLawsByDatePickerPanel", "UpdateLawsPanel") ?
                panel.Right : panel.Left - 10;
        }

        public override void SetElementSize(double widthCoef, double heightCoef, bool isEnlarge = false)
        {
            base.SetElementSize(widthCoef, heightCoef, isEnlarge);

            //устанавливаем шрифт и размер для лейблов верхних левых углов панелей
            foreach (var label in new List<Label> { SaveLastWeekLawsLabel, SaveLawsByDatesLabel, SaveLawsByNamesLabel, UpdateLawsByDayLabel })
            {
                label.Width = Calculate(label.Width, widthCoef);
                label.Height = Calculate(label.Height, heightCoef);
                label.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);
            }

            //устанавливаем шрифт, размер и положение для лейблов пикеров дат
            foreach (var label in new List<Label> { StartDatePickerLabel, EndDatePickerLabel })
            {
                label.Width = Calculate(label.Width, widthCoef);
                label.Height = Calculate(label.Height, heightCoef);
                label.Font = FontProvider.SetRobotoFont(isEnlarge ? 17.0F : 10.0F, FontStyle.Regular);
                label.Location = new Point(label.Location.X,
                    CalcVerticalDistanceBetweenControls(label.Name.Equals("StartDatePickerLabel") ? startDate : endDate, label, 5));
            }

            var prevSearchLowTextBoxHeight = SearchLowTextBox.Height;
            SearchLowTextBox.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);
            panel2.Location = new Point(panel2.Location.X, SearchLowTextBox.Height - prevSearchLowTextBoxHeight + panel2.Location.Y);

            StartDatePanel.Location = new Point(StartDatePanel.Location.X,
                CalcVerticalDistanceBetweenControls(endDatePanel, StartDatePanel, isEnlarge ? 20 : 1));

            //устанавливаем шрифт и размер  для комбобоксов
            foreach (var datePicker in new List<MetroDateTime> { startDate, endDate })
            {
                datePicker.Width = Calculate(datePicker.Width, widthCoef);
                datePicker.Height = Calculate(datePicker.Height, heightCoef);
                datePicker.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);
            }

            //устанавливаем шрифт для кнопок
            foreach (var btn in new List<Button> { SaveLawsBtn, SaveByLowNameBtn, SaveLawsByDatePickerBtn, UpdateLaws })
                btn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);

            //устанавливаем размер и положение для пиктограмм
            foreach (var pictBox in new List<PictureBox> { SaveLawsPicBox, SaveLawsByDatePickerPicBox, SaveByLowNamePicBox, UpdateLawsPicBox })
            {
                pictBox.Width = Calculate(pictBox.Width, widthCoef);
                pictBox.Height = Calculate(pictBox.Height, heightCoef);
                pictBox.Location = new Point(pictBox.Parent.Width - pictBox.Width, pictBox.Location.Y);
            }
        }

        private void UpdateLawsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveAllLawsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SaveAllLawsCheckBox.Checked)
            {
                string promptValue = Prompt.ShowDialog("Введіть пароль", "Перевірка доступу");
                if (promptValue != Password)
                {
                    SaveAllLawsCheckBox.Checked = false;
                    SaveAllLawsFromCurrentConvocationBtn.Visible = false;
                    SaveLawsByDatesLabel.Visible = true;
                }
                else
                {
                    SaveAllLawsFromCurrentConvocationBtn.Visible = true;
                    SaveLawsByDatesLabel.Visible = false;
                    var confirmResult = MessageBox.Show("Пропустити вже завантажені законопроекти?", "", MessageBoxButtons.YesNo);
                    LawsDonwloadOptions.LogBasedDownloadEnabled = confirmResult == DialogResult.Yes;
                }
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "Ок", Left = 200, Width = 50, Top = 75, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

    public static class LawsDonwloadOptions
    {
        internal static bool LogBasedDownloadEnabled = true;
    }
}
