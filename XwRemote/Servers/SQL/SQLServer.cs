using System.Drawing;
using KRBTabControlNS.CustomTab;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class SQLServer : Server
    {
        SQLForm form = null;
        //**********************************************************************************************
        public SQLServer(ServerType type)
        {
            Type = type;
        }
        
        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new SQLForm(this);
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
            bool ret = true;
            if (form != null)
                ret = form.OnTabClose();
            form = null;
            return ret;
        }

        //**********************************************************************************************
        public override void OnTabFocus()
        {
            if (form != null)
                form.OnTabFocus();
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
