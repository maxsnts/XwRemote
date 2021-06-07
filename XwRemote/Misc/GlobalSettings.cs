using System;
using System.IO;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwMaxLib.Extensions;

namespace XwRemote.Settings
{
    public partial class GlobalSettings : Form
    {
        //*************************************************************************************************************
        public GlobalSettings()
        {
            InitializeComponent();

            string folder = Main.config.GetValue("DEFAULT_FTP_LOCAL_FOLDER");

            if (folder == "#MYCOMPUTER#" || folder == "")
                radioMyComputer.Checked = true;
            else if (folder == "#DESKTOP#")
                radioDesktop.Checked = true;
            else if (folder == "#LASTUSED#")
                radioLastUsed.Checked = true;
            else
            {
                radioFixed.Checked = true;
                textFixedFolder.Text = folder;
            }

            sshFontSize.Value = Main.config.GetValue("DEFAULT_SSH_FONT_SIZE").ToIntOrDefault(10);
            checkBoxCorrectFocus.Checked = Main.config.GetValue("SSH_CORRECT_FOCUS").ToBoolOrDefault(true);

            checkMinimizeToTray.Checked = Main.config.GetValue("UI_MINIMIZE_TO_TRAY").ToBoolOrDefault(false);
            checkCloseToTray.Checked = Main.config.GetValue("UI_CLOSE_TO_TRAY").ToBoolOrDefault(false);

            radioFixed_CheckedChanged(null, null);
        }

        //*************************************************************************************************************
        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textFixedFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //*************************************************************************************************************
        private void radioFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFixed.Checked)
            {
                btnBrowseFolder.Enabled = true;
                textFixedFolder.Enabled = true;
            }
            else
            {
                btnBrowseFolder.Enabled = false;
                textFixedFolder.Enabled = false;
            }
        }

        //*************************************************************************************************************
        private void butOK_Click(object sender, EventArgs e)
        {
            if (radioMyComputer.Checked)
                Main.config.SetValue("DEFAULT_FTP_LOCAL_FOLDER", "#MYCOMPUTER#");
            else if (radioDesktop.Checked)
                Main.config.SetValue("DEFAULT_FTP_LOCAL_FOLDER", "#DESKTOP#");
            else if (radioLastUsed.Checked)
                Main.config.SetValue("DEFAULT_FTP_LOCAL_FOLDER", "#LASTUSED#");
            else if (radioFixed.Checked)
            {
                if (!Directory.Exists(textFixedFolder.Text))
                {
                    MessageBox.Show("Selected folder does not exists", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Main.config.SetValue("DEFAULT_FTP_LOCAL_FOLDER", textFixedFolder.Text);
            }

            Main.config.SetValue("DEFAULT_SSH_FONT_SIZE", sshFontSize.Value.ToString());
            Main.config.SetValue("SSH_CORRECT_FOCUS", checkBoxCorrectFocus.Checked.ToString());

            Main.config.SetValue("UI_MINIMIZE_TO_TRAY", checkMinimizeToTray.Checked.ToString());
            Main.config.SetValue("UI_CLOSE_TO_TRAY", checkCloseToTray.Checked.ToString());

            Close();
        }

        //*************************************************************************************************************
        private void butSetPassword_Click(object sender, EventArgs e)
        {
            if (textMasterPassword.Text == "")
            {
                MessageBox.Show("Passwords must not be empty", "XwRemote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textMasterPassword.Text != textPasswordRepeat.Text)
            {
                MessageBox.Show("Passwords do not match", "XwRemote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ChangePassword(textMasterPassword.Text);
                Config.MasterPassword = textMasterPassword.Text;
            }

            MessageBox.Show("Password added", "XwRemote", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //*************************************************************************************************************
        private void butRemovePass_Click(object sender, EventArgs e)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ChangePassword("");
                Config.MasterPassword = "";
            }

            MessageBox.Show("Password removed", "XwRemote", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //*************************************************************************************************************
        private void butOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Main.config.SetValue("EXPERIMENTAL-STUFF",
                    (!Main.config.GetValue("EXPERIMENTAL-STUFF",
                    false.ToString()).ToBoolOrDefault(false)).ToString());
        }
    }
}
