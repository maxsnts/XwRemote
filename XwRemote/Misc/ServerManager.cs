using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Misc;
using XwRemote.Settings;

namespace XwRemote
{
    public partial class ServerManager : Form
    {
        private int SelectedTab = 1;
        public Server ConnectToThisServer = null;
        private Main mainForm = null;
        private string tmpSelectedNode = null;
        private string tmpSelectedLabel;
        private Point tmpScrollPost;

        //****************************************************************************************************
        public ServerManager(Main main)
        {
            InitializeComponent();
            mainForm = main;
            treeServers.KeyDown += new System.Windows.Forms.KeyEventHandler(tree_KeyDown);
        }

        //****************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            treeServers.ImageList = Main.myImageList;
            SelectedTab = Main.config.GetValue("ServerManagerSelectedTab").ToIntOrDefault(1);
            Width = Main.config.GetValue("ServerManagerFormSizeW").ToIntOrDefault(600);
            Height = Main.config.GetValue("ServerManagerFormSizeH").ToIntOrDefault(600);
            CenterToParent();

            if (Main.config.GetValue("UI_SEARCH_SAVE").ToBoolOrDefault(false))
                textSearch.Text = Main.config.GetValue("UI_SEARCH_TEXT").ToString();
            
            LoadList();
            
            if (!Main.config.ShowExperimentalFeatures())
            {
                
            }
        }

        //****************************************************************************************************
        private void LoadList()
        {
            treeServers.BeginUpdate();
            tmpScrollPost = treeServers.GetTreeViewScrollPos();
            tmpSelectedNode = treeServers.SelectedNode?.Name;
            tmpSelectedLabel = treeServers.SelectedNode?.Text;
            treeServers.ContextMenuStrip = null;
            treeServers.Nodes.Clear();
            
            switch (SelectedTab)
            {
                case 1:
                    tabStrip1.SelectedTab = FilterGrouped;
                    LoadGrouped();
                    break;
                case 2:
                    tabStrip1.SelectedTab = FilterOrdered;
                    LoadOrdered(false);
                    break;
                case 3:
                    tabStrip1.SelectedTab = FilterFavorites;
                    LoadOrdered(true);
                    break;
            }
                        
            treeServers.EndUpdate();
            
            if (!textSearch.Focused)
            {
                SelectNode();
                treeServers.Focus();
                tmpSelectedNode = null;
            }
            Main.config.SetValue("ServerManagerSelectedTab", SelectedTab.ToString());
        }

        //****************************************************************************************************
        private void LoadGrouped()
        {
            foreach (Group group in Main.config.groups)
            {
                TreeNode node = treeServers.Nodes.Add($"G{group.ID}", group.Name, "folder", "folder");
                node.Tag = group;
            }
            
            //Load Grouped Servers
            foreach (Server server in Main.servers)
            {
                if (textSearch.Text.Length > 0)
                {
                    string search = $"{server.Name} {server.Host} {server.Port} {server.Notes} {server.Username}";
                    if (search.IndexOf(textSearch.Text, StringComparison.InvariantCultureIgnoreCase) == -1)
                        continue;
                }

                Color color = (mainForm.IsServerOpen(server)) ? Color.Green : SystemColors.ControlText;
                
                string image = server.GetIcon();
                TreeNode[] found = treeServers.Nodes.Find($"G{server.GroupID}", false);
                if (found.Length == 0)
                {
                    TreeNode node = treeServers.Nodes.Add($"S{server.ID}",
                        $"{server.Name} ({server.Host})",
                        image, image);
                    node.Tag = server;
                    node.ForeColor = color;
                }
                else
                {
                    found[0].Nodes.Add($"S{server.ID}",
                            $"{server.Name} ({server.Host})",
                            image, image).Tag = server;
                    found[0].ForeColor = color;
                }
            }

            //Expand groups
            foreach (TreeNode node in treeServers.Nodes)
            {
                if (node?.Tag is Group)
                {
                    if (((Group)node.Tag).Expanded)
                        node.Expand();
                }
            }
        }

        //****************************************************************************************************
        private void LoadOrdered(bool favoritesOnly)
        {
            foreach (Server server in Main.servers)
            {
                if (textSearch.Text.Length > 0)
                {
                    string search = $"{server.Name} {server.Host} {server.Port} {server.Notes} {server.Username}";
                    if (search.IndexOf(textSearch.Text, StringComparison.InvariantCultureIgnoreCase) == -1)
                        continue;
                }

                Color color = (mainForm.IsServerOpen(server)) ? Color.Green : SystemColors.ControlText;

                TreeNode[] node = treeServers.Nodes.Find($"G{server.GroupID}", false);
                if (node.Length == 0)
                {
                    if (favoritesOnly && !server.IsFavorite)
                        continue;

                    string image = server.GetIcon();
                    TreeNode srvNode = treeServers.Nodes.Add($"S{server.ID}",
                           string.Format("{0} ({1})", server.Name, server.Host),
                           image, image);
                    srvNode.Tag = server;
                    srvNode.ForeColor = color;
                }
            }
        }

        //****************************************************************************************************
        private void SelectNode()
        {
            if (tmpSelectedNode == null)
            {
                if (treeServers.Nodes.Count > 0)
                    treeServers.SelectedNode = treeServers.Nodes[0];
                treeServers.SelectedNode?.EnsureVisible();
                return;
            }

            TreeNode[] found = treeServers.Nodes.Find(tmpSelectedNode, true);
            if (found.Length > 0)
            {
                treeServers.SelectedNode = found[0];
                treeServers.SelectedNode.Parent?.Expand();
                if (tmpSelectedLabel == treeServers.SelectedNode?.Text)
                    treeServers.SetTreeViewScrollPos(tmpScrollPost);
                else
                    treeServers.SelectedNode?.EnsureVisible();
            }
        }

        //****************************************************************************************************
        private void EditServer_Click(object sender, EventArgs e)
        {
            Edit();
        }

        //****************************************************************************************************
        private void RenameGroup_Click(object sender, EventArgs e)
        {
            Edit();
        }

        //****************************************************************************************************
        private void Edit()
        {
            if (treeServers.SelectedNode?.Tag is Group)
            {
                treeServers.LabelEdit = true;
                if (treeServers.SelectedNode.Parent == null)
                    treeServers.SelectedNode.BeginEdit();
                else
                    treeServers.SelectedNode.Parent.BeginEdit();
            }
            else
            {
                EditServerById(((Server)treeServers.SelectedNode?.Tag), false);
            }
        }

        //****************************************************************************************************
        private void EditServerById(Server server, bool copy)
        {
            server.Edit();
            LoadList();
        }

        //****************************************************************************************************
        private void Connect_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Server)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                ConnectToThisServer = ((Server)treeServers.SelectedNode.Tag);
                Close();
            }
        }

        //****************************************************************************************************
        private void treeServers_DoubleClick(object sender, EventArgs e)
        {
            Connect_Click(sender, e);
        }

        //****************************************************************************************************
        private void FilterGrouped_Click(object sender, EventArgs e)
        {
            SelectedTab = 1;
            LoadList();
        }

        //****************************************************************************************************
        private void FilterOrdered_Click(object sender, EventArgs e)
        {
            SelectedTab = 2;
            LoadList();
        }

        //****************************************************************************************************
        private void FilterFavorites_Click(object sender, EventArgs e)
        {
            SelectedTab = 3;
            LoadList();
        }

        //****************************************************************************************************
        private int GetSelectedGroup()
        {
            TreeNode selected = treeServers.SelectedNode;
            if (selected == null)
                return 0;

            if (selected.Tag is Group)
                return ((Group)selected.Tag).ID;

            if (selected.Tag is Server)
                return ((Server)selected.Tag).GroupID;

            return 0;
        }
        
        //****************************************************************************************************
        private void NewFTP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.FTP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewSFTP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.SFTP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewS3_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.AWSS3);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewAzureFile_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.AZUREFILE);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewRDP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.RDP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewVNC_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.VNC);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void newSSH_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.SSH);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void newSQL_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.MYSQL);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void newIE_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.IE);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void DeleteServer_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Server)
            {
                Server srv = (Server)treeServers.SelectedNode.Tag;
                if (MessageBox.Show($"Are you sure you want to delete the server \"{srv.Name}\"?", "Delete?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    Main.config.DeleteServer(srv);
                    LoadList();
                }
            }
        }

        //****************************************************************************************************
        private void ServerManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.config.SetValue("ServerManagerFormSizeW", Size.Width.ToString());
            Main.config.SetValue("ServerManagerFormSizeH", Size.Height.ToString());
        }

        //****************************************************************************************************
        private void treeServers_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Group)
                Main.config.CollapseGroup((Group)e.Node.Tag);
        }

        //****************************************************************************************************
        private void treeServers_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Group)
                Main.config.ExpandGroup((Group)e.Node.Tag);
        }

        //****************************************************************************************************
        private void AddGroup_Click(object sender, EventArgs e)
        {
            treeServers.SelectedNode = null;
            tmpSelectedNode = null;
            treeServers.LabelEdit = true;
            treeServers.Nodes.Add("New Group").BeginEdit();
        }

        //****************************************************************************************************
        private void DeleteGroup_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Group)
            {
                if (MessageBox.Show("Delete Group?\n\nServers will remain but be ungrouped'", "Group", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                Main.config.DeleteGroup((Group)treeServers.SelectedNode.Tag);
                LoadList();
            }
        }

        //****************************************************************************************************
        private void treeServers_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null) //cancel
            {
                if (e.Node.Name == "")
                    e.Node.Remove();
                return;
            }
            treeServers.LabelEdit = false;

            if (e.Node?.Tag is Group)
            {
                ((Group)e.Node?.Tag).Name = e.Label;
                Main.config.SaveGroup((Group)e.Node?.Tag);
            }
            else
            {
                Group group = new Group();
                group.Name = e.Label;
                group.Expanded = false;
                Main.config.SaveGroup(group);
            }
            e.CancelEdit = true;
            LoadList();
        }

        //****************************************************************************************************
        private void treeServers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                treeServers.SelectedNode = null;
                TreeViewHitTestInfo info = treeServers.HitTest(new Point(e.X, e.Y));

                if (info.Location == TreeViewHitTestLocations.Image ||
                   info.Location == TreeViewHitTestLocations.Label)
                {
                    bool isServer = (info.Node?.Tag is Server);
                    bool isGroup = (info.Node?.Tag is Group);

                    if (isServer)
                    {
                        treeServers.ContextMenuStrip = contextServer;
                    }

                    if (isGroup)
                    {
                        treeServers.ContextMenuStrip = contextGroup;
                        contextGroup.Items[1].Enabled = true; //rename
                        contextGroup.Items[2].Enabled = true; //delete
                    }
                }
                else
                {
                    if (tabStrip1.SelectedTab == FilterGrouped)
                    {
                        treeServers.ContextMenuStrip = contextGroup;
                        contextGroup.Items[1].Enabled = false; //rename
                        contextGroup.Items[2].Enabled = false; //delete
                    }
                }
            }
        }

        //****************************************************************************************************
        private void treeServers_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeViewHitTestInfo info = treeServers.HitTest(new Point(e.X, e.Y));
                if (info.Location == TreeViewHitTestLocations.Image ||
                    info.Location == TreeViewHitTestLocations.Label)
                {
                    treeServers.SelectedNode = info.Node;
                }
            }
        }

        //****************************************************************************************************
        private void treeServers_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;

            tmpSelectedNode = null;

            if (tabStrip1.SelectedTab != FilterGrouped)
                return;
            
            if (((TreeNode)e.Item)?.Tag is Server)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        //****************************************************************************************************
        private void treeServers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //****************************************************************************************************
        private void treeServers_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode dropTarget = ((TreeView)sender).GetNodeAt(pt);

                if (dropTarget == null)
                    return;

                if (dropTarget.Parent != null)
                    dropTarget = dropTarget.Parent;

                if (dropTarget?.Tag is Group)
                {
                    TreeNode server = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                    dropTarget.Nodes.Add((TreeNode)server.Clone());
                    dropTarget.Expand();
                    ((Server)server.Tag).GroupID = ((Group)dropTarget.Tag).ID;
                    Main.config.SaveServer((Server)server.Tag);
                    server.Remove();
                }
            }

            if (previousHighlightedNode != null)
                previousHighlightedNode.BackColor = SystemColors.Window;
            previousHighlightedNode = null;
        }

        //****************************************************************************************************
        TreeNode previousHighlightedNode = null;
        private void treeServers_DragOver(object sender, DragEventArgs e)
        {
            Point p = treeServers.PointToClient(new Point(e.X, e.Y));
            TreeNode node = treeServers.GetNodeAt(p.X, p.Y);
            if (node != previousHighlightedNode)
            {
                if (previousHighlightedNode != null)
                    previousHighlightedNode.BackColor = SystemColors.Window;
                node.BackColor = ControlPaint.LightLight(SystemColors.HotTrack);
                previousHighlightedNode = node;
            }
        }

        //****************************************************************************************************
        private void treeServers_DragLeave(object sender, EventArgs e)
        {
            if (previousHighlightedNode != null)
                previousHighlightedNode.BackColor = SystemColors.Window;
            previousHighlightedNode = null;
        }

        //****************************************************************************************************
        private void tree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                Edit();

            if (e.KeyCode == Keys.Delete)
            {
                if (treeServers.SelectedNode?.Tag is Group)
                    DeleteGroup_Click(sender, e);
                else
                    DeleteServer_Click(sender, e);
            }
        }

        //****************************************************************************************************
        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
            {
                Main.config.SetServerFavourite(srv);
                LoadList();
            }
        }

        //****************************************************************************************************
        private void treeServers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;

            if (treeServers.SelectedNode?.Tag is Server)
            {
                EditServer.Enabled = true;
                DeleteServer.Enabled = true;
                ConnectBtn.Enabled = true;
            }
            else
            {
                EditServer.Enabled = false;
                DeleteServer.Enabled = false;
                ConnectBtn.Enabled = false;
            }
        }

        //****************************************************************************************************
        private void MakeCopy(ServerType type, Server srv)
        {
            if (srv != null)
            {
                Server newSrv = srv.Copy(type);
                EditServerById(newSrv, true);
            }
        }

        //****************************************************************************************************
        private void copyAsFTP_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.FTP, srv);
        }

        //****************************************************************************************************
        private void copyAsRDP_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.RDP, srv);
        }

        //****************************************************************************************************
        private void copyAsVNC_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.VNC, srv);
        }

        //****************************************************************************************************
        private void copyAsSSH_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.SSH, srv);
        }

        //****************************************************************************************************
        private void copyAsIE_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.IE, srv);
        }

        //****************************************************************************************************
        private void ServerManager_Activated(object sender, EventArgs e)
        {
            treeServers.Focus();
        }

        //****************************************************************************************************
        private void copyAsSFTP_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.SFTP, srv);
        }

        //****************************************************************************************************
        private void copyAsAWSS3_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.AWSS3, srv);
        }

        //****************************************************************************************************
        private void copyAsAzureFile_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv != null)
                MakeCopy(ServerType.AZUREFILE, srv);
        }

        //****************************************************************************************************
        private void ExportServer_Click(object sender, EventArgs e)
        {
            Server srv = (Server)treeServers.SelectedNode?.Tag;
            if (srv == null)
            {
                MessageBox.Show("Unable to read server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InOut inout = new InOut(false, srv);
            inout.ShowDialog();
        }

        //****************************************************************************************************
        private void ImportServer_Click(object sender, EventArgs e)
        {
            InOut inout = new InOut(true, null);
            inout.ShowDialog();
            LoadList();
        }

        //****************************************************************************************************
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();

            if (Main.config.GetValue("UI_SEARCH_SAVE").ToBoolOrDefault(false))
                Main.config.SetValue("UI_SEARCH_TEXT", textSearch.Text); 
        }
    }
}
