namespace XwRemote.Settings
{
    partial class IOSettings
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
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.IsFavorite = new System.Windows.Forms.CheckBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.HostLabel = new System.Windows.Forms.Label();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.UserBox = new System.Windows.Forms.TextBox();
            this.HostBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.DefaultPort = new System.Windows.Forms.CheckBox();
            this.tabColorBox = new ColorComboTestApp.ColorComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NotesBox = new System.Windows.Forms.TextBox();
            this.buttonOpenSshKey = new System.Windows.Forms.Button();
            this.SshKeyLabel = new System.Windows.Forms.Label();
            this.SshKeyBox = new System.Windows.Forms.TextBox();
            this.buttonShowPassword = new System.Windows.Forms.Button();
            this.FtpDataType = new System.Windows.Forms.ComboBox();
            this.FtpDataTypeLabel = new System.Windows.Forms.Label();
            this.UseTLS = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.NumericUpDown();
            this.dividerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "Connecting settings for FTP server";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.ftp;
            this.dialogHeader1.HeaderTitle = "FTP";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(395, 50);
            this.dialogHeader1.TabIndex = 7;
            this.dialogHeader1.TabStop = false;
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.butCancel);
            this.dividerPanel1.Controls.Add(this.butOK);
            this.dividerPanel1.Controls.Add(this.IsFavorite);
            this.dividerPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dividerPanel1.Location = new System.Drawing.Point(0, 461);
            this.dividerPanel1.Name = "dividerPanel1";
            this.dividerPanel1.Size = new System.Drawing.Size(395, 45);
            this.dividerPanel1.TabIndex = 13;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(308, 10);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 2;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(227, 10);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 1;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // IsFavorite
            // 
            this.IsFavorite.AutoSize = true;
            this.IsFavorite.Location = new System.Drawing.Point(12, 14);
            this.IsFavorite.Name = "IsFavorite";
            this.IsFavorite.Size = new System.Drawing.Size(143, 17);
            this.IsFavorite.TabIndex = 0;
            this.IsFavorite.Text = "Place on Favorites menu";
            this.IsFavorite.UseVisualStyleBackColor = true;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(6, 138);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 11;
            this.PasswordLabel.Text = "Password";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(6, 112);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 10;
            this.UsernameLabel.Text = "Username";
            // 
            // HostLabel
            // 
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(8, 86);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(29, 13);
            this.HostLabel.TabIndex = 9;
            this.HostLabel.Text = "Host";
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(66, 135);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(294, 20);
            this.PassBox.TabIndex = 3;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassBox_KeyDown);
            // 
            // UserBox
            // 
            this.UserBox.Location = new System.Drawing.Point(66, 109);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(317, 20);
            this.UserBox.TabIndex = 2;
            // 
            // HostBox
            // 
            this.HostBox.Location = new System.Drawing.Point(66, 83);
            this.HostBox.Name = "HostBox";
            this.HostBox.Size = new System.Drawing.Size(317, 20);
            this.HostBox.TabIndex = 1;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(66, 56);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(317, 20);
            this.NameBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(7, 165);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 12;
            this.PortLabel.Text = "Port";
            // 
            // DefaultPort
            // 
            this.DefaultPort.AutoSize = true;
            this.DefaultPort.Checked = true;
            this.DefaultPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefaultPort.Location = new System.Drawing.Point(66, 164);
            this.DefaultPort.Name = "DefaultPort";
            this.DefaultPort.Size = new System.Drawing.Size(60, 17);
            this.DefaultPort.TabIndex = 4;
            this.DefaultPort.Text = "Default";
            this.DefaultPort.UseVisualStyleBackColor = true;
            this.DefaultPort.CheckedChanged += new System.EventHandler(this.DefaultPort_CheckedChanged);
            // 
            // tabColorBox
            // 
            this.tabColorBox.Extended = true;
            this.tabColorBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabColorBox.Location = new System.Drawing.Point(64, 403);
            this.tabColorBox.Name = "tabColorBox";
            this.tabColorBox.SelectedColor = System.Drawing.Color.LightSkyBlue;
            this.tabColorBox.Size = new System.Drawing.Size(70, 23);
            this.tabColorBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tab color";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 338);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Notes";
            // 
            // NotesBox
            // 
            this.NotesBox.AcceptsReturn = true;
            this.NotesBox.Location = new System.Drawing.Point(66, 338);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(317, 59);
            this.NotesBox.TabIndex = 9;
            // 
            // buttonOpenSshKey
            // 
            this.buttonOpenSshKey.Location = new System.Drawing.Point(16, 270);
            this.buttonOpenSshKey.Name = "buttonOpenSshKey";
            this.buttonOpenSshKey.Size = new System.Drawing.Size(26, 23);
            this.buttonOpenSshKey.TabIndex = 37;
            this.buttonOpenSshKey.Text = "...";
            this.buttonOpenSshKey.UseVisualStyleBackColor = true;
            this.buttonOpenSshKey.Visible = false;
            this.buttonOpenSshKey.Click += new System.EventHandler(this.buttonOpenSshKey_Click);
            // 
            // SshKeyLabel
            // 
            this.SshKeyLabel.AutoSize = true;
            this.SshKeyLabel.Location = new System.Drawing.Point(7, 241);
            this.SshKeyLabel.Name = "SshKeyLabel";
            this.SshKeyLabel.Size = new System.Drawing.Size(43, 13);
            this.SshKeyLabel.TabIndex = 36;
            this.SshKeyLabel.Text = "SshKey";
            this.SshKeyLabel.Visible = false;
            // 
            // SshKeyBox
            // 
            this.SshKeyBox.AcceptsReturn = true;
            this.SshKeyBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SshKeyBox.Location = new System.Drawing.Point(65, 242);
            this.SshKeyBox.Multiline = true;
            this.SshKeyBox.Name = "SshKeyBox";
            this.SshKeyBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SshKeyBox.Size = new System.Drawing.Size(317, 90);
            this.SshKeyBox.TabIndex = 8;
            this.SshKeyBox.Visible = false;
            this.SshKeyBox.WordWrap = false;
            // 
            // buttonShowPassword
            // 
            this.buttonShowPassword.Image = global::XwRemote.Properties.Resources.eye;
            this.buttonShowPassword.Location = new System.Drawing.Point(360, 134);
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.Size = new System.Drawing.Size(23, 22);
            this.buttonShowPassword.TabIndex = 38;
            this.buttonShowPassword.UseVisualStyleBackColor = true;
            this.buttonShowPassword.Click += new System.EventHandler(this.buttonShowPassword_Click);
            // 
            // FtpDataType
            // 
            this.FtpDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FtpDataType.FormattingEnabled = true;
            this.FtpDataType.Location = new System.Drawing.Point(132, 188);
            this.FtpDataType.Name = "FtpDataType";
            this.FtpDataType.Size = new System.Drawing.Size(250, 21);
            this.FtpDataType.TabIndex = 6;
            // 
            // FtpDataTypeLabel
            // 
            this.FtpDataTypeLabel.AutoSize = true;
            this.FtpDataTypeLabel.Location = new System.Drawing.Point(7, 191);
            this.FtpDataTypeLabel.Name = "FtpDataTypeLabel";
            this.FtpDataTypeLabel.Size = new System.Drawing.Size(88, 13);
            this.FtpDataTypeLabel.TabIndex = 40;
            this.FtpDataTypeLabel.Text = "Connection Type";
            // 
            // UseTLS
            // 
            this.UseTLS.AutoSize = true;
            this.UseTLS.Checked = true;
            this.UseTLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseTLS.Location = new System.Drawing.Point(64, 219);
            this.UseTLS.Name = "UseTLS";
            this.UseTLS.Size = new System.Drawing.Size(164, 17);
            this.UseTLS.TabIndex = 7;
            this.UseTLS.Text = "Use TLS to connect to FTPS";
            this.UseTLS.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "TLS";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(132, 161);
            this.PortBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(251, 20);
            this.PortBox.TabIndex = 5;
            this.PortBox.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // IOSettings
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(395, 506);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.UseTLS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FtpDataTypeLabel);
            this.Controls.Add(this.FtpDataType);
            this.Controls.Add(this.buttonShowPassword);
            this.Controls.Add(this.buttonOpenSshKey);
            this.Controls.Add(this.SshKeyLabel);
            this.Controls.Add(this.SshKeyBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.NotesBox);
            this.Controls.Add(this.tabColorBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DefaultPort);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.HostLabel);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.HostBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.dialogHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IOSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FTPSettings";
            this.Load += new System.EventHandler(this.OnLoad);
            this.dividerPanel1.ResumeLayout(false);
            this.dividerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private DividerPanel.DividerPanel dividerPanel1;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.CheckBox IsFavorite;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.TextBox UserBox;
        private System.Windows.Forms.TextBox HostBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.CheckBox DefaultPort;
        private ColorComboTestApp.ColorComboBox tabColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Button buttonOpenSshKey;
        private System.Windows.Forms.Label SshKeyLabel;
        private System.Windows.Forms.TextBox SshKeyBox;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.ComboBox FtpDataType;
        private System.Windows.Forms.Label FtpDataTypeLabel;
        private System.Windows.Forms.CheckBox UseTLS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown PortBox;
    }
}