using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XwMaxLib.Objects;

namespace XwRemote
{
    public static class MyExt
    {
        //****************************************************************************************************
        public static void SelectID(this ComboBox combo, int ID)
        {
            foreach (var i in combo.Items)
            {
                ListItem item = (ListItem)i;
                if (item.ID == ID)
                {
                    combo.SelectedItem = i;
                    return;
                }
            }
        }

        //****************************************************************************************************
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;

        public static Point GetTreeViewScrollPos(this TreeView treeView)
        {
            return new Point(
                GetScrollPos(treeView.Handle, SB_HORZ),
                GetScrollPos(treeView.Handle, SB_VERT));
        }

        public static void SetTreeViewScrollPos(this TreeView treeView, Point scrollPosition)
        {
            SetScrollPos(treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos(treeView.Handle, SB_VERT, scrollPosition.Y, true);
        }

    }
}
