namespace XwRemote.Settings
{
    partial class SQLSettings
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
            this.PortBox = new System.Windows.Forms.TextBox();
            this.tabColorBox = new ColorComboTestApp.ColorComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NotesBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UserBox = new System.Windows.Forms.TextBox();
            this.EngineBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dialogHeader1.HeaderDescription = "Connecting settings for SQL";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.database;
            this.dialogHeader1.HeaderTitle = "SQL  *** Highly experimental  ***";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(391, 50);
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
            this.dividerPanel1.Location = new System.Drawing.Point(0, 327);
            this.dividerPanel1.Name = "dividerPanel1";
            this.dividerPanel1.Size = new System.Drawing.Size(391, 45);
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
            this.butOK.TabIndex = 0;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Host";
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(66, 162);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(317, 20);
            this.PassBox.TabIndex = 4;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassBox_KeyDown);
            // 
            // HostBox
            // 
            this.HostBox.Location = new System.Drawing.Point(66, 110);
            this.HostBox.Name = "HostBox";
            this.HostBox.Size = new System.Drawing.Size(317, 20);
            this.HostBox.TabIndex = 2;
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
            this.DefaultPort.Location = new System.Drawing.Point(66, 191);
            this.DefaultPort.Name = "DefaultPort";
            this.DefaultPort.Size = new System.Drawing.Size(60, 17);
            this.DefaultPort.TabIndex = 5;
            this.DefaultPort.Text = "Default";
            this.DefaultPort.UseVisualStyleBackColor = true;
            this.DefaultPort.CheckedChanged += new System.EventHandler(this.DefaultPort_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Port";
            // 
            // PortBox
            // 
            this.PortBox.Enabled = false;
            this.PortBox.Location = new System.Drawing.Point(132, 189);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(251, 20);
            this.PortBox.TabIndex = 6;
            // 
            // tabColorBox
            // 
            this.tabColorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tabColorBox.Extended = true;
            this.tabColorBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabColorBox.Location = new System.Drawing.Point(66, 298);
            this.tabColorBox.Name = "tabColorBox";
            this.tabColorBox.SelectedColor = System.Drawing.Color.LightSkyBlue;
            this.tabColorBox.Size = new System.Drawing.Size(70, 23);
            this.tabColorBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tab color";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Notes";
            // 
            // NotesBox
            // 
            this.NotesBox.AcceptsReturn = true;
            this.NotesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NotesBox.Location = new System.Drawing.Point(66, 215);
            this.NotesBox.Multiline = true;
            this.NotesBox.Name = "NotesBox";
            this.NotesBox.Size = new System.Drawing.Size(317, 77);
            this.NotesBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Username";
            // 
            // UserBox
            // 
            this.UserBox.Location = new System.Drawing.Point(66, 136);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(317, 20);
            this.UserBox.TabIndex = 3;
            // 
            // EngineBox
            // 
            this.EngineBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EngineBox.FormattingEnabled = true;
            this.EngineBox.Location = new System.Drawing.Point(66, 82);
            this.EngineBox.Name = "EngineBox";
            this.EngineBox.Size = new System.Drawing.Size(317, 21);
            this.EngineBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Engine";
            // 
            // SQLSettings
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(391, 372);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EngineBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.NotesBox);
            this.Controls.Add(this.tabColorBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DefaultPort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PortBox);
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
            this.Name = "SQLSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQLSettings";
            this.Load += new System.EventHandler(this.OnLoad);
            this.dividerPanel1.ResumeLayout(false);
            this.dividerPanel1.PerformLayout();
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
        private System.Windows.Forms.TextBox PortBox;
        private ColorComboTestApp.ColorComboBox tabColorBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NotesBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox UserBox;
        private System.Windows.Forms.ComboBox EngineBox;
        private System.Windows.Forms.Label label6;
    }
}