using System;
using System.Collections;
using System.Windows.Forms;
using XwRemote.Servers;

public enum ListViewDataSorterType
{
    Text,
    Numeric,
    Date
};

public class FileListSorter : IComparer
{
    public int column;
    public ListViewDataSorterType type = ListViewDataSorterType.Text;
    public SortOrder Order { get; set; }

    public int Compare(object a, object b)
    {
        if (Order == SortOrder.None)
            return 0;

        int result = 0;
        ListViewItem itemA = (ListViewItem)a;
        ListViewItem itemB = (ListViewItem)b;

        if (itemA.SubItems.Count - 1 < column || itemB.SubItems.Count - 1 < column)
            return 0;

        if (itemA.Text == "." || itemA.Text == ".." || itemB.Text == "." || itemB.Text == "..")
            return 0;
        
        //Folders on top 
        if (Control.ModifierKeys != Keys.Control)
        {
            var itemAdisk = (DiskItem)itemA.Tag;
            var itemBdisk = (DiskItem)itemB.Tag;
            if (itemAdisk.IsDirectory && !itemBdisk.IsDirectory)
                result = -1000000;
            if (!itemAdisk.IsDirectory && itemBdisk.IsDirectory)
                result = 1000000;
        }

        switch (type)
        {
            case ListViewDataSorterType.Text:
            {
                int diff = string.Compare(itemA.SubItems[column].Text, itemB.SubItems[column].Text);
                if (diff != 0)
                    result += diff;
                else
                {
                    result += string.Compare(itemA.Text, itemB.Text);
                    if (Order == SortOrder.Descending)
                        result *= -1;
                }
            }
            break;
            case ListViewDataSorterType.Numeric:
            {
                decimal x2, y2;
                if (!decimal.TryParse(itemA.SubItems[column].Text, out x2))
                    x2 = decimal.MinValue;
                if (!decimal.TryParse(itemB.SubItems[column].Text, out y2))
                    y2 = decimal.MinValue;

                int diff = decimal.Compare(x2, y2);
                if (diff != 0)
                    result += diff;
                else
                {
                    result += string.Compare(itemA.Text, itemB.Text);
                    if (Order == SortOrder.Descending)
                        result *= -1;
                }
            }
            break;
            case ListViewDataSorterType.Date:
            {
                DateTime x1, y1;
                if (!DateTime.TryParse(itemA.SubItems[column].Text, out x1))
                    x1 = DateTime.MinValue;
                if (!DateTime.TryParse(itemB.SubItems[column].Text, out y1))
                    y1 = DateTime.MinValue;
                int diff = DateTime.Compare(x1, y1);
                if (diff != 0)
                    result += diff;
                else
                {
                    result += string.Compare(itemA.Text, itemB.Text);
                    if (Order == SortOrder.Descending)
                        result *= -1;
                }
            }
            break;
            default:
                break;
        }

        if (Order == SortOrder.Descending)
            result *= -1;

        return result;
    }
}