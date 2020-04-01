using KRBTabControlNS.CustomTab;
using System.Drawing;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class IOServer: Server
    {
        IOForm form = null;

        //*************************************************************************************************************
        public IOServer(ServerType type)
        {
            Type = type;
            Port = GetDefaultServerPort(type);
        }

        //*************************************************************************************************************
        public override void Open(TabPageEx tab)
        {
            form = new IOForm(this);
            form.Parent = tab;
            form.Show();
        }

        //*************************************************************************************************************
        public override void New()
        {
            IOSettings form = new IOSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override void Edit()
        {
            IOSettings form = new IOSettings(this);
            form.ShowDialog();
        }

        //*************************************************************************************************************
        public override bool OnTabClose()
        {
            bool ret  = true;
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
            switch (Type)
            {
                case ServerType.FTP:
                    return "ftp";
                case ServerType.SFTP:
                    return "sftp";
                case ServerType.AWSS3:
                    return "s3";
                case ServerType.AZUREFILE:
                    return "azure";
                default:
                    return "";
            }
        }

        //*************************************************************************************************************
        public override Image GetImage()
        {
            switch (Type)
            {
                case ServerType.FTP:
                    return Resources.ftp;
                case ServerType.SFTP:
                    return Resources.sftp;
                case ServerType.AWSS3:
                    return Resources.s3;
                case ServerType.AZUREFILE:
                    return Resources.azure;
                default:
                    return null;
            }
        }
    }
}
