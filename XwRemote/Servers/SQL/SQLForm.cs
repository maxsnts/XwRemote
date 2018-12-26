using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwRemote.Servers;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class SQLForm : Form
    {
        private Server server = null;
        private XwDbCommand connection = null;
        private string connectionString = string.Empty;
        private string connectionProvider = string.Empty;

        //********************************************************************************************
        public SQLForm(Server srv)
        {
            InitializeComponent();
            splitContainer.Dock = DockStyle.Fill;
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //********************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            if (server.Port == 0)
            {
                switch (server.Type)
                {
                    default:
                        throw new Exception("Dedault port not defined");
                }
            }

            connectionString = $"server={server.Host};port={server.Port};UserID={server.Username}; password={server.Password}; database=mysql;SslMode=None;Pooling=true;MinimumPoolSize=5;MaximumPoolSize=10;ConnectionLifetime=30;Charset=utf8;";
            connectionProvider = $"Data.MySqlClient";
        }

        //********************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            Connect();
        }

        //********************************************************************************************
        private void Connect()
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
            
            connection = new XwDbCommand(connectionString, connectionProvider);
            //TODO: Connect
            connection.ExecuteTX("SELECT 1");

            loadingCircle1.Visible = false;
            statusLabel.Visible = false;
            splitContainer.Visible = true;
        }


        //********************************************************************************************
        public void SetStatusText(string txt)
        {
            statusLabel.Text = txt;
            statusLabel.AutoSize = true;
            statusLabel.Top = (this.Height / 2) - (statusLabel.Height / 2) - 10;
            statusLabel.Left = (this.Width / 2) - (statusLabel.Width / 2);
        }
        
        //********************************************************************************************
        private void OnEnter(object sender, EventArgs e)
        {
            OnTabFocus();
        }

        //********************************************************************************************
        public bool OnTabClose()
        {
            connection.Close();
            return true;
        }

        //********************************************************************************************
        public void OnTabFocus()
        {
            
        }
    }
}
