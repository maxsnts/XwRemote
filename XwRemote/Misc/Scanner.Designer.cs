namespace XwRemote.Misc
{
    partial class Scanner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scanner));
            this.dialogHeader1 = new XwMaxLib.UI.DialogHeader();
            this.ipAddressControlFrom = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControlTo = new IPAddressControlLib.IPAddressControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.listViewHosts = new System.Windows.Forms.ListView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textTcpPorts = new System.Windows.Forms.TextBox();
            this.checkTcpPorts = new System.Windows.Forms.CheckBox();
            this.timerUI = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "Try and find hosts on the network that have open ports so that you can connect to" +
    " them";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.magnifier;
            this.dialogHeader1.HeaderTitle = "Network scan";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(913, 50);
            this.dialogHeader1.TabIndex = 1;
            // 
            // ipAddressControlFrom
            // 
            this.ipAddressControlFrom.AllowInternalTab = false;
            this.ipAddressControlFrom.AutoHeight = true;
            this.ipAddressControlFrom.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlFrom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlFrom.Location = new System.Drawing.Point(87, 59);
            this.ipAddressControlFrom.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControlFrom.Name = "ipAddressControlFrom";
            this.ipAddressControlFrom.ReadOnly = false;
            this.ipAddressControlFrom.Size = new System.Drawing.Size(87, 20);
            this.ipAddressControlFrom.TabIndex = 2;
            this.ipAddressControlFrom.Text = "...";
            this.ipAddressControlFrom.TextChanged += new System.EventHandler(this.ipAddressControlFrom_TextChanged);
            // 
            // ipAddressControlTo
            // 
            this.ipAddressControlTo.AllowInternalTab = false;
            this.ipAddressControlTo.AutoHeight = true;
            this.ipAddressControlTo.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlTo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlTo.Location = new System.Drawing.Point(202, 59);
            this.ipAddressControlTo.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControlTo.Name = "ipAddressControlTo";
            this.ipAddressControlTo.ReadOnly = false;
            this.ipAddressControlTo.Size = new System.Drawing.Size(87, 20);
            this.ipAddressControlTo.TabIndex = 3;
            this.ipAddressControlTo.Text = "...";
            this.ipAddressControlTo.TextChanged += new System.EventHandler(this.ipAddressControlTo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP range from:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "to";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(826, 57);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // listViewHosts
            // 
            this.listViewHosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHosts.Location = new System.Drawing.Point(12, 103);
            this.listViewHosts.Name = "listViewHosts";
            this.listViewHosts.Size = new System.Drawing.Size(889, 582);
            this.listViewHosts.TabIndex = 7;
            this.listViewHosts.UseCompatibleStateImageBehavior = false;
            this.listViewHosts.View = System.Windows.Forms.View.Details;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 86);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(889, 11);
            this.progressBar.TabIndex = 8;
            // 
            // textTcpPorts
            // 
            this.textTcpPorts.Location = new System.Drawing.Point(399, 59);
            this.textTcpPorts.Name = "textTcpPorts";
            this.textTcpPorts.Size = new System.Drawing.Size(373, 20);
            this.textTcpPorts.TabIndex = 10;
            // 
            // checkTcpPorts
            // 
            this.checkTcpPorts.AutoSize = true;
            this.checkTcpPorts.Checked = true;
            this.checkTcpPorts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTcpPorts.Location = new System.Drawing.Point(336, 61);
            this.checkTcpPorts.Name = "checkTcpPorts";
            this.checkTcpPorts.Size = new System.Drawing.Size(65, 17);
            this.checkTcpPorts.TabIndex = 9;
            this.checkTcpPorts.Text = "tcpPorts";
            this.checkTcpPorts.UseVisualStyleBackColor = true;
            this.checkTcpPorts.CheckedChanged += new System.EventHandler(this.checkTcpPorts_CheckedChanged);
            // 
            // timerUI
            // 
            this.timerUI.Tick += new System.EventHandler(this.timerUI_Tick);
            // 
            // Scanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 697);
            this.Controls.Add(this.textTcpPorts);
            this.Controls.Add(this.checkTcpPorts);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listViewHosts);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipAddressControlTo);
            this.Controls.Add(this.ipAddressControlFrom);
            this.Controls.Add(this.dialogHeader1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XwRemote Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Scanner_FormClosing);
            this.Load += new System.EventHandler(this.Scanner_Load);
            this.Resize += new System.EventHandler(this.Scanner_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private IPAddressControlLib.IPAddressControl ipAddressControlFrom;
        private IPAddressControlLib.IPAddressControl ipAddressControlTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ListView listViewHosts;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textTcpPorts;
        private System.Windows.Forms.CheckBox checkTcpPorts;
        private System.Windows.Forms.Timer timerUI;
    }
}