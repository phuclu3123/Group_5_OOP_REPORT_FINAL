using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Common;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmProblemReport : Form
    {
        private readonly string _orderId;

        public FrmProblemReport(string orderId)
        {
            _orderId = orderId;
            InitializeComponent();
            lblTitle.Text = "Lập biên bản sự cố: " + _orderId;
            cbIssueType.DataSource = Enum.GetValues(typeof(IssueType));
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            DocumentPrintHelper.PrintText("Biên bản sự cố " + _orderId, txtDocument.Text, this);
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show(this, "Vui long nhap mo ta su co.", "Bao cao su co", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IssueType issueType = (IssueType)cbIssueType.SelectedItem!;
            var report = DependencyContainer.GetProblemReportService().CreateReport(_orderId, issueType, txtDescription.Text.Trim());
            txtDocument.Text = DependencyContainer.GetProblemReportService().GenerateProblemReportDocument(report.ReportID);
            MessageBox.Show(this, "Da lap bien ban su co.", "Bao cao su co", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
