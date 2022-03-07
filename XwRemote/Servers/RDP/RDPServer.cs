using KRBTabControlNS.CustomTab;
using System.Drawing;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class RDPServer : Server
    {
        RDPForm form = null;

        //*************************************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new RDPForm(this);
            form.Parent = tab;
            form.Show();
        }

        //*************************************************************************************************************
        public override void New()
        {
            RDPSettings form = new RDPSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override void Edit()
        {
            RDPSettings form = new RDPSettings(this);
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
            return "rdp";
        }

        //*************************************************************************************************************
        public override Image GetImage()
        {
            return Resources.rdp;
        }

        //*************************************************************************************************************
        public override void ResizeEnd()
        {
            form?.ResizeEnded();
        }

        //*************************************************************************************************************
        public override void FullScreen()
        {
            form?.FullScreen();
        }
    }
}
