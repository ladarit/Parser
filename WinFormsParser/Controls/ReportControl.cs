using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GovernmentParse.Helpers;

namespace GovernmentParse.Controls
{
    public partial class ReportControl : BaseWorkAreaControl
    {
        public ReportControl()
        {
            InitializeComponent();
            var resourceReader = new ResourceReader();
            TextBoxInformant.ReadOnly = true;
            TextBoxInformant.BackColor = SystemColors.Window;
            TextBoxInformant.Text = resourceReader.GetString("StartNotification");
            TextBoxInformant.Font = FontProvider.SetRobotoFont(10.0F, FontStyle.Regular);
            this.SaveReportBtn.Font = FontProvider.SetRobotoFont(12.0F, FontStyle.Regular);
            this.SaveReportBtn.BackColor = Color.FromArgb(51, 71, 79);
            this.SaveReportBtn.ForeColor = Color.FromArgb(255, 255, 255);
        }

        public bool ChangeTextBoxValue(string text)
        {
            TextBoxInformant.Text = text;
            TextBoxInformant.Refresh();
            return !string.IsNullOrEmpty(TextBoxInformant.Text);
        }

        /// <summary>
        /// метод позволяет сохранить отчет о парсинге
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveReportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog { FileName = "report.txt" };
            if (savefile.ShowDialog() == DialogResult.OK)
                File.WriteAllText(savefile.FileName, TextBoxInformant.Text);
        }
    }
}
