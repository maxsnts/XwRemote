using Microsoft.Win32;
using System;
using System.Windows.Forms;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class IEForm : Form
    {
        private Server server = null;
        bool tryAutoLogin = true;

        //*************************************************************************************************************
        public IEForm(Server srv)
        {
            using (RegistryKey key = 
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", 
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, 11000, RegistryValueKind.DWord);
            }

            using (RegistryKey key = 
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", 
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, 11000, RegistryValueKind.DWord);
            }

            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            if (server.Port == 0)
                server.Port = 80;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);

        }

        //*************************************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            Connect();
        }

        //*************************************************************************************************************
        private void Connect()
        {
            if (server.UseHtmlLogin)
            {
                webBrowser.Navigate(server.Host);
            }
            else
            {
                string host = server.Host;
                if (server.Username.Trim() != string.Empty)
                {
                    host = server.Host.Replace("http://", "");
                    host = string.Format("http://{0}:{1}@{2}", server.Username, server.Password, host);
                }
                webBrowser.Navigate(host);
            }
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            return true;
        }

        //*************************************************************************************************************
        public void OnTabFocus()
        {
            
        }

        //*************************************************************************************************************
        private void OnEnter(object sender, EventArgs e)
        {
            OnTabFocus();
        }

        //*************************************************************************************************************
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (server.UseHtmlLogin)
            {
                if (!tryAutoLogin)
                    return;

                System.Windows.Forms.HtmlDocument document = webBrowser.Document;

                try
                {
                    HtmlElement user = document.GetElementById(server.HtmlUserBox);
                    if (user != null)
                    {
                        user.InnerText = server.Username;
                    }

                    HtmlElement pass = document.All[server.HtmlPassBox];
                    if (pass != null)
                    {
                        pass.InnerText = server.Password;

                        HtmlElement login = document.All[server.HtmlLoginBtn];
                        if (login != null)
                        {
                            login.InvokeMember("click");
                            tryAutoLogin = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
