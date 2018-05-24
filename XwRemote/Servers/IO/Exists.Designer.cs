namespace XwRemote.Servers
{
    partial class Exists
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
            this.dialogHeader1 = new XwMaxLib.UI.DialogHeader();
            this.overwriteFile = new System.Windows.Forms.Button();
            this.resumeFile = new System.Windows.Forms.Button();
            this.skipFile = new System.Windows.Forms.Button();
            this.overwriteAll = new System.Windows.Forms.Button();
            this.resumeAll = new System.Windows.Forms.Button();
            this.skipAll = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SourceIcon = new System.Windows.Forms.PictureBox();
            this.DestinationIcon = new System.Windows.Forms.PictureBox();
            this.SourceFileName = new System.Windows.Forms.Label();
            this.DestinationFileName = new System.Windows.Forms.Label();
            this.SourceFileDate = new System.Windows.Forms.Label();
            this.SourceFileSize = new System.Windows.Forms.Label();
            this.DestinationFileDate = new System.Windows.Forms.Label();
            this.DestinationFileSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SourceIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinationIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "Please choose the desired action";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.redhealp;
            this.dialogHeader1.HeaderTitle = "File already exists";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(545, 50);
            this.dialogHeader1.TabIndex = 1;
            // 
            // overwriteFile
            // 
            this.overwriteFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overwriteFile.ForeColor = System.Drawing.Color.Green;
            this.overwriteFile.Location = new System.Drawing.Point(288, 61);
            this.overwriteFile.Name = "overwriteFile";
            this.overwriteFile.Size = new System.Drawing.Size(245, 23);
            this.overwriteFile.TabIndex = 0;
            this.overwriteFile.Text = "Overwrite this file";
            this.overwriteFile.UseVisualStyleBackColor = true;
            this.overwriteFile.Click += new System.EventHandler(this.overwriteFile_Click);
            // 
            // resumeFile
            // 
            this.resumeFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumeFile.ForeColor = System.Drawing.Color.Green;
            this.resumeFile.Location = new System.Drawing.Point(288, 119);
            this.resumeFile.Name = "resumeFile";
            this.resumeFile.Size = new System.Drawing.Size(245, 23);
            this.resumeFile.TabIndex = 1;
            this.resumeFile.Text = "Resume this file";
            this.resumeFile.UseVisualStyleBackColor = true;
            this.resumeFile.Visible = false;
            this.resumeFile.Click += new System.EventHandler(this.resumeFile_Click);
            // 
            // skipFile
            // 
            this.skipFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skipFile.ForeColor = System.Drawing.Color.Green;
            this.skipFile.Location = new System.Drawing.Point(288, 90);
            this.skipFile.Name = "skipFile";
            this.skipFile.Size = new System.Drawing.Size(245, 23);
            this.skipFile.TabIndex = 2;
            this.skipFile.Text = "Skip this file";
            this.skipFile.UseVisualStyleBackColor = true;
            this.skipFile.Click += new System.EventHandler(this.skipFile_Click);
            // 
            // overwriteAll
            // 
            this.overwriteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overwriteAll.ForeColor = System.Drawing.Color.Maroon;
            this.overwriteAll.Location = new System.Drawing.Point(288, 166);
            this.overwriteAll.Name = "overwriteAll";
            this.overwriteAll.Size = new System.Drawing.Size(245, 23);
            this.overwriteAll.TabIndex = 3;
            this.overwriteAll.Text = "Overwrite all files";
            this.overwriteAll.UseVisualStyleBackColor = true;
            this.overwriteAll.Click += new System.EventHandler(this.overwriteAll_Click);
            // 
            // resumeAll
            // 
            this.resumeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumeAll.ForeColor = System.Drawing.Color.Maroon;
            this.resumeAll.Location = new System.Drawing.Point(288, 224);
            this.resumeAll.Name = "resumeAll";
            this.resumeAll.Size = new System.Drawing.Size(245, 23);
            this.resumeAll.TabIndex = 4;
            this.resumeAll.Text = "Resume all files";
            this.resumeAll.UseVisualStyleBackColor = true;
            this.resumeAll.Visible = false;
            this.resumeAll.Click += new System.EventHandler(this.resumeAll_Click);
            // 
            // skipAll
            // 
            this.skipAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skipAll.ForeColor = System.Drawing.Color.Maroon;
            this.skipAll.Location = new System.Drawing.Point(288, 195);
            this.skipAll.Name = "skipAll";
            this.skipAll.Size = new System.Drawing.Size(245, 23);
            this.skipAll.TabIndex = 5;
            this.skipAll.Text = "Skip all files";
            this.skipAll.UseVisualStyleBackColor = true;
            this.skipAll.Click += new System.EventHandler(this.skipAll_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(288, 274);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(245, 42);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Source file:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Destination file:";
            // 
            // SourceIcon
            // 
            this.SourceIcon.Location = new System.Drawing.Point(13, 90);
            this.SourceIcon.Name = "SourceIcon";
            this.SourceIcon.Size = new System.Drawing.Size(40, 37);
            this.SourceIcon.TabIndex = 8;
            this.SourceIcon.TabStop = false;
            // 
            // DestinationIcon
            // 
            this.DestinationIcon.Location = new System.Drawing.Point(12, 224);
            this.DestinationIcon.Name = "DestinationIcon";
            this.DestinationIcon.Size = new System.Drawing.Size(41, 37);
            this.DestinationIcon.TabIndex = 8;
            this.DestinationIcon.TabStop = false;
            // 
            // SourceFileName
            // 
            this.SourceFileName.Location = new System.Drawing.Point(58, 90);
            this.SourceFileName.Name = "SourceFileName";
            this.SourceFileName.Size = new System.Drawing.Size(224, 52);
            this.SourceFileName.TabIndex = 9;
            this.SourceFileName.Text = "source file name";
            // 
            // DestinationFileName
            // 
            this.DestinationFileName.Location = new System.Drawing.Point(57, 224);
            this.DestinationFileName.Name = "DestinationFileName";
            this.DestinationFileName.Size = new System.Drawing.Size(225, 52);
            this.DestinationFileName.TabIndex = 9;
            this.DestinationFileName.Text = "destination file name";
            // 
            // SourceFileDate
            // 
            this.SourceFileDate.AutoSize = true;
            this.SourceFileDate.Location = new System.Drawing.Point(57, 146);
            this.SourceFileDate.Name = "SourceFileDate";
            this.SourceFileDate.Size = new System.Drawing.Size(79, 13);
            this.SourceFileDate.TabIndex = 10;
            this.SourceFileDate.Text = "source file date";
            // 
            // SourceFileSize
            // 
            this.SourceFileSize.AutoSize = true;
            this.SourceFileSize.Location = new System.Drawing.Point(57, 165);
            this.SourceFileSize.Name = "SourceFileSize";
            this.SourceFileSize.Size = new System.Drawing.Size(76, 13);
            this.SourceFileSize.TabIndex = 10;
            this.SourceFileSize.Text = "source file size";
            // 
            // DestinationFileDate
            // 
            this.DestinationFileDate.AutoSize = true;
            this.DestinationFileDate.Location = new System.Drawing.Point(58, 282);
            this.DestinationFileDate.Name = "DestinationFileDate";
            this.DestinationFileDate.Size = new System.Drawing.Size(98, 13);
            this.DestinationFileDate.TabIndex = 10;
            this.DestinationFileDate.Text = "destination file date";
            // 
            // DestinationFileSize
            // 
            this.DestinationFileSize.AutoSize = true;
            this.DestinationFileSize.Location = new System.Drawing.Point(58, 301);
            this.DestinationFileSize.Name = "DestinationFileSize";
            this.DestinationFileSize.Size = new System.Drawing.Size(95, 13);
            this.DestinationFileSize.TabIndex = 10;
            this.DestinationFileSize.Text = "destination file size";
            // 
            // Exists
            // 
            this.AcceptButton = this.overwriteFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(545, 327);
            this.Controls.Add(this.DestinationFileSize);
            this.Controls.Add(this.SourceFileSize);
            this.Controls.Add(this.DestinationFileDate);
            this.Controls.Add(this.SourceFileDate);
            this.Controls.Add(this.DestinationFileName);
            this.Controls.Add(this.SourceFileName);
            this.Controls.Add(this.DestinationIcon);
            this.Controls.Add(this.SourceIcon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.skipAll);
            this.Controls.Add(this.resumeAll);
            this.Controls.Add(this.overwriteAll);
            this.Controls.Add(this.skipFile);
            this.Controls.Add(this.resumeFile);
            this.Controls.Add(this.overwriteFile);
            this.Controls.Add(this.dialogHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Exists";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File exists...";
            ((System.ComponentModel.ISupportInitialize)(this.SourceIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinationIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private System.Windows.Forms.Button overwriteFile;
        private System.Windows.Forms.Button resumeFile;
        private System.Windows.Forms.Button skipFile;
        private System.Windows.Forms.Button overwriteAll;
        private System.Windows.Forms.Button resumeAll;
        private System.Windows.Forms.Button skipAll;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox SourceIcon;
        public System.Windows.Forms.PictureBox DestinationIcon;
        public System.Windows.Forms.Label SourceFileName;
        public System.Windows.Forms.Label SourceFileDate;
        public System.Windows.Forms.Label SourceFileSize;
        public System.Windows.Forms.Label DestinationFileName;
        public System.Windows.Forms.Label DestinationFileDate;
        public System.Windows.Forms.Label DestinationFileSize;

    }
}