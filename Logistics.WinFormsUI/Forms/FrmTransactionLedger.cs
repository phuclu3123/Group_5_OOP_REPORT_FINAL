using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmTransactionLedger : Form
    {
        public FrmTransactionLedger()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<TransactionRow> rows = new List<TransactionRow>();
            foreach (Transaction transaction in DependencyContainer.GetTransactionService().GetAllTransactions())
            {
                rows.Add(new TransactionRow
                {
                    TransactionId = transaction.TransactionID,
                    OrderId = transaction.OrderID,
                    Amount = transaction.Amount,
                    Method = EnumTranslator.TranslatePaymentMethod(transaction.PaymentMethod),
                    Status = EnumTranslator.TranslateTransactionStatus(transaction.Status),
                    CreatedAt = transaction.Timestamp.ToString("dd/MM/yyyy HH:mm")
                });
            }

            dgvTransactions.DataSource = rows;
            if (!dgvTransactions.Columns.Contains("btnPrint"))
            {
                DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn { Name = "btnPrint", Text = "In phiếu", UseColumnTextForButtonValue = true };
                dgvTransactions.Columns.Add(btnPrint);
            }
            if (dgvTransactions.Columns["TransactionId"] != null) dgvTransactions.Columns["TransactionId"]!.HeaderText = "Mã giao dịch";
            if (dgvTransactions.Columns["OrderId"] != null) dgvTransactions.Columns["OrderId"]!.HeaderText = "Mã vận đơn";
            if (dgvTransactions.Columns["Amount"] != null) dgvTransactions.Columns["Amount"]!.HeaderText = "Số tiền";
            if (dgvTransactions.Columns["Method"] != null) dgvTransactions.Columns["Method"]!.HeaderText = "Hình thức";
            if (dgvTransactions.Columns["Status"] != null) dgvTransactions.Columns["Status"]!.HeaderText = "Trạng thái";
            if (dgvTransactions.Columns["CreatedAt"] != null) dgvTransactions.Columns["CreatedAt"]!.HeaderText = "Ngày ghi nhận";
        }

        private void DgvTransactions_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dgvTransactions.Columns[e.ColumnIndex].Name != "btnPrint")
            {
                return;
            }

            string id = dgvTransactions.Rows[e.RowIndex].Cells["TransactionId"].Value?.ToString() ?? string.Empty;
            string receipt = DependencyContainer.GetTransactionService().GenerateReceipt(id);
            DocumentPrintHelper.PrintText("Phiếu thu " + id, receipt, this);
        }

        private sealed class TransactionRow
        {
            public string TransactionId { get; set; } = string.Empty;
            public string OrderId { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string Method { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string CreatedAt { get; set; } = string.Empty;
        }
    }
}
