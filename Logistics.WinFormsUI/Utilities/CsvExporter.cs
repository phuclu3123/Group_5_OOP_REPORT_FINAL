using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class CsvExporter
    {
        public static void ExportGrid(DataGridView dgv, string defaultFileName)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Khong co du lieu de xuat.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string? filePath = ExportHelper.ShowSaveDialog(defaultFileName, "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*");
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            try
            {
                StringBuilder sb = new StringBuilder();

                // Headers
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        sb.Append(EscapeCsvField(dgv.Columns[i].HeaderText));
                        if (i < dgv.Columns.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                }
                sb.AppendLine();

                // Rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            object? val = row.Cells[i].Value;
                            sb.Append(EscapeCsvField(val != null ? val.ToString() : string.Empty));
                            if (i < dgv.Columns.Count - 1)
                            {
                                sb.Append(",");
                            }
                        }
                    }
                    sb.AppendLine();
                }

                // Write with UTF-8 BOM so Excel opens it with correct Vietnamese encoding!
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuat du lieu thanh cong!", "Xuat CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Da xay ra loi khi xuat file CSV: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeCsvField(string? field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return string.Empty;
            }

            bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");
            if (mustQuote)
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }
    }
}
