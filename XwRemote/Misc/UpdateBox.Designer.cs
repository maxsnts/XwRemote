namespace XwRemote.Forms
{
    partial class UpdateBox
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.changes = new System.Windows.Forms.TextBox();
            this.butInstall = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.dialogHeader1 = new XwMaxLib.UI.DialogHeader();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 410);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(659, 17);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Visible = false;
            // 
            // changes
            // 
            this.changes.AcceptsReturn = true;
            this.changes.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changes.Location = new System.Drawing.Point(12, 56);
            this.changes.Multiline = true;
            this.changes.Name = "changes";
            this.changes.ReadOnly = true;
            this.changes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.changes.Size = new System.Drawing.Size(659, 348);
            this.changes.TabIndex = 2;
            // 
            // butInstall
            // 
            this.butInstall.Location = new System.Drawing.Point(515, 433);
            this.butInstall.Name = "butInstall";
            this.butInstall.Size = new System.Drawing.Size(75, 23);
            this.butInstall.TabIndex = 3;
            this.butInstall.Text = "Install";
            this.butInstall.UseVisualStyleBackColor = true;
            this.butInstall.Click += new System.EventHandler(this.butInstall_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(596, 433);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "Here is a list of all the changes made to this application";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.updates;
            this.dialogHeader1.HeaderTitle = "New update";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(683, 50);
            this.dialogHeader1.TabIndex = 4;
            // 
            // UpdateBox
            // 
            this.AcceptButton = this.butInstall;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(683, 468);
            this.ControlBox = false;
            this.Controls.Add(this.dialogHeader1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butInstall);
            this.Controls.Add(this.changes);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XwRemote";
            this.Shown += new System.EventHandler(this.UpdateBox_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox changes;
        private System.Windows.Forms.Button butInstall;
        private System.Windows.Forms.Button butCancel;
        private XwMaxLib.UI.DialogHeader dialogHeader1;
    }
}