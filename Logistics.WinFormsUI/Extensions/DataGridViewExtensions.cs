using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Extensions
{
    public static class DataGridViewExtensions
    {
        public static void StyleModern(this DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(66, 133, 244);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 68);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
        }

        public static T? GetSelectedItem<T>(this DataGridView grid) where T : class
        {
            if (grid.SelectedRows.Count == 0)
            {
                return null;
            }
            return grid.SelectedRows[0].DataBoundItem as T;
        }
    }
}
