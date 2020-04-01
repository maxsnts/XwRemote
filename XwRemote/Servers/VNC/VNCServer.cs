using KRBTabControlNS.CustomTab;
using System.Drawing;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class VNCServer : Server
    {
        VNCForm form = null;

        //*************************************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new VNCForm(this);
            form.Parent = tab;
            form.Show();
        }

        //*************************************************************************************************************
        public override void New()
        {
            VNCSettings form = new VNCSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override void Edit()
        {
            VNCSettings form = new VNCSettings(this);
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
            return "vnc";
        }

        //*************************************************************************************************************
        public override Image GetImage()
        {
            return Resources.vnc;
        }
    }
}
