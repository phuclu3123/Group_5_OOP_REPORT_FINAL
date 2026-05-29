using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class UIStyleHelper
    {
        public static void ApplyGridViewStyle(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.GridColor = Color.FromArgb(239, 241, 243);

            // Header Style
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Quan trọng để tránh lỗi NullRef khi set Height
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(242, 245, 250);
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.FromArgb(64, 64, 64);
            headerStyle.SelectionBackColor = Color.FromArgb(242, 245, 250);
            headerStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 45;

            // Row Style
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = Color.White;
            rowStyle.Font = new Font("Segoe UI", 10F);
            rowStyle.ForeColor = Color.FromArgb(71, 69, 94);
            rowStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            rowStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgv.DefaultCellStyle = rowStyle;

            dgv.RowTemplate.Height = 40;
            
            // Fix performance
            dgv.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
               ?.SetValue(dgv, true, null);
        }
    }
}
