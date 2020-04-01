using System;
using System.Threading;
using System.Windows.Forms;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class VNCForm : Form
    {
        private Server server = null;

        //*************************************************************************************************************
        public VNCForm(Server srv)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            if (server.Port == 0)
                server.Port = 5900;
        }

        //*************************************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            Connect();
        }

        //*************************************************************************************************************
        public void Connect()
        {
            loadingCircle1.BringToFront();
            loadingCircle1.Active = true;
            loadingCircle1.InnerCircleRadius = 15;
            loadingCircle1.OuterCircleRadius = 30;
            loadingCircle1.SpokeThickness = 5;
            loadingCircle1.Top = (this.Height / 2) + 10;
            loadingCircle1.Left = (this.Width / 2) - 40;
            loadingCircle1.Visible = true;

            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectTrd), this);
        }

        //*************************************************************************************************************
        static void ConnectTrd(object state)
        {
            VNCForm form = (VNCForm)state;

            form.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    form.vnc.VncPort = form.server.Port;
                    form.vnc.GetPassword = form.VNCPassword;
                    //form.vnc.Connect(form.server.Host);
                    form.vnc.Connect(form.server.Host, false, form.server.AutoScale);
                }
                catch (Exception ex)
                {
                    form.vnc_ConnectionLost(null, null);
                    MessageBox.Show(ex.Message);
                }
            });
        }

        //*************************************************************************************************************
        string VNCPassword()
        {
            return server.Password;
        }

        //*************************************************************************************************************
        private void vnc_ConnectComplete(object sender, VncSharp.ConnectEventArgs e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;

            if (server.SendKeys == true)
                vnc.SendSpecialKeys(
                    VncSharp.SpecialKeys.Alt |
                    VncSharp.SpecialKeys.AltF4 |
                    VncSharp.SpecialKeys.Ctrl |
                    VncSharp.SpecialKeys.CtrlAltDel |
                    VncSharp.SpecialKeys.CtrlEsc);
        }

        //*************************************************************************************************************
        private void vnc_ConnectionLost(object sender, EventArgs e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
            vnc.Dispose();
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            if (vnc.IsConnected)
                vnc.Disconnect();
            vnc.Dispose();
            return true;
        }

        //*************************************************************************************************************
        public void OnTabFocus()
        {
            vnc.Focus();
        }

        //*************************************************************************************************************
        private void OnEnter(object sender, EventArgs e)
        {
            OnTabFocus();
        }
    }
}
