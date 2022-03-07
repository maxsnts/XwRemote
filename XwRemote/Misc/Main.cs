using KRBTabControlNS.CustomTab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Misc;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote
{
    public partial class Main : Form
    {
        //*************************************************************************************************************
        public static ImageList myImageList = new ImageList();
        public static Config config = new Config();
        public static List<Server> servers = new List<Server>();
        public static string UpdateRepo = "XwRemote";
        public static string CurrentVersion = "";

        private System.Windows.Forms.Timer timerClose = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerCloseTab = new System.Windows.Forms.Timer();
        private bool retryClose = false;
        private TabPageEx tryCloseTab = null;
        private bool resized = false;

        //*************************************************************************************************************
        public Main()
        {
            InitializeComponent();
            CurrentVersion += System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetAssembly(typeof(Main)).Location).FileVersion.ToString();
            Text = $"XwRemote {CurrentVersion}";
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            config.Load();

            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
                if (arg == "-crash")
                    throw new Exception("Exception Test");

            if (config.GetValue("UI_CLOSE_TO_TRAY").ToBoolOrDefault(false) ||
                config.GetValue("UI_MINIMIZE_TO_TRAY").ToBoolOrDefault(false))
                appTrayIcon.Visible = true;

            Toolbar_Updates.Image = null;

            myImageList.ColorDepth = ColorDepth.Depth32Bit;
            myImageList.Images.Add("folder", Resources.folder);
            myImageList.Images.Add("rdp", Resources.rdp);
            myImageList.Images.Add("vnc", Resources.vnc);
            myImageList.Images.Add("ftp", Resources.ftp);
            myImageList.Images.Add("ssh", Resources.ssh);
            myImageList.Images.Add("ie", Resources.IE);
            myImageList.Images.Add("sql", Resources.database);
            myImageList.Images.Add("sftp", Resources.sftp);
            myImageList.Images.Add("s3", Resources.s3);
            myImageList.Images.Add("azure", Resources.azure);
            ServerTabs.ImageList = myImageList;

            Visible = false;
            WindowState = (FormWindowState)FormWindowState.Parse(WindowState.GetType(),
                config.GetValue("MainFormState", "Normal"));
            int X = config.GetValue("MainFormLocationX").ToIntOrDefault(50);
            int Y = config.GetValue("MainFormLocationY").ToIntOrDefault(50);
            int W = config.GetValue("MainFormLocationW").ToIntOrDefault(700);
            int H = config.GetValue("MainFormLocationH").ToIntOrDefault(600);
            Rectangle position = new Rectangle(X, Y, W, H);
            Rectangle screen = SystemInformation.VirtualScreen;
            screen.Inflate(20, 20);
            if (screen.Contains(position))
            {
                Location = position.Location;
                Size = position.Size;
            }
            Visible = true;

            timerClose.Interval = 100;
            timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            timerCloseTab.Interval = 100;
            timerCloseTab.Tick += new System.EventHandler(this.timerCloseTab_Tick);

            LoadFavorites();
            resized = false;
        }

        //*************************************************************************************************************
        private void Main_Shown(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadCheckUpdates), this);
        }

        //*************************************************************************************************************
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (config.GetValue("UI_CLOSE_TO_TRAY").ToBoolOrDefault(false))
            {
                if (appTrayIcon.Visible)
                {
                    Hide();
                    e.Cancel = true;
                    return;
                }
            }

            if (!retryClose) //first try
            {
                if (ServerTabs.TabPages.Count > 0)
                {
                    if (MessageBox.Show("You have open connections. \r\nAre you sure you want to close application?",
                        "Close?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                        retryClose = false;
                        return;
                    }
                }

                config.SetValue("MainFormState", WindowState.ToString());
                config.SetValue("MainFormLocationX", Location.X.ToString());
                config.SetValue("MainFormLocationY", Location.Y.ToString());
                config.SetValue("MainFormLocationW", Size.Width.ToString());
                config.SetValue("MainFormLocationH", Size.Height.ToString());
            }

            foreach (TabPageEx page in ServerTabs.TabPages)
            {
                Server server = (Server)page.SomeUserObject;
                if (server == null)
                    return;

                if (!server.OnTabClose())
                    timerClose.Start();
            }
        }

        //*************************************************************************************************************
        private void Toolbar_ServerManager_Click(object sender, EventArgs e)
        {
            ServerManager serverManager = new ServerManager(this);
            if (serverManager.ShowDialog(this) == DialogResult.OK)
                ConnectToServer(serverManager.ConnectToThisServer);
            LoadFavorites();
        }

        //*************************************************************************************************************
        private void timerClose_Tick(object sender, EventArgs e)
        {
            retryClose = true;
            timerClose.Stop();
            Close();
        }

        //*************************************************************************************************************
        private void timerCloseTab_Tick(object sender, EventArgs e)
        {
            timerCloseTab.Stop();
            ServerTabs.CloseTabByButton(tryCloseTab);
        }

        //*************************************************************************************************************
        public void ConnectToServer(Server server)
        {
            TabPageEx tab = new TabPageEx(server.Name);
            tab.Tag = server;
            ServerTabs._tabCloseBtn = KRBTabControlNS.CustomTab.KRBTabControl.TabCloseImage.Normal;
            ServerTabs.TabPages.Add(tab);
            tab.ImageIndex = (int)server.Type;
            tab.SomeUserObject = server;
            server.Open(tab);
            ServerTabs.SelectTab(ServerTabs.TabPages.Count - 1);
            FocusSelectedTab();
        }

        //*************************************************************************************************************
        public bool IsServerOpen(Server server)
        {
            foreach (TabPageEx page in ServerTabs.TabPages)
            {
                if (((Server)page.Tag).ID == server.ID)
                    return true;
            }

            return false;
        }

        //*************************************************************************************************************
        private void ServerTabs_TabClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TabPageEx tab = ((TabPageEx)ServerTabs.SelectedTab);
            if (tab == null)
                return;

            Server server = (Server)((TabPageEx)tab).SomeUserObject;
            if (server == null)
                return;

            if (tryCloseTab == null) //first try to close?
            {
                if (MessageBox.Show("You are about to close a server tab?", "Close?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            toolFullScreen.Enabled = false;
            server.OnTabClose();

            //I don't think is necessary... lets see
            //Focus();
            //FocusSelectedTab();
        }

        //*************************************************************************************************************
        private void ServerTabs_Selected(object sender, TabControlEventArgs e)
        {
            FocusSelectedTab();
        }

        //*************************************************************************************************************
        private void Main_Activated(object sender, EventArgs e)
        {
            FocusSelectedTab();
        }

        //*************************************************************************************************************
        public void FocusSelectedTab()
        {
            TabPage tab = ServerTabs.SelectedTab;
            if (tab == null)
                return;

            Server server = (Server)((TabPageEx)tab).SomeUserObject;
            ServerTabs.TabGradient.GradientStyle = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            ServerTabs.TabGradient.ColorEnd = Color.FromArgb(server.TabColor);
            server?.OnTabFocus();

            if (server.Type == ServerType.RDP)
            {
                toolFullScreen.Enabled = true;
            }
            else
            {
                toolFullScreen.Enabled = false;
            }
        }

        //*************************************************************************************************************
        static void ThreadCheckUpdates(object state)
        {
            try
            {
                Main param = (Main)state;

                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString($"https://github.com/maxsnts/{Main.UpdateRepo}/releases/latest");
                    Match m = Regex.Match(content, @"(?isx)/releases/tag/v(?<VERSION>\d+\.\d+\.\d+\.\d+)""");
                    string latestVersion = m.Result("${VERSION}");

                    if (latestVersion != Main.CurrentVersion)
                    {
                        param.BeginInvoke((MethodInvoker)delegate
                        {
                            param.Toolbar_Updates.Text = "New updates available";
                            param.Toolbar_Updates.Enabled = true;
                            param.Toolbar_Updates.Image = Resources.accept;
                        });

                        try
                        {
                            while (true)
                            {
                                Thread.Sleep(1000);
                                param.BeginInvoke((MethodInvoker)delegate
                                {
                                    param.Toolbar_Updates.Image = Resources.play;
                                });
                                Thread.Sleep(1000);
                                param.BeginInvoke((MethodInvoker)delegate
                                {
                                    param.Toolbar_Updates.Image = Resources.accept;
                                });
                            }
                        }
                        catch { /* Not important */ }
                    }
                }
            }
            catch
            { /* Not important */}
        }

        //*************************************************************************************************************
        private void MainToolbar_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }

        //*************************************************************************************************************
        private void toolStripUpdates_Click(object sender, EventArgs e)
        {
            if (ServerTabs.TabPages.Count > 0)
            {
                MessageBox.Show("You have open connections. \r\nClose server connections before update XwRemote",
                    "Connections...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Stuff stuff = new Stuff();
            stuff.faTabStrip1.SelectedItem = stuff.faTabUpdates;
            stuff.ShowDialog();
        }

        //*************************************************************************************************************
        private void LoadFavorites()
        {
            Toolbar_Favorites.DropDownItems.Clear();

            foreach (Server server in Main.servers)
            {
                if (!server.IsFavorite)
                    continue;

                ToolStripItem item = Toolbar_Favorites.DropDownItems.Add(
                    String.Format("{0} ({1})", server.Name, server.Host),
                    server.GetImage());

                item.Tag = server;
            }

            if (Toolbar_Favorites.DropDownItems.Count == 0)
            {
                ToolStripItem item = Toolbar_Favorites.DropDownItems.Add(
                    "no favorites: mark servers as favorites on the server settings window", null);
                item.Enabled = false;
            }
        }

        //*************************************************************************************************************
        private void Toolbar_Favorites_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
                return;

            ConnectToServer((Server)e.ClickedItem.Tag);
        }

        //*************************************************************************************************************
        private void toolSettings_Click(object sender, EventArgs e)
        {
            GlobalSettings settings = new GlobalSettings();
            settings.ShowDialog();

            if (config.GetValue("UI_CLOSE_TO_TRAY").ToBoolOrDefault(false) ||
               config.GetValue("UI_MINIMIZE_TO_TRAY").ToBoolOrDefault(false))
                appTrayIcon.Visible = true;
            else
                appTrayIcon.Visible = false;
        }

        //*************************************************************************************************************
        private void ServerTabs_MouseMove(object sender, MouseEventArgs e)
        {
            BringToFront();
        }

        //*************************************************************************************************************
        private void Toolbar_CanYouHelp_Click(object sender, EventArgs e)
        {
            Stuff stuff = new Stuff();
            stuff.faTabStrip1.SelectedItem = stuff.faTabDonation;
            stuff.ShowDialog();
        }

        //*************************************************************************************************************
        private void Toolbar_Stuff_Click(object sender, EventArgs e)
        {
            Stuff stuff = new Stuff();
            stuff.ShowDialog();
        }

        //*************************************************************************************************************
        private void toolScanner_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();
            scanner.Show(this);
        }

        //*************************************************************************************************************
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appTrayIcon.Visible = false;
            Application.Exit();
        }

        //*************************************************************************************************************
        private void showMainApplicationWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        //*************************************************************************************************************
        private void appTrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        //*************************************************************************************************************
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xF020;
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    {
                        if (config.GetValue("UI_MINIMIZE_TO_TRAY").ToBoolOrDefault(false))
                        {
                            int command = m.WParam.ToInt32() & 0xfff0;
                            if (command == SC_MINIMIZE)
                            {
                                Hide();
                                return;
                            }
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        //*************************************************************************************************************
        private void resetPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = (FormWindowState)FormWindowState.Parse(WindowState.GetType(),
               config.GetValue("MainFormState", "Normal"));
            Rectangle screen = SystemInformation.VirtualScreen;
            screen.Inflate(-200, -100);
            Location = screen.Location;
            Size = screen.Size;
            Show();
        }

        //*************************************************************************************************************
        private void appTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        //*************************************************************************************************************
        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            if (resized)
            {
                foreach (var tab in ServerTabs.TabPages)
                {
                    Server server = (Server)((TabPageEx)tab).SomeUserObject;
                    server?.ResizeEnd();
                }
                resized = false;
            }
        }

        //*************************************************************************************************************
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            resized = true;
        }

        //*************************************************************************************************************
        FormWindowState LastWindowState = FormWindowState.Normal;
        private void Main_Resize(object sender, EventArgs e)
        {
            // When window state changes
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;
                Main_ResizeEnd(sender, e);
            }
        }

        //*************************************************************************************************************
        private void toolFullScreen_Click(object sender, EventArgs e)
        {
            TabPageEx tab = ((TabPageEx)ServerTabs.SelectedTab);
            if (tab == null)
                return;

            Server server = (Server)((TabPageEx)tab).SomeUserObject;
            if (server == null)
                return;

            server.FullScreen();
        }
    }
}
