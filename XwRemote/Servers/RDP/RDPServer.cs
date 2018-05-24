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
    public class RDPServer : Server
    {
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            RDPForm form = new RDPForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            RDPSettings form = new RDPSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            RDPSettings form = new RDPSettings(this);
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
            return "rdp";
        }

        //**********************************************************************************************
        public override Image GetImage()
        {
            return Resources.rdp;
        }
    }
}
