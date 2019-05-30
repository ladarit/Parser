namespace GovernmentParse.Controls
{
    partial class ReportControl
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
            this.TextBoxInformant = new System.Windows.Forms.RichTextBox();
            this.SaveReportBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBoxInformant
            // 
            this.TextBoxInformant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxInformant.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextBoxInformant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxInformant.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxInformant.Location = new System.Drawing.Point(36, 31);
            this.TextBoxInformant.Name = "TextBoxInformant";
            this.TextBoxInformant.Size = new System.Drawing.Size(780, 453);
            this.TextBoxInformant.TabIndex = 6;
            this.TextBoxInformant.Text = "";
            // 
            // SaveReportBtn
            // 
            this.SaveReportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveReportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveReportBtn.Location = new System.Drawing.Point(634, 525);
            this.SaveReportBtn.Name = "SaveReportBtn";
            this.SaveReportBtn.Size = new System.Drawing.Size(182, 36);
            this.SaveReportBtn.TabIndex = 10;
            this.SaveReportBtn.Text = "Зберегти звіт";
            this.SaveReportBtn.UseVisualStyleBackColor = true;
            this.SaveReportBtn.Click += new System.EventHandler(this.SaveReportBtn_Click);
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.SaveReportBtn);
            this.Controls.Add(this.TextBoxInformant);
            this.Name = "ReportControl";
            this.Size = new System.Drawing.Size(837, 579);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextBoxInformant;
        private System.Windows.Forms.Button SaveReportBtn;
    }
}
