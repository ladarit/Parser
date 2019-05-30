namespace GovernmentParse.Controls
{
    partial class PlanarySessionsControl
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
            this.MainSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            this.LeftSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            this.Slider = new System.Windows.Forms.Panel();
            this.SaveSessionsPlanPanel = new System.Windows.Forms.Panel();
            this.SaveSessionsPlanPictBox = new System.Windows.Forms.PictureBox();
            this.SavePlenarySessionCalendarPlanLabel = new System.Windows.Forms.Label();
            this.SavePlenarySessionCalendarPlanBtn = new System.Windows.Forms.Button();
            this.EmptyPanelDownLeft = new System.Windows.Forms.Panel();
            this.RightSplitContainer = new GovernmentParse.Controls.NonFlickerSplitContainer();
            this.SaveSessionsDatesPanel = new System.Windows.Forms.Panel();
            this.SaveSessionsDatesPictBox = new System.Windows.Forms.PictureBox();
            this.SavePlenarySessionDatesBtn = new System.Windows.Forms.Button();
            this.SavePlenarySessionDatesLabel = new System.Windows.Forms.Label();
            this.EmptyPanelDownRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel1.SuspendLayout();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            this.SaveSessionsPlanPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveSessionsPlanPictBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.Panel2.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            this.SaveSessionsDatesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveSessionsDatesPictBox)).BeginInit();
            this.SuspendLayout();
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
            this.MainSplitContainer.TabIndex = 29;
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
            this.LeftSplitContainer.Panel1.Controls.Add(this.SaveSessionsPlanPanel);
            // 
            // LeftSplitContainer.Panel2
            // 
            this.LeftSplitContainer.Panel2.Controls.Add(this.EmptyPanelDownLeft);
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
            this.Slider.TabIndex = 30;
            // 
            // SaveSessionsPlanPanel
            // 
            this.SaveSessionsPlanPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveSessionsPlanPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveSessionsPlanPanel.Controls.Add(this.SaveSessionsPlanPictBox);
            this.SaveSessionsPlanPanel.Controls.Add(this.SavePlenarySessionCalendarPlanLabel);
            this.SaveSessionsPlanPanel.Controls.Add(this.SavePlenarySessionCalendarPlanBtn);
            this.SaveSessionsPlanPanel.Location = new System.Drawing.Point(39, 26);
            this.SaveSessionsPlanPanel.Name = "SaveSessionsPlanPanel";
            this.SaveSessionsPlanPanel.Size = new System.Drawing.Size(379, 217);
            this.SaveSessionsPlanPanel.TabIndex = 29;
            this.SaveSessionsPlanPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SaveSessionsPlanPictBox
            // 
            this.SaveSessionsPlanPictBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveSessionsPlanPictBox.Image = global::GovernmentParse.Properties.Resources.Brief_512;
            this.SaveSessionsPlanPictBox.Location = new System.Drawing.Point(259, 2);
            this.SaveSessionsPlanPictBox.Name = "SaveSessionsPlanPictBox";
            this.SaveSessionsPlanPictBox.Size = new System.Drawing.Size(120, 100);
            this.SaveSessionsPlanPictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveSessionsPlanPictBox.TabIndex = 7;
            this.SaveSessionsPlanPictBox.TabStop = false;
            // 
            // SavePlenarySessionCalendarPlanLabel
            // 
            this.SavePlenarySessionCalendarPlanLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SavePlenarySessionCalendarPlanLabel.ForeColor = System.Drawing.Color.Black;
            this.SavePlenarySessionCalendarPlanLabel.Location = new System.Drawing.Point(12, 11);
            this.SavePlenarySessionCalendarPlanLabel.Name = "SavePlenarySessionCalendarPlanLabel";
            this.SavePlenarySessionCalendarPlanLabel.Size = new System.Drawing.Size(249, 97);
            this.SavePlenarySessionCalendarPlanLabel.TabIndex = 5;
            this.SavePlenarySessionCalendarPlanLabel.Text = "Зберегти плани проведення всіх пленарних засідань Верховної Ради (за поточну сесс" +
    "ію)\r\n";
            // 
            // SavePlenarySessionCalendarPlanBtn
            // 
            this.SavePlenarySessionCalendarPlanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePlenarySessionCalendarPlanBtn.AutoSize = true;
            this.SavePlenarySessionCalendarPlanBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SavePlenarySessionCalendarPlanBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SavePlenarySessionCalendarPlanBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SavePlenarySessionCalendarPlanBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.SavePlenarySessionCalendarPlanBtn.FlatAppearance.BorderSize = 0;
            this.SavePlenarySessionCalendarPlanBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SavePlenarySessionCalendarPlanBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SavePlenarySessionCalendarPlanBtn.ForeColor = System.Drawing.Color.Black;
            this.SavePlenarySessionCalendarPlanBtn.Location = new System.Drawing.Point(244, 174);
            this.SavePlenarySessionCalendarPlanBtn.Name = "SavePlenarySessionCalendarPlanBtn";
            this.SavePlenarySessionCalendarPlanBtn.Size = new System.Drawing.Size(122, 31);
            this.SavePlenarySessionCalendarPlanBtn.TabIndex = 6;
            this.SavePlenarySessionCalendarPlanBtn.Text = "Завантажити";
            this.SavePlenarySessionCalendarPlanBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SavePlenarySessionCalendarPlanBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SavePlenarySessionCalendarPlanBtn.UseVisualStyleBackColor = false;
            this.SavePlenarySessionCalendarPlanBtn.Click += new System.EventHandler(this.SavePlenarySessionCalendarPlanBtn_Click);
            // 
            // EmptyPanelDownLeft
            // 
            this.EmptyPanelDownLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmptyPanelDownLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(249)))));
            this.EmptyPanelDownLeft.Location = new System.Drawing.Point(39, 0);
            this.EmptyPanelDownLeft.Name = "EmptyPanelDownLeft";
            this.EmptyPanelDownLeft.Size = new System.Drawing.Size(379, 217);
            this.EmptyPanelDownLeft.TabIndex = 20;
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
            this.RightSplitContainer.Panel1.Controls.Add(this.SaveSessionsDatesPanel);
            // 
            // RightSplitContainer.Panel2
            // 
            this.RightSplitContainer.Panel2.Controls.Add(this.EmptyPanelDownRight);
            this.RightSplitContainer.Size = new System.Drawing.Size(415, 486);
            this.RightSplitContainer.SplitterDistance = 243;
            this.RightSplitContainer.TabIndex = 0;
            // 
            // SaveSessionsDatesPanel
            // 
            this.SaveSessionsDatesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveSessionsDatesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveSessionsDatesPanel.Controls.Add(this.SaveSessionsDatesPictBox);
            this.SaveSessionsDatesPanel.Controls.Add(this.SavePlenarySessionDatesBtn);
            this.SaveSessionsDatesPanel.Controls.Add(this.SavePlenarySessionDatesLabel);
            this.SaveSessionsDatesPanel.Location = new System.Drawing.Point(0, 26);
            this.SaveSessionsDatesPanel.Name = "SaveSessionsDatesPanel";
            this.SaveSessionsDatesPanel.Size = new System.Drawing.Size(379, 217);
            this.SaveSessionsDatesPanel.TabIndex = 17;
            this.SaveSessionsDatesPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SaveSessionsDatesPictBox
            // 
            this.SaveSessionsDatesPictBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveSessionsDatesPictBox.Image = global::GovernmentParse.Properties.Resources.calendar;
            this.SaveSessionsDatesPictBox.Location = new System.Drawing.Point(259, 2);
            this.SaveSessionsDatesPictBox.Name = "SaveSessionsDatesPictBox";
            this.SaveSessionsDatesPictBox.Size = new System.Drawing.Size(120, 100);
            this.SaveSessionsDatesPictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveSessionsDatesPictBox.TabIndex = 7;
            this.SaveSessionsDatesPictBox.TabStop = false;
            // 
            // SavePlenarySessionDatesBtn
            // 
            this.SavePlenarySessionDatesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePlenarySessionDatesBtn.AutoSize = true;
            this.SavePlenarySessionDatesBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SavePlenarySessionDatesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SavePlenarySessionDatesBtn.FlatAppearance.BorderSize = 0;
            this.SavePlenarySessionDatesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SavePlenarySessionDatesBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SavePlenarySessionDatesBtn.ForeColor = System.Drawing.Color.Black;
            this.SavePlenarySessionDatesBtn.Location = new System.Drawing.Point(244, 174);
            this.SavePlenarySessionDatesBtn.Name = "SavePlenarySessionDatesBtn";
            this.SavePlenarySessionDatesBtn.Size = new System.Drawing.Size(122, 31);
            this.SavePlenarySessionDatesBtn.TabIndex = 9;
            this.SavePlenarySessionDatesBtn.Text = "Завантажити";
            this.SavePlenarySessionDatesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SavePlenarySessionDatesBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SavePlenarySessionDatesBtn.UseVisualStyleBackColor = false;
            this.SavePlenarySessionDatesBtn.Click += new System.EventHandler(this.SavePlenarySessionDatesBtn_Click);
            // 
            // SavePlenarySessionDatesLabel
            // 
            this.SavePlenarySessionDatesLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SavePlenarySessionDatesLabel.ForeColor = System.Drawing.Color.Black;
            this.SavePlenarySessionDatesLabel.Location = new System.Drawing.Point(12, 11);
            this.SavePlenarySessionDatesLabel.Name = "SavePlenarySessionDatesLabel";
            this.SavePlenarySessionDatesLabel.Size = new System.Drawing.Size(246, 66);
            this.SavePlenarySessionDatesLabel.TabIndex = 8;
            this.SavePlenarySessionDatesLabel.Text = "Зберегти графік проведень пленарних засідань Верховної Ради";
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
            // PlanarySessionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "PlanarySessionsControl";
            this.Size = new System.Drawing.Size(837, 486);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.LeftSplitContainer.Panel1.ResumeLayout(false);
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).EndInit();
            this.LeftSplitContainer.ResumeLayout(false);
            this.SaveSessionsPlanPanel.ResumeLayout(false);
            this.SaveSessionsPlanPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveSessionsPlanPictBox)).EndInit();
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            this.RightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).EndInit();
            this.RightSplitContainer.ResumeLayout(false);
            this.SaveSessionsDatesPanel.ResumeLayout(false);
            this.SaveSessionsDatesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveSessionsDatesPictBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private NonFlickerSplitContainer MainSplitContainer;
        private NonFlickerSplitContainer RightSplitContainer;
        private NonFlickerSplitContainer LeftSplitContainer;
        private System.Windows.Forms.Panel Slider;
        private System.Windows.Forms.Panel SaveSessionsPlanPanel;
        private System.Windows.Forms.PictureBox SaveSessionsPlanPictBox;
        private System.Windows.Forms.Label SavePlenarySessionCalendarPlanLabel;
        private System.Windows.Forms.Button SavePlenarySessionCalendarPlanBtn;
        private System.Windows.Forms.Panel EmptyPanelDownLeft;
        private System.Windows.Forms.Panel SaveSessionsDatesPanel;
        private System.Windows.Forms.PictureBox SaveSessionsDatesPictBox;
        private System.Windows.Forms.Button SavePlenarySessionDatesBtn;
        private System.Windows.Forms.Label SavePlenarySessionDatesLabel;
        private System.Windows.Forms.Panel EmptyPanelDownRight;
    }
}
