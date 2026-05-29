using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class UIHelper
    {
        /// <summary>
        /// Binds a list to a DataGridView using BindingList for smooth updates.
        /// </summary>
        public static void BindGrid<T>(DataGridView grid, List<T> data)
        {
            BindingList<T> bindingList = new BindingList<T>(data);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = bindingList;
            grid.DataSource = bindingSource;
        }

        public static void AutoResizeColumns(DataGridView grid)
        {
            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public static void SetAlternatingRowColors(DataGridView grid, System.Drawing.Color color)
        {
            grid.AlternatingRowsDefaultCellStyle.BackColor = color;
        }

        public static void DisableColumnSorting(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
