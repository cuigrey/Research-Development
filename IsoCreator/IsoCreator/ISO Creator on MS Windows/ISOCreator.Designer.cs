namespace ImageMasterISOCreator
{
    partial class ISOCreatorMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxIsoPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowseIso = new System.Windows.Forms.Button();
            this.textBoxVolumeName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonStartAbort = new System.Windows.Forms.Button();
            this.btnOSCDImg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxIsoPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonBrowseIso);
            this.groupBox1.Controls.Add(this.textBoxVolumeName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 76);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ISO Details";
            // 
            // textBoxIsoPath
            // 
            this.textBoxIsoPath.AllowDrop = true;
            this.textBoxIsoPath.Location = new System.Drawing.Point(94, 21);
            this.textBoxIsoPath.Name = "textBoxIsoPath";
            this.textBoxIsoPath.Size = new System.Drawing.Size(399, 20);
            this.textBoxIsoPath.TabIndex = 8;
            this.textBoxIsoPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxIsoPath_DragDrop);
            this.textBoxIsoPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxIsoPath_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Volume Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ISO File Path";
            // 
            // buttonBrowseIso
            // 
            this.buttonBrowseIso.Location = new System.Drawing.Point(499, 19);
            this.buttonBrowseIso.Name = "buttonBrowseIso";
            this.buttonBrowseIso.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseIso.TabIndex = 5;
            this.buttonBrowseIso.Text = "Browse...";
            this.buttonBrowseIso.UseVisualStyleBackColor = true;
            this.buttonBrowseIso.Click += new System.EventHandler(this.buttonBrowseIso_Click);
            // 
            // textBoxVolumeName
            // 
            this.textBoxVolumeName.Location = new System.Drawing.Point(94, 47);
            this.textBoxVolumeName.Name = "textBoxVolumeName";
            this.textBoxVolumeName.Size = new System.Drawing.Size(278, 20);
            this.textBoxVolumeName.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonBrowseFolder);
            this.groupBox3.Controls.Add(this.textBoxFolder);
            this.groupBox3.Location = new System.Drawing.Point(12, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(580, 52);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folder Path";
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Location = new System.Drawing.Point(499, 20);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseFolder.TabIndex = 4;
            this.buttonBrowseFolder.Text = "Browse...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.AllowDrop = true;
            this.textBoxFolder.Location = new System.Drawing.Point(9, 21);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(484, 20);
            this.textBoxFolder.TabIndex = 2;
            this.textBoxFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFolder_DragDrop);
            this.textBoxFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFolder_DragEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelStatus);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 79);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Progress";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.labelStatus.Location = new System.Drawing.Point(479, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelStatus.Size = new System.Drawing.Size(98, 13);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "Process not started";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 54);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 18);
            this.progressBar.TabIndex = 14;
            // 
            // buttonStartAbort
            // 
            this.buttonStartAbort.Location = new System.Drawing.Point(517, 238);
            this.buttonStartAbort.Name = "buttonStartAbort";
            this.buttonStartAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonStartAbort.TabIndex = 17;
            this.buttonStartAbort.Text = "Start";
            this.buttonStartAbort.UseVisualStyleBackColor = true;
            this.buttonStartAbort.Click += new System.EventHandler(this.buttonStartAbort_Click);
            // 
            // btnOSCDImg
            // 
            this.btnOSCDImg.Location = new System.Drawing.Point(368, 237);
            this.btnOSCDImg.Name = "btnOSCDImg";
            this.btnOSCDImg.Size = new System.Drawing.Size(137, 23);
            this.btnOSCDImg.TabIndex = 18;
            this.btnOSCDImg.Text = "By MS OSCDIMG.exe";
            this.btnOSCDImg.UseVisualStyleBackColor = true;
            this.btnOSCDImg.Click += new System.EventHandler(this.btnOSCDImg_Click);
            // 
            // ISOCreatorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 273);
            this.Controls.Add(this.btnOSCDImg);
            this.Controls.Add(this.buttonStartAbort);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "ISOCreatorMain";
            this.Text = "ISO Creator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxIsoPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrowseIso;
        private System.Windows.Forms.TextBox textBoxVolumeName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonStartAbort;
        private System.Windows.Forms.Button btnOSCDImg;
    }
}

