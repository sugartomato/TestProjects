namespace KK.CompressTools
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectCommandLine_WinRAR = new System.Windows.Forms.Button();
            this.btnSelectCommandLine_7Zip = new System.Windows.Forms.Button();
            this.txtWinRARCommandLine = new System.Windows.Forms.TextBox();
            this.txt7ZCommandLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtUseRAR = new System.Windows.Forms.RadioButton();
            this.rbtUse7Z = new System.Windows.Forms.RadioButton();
            this.rbtFileType_ZIP = new System.Windows.Forms.RadioButton();
            this.rbtFileType_RAR = new System.Windows.Forms.RadioButton();
            this.rbtFileType_7Z = new System.Windows.Forms.RadioButton();
            this.grpFileTypes = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsParentFolder = new System.Windows.Forms.CheckBox();
            this.chkAddDatetimeSufix = new System.Windows.Forms.CheckBox();
            this.chkSkipZipFiles = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.btnSelectTargetFolder = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.btnStopCompress = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpFileTypes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectCommandLine_WinRAR);
            this.groupBox1.Controls.Add(this.btnSelectCommandLine_7Zip);
            this.groupBox1.Controls.Add(this.txtWinRARCommandLine);
            this.groupBox1.Controls.Add(this.txt7ZCommandLine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "程序路径";
            // 
            // btnSelectCommandLine_WinRAR
            // 
            this.btnSelectCommandLine_WinRAR.Location = new System.Drawing.Point(436, 39);
            this.btnSelectCommandLine_WinRAR.Name = "btnSelectCommandLine_WinRAR";
            this.btnSelectCommandLine_WinRAR.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCommandLine_WinRAR.TabIndex = 3;
            this.btnSelectCommandLine_WinRAR.Text = "修改";
            this.btnSelectCommandLine_WinRAR.UseVisualStyleBackColor = true;
            // 
            // btnSelectCommandLine_7Zip
            // 
            this.btnSelectCommandLine_7Zip.Location = new System.Drawing.Point(436, 14);
            this.btnSelectCommandLine_7Zip.Name = "btnSelectCommandLine_7Zip";
            this.btnSelectCommandLine_7Zip.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCommandLine_7Zip.TabIndex = 3;
            this.btnSelectCommandLine_7Zip.Text = "修改";
            this.btnSelectCommandLine_7Zip.UseVisualStyleBackColor = true;
            // 
            // txtWinRARCommandLine
            // 
            this.txtWinRARCommandLine.Location = new System.Drawing.Point(53, 41);
            this.txtWinRARCommandLine.Name = "txtWinRARCommandLine";
            this.txtWinRARCommandLine.Size = new System.Drawing.Size(377, 21);
            this.txtWinRARCommandLine.TabIndex = 2;
            // 
            // txt7ZCommandLine
            // 
            this.txt7ZCommandLine.Location = new System.Drawing.Point(53, 16);
            this.txt7ZCommandLine.Name = "txt7ZCommandLine";
            this.txt7ZCommandLine.Size = new System.Drawing.Size(377, 21);
            this.txt7ZCommandLine.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "7Zip";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "WinRAR";
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.BackColor = System.Drawing.Color.LightGreen;
            this.btnExecute.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExecute.Location = new System.Drawing.Point(356, 368);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(100, 40);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "执行压缩";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtUseRAR);
            this.panel1.Controls.Add(this.rbtUse7Z);
            this.panel1.Location = new System.Drawing.Point(3, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 24);
            this.panel1.TabIndex = 2;
            // 
            // rbtUseRAR
            // 
            this.rbtUseRAR.AutoSize = true;
            this.rbtUseRAR.Location = new System.Drawing.Point(68, 3);
            this.rbtUseRAR.Name = "rbtUseRAR";
            this.rbtUseRAR.Size = new System.Drawing.Size(83, 16);
            this.rbtUseRAR.TabIndex = 1;
            this.rbtUseRAR.Text = "使用WinRAR";
            this.rbtUseRAR.UseVisualStyleBackColor = true;
            this.rbtUseRAR.CheckedChanged += new System.EventHandler(this.rbtUseRAR_CheckedChanged);
            // 
            // rbtUse7Z
            // 
            this.rbtUse7Z.AutoSize = true;
            this.rbtUse7Z.Checked = true;
            this.rbtUse7Z.Location = new System.Drawing.Point(3, 3);
            this.rbtUse7Z.Name = "rbtUse7Z";
            this.rbtUse7Z.Size = new System.Drawing.Size(59, 16);
            this.rbtUse7Z.TabIndex = 0;
            this.rbtUse7Z.TabStop = true;
            this.rbtUse7Z.Text = "使用7Z";
            this.rbtUse7Z.UseVisualStyleBackColor = true;
            // 
            // rbtFileType_ZIP
            // 
            this.rbtFileType_ZIP.AutoSize = true;
            this.rbtFileType_ZIP.Checked = true;
            this.rbtFileType_ZIP.Location = new System.Drawing.Point(6, 20);
            this.rbtFileType_ZIP.Name = "rbtFileType_ZIP";
            this.rbtFileType_ZIP.Size = new System.Drawing.Size(47, 16);
            this.rbtFileType_ZIP.TabIndex = 0;
            this.rbtFileType_ZIP.TabStop = true;
            this.rbtFileType_ZIP.Tag = "zip";
            this.rbtFileType_ZIP.Text = ".ZIP";
            this.rbtFileType_ZIP.UseVisualStyleBackColor = true;
            // 
            // rbtFileType_RAR
            // 
            this.rbtFileType_RAR.AutoSize = true;
            this.rbtFileType_RAR.Location = new System.Drawing.Point(59, 20);
            this.rbtFileType_RAR.Name = "rbtFileType_RAR";
            this.rbtFileType_RAR.Size = new System.Drawing.Size(47, 16);
            this.rbtFileType_RAR.TabIndex = 0;
            this.rbtFileType_RAR.Tag = "rar";
            this.rbtFileType_RAR.Text = ".RAR";
            this.rbtFileType_RAR.UseVisualStyleBackColor = true;
            // 
            // rbtFileType_7Z
            // 
            this.rbtFileType_7Z.AutoSize = true;
            this.rbtFileType_7Z.Location = new System.Drawing.Point(112, 20);
            this.rbtFileType_7Z.Name = "rbtFileType_7Z";
            this.rbtFileType_7Z.Size = new System.Drawing.Size(41, 16);
            this.rbtFileType_7Z.TabIndex = 0;
            this.rbtFileType_7Z.Tag = "7z";
            this.rbtFileType_7Z.Text = ".7Z";
            this.rbtFileType_7Z.UseVisualStyleBackColor = true;
            // 
            // grpFileTypes
            // 
            this.grpFileTypes.Controls.Add(this.rbtFileType_ZIP);
            this.grpFileTypes.Controls.Add(this.rbtFileType_RAR);
            this.grpFileTypes.Controls.Add(this.rbtFileType_7Z);
            this.grpFileTypes.Location = new System.Drawing.Point(3, 138);
            this.grpFileTypes.Name = "grpFileTypes";
            this.grpFileTypes.Size = new System.Drawing.Size(200, 46);
            this.grpFileTypes.TabIndex = 3;
            this.grpFileTypes.TabStop = false;
            this.grpFileTypes.Text = "文件格式";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.chkIsParentFolder);
            this.groupBox3.Controls.Add(this.chkAddDatetimeSufix);
            this.groupBox3.Controls.Add(this.chkSkipZipFiles);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Location = new System.Drawing.Point(3, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(518, 130);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.OrangeRed;
            this.label4.Location = new System.Drawing.Point(21, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(449, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "父目录模式下，目录下的文件和文件夹分别压缩，否则将选择的目录压缩未单文件。";
            // 
            // chkIsParentFolder
            // 
            this.chkIsParentFolder.AutoSize = true;
            this.chkIsParentFolder.Location = new System.Drawing.Point(6, 85);
            this.chkIsParentFolder.Name = "chkIsParentFolder";
            this.chkIsParentFolder.Size = new System.Drawing.Size(84, 16);
            this.chkIsParentFolder.TabIndex = 9;
            this.chkIsParentFolder.Text = "父目录模式";
            this.chkIsParentFolder.UseVisualStyleBackColor = true;
            // 
            // chkAddDatetimeSufix
            // 
            this.chkAddDatetimeSufix.AutoSize = true;
            this.chkAddDatetimeSufix.Checked = true;
            this.chkAddDatetimeSufix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddDatetimeSufix.Location = new System.Drawing.Point(6, 63);
            this.chkAddDatetimeSufix.Name = "chkAddDatetimeSufix";
            this.chkAddDatetimeSufix.Size = new System.Drawing.Size(96, 16);
            this.chkAddDatetimeSufix.TabIndex = 8;
            this.chkAddDatetimeSufix.Text = "添加日期后缀";
            this.chkAddDatetimeSufix.UseVisualStyleBackColor = true;
            // 
            // chkSkipZipFiles
            // 
            this.chkSkipZipFiles.AutoSize = true;
            this.chkSkipZipFiles.Checked = true;
            this.chkSkipZipFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkipZipFiles.Location = new System.Drawing.Point(6, 41);
            this.chkSkipZipFiles.Name = "chkSkipZipFiles";
            this.chkSkipZipFiles.Size = new System.Drawing.Size(96, 16);
            this.chkSkipZipFiles.TabIndex = 7;
            this.chkSkipZipFiles.Text = "跳过压缩文件";
            this.chkSkipZipFiles.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(53, 14);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(170, 21);
            this.txtPassword.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "密码：";
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Location = new System.Drawing.Point(3, 81);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(430, 21);
            this.txtTargetFolder.TabIndex = 5;
            // 
            // btnSelectTargetFolder
            // 
            this.btnSelectTargetFolder.Location = new System.Drawing.Point(439, 81);
            this.btnSelectTargetFolder.Name = "btnSelectTargetFolder";
            this.btnSelectTargetFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTargetFolder.TabIndex = 6;
            this.btnSelectTargetFolder.Text = "选择目录";
            this.btnSelectTargetFolder.UseVisualStyleBackColor = true;
            this.btnSelectTargetFolder.Click += new System.EventHandler(this.btnSelectTargetFolder_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtConsole.ForeColor = System.Drawing.Color.SpringGreen;
            this.txtConsole.Location = new System.Drawing.Point(568, 41);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(231, 366);
            this.txtConsole.TabIndex = 7;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.Black;
            this.btnClear.ForeColor = System.Drawing.Color.SpringGreen;
            this.btnClear.Location = new System.Drawing.Point(724, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 31);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 368);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(338, 39);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 9;
            // 
            // panelSetting
            // 
            this.panelSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSetting.Controls.Add(this.panel1);
            this.panelSetting.Controls.Add(this.grpFileTypes);
            this.panelSetting.Controls.Add(this.groupBox3);
            this.panelSetting.Controls.Add(this.txtTargetFolder);
            this.panelSetting.Controls.Add(this.btnSelectTargetFolder);
            this.panelSetting.Controls.Add(this.groupBox1);
            this.panelSetting.Location = new System.Drawing.Point(12, 12);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(542, 327);
            this.panelSetting.TabIndex = 10;
            // 
            // btnStopCompress
            // 
            this.btnStopCompress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopCompress.BackColor = System.Drawing.Color.LightCoral;
            this.btnStopCompress.Enabled = false;
            this.btnStopCompress.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStopCompress.Location = new System.Drawing.Point(462, 368);
            this.btnStopCompress.Name = "btnStopCompress";
            this.btnStopCompress.Size = new System.Drawing.Size(100, 40);
            this.btnStopCompress.TabIndex = 11;
            this.btnStopCompress.Text = "停止压缩";
            this.btnStopCompress.UseVisualStyleBackColor = false;
            this.btnStopCompress.Click += new System.EventHandler(this.btnStopCompress_Click);
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblState.AutoSize = true;
            this.lblState.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblState.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.ForeColor = System.Drawing.Color.Lime;
            this.lblState.Location = new System.Drawing.Point(12, 342);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(56, 22);
            this.lblState.TabIndex = 12;
            this.lblState.Text = "就绪";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 419);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnStopCompress);
            this.Controls.Add(this.panelSetting);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnExecute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "压缩辅助工具";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpFileTypes.ResumeLayout(false);
            this.grpFileTypes.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelSetting.ResumeLayout(false);
            this.panelSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtWinRARCommandLine;
        private System.Windows.Forms.TextBox txt7ZCommandLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectCommandLine_WinRAR;
        private System.Windows.Forms.Button btnSelectCommandLine_7Zip;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtUseRAR;
        private System.Windows.Forms.RadioButton rbtUse7Z;
        private System.Windows.Forms.RadioButton rbtFileType_ZIP;
        private System.Windows.Forms.RadioButton rbtFileType_RAR;
        private System.Windows.Forms.RadioButton rbtFileType_7Z;
        private System.Windows.Forms.GroupBox grpFileTypes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Button btnSelectTargetFolder;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkSkipZipFiles;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Button btnStopCompress;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.CheckBox chkAddDatetimeSufix;
        private System.Windows.Forms.CheckBox chkIsParentFolder;
        private System.Windows.Forms.Label label4;
    }
}

