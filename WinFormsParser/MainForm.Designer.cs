namespace GovernmentParse
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SaveLawsBtn = new System.Windows.Forms.Button();
            this.lawsInformLabel = new System.Windows.Forms.Label();
            this.saveReportBtn = new System.Windows.Forms.Button();
            this.SaveLawsByDatePickerBtn = new System.Windows.Forms.Button();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.LawProjectsTab = new System.Windows.Forms.TabPage();
            this.UpdateLaws = new System.Windows.Forms.Button();
            this.SaveByLowNameBtn = new System.Windows.Forms.Button();
            this.SearchLowTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.СommitteesTab = new System.Windows.Forms.TabPage();
            this.SaveСommitteesWorkBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveСommitteesBtn = new System.Windows.Forms.Button();
            this.SessionsTab = new System.Windows.Forms.TabPage();
            this.SavePlenarySessionDatesBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SavePlenarySessionCalendarPlanBtn = new System.Windows.Forms.Button();
            this.DeputiesTab = new System.Windows.Forms.TabPage();
            this.PhotoDownloadCheckBox = new System.Windows.Forms.CheckBox();
            this.DepVotingDateTimePickEnd = new System.Windows.Forms.DateTimePicker();
            this.DepVotingDateTimePickStart = new System.Windows.Forms.DateTimePicker();
            this.ConvocationComboBox = new System.Windows.Forms.ComboBox();
            this.SaveDepLawActivityBtn = new System.Windows.Forms.Button();
            this.SaveDepSpeechesBtn = new System.Windows.Forms.Button();
            this.SaveDepQueriesBtn = new System.Windows.Forms.Button();
            this.SaveDepVotingBtn = new System.Windows.Forms.Button();
            this.SaveFractionsBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SaveDeputyBtn = new System.Windows.Forms.Button();
            this.TextBoxInformant = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.LawProjectsTab.SuspendLayout();
            this.СommitteesTab.SuspendLayout();
            this.SessionsTab.SuspendLayout();
            this.DeputiesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveLawsBtn
            // 
            this.SaveLawsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveLawsBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveLawsBtn.Location = new System.Drawing.Point(8, 6);
            this.SaveLawsBtn.Name = "SaveLawsBtn";
            this.SaveLawsBtn.Size = new System.Drawing.Size(525, 53);
            this.SaveLawsBtn.TabIndex = 0;
            this.SaveLawsBtn.Text = "Зберегти законопроекти, зареєстровані за поточний тиждень";
            this.SaveLawsBtn.UseVisualStyleBackColor = true;
            this.SaveLawsBtn.Click += new System.EventHandler(this.SaveLawsBtn_Click);
            // 
            // lawsInformLabel
            // 
            this.lawsInformLabel.AutoSize = true;
            this.lawsInformLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lawsInformLabel.Location = new System.Drawing.Point(3, 241);
            this.lawsInformLabel.Name = "lawsInformLabel";
            this.lawsInformLabel.Size = new System.Drawing.Size(243, 15);
            this.lawsInformLabel.TabIndex = 2;
            this.lawsInformLabel.Text = "Інформація про збережені законопроекти:\r\n";
            // 
            // saveReportBtn
            // 
            this.saveReportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveReportBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveReportBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveReportBtn.Location = new System.Drawing.Point(3, 512);
            this.saveReportBtn.Name = "saveReportBtn";
            this.saveReportBtn.Size = new System.Drawing.Size(129, 29);
            this.saveReportBtn.TabIndex = 3;
            this.saveReportBtn.Text = "Зберегти звіт";
            this.saveReportBtn.UseVisualStyleBackColor = true;
            this.saveReportBtn.Click += new System.EventHandler(this.saveReportBtn_Click);
            // 
            // SaveLawsByDatePickerBtn
            // 
            this.SaveLawsByDatePickerBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveLawsByDatePickerBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveLawsByDatePickerBtn.Location = new System.Drawing.Point(9, 147);
            this.SaveLawsByDatePickerBtn.Name = "SaveLawsByDatePickerBtn";
            this.SaveLawsByDatePickerBtn.Size = new System.Drawing.Size(170, 26);
            this.SaveLawsByDatePickerBtn.TabIndex = 4;
            this.SaveLawsByDatePickerBtn.Text = "Шукати та зберегти\r\n";
            this.SaveLawsByDatePickerBtn.UseVisualStyleBackColor = true;
            this.SaveLawsByDatePickerBtn.Click += new System.EventHandler(this.SaveLawsByDatePickerBtn_Click);
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(31, 94);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(147, 20);
            this.startDate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "з:";
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(31, 120);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(147, 20);
            this.endDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "по:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Пошук за датою реєстрації";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.LawProjectsTab);
            this.MainTabControl.Controls.Add(this.СommitteesTab);
            this.MainTabControl.Controls.Add(this.SessionsTab);
            this.MainTabControl.Controls.Add(this.DeputiesTab);
            this.MainTabControl.Location = new System.Drawing.Point(3, 3);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(549, 286);
            this.MainTabControl.TabIndex = 10;
            // 
            // LawProjectsTab
            // 
            this.LawProjectsTab.BackColor = System.Drawing.SystemColors.Control;
            this.LawProjectsTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LawProjectsTab.Controls.Add(this.UpdateLaws);
            this.LawProjectsTab.Controls.Add(this.SaveByLowNameBtn);
            this.LawProjectsTab.Controls.Add(this.SearchLowTextBox);
            this.LawProjectsTab.Controls.Add(this.SaveLawsBtn);
            this.LawProjectsTab.Controls.Add(this.label3);
            this.LawProjectsTab.Controls.Add(this.lawsInformLabel);
            this.LawProjectsTab.Controls.Add(this.label7);
            this.LawProjectsTab.Controls.Add(this.label4);
            this.LawProjectsTab.Controls.Add(this.endDate);
            this.LawProjectsTab.Controls.Add(this.startDate);
            this.LawProjectsTab.Controls.Add(this.label2);
            this.LawProjectsTab.Controls.Add(this.SaveLawsByDatePickerBtn);
            this.LawProjectsTab.Location = new System.Drawing.Point(4, 22);
            this.LawProjectsTab.Name = "LawProjectsTab";
            this.LawProjectsTab.Padding = new System.Windows.Forms.Padding(3);
            this.LawProjectsTab.Size = new System.Drawing.Size(541, 260);
            this.LawProjectsTab.TabIndex = 0;
            this.LawProjectsTab.Text = "Законопроекти";
            // 
            // UpdateLaws
            // 
            this.UpdateLaws.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateLaws.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.UpdateLaws.Location = new System.Drawing.Point(8, 184);
            this.UpdateLaws.Name = "UpdateLaws";
            this.UpdateLaws.Size = new System.Drawing.Size(525, 54);
            this.UpdateLaws.TabIndex = 12;
            this.UpdateLaws.Text = "Оновити законопроекти";
            this.UpdateLaws.UseVisualStyleBackColor = true;
            this.UpdateLaws.Click += new System.EventHandler(this.UpdateLaws_Click);
            // 
            // SaveByLowNameBtn
            // 
            this.SaveByLowNameBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SaveByLowNameBtn.Location = new System.Drawing.Point(198, 147);
            this.SaveByLowNameBtn.Name = "SaveByLowNameBtn";
            this.SaveByLowNameBtn.Size = new System.Drawing.Size(170, 26);
            this.SaveByLowNameBtn.TabIndex = 11;
            this.SaveByLowNameBtn.Text = "Шукати та зберегти";
            this.SaveByLowNameBtn.UseVisualStyleBackColor = true;
            this.SaveByLowNameBtn.Click += new System.EventHandler(this.SaveByLowNameBtn_Click);
            // 
            // SearchLowTextBox
            // 
            this.SearchLowTextBox.Location = new System.Drawing.Point(198, 94);
            this.SearchLowTextBox.Name = "SearchLowTextBox";
            this.SearchLowTextBox.Size = new System.Drawing.Size(164, 20);
            this.SearchLowTextBox.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Пошук за номером реєстрації";
            // 
            // СommitteesTab
            // 
            this.СommitteesTab.BackColor = System.Drawing.SystemColors.Control;
            this.СommitteesTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.СommitteesTab.Controls.Add(this.SaveСommitteesWorkBtn);
            this.СommitteesTab.Controls.Add(this.label1);
            this.СommitteesTab.Controls.Add(this.SaveСommitteesBtn);
            this.СommitteesTab.Location = new System.Drawing.Point(4, 22);
            this.СommitteesTab.Name = "СommitteesTab";
            this.СommitteesTab.Padding = new System.Windows.Forms.Padding(3);
            this.СommitteesTab.Size = new System.Drawing.Size(541, 260);
            this.СommitteesTab.TabIndex = 1;
            this.СommitteesTab.Text = "Комітети";
            // 
            // SaveСommitteesWorkBtn
            // 
            this.SaveСommitteesWorkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveСommitteesWorkBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SaveСommitteesWorkBtn.Location = new System.Drawing.Point(8, 65);
            this.SaveСommitteesWorkBtn.Name = "SaveСommitteesWorkBtn";
            this.SaveСommitteesWorkBtn.Size = new System.Drawing.Size(525, 53);
            this.SaveСommitteesWorkBtn.TabIndex = 3;
            this.SaveСommitteesWorkBtn.Text = "Зберегти план роботи комитетів Верховної Ради";
            this.SaveСommitteesWorkBtn.UseVisualStyleBackColor = true;
            this.SaveСommitteesWorkBtn.Click += new System.EventHandler(this.SaveСommitteesWorkBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(3, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Інформація про збережені комітети ВР:";
            // 
            // SaveСommitteesBtn
            // 
            this.SaveСommitteesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveСommitteesBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SaveСommitteesBtn.Location = new System.Drawing.Point(8, 6);
            this.SaveСommitteesBtn.Name = "SaveСommitteesBtn";
            this.SaveСommitteesBtn.Size = new System.Drawing.Size(525, 53);
            this.SaveСommitteesBtn.TabIndex = 0;
            this.SaveСommitteesBtn.Text = "Зберегти перелік комитетів Верховної Ради";
            this.SaveСommitteesBtn.UseVisualStyleBackColor = true;
            this.SaveСommitteesBtn.Click += new System.EventHandler(this.SaveСommitteesBtn_Click);
            // 
            // SessionsTab
            // 
            this.SessionsTab.BackColor = System.Drawing.SystemColors.Control;
            this.SessionsTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SessionsTab.Controls.Add(this.SavePlenarySessionDatesBtn);
            this.SessionsTab.Controls.Add(this.label5);
            this.SessionsTab.Controls.Add(this.SavePlenarySessionCalendarPlanBtn);
            this.SessionsTab.Location = new System.Drawing.Point(4, 22);
            this.SessionsTab.Name = "SessionsTab";
            this.SessionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SessionsTab.Size = new System.Drawing.Size(541, 260);
            this.SessionsTab.TabIndex = 2;
            this.SessionsTab.Text = "Пленарні засідання";
            // 
            // SavePlenarySessionDatesBtn
            // 
            this.SavePlenarySessionDatesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePlenarySessionDatesBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SavePlenarySessionDatesBtn.Location = new System.Drawing.Point(8, 65);
            this.SavePlenarySessionDatesBtn.Name = "SavePlenarySessionDatesBtn";
            this.SavePlenarySessionDatesBtn.Size = new System.Drawing.Size(525, 53);
            this.SavePlenarySessionDatesBtn.TabIndex = 5;
            this.SavePlenarySessionDatesBtn.Text = "Зберегти графік проведень пленарних засідань Верховної Ради\r\n(за поточну сессію)\r" +
    "\n";
            this.SavePlenarySessionDatesBtn.UseVisualStyleBackColor = true;
            this.SavePlenarySessionDatesBtn.Click += new System.EventHandler(this.SavePlenarySessionDatesBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(3, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(313, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Інформація про збережені графіки пленарних засідань:";
            // 
            // SavePlenarySessionCalendarPlanBtn
            // 
            this.SavePlenarySessionCalendarPlanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePlenarySessionCalendarPlanBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SavePlenarySessionCalendarPlanBtn.Location = new System.Drawing.Point(8, 6);
            this.SavePlenarySessionCalendarPlanBtn.Name = "SavePlenarySessionCalendarPlanBtn";
            this.SavePlenarySessionCalendarPlanBtn.Size = new System.Drawing.Size(525, 53);
            this.SavePlenarySessionCalendarPlanBtn.TabIndex = 1;
            this.SavePlenarySessionCalendarPlanBtn.Text = "Зберегти календарний план пленарних засідань Верховної Ради\r\n(за поточну сессію)";
            this.SavePlenarySessionCalendarPlanBtn.UseVisualStyleBackColor = true;
            this.SavePlenarySessionCalendarPlanBtn.Click += new System.EventHandler(this.SavePlenarySessionCalendarPlanBtn_Click);
            // 
            // DeputiesTab
            // 
            this.DeputiesTab.BackColor = System.Drawing.SystemColors.Control;
            this.DeputiesTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DeputiesTab.Controls.Add(this.PhotoDownloadCheckBox);
            this.DeputiesTab.Controls.Add(this.DepVotingDateTimePickEnd);
            this.DeputiesTab.Controls.Add(this.DepVotingDateTimePickStart);
            this.DeputiesTab.Controls.Add(this.SaveDepLawActivityBtn);
            this.DeputiesTab.Controls.Add(this.SaveDepSpeechesBtn);
            this.DeputiesTab.Controls.Add(this.SaveDepQueriesBtn);
            this.DeputiesTab.Controls.Add(this.SaveDepVotingBtn);
            this.DeputiesTab.Controls.Add(this.SaveFractionsBtn);
            this.DeputiesTab.Controls.Add(this.label6);
            this.DeputiesTab.Controls.Add(this.SaveDeputyBtn);
            this.DeputiesTab.Location = new System.Drawing.Point(4, 22);
            this.DeputiesTab.Name = "DeputiesTab";
            this.DeputiesTab.Padding = new System.Windows.Forms.Padding(3);
            this.DeputiesTab.Size = new System.Drawing.Size(541, 260);
            this.DeputiesTab.TabIndex = 3;
            this.DeputiesTab.Text = "Депутати";
            // 
            // PhotoDownloadCheckBox
            // 
            this.PhotoDownloadCheckBox.AutoSize = true;
            this.PhotoDownloadCheckBox.Location = new System.Drawing.Point(427, 65);
            this.PhotoDownloadCheckBox.Name = "PhotoDownloadCheckBox";
            this.PhotoDownloadCheckBox.Size = new System.Drawing.Size(93, 30);
            this.PhotoDownloadCheckBox.TabIndex = 14;
            this.PhotoDownloadCheckBox.Text = "Завантажити\r\nфото";
            this.PhotoDownloadCheckBox.UseVisualStyleBackColor = true;
            this.PhotoDownloadCheckBox.CheckedChanged += new System.EventHandler(this.PhotoDownloadCheckBox_CheckedChanged);
            // 
            // DepVotingDateTimePickEnd
            // 
            this.DepVotingDateTimePickEnd.Location = new System.Drawing.Point(276, 70);
            this.DepVotingDateTimePickEnd.Name = "DepVotingDateTimePickEnd";
            this.DepVotingDateTimePickEnd.Size = new System.Drawing.Size(144, 20);
            this.DepVotingDateTimePickEnd.TabIndex = 13;
            // 
            // DepVotingDateTimePickStart
            // 
            this.DepVotingDateTimePickStart.Location = new System.Drawing.Point(140, 70);
            this.DepVotingDateTimePickStart.Name = "DepVotingDateTimePickStart";
            this.DepVotingDateTimePickStart.Size = new System.Drawing.Size(129, 20);
            this.DepVotingDateTimePickStart.TabIndex = 12;
            // 
            // ConvocationComboBox
            // 
            this.ConvocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConvocationComboBox.FormattingEnabled = true;
            this.ConvocationComboBox.Location = new System.Drawing.Point(212, 518);
            this.ConvocationComboBox.Name = "ConvocationComboBox";
            this.ConvocationComboBox.Size = new System.Drawing.Size(121, 21);
            this.ConvocationComboBox.TabIndex = 11;
            this.ConvocationComboBox.SelectedIndexChanged += new System.EventHandler(this.ConvocationComboBox_SelectedIndexChanged);
            // 
            // SaveDepLawActivityBtn
            // 
            this.SaveDepLawActivityBtn.Location = new System.Drawing.Point(404, 107);
            this.SaveDepLawActivityBtn.Name = "SaveDepLawActivityBtn";
            this.SaveDepLawActivityBtn.Size = new System.Drawing.Size(129, 70);
            this.SaveDepLawActivityBtn.TabIndex = 10;
            this.SaveDepLawActivityBtn.Text = "Зберегти (чи оновити) інформацію про законотворчу діяльність\r\n";
            this.SaveDepLawActivityBtn.UseVisualStyleBackColor = true;
            this.SaveDepLawActivityBtn.Click += new System.EventHandler(this.SaveDepLawActivityBtn_Click);
            // 
            // SaveDepSpeechesBtn
            // 
            this.SaveDepSpeechesBtn.Location = new System.Drawing.Point(272, 107);
            this.SaveDepSpeechesBtn.Name = "SaveDepSpeechesBtn";
            this.SaveDepSpeechesBtn.Size = new System.Drawing.Size(129, 70);
            this.SaveDepSpeechesBtn.TabIndex = 9;
            this.SaveDepSpeechesBtn.Text = "Зберегти (чи оновити) хронологію виступів депутатів\r\n";
            this.SaveDepSpeechesBtn.UseVisualStyleBackColor = true;
            this.SaveDepSpeechesBtn.Click += new System.EventHandler(this.SaveDepSpeechesBtn_Click);
            // 
            // SaveDepQueriesBtn
            // 
            this.SaveDepQueriesBtn.Location = new System.Drawing.Point(140, 107);
            this.SaveDepQueriesBtn.Name = "SaveDepQueriesBtn";
            this.SaveDepQueriesBtn.Size = new System.Drawing.Size(129, 70);
            this.SaveDepQueriesBtn.TabIndex = 8;
            this.SaveDepQueriesBtn.Text = "Зберегти (чи оновити) інформацію про депутатські запити";
            this.SaveDepQueriesBtn.UseVisualStyleBackColor = true;
            this.SaveDepQueriesBtn.Click += new System.EventHandler(this.SaveDepQueriesBtn_Click);
            // 
            // SaveDepVotingBtn
            // 
            this.SaveDepVotingBtn.Location = new System.Drawing.Point(8, 107);
            this.SaveDepVotingBtn.Name = "SaveDepVotingBtn";
            this.SaveDepVotingBtn.Size = new System.Drawing.Size(129, 70);
            this.SaveDepVotingBtn.TabIndex = 7;
            this.SaveDepVotingBtn.Text = "Зберегти (чи оновити) інформацію про голосування депутатів\r\n";
            this.SaveDepVotingBtn.UseVisualStyleBackColor = true;
            this.SaveDepVotingBtn.Click += new System.EventHandler(this.SaveDepVotingBtn_Click);
            // 
            // SaveFractionsBtn
            // 
            this.SaveFractionsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFractionsBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SaveFractionsBtn.Location = new System.Drawing.Point(8, 182);
            this.SaveFractionsBtn.Name = "SaveFractionsBtn";
            this.SaveFractionsBtn.Size = new System.Drawing.Size(525, 53);
            this.SaveFractionsBtn.TabIndex = 6;
            this.SaveFractionsBtn.Text = "Зберегти (чи оновити) депутатські фракції і групи\r\n";
            this.SaveFractionsBtn.UseVisualStyleBackColor = true;
            this.SaveFractionsBtn.Click += new System.EventHandler(this.SaveFractionsBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(3, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Імена депутатів, інформацію про яких було збережено:";
            // 
            // SaveDeputyBtn
            // 
            this.SaveDeputyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDeputyBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.SaveDeputyBtn.Location = new System.Drawing.Point(8, 6);
            this.SaveDeputyBtn.Name = "SaveDeputyBtn";
            this.SaveDeputyBtn.Size = new System.Drawing.Size(525, 53);
            this.SaveDeputyBtn.TabIndex = 2;
            this.SaveDeputyBtn.Text = "Зберегти (чи оновити) загальну інформацію про депутатів";
            this.SaveDeputyBtn.UseVisualStyleBackColor = true;
            this.SaveDeputyBtn.Click += new System.EventHandler(this.SaveDeputyBtn_Click);
            // 
            // TextBoxInformant
            // 
            this.TextBoxInformant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxInformant.Location = new System.Drawing.Point(5, 291);
            this.TextBoxInformant.Name = "TextBoxInformant";
            this.TextBoxInformant.Size = new System.Drawing.Size(545, 194);
            this.TextBoxInformant.TabIndex = 12;
            this.TextBoxInformant.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.Location = new System.Drawing.Point(5, 487);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(544, 23);
            this.progressBar.TabIndex = 10;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.CancelBtn.Location = new System.Drawing.Point(420, 512);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(129, 29);
            this.CancelBtn.TabIndex = 13;
            this.CancelBtn.Text = "Зупинити пошук";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 544);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.TextBoxInformant);
            this.Controls.Add(this.ConvocationComboBox);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.saveReportBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Зберігання данних з сайту ВР";
            this.MainTabControl.ResumeLayout(false);
            this.LawProjectsTab.ResumeLayout(false);
            this.LawProjectsTab.PerformLayout();
            this.СommitteesTab.ResumeLayout(false);
            this.СommitteesTab.PerformLayout();
            this.SessionsTab.ResumeLayout(false);
            this.SessionsTab.PerformLayout();
            this.DeputiesTab.ResumeLayout(false);
            this.DeputiesTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveLawsBtn;
        private System.Windows.Forms.Label lawsInformLabel;
        private System.Windows.Forms.Button saveReportBtn;
        private System.Windows.Forms.Button SaveLawsByDatePickerBtn;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage LawProjectsTab;
        private System.Windows.Forms.TabPage СommitteesTab;
        private System.Windows.Forms.Button SaveСommitteesBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage SessionsTab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SavePlenarySessionCalendarPlanBtn;
        private System.Windows.Forms.RichTextBox TextBoxInformant;
        private System.Windows.Forms.TabPage DeputiesTab;
        private System.Windows.Forms.Button SaveDeputyBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SaveFractionsBtn;
        private System.Windows.Forms.Button SaveDepLawActivityBtn;
        private System.Windows.Forms.Button SaveDepSpeechesBtn;
        private System.Windows.Forms.Button SaveDepQueriesBtn;
        private System.Windows.Forms.Button SaveDepVotingBtn;
        private System.Windows.Forms.Button SaveСommitteesWorkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SavePlenarySessionDatesBtn;
        private System.Windows.Forms.Button UpdateLaws;
        private System.Windows.Forms.Button SaveByLowNameBtn;
        private System.Windows.Forms.TextBox SearchLowTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ConvocationComboBox;
        private System.Windows.Forms.DateTimePicker DepVotingDateTimePickEnd;
        private System.Windows.Forms.DateTimePicker DepVotingDateTimePickStart;
        private System.Windows.Forms.CheckBox PhotoDownloadCheckBox;
    }
}

