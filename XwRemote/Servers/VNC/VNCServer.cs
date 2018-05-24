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
    public class VNCServer : Server
    {
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            VNCForm form = new VNCForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            VNCSettings form = new VNCSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            VNCSettings form = new VNCSettings(this);
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
            return "vnc";
        }

        //**********************************************************************************************
        public override Image GetImage()
        {
            return Resources.vnc;
        }
    }
}
