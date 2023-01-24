using FluentFTP.Helpers;
using ShellDll;
using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwMaxLib.Objects;
using XwRemote.Properties;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public class LocalList : ListView
    {
        //*************************************************************************************************************
        public IOForm form = null;
        private FileSystemWatcher fileSystemWatcher = new System.IO.FileSystemWatcher();
        public string CurrentDirectory = string.Empty;
        private ContextMenuStrip contextMenu = new ContextMenuStrip();
        FileListSorter sorter = null;
        public bool CheckLink = false;
        
        //*************************************************************************************************************
        public LocalList()
        {
            FullRowSelect = true;
            Columns.Add("Name", 205);
            Columns.Add("Size", 100, HorizontalAlignment.Right);
            Columns.Add("Last Update", 125);
            
            DoubleClick += new System.EventHandler(this_DoubleClick);
            ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this_ItemDrag);
            DragDrop += new System.Windows.Forms.DragEventHandler(this_DragDrop);
            DragEnter += new System.Windows.Forms.DragEventHandler(this_DragEnter);
            KeyDown += new System.Windows.Forms.KeyEventHandler(this_KeyDown);
            MouseUp += new System.Windows.Forms.MouseEventHandler(this_MouseUp);
            AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this_AfterLabelEdit);

            fileSystemWatcher.IncludeSubdirectories = false;
            fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size;

            fileSystemWatcher.SynchronizingObject = this;
            fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher_Renamed);
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.List_ColumnClick);
        }

        //*************************************************************************************************************
        public void Init(IOForm f)
        {
            form = f;
            LoadPins();
            ContextMenuStrip = contextMenu;
        }

        //*************************************************************************************************************
        private void this_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedItems.Count == 0)
                return;

            DiskItem di = ((DiskItem)(SelectedItems[0]).Tag);

            if (di.IsDirectory)
            {
                if (di.path == ".")
                {
                    int li = CurrentDirectory.Replace("\\\\", "").IndexOf("\\");
                    if (CurrentDirectory.Contains("\\\\"))
                        li += 2;
                    
                    if (li > 0)
                    {
                        string path = CurrentDirectory.Substring(0, li + 1);

                        if (path.Contains("\\\\"))
                        {
                            if (path.EndsWith("\\"))
                                path = path.Remove(path.Length - 1, 1);
                        }

                        LoadList(path);
                    }
                    return;
                }

                if (di.path == "..")
                {
                    int li = CurrentDirectory.LastIndexOf("\\");
                    if (li > 0)
                    {
                        string path = CurrentDirectory.Substring(0, li);
                        if (!path.Contains("\\"))
                            path += "\\";
                        LoadList(path);
                    }
                    return;
                }

                if (di.IsDirectory == true)
                {
                    LoadList(di.path);
                }
            }
            else
                Menu_Upload_Click(sender, e);
        }

        //*************************************************************************************************************
        private void this_ItemDrag(object sender, ItemDragEventArgs e)
        {
            AllowDrop = false;
            form.RemoteList.AllowDrop = true;
            DoDragDrop(SelectedItems, DragDropEffects.Move);
        }

        //*************************************************************************************************************
        private void this_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //*************************************************************************************************************
        private void this_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) //from explorer
            {
                string[] filePaths = (string[])e.Data.GetData("FileDrop");
            }
            else //from listview
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection items =
                    (System.Windows.Forms.ListView.SelectedListViewItemCollection)e.Data.GetData(
                    typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection));
                
                foreach (ListViewItem item in items)
                {
                    DiskItem disk = (DiskItem)item.Tag;
                    form.QueueList.QueueDownloadItem(
                        disk.IsDirectory, disk.path, form.LocalList.CurrentDirectory, 
                        disk.name, item.ImageIndex, disk.size);
                }

                form.QueueList.StartQueue();
            }
            
            AllowDrop = true;
            form.RemoteList.AllowDrop = true;
        }

        //*************************************************************************************************************
        private void DeleteSelectedItems()
        {
            if (MessageBox.Show("Delete the selected items?", "", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in SelectedItems)
                {
                    try
                    {
                        DiskItem diskItem = (DiskItem)item.Tag;

                        if (diskItem.path == "." || diskItem.path == "..")
                            continue;

                        if (diskItem.IsDirectory)
                        {
                            Directory.Delete(diskItem.path, true);
                        }
                        else
                        {
                            File.Delete(diskItem.path);
                        }

                        item.Remove();
                    }
                    catch (Exception ex)
                    {
                        form.Log(ex.Message, Color.Red);
                    }
                }

                RealLoadList(CurrentDirectory, true);
            }
        }

        //*************************************************************************************************************
        public void LoadList(string path)
        {
            if (path == CurrentDirectory)
                return;

            if (path.StartsWith("\\\\"))
                RealLoadList(path, false);
            else
                form.LocalTree.SelectPath(path);

            //if (form.LocalTree.SelectPath(path))
            //RealLoadList(path, false);

            CheckPin();
        }

        //*************************************************************************************************************
        public void RealLoadList(string path, bool skipCheckLink)
        {
            fileSystemWatcher.EnableRaisingEvents = false;

            form.SetLocalStatusText("");
            BackColor = Color.FromArgb(240, 240, 240);

            BeginUpdate();
            Items.Clear();

            if (skipCheckLink)
                form.SkipCheckLink = true;
            
            if (path == string.Empty && form.LocalTree.SelectedNode != null) //my computer
            {
                foreach (TreeNode n in form.LocalTree.SelectedNode.Nodes)
                {
                    ListViewItem i = Items.Add(n.Text, n.ImageIndex);
                    i.Tag = new DiskItem(true, false, n.Tag.ToString(), "");
                }

                form.SetLocalStatusText($"{Items.Count} Items");
            }
            else
            {
                try
                {
                    if (path.Contains("\\\\") && !path.Replace("\\\\", "").Contains("\\"))
                    {
                        try
                        {
                            //New instance of the management path so we can use its properties
                            ManagementPath mpath = new ManagementPath();
                            //Set the Servername
                            mpath.Server = path.Replace("\\\\", "");
                            //Set the WMI namespace
                            mpath.NamespacePath = @"root\cimv2";
                            //Here we are using the default connections but we can also use different.
                            //Username and password if we need to.
                            ConnectionOptions oConn = new ConnectionOptions();
                            //Set the Scope ...Computername and WMI namespace
                            ManagementScope scope = new ManagementScope(path, oConn);
                            //Set the WMI Class
                            mpath.RelativePath = "Win32_Share";

                            //Set shares to null
                            ManagementClass Shares = null;
                            //Here we are connecting using the Servername and WMI Namespace/Class
                            using (Shares = new ManagementClass(scope, mpath, null))
                            {
                                //Return a collection of Shares here
                                ManagementObjectCollection moc = Shares.GetInstances();

                                //Go thru each share and display its name property in the list box.
                                foreach (ManagementObject mo in moc)
                                {
                                    string share = mo["Name"].ToString();
                                    ListViewItem item = new ListViewItem(share, 0);
                                    item.Tag = new DiskItem(true, false, Path.Combine(path, share), share);
                                    item.ImageIndex = ShellImageList.GetSpecialFolderImageIndex((ShellAPI.CSIDL)49);
                                    Items.Add(item);
                                }
                            }
                        }
                        catch (Exception) //catch any exceptions we might have .
                        {
                            MessageBox.Show("Unable to return sharenames . Please make share Servername is correct.");
                        }

                        form.SetLocalStatusText($"{Items.Count} Items");
                    }
                    else
                    {
                        DirectoryInfo nodeDirInfo = new DirectoryInfo(path);
                        ListViewItem.ListViewSubItem[] subItems;
                        ListViewItem item = null;

                        ListViewItem item1 = Items.Add(".", ShellImageList.GetFileImageIndex(".", FileAttributes.Directory));
                        item1.Tag = new DiskItem(true, false, ".", ".");
                        item1.BackColor = Color.FromArgb(240, 240, 240);
                        ListViewItem item2 = Items.Add("..", ShellImageList.GetFileImageIndex("Folder", FileAttributes.Directory));
                        item2.Tag = new DiskItem(true, false, "..", "..");
                        item2.BackColor = Color.FromArgb(230, 230, 230);

                        int ndirs = 0;
                        var dirs = nodeDirInfo.EnumerateDirectories();
                        foreach (DirectoryInfo dir in dirs)
                        {
                            if ((dir.Attributes & FileAttributes.Hidden) != 0)
                                continue;

                            item = new ListViewItem(dir.Name, 0);
                            subItems = new ListViewItem.ListViewSubItem[]
                            {
                                new ListViewItem.ListViewSubItem(item, " - - - "), 
                                new ListViewItem.ListViewSubItem(item, 
						        dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"))
                            };

                            item.SubItems.AddRange(subItems);
                            item.Tag = new DiskItem(true, false, dir.FullName, dir.Name);
                            item.ImageIndex = ShellImageList.GetFileImageIndex(dir.FullName, dir.Attributes);
                            Items.Add(item);
                            ndirs++;
                        }

                        int nfiles = 0;
                        var files = nodeDirInfo.EnumerateFiles();
                        foreach (FileInfo file in files)
                        {
                            if ((file.Attributes & FileAttributes.Hidden) != 0)
                                continue;

                            item = new ListViewItem(file.Name, 1);
                            subItems = new ListViewItem.ListViewSubItem[]
                            { 
                                new ListViewItem.ListViewSubItem(item, string.Format("{0:0,0}", file.Length)), 
                                new ListViewItem.ListViewSubItem(item, 
						        file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"))
                            };

                            item.SubItems.AddRange(subItems);
                            item.Tag = new DiskItem(false, false, file.FullName, file.Name, file.Length);
                            item.ImageIndex = ShellImageList.GetFileImageIndex(file.FullName, file.Attributes);
                            Items.Add(item);
                            nfiles++;
                        }

                        form.SetLocalStatusText($"{ndirs+nfiles} Items: {ndirs} Folders, {nfiles} Files");

                        fileSystemWatcher.Path = path;
                        fileSystemWatcher.EnableRaisingEvents = true;

                        CurrentDirectory = path;
                        form.LocalPath.Text = CurrentDirectory;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RealLoadList(CurrentDirectory, false);
                }
            }

            EndUpdate();
            BackColor = SystemColors.Window;
        }

        //*************************************************************************************************************
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!fileSystemWatcher.EnableRaisingEvents)
                return;

            fileSystemWatcher.EnableRaisingEvents = false;
            //Because sometimes a folder is Deleted just to be created again
            //It prevent the list from going into the root
            if (e.ChangeType == WatcherChangeTypes.Deleted)
                Thread.Sleep(250);

            ReloadListFromFileWatcher();
        }

        //*************************************************************************************************************
        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (!fileSystemWatcher.EnableRaisingEvents)
                return;

            fileSystemWatcher.EnableRaisingEvents = false;
            ReloadListFromFileWatcher();
        }

        //*************************************************************************************************************
        private void ReloadListFromFileWatcher()
        {
            if (form.QueueList.Items.Count == 0)
            {
                if (Directory.Exists(CurrentDirectory))
                {
                    RealLoadList(CurrentDirectory, true);
                }
            }
        }

        //*************************************************************************************************************
        private void LoadPins()
        {
            ComboBox box = form.LocalPath;
            box.Items.Clear();

            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT ID,path FROM Pins WHERE Local=1 AND ServerID={form.server.ID} ORDER BY Path");
                while (sql.Read())
                    box.Items.Add(new ListItem(sql.Value("ID").ToInt32(), sql.Value("Path").ToString()));
            }
        }

        //*************************************************************************************************************
        public int PinFolder()
        {
            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT id FROM Pins WHERE ServerID={form.server.ID} AND Local=1 AND Path='{CurrentDirectory}'");
                if (sql.Read())
                {
                    return sql.Value(0).ToInt32();
                }
                else
                {
                    sql.ExecuteTX($"INSERT INTO Pins (ServerID, Local, Path) VALUES ({form.server.ID}, 1, '{CurrentDirectory}')");
                    sql.ExecuteTX("SELECT MAX(id) FROM Pins");
                    sql.Read();
                    int id = sql.Value(0).ToInt32();
                    LoadPins();
                    form.LocalPin.Image = Resources.PinDown;
                    form.localPinTip.SetToolTip(form.LocalPin, "Unpin Folder");
                    return id;
                }
            }
        }

        //*************************************************************************************************************
        public void UnpinFolder()
        {
            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                int index = form.LocalPath.FindStringExact(CurrentDirectory);
                int ID = ((ListItem)form.LocalPath.Items[index]).ID;
                sql.ExecuteTX($"DELETE FROM Pins WHERE ID={ID}");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE LinkTo={ID}");
                LoadPins();
                form.LocalPin.Image = Resources.PinUp;
                form.localPinTip.SetToolTip(form.LocalPin, "Pin Folder for future use");
                
                form.LinkPath.Tag = false;
                form.LinkPath.Image = Resources.link_break;
                form.linkTip.SetToolTip(form.LinkPath, "Link local and remote folders");
            }
        }

        //*************************************************************************************************************
        private async void CheckPin()
        {
            //TODO: Move this into the Config
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT * FROM pins WHERE Local=1 AND path='{CurrentDirectory}' AND ServerID={form.server.ID}");
                if (sql.Read())
                {
                    form.LocalPin.Image = Resources.PinDown;
                    form.localPinTip.SetToolTip(form.LocalPin, "Unpin Folder");

                    if (CheckLink)
                        await form.CheckLink(CurrentDirectory, true);
                }
                else
                {
                    form.LocalPin.Image = Resources.PinUp;
                    form.localPinTip.SetToolTip(form.LocalPin, "Pin Folder for future use");
                }
            }

            CheckLink = false;
        }

        //*************************************************************************************************************
        private void this_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenu.Items.Clear();

                ListViewHitTestInfo info = HitTest(new Point(e.X, e.Y));

                ToolStripMenuItem menuitemRefresh = 
                    new ToolStripMenuItem("Refresh", Resources.refresh, Menu_Refresh_Click, "Refresh");
                menuitemRefresh.ShortcutKeyDisplayString = "F5";
                contextMenu.Items.Add(menuitemRefresh);

                if (info.Item != null)
                {
                    ToolStripMenuItem menuitemUpload = 
                        new ToolStripMenuItem("Upload", Resources.upload, Menu_Upload_Click, "Upload");
                    contextMenu.Items.Add(menuitemUpload);

                    ToolStripMenuItem menuitemDelete = 
                        new ToolStripMenuItem("Delete", Resources.delete, Menu_Delete_Click, "Delete");
                    menuitemDelete.ShortcutKeyDisplayString = "Del";
                    contextMenu.Items.Add(menuitemDelete);

                    ToolStripMenuItem menuitemRename = 
                        new ToolStripMenuItem("Rename", Resources.rename, Menu_Rename_Click, "Rename");
                    menuitemRename.ShortcutKeyDisplayString = "F2";
                    contextMenu.Items.Add(menuitemRename);
                }

                ToolStripMenuItem menuitemCreateFolder = 
                    new ToolStripMenuItem("Create Folder", Resources.folder, Menu_CreateFolder_Click, "CreateFolder");
                menuitemCreateFolder.ShortcutKeyDisplayString = "  ";
                contextMenu.Items.Add(menuitemCreateFolder);
            }
        }

        //*************************************************************************************************************
        private void Menu_CreateFolder_Click(object sender, EventArgs e)
        {
            LabelEdit = true;
            ListViewItem item = Items.Add(new ListViewItem("New Folder"));
            item.BeginEdit();
            item.EnsureVisible();
        }

        //*************************************************************************************************************
        private void this_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (Regex.IsMatch(e.Label, @"[:\*\\/\?""<>|]"))
                {
                    MessageBox.Show("The provided name has some invalid characters", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Items[e.Item].BeginEdit();
                    return;
                }
                
                LabelEdit = false;
                e.CancelEdit = true;

                string newDir = Path.Combine(CurrentDirectory, e.Label);

                if (Items[e.Item].Tag == null) //new folder
                {
                    if (!Directory.Exists(newDir))
                        Directory.CreateDirectory(newDir);
                }
                else
                {
                    DiskItem disk = (DiskItem)Items[e.Item].Tag;

                    if (disk.IsDirectory)
                    {
                        if (Directory.Exists(newDir))
                            MessageBox.Show("There is already a folder with that name", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            Directory.Move(disk.path, newDir);
                    }
                    else
                    {
                        if (File.Exists(newDir))
                            MessageBox.Show("There is already a file with that name", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            File.Move(disk.path, newDir);
                    }

                }
            }

            RealLoadList(CurrentDirectory, true);
        }

        //*************************************************************************************************************
        private void Menu_Refresh_Click(object sender, EventArgs e)
        {
            RealLoadList(CurrentDirectory, true);
        }

        //*************************************************************************************************************
        private void Menu_Upload_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection items = SelectedItems;
            foreach (ListViewItem item in items)
            {
                DiskItem disk = (DiskItem)item.Tag;
                form.QueueList.QueueUploadItem(disk.IsDirectory, disk.path, 
                    form.RemoteList.CurrentDirectory, disk.name, item.ImageIndex, disk.size);
            }

            form.QueueList.StartQueue();
        }

        //*************************************************************************************************************
        private void Menu_Delete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        //*************************************************************************************************************
        private void Menu_Rename_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in SelectedItems)
                {
                    if (item.Focused)
                    {
                        SelectedItems.Clear();
                        LabelEdit = true;
                        item.BeginEdit();
                        break;
                    }
                }
            }
        }

        //*************************************************************************************************************
        private void List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (sorter == null)
                sorter = new FileListSorter();
            
            sorter.column = e.Column;

            if (sorter.column == 0)
                sorter.type = ListViewDataSorterType.Text;
            if (sorter.column == 1)
                sorter.type = ListViewDataSorterType.Numeric;
            if (sorter.column == 2)
                sorter.type = ListViewDataSorterType.Date;
            
            ListViewItemSorter = sorter;

            if (sorter.Order == SortOrder.Ascending)
                sorter.Order = SortOrder.Descending;
            else
                sorter.Order = SortOrder.Ascending;

            Sort();
        }

        //*************************************************************************************************************
        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedItems();
                return;
            }

            if (e.KeyCode == Keys.F5)
            {
                RealLoadList(CurrentDirectory, true);
                return;
            }

            if (e.KeyCode == Keys.F2)
            {
                Menu_Rename_Click(sender, null);
                return;
            }
            
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in Items)
                {
                    if (item.Text == "." || item.Text == "..")
                        item.Selected = false;
                    else
                        item.Selected = true;
                }
                return;
            }
        }
    }
}