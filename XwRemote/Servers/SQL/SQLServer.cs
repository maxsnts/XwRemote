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
    public class SQLServer : Server
    {
        //**********************************************************************************************
        public SQLServer(ServerType type)
        {
            Type = type;
        }
        
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            SQLForm form = new SQLForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            SQLSettings form = new SQLSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            SQLSettings form = new SQLSettings(this);
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
            return "sql";
        }

        //**********************************************************************************************
        public override Image GetImage()
        {
            return Resources.database;
        }
    }
}
