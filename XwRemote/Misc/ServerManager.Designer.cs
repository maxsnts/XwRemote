namespace XwRemote
{
    partial class ServerManager
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.treeServers = new System.Windows.Forms.TreeView();
            this.tabStrip1 = new Messir.Windows.Forms.TabStrip();
            this.FilterGrouped = new Messir.Windows.Forms.TabStripButton();
            this.FilterOrdered = new Messir.Windows.Forms.TabStripButton();
            this.FilterFavorites = new Messir.Windows.Forms.TabStripButton();
            this.contextGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportServer = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.NewFTP = new System.Windows.Forms.Button();
            this.NewRDP = new System.Windows.Forms.Button();
            this.NewVNC = new System.Windows.Forms.Button();
            this.NewSSH = new System.Windows.Forms.Button();
            this.EditServer = new System.Windows.Forms.Button();
            this.DeleteServer = new System.Windows.Forms.Button();
            this.contextServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.makeCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsRDP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsSSH = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsSFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsAWSS3 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsAzureFile = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsVNC = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsIE = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportServer = new System.Windows.Forms.ToolStripMenuItem();
            this.NewIE = new System.Windows.Forms.Button();
            this.dialogHeader1 = new XwMaxLib.UI.DialogHeader();
            this.NewSFTP = new System.Windows.Forms.Button();
            this.NewS3 = new System.Windows.Forms.Button();
            this.NewAzureFile = new System.Windows.Forms.Button();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.contextGroup.SuspendLayout();
            this.contextServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.treeServers);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(404, 513);
            this.toolStripContainer1.Location = new System.Drawing.Point(12, 56);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(404, 538);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tabStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // treeServers
            // 
            this.treeServers.AllowDrop = true;
            this.treeServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeServers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeServers.HideSelection = false;
            this.treeServers.LineColor = System.Drawing.Color.SkyBlue;
            this.treeServers.Location = new System.Drawing.Point(0, 0);
            this.treeServers.Name = "treeServers";
            this.treeServers.ShowNodeToolTips = true;
            this.treeServers.Size = new System.Drawing.Size(404, 513);
            this.treeServers.TabIndex = 0;
            this.treeServers.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeServers_AfterLabelEdit);
            this.treeServers.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeServers_AfterCollapse);
            this.treeServers.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeServers_AfterExpand);
            this.treeServers.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeServers_ItemDrag);
            this.treeServers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeServers_AfterSelect);
            this.treeServers.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeServers_DragDrop);
            this.treeServers.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeServers_DragEnter);
            this.treeServers.DragOver += new System.Windows.Forms.DragEventHandler(this.treeServers_DragOver);
            this.treeServers.DragLeave += new System.EventHandler(this.treeServers_DragLeave);
            this.treeServers.DoubleClick += new System.EventHandler(this.treeServers_DoubleClick);
            this.treeServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeServers_MouseDown);
            this.treeServers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeServers_MouseUp);
            // 
            // tabStrip1
            // 
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabStrip1.FlipButtons = false;
            this.tabStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.tabStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterGrouped,
            this.FilterOrdered,
            this.FilterFavorites});
            this.tabStrip1.Location = new System.Drawing.Point(3, 0);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabStrip1.SelectedTab = this.FilterFavorites;
            this.tabStrip1.Size = new System.Drawing.Size(219, 25);
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.UseVisualStyles = false;
            // 
            // FilterGrouped
            // 
            this.FilterGrouped.AutoToolTip = false;
            this.FilterGrouped.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterGrouped.Image = global::XwRemote.Properties.Resources.group;
            this.FilterGrouped.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FilterGrouped.IsSelected = false;
            this.FilterGrouped.Margin = new System.Windows.Forms.Padding(0);
            this.FilterGrouped.Name = "FilterGrouped";
            this.FilterGrouped.Padding = new System.Windows.Forms.Padding(0);
            this.FilterGrouped.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterGrouped.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterGrouped.Size = new System.Drawing.Size(73, 25);
            this.FilterGrouped.Text = "Grouped";
            this.FilterGrouped.Click += new System.EventHandler(this.FilterGrouped_Click);
            // 
            // FilterOrdered
            // 
            this.FilterOrdered.AutoToolTip = false;
            this.FilterOrdered.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterOrdered.Image = global::XwRemote.Properties.Resources.nogroup;
            this.FilterOrdered.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FilterOrdered.IsSelected = false;
            this.FilterOrdered.Margin = new System.Windows.Forms.Padding(0);
            this.FilterOrdered.Name = "FilterOrdered";
            this.FilterOrdered.Padding = new System.Windows.Forms.Padding(0);
            this.FilterOrdered.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterOrdered.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterOrdered.Size = new System.Drawing.Size(70, 25);
            this.FilterOrdered.Text = "Ordered";
            this.FilterOrdered.Click += new System.EventHandler(this.FilterOrdered_Click);
            // 
            // FilterFavorites
            // 
            this.FilterFavorites.AutoToolTip = false;
            this.FilterFavorites.Checked = true;
            this.FilterFavorites.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterFavorites.Image = global::XwRemote.Properties.Resources.favs;
            this.FilterFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FilterFavorites.IsSelected = true;
            this.FilterFavorites.Margin = new System.Windows.Forms.Padding(0);
            this.FilterFavorites.Name = "FilterFavorites";
            this.FilterFavorites.Padding = new System.Windows.Forms.Padding(0);
            this.FilterFavorites.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.FilterFavorites.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.FilterFavorites.Size = new System.Drawing.Size(74, 25);
            this.FilterFavorites.Text = "Favorites";
            this.FilterFavorites.Click += new System.EventHandler(this.FilterFavorites_Click);
            // 
            // contextGroup
            // 
            this.contextGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripMenuItem,
            this.renameGroupToolStripMenuItem,
            this.deleteGroupToolStripMenuItem,
            this.ImportServer});
            this.contextGroup.Name = "contextMenuStrip1";
            this.contextGroup.Size = new System.Drawing.Size(173, 92);
            // 
            // addGroupToolStripMenuItem
            // 
            this.addGroupToolStripMenuItem.Image = global::XwRemote.Properties.Resources.folder;
            this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
            this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.addGroupToolStripMenuItem.Text = "Add Group";
            this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.AddGroup_Click);
            // 
            // renameGroupToolStripMenuItem
            // 
            this.renameGroupToolStripMenuItem.Image = global::XwRemote.Properties.Resources.pencil;
            this.renameGroupToolStripMenuItem.Name = "renameGroupToolStripMenuItem";
            this.renameGroupToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.renameGroupToolStripMenuItem.Text = "Rename Group";
            this.renameGroupToolStripMenuItem.Click += new System.EventHandler(this.RenameGroup_Click);
            // 
            // deleteGroupToolStripMenuItem
            // 
            this.deleteGroupToolStripMenuItem.Image = global::XwRemote.Properties.Resources.delete;
            this.deleteGroupToolStripMenuItem.Name = "deleteGroupToolStripMenuItem";
            this.deleteGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteGroupToolStripMenuItem.Text = "Delete Group";
            this.deleteGroupToolStripMenuItem.Click += new System.EventHandler(this.DeleteGroup_Click);
            // 
            // ImportServer
            // 
            this.ImportServer.Image = global::XwRemote.Properties.Resources.inout;
            this.ImportServer.Name = "ImportServer";
            this.ImportServer.Size = new System.Drawing.Size(172, 22);
            this.ImportServer.Text = "Import Server";
            this.ImportServer.Click += new System.EventHandler(this.ImportServer_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectBtn.Image = global::XwRemote.Properties.Resources.connect;
            this.ConnectBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ConnectBtn.Location = new System.Drawing.Point(422, 569);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(141, 57);
            this.ConnectBtn.TabIndex = 12;
            this.ConnectBtn.TabStop = false;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.Connect_Click);
            // 
            // NewFTP
            // 
            this.NewFTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewFTP.Image = global::XwRemote.Properties.Resources.ftp;
            this.NewFTP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewFTP.Location = new System.Drawing.Point(422, 139);
            this.NewFTP.Name = "NewFTP";
            this.NewFTP.Size = new System.Drawing.Size(141, 23);
            this.NewFTP.TabIndex = 4;
            this.NewFTP.TabStop = false;
            this.NewFTP.Text = "New FTP";
            this.NewFTP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewFTP.UseVisualStyleBackColor = true;
            this.NewFTP.Click += new System.EventHandler(this.NewFTP_Click);
            // 
            // NewRDP
            // 
            this.NewRDP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewRDP.Image = global::XwRemote.Properties.Resources.rdp;
            this.NewRDP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewRDP.Location = new System.Drawing.Point(422, 81);
            this.NewRDP.Name = "NewRDP";
            this.NewRDP.Size = new System.Drawing.Size(141, 23);
            this.NewRDP.TabIndex = 2;
            this.NewRDP.TabStop = false;
            this.NewRDP.Text = "New RDP";
            this.NewRDP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewRDP.UseVisualStyleBackColor = true;
            this.NewRDP.Click += new System.EventHandler(this.NewRDP_Click);
            // 
            // NewVNC
            // 
            this.NewVNC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewVNC.Image = global::XwRemote.Properties.Resources.vnc;
            this.NewVNC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewVNC.Location = new System.Drawing.Point(422, 255);
            this.NewVNC.Name = "NewVNC";
            this.NewVNC.Size = new System.Drawing.Size(141, 23);
            this.NewVNC.TabIndex = 8;
            this.NewVNC.TabStop = false;
            this.NewVNC.Text = "New VNC";
            this.NewVNC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewVNC.UseVisualStyleBackColor = true;
            this.NewVNC.Click += new System.EventHandler(this.NewVNC_Click);
            // 
            // NewSSH
            // 
            this.NewSSH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewSSH.Image = global::XwRemote.Properties.Resources.ssh;
            this.NewSSH.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewSSH.Location = new System.Drawing.Point(422, 110);
            this.NewSSH.Name = "NewSSH";
            this.NewSSH.Size = new System.Drawing.Size(141, 23);
            this.NewSSH.TabIndex = 3;
            this.NewSSH.TabStop = false;
            this.NewSSH.Text = "New SSH";
            this.NewSSH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewSSH.UseVisualStyleBackColor = true;
            this.NewSSH.Click += new System.EventHandler(this.newSSH_Click);
            // 
            // EditServer
            // 
            this.EditServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditServer.Image = global::XwRemote.Properties.Resources.pencil;
            this.EditServer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EditServer.Location = new System.Drawing.Point(422, 540);
            this.EditServer.Name = "EditServer";
            this.EditServer.Size = new System.Drawing.Size(141, 23);
            this.EditServer.TabIndex = 11;
            this.EditServer.TabStop = false;
            this.EditServer.Text = "Edit Server";
            this.EditServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditServer.UseVisualStyleBackColor = true;
            this.EditServer.Click += new System.EventHandler(this.EditServer_Click);
            // 
            // DeleteServer
            // 
            this.DeleteServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteServer.Image = global::XwRemote.Properties.Resources.delete;
            this.DeleteServer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DeleteServer.Location = new System.Drawing.Point(422, 511);
            this.DeleteServer.Name = "DeleteServer";
            this.DeleteServer.Size = new System.Drawing.Size(141, 23);
            this.DeleteServer.TabIndex = 10;
            this.DeleteServer.TabStop = false;
            this.DeleteServer.Text = "Delete Server";
            this.DeleteServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteServer.UseVisualStyleBackColor = true;
            this.DeleteServer.Click += new System.EventHandler(this.DeleteServer_Click);
            // 
            // contextServer
            // 
            this.contextServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editServerToolStripMenuItem,
            this.deleteServerToolStripMenuItem,
            this.addToFavoritesToolStripMenuItem,
            this.toolStripSeparator3,
            this.makeCopyToolStripMenuItem,
            this.ExportServer});
            this.contextServer.Name = "contextServer";
            this.contextServer.Size = new System.Drawing.Size(161, 120);
            // 
            // editServerToolStripMenuItem
            // 
            this.editServerToolStripMenuItem.Image = global::XwRemote.Properties.Resources.pencil;
            this.editServerToolStripMenuItem.Name = "editServerToolStripMenuItem";
            this.editServerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.editServerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.editServerToolStripMenuItem.Text = "Edit Server";
            this.editServerToolStripMenuItem.Click += new System.EventHandler(this.EditServer_Click);
            // 
            // deleteServerToolStripMenuItem
            // 
            this.deleteServerToolStripMenuItem.Image = global::XwRemote.Properties.Resources.delete;
            this.deleteServerToolStripMenuItem.Name = "deleteServerToolStripMenuItem";
            this.deleteServerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteServerToolStripMenuItem.Text = "Delete Server";
            this.deleteServerToolStripMenuItem.Click += new System.EventHandler(this.DeleteServer_Click);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Image = global::XwRemote.Properties.Resources.favs;
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Add to Favorites";
            this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // makeCopyToolStripMenuItem
            // 
            this.makeCopyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAsRDP,
            this.copyAsSSH,
            this.copyAsFTP,
            this.copyAsSFTP,
            this.copyAsAWSS3,
            this.copyAsAzureFile,
            this.copyAsVNC,
            this.copyAsIE});
            this.makeCopyToolStripMenuItem.Image = global::XwRemote.Properties.Resources.copy;
            this.makeCopyToolStripMenuItem.Name = "makeCopyToolStripMenuItem";
            this.makeCopyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.makeCopyToolStripMenuItem.Text = "Make Copy as...";
            // 
            // copyAsRDP
            // 
            this.copyAsRDP.Image = global::XwRemote.Properties.Resources.rdp;
            this.copyAsRDP.Name = "copyAsRDP";
            this.copyAsRDP.Size = new System.Drawing.Size(125, 22);
            this.copyAsRDP.Text = "RDP";
            this.copyAsRDP.Click += new System.EventHandler(this.copyAsRDP_Click);
            // 
            // copyAsSSH
            // 
            this.copyAsSSH.Image = global::XwRemote.Properties.Resources.ssh;
            this.copyAsSSH.Name = "copyAsSSH";
            this.copyAsSSH.Size = new System.Drawing.Size(125, 22);
            this.copyAsSSH.Text = "SSH";
            this.copyAsSSH.Click += new System.EventHandler(this.copyAsSSH_Click);
            // 
            // copyAsFTP
            // 
            this.copyAsFTP.Image = global::XwRemote.Properties.Resources.ftp;
            this.copyAsFTP.Name = "copyAsFTP";
            this.copyAsFTP.Size = new System.Drawing.Size(125, 22);
            this.copyAsFTP.Text = "FTP";
            this.copyAsFTP.Click += new System.EventHandler(this.copyAsFTP_Click);
            // 
            // copyAsSFTP
            // 
            this.copyAsSFTP.Image = global::XwRemote.Properties.Resources.sftp;
            this.copyAsSFTP.Name = "copyAsSFTP";
            this.copyAsSFTP.Size = new System.Drawing.Size(125, 22);
            this.copyAsSFTP.Text = "SFTP";
            this.copyAsSFTP.Click += new System.EventHandler(this.copyAsSFTP_Click);
            // 
            // copyAsAWSS3
            // 
            this.copyAsAWSS3.Image = global::XwRemote.Properties.Resources.s3;
            this.copyAsAWSS3.Name = "copyAsAWSS3";
            this.copyAsAWSS3.Size = new System.Drawing.Size(125, 22);
            this.copyAsAWSS3.Text = "AWS S3";
            this.copyAsAWSS3.Click += new System.EventHandler(this.copyAsAWSS3_Click);
            // 
            // copyAsAzureFile
            // 
            this.copyAsAzureFile.Image = global::XwRemote.Properties.Resources.azure;
            this.copyAsAzureFile.Name = "copyAsAzureFile";
            this.copyAsAzureFile.Size = new System.Drawing.Size(125, 22);
            this.copyAsAzureFile.Text = "Azure File";
            this.copyAsAzureFile.Click += new System.EventHandler(this.copyAsAzureFile_Click);
            // 
            // copyAsVNC
            // 
            this.copyAsVNC.Image = global::XwRemote.Properties.Resources.vnc;
            this.copyAsVNC.Name = "copyAsVNC";
            this.copyAsVNC.Size = new System.Drawing.Size(125, 22);
            this.copyAsVNC.Text = "VNC";
            this.copyAsVNC.Click += new System.EventHandler(this.copyAsVNC_Click);
            // 
            // copyAsIE
            // 
            this.copyAsIE.Image = global::XwRemote.Properties.Resources.IE;
            this.copyAsIE.Name = "copyAsIE";
            this.copyAsIE.Size = new System.Drawing.Size(125, 22);
            this.copyAsIE.Text = "IE";
            this.copyAsIE.Click += new System.EventHandler(this.copyAsIE_Click);
            // 
            // ExportServer
            // 
            this.ExportServer.Image = global::XwRemote.Properties.Resources.inout;
            this.ExportServer.Name = "ExportServer";
            this.ExportServer.Size = new System.Drawing.Size(160, 22);
            this.ExportServer.Text = "Export Server";
            this.ExportServer.Click += new System.EventHandler(this.ExportServer_Click);
            // 
            // NewIE
            // 
            this.NewIE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewIE.Image = global::XwRemote.Properties.Resources.IE;
            this.NewIE.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewIE.Location = new System.Drawing.Point(422, 284);
            this.NewIE.Name = "NewIE";
            this.NewIE.Size = new System.Drawing.Size(141, 23);
            this.NewIE.TabIndex = 9;
            this.NewIE.TabStop = false;
            this.NewIE.Text = "New IE";
            this.NewIE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewIE.UseVisualStyleBackColor = true;
            this.NewIE.Click += new System.EventHandler(this.newIE_Click);
            // 
            // dialogHeader1
            // 
            this.dialogHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogHeader1.Gradient1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient3 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.dialogHeader1.Gradient4 = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dialogHeader1.HeaderDescription = "This is where you manage your server connections";
            this.dialogHeader1.HeaderImage = global::XwRemote.Properties.Resources.xwremote1;
            this.dialogHeader1.HeaderTitle = "Connections";
            this.dialogHeader1.Location = new System.Drawing.Point(0, 0);
            this.dialogHeader1.Name = "dialogHeader1";
            this.dialogHeader1.Size = new System.Drawing.Size(572, 50);
            this.dialogHeader1.TabIndex = 8;
            this.dialogHeader1.TabStop = false;
            // 
            // NewSFTP
            // 
            this.NewSFTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewSFTP.Image = global::XwRemote.Properties.Resources.sftp;
            this.NewSFTP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewSFTP.Location = new System.Drawing.Point(422, 168);
            this.NewSFTP.Name = "NewSFTP";
            this.NewSFTP.Size = new System.Drawing.Size(141, 23);
            this.NewSFTP.TabIndex = 5;
            this.NewSFTP.TabStop = false;
            this.NewSFTP.Text = "New SFTP";
            this.NewSFTP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewSFTP.UseVisualStyleBackColor = true;
            this.NewSFTP.Click += new System.EventHandler(this.NewSFTP_Click);
            // 
            // NewS3
            // 
            this.NewS3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewS3.Image = global::XwRemote.Properties.Resources.s3;
            this.NewS3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewS3.Location = new System.Drawing.Point(422, 197);
            this.NewS3.Name = "NewS3";
            this.NewS3.Size = new System.Drawing.Size(141, 23);
            this.NewS3.TabIndex = 6;
            this.NewS3.TabStop = false;
            this.NewS3.Text = "New AWS S3";
            this.NewS3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewS3.UseVisualStyleBackColor = true;
            this.NewS3.Click += new System.EventHandler(this.NewS3_Click);
            // 
            // NewAzureFile
            // 
            this.NewAzureFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewAzureFile.Image = global::XwRemote.Properties.Resources.azure;
            this.NewAzureFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewAzureFile.Location = new System.Drawing.Point(422, 226);
            this.NewAzureFile.Name = "NewAzureFile";
            this.NewAzureFile.Size = new System.Drawing.Size(141, 23);
            this.NewAzureFile.TabIndex = 7;
            this.NewAzureFile.TabStop = false;
            this.NewAzureFile.Text = "New Azure File";
            this.NewAzureFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewAzureFile.UseVisualStyleBackColor = true;
            this.NewAzureFile.Click += new System.EventHandler(this.NewAzureFile_Click);
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearch.Location = new System.Drawing.Point(32, 604);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(366, 20);
            this.textSearch.TabIndex = 1;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::XwRemote.Properties.Resources.magnifier;
            this.pictureBox1.Location = new System.Drawing.Point(12, 604);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 20);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // ServerManager
            // 
            this.AcceptButton = this.ConnectBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 636);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.NewAzureFile);
            this.Controls.Add(this.NewS3);
            this.Controls.Add(this.NewSFTP);
            this.Controls.Add(this.dialogHeader1);
            this.Controls.Add(this.NewRDP);
            this.Controls.Add(this.NewSSH);
            this.Controls.Add(this.NewVNC);
            this.Controls.Add(this.NewFTP);
            this.Controls.Add(this.NewIE);
            this.Controls.Add(this.DeleteServer);
            this.Controls.Add(this.EditServer);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.toolStripContainer1);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "ServerManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server Manager";
            this.Activated += new System.EventHandler(this.ServerManager_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerManager_FormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.contextGroup.ResumeLayout(false);
            this.contextServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button NewFTP;
        private System.Windows.Forms.Button NewRDP;
        private System.Windows.Forms.Button NewVNC;
        private System.Windows.Forms.Button NewSSH;
        private System.Windows.Forms.Button EditServer;
        private System.Windows.Forms.Button DeleteServer;
        private Messir.Windows.Forms.TabStrip tabStrip1;
        private Messir.Windows.Forms.TabStripButton FilterGrouped;
        private Messir.Windows.Forms.TabStripButton FilterOrdered;
        private Messir.Windows.Forms.TabStripButton FilterFavorites;
        private System.Windows.Forms.TreeView treeServers;
        private XwMaxLib.UI.DialogHeader dialogHeader1;
        private System.Windows.Forms.ContextMenuStrip contextGroup;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGroupToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextServer;
        private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button NewIE;
        private System.Windows.Forms.ToolStripMenuItem copyAsFTP;
        private System.Windows.Forms.ToolStripMenuItem copyAsRDP;
        private System.Windows.Forms.ToolStripMenuItem copyAsVNC;
        private System.Windows.Forms.ToolStripMenuItem copyAsSSH;
        private System.Windows.Forms.ToolStripMenuItem copyAsIE;
        private System.Windows.Forms.Button NewSFTP;
        private System.Windows.Forms.Button NewS3;
        private System.Windows.Forms.Button NewAzureFile;
        private System.Windows.Forms.ToolStripMenuItem copyAsSFTP;
        private System.Windows.Forms.ToolStripMenuItem copyAsAWSS3;
        private System.Windows.Forms.ToolStripMenuItem copyAsAzureFile;
        private System.Windows.Forms.ToolStripMenuItem ImportServer;
        private System.Windows.Forms.ToolStripMenuItem ExportServer;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}