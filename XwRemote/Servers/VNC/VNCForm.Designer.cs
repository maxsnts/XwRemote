namespace XwRemote.Servers
{
    partial class VNCForm
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
            this.vnc = new VncSharp.RemoteDesktop();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.vnc.SuspendLayout();
            this.SuspendLayout();
            // 
            // vnc
            // 
            this.vnc.AutoScroll = true;
            this.vnc.AutoScrollMinSize = new System.Drawing.Size(608, 427);
            this.vnc.Controls.Add(this.loadingCircle1);
            this.vnc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vnc.ForeColor = System.Drawing.Color.White;
            this.vnc.Location = new System.Drawing.Point(0, 0);
            this.vnc.Name = "vnc";
            this.vnc.Size = new System.Drawing.Size(593, 447);
            this.vnc.TabIndex = 0;
            this.vnc.ConnectComplete += new VncSharp.ConnectCompleteHandler(this.vnc_ConnectComplete);
            this.vnc.ConnectionLost += new System.EventHandler(this.vnc_ConnectionLost);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(253, 149);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(80, 82);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 0;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // VNCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 447);
            this.Controls.Add(this.vnc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VNCForm";
            this.Text = "VNCForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.Enter += new System.EventHandler(this.OnEnter);
            this.vnc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VncSharp.RemoteDesktop vnc;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
    }
}