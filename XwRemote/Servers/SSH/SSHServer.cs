using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRBTabControlNS.CustomTab;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class SSHServer : Server
    {
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            SSHForm form = new SSHForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            SSHSettings form = new SSHSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            SSHSettings form = new SSHSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override bool OnTabClose()
        {

            return true;
        }

        //**********************************************************************************************
        public override void OnTabFocus()
        {

        }

        //**********************************************************************************************
        public override string GetIcon()
        {
            return "ssh";
        }

        //**********************************************************************************************
        public override Image GetImage()
        {
            return Resources.ssh;
        }
    }
}
