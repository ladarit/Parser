namespace GovernmentParse.Controls
{
    partial class FractionsControl
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
            this.EmptyPanelDownLeft = new System.Windows.Forms.Panel();
            this.EmptyPanelTopRight = new System.Windows.Forms.Panel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.LeftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.Slider = new System.Windows.Forms.Panel();
            this.SaveFractionsPanel = new System.Windows.Forms.Panel();
            this.SaveFractionsPictBox = new System.Windows.Forms.PictureBox();
            this.SaveFractionsLabel = new System.Windows.Forms.Label();
            this.SaveFractionsBtn = new System.Windows.Forms.Button();
            this.ConvocationFractionsComboBox = new MetroFramework.Controls.MetroComboBox();
            this.ConvocationFractionsLabel = new System.Windows.Forms.Label();
            this.RightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.EmptyPanelDownRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).BeginInit();
            this.LeftSplitContainer.Panel1.SuspendLayout();
            this.LeftSplitContainer.Panel2.SuspendLayout();
            this.LeftSplitContainer.SuspendLayout();
            this.SaveFractionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveFractionsPictBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).BeginInit();
            this.RightSplitContainer.Panel1.SuspendLayout();
            this.RightSplitContainer.Panel2.SuspendLayout();
            this.RightSplitContainer.SuspendLayout();
            this.SuspendLayout();
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
            this.EmptyPanelDownLeft.TabIndex = 25;
            this.EmptyPanelDownLeft.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // EmptyPanelTopRight
            // 
            this.EmptyPanelTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmptyPanelTopRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(249)))));
            this.EmptyPanelTopRight.Location = new System.Drawing.Point(0, 26);
            this.EmptyPanelTopRight.Name = "EmptyPanelTopRight";
            this.EmptyPanelTopRight.Size = new System.Drawing.Size(379, 217);
            this.EmptyPanelTopRight.TabIndex = 24;
            this.EmptyPanelTopRight.MouseEnter += new System.EventHandler(this.MoveSlider);
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
            this.MainSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.MainSplitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MainSplitContainer.Panel1.Controls.Add(this.LeftSplitContainer);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.MainSplitContainer.Panel2.Controls.Add(this.RightSplitContainer);
            this.MainSplitContainer.Size = new System.Drawing.Size(837, 486);
            this.MainSplitContainer.SplitterDistance = 418;
            this.MainSplitContainer.TabIndex = 28;
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
            this.LeftSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.LeftSplitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LeftSplitContainer.Panel1.Controls.Add(this.Slider);
            this.LeftSplitContainer.Panel1.Controls.Add(this.SaveFractionsPanel);
            // 
            // LeftSplitContainer.Panel2
            // 
            this.LeftSplitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.LeftSplitContainer.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
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
            this.Slider.TabIndex = 27;
            // 
            // SaveFractionsPanel
            // 
            this.SaveFractionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFractionsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveFractionsPanel.Controls.Add(this.SaveFractionsPictBox);
            this.SaveFractionsPanel.Controls.Add(this.SaveFractionsLabel);
            this.SaveFractionsPanel.Controls.Add(this.SaveFractionsBtn);
            this.SaveFractionsPanel.Controls.Add(this.ConvocationFractionsComboBox);
            this.SaveFractionsPanel.Controls.Add(this.ConvocationFractionsLabel);
            this.SaveFractionsPanel.Location = new System.Drawing.Point(39, 26);
            this.SaveFractionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SaveFractionsPanel.Name = "SaveFractionsPanel";
            this.SaveFractionsPanel.Size = new System.Drawing.Size(379, 217);
            this.SaveFractionsPanel.TabIndex = 24;
            this.SaveFractionsPanel.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // SaveFractionsPictBox
            // 
            this.SaveFractionsPictBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFractionsPictBox.Image = global::GovernmentParse.Properties.Resources.group_512;
            this.SaveFractionsPictBox.Location = new System.Drawing.Point(259, 0);
            this.SaveFractionsPictBox.Name = "SaveFractionsPictBox";
            this.SaveFractionsPictBox.Size = new System.Drawing.Size(120, 100);
            this.SaveFractionsPictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SaveFractionsPictBox.TabIndex = 8;
            this.SaveFractionsPictBox.TabStop = false;
            // 
            // SaveFractionsLabel
            // 
            this.SaveFractionsLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveFractionsLabel.ForeColor = System.Drawing.Color.Black;
            this.SaveFractionsLabel.Location = new System.Drawing.Point(12, 11);
            this.SaveFractionsLabel.Name = "SaveFractionsLabel";
            this.SaveFractionsLabel.Size = new System.Drawing.Size(234, 58);
            this.SaveFractionsLabel.TabIndex = 6;
            this.SaveFractionsLabel.Text = "Зберегти (чи оновити) депутатські фракції і групи";
            // 
            // SaveFractionsBtn
            // 
            this.SaveFractionsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFractionsBtn.AutoSize = true;
            this.SaveFractionsBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveFractionsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(197)))), ((int)(((byte)(242)))));
            this.SaveFractionsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveFractionsBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.SaveFractionsBtn.FlatAppearance.BorderSize = 0;
            this.SaveFractionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveFractionsBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.SaveFractionsBtn.ForeColor = System.Drawing.Color.Black;
            this.SaveFractionsBtn.Location = new System.Drawing.Point(244, 174);
            this.SaveFractionsBtn.Name = "SaveFractionsBtn";
            this.SaveFractionsBtn.Size = new System.Drawing.Size(122, 31);
            this.SaveFractionsBtn.TabIndex = 6;
            this.SaveFractionsBtn.Text = "Завантажити";
            this.SaveFractionsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveFractionsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SaveFractionsBtn.UseVisualStyleBackColor = false;
            this.SaveFractionsBtn.Click += new System.EventHandler(this.SaveFractionsBtn_Click);
            // 
            // ConvocationFractionsComboBox
            // 
            this.ConvocationFractionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConvocationFractionsComboBox.ForeColor = System.Drawing.Color.Black;
            this.ConvocationFractionsComboBox.FormattingEnabled = true;
            this.ConvocationFractionsComboBox.ItemHeight = 23;
            this.ConvocationFractionsComboBox.Location = new System.Drawing.Point(16, 176);
            this.ConvocationFractionsComboBox.Name = "ConvocationFractionsComboBox";
            this.ConvocationFractionsComboBox.Size = new System.Drawing.Size(160, 29);
            this.ConvocationFractionsComboBox.TabIndex = 0;
            this.ConvocationFractionsComboBox.UseCustomBackColor = true;
            this.ConvocationFractionsComboBox.UseCustomForeColor = true;
            this.ConvocationFractionsComboBox.UseSelectable = true;
            this.ConvocationFractionsComboBox.UseStyleColors = true;
            // 
            // ConvocationFractionsLabel
            // 
            this.ConvocationFractionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConvocationFractionsLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConvocationFractionsLabel.Location = new System.Drawing.Point(15, 144);
            this.ConvocationFractionsLabel.Name = "ConvocationFractionsLabel";
            this.ConvocationFractionsLabel.Size = new System.Drawing.Size(186, 20);
            this.ConvocationFractionsLabel.TabIndex = 11;
            this.ConvocationFractionsLabel.Text = "пошук за скликанням";
            this.ConvocationFractionsLabel.UseCompatibleTextRendering = true;
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
            this.RightSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.RightSplitContainer.Panel1.Controls.Add(this.EmptyPanelTopRight);
            // 
            // RightSplitContainer.Panel2
            // 
            this.RightSplitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.RightSplitContainer.Panel2.Controls.Add(this.EmptyPanelDownRight);
            this.RightSplitContainer.Size = new System.Drawing.Size(415, 486);
            this.RightSplitContainer.SplitterDistance = 243;
            this.RightSplitContainer.TabIndex = 0;
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
            this.EmptyPanelDownRight.TabIndex = 26;
            this.EmptyPanelDownRight.MouseEnter += new System.EventHandler(this.MoveSlider);
            // 
            // FractionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "FractionsControl";
            this.Size = new System.Drawing.Size(837, 486);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.LeftSplitContainer.Panel1.ResumeLayout(false);
            this.LeftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftSplitContainer)).EndInit();
            this.LeftSplitContainer.ResumeLayout(false);
            this.SaveFractionsPanel.ResumeLayout(false);
            this.SaveFractionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveFractionsPictBox)).EndInit();
            this.RightSplitContainer.Panel1.ResumeLayout(false);
            this.RightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RightSplitContainer)).EndInit();
            this.RightSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel EmptyPanelDownLeft;
        private System.Windows.Forms.Panel EmptyPanelTopRight;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.SplitContainer LeftSplitContainer;
        private System.Windows.Forms.SplitContainer RightSplitContainer;
        private System.Windows.Forms.Panel SaveFractionsPanel;
        private MetroFramework.Controls.MetroComboBox ConvocationFractionsComboBox;
        private System.Windows.Forms.Label ConvocationFractionsLabel;
        private System.Windows.Forms.Button SaveFractionsBtn;
        private System.Windows.Forms.Panel EmptyPanelDownRight;
        private System.Windows.Forms.Panel Slider;
        private System.Windows.Forms.Label SaveFractionsLabel;
        private System.Windows.Forms.PictureBox SaveFractionsPictBox;
    }
}
