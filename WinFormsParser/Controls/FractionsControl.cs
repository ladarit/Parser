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
    public partial class FractionsControl : BaseWorkAreaControl
    {
        public delegate void SaveBtnPress(object sender, UrlsCollection urls);

        public event SaveBtnPress SaveFractionsEvent;

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        public FractionsControl()
        {
            InitializeComponent();
            _ini = new IniReader();
	        ConvocationFractionsComboBox.DataSource = new List<string>();//FillConvocationList(7);
            //задаем шрифт для лейблов
            SaveFractionsLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            ConvocationFractionsLabel.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            //устанавливаем шрифт для комбобокса
            ConvocationFractionsComboBox.Font = FontProvider.SetRobotoFont(11.0F, FontStyle.Regular);
            //задаем шрифт для кнопок
            SaveFractionsBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
        }

        /// <summary>
        /// метод запускает парсинг фракций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFractionsBtn_Click(object sender, EventArgs e)
        {
            var pageNumber = SpotConvocationLink();
            _urls.FractionsMainPage += pageNumber;
            SaveFractionsEvent?.Invoke(sender, _urls);
        }

        private int SpotConvocationLink()
        {
            _urls = _ini.AutoReadIni();
            var text = ConvocationFractionsComboBox.Text.Split(' ').First();
            return RomanArabicNumerals.ToArabic(text) + 1;
        }

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
            //сюда надо подставлять панели, расположенные справа
            //Slider.Left = panel.Name.ContainsAny("SaveLawsByDatePickerPanel", "UpdateLawsPanel") ?
            //    panel.Right : panel.Left - 10;
        }

        public override void SetElementSize(double widthCoef, double heightCoef, bool isEnlarge = false)
        {
            base.SetElementSize(widthCoef, heightCoef, isEnlarge);

            //устанавливаем шрифт и размер для лейблов
            SaveFractionsLabel.Width = Calculate(SaveFractionsLabel.Width, widthCoef);
            SaveFractionsLabel.Height = Calculate(SaveFractionsLabel.Height, heightCoef);
            SaveFractionsLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);

            ConvocationFractionsLabel.Width = Calculate(ConvocationFractionsLabel.Width, widthCoef);
            ConvocationFractionsLabel.Height = Calculate(ConvocationFractionsLabel.Height, heightCoef);
            ConvocationFractionsLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);
            ConvocationFractionsLabel.Location = new Point(ConvocationFractionsLabel.Location.X,
                                                           CalcVerticalDistanceBetweenControls(ConvocationFractionsComboBox, ConvocationFractionsLabel, 10));

            //устанавливаем шрифт для комбобокса
            ConvocationFractionsComboBox.Width = Calculate(ConvocationFractionsComboBox.Width, widthCoef);
            ConvocationFractionsComboBox.Height = Calculate(ConvocationFractionsComboBox.Height, heightCoef);
            ConvocationFractionsComboBox.Font = FontProvider.SetRobotoFont(isEnlarge ? 18.0F : 11.0F, FontStyle.Regular);

            //задаем шрифт для кнопок
            SaveFractionsBtn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);

            SaveFractionsPictBox.Width = Calculate(SaveFractionsPictBox.Width, widthCoef);
            SaveFractionsPictBox.Height = Calculate(SaveFractionsPictBox.Height, heightCoef);

            var newImgLocation = SaveFractionsPanel.Width - SaveFractionsPictBox.Width;
            SaveFractionsPictBox.Location = new Point(newImgLocation, SaveFractionsPictBox.Location.Y);
        }
    }
}
