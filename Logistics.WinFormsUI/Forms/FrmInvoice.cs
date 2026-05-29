using System.Windows.Forms;
using System.IO;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmInvoice : Form
    {
        public FrmInvoice()
        {
            InitializeComponent();
        }

        public FrmInvoice(string trackingNumber) : this()
        {
            txtOrderId.Text = trackingNumber;
            LoadInvoice();
        }

        private void BtnPreview_Click(object? sender, System.EventArgs e)
        {
            LoadInvoice();
        }

        private void BtnExport_Click(object? sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbInvoice.Text))
            {
                MessageBox.Show("Chua co noi dung hoa don de xuat.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.txt)|*.txt";
            dialog.FileName = "invoice_" + SafeFileName(txtOrderId.Text) + ".txt";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, rtbInvoice.Text, System.Text.Encoding.UTF8);
                MessageBox.Show("Da xuat hoa don.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnPrintPdf_Click(object? sender, System.EventArgs e)
        {
            DocumentPrintHelper.PrintText("Hoa don " + txtOrderId.Text.Trim(), rtbInvoice.Text, this);
        }

        private void LoadInvoice()
        {
            string orderId = txtOrderId.Text.Trim();
            if (string.IsNullOrWhiteSpace(orderId))
            {
                MessageBox.Show("Vui long nhap ma don hang.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rtbInvoice.Text = DependencyContainer.GetReportService().GenerateInvoice(orderId);
        }

        private static string SafeFileName(string value)
        {
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(invalid, '_');
            }

            return string.IsNullOrWhiteSpace(value) ? "unknown" : value;
        }
    }
}
