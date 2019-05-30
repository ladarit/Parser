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
    public partial class CommitteesControl : BaseWorkAreaControl
    {
        public delegate void SaveBtnPress(object sender, UrlsCollection urls);

        public event SaveBtnPress SaveCommitteesEvent;

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        public CommitteesControl()
        {
            InitializeComponent(); 
            _ini = new IniReader();
	        ConvocationCommitteeComboBox.DataSource = new List<string>();// FillConvocationList(7);
            //устанавливаем шрифт для лейблов
            ConvocationCommitteeLabel.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            SaveCommiteesLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            //устанавливаем шрифт для комбобокса
            ConvocationCommitteeComboBox.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            //устанавливаем шрифт для кнопок
            SaveСommitteesBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
        }

        /// <summary>
        /// метод запускает парсинг комитетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveСommitteesBtn_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            var pageNumber = SpotConvocationLink();
            _urls.CommitteesPage += pageNumber;
            SaveCommitteesEvent?.Invoke(sender, _urls);
        }

        ///// <summary>
        ///// метод запускает парсинг еженедельного плана работы комитетов
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SaveСommitteesWorkBtn_Click(object sender, EventArgs e)
        //{
        //    SaveCommitteesEvent?.Invoke(sender, _urls);
        //}

        /// <summary>
        /// метод определяет номер созыва, использующийся в url Верховной Рады согласно выбранного в ConvocationComboBox созыва
        /// </summary>
        /// <returns>возвращает номер страницы созыва, использующийся в url страниц Верховной Рады</returns>
        private int SpotConvocationLink()
        {
            _urls = _ini.AutoReadIni();
            var text = ConvocationCommitteeComboBox.Text.Split(' ').First();
            return RomanArabicNumerals.ToArabic(text) + 1;
        }

        public void ChangeButtonStyleAndState(bool state)
        {
            ChangeButtonStyleAndState(this, state);
        }

        public void MoveSlider(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if(panel.Name.Contains("EmptyPanel"))
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
            SaveCommiteesLabel.Width = Calculate(SaveCommiteesLabel.Width, widthCoef);
            SaveCommiteesLabel.Height = Calculate(SaveCommiteesLabel.Height, heightCoef);
            SaveCommiteesLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);

            ConvocationCommitteeLabel.Width = Calculate(ConvocationCommitteeLabel.Width, widthCoef);
            ConvocationCommitteeLabel.Height = Calculate(ConvocationCommitteeLabel.Height, heightCoef);
            ConvocationCommitteeLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);
            ConvocationCommitteeLabel.Location = new Point(ConvocationCommitteeLabel.Location.X,
                CalcVerticalDistanceBetweenControls(ConvocationCommitteeComboBox, ConvocationCommitteeLabel, 10));

            //устанавливаем шрифт и размер  для комбобокса
            ConvocationCommitteeComboBox.Width = Calculate(ConvocationCommitteeComboBox.Width, widthCoef);
            ConvocationCommitteeComboBox.Height = Calculate(ConvocationCommitteeComboBox.Height, heightCoef);
            ConvocationCommitteeComboBox.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);

            //устанавливаем шрифт для кнопок
            SaveСommitteesBtn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);

            //устанавливаем размер и положение для пиктограммы
            SaveCommitteesPictBox.Width = Calculate(SaveCommitteesPictBox.Width, widthCoef);
            SaveCommitteesPictBox.Height = Calculate(SaveCommitteesPictBox.Height, heightCoef);
            var newImgLocation = SaveCommitteesPanel.Width - SaveCommitteesPictBox.Width;
            SaveCommitteesPictBox.Location = new Point(newImgLocation, SaveCommitteesPictBox.Location.Y);
        }
    }
}
