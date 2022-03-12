namespace XwRemote.Settings
{
    partial class SSHSettings
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.UserBox = new System.Windows.Forms.TextBox();
            this.HostBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IsFavorite = new System.Windows.Forms.CheckBox();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.DefaultPort = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkSSH1 = new System.Windows.Forms.CheckBox();
            this.tabColorBox = new ColorComboTestApp.ColorComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NotesBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOpenSshKey = new System.Windows.Forms.Button();
            this.SshKeyBox = new System.Windows.Forms.TextBox();
            this.buttonShowPassword = new System.Windows.Forms.Button();
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
            this.dialogHeader1.HeaderDescription = "Connecting settings for SSH server";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.ssh;
            this.dialogHeader1.HeaderTitle = "SSH";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(391, 50);
            this.dialogHeader1.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(307, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(226, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Host";
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(65, 135);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(293, 20);
            this.PassBox.TabIndex = 3;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassBox_KeyDown);
            // 
            // UserBox
            // 
            this.UserBox.Location = new System.Drawing.Point(65, 109);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(317, 20);
            this.UserBox.TabIndex = 2;
            // 
            // HostBox
            // 
            this.HostBox.Location = new System.Drawing.Point(65, 83);
            this.HostBox.Name = "HostBox";
            this.HostBox.Size = new System.Drawing.Size(317, 20);
            this.HostBox.TabIndex = 1;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(65, 56);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(317, 20);
            this.NameBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name";
            // 
            // IsFavorite
            // 
            this.IsFavorite.AutoSize = true;
            this.IsFavorite.Location = new System.Drawing.Point(11, 14);
            this.IsFavorite.Name = "IsFavorite";
            this.IsFavorite.Size = new System.Drawing.Size(143, 17);
            this.IsFavorite.TabIndex = 0;
            this.IsFavorite.Text = "Place on Favorites menu";
            this.IsFavorite.UseVisualStyleBackColor = true;
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.IsFavorite);
            this.dividerPanel1.Controls.Add(this.btnCancel);
            this.dividerPanel1.Controls.Add(this.btnOK);
            this.dividerPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dividerPanel1.Location = new System.Drawing.Point(0, 427);
            this.dividerPanel1.Name = "dividerPanel1";
            this.dividerPanel1.Size = new System.Drawing.Size(391, 45);
            this.dividerPanel1.TabIndex = 13;
            // 
            // DefaultPort
            // 
            this.DefaultPort.AutoSize = true;
            this.DefaultPort.Checked = true;
            this.DefaultPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefaultPort.Location = new System.Drawing.Point(65, 163);
            this.DefaultPort.Name = "DefaultPort";
            this.DefaultPort.Size = new System.Drawing.Size(60, 17);
            this.DefaultPort.TabIndex = 4;
            this.DefaultPort.Text = "Default";
            this.DefaultPort.UseVisualStyleBackColor = true;
            this.DefaultPort.CheckedChanged += new System.EventHandler(this.DefaultPort_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Port";
            // 
            // checkSSH1
            // 
            this.checkSSH1.AutoSize = true;
            this.checkSSH1.Location = new System.Drawing.Point(65, 187);
            this.checkSSH1.Name = "checkSSH1";
            this.checkSSH1.Size = new System.Drawing.Size(119, 17);
            this.checkSSH1.TabIndex = 5;
            this.checkSSH1.Text = "Connect with SSH1";
            this.checkSSH1.UseVisualStyleBackColor = true;
            // 
            // tabColorBox
            // 
            this.tabColorBox.Extended = true;
            this.tabColorBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabColorBox.Location = new System.Drawing.Point(65, 398);
            this.tabColorBox.Name = "tabColorBox";
            this.tabColorBox.SelectedColor = System.Drawing.Color.LightSkyBlue;
            this.tabColorBox.Size = new System.Drawing.Size(70, 23);
            this.tabColorBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tab color";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Notes";
            // 
            // NotesBox
            // 
            this.NotesBox.AcceptsReturn = true;
            this.NotesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NotesBox.Location = new System.Drawing.Point(65, 332);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(317, 62);
            this.NotesBox.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "SshKey";
            // 
            // buttonOpenSshKey
            // 
            this.buttonOpenSshKey.Location = new System.Drawing.Point(16, 266);
            this.buttonOpenSshKey.Name = "buttonOpenSshKey";
            this.buttonOpenSshKey.Size = new System.Drawing.Size(26, 23);
            this.buttonOpenSshKey.TabIndex = 34;
            this.buttonOpenSshKey.Text = "...";
            this.buttonOpenSshKey.UseVisualStyleBackColor = true;
            this.buttonOpenSshKey.Click += new System.EventHandler(this.buttonOpenSshKey_Click);
            // 
            // SshKeyBox
            // 
            this.SshKeyBox.AcceptsReturn = true;
            this.SshKeyBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SshKeyBox.Location = new System.Drawing.Point(65, 237);
            this.SshKeyBox.Multiline = true;
            this.SshKeyBox.Name = "SshKeyBox";
            this.SshKeyBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SshKeyBox.Size = new System.Drawing.Size(317, 90);
            this.SshKeyBox.TabIndex = 6;
            this.SshKeyBox.WordWrap = false;
            // 
            // buttonShowPassword
            // 
            this.buttonShowPassword.Image = global::XwRemote.Properties.Resources.eye;
            this.buttonShowPassword.Location = new System.Drawing.Point(359, 134);
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.Size = new System.Drawing.Size(23, 22);
            this.buttonShowPassword.TabIndex = 40;
            this.buttonShowPassword.UseVisualStyleBackColor = true;
            this.buttonShowPassword.Click += new System.EventHandler(this.buttonShowPassword_Click);
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(131, 162);
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
            this.PortBox.TabIndex = 4;
            this.PortBox.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // SSHSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(391, 472);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.buttonShowPassword);
            this.Controls.Add(this.SshKeyBox);
            this.Controls.Add(this.buttonOpenSshKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.NotesBox);
            this.Controls.Add(this.tabColorBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkSSH1);
            this.Controls.Add(this.DefaultPort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.HostBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dialogHeader1);
            this.Controls.Add(this.dividerPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SSHSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SSH Settings";
            this.Load += new System.EventHandler(this.OnLoad);
            this.dividerPanel1.ResumeLayout(false);
            this.dividerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.TextBox UserBox;
        private System.Windows.Forms.TextBox HostBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox IsFavorite;
        private DividerPanel.DividerPanel dividerPanel1;
        private System.Windows.Forms.CheckBox DefaultPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkSSH1;
        private ColorComboTestApp.ColorComboBox tabColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOpenSshKey;
        private System.Windows.Forms.TextBox SshKeyBox;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.NumericUpDown PortBox;
    }
}