using System.Drawing;
using MetroFramework.Components;

namespace GovernmentParse.Controls
{
    partial class LawsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LawsControl));
            this.RightSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            this.SaveLawsByDatePickerPanel = new System.Windows.Forms.Panel();
            this.SaveAllLawsFromCurrentConvocationBtn = new System.Windows.Forms.Button();
            this.SaveAllLawsCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveLawsByDatePickerBtn = new System.Windows.Forms.Button();
            this.endDatePanel = new System.Windows.Forms.Panel();
            this.EndDatePickerLabel = new System.Windows.Forms.Label();
            this.endDate = new MetroFramework.Controls.MetroDateTime();
            this.StartDatePanel = new System.Windows.Forms.Panel();
            this.startDate = new MetroFramework.Controls.MetroDateTime();
            this.StartDatePickerLabel = new System.Windows.Forms.Label();
            this.SaveLawsByDatePickerPicBox = new System.Windows.Forms.PictureBox();
            this.SaveLawsByDatesLabel = new System.Windows.Forms.Label();
            this.UpdateLawsPanel = new System.Windows.Forms.Panel();
            this.UpdateLawsPicBox = new System.Windows.Forms.PictureBox();
            this.UpdateLawsByDayLabel = new System.Windows.Forms.Label();
            this.UpdateLaws = new System.Windows.Forms.Button();
            this.EmptyPanelDownRight = new System.Windows.Forms.Panel();
            this.LeftSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            this.Slider = new System.Windows.Forms.Panel();
            this.SaveLawsPanel = new System.Windows.Forms.Panel();
            this.SaveLawsBtn = new System.Windows.Forms.Button();
            this.SaveLawsPicBox = new System.Windows.Forms.PictureBox();
            this.SaveLastWeekLawsLabel = new System.Windows.Forms.Label();
            this.SaveByLowNamePanel = new System.Windows.Forms.Panel();
            this.SearchLowTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SaveByLowNamePicBox = new System.Windows.Forms.PictureBox();
            this.SaveLawsByNamesLabel = new System.Windows.Forms.Label();
            this.SaveByLowNameBtn = new System.Windows.Forms.Button();
            this.MainSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.Panel2.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            this.SaveLawsByDatePickerPanel.SuspendLayout();
            this.endDatePanel.SuspendLayout();
            this.StartDatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveLawsByDatePickerPicBox)).BeginInit();
            this.UpdateLawsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateLawsPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel1.SuspendLayout();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            this.SaveLawsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveLawsPicBox)).BeginInit();
            this.SaveByLowNamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveByLowNamePicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // RightSplitContainer
            // 
            this.RightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightSplitContainer.IsSplitterFixed = true;
            this.RightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.RightSplitContainer.Name = "RightSplitContainer";
            this.RightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // RightSplitContainer.Panel1
            // 
            this.RightSplitContainer.Panel1.Controls.Add(this.SaveLawsByDatePickerPanel);
            // 
            // RightSplitContainer.Panel2
            // 
            this.RightSplitContainer.Panel2.Controls.Add(this.UpdateLawsPanel);
            this.RightSplitContainer.Panel2.Controls.Add(this.EmptyPanelDownRight);
            this.RightSplitContainer.Size = new System.Drawing.Size(415, 486);
            this.RightSplitContainer.SplitterDistance = 243;
            this.RightSplitContainer.TabIndex = 0;
            // 
            // SaveLawsByDatePickerPanel
            // 
            this.SaveLawsByDatePickerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsByDatePickerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveLawsByDatePickerPanel.Controls.Add(this.SaveAllLawsFromCurrentConvocationBtn);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.SaveAllLawsCheckBox);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.SaveLawsByDatePickerBtn);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.endDatePanel);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.StartDatePanel);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.SaveLawsByDatePickerPicBox);
            this.SaveLawsByDatePickerPanel.Controls.Add(this.SaveLawsByDatesLabel);
            this.SaveLawsByDatePickerPanel.Location = new System.Drawing.Point(0, 26);
            this.SaveLawsByDatePickerPanel.Name = "SaveLawsByDatePickerPanel";
            this.SaveLawsByDatePickerPanel.Size = new System.Drawing.Size(379, 217);
            this.SaveLawsByDatePickerPanel.TabIndex = 16;
            this.SaveLawsByDatePickerPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SaveAllLawsFromCurrentConvocationBtn
            // 
            this.SaveAllLawsFromCurrentConvocationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveAllLawsFromCurrentConvocationBtn.AutoSize = true;
            this.SaveAllLawsFromCurrentConvocationBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveAllLawsFromCurrentConvocationBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveAllLawsFromCurrentConvocationBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.SaveAllLawsFromCurrentConvocationBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.SaveAllLawsFromCurrentConvocationBtn.FlatAppearance.BorderSize = 0;
            this.SaveAllLawsFromCurrentConvocationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveAllLawsFromCurrentConvocationBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveAllLawsFromCurrentConvocationBtn.ForeColor = System.Drawing.Color.Black;
            this.SaveAllLawsFromCurrentConvocationBtn.Location = new System.Drawing.Point(244, 174);
            this.SaveAllLawsFromCurrentConvocationBtn.Name = "SaveAllLawsFromCurrentConvocationBtn";
            this.SaveAllLawsFromCurrentConvocationBtn.Size = new System.Drawing.Size(122, 31);
            this.SaveAllLawsFromCurrentConvocationBtn.TabIndex = 22;
            this.SaveAllLawsFromCurrentConvocationBtn.Text = "Завантажити";
            this.SaveAllLawsFromCurrentConvocationBtn.UseVisualStyleBackColor = false;
            this.SaveAllLawsFromCurrentConvocationBtn.Click += new System.EventHandler(this.SaveAllLawsFromCurrentConvocationBtn_Click);
            // 
            // SaveAllLawsCheckBox
            // 
            this.SaveAllLawsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SaveAllLawsCheckBox.AutoSize = true;
            this.SaveAllLawsCheckBox.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.SaveAllLawsCheckBox.Location = new System.Drawing.Point(253, 120);
            this.SaveAllLawsCheckBox.Name = "SaveAllLawsCheckBox";
            this.SaveAllLawsCheckBox.Size = new System.Drawing.Size(113, 24);
            this.SaveAllLawsCheckBox.TabIndex = 21;
            this.SaveAllLawsCheckBox.Text = "за весь час";
            this.SaveAllLawsCheckBox.UseVisualStyleBackColor = true;
            this.SaveAllLawsCheckBox.CheckedChanged += new System.EventHandler(this.SaveAllLawsCheckBox_CheckedChanged);
            // 
            // SaveLawsByDatePickerBtn
            // 
            this.SaveLawsByDatePickerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsByDatePickerBtn.AutoSize = true;
            this.SaveLawsByDatePickerBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveLawsByDatePickerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveLawsByDatePickerBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.SaveLawsByDatePickerBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.SaveLawsByDatePickerBtn.FlatAppearance.BorderSize = 0;
            this.SaveLawsByDatePickerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveLawsByDatePickerBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveLawsByDatePickerBtn.ForeColor = System.Drawing.Color.Black;
            this.SaveLawsByDatePickerBtn.Location = new System.Drawing.Point(244, 174);
            this.SaveLawsByDatePickerBtn.Name = "SaveLawsByDatePickerBtn";
            this.SaveLawsByDatePickerBtn.Size = new System.Drawing.Size(122, 31);
            this.SaveLawsByDatePickerBtn.TabIndex = 8;
            this.SaveLawsByDatePickerBtn.Text = "Завантажити";
            this.SaveLawsByDatePickerBtn.UseVisualStyleBackColor = false;
            this.SaveLawsByDatePickerBtn.Click += new System.EventHandler(this.SaveLawsByDatePickerBtn_Click);
            // 
            // endDatePanel
            // 
            this.endDatePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.endDatePanel.AutoSize = true;
            this.endDatePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.endDatePanel.Controls.Add(this.EndDatePickerLabel);
            this.endDatePanel.Controls.Add(this.endDate);
            this.endDatePanel.Location = new System.Drawing.Point(0, 151);
            this.endDatePanel.Name = "endDatePanel";
            this.endDatePanel.Size = new System.Drawing.Size(176, 66);
            this.endDatePanel.TabIndex = 18;
            // 
            // EndDatePickerLabel
            // 
            this.EndDatePickerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EndDatePickerLabel.AutoSize = true;
            this.EndDatePickerLabel.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.EndDatePickerLabel.Location = new System.Drawing.Point(11, 2);
            this.EndDatePickerLabel.Name = "EndDatePickerLabel";
            this.EndDatePickerLabel.Size = new System.Drawing.Size(56, 20);
            this.EndDatePickerLabel.TabIndex = 11;
            this.EndDatePickerLabel.Text = "кінець";
            // 
            // endDate
            // 
            this.endDate.CalendarForeColor = System.Drawing.Color.Black;
            this.endDate.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.endDate.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.endDate.DisplayFocus = true;
            this.endDate.Location = new System.Drawing.Point(13, 23);
            this.endDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(160, 29);
            this.endDate.TabIndex = 17;
            this.endDate.TabStop = false;
            this.endDate.UseCustomForeColor = true;
            // 
            // StartDatePanel
            // 
            this.StartDatePanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StartDatePanel.AutoSize = true;
            this.StartDatePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartDatePanel.Controls.Add(this.startDate);
            this.StartDatePanel.Controls.Add(this.StartDatePickerLabel);
            this.StartDatePanel.Location = new System.Drawing.Point(0, 85);
            this.StartDatePanel.Name = "StartDatePanel";
            this.StartDatePanel.Size = new System.Drawing.Size(176, 66);
            this.StartDatePanel.TabIndex = 19;
            // 
            // startDate
            // 
            this.startDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startDate.CalendarForeColor = System.Drawing.Color.Black;
            this.startDate.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startDate.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.startDate.CalendarTrailingForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startDate.DisplayFocus = true;
            this.startDate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.startDate.Location = new System.Drawing.Point(13, 31);
            this.startDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(160, 29);
            this.startDate.TabIndex = 16;
            this.startDate.TabStop = false;
            this.startDate.UseCustomBackColor = true;
            this.startDate.UseCustomForeColor = true;
            this.startDate.UseStyleColors = true;
            // 
            // StartDatePickerLabel
            // 
            this.StartDatePickerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StartDatePickerLabel.AutoSize = true;
            this.StartDatePickerLabel.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.StartDatePickerLabel.Location = new System.Drawing.Point(11, 10);
            this.StartDatePickerLabel.Name = "StartDatePickerLabel";
            this.StartDatePickerLabel.Size = new System.Drawing.Size(69, 20);
            this.StartDatePickerLabel.TabIndex = 11;
            this.StartDatePickerLabel.Text = "початок";
            // 
            // SaveLawsByDatePickerPicBox
            // 
            this.SaveLawsByDatePickerPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsByDatePickerPicBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveLawsByDatePickerPicBox.Image")));
            this.SaveLawsByDatePickerPicBox.Location = new System.Drawing.Point(259, 2);
            this.SaveLawsByDatePickerPicBox.Name = "SaveLawsByDatePickerPicBox";
            this.SaveLawsByDatePickerPicBox.Size = new System.Drawing.Size(120, 100);
            this.SaveLawsByDatePickerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveLawsByDatePickerPicBox.TabIndex = 7;
            this.SaveLawsByDatePickerPicBox.TabStop = false;
            // 
            // SaveLawsByDatesLabel
            // 
            this.SaveLawsByDatesLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveLawsByDatesLabel.ForeColor = System.Drawing.Color.Black;
            this.SaveLawsByDatesLabel.Location = new System.Drawing.Point(12, 11);
            this.SaveLawsByDatesLabel.Name = "SaveLawsByDatesLabel";
            this.SaveLawsByDatesLabel.Size = new System.Drawing.Size(249, 66);
            this.SaveLawsByDatesLabel.TabIndex = 8;
            this.SaveLawsByDatesLabel.Text = "Шукати та зберегти законопроекти за обраний період";
            // 
            // UpdateLawsPanel
            // 
            this.UpdateLawsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateLawsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.UpdateLawsPanel.Controls.Add(this.UpdateLawsPicBox);
            this.UpdateLawsPanel.Controls.Add(this.UpdateLawsByDayLabel);
            this.UpdateLawsPanel.Controls.Add(this.UpdateLaws);
            this.UpdateLawsPanel.Location = new System.Drawing.Point(0, 0);
            this.UpdateLawsPanel.Name = "UpdateLawsPanel";
            this.UpdateLawsPanel.Size = new System.Drawing.Size(379, 217);
            this.UpdateLawsPanel.TabIndex = 18;
            this.UpdateLawsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateLawsPanel_Paint);
            this.UpdateLawsPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // UpdateLawsPicBox
            // 
            this.UpdateLawsPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateLawsPicBox.Image = ((System.Drawing.Image)(resources.GetObject("UpdateLawsPicBox.Image")));
            this.UpdateLawsPicBox.Location = new System.Drawing.Point(259, 2);
            this.UpdateLawsPicBox.Name = "UpdateLawsPicBox";
            this.UpdateLawsPicBox.Size = new System.Drawing.Size(120, 100);
            this.UpdateLawsPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UpdateLawsPicBox.TabIndex = 7;
            this.UpdateLawsPicBox.TabStop = false;
            // 
            // UpdateLawsByDayLabel
            // 
            this.UpdateLawsByDayLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.UpdateLawsByDayLabel.ForeColor = System.Drawing.Color.Black;
            this.UpdateLawsByDayLabel.Location = new System.Drawing.Point(12, 11);
            this.UpdateLawsByDayLabel.Name = "UpdateLawsByDayLabel";
            this.UpdateLawsByDayLabel.Size = new System.Drawing.Size(249, 97);
            this.UpdateLawsByDayLabel.TabIndex = 15;
            this.UpdateLawsByDayLabel.Text = "Зберегти законопроекти, які були зареєстровані чи оновилися за минулу добу \r\n";
            // 
            // UpdateLaws
            // 
            this.UpdateLaws.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateLaws.AutoSize = true;
            this.UpdateLaws.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UpdateLaws.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.UpdateLaws.FlatAppearance.BorderSize = 0;
            this.UpdateLaws.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateLaws.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.UpdateLaws.ForeColor = System.Drawing.Color.Black;
            this.UpdateLaws.Location = new System.Drawing.Point(244, 174);
            this.UpdateLaws.Name = "UpdateLaws";
            this.UpdateLaws.Size = new System.Drawing.Size(122, 31);
            this.UpdateLaws.TabIndex = 6;
            this.UpdateLaws.Text = "Завантажити";
            this.UpdateLaws.UseVisualStyleBackColor = false;
            this.UpdateLaws.Click += new System.EventHandler(this.UpdateLaws_Click);
            // 
            // EmptyPanelDownRight
            // 
            this.EmptyPanelDownRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmptyPanelDownRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(249)))));
            this.EmptyPanelDownRight.Location = new System.Drawing.Point(0, 0);
            this.EmptyPanelDownRight.Name = "EmptyPanelDownRight";
            this.EmptyPanelDownRight.Size = new System.Drawing.Size(379, 217);
            this.EmptyPanelDownRight.TabIndex = 19;
            // 
            // LeftSplitContainer
            // 
            this.LeftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftSplitContainer.IsSplitterFixed = true;
            this.LeftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.LeftSplitContainer.Name = "LeftSplitContainer";
            this.LeftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LeftSplitContainer.Panel1
            // 
            this.LeftSplitContainer.Panel1.Controls.Add(this.Slider);
            this.LeftSplitContainer.Panel1.Controls.Add(this.SaveLawsPanel);
            // 
            // LeftSplitContainer.Panel2
            // 
            this.LeftSplitContainer.Panel2.Controls.Add(this.SaveByLowNamePanel);
            this.LeftSplitContainer.Size = new System.Drawing.Size(418, 486);
            this.LeftSplitContainer.SplitterDistance = 243;
            this.LeftSplitContainer.TabIndex = 0;
            // 
            // Slider
            // 
            this.Slider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Slider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(111)))), ((int)(((byte)(209)))));
            this.Slider.Location = new System.Drawing.Point(29, 26);
            this.Slider.Name = "Slider";
            this.Slider.Size = new System.Drawing.Size(10, 217);
            this.Slider.TabIndex = 20;
            // 
            // SaveLawsPanel
            // 
            this.SaveLawsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveLawsPanel.Controls.Add(this.SaveLawsBtn);
            this.SaveLawsPanel.Controls.Add(this.SaveLawsPicBox);
            this.SaveLawsPanel.Controls.Add(this.SaveLastWeekLawsLabel);
            this.SaveLawsPanel.Location = new System.Drawing.Point(39, 26);
            this.SaveLawsPanel.Name = "SaveLawsPanel";
            this.SaveLawsPanel.Size = new System.Drawing.Size(379, 217);
            this.SaveLawsPanel.TabIndex = 19;
            this.SaveLawsPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SaveLawsBtn
            // 
            this.SaveLawsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsBtn.AutoSize = true;
            this.SaveLawsBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveLawsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveLawsBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.SaveLawsBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.SaveLawsBtn.FlatAppearance.BorderSize = 0;
            this.SaveLawsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveLawsBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveLawsBtn.ForeColor = System.Drawing.Color.Black;
            this.SaveLawsBtn.Location = new System.Drawing.Point(244, 174);
            this.SaveLawsBtn.Name = "SaveLawsBtn";
            this.SaveLawsBtn.Size = new System.Drawing.Size(122, 31);
            this.SaveLawsBtn.TabIndex = 0;
            this.SaveLawsBtn.Text = "Завантажити";
            this.SaveLawsBtn.UseVisualStyleBackColor = false;
            this.SaveLawsBtn.Click += new System.EventHandler(this.SaveLawsBtn_Click);
            // 
            // SaveLawsPicBox
            // 
            this.SaveLawsPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLawsPicBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveLawsPicBox.Image")));
            this.SaveLawsPicBox.Location = new System.Drawing.Point(259, 2);
            this.SaveLawsPicBox.Name = "SaveLawsPicBox";
            this.SaveLawsPicBox.Size = new System.Drawing.Size(120, 100);
            this.SaveLawsPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveLawsPicBox.TabIndex = 7;
            this.SaveLawsPicBox.TabStop = false;
            // 
            // SaveLastWeekLawsLabel
            // 
            this.SaveLastWeekLawsLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveLastWeekLawsLabel.ForeColor = System.Drawing.Color.Black;
            this.SaveLastWeekLawsLabel.Location = new System.Drawing.Point(12, 11);
            this.SaveLastWeekLawsLabel.Name = "SaveLastWeekLawsLabel";
            this.SaveLastWeekLawsLabel.Size = new System.Drawing.Size(249, 97);
            this.SaveLastWeekLawsLabel.TabIndex = 5;
            this.SaveLastWeekLawsLabel.Text = "Зберегти законопроекти, зареєстровані за поточний тиждень\r\n";
            // 
            // SaveByLowNamePanel
            // 
            this.SaveByLowNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveByLowNamePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveByLowNamePanel.Controls.Add(this.SearchLowTextBox);
            this.SaveByLowNamePanel.Controls.Add(this.panel2);
            this.SaveByLowNamePanel.Controls.Add(this.SaveByLowNamePicBox);
            this.SaveByLowNamePanel.Controls.Add(this.SaveLawsByNamesLabel);
            this.SaveByLowNamePanel.Controls.Add(this.SaveByLowNameBtn);
            this.SaveByLowNamePanel.Location = new System.Drawing.Point(39, 0);
            this.SaveByLowNamePanel.Name = "SaveByLowNamePanel";
            this.SaveByLowNamePanel.Size = new System.Drawing.Size(379, 217);
            this.SaveByLowNamePanel.TabIndex = 17;
            this.SaveByLowNamePanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SearchLowTextBox
            // 
            this.SearchLowTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchLowTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SearchLowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchLowTextBox.Location = new System.Drawing.Point(20, 122);
            this.SearchLowTextBox.Name = "SearchLowTextBox";
            this.SearchLowTextBox.Size = new System.Drawing.Size(346, 13);
            this.SearchLowTextBox.TabIndex = 16;
            this.SearchLowTextBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(104)))));
            this.panel2.Location = new System.Drawing.Point(20, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(346, 1);
            this.panel2.TabIndex = 15;
            // 
            // SaveByLowNamePicBox
            // 
            this.SaveByLowNamePicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveByLowNamePicBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveByLowNamePicBox.Image")));
            this.SaveByLowNamePicBox.Location = new System.Drawing.Point(259, 2);
            this.SaveByLowNamePicBox.Name = "SaveByLowNamePicBox";
            this.SaveByLowNamePicBox.Size = new System.Drawing.Size(120, 100);
            this.SaveByLowNamePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveByLowNamePicBox.TabIndex = 7;
            this.SaveByLowNamePicBox.TabStop = false;
            // 
            // SaveLawsByNamesLabel
            // 
            this.SaveLawsByNamesLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveLawsByNamesLabel.ForeColor = System.Drawing.Color.Black;
            this.SaveLawsByNamesLabel.Location = new System.Drawing.Point(12, 11);
            this.SaveLawsByNamesLabel.Name = "SaveLawsByNamesLabel";
            this.SaveLawsByNamesLabel.Size = new System.Drawing.Size(249, 97);
            this.SaveLawsByNamesLabel.TabIndex = 7;
            this.SaveLawsByNamesLabel.Text = "Шукати та зберегти законопроекти за номером реєстрації\r\n";
            // 
            // SaveByLowNameBtn
            // 
            this.SaveByLowNameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveByLowNameBtn.AutoSize = true;
            this.SaveByLowNameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveByLowNameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveByLowNameBtn.FlatAppearance.BorderSize = 0;
            this.SaveByLowNameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveByLowNameBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveByLowNameBtn.ForeColor = System.Drawing.Color.Black;
            this.SaveByLowNameBtn.Location = new System.Drawing.Point(244, 174);
            this.SaveByLowNameBtn.Name = "SaveByLowNameBtn";
            this.SaveByLowNameBtn.Size = new System.Drawing.Size(122, 31);
            this.SaveByLowNameBtn.TabIndex = 1;
            this.SaveByLowNameBtn.TabStop = false;
            this.SaveByLowNameBtn.Text = "Завантажити";
            this.SaveByLowNameBtn.UseVisualStyleBackColor = false;
            this.SaveByLowNameBtn.Click += new System.EventHandler(this.SaveByLowNameBtn_Click);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.IsSplitterFixed = true;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.LeftSplitContainer);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.RightSplitContainer);
            this.MainSplitContainer.Size = new System.Drawing.Size(837, 486);
            this.MainSplitContainer.SplitterDistance = 418;
            this.MainSplitContainer.TabIndex = 19;
            // 
            // LawsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "LawsControl";
            this.Size = new System.Drawing.Size(837, 486);
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            this.RightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).EndInit();
            this.RightSplitContainer.ResumeLayout(false);
            this.SaveLawsByDatePickerPanel.ResumeLayout(false);
            this.SaveLawsByDatePickerPanel.PerformLayout();
            this.endDatePanel.ResumeLayout(false);
            this.endDatePanel.PerformLayout();
            this.StartDatePanel.ResumeLayout(false);
            this.StartDatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveLawsByDatePickerPicBox)).EndInit();
            this.UpdateLawsPanel.ResumeLayout(false);
            this.UpdateLawsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateLawsPicBox)).EndInit();
            this.LeftSplitContainer.Panel1.ResumeLayout(false);
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).EndInit();
            this.LeftSplitContainer.ResumeLayout(false);
            this.SaveLawsPanel.ResumeLayout(false);
            this.SaveLawsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveLawsPicBox)).EndInit();
            this.SaveByLowNamePanel.ResumeLayout(false);
            this.SaveByLowNamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveByLowNamePicBox)).EndInit();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NonFlickerSplitContainer RightSplitContainer;
        private System.Windows.Forms.Panel SaveLawsByDatePickerPanel;
        private MetroFramework.Controls.MetroDateTime endDate;
        private MetroFramework.Controls.MetroDateTime startDate;
        private System.Windows.Forms.PictureBox SaveLawsByDatePickerPicBox;
        private System.Windows.Forms.Label SaveLawsByDatesLabel;
        private System.Windows.Forms.Label StartDatePickerLabel;
        private System.Windows.Forms.Label EndDatePickerLabel;
        private System.Windows.Forms.Panel UpdateLawsPanel;
        private System.Windows.Forms.PictureBox UpdateLawsPicBox;
        private System.Windows.Forms.Label UpdateLawsByDayLabel;
        private System.Windows.Forms.Button UpdateLaws;
        private NonFlickerSplitContainer LeftSplitContainer;
        private System.Windows.Forms.Panel Slider;
        private System.Windows.Forms.Panel SaveLawsPanel;
        private System.Windows.Forms.PictureBox SaveLawsPicBox;
        private System.Windows.Forms.Label SaveLastWeekLawsLabel;
        private System.Windows.Forms.Panel SaveByLowNamePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox SaveByLowNamePicBox;
        private System.Windows.Forms.Label SaveLawsByNamesLabel;
        private System.Windows.Forms.Button SaveByLowNameBtn;
        private NonFlickerSplitContainer MainSplitContainer;
        private System.Windows.Forms.Panel endDatePanel;
        private System.Windows.Forms.Panel StartDatePanel;
        private System.Windows.Forms.Button SaveLawsByDatePickerBtn;
        private System.Windows.Forms.Button SaveLawsBtn;
        private System.Windows.Forms.TextBox SearchLowTextBox;
        private System.Windows.Forms.Panel EmptyPanelDownRight;
        private System.Windows.Forms.CheckBox SaveAllLawsCheckBox;
        private System.Windows.Forms.Button SaveAllLawsFromCurrentConvocationBtn;
    }
}
