using ShellDll;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwRemote.Properties;
using XwRemote.Servers.IO;
using XwRemote.Settings;
using static XwRemote.Servers.IO.XwRemoteIO;

namespace XwRemote.Servers
{
    public partial class IOForm : Form
    {
        public Server server = null;
        public ToolTip localPinTip = new ToolTip();
        public ToolTip remotePinTip = new ToolTip();
        public ToolTip linkTip = new ToolTip();
        public bool SkipCheckLink = false;
        private XwRemoteIO remoteIO = new XwRemoteIO();
        private bool Closing = false;

        //*************************************************************************************************************
        public IOForm(Server srv)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            ShellImageList.SetSmallImageList(LocalList);
            ShellImageList.SetSmallImageList(RemoteList);
            statusMain.Items["statusMainLabel"].Text = "";
            statusLocal.Items["statusLocalLabel"].Text = "";
            statusRemote.Items["statusRemoteLabel"].Text = "";

            LocalList.Init(this);
            RemoteList.Init(this, remoteIO);
            QueueList.Init(this, remoteIO);

            localPinTip.SetToolTip(LocalPin, "");
            remotePinTip.SetToolTip(RemotePin, "");
            linkTip.SetToolTip(LinkPath, "");
        }

        //*************************************************************************************************************
        private async void OnShown(object sender, EventArgs e)
        {
            await Connect();
        }

        //*************************************************************************************************************
        public async Task Connect()
        {
            loadingCircle1.Active = true;
            loadingCircle1.InnerCircleRadius = 15;
            loadingCircle1.OuterCircleRadius = 30;
            loadingCircle1.SpokeThickness = 5;
            loadingCircle1.Top = (this.Height / 2) + 10;
            loadingCircle1.Left = (this.Width / 2) - 40;
            loadingCircle1.Visible = true;
            loadingCircle1.BringToFront();
            SetStatusText("Connecting...");

            Update();

            XwRemoteIOResult result = null;
            switch (server.Type)
            {
                case ServerType.FTP:
                result = await remoteIO.ConnectToFTP(
                        server.Host,
                        server.Port,
                        server.Username,
                        server.Password,
                        server.FtpDataType, 
                        server.Encryption);
                    break;
                case ServerType.SFTP:
                result = await remoteIO.ConnectToSFTP(
                        server.Host,
                        server.Port,
                        server.Username,
                        server.Password,
                        server.SshKey);
                    break;
                case ServerType.AWSS3:
                result = await remoteIO.ConnectToAWSS3(
                        server.Host,
                        server.Username,
                        server.Password);
                    break;
                case ServerType.AZUREFILE:
                result = await remoteIO.ConnectToAZUREFILE(
                        server.Username,
                        server.Password);
                    break;
                default:
                    throw new Exception("Invalid engine");
            }

            if (result.Success)
            {
                SetMainStatusText($"Connected to {server.Name} ({server.Host}) with user {server.Username}");
                Log(result.Message);
                await RemoteList.Load();
            }
            else
            {
                loadingCircle1.Visible = false;
                SetStatusText(result.Message);
            }
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            Closing = true;
            remoteIO.Close();
            return true;
        }

        //*************************************************************************************************************
        public void OnTabFocus()
        {
            
        }

        //*************************************************************************************************************
        public void SetStatusText(string txt)
        {
            statusLabel.Text = txt;
            statusLabel.AutoSize = true;
            statusLabel.Top = (this.Height / 2) - (statusLabel.Height / 2) - 10;
            statusLabel.Left = (this.Width / 2) - (statusLabel.Width / 2);
        }

        //*************************************************************************************************************
        public void SetMainStatusText(string txt)
        {
            if (statusMain.Items.ContainsKey("statusMainLabel"))
                statusMain.Items["statusMainLabel"].Text = txt;
        }

        //*************************************************************************************************************
        public void SetLocalStatusText(string txt)
        {
            if (statusLocal.Items.ContainsKey("statusLocalLabel"))
                statusLocal.Items["statusLocalLabel"].Text = txt;
        }

        //*************************************************************************************************************
        public void SetRemoteStatusText(string txt)
        {
            if (statusRemote.Items.ContainsKey("statusRemoteLabel"))
                statusRemote.Items["statusRemoteLabel"].Text = txt;
        }

        //*************************************************************************************************************
        public void Log(string text)
        {
            Log(text, Color.Black);
        }

        //*************************************************************************************************************
        public void Log(string text, Color textColor)
        {
            if (Closing)
                return;

            if (text.StartsWith("OK   :"))
                textColor = Color.Green;

            if (text.StartsWith("ERROR:"))
                textColor = Color.Red;

            Invoke((Action)(() =>
            {
                lock (StatusBox)
                {
                    try
                    {
                        string str = text.Replace("\r\n", "\n") + "\n";
                        StatusBox.AppendText(str);
                        StatusBox.Select(StatusBox.Text.Length - str.Length, str.Length);
                        StatusBox.SelectionColor = textColor;

                        if (StatusBox.Text.Length > 2048)
                            StatusBox.Text = StatusBox.Text.Remove(0, StatusBox.Text.Length - 2048);
                        
                        StatusBox.Select(StatusBox.Text.Length, 0);
                        StatusBox.ScrollToCaret();
                    }
                    catch { }
                }
            }));
        }

        //*************************************************************************************************************
        private void LocalTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = e.Node.Tag.ToString();
            LocalList.RealLoadList(path, false);
            if (Main.config.GetValue("DEFAULT_FTP_LOCAL_FOLDER") == "#LASTUSED#")
                Main.config.SetValue("FTP_LAST_USED_FOLDER", path);
        }

        //*************************************************************************************************************
        private void LocalPin_Click(object sender, EventArgs e)
        {
            if (LocalPath.FindStringExact(LocalList.CurrentDirectory) == -1)
                LocalList.PinFolder();
            else
                LocalList.UnpinFolder();
        }

        //*************************************************************************************************************
        private void RemotePin_Click(object sender, EventArgs e)
        {
            if (RemotePath.FindStringExact(RemoteList.CurrentDirectory) == -1)
                RemoteList.PinFolder();
            else
                RemoteList.UnpinFolder();
        }

        //*************************************************************************************************************
        private void LocalPath_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        //*************************************************************************************************************
        private void LocalPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = (LocalPath.SelectedItem == null) ? LocalPath.Text : LocalPath.SelectedItem.ToString();
            if (!Directory.Exists(path))
            {
                MessageBox.Show($"Path not found: {path}", "Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LocalList.CheckLink = true;
            LocalList.LoadList(path);
        }

        //*************************************************************************************************************
        private void LocalPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                LocalList.LoadList(LocalPath.Text);
        }

        //*************************************************************************************************************
        private async void RemotePath_DropDownClosed(object sender, EventArgs e)
        {
            RemoteList.CheckLick = true;
            string path = (RemotePath.SelectedItem == null) ? RemotePath.Text : RemotePath.SelectedItem.ToString();
            await RemoteList.LoadList(path);
        }

        //*************************************************************************************************************
        private async void RemotePath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                await RemoteList.LoadList(RemotePath.Text);
        }

        //*************************************************************************************************************
        private void LinkPath_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(LinkPath.Tag) == false)
                LinkFolders();
            else
                UnlinkFolders();
        }

        //*************************************************************************************************************
        private void LinkFolders()
        {
            int LocalPinID = LocalList.PinFolder();
            int RemotePinID = RemoteList.PinFolder();

            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE LinkTo={LocalPinID}");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE LinkTo={RemotePinID}");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo={LocalPinID} WHERE ID={RemotePinID}");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo={RemotePinID} WHERE ID={LocalPinID}");
                LinkPath.Image = Resources.link;
                linkTip.SetToolTip(LinkPath, "Unlink folders");
                LinkPath.Tag = true;
            }
        }

        //*************************************************************************************************************
        private void UnlinkFolders()
        {
            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE ServerID={server.ID} AND Local=1 AND Path='{LocalPath.Text}'");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE ServerID={server.ID} AND Local=0 AND Path='{RemotePath.Text}'");
                LinkPath.Image = Resources.link_break;
                linkTip.SetToolTip(LinkPath, "Link local and remote folders");
                LinkPath.Tag = false;
            }
        }

        //*************************************************************************************************************
        public async Task CheckLink(string path, bool local)
        {
            if (SkipCheckLink)
            {
                SkipCheckLink = false;
                return;
            }

            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("Local", local);
                sql.ExecuteTX($@"SELECT l.[path] FROM pins p 
                    INNER JOIN pins l ON l.ID=p.LinkTo
                    WHERE p.Local=@Local AND p.path='{path}' AND p.ServerID={server.ID}");

                if (sql.Read())
                {
                    LinkPath.Tag = true;
                    LinkPath.Image = Resources.link;
                    string link = sql.Value(0).ToString();
                    linkTip.SetToolTip(LinkPath, "Unlink folders");

                    if (local)
                        await RemoteList.LoadList(link);
                    else
                        LocalList.LoadList(link);
                }
                else
                {
                    LinkPath.Tag = false;
                    LinkPath.Image = Resources.link_break;
                    linkTip.SetToolTip(LinkPath, "Link local and remote folders");
                }
            }
        }

        //*************************************************************************************************************
        private void FTPForm_Enter(object sender, EventArgs e)
        {
            
        }

      
    }
}
