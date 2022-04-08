using FluentFTP;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwMaxLib.Extentions;
using XwMaxLib.Objects;

namespace XwRemote.Settings
{
    public partial class IOSettings : Form
    {
        private Server server = null;

        //*************************************************************************************************************
        public IOSettings(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            NameBox.Text = server.Name;
            HostBox.Text = server.Host;
            UserBox.Text = server.Username;
            PassBox.Text = server.Password;
            IsFavorite.Checked = server.IsFavorite;
            NotesBox.Text = server.Notes;
            tabColorBox.SelectedColor = Color.FromArgb(server.TabColor);
            SshKeyBox.Text = server.SshKey;
            UseTLS.Checked = server.Encryption;

            switch (server.Type)
            {
                case ServerType.FTP:
                {
                    Text = "FTP Settings";
                    dialogHeader1.HeaderImage = XwRemote.Properties.Resources.ftp;
                    if (server.Port == 21 || server.Port == 0)
                    {
                        DefaultPort.Checked = true;
                        PortBox.Enabled = false;
                        PortBox.Value = 21;
                    }
                    else
                    {
                        DefaultPort.Checked = false;
                        PortBox.Enabled = true;
                        PortBox.Value = server.Port;
                    }
                    LoadFtpDataType(server.FtpDataType);
                }
                break;
                case ServerType.SFTP:
                {
                    Text = "SFTP Settings";
                    dialogHeader1.HeaderTitle = dialogHeader1.HeaderTitle.Replace("FTP", "SFTP");
                    dialogHeader1.HeaderDescription = dialogHeader1.HeaderDescription.Replace("FTP", "SFTP");
                    dialogHeader1.HeaderImage = XwRemote.Properties.Resources.sftp;
                    if (server.Port == 22 || server.Port == 0)
                    {
                        DefaultPort.Checked = true;
                        PortBox.Enabled = false;
                        PortBox.Value = 22;
                    }
                    else
                    {
                        DefaultPort.Checked = false;
                        PortBox.Enabled = true;
                        PortBox.Value = server.Port;
                    }
                    SshKeyBox.Visible = true;
                    SshKeyLabel.Visible = true;
                    buttonOpenSshKey.Visible = true;
                    FtpDataType.Visible = false;
                    FtpDataTypeLabel.Visible = false;
                    UseTLS.Visible = false;
                }
                break;
                case ServerType.AWSS3:
                {
                    Text = "AWS S3 Settings";
                    dialogHeader1.HeaderTitle = dialogHeader1.HeaderTitle.Replace("FTP", "AWS S3");
                    dialogHeader1.HeaderDescription = dialogHeader1.HeaderDescription.Replace("FTP", "AWS S3");
                    dialogHeader1.HeaderImage = XwRemote.Properties.Resources.s3;

                    DefaultPort.Visible = false;
                    PortBox.Visible = false;
                    PortLabel.Visible = false;
                    HostLabel.Text = "Bucket";
                    UsernameLabel.Text = "AccessKey";
                    PasswordLabel.Text = "SecretKey";
                    PassBox.UseSystemPasswordChar = false;
                    FtpDataType.Visible = false;
                    FtpDataTypeLabel.Visible = false;
                    UseTLS.Visible = false;
                }
                break;
                case ServerType.AZUREFILE:
                {
                    Text = "Azure File Settings";
                    dialogHeader1.HeaderTitle = dialogHeader1.HeaderTitle.Replace("FTP", "Azure File");
                    dialogHeader1.HeaderDescription = dialogHeader1.HeaderDescription.Replace("FTP", "Azure File");
                    dialogHeader1.HeaderImage = XwRemote.Properties.Resources.azure;

                    DefaultPort.Visible = false;
                    PortBox.Visible = false;
                    PortLabel.Visible = false;
                    HostLabel.Visible = false;
                    HostBox.Visible = false;
                    UsernameLabel.Text = "Storage";
                    PasswordLabel.Text = "Key";
                    PassBox.UseSystemPasswordChar = false;
                    FtpDataType.Visible = false;
                    FtpDataTypeLabel.Visible = false;
                    UseTLS.Visible = false;
                }
                break;
            }
        }

        //*************************************************************************************************************
        private void LoadFtpDataType(int type)
        {
            FtpDataType.Items.Clear();
            FtpDataType.Items.Add(new ListItem((int)FtpDataConnectionType.AutoPassive, "Passive - Auto - Recommended"));
            FtpDataType.Items.Add(new ListItem((int)FtpDataConnectionType.PASVEX, "Passive - Ignore routing info"));
            FtpDataType.Items.Add(new ListItem((int)FtpDataConnectionType.AutoActive, "Active"));

            if (type == 0)
                type = 1;

            switch ((FtpDataConnectionType)type)
            {
                case FtpDataConnectionType.AutoPassive:
                case FtpDataConnectionType.PASV:
                    FtpDataType.SelectedIndex = 0;
                    break;
                case FtpDataConnectionType.PASVEX:
                    FtpDataType.SelectedIndex = 1;
                    break;
                case FtpDataConnectionType.AutoActive:
                    FtpDataType.SelectedIndex = 2;
                    break;
            }
        }

        //*************************************************************************************************************
        private void butOK_Click(object sender, EventArgs e)
        {
            if (NameBox.Text.Trim() == string.Empty)
            {
                HostBox.ShowBalloon(ToolTipIcon.Warning, "Name", "must not be empty");
                return;
            }

            server.Name = NameBox.Text;
            server.Host = HostBox.Text;
            server.Username = UserBox.Text;
            server.Password = PassBox.Text;
            server.IsFavorite = IsFavorite.Checked;
            server.Port = PortBox.Value.ToIntOrDefault(21);
            if (server.Type == ServerType.FTP)
                server.FtpDataType = ((ListItem)FtpDataType.SelectedItem).ID;
            server.TabColor = tabColorBox.SelectedColor.ToArgb();
            server.Notes = NotesBox.Text;
            server.SshKey = SshKeyBox.Text;
            server.Encryption = UseTLS.Checked;
            Main.config.SaveServer(server);
            Close();
        }

        //*************************************************************************************************************
        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*************************************************************************************************************
        private void DefaultPort_CheckedChanged(object sender, EventArgs e)
        {
            if (DefaultPort.Checked)
            {
                PortBox.Enabled = false;
                PortBox.Value = 21;
            }
            else
            {
                PortBox.Enabled = true;
            }
        }

        //*************************************************************************************************************
        private void PassBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }

        //*************************************************************************************************************
        private void buttonOpenSshKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                SshKeyBox.Text = File.ReadAllText(open.FileName);
            }
        }

        //*************************************************************************************************************
        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }
    }
}
