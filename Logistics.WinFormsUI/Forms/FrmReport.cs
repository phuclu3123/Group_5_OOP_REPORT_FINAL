using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmReport : Form
    {
        private readonly IReportService? _reportService;

        public FrmReport()
        {
            InitializeComponent();
        }

        public FrmReport(IReportService reportService) : this()
        {
            _reportService = reportService;

            if (!DesignerHelper.IsInDesignMode(this))
            {
                LoadReportData();
            }
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            LoadReportData();
        }

        private void btnExport_Click(object? sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Chua co du lieu bao cao de xuat.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            dialog.FileName = "bao_cao_quan_tri.csv";
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            StringBuilder content = new StringBuilder();
            content.AppendLine("Chi tieu,Gia tri");
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.IsNewRow) continue;
                string metric = Convert.ToString(row.Cells["Metric"].Value) ?? string.Empty;
                string value = Convert.ToString(row.Cells["Value"].Value) ?? string.Empty;
                content.AppendLine(EscapeCsv(metric) + "," + EscapeCsv(value));
            }

            File.WriteAllText(dialog.FileName, content.ToString(), Encoding.UTF8);
            MessageBox.Show("Da xuat bao cao.", "Bao cao", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadReportData()
        {
            if (_reportService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            DashboardStatisticsDTO stats = _reportService.GetOverallStatistics();
            lblTotalOrdersValue.Text = stats.TotalOrders.ToString("N0");
            lblRevenueValue.Text = stats.TotalRevenue.ToString("N0") + " VND";
            lblSuccessRateValue.Text = stats.SuccessRate.ToString("N1") + "%";
            lblResourcesValue.Text = $"{stats.AvailableVehicles:N0}/{stats.TotalVehicles:N0} xe";

            decimal paidRevenue = CalculatePaidRevenue();
            decimal payroll = CalculatePayroll();
            int openIssues = CountOpenIssues();

            dgvReport.DataSource = new List<ReportRow>
            {
                new ReportRow("Don hang dang cho", stats.PendingOrders),
                new ReportRow("Don hang dang giao", stats.InTransitOrders),
                new ReportRow("Don hang da giao", stats.DeliveredOrders),
                new ReportRow("Don hang da huy", stats.CancelledOrders),
                new ReportRow("Don hang that bai", stats.FailedOrders),
                new ReportRow("Don hang hoan tra", stats.ReturnedOrders),
                new ReportRow("Doanh thu da thu", paidRevenue.ToString("N0") + " VND"),
                new ReportRow("Tong quy luong hien tai", payroll.ToString("N0") + " VND"),
                new ReportRow("Su co dang xu ly", openIssues),
                new ReportRow("Tong nhan su", stats.TotalStaff),
                new ReportRow("Tai xe san sang", stats.AvailableDrivers),
                new ReportRow("Tong khach hang", stats.TotalCustomers),
                new ReportRow("Tong kho bai", stats.TotalWarehouses)
            };

            if (dgvReport.Columns["Metric"] != null) dgvReport.Columns["Metric"]!.HeaderText = "Chi tieu";
            if (dgvReport.Columns["Value"] != null) dgvReport.Columns["Value"]!.HeaderText = "Gia tri";
        }

        private sealed class ReportRow
        {
            public string Metric { get; private set; }
            public string Value { get; private set; }

            public ReportRow(string metric, int value) : this(metric, value.ToString("N0"))
            {
            }

            public ReportRow(string metric, string value)
            {
                Metric = metric;
                Value = value;
            }
        }

        private static decimal CalculatePaidRevenue()
        {
            decimal total = 0;
            List<Transaction> transactions = DependencyContainer.GetTransactionService().GetAllTransactions();
            for (int i = 0; i < transactions.Count; i++)
            {
                if (transactions[i] != null && transactions[i].Status == TransactionStatus.Completed)
                {
                    total += transactions[i].Amount;
                }
            }

            return total;
        }

        private static int CountOpenIssues()
        {
            int total = 0;
            List<ProblemReport> reports = DependencyContainer.GetProblemReportService().GetAllReports();
            for (int i = 0; i < reports.Count; i++)
            {
                if (reports[i] != null && reports[i].ResolutionStatus != ResolutionStatus.Resolved)
                {
                    total++;
                }
            }

            return total;
        }

        private static decimal CalculatePayroll()
        {
            decimal total = 0;
            var staffService = DependencyContainer.GetStaffManagementService();

            foreach (Driver driver in staffService.GetAllDrivers())
            {
                total += driver.CalculateSalary();
            }

            foreach (Dispatcher dispatcher in staffService.GetAllDispatchers())
            {
                total += dispatcher.CalculateSalary();
            }

            foreach (WarehouseStaff staff in staffService.GetAllWarehouseStaff())
            {
                total += staff.CalculateSalary();
            }

            return total;
        }

        private static string EscapeCsv(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }
    }
}
