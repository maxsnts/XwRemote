using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwMaxLib.Extentions;

namespace XwRemote.Settings
{
    public partial class SSHSettings : Form
    {
        private Server server = null;

        //*************************************************************************************************************
        public SSHSettings(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            server.Type = ServerType.SSH;
            NameBox.Text = server.Name;
            HostBox.Text = server.Host;
            UserBox.Text = server.Username;
            PassBox.Text = server.Password;
            IsFavorite.Checked = server.IsFavorite;
            checkSSH1.Checked = server.SSH1;
            tabColorBox.SelectedColor = Color.FromArgb(server.TabColor);
            NotesBox.Text = server.Notes;
            SshKeyBox.Text = server.SshKey;

            if (server.Port == 22 || server.Port == 0)
            {
                DefaultPort.Checked = true;
                PortBox.Enabled = false;
                PortBox.Value = 22;
            }
            else
            {
                DefaultPort.Checked = false;
                PortBox.Value = server.Port;
                PortBox.Enabled = true;
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

            server.Name = NameBox.Text;
            server.Host = HostBox.Text;
            server.Username = UserBox.Text;
            server.Password = PassBox.Text;
            server.IsFavorite = IsFavorite.Checked;
            server.SSH1 = checkSSH1.Checked;
            server.Port = PortBox.Value.ToIntOrDefault(22);
            server.TabColor = tabColorBox.SelectedColor.ToArgb();
            server.Notes = NotesBox.Text;
            server.SshKey = SshKeyBox.Text;
            server.SshTerminal = 1;

            Main.config.SaveServer(server);
            Close();
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
                PortBox.Value = 22;
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
