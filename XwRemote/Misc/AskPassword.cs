using System;
using System.Windows.Forms;

namespace XwRemote.Misc
{
    public partial class AskPassword : Form
    {
        public AskPassword()
        {
            InitializeComponent();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
