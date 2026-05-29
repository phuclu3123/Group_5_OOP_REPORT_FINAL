using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class DocumentPrintHelper
    {
        public static void PrintText(string title, string content, IWin32Window owner)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show(owner, "Chua co noi dung de in.", "In chung tu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string remainingText = NormalizeDocument(title, content);
            using PrintDocument document = new PrintDocument();
            document.DocumentName = title;
            document.PrintPage += (sender, e) =>
            {
                if (e.Graphics == null)
                {
                    e.HasMorePages = false;
                    return;
                }

                using Font font = new Font("Consolas", 10F);
                RectangleF bounds = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
                e.Graphics.MeasureString(remainingText, font, bounds.Size, StringFormat.GenericTypographic, out int charsFitted, out int linesFilled);
                e.Graphics.DrawString(remainingText.Substring(0, charsFitted), font, Brushes.Black, bounds, StringFormat.GenericTypographic);
                remainingText = remainingText.Substring(charsFitted);
                e.HasMorePages = remainingText.Length > 0;
            };

            using PrintDialog dialog = new PrintDialog();
            dialog.Document = document;
            dialog.UseEXDialog = true;
            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                document.Print();
            }
        }

        private static string NormalizeDocument(string title, string content)
        {
            if (content.TrimStart().StartsWith("CONG TY TNHH LOGISTICS SYSTEM", StringComparison.OrdinalIgnoreCase))
            {
                return content;
            }

            string line = new string('=', 86);
            return "CONG TY TNHH LOGISTICS SYSTEM".PadRight(52) + title + "\n" +
                   "Dia chi: 01 Nguyen Van Cu, Quan 5, TP.HCM\n" +
                   "Dien thoai: 028 0000 0000 | Email: support@logistics.local\n" +
                   "Ngay in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n" +
                   line + "\n" +
                   content.Trim() + "\n" +
                   line + "\n" +
                   "Nguoi lap".PadRight(28) + "Nguoi nhan".PadRight(28) + "Ke toan\n\n" +
                   "................".PadRight(28) + "................".PadRight(28) + "................";
        }
    }
}
