using KRBTabControlNS.CustomTab;
using System.Drawing;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class SSHServer : Server
    {
        SSHForm form = null;

        //*************************************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new SSHForm(this);
            form.Parent = tab;
            form.Show();
        }

        //*************************************************************************************************************
        public override void New()
        {
            SSHSettings form = new SSHSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override void Edit()
        {
            SSHSettings form = new SSHSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override bool OnTabClose()
        {
            bool ret = true;
            if (form != null)
                ret = form.OnTabClose();
            form = null;
            return ret;
        }

        //*************************************************************************************************************
        public override void OnTabFocus()
        {
            if (form != null)
                form.OnTabFocus();
        }

        //*************************************************************************************************************
        public override string GetIcon()
        {
            return "ssh";
        }

        //*************************************************************************************************************
        public override Image GetImage()
        {
            return Resources.ssh;
        }
    }
}
