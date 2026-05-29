using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmPayment : Form
    {
        private readonly string _orderId;

        public FrmPayment(string orderId, decimal suggestedAmount)
        {
            _orderId = orderId;
            InitializeComponent();
            lblTitle.Text = "Ghi nhận thanh toán: " + _orderId;
            cbMethod.DataSource = Enum.GetValues(typeof(PaymentMethod));
            decimal remaining = DependencyContainer.GetTransactionService().GetRemainingAmount(_orderId);
            if (remaining <= 0 && suggestedAmount > 0)
            {
                remaining = GetFallbackAmountIfOrderHasNoCost(suggestedAmount);
            }

            ApplyRemainingAmount(remaining);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (nudAmount.Value <= 0)
            {
                MessageBox.Show(this, "So tien phai lon hon 0.", "Thanh toan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PaymentMethod method = (PaymentMethod)cbMethod.SelectedItem!;
                Transaction transaction = DependencyContainer.GetTransactionService().CreatePayment(_orderId, nudAmount.Value, method);
                txtReceipt.Text = DependencyContainer.GetTransactionService().GenerateReceipt(transaction.TransactionID);
                decimal remaining = DependencyContainer.GetTransactionService().GetRemainingAmount(_orderId);
                ApplyRemainingAmount(remaining);
                MessageBox.Show(this, "Da ghi nhan thanh toan.", "Thanh toan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Thanh toan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            DocumentPrintHelper.PrintText("Phieu thu " + _orderId, txtReceipt.Text, this);
        }

        private decimal GetFallbackAmountIfOrderHasNoCost(decimal suggestedAmount)
        {
            Order order = DependencyContainer.GetOrderService().GetOrderById(_orderId);
            if (order != null && order.TotalCost <= 0)
            {
                return suggestedAmount;
            }

            return 0m;
        }

        private void ApplyRemainingAmount(decimal remaining)
        {
            bool canPay = remaining > 0;
            lblRemaining.Text = canPay ? "Con lai: " + remaining.ToString("N0") + " VND" : "Don hang da thanh toan du.";
            nudAmount.Enabled = canPay;
            cbMethod.Enabled = canPay;
            btnSave.Enabled = canPay;
            nudAmount.Maximum = canPay ? remaining : 1;
            nudAmount.Value = canPay ? remaining : 0;
        }
    }
}
