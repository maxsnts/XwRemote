namespace XwRemote.Misc
{
    partial class Stuff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stuff));
            this.dialogHeader1 = new XwMaxLib.UI.DialogHeader();
            this.faTabStrip1 = new FarsiLibrary.Win.FATabStrip();
            this.faTabAbout = new FarsiLibrary.Win.FATabStripItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.faTabUpdates = new FarsiLibrary.Win.FATabStripItem();
            this.UpdateProgress = new System.Windows.Forms.ProgressBar();
            this.linkReleases = new System.Windows.Forms.LinkLabel();
            this.ReleaseNotes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.linkLatest = new System.Windows.Forms.LinkLabel();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.faTabContact = new FarsiLibrary.Win.FATabStripItem();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MailBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CommentBox = new System.Windows.Forms.TextBox();
            this.faTabDonation = new FarsiLibrary.Win.FATabStripItem();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.faTabCredits = new FarsiLibrary.Win.FATabStripItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.faTabWhyNot = new FarsiLibrary.Win.FATabStripItem();
            this.scroller1 = new ExtendedComponents.Scroller();
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).BeginInit();
            this.faTabStrip1.SuspendLayout();
            this.faTabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.faTabUpdates.SuspendLayout();
            this.faTabContact.SuspendLayout();
            this.faTabDonation.SuspendLayout();
            this.faTabCredits.SuspendLayout();
            this.faTabWhyNot.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "Whatever is there that get nowhere else...";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.xwremote1;
            this.dialogHeader1.HeaderTitle = "Stuff...";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(559, 50);
            this.dialogHeader1.TabIndex = 0;
            // 
            // faTabStrip1
            // 
            this.faTabStrip1.AlwaysShowClose = false;
            this.faTabStrip1.AlwaysShowMenuGlyph = false;
            this.faTabStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faTabStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.faTabStrip1.Items.AddRange(new FarsiLibrary.Win.FATabStripItem[] {
            this.faTabAbout,
            this.faTabUpdates,
            this.faTabContact,
            this.faTabDonation,
            this.faTabCredits,
            this.faTabWhyNot});
            this.faTabStrip1.Location = new System.Drawing.Point(0, 50);
            this.faTabStrip1.Name = "faTabStrip1";
            this.faTabStrip1.SelectedItem = this.faTabAbout;
            this.faTabStrip1.Size = new System.Drawing.Size(559, 388);
            this.faTabStrip1.TabIndex = 1;
            this.faTabStrip1.Text = "faTabStrip1";
            this.faTabStrip1.TabStripItemSelectionChanged += new FarsiLibrary.Win.TabStripItemChangedHandler(this.faTabStrip1_TabStripItemSelectionChanged);
            // 
            // faTabAbout
            // 
            this.faTabAbout.CanClose = false;
            this.faTabAbout.Controls.Add(this.linkLabel1);
            this.faTabAbout.Controls.Add(this.label1);
            this.faTabAbout.Controls.Add(this.version);
            this.faTabAbout.Controls.Add(this.pictureBox1);
            this.faTabAbout.IsDrawn = true;
            this.faTabAbout.Name = "faTabAbout";
            this.faTabAbout.Selected = true;
            this.faTabAbout.Size = new System.Drawing.Size(557, 367);
            this.faTabAbout.TabIndex = 0;
            this.faTabAbout.Title = "About";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(176, 68);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(196, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/maxsnts/XwRemote";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "XwRemote";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(306, 28);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(42, 13);
            this.version.TabIndex = 7;
            this.version.Text = "version";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::XwRemote.Properties.Resources.xwremote1;
            this.pictureBox1.Location = new System.Drawing.Point(195, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 35);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // faTabUpdates
            // 
            this.faTabUpdates.CanClose = false;
            this.faTabUpdates.Controls.Add(this.UpdateProgress);
            this.faTabUpdates.Controls.Add(this.linkReleases);
            this.faTabUpdates.Controls.Add(this.ReleaseNotes);
            this.faTabUpdates.Controls.Add(this.label9);
            this.faTabUpdates.Controls.Add(this.linkLatest);
            this.faTabUpdates.Controls.Add(this.buttonUpdate);
            this.faTabUpdates.Controls.Add(this.labelVersion);
            this.faTabUpdates.IsDrawn = true;
            this.faTabUpdates.Name = "faTabUpdates";
            this.faTabUpdates.Size = new System.Drawing.Size(557, 367);
            this.faTabUpdates.TabIndex = 1;
            this.faTabUpdates.Title = "Updates";
            // 
            // UpdateProgress
            // 
            this.UpdateProgress.Location = new System.Drawing.Point(10, 77);
            this.UpdateProgress.Name = "UpdateProgress";
            this.UpdateProgress.Size = new System.Drawing.Size(535, 10);
            this.UpdateProgress.TabIndex = 22;
            this.UpdateProgress.Visible = false;
            // 
            // linkReleases
            // 
            this.linkReleases.AutoSize = true;
            this.linkReleases.Location = new System.Drawing.Point(279, 103);
            this.linkReleases.Name = "linkReleases";
            this.linkReleases.Size = new System.Drawing.Size(240, 13);
            this.linkReleases.TabIndex = 21;
            this.linkReleases.TabStop = true;
            this.linkReleases.Text = "https://github.com/maxsnts/XwRemote/releases";
            this.linkReleases.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReleases_LinkClicked);
            // 
            // ReleaseNotes
            // 
            this.ReleaseNotes.AcceptsReturn = true;
            this.ReleaseNotes.AcceptsTab = true;
            this.ReleaseNotes.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReleaseNotes.Location = new System.Drawing.Point(11, 122);
            this.ReleaseNotes.Multiline = true;
            this.ReleaseNotes.Name = "ReleaseNotes";
            this.ReleaseNotes.ReadOnly = true;
            this.ReleaseNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ReleaseNotes.Size = new System.Drawing.Size(535, 234);
            this.ReleaseNotes.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(247, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Release notes for the latest version, more info at ";
            // 
            // linkLatest
            // 
            this.linkLatest.Location = new System.Drawing.Point(12, 32);
            this.linkLatest.Name = "linkLatest";
            this.linkLatest.Size = new System.Drawing.Size(532, 13);
            this.linkLatest.TabIndex = 18;
            this.linkLatest.TabStop = true;
            this.linkLatest.Text = "lastest";
            this.linkLatest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLatest.Visible = false;
            this.linkLatest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLatest_LinkClicked);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(198, 50);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(165, 23);
            this.buttonUpdate.TabIndex = 17;
            this.buttonUpdate.Text = "Update now";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(11, 10);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(535, 21);
            this.labelVersion.TabIndex = 16;
            this.labelVersion.Text = "---";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // faTabContact
            // 
            this.faTabContact.CanClose = false;
            this.faTabContact.Controls.Add(this.linkLabel2);
            this.faTabContact.Controls.Add(this.label5);
            this.faTabContact.Controls.Add(this.label4);
            this.faTabContact.Controls.Add(this.label3);
            this.faTabContact.Controls.Add(this.MailBox);
            this.faTabContact.Controls.Add(this.NameBox);
            this.faTabContact.Controls.Add(this.buttonSend);
            this.faTabContact.Controls.Add(this.label2);
            this.faTabContact.Controls.Add(this.CommentBox);
            this.faTabContact.IsDrawn = true;
            this.faTabContact.Name = "faTabContact";
            this.faTabContact.Size = new System.Drawing.Size(557, 367);
            this.faTabContact.TabIndex = 2;
            this.faTabContact.Title = "Contact";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(173, 29);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(229, 13);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://github.com/maxsnts/XwRemote/issues";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(535, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "You can use this if you want to contact me privately, but if its about a bug or f" +
    "eature, i would prefer that you use Github.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Your mail, if you want a reply (Optional)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Your name (Optional)";
            // 
            // MailBox
            // 
            this.MailBox.Location = new System.Drawing.Point(212, 303);
            this.MailBox.Name = "MailBox";
            this.MailBox.Size = new System.Drawing.Size(334, 21);
            this.MailBox.TabIndex = 9;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(212, 276);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(334, 21);
            this.NameBox.TabIndex = 8;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(381, 333);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(165, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Message";
            // 
            // CommentBox
            // 
            this.CommentBox.AcceptsReturn = true;
            this.CommentBox.AcceptsTab = true;
            this.CommentBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentBox.Location = new System.Drawing.Point(11, 77);
            this.CommentBox.Multiline = true;
            this.CommentBox.Name = "CommentBox";
            this.CommentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommentBox.Size = new System.Drawing.Size(535, 190);
            this.CommentBox.TabIndex = 5;
            // 
            // faTabDonation
            // 
            this.faTabDonation.CanClose = false;
            this.faTabDonation.Controls.Add(this.label8);
            this.faTabDonation.Controls.Add(this.label7);
            this.faTabDonation.Controls.Add(this.label6);
            this.faTabDonation.Controls.Add(this.linkLabel3);
            this.faTabDonation.IsDrawn = true;
            this.faTabDonation.Name = "faTabDonation";
            this.faTabDonation.Size = new System.Drawing.Size(557, 367);
            this.faTabDonation.TabIndex = 3;
            this.faTabDonation.Title = "Can you Help?";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(535, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "Thank you";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(535, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "While XwRemote is free, donations are appreciated.";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(535, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "If you like this application and if you can, consider making a contribution.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(161, 101);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(235, 13);
            this.linkLabel3.TabIndex = 0;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Make a contribution however small using paypal";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked_1);
            // 
            // faTabCredits
            // 
            this.faTabCredits.CanClose = false;
            this.faTabCredits.Controls.Add(this.richTextBox1);
            this.faTabCredits.IsDrawn = true;
            this.faTabCredits.Name = "faTabCredits";
            this.faTabCredits.Size = new System.Drawing.Size(557, 367);
            this.faTabCredits.TabIndex = 4;
            this.faTabCredits.Title = "Credits";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(557, 367);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            // 
            // faTabWhyNot
            // 
            this.faTabWhyNot.CanClose = false;
            this.faTabWhyNot.Controls.Add(this.scroller1);
            this.faTabWhyNot.IsDrawn = true;
            this.faTabWhyNot.Name = "faTabWhyNot";
            this.faTabWhyNot.Size = new System.Drawing.Size(557, 367);
            this.faTabWhyNot.TabIndex = 5;
            this.faTabWhyNot.Title = "Why Not";
            // 
            // scroller1
            // 
            this.scroller1.BackColor = System.Drawing.Color.Black;
            this.scroller1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.scroller1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scroller1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scroller1.ForeColor = System.Drawing.Color.Yellow;
            this.scroller1.Interval = 50;
            this.scroller1.Location = new System.Drawing.Point(0, 0);
            this.scroller1.Name = "scroller1";
            this.scroller1.Size = new System.Drawing.Size(557, 367);
            this.scroller1.TabIndex = 0;
            this.scroller1.TextFont = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.scroller1.TextToScroll = "";
            this.scroller1.TopPartSizePercent = 50;
            // 
            // Stuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 438);
            this.Controls.Add(this.faTabStrip1);
            this.Controls.Add(this.dialogHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Stuff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info";
            this.Load += new System.EventHandler(this.Stuff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.faTabStrip1)).EndInit();
            this.faTabStrip1.ResumeLayout(false);
            this.faTabAbout.ResumeLayout(false);
            this.faTabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.faTabUpdates.ResumeLayout(false);
            this.faTabUpdates.PerformLayout();
            this.faTabContact.ResumeLayout(false);
            this.faTabContact.PerformLayout();
            this.faTabDonation.ResumeLayout(false);
            this.faTabDonation.PerformLayout();
            this.faTabCredits.ResumeLayout(false);
            this.faTabWhyNot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private FarsiLibrary.Win.FATabStripItem faTabAbout;
        private FarsiLibrary.Win.FATabStripItem faTabCredits;
        private FarsiLibrary.Win.FATabStripItem faTabWhyNot;
        private FarsiLibrary.Win.FATabStripItem faTabContact;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ExtendedComponents.Scroller scroller1;
        private System.Windows.Forms.TextBox CommentBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MailBox;
        private System.Windows.Forms.TextBox NameBox;
        public FarsiLibrary.Win.FATabStrip faTabStrip1;
        public FarsiLibrary.Win.FATabStripItem faTabDonation;
        public FarsiLibrary.Win.FATabStripItem faTabUpdates;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.LinkLabel linkLatest;
        private System.Windows.Forms.TextBox ReleaseNotes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkReleases;
        private System.Windows.Forms.ProgressBar UpdateProgress;
    }
}