using ShellDll;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Data;
using XwMaxLib.Extensions;
using XwMaxLib.Objects;
using XwRemote.Properties;
using XwRemote.Servers.IO;
using XwRemote.Settings;
using static XwRemote.Servers.IO.XwRemoteIO;

namespace XwRemote.Servers
{
    public class RemoteList : ListView
    {
        //*************************************************************************************************************
        private IOForm form = null;
        private ContextMenuStrip contextMenu = new ContextMenuStrip();
        public string CurrentDirectory = "/";
        FileListSorter sorter = null;
        public bool CheckLick = false;
        private XwRemoteIO remoteIO = null;

        //*************************************************************************************************************
        public RemoteList()
        {
            FullRowSelect = true;
            Columns.Add("Name", 220);
            Columns.Add("Size", 100, HorizontalAlignment.Right);
            Columns.Add("Last Update", 125);

            DoubleClick += new System.EventHandler(this_DoubleClick);
            ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this_ItemDrag);
            DragDrop += new System.Windows.Forms.DragEventHandler(this_DragDrop);
            DragEnter += new System.Windows.Forms.DragEventHandler(this_DragEnter);
            KeyDown += new System.Windows.Forms.KeyEventHandler(this_KeyDown);
            MouseUp += new System.Windows.Forms.MouseEventHandler(this_MouseUp);
            AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this_AfterLabelEdit);
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.List_ColumnClick);
        }

        //*************************************************************************************************************
        public void Init(IOForm f, XwRemoteIO remote)
        {
            form = f;
            remoteIO = remote;
            LoadPins();
            ContextMenuStrip = contextMenu;
        }

        //*************************************************************************************************************
        private async void this_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedItems.Count == 0)
                return;

            DiskItem di = ((DiskItem)(SelectedItems[0]).Tag);
            if (di.IsDirectory)
                await LoadList(di.path);
            else
                Menu_Download_Click(sender, e);
        }

        //*************************************************************************************************************
        private void this_ItemDrag(object sender, ItemDragEventArgs e)
        {
            form.LocalList.AllowDrop = true;
            AllowDrop = false;
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
                Cursor.Current = Cursors.WaitCursor;
                foreach (string path in filePaths)
                {
                    DiskItem disk;
                    int imageIndex = 0;

                    if (Directory.Exists(path))
                    {
                        disk = new DiskItem(true, false, path, Path.GetFileName(path));
                        imageIndex = ShellImageList.GetFileImageIndex(disk.path, FileAttributes.Directory);
                    }
                    else
                    {
                        FileInfo f = new FileInfo(path);
                        disk = new DiskItem(false, false, path, Path.GetFileName(path), f.Length);
                        imageIndex = ShellImageList.GetFileImageIndex(disk.path, File.GetAttributes(path));
                    }
                    form.QueueList.QueueUploadItem(disk.IsDirectory, disk.path, 
                        CurrentDirectory, disk.name, imageIndex, disk.size);
                }
            }
            else //from listview
            {
                System.Windows.Forms.ListView.SelectedListViewItemCollection items =
                    (System.Windows.Forms.ListView.SelectedListViewItemCollection)e.Data.GetData(
                    typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection));
                Cursor.Current = Cursors.WaitCursor;
                foreach (ListViewItem item in items)
                {
                    DiskItem disk = (DiskItem)item.Tag;
                    form.QueueList.QueueUploadItem(disk.IsDirectory, disk.path, 
                        CurrentDirectory, disk.name, item.ImageIndex, disk.size);
                }
            }

            form.QueueList.StartQueue();

            form.LocalList.AllowDrop = true;
            AllowDrop = true;
        }

        //*************************************************************************************************************
        private async Task DeleteSelectedItems()
        {
            if (MessageBox.Show("Delete the selected items?", "", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                foreach (ListViewItem item in SelectedItems)
                {
                    try
                    {
                        DiskItem diskItem = (DiskItem)item.Tag;

                        if (diskItem.path == "." || diskItem.path == "..")
                            continue;

                        if (diskItem.IsSymlink)
                        {
                            form.Log("Delete Symlinks not implemented yet!", Color.Red);
                            continue;
                        }

                        if (diskItem.IsDirectory)
                        {
                            var result = await remoteIO.DeleteDirectory(diskItem.path);
                            form.Log(result.Message);
                        }
                        else
                        {
                            var result = await remoteIO.DeleteFile(diskItem.path);
                            form.Log(result.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        form.Log(ex.Message, Color.Red);
                    }

                    item.Remove();
                }

                await LoadList(CurrentDirectory);
            }
        }

        //*************************************************************************************************************
        public async Task Load()
        {
            form.loadingCircle1.Active = false;
            form.loadingCircle1.Visible = false;
            form.statusLabel.Visible = false;
            form.splitContainerStatus.Visible = true;
                    
            try
            {
                string loadFolder = Main.config.GetValue("DEFAULT_FTP_LOCAL_FOLDER");
                if (loadFolder == "#MYCOMPUTER#")
                {
                    form.LocalTree.Load();
                    form.LocalTree.SelectPath("");
                }
                if (loadFolder == "#DESKTOP#")
                {
                    form.LocalTree.Load();
                }
                else if (loadFolder == "#LASTUSED#")
                {
                    loadFolder = Main.config.GetValue("FTP_LAST_USED_FOLDER");
                    form.LocalTree.Load();
                    if (Directory.Exists(loadFolder))
                        form.LocalTree.SelectPath(loadFolder);
                }
                else
                {
                    form.LocalTree.Load();
                    form.LocalTree.SelectPath(loadFolder);
                }

                form.statusLabel.Visible = false;
                form.splitContainerStatus.Visible = true;
            }
            catch (Exception ex)
            {
                form.Log(ex.Message, Color.Red);
            }

            await LoadList("/");
        }

        //*************************************************************************************************************
        public async Task LoadList(string path)
        {
            form.SetRemoteStatusText("");
            Cursor.Current = Cursors.WaitCursor;
            BackColor = Color.FromArgb(240, 240, 240);

            try
            {
                if (!remoteIO.IsConnected)
                    await remoteIO.Reconnect();

                if (path == ".")
                    path = "/";
                if (path == "..")
                    path = CurrentDirectory.GetUnixParentPath();
                
                var result = await remoteIO.ListDirectory(path);
                if (result.Success)
                {
                    form.Log(result.Message);

                    BeginUpdate();
                    Items.Clear();

                    ListViewItem item1 = Items.Add(".", ShellImageList.GetFileImageIndex("c:", FileAttributes.Device));
                    item1.Tag = new DiskItem(true, false, ".", "");
                    item1.BackColor = Color.FromArgb(240, 240, 240);
                    ListViewItem item2 = Items.Add("..", ShellImageList.GetFileImageIndex("Folder", FileAttributes.Directory));
                    item2.Tag = new DiskItem(true, false, "..", "");
                    item2.BackColor = Color.FromArgb(230, 230, 230);

                    ListViewItem.ListViewSubItem[] subItems;
                    ListViewItem item = null;

                    int ndirs = 0;
                    foreach (XwRemoteIOItem remoteItem in result.Items)
                    {
                        if (!remoteItem.IsDirectory)
                            continue;

                        item = new ListViewItem(remoteItem.Name, 0);
                        subItems = new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(item, " - - - "),
                        new ListViewItem.ListViewSubItem(item,
                        (remoteItem.Modified == DateTime.MinValue) 
                        ? "- - -" 
                        : remoteItem.Modified.ToString("yyyy-MM-dd HH:mm:ss"))
                        };

                        item.SubItems.AddRange(subItems);
                        item.Tag = new DiskItem(remoteItem.IsDirectory, 
                            remoteItem.IsSymlink, remoteItem.FullName, remoteItem.Name);
                        item.ImageIndex = ShellImageList.GetFileImageIndex(remoteItem.Name, 
                            FileAttributes.Directory | ((remoteItem.IsSymlink) ? FileAttributes.Compressed : 0));

                        Items.Add(item);
                        ndirs++;
                    }

                    int nfiles = 0;
                    foreach (XwRemoteIOItem remoteItem in result.Items)
                    {
                        if (remoteItem.IsDirectory)
                            continue;

                        item = new ListViewItem(remoteItem.Name, 0);
                        subItems = new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(item, string.Format("{0:0,0}", remoteItem.Size)),
                        new ListViewItem.ListViewSubItem(item,
                        (remoteItem.Modified == DateTime.MinValue) 
                        ? "- - -" 
                        : remoteItem.Modified.ToString("yyyy-MM-dd HH:mm:ss"))
                        };

                        item.SubItems.AddRange(subItems);
                        item.Tag = new DiskItem(remoteItem.IsDirectory, remoteItem.IsSymlink, 
                            remoteItem.FullName, remoteItem.Name, remoteItem.Size);
                        item.ImageIndex = ShellImageList.GetFileImageIndex(remoteItem.Name, 
                            FileAttributes.Normal | ((remoteItem.IsSymlink) ? FileAttributes.Compressed : 0));

                        Items.Add(item);
                        nfiles++;
                    }

                    form.SetRemoteStatusText($"{ndirs + nfiles} Items: {ndirs} Folders, {nfiles} Files");
                    EndUpdate();

                    CurrentDirectory = path;
                    form.RemotePath.Text = CurrentDirectory;
                    CheckPin();
                }
                else
                {
                    if (form.splitContainerStatus.Visible) //connecting
                    {
                        form.Log($"An error occurred while retrieving the directory list.\nReason: {result.Message}", 
                            Color.Red);
                    }
                    else
                    {
                        form.loadingCircle1.Active = true;
                        form.loadingCircle1.Visible = false;
                        form.SetStatusText(result.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                form.Log(ex.Message, Color.Red);
                Enabled = true;
            }

            Cursor.Current = Cursors.Default;
            BackColor = SystemColors.Window;
        }

        #region PINS

        //*************************************************************************************************************
        private void LoadPins()
        {
            ComboBox box = form.RemotePath;
            box.Items.Clear();
            
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT ID,path FROM Pins WHERE Local=0 AND ServerID={form.server.ID} ORDER BY Path");
                while (sql.Read())
                    box.Items.Add(new ListItem(sql.Value("ID").ToInt32(), sql.Value("Path").ToString()));
            }
        }

        //*************************************************************************************************************
        public int PinFolder()
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT id FROM Pins WHERE ServerID={form.server.ID} AND Local=0 AND Path='{CurrentDirectory}'");
                if (sql.Read())
                {
                    return sql.Value(0).ToInt32();
                }
                else
                {
                    sql.ExecuteTX($"INSERT INTO Pins (ServerID, Local, Path) VALUES ({form.server.ID}, 0, '{CurrentDirectory}')");
                    sql.ExecuteTX($"SELECT MAX(id) FROM Pins");
                    sql.Read();
                    int id = sql.Value(0).ToInt32();
                    LoadPins();
                    form.RemotePin.Image = Resources.PinDown;
                    form.remotePinTip.SetToolTip(form.RemotePin, "Unpin Folder");
                    return id;
                }
            }
        }

        //*************************************************************************************************************
        public void UnpinFolder()
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                int index = form.RemotePath.FindStringExact(CurrentDirectory);
                int ID = ((ListItem)form.RemotePath.Items[index]).ID;
                sql.ExecuteTX($"DELETE FROM Pins WHERE ID={ID}");
                sql.ExecuteTX($"UPDATE Pins SET LinkTo=NULL WHERE LinkTo={ID}");
                LoadPins();
                form.RemotePin.Image = Resources.PinUp;
                form.remotePinTip.SetToolTip(form.RemotePin, "Pin Folder for future use");

                form.LinkPath.Tag = false;
                form.LinkPath.Image = Resources.link_break;
                form.linkTip.SetToolTip(form.LinkPath, "Link local and remote folders");
            }
        }

        //*************************************************************************************************************
        private async void CheckPin()
        {
            using (XwDbCommand sql = new XwDbCommand(Config.GetConnectionString(), "Data.SQLite"))
            {
                sql.ExecuteTX($"SELECT * FROM pins WHERE Local=0 AND path='{CurrentDirectory}' AND ServerID={form.server.ID}");
                if (sql.Read())
                {
                    form.RemotePin.Image = Resources.PinDown;
                    form.remotePinTip.SetToolTip(form.RemotePin, "Unpin Folder");
                    
                    if (CheckLick)
                        await form.CheckLink(CurrentDirectory, false);
                }
                else
                {
                    form.RemotePin.Image = Resources.PinUp;
                    form.remotePinTip.SetToolTip(form.RemotePin, "Pin Folder for future use");
                }
            }

            CheckLick = false;
        }
        #endregion

        //*************************************************************************************************************
        private void this_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenu.Items.Clear();

                ListViewHitTestInfo info = HitTest(new Point(e.X, e.Y));

                ToolStripMenuItem menuitemRefresh = new ToolStripMenuItem("Refresh", 
                    Resources.refresh, Menu_Refresh_Click, "Refresh");
                menuitemRefresh.ShortcutKeyDisplayString = "F5";
                contextMenu.Items.Add(menuitemRefresh);

                if (info.Item != null)
                {
                    ToolStripMenuItem menuitemDownload = new ToolStripMenuItem("Download", 
                        Resources.download, Menu_Download_Click, "Download");
                    contextMenu.Items.Add(menuitemDownload);

                    ToolStripMenuItem menuitemDelete = new ToolStripMenuItem("Delete", 
                        Resources.delete, Menu_Delete_Click, "Delete");
                    menuitemDelete.ShortcutKeyDisplayString = "Del";
                    contextMenu.Items.Add(menuitemDelete);

                    ToolStripMenuItem menuitemRename = new ToolStripMenuItem("Rename", 
                        Resources.rename, Menu_Rename_Click, "Rename");
                    menuitemRename.ShortcutKeyDisplayString = "F2";
                    contextMenu.Items.Add(menuitemRename);
                }

                ToolStripMenuItem menuitemCreateFolder = new ToolStripMenuItem("Create Folder", 
                    Resources.folder, Menu_CreateFolder_Click, "CreateFolder");
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
        private async void this_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string CurDir = CurrentDirectory;

            try
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
                        if (!await remoteIO.Exists(newDir))
                        {
                            var result = await remoteIO.CreateDirectory(newDir);
                            form.Log(result.Message);
                        }
                    }
                    else
                    {
                        DiskItem disk = (DiskItem)Items[e.Item].Tag;
                        if (await remoteIO.Exists(newDir))
                            MessageBox.Show(string.Format("There is already a {0} with that name", 
                                (disk.IsDirectory) ? "folder" : "file"), "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            var result = await remoteIO.Rename(disk.path, newDir);
                            form.Log(result.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                form.Log(ex.Message, Color.Red);
            }

            await LoadList(CurDir);
        }

        //*************************************************************************************************************
        private void Menu_Download_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection items = SelectedItems;
            Cursor.Current = Cursors.WaitCursor;
            foreach (ListViewItem item in items)
            {
                DiskItem disk = (DiskItem)item.Tag;
                form.QueueList.QueueDownloadItem(disk.IsDirectory, disk.path, 
                    form.LocalList.CurrentDirectory, disk.name, item.ImageIndex, disk.size);
            }
            form.QueueList.StartQueue();
        }

        //*************************************************************************************************************
        private async void Menu_Delete_Click(object sender, EventArgs e)
        {
            await DeleteSelectedItems();
        }

        //*************************************************************************************************************
        private async void Menu_Refresh_Click(object sender, EventArgs e)
        {
            await LoadList(CurrentDirectory);
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
        private async void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                await DeleteSelectedItems();
                return;
            }

            if (e.KeyCode == Keys.F5)
            {
                await LoadList(CurrentDirectory);
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
 