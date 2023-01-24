using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwMaxLib.Extentions;
using XwMaxLib.Objects;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class RDPSettings : Form
    {
        private Server server = null;
        
        //*************************************************************************************************************
        public RDPSettings(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            LoadSize(server.Width, server.Height);
            LoadColor(server.Color);

            server.Type = ServerType.RDP;
            NameBox.Text = server.Name;
            HostBox.Text = server.Host;
            UserBox.Text = server.Username;
            PassBox.Text = server.Password;
            UseSound.Checked = server.UseSound;
            SendKeys.Checked = server.SendKeys;
            ConnectDrives.Checked = server.ConnectDrives;
            IsFavorite.Checked = server.IsFavorite;
            checkThemes.Checked = server.Themes;
            Certificates.Checked = server.Certificates;
            NotesBox.Text = server.Notes;
            tabColorBox.SelectedColor = Color.FromArgb(server.TabColor);
            Encryption.Checked = server.Encryption;

            if (server.Port == 3389 || server.Port == 0)
            {
                DefaultPort.Checked = true;
                PortBox.Enabled = false;
                PortBox.Value = 3389;
            }
            else
            {
                DefaultPort.Checked = false;
                PortBox.Enabled = true;
                PortBox.Value = server.Port;
            }
        }

        //*************************************************************************************************************
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*************************************************************************************************************
        private void DefaultPort_CheckedChanged(object sender, EventArgs e)
        {
            if (DefaultPort.Checked)
            {
                PortBox.Enabled = false;
                PortBox.Value = 3389;
            }
            else
            {
                PortBox.Enabled = true;
            }
        }

        //*************************************************************************************************************
        private void LoadColor(int bit)
        {
            ColorCombo.Items.Clear();
            ColorCombo.Items.Add(new ListItem(8, "256 Colors (8 bit)"));
            ColorCombo.Items.Add(new ListItem(15, "Medium Color (15 bit)"));
            ColorCombo.Items.Add(new ListItem(16, "High Color (16 bit)"));
            ColorCombo.Items.Add(new ListItem(24, "True Color (24 bit)"));
            ColorCombo.Items.Add(new ListItem(32, "Highest Color (32 bit)"));

            switch (bit)
            {
                case 8:
                    ColorCombo.SelectedIndex = 0;
                    break;
                case 15:
                    ColorCombo.SelectedIndex = 1;
                    break;
                case 16:
                    ColorCombo.SelectedIndex = 2;
                    break;
                case 24:
                    ColorCombo.SelectedIndex = 3;
                    break;
                case 32:
                    ColorCombo.SelectedIndex = 4;
                    break;
                default:
                    ColorCombo.SelectedIndex = 1;
                    break;
            }
        }

        //*************************************************************************************************************
        private void LoadSize(int X, int Y)
        {
            SizeCombo.Items.Clear();
            SizeCombo.Items.Add(new ListItem(0, "Fit to window (On resize: Scale remote image)"));
            SizeCombo.Items.Add(new ListItem(4, "Fit to window (On resize: Reconnect with new resolution)"));
            SizeCombo.Items.Add(new ListItem(6, "Full Screen ((On resize: Reconnect) Not recommended, but... "));
            SizeCombo.Items.Add(new ListItem(1, "800x600"));
            SizeCombo.Items.Add(new ListItem(2, "1024x768"));
            SizeCombo.Items.Add(new ListItem(3, "1280x1024"));
            SizeCombo.Items.Add(new ListItem(5, "1600x1200"));

            switch (X)
            {
                case 0:
                    SizeCombo.SelectedIndex = 0;
                    break;
                case -1:
                    SizeCombo.SelectedIndex = 1;
                    break;
                case -2:
                    SizeCombo.SelectedIndex = 2;
                    break;
                case 800:
                    SizeCombo.SelectedIndex = 3;
                    break;
                case 1024:
                    SizeCombo.SelectedIndex = 4;
                    break;
                case 1280:
                    SizeCombo.SelectedIndex = 5;
                    break;
                case 1600:
                    SizeCombo.SelectedIndex = 6;
                    break;
            }
        }

        //*************************************************************************************************************
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (NameBox.Text.Trim() == string.Empty)
            {
                NameBox.ShowBalloon(ToolTipIcon.Warning, "Name", "must not be empty");
                return;
            }

            if (HostBox.Text.Trim() == string.Empty)
            {
                HostBox.ShowBalloon(ToolTipIcon.Warning, "Host", "must not be empty");
                return;
            }
            
            if (ColorCombo.SelectedItem != null)
                server.Color = ((ListItem)ColorCombo.SelectedItem).ID;

            server.Name = NameBox.Text;
            server.Host = HostBox.Text;
            server.Username = UserBox.Text;
            server.Password = PassBox.Text;
            server.IsFavorite = IsFavorite.Checked;
            server.UseSound = UseSound.Checked;
            server.ConnectDrives = ConnectDrives.Checked;
            server.SendKeys = SendKeys.Checked;
            server.Port = (int)PortBox.Value;
            server.Themes = checkThemes.Checked;
            server.Certificates = Certificates.Checked;
            server.TabColor = tabColorBox.SelectedColor.ToArgb();
            server.Encryption = Encryption.Checked;
            
            switch (SizeCombo.SelectedIndex)
            {
                case 0:
                    server.Width = 0;
                    server.Height = 0;
                    break;
                case 1:
                    server.Width = -1;
                    server.Height = -1;
                    break;
                case 2:
                    server.Width = -2;
                    server.Height = -2;
                    break;
                case 3:
                    server.Width = 800;
                    server.Height = 600;
                    break;
                case 4:
                    server.Width = 1024;
                    server.Height = 768;
                    break;
                case 5:
                    server.Width = 1280;
                    server.Height = 1024;
                    break;
                case 6:
                    server.Width = 1600;
                    server.Height = 1200;
                    break;
            }

            server.Notes = NotesBox.Text;
            Main.config.SaveServer(server);
            Close();
        }

        //*************************************************************************************************************
        private void PassBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }

        //*************************************************************************************************************
        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }
    }
}
