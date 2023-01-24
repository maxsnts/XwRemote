using XwMaxLib.UI;
namespace XwRemote.Servers
{
    partial class IOForm
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
            this.splitContainerStatus = new System.Windows.Forms.SplitContainer();
            this.splitContainerRemote = new System.Windows.Forms.SplitContainer();
            this.LinkPath = new System.Windows.Forms.Button();
            this.LocalPin = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LocalTree = new XwMaxLib.UI.Shell.XwShellTree();
            this.statusLocal = new System.Windows.Forms.StatusStrip();
            this.statusLocalLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LocalList = new XwRemote.Servers.LocalList();
            this.LocalPath = new System.Windows.Forms.ComboBox();
            this.statusRemote = new System.Windows.Forms.StatusStrip();
            this.statusRemoteLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.RemotePin = new System.Windows.Forms.Button();
            this.RemoteList = new XwRemote.Servers.RemoteList();
            this.RemotePath = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.QueueList = new XwRemote.Servers.QueueList();
            this.StatusBox = new System.Windows.Forms.RichTextBox();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.faTabStats = new FarsiLibrary.Win.FATabStripItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.statusMainLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TotalQueueText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusTransferRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.faTabStripItem4 = new FarsiLibrary.Win.FATabStripItem();
            this.statusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerStatus)).BeginInit();
            this.splitContainerStatus.Panel1.SuspendLayout();
            this.splitContainerStatus.Panel2.SuspendLayout();
            this.splitContainerStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRemote)).BeginInit();
            this.splitContainerRemote.Panel1.SuspendLayout();
            this.splitContainerRemote.Panel2.SuspendLayout();
            this.splitContainerRemote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusLocal.SuspendLayout();
            this.statusRemote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerStatus
            // 
            this.splitContainerStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainerStatus.Location = new System.Drawing.Point(0, 0);
            this.splitContainerStatus.Name = "splitContainerStatus";
            this.splitContainerStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerStatus.Panel1
            // 
            this.splitContainerStatus.Panel1.Controls.Add(this.splitContainerRemote);
            // 
            // splitContainerStatus.Panel2
            // 
            this.splitContainerStatus.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainerStatus.Size = new System.Drawing.Size(896, 669);
            this.splitContainerStatus.SplitterDistance = 527;
            this.splitContainerStatus.TabIndex = 0;
            this.splitContainerStatus.Visible = false;
            // 
            // splitContainerRemote
            // 
            this.splitContainerRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRemote.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRemote.Name = "splitContainerRemote";
            // 
            // splitContainerRemote.Panel1
            // 
            this.splitContainerRemote.Panel1.Controls.Add(this.LinkPath);
            this.splitContainerRemote.Panel1.Controls.Add(this.LocalPin);
            this.splitContainerRemote.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainerRemote.Panel1.Controls.Add(this.LocalPath);
            // 
            // splitContainerRemote.Panel2
            // 
            this.splitContainerRemote.Panel2.Controls.Add(this.statusRemote);
            this.splitContainerRemote.Panel2.Controls.Add(this.RemotePin);
            this.splitContainerRemote.Panel2.Controls.Add(this.RemoteList);
            this.splitContainerRemote.Panel2.Controls.Add(this.RemotePath);
            this.splitContainerRemote.Size = new System.Drawing.Size(896, 527);
            this.splitContainerRemote.SplitterDistance = 562;
            this.splitContainerRemote.TabIndex = 0;
            // 
            // LinkPath
            // 
            this.LinkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LinkPath.FlatAppearance.BorderSize = 0;
            this.LinkPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LinkPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkPath.Image = global::XwRemote.Properties.Resources.link_break;
            this.LinkPath.Location = new System.Drawing.Point(540, 3);
            this.LinkPath.Name = "LinkPath";
            this.LinkPath.Size = new System.Drawing.Size(19, 16);
            this.LinkPath.TabIndex = 4;
            this.LinkPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LinkPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.LinkPath.UseVisualStyleBackColor = true;
            this.LinkPath.Click += new System.EventHandler(this.LinkPath_Click);
            // 
            // LocalPin
            // 
            this.LocalPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalPin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LocalPin.FlatAppearance.BorderSize = 0;
            this.LocalPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocalPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalPin.Image = global::XwRemote.Properties.Resources.PinUp;
            this.LocalPin.Location = new System.Drawing.Point(507, 2);
            this.LocalPin.Name = "LocalPin";
            this.LocalPin.Size = new System.Drawing.Size(19, 16);
            this.LocalPin.TabIndex = 3;
            this.LocalPin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LocalPin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.LocalPin.UseVisualStyleBackColor = true;
            this.LocalPin.Click += new System.EventHandler(this.LocalPin_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 21);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LocalTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusLocal);
            this.splitContainer1.Panel2.Controls.Add(this.LocalList);
            this.splitContainer1.Size = new System.Drawing.Size(562, 506);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 2;
            // 
            // LocalTree
            // 
            this.LocalTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocalTree.HideSelection = false;
            this.LocalTree.Location = new System.Drawing.Point(0, 0);
            this.LocalTree.Name = "LocalTree";
            this.LocalTree.Size = new System.Drawing.Size(240, 506);
            this.LocalTree.TabIndex = 0;
            this.LocalTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LocalTree_AfterSelect);
            // 
            // statusLocal
            // 
            this.statusLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLocalLabel});
            this.statusLocal.Location = new System.Drawing.Point(0, 484);
            this.statusLocal.Name = "statusLocal";
            this.statusLocal.Size = new System.Drawing.Size(318, 22);
            this.statusLocal.TabIndex = 1;
            this.statusLocal.Text = "statusLocal";
            // 
            // statusLocalLabel
            // 
            this.statusLocalLabel.Name = "statusLocalLabel";
            this.statusLocalLabel.Size = new System.Drawing.Size(94, 17);
            this.statusLocalLabel.Text = "statusLocalLabel";
            // 
            // LocalList
            // 
            this.LocalList.AllowDrop = true;
            this.LocalList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalList.AutoArrange = false;
            this.LocalList.Cursor = System.Windows.Forms.Cursors.Default;
            this.LocalList.FullRowSelect = true;
            this.LocalList.HideSelection = false;
            this.LocalList.Location = new System.Drawing.Point(0, 0);
            this.LocalList.Name = "LocalList";
            this.LocalList.Size = new System.Drawing.Size(318, 481);
            this.LocalList.TabIndex = 0;
            this.LocalList.UseCompatibleStateImageBehavior = false;
            this.LocalList.View = System.Windows.Forms.View.Details;
            // 
            // LocalPath
            // 
            this.LocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LocalPath.FormattingEnabled = true;
            this.LocalPath.Location = new System.Drawing.Point(0, 0);
            this.LocalPath.Name = "LocalPath";
            this.LocalPath.Size = new System.Drawing.Size(504, 21);
            this.LocalPath.TabIndex = 1;
            this.LocalPath.SelectedIndexChanged += new System.EventHandler(this.LocalPath_SelectedIndexChanged);
            this.LocalPath.DropDownClosed += new System.EventHandler(this.LocalPath_DropDownClosed);
            this.LocalPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LocalPath_KeyDown);
            // 
            // statusRemote
            // 
            this.statusRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusRemoteLabel});
            this.statusRemote.Location = new System.Drawing.Point(0, 505);
            this.statusRemote.Name = "statusRemote";
            this.statusRemote.Size = new System.Drawing.Size(330, 22);
            this.statusRemote.TabIndex = 5;
            this.statusRemote.Text = "statusRemote";
            // 
            // statusRemoteLabel
            // 
            this.statusRemoteLabel.Name = "statusRemoteLabel";
            this.statusRemoteLabel.Size = new System.Drawing.Size(107, 17);
            this.statusRemoteLabel.Text = "statusRemoteLabel";
            // 
            // RemotePin
            // 
            this.RemotePin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemotePin.FlatAppearance.BorderSize = 0;
            this.RemotePin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemotePin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemotePin.Image = global::XwRemote.Properties.Resources.PinUp;
            this.RemotePin.Location = new System.Drawing.Point(3, 2);
            this.RemotePin.Name = "RemotePin";
            this.RemotePin.Size = new System.Drawing.Size(19, 16);
            this.RemotePin.TabIndex = 4;
            this.RemotePin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RemotePin.UseVisualStyleBackColor = true;
            this.RemotePin.Click += new System.EventHandler(this.RemotePin_Click);
            // 
            // RemoteList
            // 
            this.RemoteList.AllowDrop = true;
            this.RemoteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteList.AutoArrange = false;
            this.RemoteList.FullRowSelect = true;
            this.RemoteList.HideSelection = false;
            this.RemoteList.Location = new System.Drawing.Point(0, 21);
            this.RemoteList.Name = "RemoteList";
            this.RemoteList.Size = new System.Drawing.Size(330, 481);
            this.RemoteList.TabIndex = 1;
            this.RemoteList.UseCompatibleStateImageBehavior = false;
            this.RemoteList.View = System.Windows.Forms.View.Details;
            // 
            // RemotePath
            // 
            this.RemotePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RemotePath.FormattingEnabled = true;
            this.RemotePath.Location = new System.Drawing.Point(25, 0);
            this.RemotePath.Name = "RemotePath";
            this.RemotePath.Size = new System.Drawing.Size(305, 21);
            this.RemotePath.TabIndex = 0;
            this.RemotePath.DropDownClosed += new System.EventHandler(this.RemotePath_DropDownClosed);
            this.RemotePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemotePath_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.QueueList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.StatusBox);
            this.splitContainer2.Size = new System.Drawing.Size(896, 138);
            this.splitContainer2.SplitterDistance = 561;
            this.splitContainer2.TabIndex = 4;
            // 
            // QueueList
            // 
            this.QueueList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueList.FullRowSelect = true;
            this.QueueList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.QueueList.HideSelection = false;
            this.QueueList.Location = new System.Drawing.Point(0, 0);
            this.QueueList.Name = "QueueList";
            this.QueueList.OwnerDraw = true;
            this.QueueList.ShowItemToolTips = true;
            this.QueueList.Size = new System.Drawing.Size(561, 138);
            this.QueueList.TabIndex = 1;
            this.QueueList.UseCompatibleStateImageBehavior = false;
            this.QueueList.View = System.Windows.Forms.View.Details;
            // 
            // StatusBox
            // 
            this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusBox.DetectUrls = false;
            this.StatusBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBox.Location = new System.Drawing.Point(0, 0);
            this.StatusBox.MaxLength = 0;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.Size = new System.Drawing.Size(331, 138);
            this.StatusBox.TabIndex = 4;
            this.StatusBox.Text = "";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.Color.Transparent;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(109, 197);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(75, 79);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 3;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // faTabStats
            // 
            this.faTabStats.CanClose = false;
            this.faTabStats.IsDrawn = true;
            this.faTabStats.Name = "faTabStats";
            this.faTabStats.Size = new System.Drawing.Size(797, 97);
            this.faTabStats.TabIndex = 2;
            this.faTabStats.Title = "Stats";
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMainLabel});
            this.statusMain.Location = new System.Drawing.Point(0, 670);
            this.statusMain.Name = "statusMain";
            this.statusMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusMain.Size = new System.Drawing.Size(896, 22);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusMain";
            // 
            // statusMainLabel
            // 
            this.statusMainLabel.Name = "statusMainLabel";
            this.statusMainLabel.Size = new System.Drawing.Size(93, 17);
            this.statusMainLabel.Text = "statusMainLabel";
            // 
            // TotalQueueText
            // 
            this.TotalQueueText.Name = "TotalQueueText";
            this.TotalQueueText.Size = new System.Drawing.Size(94, 17);
            this.TotalQueueText.Text = "0 Items in queue";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusTransferRate
            // 
            this.toolStripStatusTransferRate.Name = "toolStripStatusTransferRate";
            this.toolStripStatusTransferRate.Padding = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.toolStripStatusTransferRate.Size = new System.Drawing.Size(161, 17);
            this.toolStripStatusTransferRate.Text = "--- Kb/sec";
            // 
            // faTabStripItem4
            // 
            this.faTabStripItem4.CanClose = false;
            this.faTabStripItem4.IsDrawn = true;
            this.faTabStripItem4.Name = "faTabStripItem4";
            this.faTabStripItem4.Selected = true;
            this.faTabStripItem4.Size = new System.Drawing.Size(797, 101);
            this.faTabStripItem4.TabIndex = 3;
            this.faTabStripItem4.Title = "TabStrip Page 4";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(98, 197);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(70, 13);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Connecting...";
            // 
            // IOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(896, 692);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.splitContainerStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IOForm";
            this.Text = "FTPForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.Enter += new System.EventHandler(this.FTPForm_Enter);
            this.splitContainerStatus.Panel1.ResumeLayout(false);
            this.splitContainerStatus.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerStatus)).EndInit();
            this.splitContainerStatus.ResumeLayout(false);
            this.splitContainerRemote.Panel1.ResumeLayout(false);
            this.splitContainerRemote.Panel2.ResumeLayout(false);
            this.splitContainerRemote.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRemote)).EndInit();
            this.splitContainerRemote.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusLocal.ResumeLayout(false);
            this.statusLocal.PerformLayout();
            this.statusRemote.ResumeLayout(false);
            this.statusRemote.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.SplitContainer splitContainerStatus;
        private System.Windows.Forms.SplitContainer splitContainerRemote;
        private FarsiLibrary.Win.FATabStripItem faTabStats;
        private FarsiLibrary.Win.FATabStripItem faTabStripItem4;
        public System.Windows.Forms.ComboBox LocalPath;
        public System.Windows.Forms.ComboBox RemotePath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public  LocalList LocalList;
        public  RemoteList RemoteList;
        public System.Windows.Forms.ToolStripStatusLabel TotalQueueText;
        public System.Windows.Forms.Button LocalPin;
        public System.Windows.Forms.Button RemotePin;
        public System.Windows.Forms.Button LinkPath;
        public XwMaxLib.UI.Shell.XwShellTree LocalTree;
        private System.Windows.Forms.StatusStrip statusLocal;
        private System.Windows.Forms.StatusStrip statusRemote;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusTransferRate;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        public System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        public QueueList QueueList;
        private System.Windows.Forms.RichTextBox StatusBox;
        public System.Windows.Forms.StatusStrip statusMain;
        public System.Windows.Forms.ToolStripStatusLabel statusMainLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLocalLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusRemoteLabel;
    }
}