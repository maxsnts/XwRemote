using KRBTabControlNS.CustomTab;
using System.Drawing;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class IEServer : Server
    {
        IEForm form = null;
        //*************************************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new IEForm(this);
            form.Parent = tab;
            form.Show();
        }

        //*************************************************************************************************************
        public override void New()
        {
            IESettings form = new IESettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override void Edit()
        {
            IESettings form = new IESettings(this);
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
            return "ie";
        }

        //*************************************************************************************************************
        public override Image GetImage()
        {
            return Resources.IE;
        }
    }
}
