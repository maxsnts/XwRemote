using AxMSTSCLib;

namespace XwRemote.Servers
{
    partial class RDPForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RDPForm));
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.axMsRdpClient10NotSafeForScripting1 = new AxMSTSCLib.AxMsRdpClient10NotSafeForScripting();
            this.rdpControl = new AxMSTSCLib.AxMsRdpClient9NotSafeForScripting();
            this.labelMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient10NotSafeForScripting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpControl)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Window;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(331, 188);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(70, 70);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 1;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(311, 322);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(114, 35);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // axMsRdpClient10NotSafeForScripting1
            // 
            this.axMsRdpClient10NotSafeForScripting1.Enabled = true;
            this.axMsRdpClient10NotSafeForScripting1.Location = new System.Drawing.Point(0, 0);
            this.axMsRdpClient10NotSafeForScripting1.Name = "axMsRdpClient10NotSafeForScripting1";
            this.axMsRdpClient10NotSafeForScripting1.Size = new System.Drawing.Size(75, 23);
            this.axMsRdpClient10NotSafeForScripting1.TabIndex = 0;
            // 
            // rdpControl
            // 
            this.rdpControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdpControl.Enabled = true;
            this.rdpControl.Location = new System.Drawing.Point(0, 0);
            this.rdpControl.Name = "rdpControl";
            this.rdpControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpControl.OcxState")));
            this.rdpControl.Size = new System.Drawing.Size(734, 579);
            this.rdpControl.TabIndex = 3;
            this.rdpControl.OnConnected += new System.EventHandler(this.rdpControl_OnConnected);
            this.rdpControl.OnDisconnected += new AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEventHandler(this.rdpControl_OnDisconnected);
            this.rdpControl.OnFatalError += new AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEventHandler(this.rdpControl_OnFatalError);
            this.rdpControl.OnWarning += new AxMSTSCLib.IMsTscAxEvents_OnWarningEventHandler(this.rdpControl_OnWarning);
            this.rdpControl.Enter += new System.EventHandler(this.rdpControl_Enter);
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.SystemColors.Window;
            this.labelMessage.Location = new System.Drawing.Point(329, 261);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(72, 13);
            this.labelMessage.TabIndex = 4;
            this.labelMessage.Text = "labelMessage";
            this.labelMessage.Visible = false;
            // 
            // RDPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 579);
            this.ControlBox = false;
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.rdpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RDPForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "RDP";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.SizeChanged += new System.EventHandler(this.RDPForm_SizeChanged);
            this.Enter += new System.EventHandler(this.OnEnter);
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient10NotSafeForScripting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Button buttonConnect;
        private AxMsRdpClient10NotSafeForScripting axMsRdpClient10NotSafeForScripting1;
        private AxMsRdpClient9NotSafeForScripting rdpControl;
        private System.Windows.Forms.Label labelMessage;
    }
}