using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Objects;
using XwMaxLib.Extensions;
using XwMaxLib.Extentions;

namespace XwRemote.Settings
{
    public partial class SQLSettings : Form
    {
        private Server server = null;
        //**************************************************************************************
        public SQLSettings(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        //**************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            EngineBox.Items.Add(new ListItem(6, "MySQL"));
            
            NameBox.Text = server.Name;
            HostBox.Text = server.Host;
            UserBox.Text = server.Username;
            PassBox.Text = server.Password;
            IsFavorite.Checked = server.IsFavorite;
            PortBox.Text = server.Port.ToString();
            tabColorBox.SelectedColor = Color.FromArgb(server.TabColor);
            NotesBox.Text = server.Notes;
            EngineBox.SelectID((int)server.Type);
            
            if (server.Port == 3306 || server.Port == 0)
            {
                DefaultPort.Checked = true;
                PortBox.Enabled = false;
                PortBox.Text = "3306";
            }
            else
            {
                DefaultPort.Checked = false;
                PortBox.Enabled = true;
            }

            switch (server.Type)
            {
                case ServerType.MYSQL:
                    Text = "MySQL Settings";
                    break;
            }
        }

        //**************************************************************************************
        private void butOK_Click(object sender, EventArgs e)
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
            server.Port = PortBox.Text.ToIntOrDefault(3306);
            server.TabColor = tabColorBox.SelectedColor.ToArgb();
            server.Notes = NotesBox.Text;
            Main.config.SaveServer(server);
            Close();
        }

        //**************************************************************************************
        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //**************************************************************************************
        private void DefaultPort_CheckedChanged(object sender, EventArgs e)
        {
            if (DefaultPort.Checked)
            {
                PortBox.Enabled = false;
                PortBox.Text = "3306";
            }
            else
            {
                PortBox.Enabled = true;
            }
        }

        //**************************************************************************************
        private void PassBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }
    }
}
