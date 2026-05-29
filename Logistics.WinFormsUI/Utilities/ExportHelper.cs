using System.IO;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    /// <summary>
    /// Helper for exporting data to PDF or Excel formats.
    /// Extend with actual iTextSharp / EPPlus calls.
    /// </summary>
    public static class ExportHelper
    {
        public static void ExportToCsv(string filePath, string csvContent)
        {
            File.WriteAllText(filePath, csvContent, System.Text.Encoding.UTF8);
        }

        public static string? ShowSaveDialog(string defaultFileName, string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = defaultFileName;
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }

        public static string? ShowPdfSaveDialog(string defaultFileName)
        {
            return ShowSaveDialog(defaultFileName, "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*");
        }

        public static string? ShowExcelSaveDialog(string defaultFileName)
        {
            return ShowSaveDialog(defaultFileName, "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*");
        }
    }
}
