using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwMaxLib.Extensions;
using XwRemote.Misc;

namespace XwRemote.Settings
{
    public class Config
    {
        public List<Group> groups = new List<Group>();
        private Dictionary<string, string> values = new Dictionary<string, string>();
        public static string MasterPassword = "";

        //*************************************************************************************************************
        public void Load()
        {
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Main)).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);

            if (!IsDirectoryWritable(theDirectory, false))
            {
                MessageBox.Show(
                    $"This application was built to be portable.\n\nIt needs to be able to write the configuration in {theDirectory}", 
                    "XwRemote", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            if (File.Exists("XwRemote.dat") && !File.Exists("#XwRemoteServerDatabase#"))
            {
                File.Move("XwRemote.dat", "#XwRemoteServerDatabase#");
            }

            UpgradeConfigDB();
            Refresh();
        }

        //*************************************************************************************************************
        public bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(dirPath, Path.GetTempFileName()),1,FileOptions.DeleteOnClose))
                { }
                return true;
            }
            catch
            {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }

        //*************************************************************************************************************
        private void Refresh()
        {
            LoadValues();
            LoadGroups();
            LoadServers();
        }

        //*************************************************************************************************************
        public static string GetDBPath()
        {
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Main)).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            return Path.Combine(theDirectory, "#XwRemoteServerDatabase#");
        }

        //*************************************************************************************************************
        public static string GetConnectionString()
        {
            string connString = $"Data Source={GetDBPath()};Version=3;";
            if (MasterPassword != "")
                connString += $"Password={MasterPassword};";
            return connString;
        }

        //*************************************************************************************************************
        //This could be condensed in fewer commands, but would breake compatibility with previous versions
        private void UpgradeConfigDB()
        {
            //Get password ?!?
            if (File.Exists(GetDBPath()))
            {
                using (XwDbCommand tryOne = new XwDbCommand(GetConnectionString(), "Data.SQLite"))
                {
                    try
                    {
                        tryOne.ExecuteTX("SELECT * FROM System");
                    }
                    catch (Exception ex1)
                    {
                        if (ex1.Message.Contains("file is not a database"))
                        {
                            AskPassword ask = new AskPassword();
                            ask.ShowDialog();
                            Config.MasterPassword = ask.PasswordBox.Text;

                            
                            using (XwDbCommand tryTwo = new XwDbCommand(GetConnectionString(), "Data.SQLite"))
                            {
                                try
                                {
                                    tryTwo.ExecuteTX("SELECT * FROM System");
                                }
                                catch (Exception ex2)
                                {
                                    if (ex2.Message.Contains("file is not a database"))
                                    {
                                        MessageBox.Show("Bad password or config is corrupted.", "XwRemote", 
                                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        Environment.Exit(1);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            using (XwDbCommand sql = new XwDbCommand(GetConnectionString(), "Data.SQLite"))
            {
                if (!sql.TableExists("System"))
                {
                    sql.ExecuteTX(@"CREATE TABLE [System] (
                      [SystemKey] TEXT, 
                      [SystemValue] TEXT, 
                      CONSTRAINT [] PRIMARY KEY ([SystemKey]));");
                }

                if (!sql.TableExists("Groups"))
                {
                    sql.ExecuteTX(@"CREATE TABLE [Groups] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
                        [Name] TEXT, 
                        [Icon] INTEGER);");
                }

                if (!sql.TableExists("Servers"))
                {
                    sql.ExecuteTX(@"CREATE TABLE [Servers] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
                        [Name] TEXT, 
                        [Host] TEXT, 
                        [Port] INTEGER, 
                        [X] INTEGER, 
                        [Y] INTEGER, 
                        [Username] TEXT, 
                        [Password] TEXT, 
                        [Color] INTEGER, 
                        [SendKeys] BOOLEAN DEFAULT (1), 
                        [GroupID] INTEGER CONSTRAINT [Servers_GroupID] REFERENCES [Groups]([ID]), 
                        [Sound] BOOLEAN, 
                        [Drives] BOOLEAN, 
                        [ServerType] INTEGER, 
                        [Favorite] BOOLEAN,
                        [Console] BOOLEAN DEFAULT (0),
                        [AutoScale] BOOLEAN DEFAULT (1),
                        [SSH1] BOOLEAN DEFAULT (0));");
                }

                if (!sql.TableExists("Pins"))
                {
                    sql.ExecuteTX(@"CREATE TABLE [Pins] (
                        [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
                        [ServerID] INTEGER CONSTRAINT [Pins_ServerID] REFERENCES [Servers]([ID]), 
                        [Local] BOOLEAN DEFAULT (0), 
                        [Path] TEXT);");
                }

                if (!sql.ColumnExists("Pins", "LinkTo"))
                {
                    sql.ExecuteTX("ALTER TABLE Pins ADD COLUMN LinkTo INTEGER;");
                }

                if (!sql.ColumnExists("Servers", "Passive"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN Passive BOOLEAN;");
                    sql.ExecuteTX("UPDATE Servers SET Passive=1 WHERE ServerType=3;");
                }

                if (!sql.ColumnExists("Servers", "Themes"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN Themes BOOLEAN;");
                    sql.ExecuteTX("UPDATE Servers SET Themes=0");
                }

                if (!sql.ColumnExists("Servers", "Certificates"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN Certificates BOOLEAN;");
                    sql.ExecuteTX("UPDATE Servers SET Certificates=0");
                }

                if (!sql.ColumnExists("Servers", "UseHtmlLogin"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN UseHtmlLogin BOOLEAN;");
                    sql.ExecuteTX("UPDATE Servers SET UseHtmlLogin=0");
                }

                if (!sql.ColumnExists("Servers", "HtmlUserBox"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN HtmlUserBox TEXT;");
                    sql.ExecuteTX("UPDATE Servers SET HtmlUserBox=''");
                }

                if (!sql.ColumnExists("Servers", "HtmlPassBox"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN HtmlPassBox TEXT;");
                    sql.ExecuteTX("UPDATE Servers SET HtmlPassBox=''");
                }

                if (!sql.ColumnExists("Servers", "HtmlLoginBtn"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN HtmlLoginBtn TEXT;");
                    sql.ExecuteTX("UPDATE Servers SET HtmlLoginBtn=''");
                }

                if (!sql.ColumnExists("Servers", "TabColor"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN TabColor INTEGER;");
                    sql.ExecuteTX("UPDATE Servers SET TabColor=-4144960");
                }
                
                if (!sql.ColumnExists("Servers", "TabColorTmp"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN TabColorTmp INTEGER;");
                    sql.ExecuteTX("UPDATE Servers SET TabColor=-4144960 WHERE TabColor=-5192482");
                }

                if (!sql.ColumnExists("Servers", "Notes"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN Notes TEXT;");
                    sql.ExecuteTX("UPDATE Servers SET Notes=''");
                }

                if (!sql.ColumnExists("Servers", "SshTerminal"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN SshTerminal INTEGER;");
                    sql.ExecuteTX("UPDATE Servers SET SshTerminal=1");
                }

                if (!sql.ColumnExists("Groups", "Expanded"))
                {
                    sql.ExecuteTX("ALTER TABLE Groups ADD COLUMN Expanded BOOLEAN;");
                    sql.ExecuteTX("UPDATE Groups SET Expanded=0");
                }

                if (!sql.ColumnExists("Servers", "Encryption"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN Encryption BOOLEAN;");
                    sql.ExecuteTX("UPDATE Servers SET Encryption=0");
                }

                if (!sql.ColumnExists("Servers", "SshKey"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN SshKey TEXT;");
                    sql.ExecuteTX("UPDATE Servers SET SshKey=''");
                }

                if (!sql.ColumnExists("Servers", "FtpDataType"))
                {
                    sql.ExecuteTX("ALTER TABLE Servers ADD COLUMN FtpDataType INTEGER;");
                    sql.ExecuteTX("UPDATE Servers SET FtpDataType=0 WHERE ServerType=3;");
                }

                sql.ExecuteTX("UPDATE Servers SET TabColor=-4144960 WHERE TabColor=0");
            }
        }

        //*************************************************************************************************************
        public string GetValue(string key, string defaultValue = "")
        {
            if (!values.ContainsKey(key))
                return defaultValue;

            return values[key];
        }

        //*************************************************************************************************************
        public void SetValue(string key, string value)
        {
            values[key] = value;
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter(@"KEY", key);
                sql.AddParameter(@"VALUE", value);
                sql.ExecuteTX(@"INSERT OR REPLACE INTO System (SystemKey,SystemValue) VALUES (@KEY,@VALUE)");
            }
        }

        //*************************************************************************************************************
        private void LoadServers()
        {
            Main.servers.Clear();
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($@"SELECT 
                    id
                    ,name
                    ,host
                    ,ServerType
                    ,Username
                    ,Password
                    ,port   
                    ,Sendkeys
                    ,sound
                    ,drives
                    ,favorite
                    ,groupid
                    ,color
                    ,X
                    ,Y
                    ,Console
                    ,AutoScale
                    ,SSH1
                    ,Passive
                    ,Themes
                    ,Certificates
                    ,UseHtmlLogin
                    ,HtmlUserBox
                    ,HtmlPassBox
                    ,HtmlLoginBtn
                    ,TabColor
                    ,Notes
                    ,SshTerminal
                    ,Encryption
                    ,SshKey
                    ,FtpDataType
                    FROM Servers
                    ORDER BY name
                    COLLATE NOCASE");

                while (sql.Read())
                {
                    ServerType type = (ServerType)sql.Value("ServerType").ToInt32();
                    var server = Server.GetServerInstance(type);
                    server.ID = sql.Value("id").ToInt32();
                    server.Type = type;
                    server.Name = sql.Value("name").ToString();
                    server.Host = sql.Value("host").ToString();
                    server.Username = sql.Value("username").ToString();
                    server.Password = sql.Value("password").ToString();
                    server.Port = sql.Value("port").ToInt32();
                    server.SendKeys = sql.Value("sendkeys").ToBoolean();
                    server.UseSound = sql.Value("sound").ToBoolean();
                    server.ConnectDrives = sql.Value("drives").ToBoolean();
                    server.IsFavorite = sql.Value("favorite").ToBoolean();
                    server.GroupID = sql.Value("GroupID").ToInt32();
                    server.Color = sql.Value("Color").ToInt32();
                    server.Width = sql.Value("X").ToInt32();
                    server.Height = sql.Value("Y").ToInt32();
                    server.AutoScale = sql.Value("AutoScale").ToBoolean();
                    server.SSH1 = sql.Value("SSH1").ToBoolean();
                    server.Passive = sql.Value("Passive").ToBoolean();
                    server.Themes = sql.Value("Themes").ToBoolean();
                    server.Certificates = sql.Value("Certificates").ToBoolean();
                    server.UseHtmlLogin = sql.Value("UseHtmlLogin").ToBoolean();
                    server.HtmlUserBox = sql.Value("HtmlUserBox").ToString();
                    server.HtmlPassBox = sql.Value("HtmlPassBox").ToString();
                    server.HtmlLoginBtn = sql.Value("HtmlLoginBtn").ToString();
                    server.TabColor = sql.Value("TabColor").ToInt32();
                    server.Notes = sql.Value("Notes").ToString();
                    server.SshTerminal = sql.Value("SshTerminal").ToInt32();
                    server.Encryption = sql.Value("Encryption").ToBoolean();
                    server.SshKey = sql.Value("SshKey").ToString();
                    server.FtpDataType = sql.Value("FtpDataType").ToInt32();
                    Main.servers.Add(server);
                }
            }
        }

        //*************************************************************************************************************
        private void LoadGroups()
        {
            groups.Clear();
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX("SELECT id,name,expanded FROM groups ORDER BY name COLLATE NOCASE");
                while (sql.Read())
                {
                    Group group = new Group();
                    group.ID = sql.Value("id").ToInt32();
                    group.Name = sql.Value("name").ToString();
                    group.Expanded = sql.Value("expanded").ToBoolean();
                    groups.Add(group);
                }
            }
        }

        //*************************************************************************************************************
        private void LoadValues()
        {
            values.Clear();
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX("SELECT SystemKey, SystemValue FROM System");
                while (sql.Read())
                {
                    values.Add(sql.Value("SystemKey").ToString(), sql.Value("SystemValue").ToString());
                }
            }
        }

        //*************************************************************************************************************
        public  void SaveServer(Server server)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("Name", server.Name);
                sql.AddParameter("Host", server.Host);
                sql.AddParameter("Username", server.Username);
                sql.AddParameter("Password", server.Password);
                sql.AddParameter("Favorite", server.IsFavorite);
                sql.AddParameter("ServerType", (int)server.Type);
                sql.AddParameter("GroupID", server.GroupID);
                sql.AddParameter("Color", server.Color);
                sql.AddParameter("SendKeys", server.SendKeys);
                sql.AddParameter("Sound", server.UseSound);
                sql.AddParameter("Drives", server.ConnectDrives);
                sql.AddParameter("Port", server.Port);
                sql.AddParameter("X", server.Width);
                sql.AddParameter("Y", server.Height);
                sql.AddParameter("AutoScale", server.AutoScale);
                sql.AddParameter("SSH1", server.SSH1);
                sql.AddParameter("Passive", server.Passive);
                sql.AddParameter("Themes", server.Themes);
                sql.AddParameter("Certificates", server.Certificates);
                sql.AddParameter("UseHtmlLogin", server.UseHtmlLogin);
                sql.AddParameter("HtmlUserBox", server.HtmlUserBox);
                sql.AddParameter("HtmlPassBox", server.HtmlPassBox);
                sql.AddParameter("HtmlLoginBtn", server.HtmlLoginBtn);
                sql.AddParameter("TabColor", server.TabColor);
                sql.AddParameter("Notes", server.Notes);
                sql.AddParameter("SshTerminal", server.SshTerminal);
                sql.AddParameter("Encryption", server.Encryption);
                sql.AddParameter("SshKey", server.SshKey);
                sql.AddParameter("FtpDataType", server.FtpDataType);

                if (server.ID == 0)
                {
                    sql.Make(MakeType.INSERT, "Servers");
                }
                else
                {
                    sql.AddParameter("ID", server.ID);
                    sql.Make(MakeType.UPDATE, "Servers", "id=@ID");
                }

                sql.ExecuteMK();
            }
            
            Refresh();
        }

        //*************************************************************************************************************
        public void DeleteServer(Server server)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("ID", server.ID);
                sql.ExecuteTX("DELETE FROM servers WHERE ID=@ID");
            }
            Refresh();
        }

        //*************************************************************************************************************
        public void SetServerFavourite(Server server)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("ID", server.ID);
                sql.ExecuteTX("UPDATE servers SET Favorite=1 WHERE id=@ID");
            }
            Refresh();
        }

        //*************************************************************************************************************
        public void SaveGroup(Group group)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("Name", (group.Name == null) ? "New Group" : group.Name);
                sql.AddParameter("Expanded", group.Expanded);
                if (group.ID == 0)
                {
                    sql.Make(MakeType.INSERT, "Groups");
                }
                else
                {
                    sql.AddParameter("@ID", group.ID);
                    sql.Make(MakeType.UPDATE, "Groups", "id=@ID");
                }
                sql.ExecuteMK();
            }
            Refresh();
        }


        //*************************************************************************************************************
        public void DeleteGroup(Group group)
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("ID", group.ID);
                sql.ExecuteTX("UPDATE servers SET GroupID=0 WHERE GroupID=@ID", false);
                sql.ExecuteTX("DELETE FROM groups WHERE ID=@ID");
            }
            Refresh();
        }

        //*************************************************************************************************************
        public void CollapseGroup(Group group)
        {
            group.Expanded = false;
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("ID", group.ID);
                sql.ExecuteTX("UPDATE groups SET Expanded=0 WHERE ID=@ID");
            }
        }

        //*************************************************************************************************************
        public void ExpandGroup(Group group)
        {
            group.Expanded = true;
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.AddParameter("ID", group.ID);
                sql.ExecuteTX("UPDATE groups SET Expanded=1 WHERE ID=@ID");
            }
        }

        //*************************************************************************************************************
        public bool ShowExperimentalFeatures()
        {
            return Main.config.GetValue("EXPERIMENTAL-STUFF", false.ToString()).ToBoolOrDefault(false);
        }
    }
}
