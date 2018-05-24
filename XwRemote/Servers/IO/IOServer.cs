using System;
using System.Drawing;
using KRBTabControlNS.CustomTab;
using XwRemote.Misc;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class IOServer: Server
    {
        //**********************************************************************************************
        public IOServer(ServerType type)
        {
            Type = type;
        }

        //**********************************************************************************************
        public override void Open(TabPageEx tab)
        {
            IOForm form = new IOForm(this);
            form.Parent = tab;
            form.Show();
        }

        //**********************************************************************************************
        public override void New()
        {
            IOSettings form = new IOSettings(this);
            form.ShowDialog();
        }

        //**********************************************************************************************
        public override void Edit()
        {
            IOSettings form = new IOSettings(this);
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

        //**********************************************************************************************
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
