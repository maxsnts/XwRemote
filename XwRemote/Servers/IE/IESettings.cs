using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Extentions;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class IESettings : Form
    {
        private Server server = null;
        
        //*************************************************************************************************************
        public IESettings(Server server)
        {
            InitializeComponent();
            this.server = server;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            server.Type = ServerType.IE;
            NameBox.Text = server.Name;
            HostBox.Text = server.Host;
            UserBox.Text = server.Username;
            PassBox.Text = server.Password;
            IsFavorite.Checked = server.IsFavorite;
            checkUseHtml.Checked = server.UseHtmlLogin;
            userIDbox.Text = server.HtmlUserBox;
            passIDbox.Text = server.HtmlPassBox;
            loginIDbox.Text = server.HtmlLoginBtn;
            userIDbox.Enabled = server.UseHtmlLogin;
            passIDbox.Enabled = server.UseHtmlLogin;
            loginIDbox.Enabled = server.UseHtmlLogin;
            NotesBox.Text = server.Notes;
            tabColorBox.SelectedColor = Color.FromArgb(server.TabColor);
        }

        //*************************************************************************************************************
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            server.UseHtmlLogin = checkUseHtml.Checked;
            server.HtmlUserBox = userIDbox.Text;
            server.HtmlPassBox = passIDbox.Text;
            server.HtmlLoginBtn = loginIDbox.Text;
            server.TabColor = tabColorBox.SelectedColor.ToArgb();
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
        private void checkUseHtml_CheckedChanged(object sender, EventArgs e)
        {
            userIDbox.Enabled = checkUseHtml.Checked;
            passIDbox.Enabled = checkUseHtml.Checked;
            loginIDbox.Enabled = checkUseHtml.Checked;
        }

        //*************************************************************************************************************
        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;
        }
    }
}
