namespace WebSnapAwesomium
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblURLs = new System.Windows.Forms.Label();
            this.txtUrls = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.lblBaseWidth = new System.Windows.Forms.Label();
            this.cmbBaseWidth = new System.Windows.Forms.ComboBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.chkFullPage = new System.Windows.Forms.CheckBox();
            this.lblSaveFolder = new System.Windows.Forms.Label();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblCommas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblURLs
            // 
            this.lblURLs.AutoSize = true;
            this.lblURLs.Location = new System.Drawing.Point(12, 15);
            this.lblURLs.Name = "lblURLs";
            this.lblURLs.Size = new System.Drawing.Size(35, 12);
            this.lblURLs.TabIndex = 0;
            this.lblURLs.Text = "URLs:";
            // 
            // txtUrls
            // 
            this.txtUrls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrls.Location = new System.Drawing.Point(89, 12);
            this.txtUrls.Multiline = true;
            this.txtUrls.Name = "txtUrls";
            this.txtUrls.Size = new System.Drawing.Size(426, 115);
            this.txtUrls.TabIndex = 1;
            this.txtUrls.Text = "http://www.baidu.com, ";
            // 
            // btnCapture
            // 
            this.btnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapture.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCapture.Location = new System.Drawing.Point(89, 204);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(426, 40);
            this.btnCapture.TabIndex = 13;
            this.btnCapture.Text = "Capture!";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // lblBaseWidth
            // 
            this.lblBaseWidth.AutoSize = true;
            this.lblBaseWidth.Location = new System.Drawing.Point(12, 142);
            this.lblBaseWidth.Name = "lblBaseWidth";
            this.lblBaseWidth.Size = new System.Drawing.Size(71, 12);
            this.lblBaseWidth.TabIndex = 3;
            this.lblBaseWidth.Text = "Base Width:";
            // 
            // cmbBaseWidth
            // 
            this.cmbBaseWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaseWidth.FormattingEnabled = true;
            this.cmbBaseWidth.Items.AddRange(new object[] {
            "2560",
            "2048",
            "1920",
            "1680",
            "1600",
            "1440",
            "1366",
            "1280",
            "1152",
            "1024",
            "800",
            "750",
            "640",
            "480"});
            this.cmbBaseWidth.Location = new System.Drawing.Point(89, 138);
            this.cmbBaseWidth.Name = "cmbBaseWidth";
            this.cmbBaseWidth.Size = new System.Drawing.Size(50, 20);
            this.cmbBaseWidth.TabIndex = 4;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(181, 142);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(41, 12);
            this.lblWidth.TabIndex = 5;
            this.lblWidth.Text = "Width:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(222, 138);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(46, 21);
            this.txtWidth.TabIndex = 6;
            this.txtWidth.Text = "480";
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(352, 138);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(46, 21);
            this.txtHeight.TabIndex = 8;
            this.txtHeight.Text = "320";
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(305, 142);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(47, 12);
            this.lblHeight.TabIndex = 7;
            this.lblHeight.Text = "Height:";
            // 
            // chkFullPage
            // 
            this.chkFullPage.AutoSize = true;
            this.chkFullPage.Location = new System.Drawing.Point(437, 140);
            this.chkFullPage.Name = "chkFullPage";
            this.chkFullPage.Size = new System.Drawing.Size(78, 16);
            this.chkFullPage.TabIndex = 9;
            this.chkFullPage.Text = "Full Page";
            this.chkFullPage.UseVisualStyleBackColor = true;
            // 
            // lblSaveFolder
            // 
            this.lblSaveFolder.AutoSize = true;
            this.lblSaveFolder.Location = new System.Drawing.Point(12, 174);
            this.lblSaveFolder.Name = "lblSaveFolder";
            this.lblSaveFolder.Size = new System.Drawing.Size(77, 12);
            this.lblSaveFolder.TabIndex = 10;
            this.lblSaveFolder.Text = "Save Folder:";
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.BackColor = System.Drawing.SystemColors.Window;
            this.txtSaveFolder.Location = new System.Drawing.Point(89, 170);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.ReadOnly = true;
            this.txtSaveFolder.Size = new System.Drawing.Size(345, 21);
            this.txtSaveFolder.TabIndex = 11;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(440, 169);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblCommas
            // 
            this.lblCommas.Location = new System.Drawing.Point(11, 34);
            this.lblCommas.Name = "lblCommas";
            this.lblCommas.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCommas.Size = new System.Drawing.Size(71, 40);
            this.lblCommas.TabIndex = 2;
            this.lblCommas.Text = "separate with       commas";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 256);
            this.Controls.Add(this.lblCommas);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtSaveFolder);
            this.Controls.Add(this.lblSaveFolder);
            this.Controls.Add(this.chkFullPage);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.cmbBaseWidth);
            this.Controls.Add(this.lblBaseWidth);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.txtUrls);
            this.Controls.Add(this.lblURLs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(543, 295);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(543, 295);
            this.Name = "frmMain";
            this.Text = "Webpage Snapshot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblURLs;
        private System.Windows.Forms.TextBox txtUrls;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label lblBaseWidth;
        private System.Windows.Forms.ComboBox cmbBaseWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.CheckBox chkFullPage;
        private System.Windows.Forms.Label lblSaveFolder;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblCommas;
    }
}

