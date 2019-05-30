using System;
using System.Drawing;
using System.Windows.Forms;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse.Controls
{
    public partial class PlanarySessionsControl : BaseWorkAreaControl
    {
        public delegate void SaveBtnPress(object sender, UrlsCollection urls);

        public event SaveBtnPress SaveSessionsEvent;

        private UrlsCollection _urls;

        private readonly IniReader _ini;

        public PlanarySessionsControl()
        {
            InitializeComponent();
            _ini = new IniReader();
            //задаем шрифт для лейблов
            SavePlenarySessionCalendarPlanLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            SavePlenarySessionDatesLabel.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            //задаем шрифт для кнопок
            SavePlenarySessionCalendarPlanBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
            SavePlenarySessionDatesBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Underline);
        }

        /// <summary>
        /// метод запускает парсинг 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlenarySessionCalendarPlanBtn_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            SaveSessionsEvent?.Invoke(sender, _urls);
        }

        /// <summary>
        /// метод запускает парсинг графика проведения пленарных заседаний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlenarySessionDatesBtn_Click(object sender, EventArgs e)
        {
            _urls = _ini.AutoReadIni();
            SaveSessionsEvent?.Invoke(sender, _urls);
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
            Slider.Left = panel.Name.ContainsAny("SaveSessionsDatesPanel") ?
                panel.Right : panel.Left - 10;
        }

        public override void SetElementSize(double widthCoef, double heightCoef, bool isEnlarge = false)
        {
            base.SetElementSize(widthCoef, heightCoef, isEnlarge);

            //устанавливаем шрифт и размер для лейблов
            SavePlenarySessionCalendarPlanLabel.Width = Calculate(SavePlenarySessionCalendarPlanLabel.Width, widthCoef);
            SavePlenarySessionCalendarPlanLabel.Height = Calculate(SavePlenarySessionCalendarPlanLabel.Height, heightCoef);
            SavePlenarySessionCalendarPlanLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);

            SavePlenarySessionDatesLabel.Width = Calculate(SavePlenarySessionDatesLabel.Width, widthCoef);
            SavePlenarySessionDatesLabel.Height = Calculate(SavePlenarySessionDatesLabel.Height, heightCoef);
            SavePlenarySessionDatesLabel.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Regular);


            //устанавливаем шрифт для кнопок
            SavePlenarySessionCalendarPlanBtn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);
            SavePlenarySessionDatesBtn.Font = FontProvider.SetRobotoFont(isEnlarge ? 19.0F : 12.0F, FontStyle.Underline);

            //устанавливаем размер и положение для пиктограмм
            SaveSessionsPlanPictBox.Width = Calculate(SaveSessionsPlanPictBox.Width, widthCoef);
            SaveSessionsPlanPictBox.Height = Calculate(SaveSessionsPlanPictBox.Height, heightCoef);
            var newImgLocation = SaveSessionsPlanPanel.Width - SaveSessionsPlanPictBox.Width;
            SaveSessionsPlanPictBox.Location = new Point(newImgLocation, SaveSessionsPlanPictBox.Location.Y);

            SaveSessionsDatesPictBox.Width = Calculate(SaveSessionsDatesPictBox.Width, widthCoef);
            SaveSessionsDatesPictBox.Height = Calculate(SaveSessionsDatesPictBox.Height, heightCoef);
            newImgLocation = SaveSessionsDatesPanel.Width - SaveSessionsDatesPictBox.Width;
            SaveSessionsDatesPictBox.Location = new Point(newImgLocation, SaveSessionsDatesPictBox.Location.Y);
        }

    }
}
