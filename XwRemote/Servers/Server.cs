using KRBTabControlNS.CustomTab;
using System;
using System.Drawing;
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
        public string Name = "";
        public string Host = "";
        public int Port;
        public string Username = "";
        public string Password = "";
        public bool SendKeys = true;
        public bool UseSound = false;
        public bool IsFavorite = false;
        public bool ConnectDrives = false;
        public int GroupID = 0;
        public int Color = 15;
        public int Width = 0;
        public int Height = 0;
        public bool AutoScale = true;
        public bool SSH1 = false;
        public bool Passive = true;
        public bool Themes = false;
        public bool Certificates = false;
        public bool Encryption = false;
        public bool UseHtmlLogin = false;
        public string HtmlUserBox = "";
        public string HtmlPassBox = "";
        public string HtmlLoginBtn = "";
        public int TabColor = -4144960;
        public string Notes;
        public int SshTerminal = 1;
        public string SshKey = "";
        public int FtpDataType = 0;

        //*************************************************************************************************************
        public Server Copy(ServerType newType)
        {
            //How the hell can i use MemberwiseClone while changing type?!?
            //Server copy = (Server)MemberwiseClone();
            //Oh well, i will just copy the members
            //it may be betters since i need to make changes
            //to the values when converting between types
            Server copy = Server.GetServerInstance(newType);
            copy.IsFavorite = false;
            copy.Name = Name;
            copy.Host = Host;
            copy.Username = Username;
            copy.Password = Password;
            copy.SendKeys = SendKeys;
            copy.UseSound = UseSound;
            copy.ConnectDrives = ConnectDrives;
            copy.GroupID = GroupID;
            copy.Color = Color;
            copy.Width = Width;
            copy.Height = Height;
            copy.AutoScale = AutoScale;
            copy.SSH1 = SSH1;
            copy.Passive = Passive;
            copy.Themes = Themes;
            copy.Certificates = Certificates;
            copy.Encryption = Encryption;
            copy.UseHtmlLogin = UseHtmlLogin;
            copy.HtmlUserBox = HtmlUserBox;
            copy.HtmlPassBox = HtmlPassBox;
            copy.HtmlLoginBtn = HtmlLoginBtn;
            copy.TabColor = TabColor;
            copy.Notes = Notes;
            copy.SshTerminal = SshTerminal;
            copy.SshKey = SshKey;
            copy.FtpDataType = FtpDataType;
            //don't copy the port, use the protocols default
            copy.Port = GetDefaultServerPort(newType);
            return copy;
        }

        //*************************************************************************************************************
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
                case ServerType.SSH:
                    return new SSHServer();
                case ServerType.VNC:
                    return new VNCServer();
                default:
                    throw new Exception($"Type not valid {type}");
            }
        }

        //*************************************************************************************************************
        public int GetDefaultServerPort(ServerType type)
        {
            switch (type)
            {
                case ServerType.FTP:
                    return 21;
                case ServerType.RDP:
                    return 3389;
                case ServerType.VNC:
                    return 5900;
                case ServerType.SSH:
                case ServerType.SFTP:
                    return 22;
                default:
                    return 0;
            }
        }

        //*************************************************************************************************************
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

        public virtual void FullScreen()
        {

        }

        public virtual string GetIcon()
        {
            return "";   
        }

        public virtual Image GetImage()
        {
            return null;
        }
        public virtual void ResizeEnd()
        {
        }
    }
}
