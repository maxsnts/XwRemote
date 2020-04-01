using System;
using System.Windows.Forms;
using static XwRemote.Servers.IO.XwRemoteIO;

namespace XwRemote.Servers
{
    public partial class Exists : Form
    {
        public XwFileAction DoToFile = XwFileAction.Ask;
        public XwFileAction DoToAllFiles = XwFileAction.Ask;

        //*************************************************************************************************************
        public Exists()
        {
            InitializeComponent();
        }

        //*************************************************************************************************************
        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //*************************************************************************************************************
        private void overwriteFile_Click(object sender, EventArgs e)
        {
            DoToFile = XwFileAction.Overwrite;
            DialogResult = DialogResult.OK;
            Close();
        }

        //*************************************************************************************************************
        private void resumeFile_Click(object sender, EventArgs e)
        {
            DoToFile = XwFileAction.Resume;
            DialogResult = DialogResult.OK;
            Close();
        }

        //*************************************************************************************************************
        private void skipFile_Click(object sender, EventArgs e)
        {
            DoToFile = XwFileAction.Skip;
            DialogResult = DialogResult.OK;
            Close();
        }

        //*************************************************************************************************************
        private void overwriteAll_Click(object sender, EventArgs e)
        {
            DoToAllFiles = XwFileAction.Overwrite;
            DialogResult = DialogResult.OK;
            Close();
        }

        //*************************************************************************************************************
        private void resumeAll_Click(object sender, EventArgs e)
        {
            DoToAllFiles = XwFileAction.Resume;
            DialogResult = DialogResult.OK;
            Close();
        }

        //*************************************************************************************************************
        private void skipAll_Click(object sender, EventArgs e)
        {
            DoToAllFiles = XwFileAction.Skip;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
