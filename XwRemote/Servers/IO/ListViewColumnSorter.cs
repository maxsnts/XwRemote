using System.Collections;
using System.Windows.Forms;
using System;

public enum ListViewDataSorterType
{
    Text,
    Numeric,
    Date
};

public class ListViewDataSorter : IComparer
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

        switch (type)
        {
            case ListViewDataSorterType.Text:
                {
                    result = String.Compare(itemA.SubItems[column].Text, itemB.SubItems[column].Text);
                }
                break;
            case ListViewDataSorterType.Numeric:
                {
                    decimal x2, y2;
                    if (!Decimal.TryParse(itemA.SubItems[column].Text, out x2))
                        x2 = Decimal.MinValue;
                    if (!Decimal.TryParse(itemB.SubItems[column].Text, out y2))
                        y2 = Decimal.MinValue;
                    result = Decimal.Compare(x2, y2);
                }
                break;
            case ListViewDataSorterType.Date:
                {
                    DateTime x1, y1;
                    if (!DateTime.TryParse(itemA.SubItems[column].Text, out x1))
                        x1 = DateTime.MinValue;
                    if (!DateTime.TryParse(itemB.SubItems[column].Text, out y1))
                        y1 = DateTime.MinValue;
                    result = DateTime.Compare(x1, y1);
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