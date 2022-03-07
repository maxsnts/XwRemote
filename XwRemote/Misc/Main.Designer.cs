namespace XwRemote
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.ServerTabs = new KRBTabControlNS.CustomTab.KRBTabControl();
            this.Toolbar_ServerManager = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Updates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Favorites = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSettings = new System.Windows.Forms.ToolStripButton();
            this.toolScanner = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_CanYouHelp = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Stuff = new System.Windows.Forms.ToolStripButton();
            this.appTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainApplicationWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolFullScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.MainToolbar.SuspendLayout();
            this.appTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ServerTabs);
            this.toolStripContainer1.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(869, 632);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 25);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(869, 632);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // ServerTabs
            // 
            this.ServerTabs.AllowDrop = true;
            this.ServerTabs.BackgroundHatcher.BackColor = System.Drawing.Color.Gray;
            this.ServerTabs.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.ServerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ServerTabs.ItemSize = new System.Drawing.Size(73, 28);
            this.ServerTabs.Location = new System.Drawing.Point(0, 0);
            this.ServerTabs.Name = "ServerTabs";
            this.ServerTabs.Size = new System.Drawing.Size(869, 632);
            this.ServerTabs.TabGradient.ColorEnd = System.Drawing.Color.LightSteelBlue;
            this.ServerTabs.TabGradient.GradientStyle = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.ServerTabs.TabIndex = 0;
            this.ServerTabs.TabStyles = KRBTabControlNS.CustomTab.KRBTabControl.TabStyle.Sequence;
            this.ServerTabs.UpDownStyle = KRBTabControlNS.CustomTab.KRBTabControl.UpDown32Style.OfficeBlue;
            this.ServerTabs.TabClosing += new System.ComponentModel.CancelEventHandler(this.ServerTabs_TabClosing);
            this.ServerTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.ServerTabs_Selected);
            this.ServerTabs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ServerTabs_MouseMove);
            // 
            // Toolbar_ServerManager
            // 
            this.Toolbar_ServerManager.Image = global::XwRemote.Properties.Resources.server_connect;
            this.Toolbar_ServerManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Toolbar_ServerManager.Name = "Toolbar_ServerManager";
            this.Toolbar_ServerManager.Size = new System.Drawing.Size(109, 22);
            this.Toolbar_ServerManager.Text = "Server Manager";
            this.Toolbar_ServerManager.ToolTipText = "Server Manager";
            this.Toolbar_ServerManager.Click += new System.EventHandler(this.Toolbar_ServerManager_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Toolbar_Updates
            // 
            this.Toolbar_Updates.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Toolbar_Updates.Enabled = false;
            this.Toolbar_Updates.Image = ((System.Drawing.Image)(resources.GetObject("Toolbar_Updates.Image")));
            this.Toolbar_Updates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Toolbar_Updates.Name = "Toolbar_Updates";
            this.Toolbar_Updates.Size = new System.Drawing.Size(137, 22);
            this.Toolbar_Updates.Text = "No updates available";
            this.Toolbar_Updates.Click += new System.EventHandler(this.toolStripUpdates_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // MainToolbar
            // 
            this.MainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Toolbar_ServerManager,
            this.toolStripSeparator4,
            this.Toolbar_Favorites,
            this.toolStripSeparator5,
            this.toolSettings,
            this.toolScanner,
            this.Toolbar_CanYouHelp,
            this.toolStripSeparator3,
            this.Toolbar_Stuff,
            this.toolStripSeparator2,
            this.Toolbar_Updates,
            this.toolFullScreen});
            this.MainToolbar.Location = new System.Drawing.Point(0, 0);
            this.MainToolbar.Name = "MainToolbar";
            this.MainToolbar.Size = new System.Drawing.Size(869, 25);
            this.MainToolbar.TabIndex = 1;
            this.MainToolbar.Text = "toolStrip1";
            this.MainToolbar.MouseEnter += new System.EventHandler(this.MainToolbar_MouseEnter);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // Toolbar_Favorites
            // 
            this.Toolbar_Favorites.Image = global::XwRemote.Properties.Resources.favs;
            this.Toolbar_Favorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Toolbar_Favorites.Name = "Toolbar_Favorites";
            this.Toolbar_Favorites.Size = new System.Drawing.Size(83, 22);
            this.Toolbar_Favorites.Text = "Favorites";
            this.Toolbar_Favorites.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Toolbar_Favorites_DropDownItemClicked);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSettings
            // 
            this.toolSettings.Image = global::XwRemote.Properties.Resources.settings;
            this.toolSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSettings.Name = "toolSettings";
            this.toolSettings.Size = new System.Drawing.Size(69, 22);
            this.toolSettings.Text = "Settings";
            this.toolSettings.Click += new System.EventHandler(this.toolSettings_Click);
            // 
            // toolScanner
            // 
            this.toolScanner.Image = global::XwRemote.Properties.Resources.magnifier;
            this.toolScanner.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolScanner.Name = "toolScanner";
            this.toolScanner.Size = new System.Drawing.Size(69, 22);
            this.toolScanner.Text = "Scanner";
            this.toolScanner.ToolTipText = "Network scanner";
            this.toolScanner.Click += new System.EventHandler(this.toolScanner_Click);
            // 
            // Toolbar_CanYouHelp
            // 
            this.Toolbar_CanYouHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Toolbar_CanYouHelp.Image = global::XwRemote.Properties.Resources.paypal;
            this.Toolbar_CanYouHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Toolbar_CanYouHelp.Name = "Toolbar_CanYouHelp";
            this.Toolbar_CanYouHelp.Size = new System.Drawing.Size(102, 22);
            this.Toolbar_CanYouHelp.Text = "Can you help?";
            this.Toolbar_CanYouHelp.Click += new System.EventHandler(this.Toolbar_CanYouHelp_Click);
            // 
            // Toolbar_Stuff
            // 
            this.Toolbar_Stuff.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Toolbar_Stuff.Image = global::XwRemote.Properties.Resources.help;
            this.Toolbar_Stuff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Toolbar_Stuff.Name = "Toolbar_Stuff";
            this.Toolbar_Stuff.Size = new System.Drawing.Size(48, 22);
            this.Toolbar_Stuff.Text = "Info";
            this.Toolbar_Stuff.Click += new System.EventHandler(this.Toolbar_Stuff_Click);
            // 
            // appTrayMenu
            // 
            this.appTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainApplicationWindowToolStripMenuItem,
            this.resetPositionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.appTrayMenu.Name = "appTrayMenu";
            this.appTrayMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // showMainApplicationWindowToolStripMenuItem
            // 
            this.showMainApplicationWindowToolStripMenuItem.Name = "showMainApplicationWindowToolStripMenuItem";
            this.showMainApplicationWindowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.showMainApplicationWindowToolStripMenuItem.Text = "Show window";
            this.showMainApplicationWindowToolStripMenuItem.Click += new System.EventHandler(this.showMainApplicationWindowToolStripMenuItem_Click);
            // 
            // resetPositionToolStripMenuItem
            // 
            this.resetPositionToolStripMenuItem.Name = "resetPositionToolStripMenuItem";
            this.resetPositionToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.resetPositionToolStripMenuItem.Text = "Reset position";
            this.resetPositionToolStripMenuItem.Click += new System.EventHandler(this.resetPositionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // appTrayIcon
            // 
            this.appTrayIcon.BalloonTipTitle = "XwComPortDetector";
            this.appTrayIcon.ContextMenuStrip = this.appTrayMenu;
            this.appTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("appTrayIcon.Icon")));
            this.appTrayIcon.Text = "AppTrayIcon";
            this.appTrayIcon.DoubleClick += new System.EventHandler(this.appTrayIcon_DoubleClick);
            this.appTrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.appTrayIcon_MouseDoubleClick);
            // 
            // toolFullScreen
            // 
            this.toolFullScreen.Enabled = false;
            this.toolFullScreen.Image = global::XwRemote.Properties.Resources.fullscreen;
            this.toolFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFullScreen.Name = "toolFullScreen";
            this.toolFullScreen.Size = new System.Drawing.Size(84, 22);
            this.toolFullScreen.Text = "Full Screen";
            this.toolFullScreen.Click += new System.EventHandler(this.toolFullScreen_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 657);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.MainToolbar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "XwRemote";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.MainToolbar.ResumeLayout(false);
            this.MainToolbar.PerformLayout();
            this.appTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public KRBTabControlNS.CustomTab.KRBTabControl ServerTabs;
        private System.Windows.Forms.ToolStripButton Toolbar_ServerManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip MainToolbar;
        public System.Windows.Forms.ToolStripButton Toolbar_Updates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton Toolbar_Favorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolScanner;
        private System.Windows.Forms.ToolStripButton Toolbar_CanYouHelp;
        private System.Windows.Forms.ToolStripButton Toolbar_Stuff;
        private System.Windows.Forms.ToolStripButton toolSettings;
        private System.Windows.Forms.ContextMenuStrip appTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem showMainApplicationWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPositionToolStripMenuItem;
        public System.Windows.Forms.NotifyIcon appTrayIcon;
        private System.Windows.Forms.ToolStripButton toolFullScreen;
    }
}

