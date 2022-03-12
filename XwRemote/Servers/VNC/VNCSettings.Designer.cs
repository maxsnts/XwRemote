namespace XwRemote.Settings
{
    partial class VNCSettings
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
            this.IsFavorite = new System.Windows.Forms.CheckBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.HostBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DefaultPort = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkAutoScale = new System.Windows.Forms.CheckBox();
            this.tabColorBox = new ColorComboTestApp.ColorComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NotesBox = new System.Windows.Forms.TextBox();
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
            this.dialogHeader1.HeaderDescription = "Connecting settings for VNC server";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.vnc;
            this.dialogHeader1.HeaderTitle = "VNC";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(392, 50);
            this.dialogHeader1.TabIndex = 6;
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.IsFavorite);
            this.dividerPanel1.Controls.Add(this.butCancel);
            this.dividerPanel1.Controls.Add(this.butOK);
            this.dividerPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dividerPanel1.Location = new System.Drawing.Point(0, 294);
            this.dividerPanel1.Name = "dividerPanel1";
            this.dividerPanel1.Size = new System.Drawing.Size(392, 45);
            this.dividerPanel1.TabIndex = 11;
            // 
            // IsFavorite
            // 
            this.IsFavorite.AutoSize = true;
            this.IsFavorite.Location = new System.Drawing.Point(12, 16);
            this.IsFavorite.Name = "IsFavorite";
            this.IsFavorite.Size = new System.Drawing.Size(143, 17);
            this.IsFavorite.TabIndex = 0;
            this.IsFavorite.Text = "Place on Favorites menu";
            this.IsFavorite.UseVisualStyleBackColor = true;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Host";
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(66, 109);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(293, 20);
            this.PassBox.TabIndex = 2;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassBox_KeyDown);
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
            this.label2.TabIndex = 7;
            this.label2.Text = "Name";
            // 
            // DefaultPort
            // 
            this.DefaultPort.AutoSize = true;
            this.DefaultPort.Checked = true;
            this.DefaultPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefaultPort.Location = new System.Drawing.Point(66, 137);
            this.DefaultPort.Name = "DefaultPort";
            this.DefaultPort.Size = new System.Drawing.Size(60, 17);
            this.DefaultPort.TabIndex = 3;
            this.DefaultPort.Text = "Default";
            this.DefaultPort.UseVisualStyleBackColor = true;
            this.DefaultPort.CheckedChanged += new System.EventHandler(this.DefaultPort_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Port";
            // 
            // checkAutoScale
            // 
            this.checkAutoScale.AutoSize = true;
            this.checkAutoScale.Location = new System.Drawing.Point(66, 160);
            this.checkAutoScale.Name = "checkAutoScale";
            this.checkAutoScale.Size = new System.Drawing.Size(78, 17);
            this.checkAutoScale.TabIndex = 5;
            this.checkAutoScale.Text = "Auto Scale";
            this.checkAutoScale.UseVisualStyleBackColor = true;
            // 
            // tabColorBox
            // 
            this.tabColorBox.Extended = true;
            this.tabColorBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabColorBox.Location = new System.Drawing.Point(66, 266);
            this.tabColorBox.Name = "tabColorBox";
            this.tabColorBox.SelectedColor = System.Drawing.Color.LightSkyBlue;
            this.tabColorBox.Size = new System.Drawing.Size(70, 23);
            this.tabColorBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tab color";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Notes";
            // 
            // NotesBox
            // 
            this.NotesBox.AcceptsReturn = true;
            this.NotesBox.Location = new System.Drawing.Point(66, 183);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(317, 77);
            this.NotesBox.TabIndex = 6;
            // 
            // buttonShowPassword
            // 
            this.buttonShowPassword.Image = global::XwRemote.Properties.Resources.eye;
            this.buttonShowPassword.Location = new System.Drawing.Point(360, 108);
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.Size = new System.Drawing.Size(23, 22);
            this.buttonShowPassword.TabIndex = 40;
            this.buttonShowPassword.UseVisualStyleBackColor = true;
            this.buttonShowPassword.Click += new System.EventHandler(this.buttonShowPassword_Click);
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(132, 136);
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
            5900,
            0,
            0,
            0});
            // 
            // VNCSettings
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(392, 339);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.buttonShowPassword);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.NotesBox);
            this.Controls.Add(this.tabColorBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkAutoScale);
            this.Controls.Add(this.DefaultPort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.HostBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.dialogHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VNCSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VNC Settings";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.TextBox HostBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox DefaultPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkAutoScale;
        private ColorComboTestApp.ColorComboBox tabColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.NumericUpDown PortBox;
    }
}