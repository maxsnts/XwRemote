using System;
using System.Diagnostics;
using System.Drawing;
using KRBTabControlNS.CustomTab;
using XwRemote.Servers;

namespace XwRemote.Settings
{
    public enum ServerType
    {
        RDP = 1,
        VNC = 2,
        FTP = 3,
        SSH = 4,
        IE = 5,
        MYSQL = 6,
        SFTP = 7,
        AWSS3 = 8,
        AZUREFILE = 9,
    }
    
    public abstract class Server
    {
        public int ID = 0;
        public ServerType Type;
        public string Name;
        public string Host;
        public int Port;
        public string Username;
        public string Password;
        public bool SendKeys = true;
        public bool UseSound = false;
        public bool IsFavorite = false;
        public bool ConnectDrives = false;
        public int GroupID = 0;
        public int Color = 15;
        public int Width = 0;
        public int Height = 0;
        public bool Console = false;
        public bool AutoScale = true;
        public bool SSH1 = false;
        public bool Passive = true;
        public bool Themes = false;
        public bool Certificates = false;
        public bool Encryption = false;
        public bool UseHtmlLogin = false;
        public string HtmlUserBox;
        public string HtmlPassBox;
        public string HtmlLoginBtn;
        public int TabColor = -4144960;
        public string Notes;
        public int SshTerminal = 1;

        //**********************************************************************************************
        public Server Copy()
        {
            Server copy = (Server)MemberwiseClone();
            copy.ID = 0;
            return copy;
        }

        //**********************************************************************************************
        public static Server GetServerInstance(ServerType type)
        {
            switch (type)
            {
                case ServerType.FTP:
                case ServerType.SFTP:
                case ServerType.AWSS3:
                case ServerType.AZUREFILE:
                    return new IOServer(type);
                case ServerType.IE:
                    return new IEServer();
                case ServerType.RDP:
                    return new RDPServer();
                case ServerType.MYSQL:
                    return new SQLServer(type);
                case ServerType.SSH:
                    return new SSHServer();
                case ServerType.VNC:
                    return new VNCServer();
                default:
                    throw new Exception($"Type not valid {type}");
            }
        }
        
        //**********************************************************************************************
        // Virtuals, usually empty
        public virtual void Open(TabPageEx tab)
        {
            throw new Exception("'Open' not implementd");
        }
        public virtual void New()
        {
            throw new Exception("'New' not implementd");
        }
        public virtual void Edit()
        {
            throw new Exception("'Edit' not implementd");
        }
        public virtual void OnTabFocus()
        {
        }
        public virtual bool OnTabClose()
        {
            return true;
        }
        public virtual string GetIcon()
        {
            return "";   
        }

        public virtual Image GetImage()
        {
            return null;
        }
    }
}
