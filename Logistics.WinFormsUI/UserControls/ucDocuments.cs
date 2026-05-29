using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucDocuments : UserControl
    {
        public ucDocuments()
        {
            InitializeComponent();
            LoadOrders();
        }

        private static Button CreateActionButton(string text, Color color, Point location)
        {
            return new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = color,
                FlatStyle = FlatStyle.Flat,
                Location = location,
                Size = new Size(text.Length > 14 ? 150 : 120, 34),
                Cursor = Cursors.Hand
            };
        }

        private void LoadOrders()
        {
            List<DocumentOrderRow> rows = new List<DocumentOrderRow>();
            foreach (Order order in DependencyContainer.GetOrderService().GetAllOrders())
            {
                rows.Add(new DocumentOrderRow
                {
                    TrackingNumber = order.TrackingNumber,
                    SenderId = order.SenderID,
                    ReceiverId = order.ReceiverID,
                    TotalWeight = order.TotalWeight,
                    TotalCost = order.TotalCost,
                    Status = EnumTranslator.TranslateOrderStatus(order.CurrentStatus),
                    CreatedDate = order.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                });
            }

            dgvOrders.DataSource = rows;
            SetColumnText("TrackingNumber", "Mã vận đơn");
            SetColumnText("SenderId", "Người gửi");
            SetColumnText("ReceiverId", "Người nhận");
            SetColumnText("TotalWeight", "Khối lượng");
            SetColumnText("TotalCost", "Tổng cước");
            SetColumnText("Status", "Trạng thái");
            SetColumnText("CreatedDate", "Ngày tạo");
        }

        private void SetColumnText(string name, string text)
        {
            if (dgvOrders.Columns[name] != null)
            {
                dgvOrders.Columns[name]!.HeaderText = text;
            }
        }

        private void DgvOrders_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            txtTrackingNumber.Text = dgvOrders.Rows[e.RowIndex].Cells["TrackingNumber"].Value?.ToString() ?? string.Empty;
            LoadInvoicePreview(false);
        }

        private void BtnPreviewInvoice_Click(object? sender, EventArgs e)
        {
            LoadInvoicePreview(true);
        }

        private void BtnPrintInvoice_Click(object? sender, EventArgs e)
        {
            if (LoadInvoicePreview(true))
            {
                DocumentPrintHelper.PrintText("Hoa don " + txtTrackingNumber.Text.Trim(), rtbPreview.Text, this);
            }
        }

        private void BtnOpenInvoice_Click(object? sender, EventArgs e)
        {
            string trackingNumber = txtTrackingNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã vận đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FrmInvoice invoice = new FrmInvoice(trackingNumber);
            invoice.ShowDialog(this);
        }

        private void BtnReceipts_Click(object? sender, EventArgs e)
        {
            using FrmTransactionLedger ledger = new FrmTransactionLedger();
            ledger.ShowDialog(this);
        }

        private void BtnIssues_Click(object? sender, EventArgs e)
        {
            using FrmProblemReportLedger ledger = new FrmProblemReportLedger();
            ledger.ShowDialog(this);
        }

        private bool LoadInvoicePreview(bool showWarning)
        {
            string trackingNumber = txtTrackingNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                if (showWarning)
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã vận đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            string invoice = DependencyContainer.GetReportService().GenerateInvoice(trackingNumber);
            rtbPreview.Text = invoice;
            return !string.IsNullOrWhiteSpace(invoice);
        }

        private sealed class DocumentOrderRow
        {
            public string TrackingNumber { get; set; } = string.Empty;
            public string SenderId { get; set; } = string.Empty;
            public string ReceiverId { get; set; } = string.Empty;
            public double TotalWeight { get; set; }
            public decimal TotalCost { get; set; }
            public string Status { get; set; } = string.Empty;
            public string CreatedDate { get; set; } = string.Empty;
        }
    }
}
