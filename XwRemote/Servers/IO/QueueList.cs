using ShellDll;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwMaxLib.UI;
using XwRemote.Properties;
using XwRemote.Servers.IO;
using static XwRemote.Servers.IO.XwRemoteIO;

namespace XwRemote.Servers
{
    public class QueueList : XwListView
    {
        //*************************************************************************************************************
        private IOForm form = null;
        private ContextMenuStrip contextMenu = new ContextMenuStrip();
        private bool QueueRunning = false;
        public XwFileAction DoToAllFiles = XwFileAction.Ask;
        private List<string> SkipExistsValidation = new List<string>();
        private XwRemoteIO remoteIO = null;

        //*************************************************************************************************************
        public QueueList()
        {
            InsertColumn("Progress", 120);
            InsertColumn("Local", 500);
            InsertColumn("", 18);
            InsertColumn("Remote", 500);
            InsertColumn("Size", 100);
            ShowItemToolTips = true;

            DoubleClick += new System.EventHandler(this_DoubleClick);
            KeyDown += new System.Windows.Forms.KeyEventHandler(this_KeyDown);
            MouseUp += new System.Windows.Forms.MouseEventHandler(this_MouseUp);
        }

        //*************************************************************************************************************
        public void Init(IOForm f, XwRemoteIO remote)
        {
            form = f;
            remoteIO = remote;
            ContextMenuStrip = contextMenu;
            remoteIO.OnFileProgress += OnFileProgress;
        }

        //*************************************************************************************************************
        public bool Disconnect()
        {
            /*
            if (Items.Count > 0)
            {
                foreach (ListViewItem item in Items)
                {
                    QueueItem qi = (QueueItem)item.Tag;
                    ftp.Cancel(qi.TransferID);
                    DeleteByKey(item.Name);
                }
            }
            */
            return true;
        }

        //*************************************************************************************************************
        private void OnFileProgress(XwRemoteIOFileProgressResult result)
        {
            //if (result.Cancel)
            //    return;

            if (Items.Count == 0)
                return;
            
            BeginInvoke((Action)(() =>
            {
                int index = Items.IndexOfKey(result.TreansferID);
                if (index != -1)
                {
                    QueueItem item = (QueueItem)Items[index].Tag;
                    //item.Transferred += result.TranferedBytes;
                    Items[index].Tag = item;
                    SetSubItemProgress(Items[index], 0, result.Percentage);
                    //form.toolStripStatusTransferRate.Text = XwMaxLib.Helper.IO.GetFileSize(e.BytesPerSecond) + "/sec";
                }
            }));
        }

        //*************************************************************************************************************
        public void QueueUploadItem(bool IsDir, string source, string destination, string name, int ImageIndex, long size)
        {
            if (source.IsIn(".", ".."))
                return;

            form.statusMainLabel.Text = $"Queueing {source}";

            QueueItem queue = new QueueItem();
            queue.Download = false;
            queue.SourceIsdir = IsDir;
            queue.SourcePath = source;
            queue.ImageIndex = ImageIndex;
            queue.Name = name;
            queue.DestinationPath = Path.Combine(destination, name);
            queue.Status = QueueStatus.Queue;
            queue.Size = size;
            queue.CancelTokenSource = new CancellationTokenSource();
            InsertOnQueue(queue);
            
            if (IsDir)
            {
                DirectoryInfo DirInfo = new DirectoryInfo(source);
                foreach (DirectoryInfo dir in DirInfo.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    QueueUploadItem(true, dir.FullName, queue.DestinationPath, dir.Name, 
                        ShellImageList.GetFileImageIndex("Folder", FileAttributes.Directory), 0);
                }

                foreach (FileInfo file in DirInfo.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.Hidden) != 0)
                        continue;

                    QueueUploadItem(false, file.FullName, queue.DestinationPath, file.Name, 
                        ShellImageList.GetFileImageIndex(file.Name, FileAttributes.Archive), file.Length);
                }
            }
        }

        //*************************************************************************************************************
        public void QueueDownloadItem(bool IsDir, string source, string destination, string name, int ImageIndex, long size)
        {
            if (source.IsIn(".", ".."))
                return;

            form.statusMainLabel.Text = $"Queueing {source}";

            Cursor.Current = Cursors.WaitCursor;
            QueueItem queue = new QueueItem();
            queue.Download = true;
            queue.SourceIsdir = IsDir;
            queue.SourcePath = source;
            queue.ImageIndex = ImageIndex;
            queue.Name = name;
            queue.DestinationPath = Path.Combine(destination, name);
            queue.Status = QueueStatus.Queue;
            queue.Size = size;
            queue.CancelTokenSource = new CancellationTokenSource();
            InsertOnQueue(queue);

            if (IsDir)
            {
                var task = Task.Run(async () => await remoteIO.ListDirectory(source));
                var result = task.Result;
                if (result.Success)
                {
                    foreach (XwRemoteIOItem item in result.Items)
                    {
                        int image = ShellImageList.GetFileImageIndex(item.Name, 
                            (item.IsDirectory) ? FileAttributes.Directory : FileAttributes.Archive);
                        QueueDownloadItem(item.IsDirectory, item.FullName, 
                            queue.DestinationPath, item.Name, image, item.Size);
                    }
                }
                else
                    form.Log(result.Message, Color.Red);
            }
        }

        //*************************************************************************************************************
        public void InsertOnQueue(QueueItem queue)
        {
            if (queue.SourcePath == "." ||
                queue.SourcePath == "..")
                return;

            queue.TransferID = Guid.NewGuid().ToString();

            Image image = ShellImageList.GetIcon(queue.ImageIndex, true).ToBitmap();
            ListViewItem listitem = InsertItem();
            SetSubItemImage(listitem, 0, "In queue", Resources.bullet_arrow_up);
            SetSubItemImage(listitem, 1, (queue.Download) ? queue.DestinationPath : queue.SourcePath, image);
            SetSubItemImage(listitem, 2, string.Empty, (queue.Download) ? Resources.download : Resources.upload);
            SetSubItemImage(listitem, 3, (queue.Download) ? queue.SourcePath : queue.DestinationPath, image);
            SetSubItemText(listitem, 4, XwMaxLib.IO.Drive.GetFileSize(queue.Size));
            listitem.Tag = queue;
            listitem.Name = queue.TransferID;
            form.TotalQueueText.Text = string.Format("{0} Items in queue", Items.Count);
        }

        //*************************************************************************************************************
        public async void StartQueue(bool force = false)
        {
            form.statusMainLabel.Text = "";

            if (QueueRunning && !force)
                return;
            
            QueueRunning = true;
            await ProcessQueue();
        }

        //*************************************************************************************************************
        public async Task ProcessQueue()
        {
            while (Items.Count > 0)
            {
                Update();
                form.TotalQueueText.Text = string.Format("{0} Items in queue", Items.Count);
                QueueItem item = new QueueItem();
                bool refreshLists = true;
                bool alldone = true;
                foreach (ListViewItem listitem in Items)
                {
                    if (((QueueItem)listitem.Tag).Status == QueueStatus.Queue)
                    {
                        SetSubItemImage(listitem, 0, "Processing...", Resources.play);
                        item = (QueueItem)listitem.Tag;
                        listitem.EnsureVisible();
                        refreshLists = false;
                        alldone = false;
                        break;
                    }
                }

                if (alldone)
                {
                    QueueRunning = false;
                    return;
                }

                if (refreshLists)
                {
                    QueueRunning = false;
                    RefreshLists();
                }

                if (item.TransferID != null)
                {
                    try
                    {
                        if (item.Download) //Download
                        {
                            if (item.SourceIsdir) //Create Directory
                            {
                                Directory.CreateDirectory(item.DestinationPath);
                                DeleteByKey(item.TransferID);
                            }
                            else //Get File
                            {
                                XwFileAction action = XwFileAction.Overwrite;
                                if (File.Exists(item.DestinationPath))
                                {
                                    if (DoToAllFiles == XwFileAction.Ask)
                                    {
                                        Exists exists = new Exists();

                                        exists.SourceFileName.Text = item.SourcePath;
                                        exists.DestinationFileName.Text = item.DestinationPath;

                                        exists.SourceIcon.Image = ShellImageList.GetIcon(item.ImageIndex, false).ToBitmap();
                                        exists.DestinationIcon.Image = ShellImageList.GetIcon(item.ImageIndex, false).ToBitmap();

                                        exists.SourceFileSize.Text = string.Format("{0:#,#}", item.Size);
                                        FileInfo fi = new FileInfo(item.DestinationPath);
                                        exists.DestinationFileSize.Text = string.Format("{0:#,#}", fi.Length);
                                        exists.DestinationFileDate.Text = File.GetLastWriteTime(item.DestinationPath).ToStringUI();

                                        if (exists.ShowDialog(this) == DialogResult.Cancel)
                                        {
                                            DeleteAll();
                                            return;
                                        }
                                        else
                                        {
                                            if (exists.DoToAllFiles != XwFileAction.Ask)
                                            {
                                                DoToAllFiles = exists.DoToAllFiles;
                                                action = exists.DoToAllFiles;
                                            }
                                            else
                                                action = exists.DoToFile;
                                        }
                                    }
                                    else
                                        action = DoToAllFiles;
                                }

                                if (action == XwFileAction.Skip)
                                {
                                    DeleteByKey(item.TransferID);
                                }
                                else
                                {
                                    int index = Items.IndexOfKey(item.TransferID);
                                    if (index != -1)
                                    {
                                        item.Transferred = 0;
                                        item.Status = QueueStatus.Progress;
                                        Items.IndexOfKey(item.TransferID);
                                        Items[index].Tag = item;
                                    }
                                    var result = await remoteIO.DownloadFile(item.DestinationPath, item.SourcePath, 
                                        item.TransferID, action == XwFileAction.Resume, item.CancelTokenSource.Token);
                                    if (result.Success)
                                    {
                                        DeleteByKey(item.TransferID);
                                        form.TotalQueueText.Text = string.Format("{0} Items in queue", Items.Count);
                                    }
                                    else
                                    {
                                        form.Log(result.Message, Color.Red);
                                        int idx = Items.IndexOfKey(item.TransferID);
                                        if (idx >= 0)
                                        {
                                            ListViewItem litem = Items[index];
                                            QueueItem queue = (QueueItem)litem.Tag;
                                            queue.Status = QueueStatus.Error;
                                            litem.Tag = queue;
                                            litem.ToolTipText = result.Message;
                                            SetSubItemImage(litem, 0, "Error", Resources.error);
                                        }
                                    }
                                }
                            }
                        }
                        else //Upload
                        {
                            if (item.SourceIsdir) // Create Directory
                            {
                                if (!SkipExistsValidation.Contains(item.DestinationPath))
                                {
                                    if (!await remoteIO.Exists(item.DestinationPath))
                                    {
                                        var result = await remoteIO.CreateDirectory(item.DestinationPath);
                                        if (!result.Success)
                                            form.Log(result.Message, Color.Red);
                                        SkipExistsValidation.Add(item.DestinationPath);
                                    }
                                }
                                DeleteByKey(item.TransferID);
                            }
                            else //Put File
                            {
                                XwFileAction action = XwFileAction.Overwrite;
                                string dir = item.DestinationPath.Substring(0, item.DestinationPath.LastIndexOf('/'));
                                if (dir == "")
                                    dir = "/";

                                if (await remoteIO.Exists(item.DestinationPath))
                                {
                                    if (DoToAllFiles == XwFileAction.Ask)
                                    {
                                        Exists exists = new Exists();

                                        exists.SourceFileName.Text = item.SourcePath;
                                        exists.DestinationFileName.Text = item.DestinationPath;

                                        exists.SourceIcon.Image = ShellImageList.GetIcon(item.ImageIndex, false).ToBitmap();
                                        exists.DestinationIcon.Image = ShellImageList.GetIcon(item.ImageIndex, false).ToBitmap();

                                        exists.SourceFileSize.Text = string.Format("{0:#,#}", item.Size);
                                        var result = await remoteIO.GetFileSize(item.DestinationPath);
                                        if (!result.Success)
                                            form.Log(result.Message, Color.Red);
                                        exists.DestinationFileSize.Text = string.Format("{0:#,#}", result.Size);

                                        exists.SourceFileDate.Text = File.GetLastWriteTime(item.SourcePath).ToStringUI();
                                        result = await remoteIO.GetDateModified(item.DestinationPath);
                                        if (!result.Success)
                                            form.Log(result.Message, Color.Red);
                                        exists.DestinationFileDate.Text = result.Modified.ToStringUI();

                                        if (exists.ShowDialog(this) == DialogResult.Cancel)
                                        {
                                            DeleteAll();
                                            QueueRunning = false;
                                            DoToAllFiles = XwFileAction.Ask;
                                            RefreshLists();
                                            return;
                                        }
                                        else
                                        {
                                            if (exists.DoToAllFiles != XwFileAction.Ask)
                                            {
                                                DoToAllFiles = exists.DoToAllFiles;
                                                action = exists.DoToAllFiles;
                                            }
                                            else
                                                action = exists.DoToFile;
                                        }
                                    }
                                    else
                                        action = DoToAllFiles;
                                }

                                if (action == XwFileAction.Skip)
                                {
                                    DeleteByKey(item.TransferID);
                                }
                                else
                                {
                                    int index = Items.IndexOfKey(item.TransferID);
                                    if (index != -1)
                                    {
                                        item.Transferred = 0;
                                        item.Status = QueueStatus.Progress;
                                        Items.IndexOfKey(item.TransferID);
                                        Items[index].Tag = item;
                                    }

                                    var result = await remoteIO.UploadFile(item.SourcePath, item.DestinationPath, 
                                        item.TransferID, action == XwFileAction.Resume, item.CancelTokenSource.Token);
                                    if (result.Success)
                                    {
                                        DeleteByKey(item.TransferID);
                                        form.TotalQueueText.Text = string.Format("{0} Items in queue", Items.Count);
                                    }
                                    else
                                    {
                                        form.Log(result.Message, Color.Red);
                                        int idx = Items.IndexOfKey(item.TransferID);
                                        if (idx >= 0)
                                        {
                                            ListViewItem litem = Items[index];
                                            QueueItem queue = (QueueItem)litem.Tag;
                                            queue.Status = QueueStatus.Error;
                                            litem.Tag = queue;
                                            litem.ToolTipText = result.Message;
                                            SetSubItemImage(litem, 0, "Error", Resources.error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("InnerException"))
                            form.Log(ex.InnerException.Message, Color.Red);
                        else
                            form.Log(ex.Message, Color.Red);
                        int index = Items.IndexOfKey(item.TransferID);
                        if (index != -1)
                        {
                            QueueItem qi = (QueueItem)Items[index].Tag;
                            qi.Status = QueueStatus.Error;
                            Items[index].Tag = qi;
                            SetSubItemImage(Items[index], 0, "Error", Resources.exclamation);
                            Items[index].ToolTipText = ex.Message;
                        }
                    }
                }
            }

            SkipExistsValidation.Clear();
            QueueRunning = false;
            DoToAllFiles = XwFileAction.Ask;
            RefreshLists();
        }

        //*************************************************************************************************************
        private async void RefreshLists()
        {
            await form.RemoteList.LoadList(form.RemoteList.CurrentDirectory);
            form.LocalList.RealLoadList(form.LocalList.CurrentDirectory, true);
        }

        //*************************************************************************************************************
        private void this_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenu.Items.Clear();

                ListViewHitTestInfo info = HitTest(new Point(e.X, e.Y));

                if (info.Item != null)
                {
                    if (SelectedItems.Count >= 1)
                    {
                        QueueStatus status = ((QueueItem)info.Item.Tag).Status;

                        if (status == QueueStatus.Error)
                        {
                            ToolStripMenuItem menuitemError = new ToolStripMenuItem("Error information", 
                                Resources.error, Menu_Error_Click, "Error");
                            contextMenu.Items.Add(menuitemError);
                        
                            ToolStripMenuItem menuitemRetry = new ToolStripMenuItem("Retry", 
                                Resources.retry, Menu_Start_Click, "Retry");
                            contextMenu.Items.Add(menuitemRetry);
                        }

                        if (status == QueueStatus.Stopped || status == QueueStatus.Queue)
                        {
                            ToolStripMenuItem menuitemRetry = new ToolStripMenuItem("Start", 
                                Resources.play, Menu_Start_Click, "Start");
                            contextMenu.Items.Add(menuitemRetry);
                        }

                        /*
                        if (status == QueueStatus.Progress)
                        {
                            ToolStripMenuItem menuitemPause = new ToolStripMenuItem("Cancel", Resources.stop, Menu_Stop_Click, "Pause");
                            contextMenu.Items.Add(menuitemPause);
                        }
                        */
                    }

                    if (SelectedItems.Count > 0)
                    {
                        ToolStripMenuItem menuitemRemove = new ToolStripMenuItem("Remove", 
                            Resources.delete, Menu_Remove_Click, "Remove");
                        contextMenu.Items.Add(menuitemRemove);
                    }
                }
            }
        }

        //*************************************************************************************************************
        private void Menu_Error_Click(object sender, EventArgs e)
        {
            ListViewItem item = SelectedItems[0];

            if (item != null)
                MessageBox.Show(item.ToolTipText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //*************************************************************************************************************
        private void Menu_Start_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in SelectedItems)
            {
                QueueItem item = (QueueItem)i.Tag;
                item.Status = QueueStatus.Queue;
                i.Tag = item;
                i.ToolTipText = string.Empty;
                SetSubItemImage(i, 0, string.Empty, null);
            }
        StartQueue(true);
        }

        //*************************************************************************************************************
        private void Menu_Remove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete the selected items?", "", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in SelectedItems)
                {
                    QueueItem qi = (QueueItem)item.Tag;
                    qi.CancelTokenSource.Cancel();
                    DeleteByKey(item.Name);

                    foreach (ListViewItem i in Items)
                    {
                        QueueItem qid = (QueueItem)i.Tag;
                        if (qid.DestinationPath.StartsWith(qi.DestinationPath))
                        {
                            DeleteByKey(i.Name);
                        }
                    }
                }
                form.TotalQueueText.Text = string.Format("{0} Items in queue", Items.Count);
            }
        }

        //*************************************************************************************************************
        private void Menu_Stop_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in SelectedItems)
            {
                QueueItem qi = (QueueItem)item.Tag;
                //ftp.Cancel(qi.TransferID);
                DeleteByKey(item.Name);
            }
        }

        //*************************************************************************************************************
        private void this_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedItems.Count == 1)
            {
                QueueStatus status = ((QueueItem)SelectedItems[0].Tag).Status;
                if (status == QueueStatus.Error)
                    MessageBox.Show(SelectedItems[0].ToolTipText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //*************************************************************************************************************
        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Menu_Remove_Click(sender, e);
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
