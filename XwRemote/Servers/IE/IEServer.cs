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
    public class IEServer : Server
    {
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            IEForm form = new IEForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            IESettings form = new IESettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            IESettings form = new IESettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void OnTabFocus()
        {

        }

        //**********************************************************************************************
        public override string GetIcon()
        {
            return "ie";
        }

        //**********************************************************************************************
        public override Image GetImage()
        {
            return Resources.IE;
        }
    }
}
