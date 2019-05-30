using GovernmentParse.Controls;

namespace GovernmentParse
{
    partial class ModernForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModernForm));
			this.SidePanel = new System.Windows.Forms.Panel();
			this.SidePanelSlider = new System.Windows.Forms.Panel();
			this.ReportTabButton = new System.Windows.Forms.Button();
			this.FractionsTabButton = new System.Windows.Forms.Button();
			this.DeputiesTabButton = new System.Windows.Forms.Button();
			this.PlanarySessionsTabButton = new System.Windows.Forms.Button();
			this.CommitteesTabButton = new System.Windows.Forms.Button();
			this.LawsTabButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ReportPanel = new System.Windows.Forms.Panel();
			this.HeaderPanel = new System.Windows.Forms.Panel();
			this.HeaderText = new System.Windows.Forms.Label();
			this.CloseWindowBtn = new System.Windows.Forms.Button();
			this.MaximizeWindowBtn = new System.Windows.Forms.Button();
			this.MinimizeWindowBtn = new System.Windows.Forms.Button();
			this.WorkAreaPanel = new System.Windows.Forms.Panel();
			this.toolTipModernForm = new System.Windows.Forms.ToolTip(this.components);
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanelOuter = new System.Windows.Forms.TableLayoutPanel();
			this.progressBarsPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanelInsider = new System.Windows.Forms.TableLayoutPanel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cursor = new System.Windows.Forms.Panel();
			this.FractionsControl = new GovernmentParse.Controls.FractionsControl();
			this.PlanarySessionsControl = new GovernmentParse.Controls.PlanarySessionsControl();
			this.DeputiesControl = new GovernmentParse.Controls.DeputiesControl();
			this.CommitteesControl = new GovernmentParse.Controls.CommitteesControl();
			this.LawsControl = new GovernmentParse.Controls.LawsControl();
			this.SaveProgressBar = new GovernmentParse.Controls.SmoothProgressBar();
			this.CompareProgressBar = new GovernmentParse.Controls.SmoothProgressBar();
			this.SearchProgressBar = new GovernmentParse.Controls.SmoothProgressBar();
			this.ReportControl = new GovernmentParse.Controls.ReportControl();
			this.SidePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.ReportPanel.SuspendLayout();
			this.HeaderPanel.SuspendLayout();
			this.WorkAreaPanel.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			this.tableLayoutPanelOuter.SuspendLayout();
			this.progressBarsPanel.SuspendLayout();
			this.tableLayoutPanelInsider.SuspendLayout();
			this.SuspendLayout();
			// 
			// SidePanel
			// 
			this.SidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(35)))));
			this.SidePanel.Controls.Add(this.SidePanelSlider);
			this.SidePanel.Controls.Add(this.ReportTabButton);
			this.SidePanel.Controls.Add(this.FractionsTabButton);
			this.SidePanel.Controls.Add(this.DeputiesTabButton);
			this.SidePanel.Controls.Add(this.PlanarySessionsTabButton);
			this.SidePanel.Controls.Add(this.CommitteesTabButton);
			this.SidePanel.Controls.Add(this.LawsTabButton);
			this.SidePanel.Controls.Add(this.pictureBox1);
			this.SidePanel.Location = new System.Drawing.Point(0, 0);
			this.SidePanel.Name = "SidePanel";
			this.SidePanel.Size = new System.Drawing.Size(207, 617);
			this.SidePanel.TabIndex = 0;
			// 
			// SidePanelSlider
			// 
			this.SidePanelSlider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(111)))), ((int)(((byte)(209)))));
			this.SidePanelSlider.Location = new System.Drawing.Point(0, 146);
			this.SidePanelSlider.Name = "SidePanelSlider";
			this.SidePanelSlider.Size = new System.Drawing.Size(10, 58);
			this.SidePanelSlider.TabIndex = 2;
			// 
			// ReportTabButton
			// 
			this.ReportTabButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ReportTabButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(35)))));
			this.ReportTabButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ReportTabButton.FlatAppearance.BorderSize = 0;
			this.ReportTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ReportTabButton.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ReportTabButton.ForeColor = System.Drawing.Color.White;
			this.ReportTabButton.Image = global::GovernmentParse.Properties.Resources.Save_48px;
			this.ReportTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ReportTabButton.Location = new System.Drawing.Point(12, 559);
			this.ReportTabButton.Name = "ReportTabButton";
			this.ReportTabButton.Size = new System.Drawing.Size(195, 58);
			this.ReportTabButton.TabIndex = 1;
			this.ReportTabButton.Text = "   Звіт";
			this.ReportTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.ReportTabButton.UseVisualStyleBackColor = false;
			this.ReportTabButton.Click += new System.EventHandler(this.ReportTabButton_Click);
			// 
			// FractionsTabButton
			// 
			this.FractionsTabButton.FlatAppearance.BorderSize = 0;
			this.FractionsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.FractionsTabButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FractionsTabButton.ForeColor = System.Drawing.Color.White;
			this.FractionsTabButton.Image = ((System.Drawing.Image)(resources.GetObject("FractionsTabButton.Image")));
			this.FractionsTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.FractionsTabButton.Location = new System.Drawing.Point(12, 402);
			this.FractionsTabButton.Name = "FractionsTabButton";
			this.FractionsTabButton.Size = new System.Drawing.Size(195, 58);
			this.FractionsTabButton.TabIndex = 1;
			this.FractionsTabButton.Text = "   Фракції";
			this.FractionsTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.FractionsTabButton.UseVisualStyleBackColor = true;
			this.FractionsTabButton.Click += new System.EventHandler(this.FractionsTabButton_Click);
			// 
			// DeputiesTabButton
			// 
			this.DeputiesTabButton.FlatAppearance.BorderSize = 0;
			this.DeputiesTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DeputiesTabButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.DeputiesTabButton.ForeColor = System.Drawing.Color.White;
			this.DeputiesTabButton.Image = ((System.Drawing.Image)(resources.GetObject("DeputiesTabButton.Image")));
			this.DeputiesTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.DeputiesTabButton.Location = new System.Drawing.Point(12, 338);
			this.DeputiesTabButton.Name = "DeputiesTabButton";
			this.DeputiesTabButton.Size = new System.Drawing.Size(195, 58);
			this.DeputiesTabButton.TabIndex = 1;
			this.DeputiesTabButton.Text = "   Депутати";
			this.DeputiesTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.DeputiesTabButton.UseVisualStyleBackColor = true;
			this.DeputiesTabButton.Click += new System.EventHandler(this.DeputiesTabButton_Click);
			// 
			// PlanarySessionsTabButton
			// 
			this.PlanarySessionsTabButton.FlatAppearance.BorderSize = 0;
			this.PlanarySessionsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.PlanarySessionsTabButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.PlanarySessionsTabButton.ForeColor = System.Drawing.Color.White;
			this.PlanarySessionsTabButton.Image = global::GovernmentParse.Properties.Resources.Calendar_25px;
			this.PlanarySessionsTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PlanarySessionsTabButton.Location = new System.Drawing.Point(12, 274);
			this.PlanarySessionsTabButton.Name = "PlanarySessionsTabButton";
			this.PlanarySessionsTabButton.Size = new System.Drawing.Size(195, 58);
			this.PlanarySessionsTabButton.TabIndex = 1;
			this.PlanarySessionsTabButton.Text = "   Пленарні \r\n   засідання";
			this.PlanarySessionsTabButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.PlanarySessionsTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.PlanarySessionsTabButton.UseVisualStyleBackColor = true;
			this.PlanarySessionsTabButton.Click += new System.EventHandler(this.PlanarySessionsTabButton_Click);
			// 
			// CommitteesTabButton
			// 
			this.CommitteesTabButton.FlatAppearance.BorderSize = 0;
			this.CommitteesTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CommitteesTabButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CommitteesTabButton.ForeColor = System.Drawing.Color.White;
			this.CommitteesTabButton.Image = global::GovernmentParse.Properties.Resources.Training_25px;
			this.CommitteesTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CommitteesTabButton.Location = new System.Drawing.Point(12, 210);
			this.CommitteesTabButton.Name = "CommitteesTabButton";
			this.CommitteesTabButton.Size = new System.Drawing.Size(195, 58);
			this.CommitteesTabButton.TabIndex = 1;
			this.CommitteesTabButton.Text = "   Комітети";
			this.CommitteesTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CommitteesTabButton.UseVisualStyleBackColor = true;
			this.CommitteesTabButton.Click += new System.EventHandler(this.CommitteesTabButton_Click);
			// 
			// LawsTabButton
			// 
			this.LawsTabButton.FlatAppearance.BorderSize = 0;
			this.LawsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LawsTabButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LawsTabButton.ForeColor = System.Drawing.Color.White;
			this.LawsTabButton.Image = global::GovernmentParse.Properties.Resources.News1111_100px;
			this.LawsTabButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LawsTabButton.Location = new System.Drawing.Point(12, 146);
			this.LawsTabButton.Name = "LawsTabButton";
			this.LawsTabButton.Size = new System.Drawing.Size(195, 58);
			this.LawsTabButton.TabIndex = 1;
			this.LawsTabButton.Text = "   Законопроекти";
			this.LawsTabButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.LawsTabButton.UseVisualStyleBackColor = true;
			this.LawsTabButton.Click += new System.EventHandler(this.LawsTabButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::GovernmentParse.Properties.Resources.logo_2014;
			this.pictureBox1.Location = new System.Drawing.Point(36, 48);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(135, 90);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// ReportPanel
			// 
			this.ReportPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ReportPanel.Controls.Add(this.ReportControl);
			this.ReportPanel.Location = new System.Drawing.Point(207, 39);
			this.ReportPanel.Name = "ReportPanel";
			this.ReportPanel.Size = new System.Drawing.Size(837, 579);
			this.ReportPanel.TabIndex = 15;
			// 
			// HeaderPanel
			// 
			this.HeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(40)))), ((int)(((byte)(122)))));
			this.HeaderPanel.Controls.Add(this.HeaderText);
			this.HeaderPanel.Controls.Add(this.CloseWindowBtn);
			this.HeaderPanel.Controls.Add(this.MaximizeWindowBtn);
			this.HeaderPanel.Controls.Add(this.MinimizeWindowBtn);
			this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
			this.HeaderPanel.Name = "HeaderPanel";
			this.HeaderPanel.Size = new System.Drawing.Size(1046, 39);
			this.HeaderPanel.TabIndex = 1;
			this.HeaderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			// 
			// HeaderText
			// 
			this.HeaderText.AutoSize = true;
			this.HeaderText.Location = new System.Drawing.Point(12, 9);
			this.HeaderText.Name = "HeaderText";
			this.HeaderText.Size = new System.Drawing.Size(146, 13);
			this.HeaderText.TabIndex = 2;
			this.HeaderText.Text = "Робота з законопроектами";
			// 
			// CloseWindowBtn
			// 
			this.CloseWindowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseWindowBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseWindowBtn.BackgroundImage")));
			this.CloseWindowBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.CloseWindowBtn.FlatAppearance.BorderSize = 0;
			this.CloseWindowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseWindowBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CloseWindowBtn.ForeColor = System.Drawing.Color.White;
			this.CloseWindowBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CloseWindowBtn.Location = new System.Drawing.Point(1008, 2);
			this.CloseWindowBtn.Name = "CloseWindowBtn";
			this.CloseWindowBtn.Size = new System.Drawing.Size(35, 35);
			this.CloseWindowBtn.TabIndex = 1;
			this.CloseWindowBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.CloseWindowBtn.UseVisualStyleBackColor = true;
			this.CloseWindowBtn.Click += new System.EventHandler(this.CloseWindowBtn_Click);
			// 
			// MaximizeWindowBtn
			// 
			this.MaximizeWindowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MaximizeWindowBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MaximizeWindowBtn.BackgroundImage")));
			this.MaximizeWindowBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.MaximizeWindowBtn.FlatAppearance.BorderSize = 0;
			this.MaximizeWindowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MaximizeWindowBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MaximizeWindowBtn.ForeColor = System.Drawing.Color.White;
			this.MaximizeWindowBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.MaximizeWindowBtn.Location = new System.Drawing.Point(967, 3);
			this.MaximizeWindowBtn.Name = "MaximizeWindowBtn";
			this.MaximizeWindowBtn.Size = new System.Drawing.Size(35, 35);
			this.MaximizeWindowBtn.TabIndex = 1;
			this.MaximizeWindowBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.MaximizeWindowBtn.UseVisualStyleBackColor = true;
			this.MaximizeWindowBtn.Click += new System.EventHandler(this.MaximizeWindowBtn_Click);
			// 
			// MinimizeWindowBtn
			// 
			this.MinimizeWindowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MinimizeWindowBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MinimizeWindowBtn.BackgroundImage")));
			this.MinimizeWindowBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.MinimizeWindowBtn.FlatAppearance.BorderSize = 0;
			this.MinimizeWindowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MinimizeWindowBtn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MinimizeWindowBtn.ForeColor = System.Drawing.Color.White;
			this.MinimizeWindowBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.MinimizeWindowBtn.Location = new System.Drawing.Point(926, 3);
			this.MinimizeWindowBtn.Name = "MinimizeWindowBtn";
			this.MinimizeWindowBtn.Size = new System.Drawing.Size(35, 35);
			this.MinimizeWindowBtn.TabIndex = 1;
			this.MinimizeWindowBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.MinimizeWindowBtn.UseVisualStyleBackColor = true;
			this.MinimizeWindowBtn.Click += new System.EventHandler(this.MinimizeWindowBtn_Click);
			// 
			// WorkAreaPanel
			// 
			this.WorkAreaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.WorkAreaPanel.Controls.Add(this.FractionsControl);
			this.WorkAreaPanel.Controls.Add(this.PlanarySessionsControl);
			this.WorkAreaPanel.Controls.Add(this.DeputiesControl);
			this.WorkAreaPanel.Controls.Add(this.CommitteesControl);
			this.WorkAreaPanel.Controls.Add(this.LawsControl);
			this.WorkAreaPanel.Location = new System.Drawing.Point(207, 39);
			this.WorkAreaPanel.Name = "WorkAreaPanel";
			this.WorkAreaPanel.Size = new System.Drawing.Size(837, 504);
			this.WorkAreaPanel.TabIndex = 2;
			// 
			// BottomPanel
			// 
			this.BottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BottomPanel.Controls.Add(this.tableLayoutPanelOuter);
			this.BottomPanel.Location = new System.Drawing.Point(207, 546);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(837, 70);
			this.BottomPanel.TabIndex = 13;
			// 
			// tableLayoutPanelOuter
			// 
			this.tableLayoutPanelOuter.ColumnCount = 4;
			this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanelOuter.Controls.Add(this.progressBarsPanel, 1, 1);
			this.tableLayoutPanelOuter.Controls.Add(this.CancelBtn, 2, 1);
			this.tableLayoutPanelOuter.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelOuter.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelOuter.Name = "tableLayoutPanelOuter";
			this.tableLayoutPanelOuter.RowCount = 2;
			this.tableLayoutPanelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanelOuter.Size = new System.Drawing.Size(837, 70);
			this.tableLayoutPanelOuter.TabIndex = 17;
			// 
			// progressBarsPanel
			// 
			this.progressBarsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBarsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progressBarsPanel.Controls.Add(this.tableLayoutPanelInsider);
			this.progressBarsPanel.Location = new System.Drawing.Point(39, 24);
			this.progressBarsPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.progressBarsPanel.Name = "progressBarsPanel";
			this.progressBarsPanel.Size = new System.Drawing.Size(599, 35);
			this.progressBarsPanel.TabIndex = 9;
			// 
			// tableLayoutPanelInsider
			// 
			this.tableLayoutPanelInsider.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanelInsider.ColumnCount = 3;
			this.tableLayoutPanelInsider.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
			this.tableLayoutPanelInsider.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
			this.tableLayoutPanelInsider.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
			this.tableLayoutPanelInsider.Controls.Add(this.SaveProgressBar, 2, 0);
			this.tableLayoutPanelInsider.Controls.Add(this.CompareProgressBar, 1, 0);
			this.tableLayoutPanelInsider.Controls.Add(this.SearchProgressBar, 0, 0);
			this.tableLayoutPanelInsider.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelInsider.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanelInsider.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelInsider.Name = "tableLayoutPanelInsider";
			this.tableLayoutPanelInsider.RowCount = 1;
			this.tableLayoutPanelInsider.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelInsider.Size = new System.Drawing.Size(597, 33);
			this.tableLayoutPanelInsider.TabIndex = 18;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.CancelBtn.Location = new System.Drawing.Point(647, 23);
			this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(148, 37);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Зупинити пошук";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanelOuter.SetColumnSpan(this.panel1, 4);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(831, 6);
			this.panel1.TabIndex = 3;
			// 
			// cursor
			// 
			this.cursor.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.cursor.BackColor = System.Drawing.Color.Transparent;
			this.cursor.BackgroundImage = global::GovernmentParse.Properties.Resources.treangle;
			this.cursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.cursor.Location = new System.Drawing.Point(670, 605);
			this.cursor.Name = "cursor";
			this.cursor.Size = new System.Drawing.Size(10, 10);
			this.cursor.TabIndex = 9;
			// 
			// FractionsControl
			// 
			this.FractionsControl.BackColor = System.Drawing.Color.White;
			this.FractionsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FractionsControl.Location = new System.Drawing.Point(0, 0);
			this.FractionsControl.Name = "FractionsControl";
			this.FractionsControl.Size = new System.Drawing.Size(837, 504);
			this.FractionsControl.TabIndex = 8;
			// 
			// PlanarySessionsControl
			// 
			this.PlanarySessionsControl.BackColor = System.Drawing.Color.White;
			this.PlanarySessionsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PlanarySessionsControl.Location = new System.Drawing.Point(0, 0);
			this.PlanarySessionsControl.Name = "PlanarySessionsControl";
			this.PlanarySessionsControl.Size = new System.Drawing.Size(837, 504);
			this.PlanarySessionsControl.TabIndex = 7;
			// 
			// DeputiesControl
			// 
			this.DeputiesControl.BackColor = System.Drawing.Color.White;
			this.DeputiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DeputiesControl.Location = new System.Drawing.Point(0, 0);
			this.DeputiesControl.Name = "DeputiesControl";
			this.DeputiesControl.Size = new System.Drawing.Size(837, 504);
			this.DeputiesControl.TabIndex = 6;
			// 
			// CommitteesControl
			// 
			this.CommitteesControl.BackColor = System.Drawing.Color.White;
			this.CommitteesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CommitteesControl.Location = new System.Drawing.Point(0, 0);
			this.CommitteesControl.Name = "CommitteesControl";
			this.CommitteesControl.Size = new System.Drawing.Size(837, 504);
			this.CommitteesControl.TabIndex = 5;
			// 
			// LawsControl
			// 
			this.LawsControl.BackColor = System.Drawing.Color.White;
			this.LawsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LawsControl.Location = new System.Drawing.Point(0, 0);
			this.LawsControl.Name = "LawsControl";
			this.LawsControl.Size = new System.Drawing.Size(837, 504);
			this.LawsControl.TabIndex = 0;
			// 
			// SaveProgressBar
			// 
			this.SaveProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SaveProgressBar.Location = new System.Drawing.Point(511, 0);
			this.SaveProgressBar.Margin = new System.Windows.Forms.Padding(0);
			this.SaveProgressBar.Maximum = 100;
			this.SaveProgressBar.Minimum = 0;
			this.SaveProgressBar.Name = "SaveProgressBar";
			this.SaveProgressBar.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(111)))), ((int)(((byte)(209)))));
			this.SaveProgressBar.Size = new System.Drawing.Size(86, 33);
			this.SaveProgressBar.TabIndex = 16;
			this.SaveProgressBar.Value = 0;
			// 
			// CompareProgressBar
			// 
			this.CompareProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CompareProgressBar.Location = new System.Drawing.Point(426, 0);
			this.CompareProgressBar.Margin = new System.Windows.Forms.Padding(0);
			this.CompareProgressBar.Maximum = 100;
			this.CompareProgressBar.Minimum = 0;
			this.CompareProgressBar.Name = "CompareProgressBar";
			this.CompareProgressBar.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(111)))), ((int)(((byte)(209)))));
			this.CompareProgressBar.Size = new System.Drawing.Size(85, 33);
			this.CompareProgressBar.TabIndex = 15;
			this.CompareProgressBar.Value = 0;
			// 
			// SearchProgressBar
			// 
			this.SearchProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SearchProgressBar.Location = new System.Drawing.Point(0, 0);
			this.SearchProgressBar.Margin = new System.Windows.Forms.Padding(0);
			this.SearchProgressBar.Maximum = 100;
			this.SearchProgressBar.Minimum = 0;
			this.SearchProgressBar.Name = "SearchProgressBar";
			this.SearchProgressBar.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(111)))), ((int)(((byte)(209)))));
			this.SearchProgressBar.Size = new System.Drawing.Size(426, 33);
			this.SearchProgressBar.TabIndex = 14;
			this.SearchProgressBar.Value = 0;
			// 
			// ReportControl
			// 
			this.ReportControl.BackColor = System.Drawing.Color.White;
			this.ReportControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReportControl.Location = new System.Drawing.Point(0, 0);
			this.ReportControl.Name = "ReportControl";
			this.ReportControl.Size = new System.Drawing.Size(837, 579);
			this.ReportControl.TabIndex = 1;
			// 
			// ModernForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1046, 617);
			this.Controls.Add(this.cursor);
			this.Controls.Add(this.WorkAreaPanel);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.ReportPanel);
			this.Controls.Add(this.HeaderPanel);
			this.Controls.Add(this.SidePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ModernForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Зберігання данних з сайту ВР";
			this.SidePanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ReportPanel.ResumeLayout(false);
			this.HeaderPanel.ResumeLayout(false);
			this.HeaderPanel.PerformLayout();
			this.WorkAreaPanel.ResumeLayout(false);
			this.BottomPanel.ResumeLayout(false);
			this.tableLayoutPanelOuter.ResumeLayout(false);
			this.progressBarsPanel.ResumeLayout(false);
			this.tableLayoutPanelInsider.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LawsTabButton;
        private System.Windows.Forms.Button FractionsTabButton;
        private System.Windows.Forms.Button DeputiesTabButton;
        private System.Windows.Forms.Button PlanarySessionsTabButton;
        private System.Windows.Forms.Button CommitteesTabButton;
        private System.Windows.Forms.Panel SidePanelSlider;
        private System.Windows.Forms.Panel WorkAreaPanel;
        private System.Windows.Forms.Button CloseWindowBtn;
        private System.Windows.Forms.Button MaximizeWindowBtn;
        private System.Windows.Forms.Button MinimizeWindowBtn;
        private System.Windows.Forms.Button ReportTabButton;
        private LawsControl LawsControl;
        private ReportControl ReportControl;
        private CommitteesControl CommitteesControl;
        private DeputiesControl DeputiesControl;
        private PlanarySessionsControl PlanarySessionsControl;
        private FractionsControl FractionsControl;
        private System.Windows.Forms.Label HeaderText;
        private System.Windows.Forms.ToolTip toolTipModernForm;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Panel ReportPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOuter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInsider;
        private System.Windows.Forms.Panel progressBarsPanel;
        private SmoothProgressBar SaveProgressBar;
        private SmoothProgressBar CompareProgressBar;
        private SmoothProgressBar SearchProgressBar;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel cursor;
    }
}