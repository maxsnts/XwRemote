using KRBTabControlNS.CustomTab;
using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    enum PerformanceFlags
    {
        TS_PERF_DISABLE_CURSOR_SHADOW = 0x00000020,
        TS_PERF_DISABLE_CURSORSETTINGS = 0x00000040,
        TS_PERF_DISABLE_FULLWINDOWDRAG = 0x00000002,
        TS_PERF_DISABLE_MENUANIMATIONS = 0x00000004,
        TS_PERF_DISABLE_NOTHING = 0x00000000,
        TS_PERF_DISABLE_THEMING = 0x00000008,
        TS_PERF_DISABLE_WALLPAPER = 0x00000001,
        TS_PERF_ENABLE_FONT_SMOOTHING = 0x00000080,
        TS_PERF_ENABLE_DESKTOP_COMPOSITION = 0x00000100
    }

    public partial class RDPForm : Form
    {
        private Server server = null;

        //*************************************************************************************************************
        public RDPForm(Server server)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            this.server = server;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            rdpControl.Visible = true;
            rdpControl.ConnectingText = "Connecting...";
            rdpControl.DisconnectedText = "Disconnected!";
            buttonConnect.Visible = false;
            buttonConnect.BringToFront();
        }

        //*************************************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            if (server.Port == 0)
                server.Port = 3389;

            Connect();
        }

        //*************************************************************************************************************
        public void ResizeEnded()
        {
            if (server.Width == -1)
                rdpControl.Reconnect((uint)Width, (uint)Height);
        }

        //*************************************************************************************************************
        public void Connect()
        {
            buttonConnect.Visible = false;
            loadingCircle1.BringToFront();
            loadingCircle1.Active = true;
            loadingCircle1.InnerCircleRadius = 15;
            loadingCircle1.OuterCircleRadius = 30;
            loadingCircle1.SpokeThickness = 5;
            loadingCircle1.Top = (this.Height / 2) + 10;
            loadingCircle1.Left = (this.Width / 2) - 40;
            loadingCircle1.Visible = true;


            //CONNECT
            rdpControl.Server = server.Host;
            rdpControl.AdvancedSettings7.RDPPort = server.Port;
            rdpControl.AdvancedSettings7.EnableAutoReconnect = true;
            rdpControl.AdvancedSettings7.MaxReconnectAttempts = 10;
            rdpControl.AdvancedSettings7.EncryptionEnabled = server.Encryption.ToIntOrDefault(0);
            rdpControl.AdvancedSettings7.keepAliveInterval = 30000;
            //rdpControl.AdvancedSettings7.ConnectToAdministerServer = true;
            //rdpControl.AdvancedSettings7.singleConnectionTimeout;

            //AUTH
            rdpControl.AdvancedSettings7.DisableCtrlAltDel = 1;
            rdpControl.AdvancedSettings7.EnableCredSspSupport = server.Certificates;
            rdpControl.UserName = server.Username;
            if (!string.IsNullOrEmpty(server.Password))
                rdpControl.AdvancedSettings7.ClearTextPassword = server.Password;
            rdpControl.AdvancedSettings7.AuthenticationLevel = 0;
           
            //INPUT
            rdpControl.SecuredSettings2.KeyboardHookMode = 1;
            rdpControl.AdvancedSettings7.allowBackgroundInput = 0;
            rdpControl.AdvancedSettings7.GrabFocusOnConnect = true;
            rdpControl.AdvancedSettings7.EnableWindowsKey = 1;
            rdpControl.AdvancedSettings7.DoubleClickDetect = 1; //??

            //DEVICES
            rdpControl.AdvancedSettings7.RedirectClipboard = true;
            rdpControl.AdvancedSettings7.AudioRedirectionMode = (uint)((server.UseSound) ? 0 : 1);
            rdpControl.AdvancedSettings7.RedirectDrives = server.ConnectDrives;
            rdpControl.AdvancedSettings7.RedirectDevices = false;
            rdpControl.AdvancedSettings7.RedirectPorts = false;
            rdpControl.AdvancedSettings7.RedirectPrinters = false;
            rdpControl.AdvancedSettings7.RedirectSmartCards = false;

            //PERFORMANCE
            rdpControl.ColorDepth = server.Color;
            rdpControl.AdvancedSettings7.BitmapPeristence = 1;
            rdpControl.AdvancedSettings7.Compress = 1;
            rdpControl.AdvancedSettings7.PerformanceFlags =
                  ((int)PerformanceFlags.TS_PERF_DISABLE_CURSOR_SHADOW)
                | ((int)PerformanceFlags.TS_PERF_DISABLE_CURSORSETTINGS)
                | ((int)PerformanceFlags.TS_PERF_DISABLE_FULLWINDOWDRAG)
                | ((int)PerformanceFlags.TS_PERF_DISABLE_MENUANIMATIONS)
                //| ((int)PerformanceFlags.TS_PERF_ENABLE_FONT_SMOOTHING)
                | ((int)PerformanceFlags.TS_PERF_ENABLE_DESKTOP_COMPOSITION)
                | ((server.Themes) ? 0 : ((int)PerformanceFlags.TS_PERF_DISABLE_THEMING))
                ;

            //SIZE
            rdpControl.AutoSize = true;
            rdpControl.FullScreen = false;
            rdpControl.AdvancedSettings7.SmartSizing = true;
            rdpControl.DesktopWidth = (server.Width > 0) ? server.Width : Width;
            rdpControl.DesktopHeight = (server.Height > 0) ? server.Height: Height;
            
            rdpControl.OnEnterFullScreenMode += RdpControl_OnEnterFullScreenMode;
            rdpControl.OnLeaveFullScreenMode += RdpControl_OnLeaveFullScreenMode;
            rdpControl.FullScreenTitle = $"{server.Name} ({server.Host})";

            if (server.Width == -2)
            {
                Rectangle screen = Screen.FromControl(this).Bounds;
                rdpControl.DesktopWidth = screen.Width;
                rdpControl.DesktopHeight = screen.Height;                
                FullScreen();
            }

            rdpControl.Connect();
        }

        //*************************************************************************************************************
        private void RdpControl_OnEnterFullScreenMode(object sender, EventArgs e)
        {
            labelMessage.Visible = true;
            Rectangle screen = Screen.FromControl(this).Bounds;
            rdpControl.Reconnect((uint)screen.Width, (uint)screen.Height);
        }

        //*************************************************************************************************************
        private void RdpControl_OnLeaveFullScreenMode(object sender, EventArgs e)
        {
            labelMessage.Visible = false;
            rdpControl.FullScreen = false;
            int X = (server.Width > 0) ? server.Width : Width;
            int Y = (server.Height > 0) ? server.Height : Height;
            rdpControl.Reconnect((uint)X, (uint)Y);
        }

        //*************************************************************************************************************
        private void rdpControl_OnConnected(object sender, EventArgs e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        //*************************************************************************************************************
        private void rdpControl_OnDisconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
            buttonConnect.Visible = true;
            
            switch (e.discReason)
            {
                case 2308:
                    rdpControl.DisconnectedText = "Socket closed";
                    break;
                case 3:
                    //Remote disconnection by server. This is not an error code.
                    ((KRBTabControl)(Parent.Parent)).TabPages.Remove((TabPageEx)Parent);
                    rdpControl.Dispose();
                    break;
                case 3080:
                    rdpControl.DisconnectedText = "Decompression error.";
                    break;
                case 264:
                    rdpControl.DisconnectedText = "Connection timed out.";
                    break;
                case 3078:
                    rdpControl.DisconnectedText = "Decryption error.";
                    break;
                case 260:
                    rdpControl.DisconnectedText = "DNS name lookup failure.";
                    break;
                case 1288:
                    rdpControl.DisconnectedText = "DNS lookup failed.";
                    break;
                case 2822:
                    rdpControl.DisconnectedText = "Encryption error.";
                    break;
                case 1540:
                    rdpControl.DisconnectedText = "Windows Sockets gethostbyname call failed.";
                    break;
                case 520:
                    rdpControl.DisconnectedText = "Host not found error.";
                    break;
                case 1032:
                    rdpControl.DisconnectedText = "Internal error.";
                    break;
                case 2310:
                    rdpControl.DisconnectedText = "Internal security error.";
                    break;
                case 2566:
                    rdpControl.DisconnectedText = "Internal security error.";
                    break;
                case 1286:
                    rdpControl.DisconnectedText = "The encryption method specified is not valid.";
                    break;
                case 2052:
                    rdpControl.DisconnectedText = "Bad IP address specified.";
                    break;
                case 1542:
                    rdpControl.DisconnectedText = "Server security data is not valid.";
                    break;
                case 1030:
                    rdpControl.DisconnectedText = "Security data is not valid.";
                    break;
                case 776:
                    rdpControl.DisconnectedText = "The IP address specified is not valid.";
                    break;
                case 2056:
                    rdpControl.DisconnectedText = "License negotiation failed.";
                    break;
                case 2312:
                    rdpControl.DisconnectedText = "Licensing time-out.";
                    break;
                case 1:
                    //Local disconnection. This is not an error code.
                    ((KRBTabControl)(Parent.Parent)).TabPages.Remove((TabPageEx)Parent);
                    rdpControl.Dispose();
                    break;
                case 0:
                    rdpControl.DisconnectedText = "No information is available.";
                    break;
                case 262:
                    rdpControl.DisconnectedText = "Out of memory.";
                    break;
                case 518:
                    rdpControl.DisconnectedText = "Out of memory.";
                    break;
                case 774:
                    rdpControl.DisconnectedText = "Out of memory.";
                    break;
                case 2:
                    //Remote disconnection by user. This is not an error code.
                    ((KRBTabControl)(Parent.Parent)).TabPages.Remove((TabPageEx)Parent);
                    rdpControl.Dispose();
                    break;
                case 1798:
                    rdpControl.DisconnectedText = "Failed to unpack server certificate.";
                    break;
                case 516:
                    rdpControl.DisconnectedText = "Windows Sockets connect failed.";
                    break;
                case 1028:
                    rdpControl.DisconnectedText = "Windows Sockets recv call failed.";
                    break;
                case 1796:
                    rdpControl.DisconnectedText = "Time-out occurred.";
                    break;
                case 1544:
                    rdpControl.DisconnectedText = "Internal timer error.";
                    break;
                case 772:
                    rdpControl.DisconnectedText = "Windows Sockets send call failed.";
                    break;
                case 2823:
                    rdpControl.DisconnectedText = "The account is disabled.";
                    break;
                case 3591:
                    rdpControl.DisconnectedText = "The account is expired.";
                    break;
                case 3335:
                    rdpControl.DisconnectedText = "The account is locked out.";
                    break;
                case 3079:
                    rdpControl.DisconnectedText = "The account is restricted.";
                    break;
                case 6919:
                    rdpControl.DisconnectedText = "The received certificate is expired.";
                    break;
                case 5639:
                    rdpControl.DisconnectedText = "The policy does not support delegation of credentials to the target server.";
                    break;
                case 8455:
                    rdpControl.DisconnectedText = "The server authentication policy does not allow connection requests using saved credentials. The user must enter new credentials.";
                    break;
                case 2055:
                    rdpControl.DisconnectedText = "Login failed.";
                    break;
                case 6151:
                    rdpControl.DisconnectedText = "No authority could be contacted for authentication. The domain name of the authenticating party could be wrong, the domain could be unreachable, or there might have been a trust relationship failure.";
                    break;
                case 2567:
                    rdpControl.DisconnectedText = "The specified user has no account.";
                    break;
                case 3847:
                    rdpControl.DisconnectedText = "The password is expired.";
                    break;
                case 4615:
                    rdpControl.DisconnectedText = "The user password must be changed before logging on for the first time.";
                    break;
                case 5895:
                    rdpControl.DisconnectedText = "Delegation of credentials to the target server is not allowed unless mutual authentication has been achieved.";
                    break;
                case 8711:
                    rdpControl.DisconnectedText = "The smart card is blocked.";
                    break;
                case 7175:
                    rdpControl.DisconnectedText = "An incorrect PIN was presented to the smart card.";
                    break;
                case 2825:
                    rdpControl.DisconnectedText = "The remote computer requires Network Level Authentication, which your computer does not support. For assistance, contact your system administrator or technical support.";
                    break;
            }
        }

        //*************************************************************************************************************
        private void rdpControl_OnFatalError(object sender, AxMSTSCLib.IMsTscAxEvents_OnFatalErrorEvent e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        //*************************************************************************************************************
        private void RDPForm_SizeChanged(object sender, EventArgs e)
        {
            loadingCircle1.Top = (this.Height / 2) + 10;
            loadingCircle1.Left = (this.Width / 2) - 40;
            buttonConnect.Top = (this.Height / 2) + 20;
            buttonConnect.Left = (this.Width / 2) - 60;
        }

        //*************************************************************************************************************
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        //*************************************************************************************************************
        private void rdpControl_OnWarning(object sender, AxMSTSCLib.IMsTscAxEvents_OnWarningEvent e)
        {
            //MessageBox.Show(e.warningCode.ToString());
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            try
            {
                rdpControl.Disconnect();
                rdpControl.Dispose();
            }
            catch { }
            return true;
        }

        //*************************************************************************************************************
        public void OnTabFocus()
        {
            rdpControl.Focus();
        }

        //*************************************************************************************************************
        private void OnEnter(object sender, EventArgs e)
        {
            OnTabFocus();
        }

        //*************************************************************************************************************
        private void rdpControl_Enter(object sender, EventArgs e)
        {
            rdpControl.Focus();
        }

        //*************************************************************************************************************
        public void FullScreen()
        {
            rdpControl.FullScreen = true;
            labelMessage.Text = "Is the RDP windows minimized?";
            labelMessage.Visible = true;
        }
    }
}
