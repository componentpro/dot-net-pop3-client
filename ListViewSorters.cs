using System;
using System.Collections;
using System.Windows.Forms;

namespace Pop3Samples
{
    /// <summary>
    /// Date time sorter.
    /// </summary>
    public class ListViewItemDateSorter : IComparer
    {
        private readonly int _columnToSort;
        private readonly SortOrder _sortOrder;

        public ListViewItemDateSorter(int columnToSort, SortOrder sortOrder)
        {
            _columnToSort = columnToSort;
            _sortOrder = sortOrder;
        }

        public int Compare(object xobject, object yobject)
        {
            int result;

            ListViewItem x;
            ListViewItem y;

            x = (ListViewItem)xobject;
            y = (ListViewItem)yobject;

            if (_columnToSort == 0)
            {
                result = DateTime.Compare(Convert.ToDateTime(x.Text), Convert.ToDateTime(y.Text));
            }
            else
            {
                result = DateTime.Compare(Convert.ToDateTime(x.SubItems[_columnToSort].Text), Convert.ToDateTime(y.SubItems[_columnToSort].Text));
            }

            if (_sortOrder == SortOrder.Descending)
            {
                result = -result;
            }

            return result;
        }
    }

    /// <summary>
    /// Name sorter.
    /// </summary>
    public class ListViewItemNameSorter : IComparer
    {
        private readonly int _columnToSort;
        private readonly SortOrder _sortOrder;

        public ListViewItemNameSorter(int columnToSort, SortOrder sortOrder)
        {
            _columnToSort = columnToSort;
            _sortOrder = sortOrder;
        }

        public int Compare(object xobject, object yobject)
        {
            ListViewItem x = (ListViewItem)xobject;
            ListViewItem y = (ListViewItem)yobject;
            string xname, yname;

            if (_columnToSort == 0)
            {
                xname = x.Text;
                yname = y.Text;
            }
            else
            {
                xname = x.SubItems[_columnToSort].Text;
                yname = y.SubItems[_columnToSort].Text;
            }

            int result = xname.CompareTo(yname);

            if (_sortOrder == SortOrder.Descending)
                result = -result;

            return result;
        }
    }

    /// <summary>
    /// Size sorter.
    /// </summary>
    public class ListViewItemSizeSorter : IComparer
    {
        private readonly int _columnToSort;
        private readonly SortOrder _sortOrder;

        public ListViewItemSizeSorter(int columnToSort, SortOrder sortOrder)
        {
            _columnToSort = columnToSort;
            _sortOrder = sortOrder;
        }

        public int Compare(object xobject, object yobject)
        {
            int result;

            ListViewItem x;
            ListViewItem y;

            string xname;
            string yname;

            x = (ListViewItem)xobject;
            y = (ListViewItem)yobject;

            if (_columnToSort == 0)
            {
                xname = x.Text;
                yname = y.Text;
            }
            else
            {
                xname = x.SubItems[_columnToSort].Text;
                yname = y.SubItems[_columnToSort].Text;
            }

            if (xname == "")
            {
                xname = "-1";
            }

            if (yname == "")
            {
                yname = "-1";
            }

            result = Decimal.Compare(Convert.ToDecimal(xname), Convert.ToDecimal(yname));

            if (_sortOrder.Equals(SortOrder.Descending))
            {
                result = -result;
            }

            return result;
        }
    }
}