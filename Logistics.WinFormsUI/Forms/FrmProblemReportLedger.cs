using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmProblemReportLedger : Form
    {
        public FrmProblemReportLedger()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<ProblemReportRow> rows = new List<ProblemReportRow>();
            foreach (ProblemReport report in DependencyContainer.GetProblemReportService().GetAllReports())
            {
                rows.Add(new ProblemReportRow
                {
                    ReportId = report.ReportID,
                    OrderId = report.OrderID,
                    IssueType = EnumTranslator.TranslateIssueType(report.IssueType),
                    Status = EnumTranslator.TranslateResolutionStatus(report.ResolutionStatus),
                    Description = report.Description,
                    CreatedAt = report.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                });
            }

            dgvReports.DataSource = rows;
            AddButtonColumn("btnInvestigating", "Đang xử lý");
            AddButtonColumn("btnResolved", "Đã xử lý");
            AddButtonColumn("btnPrint", "In biên bản");

            if (dgvReports.Columns["ReportId"] != null) dgvReports.Columns["ReportId"]!.HeaderText = "Mã sự cố";
            if (dgvReports.Columns["OrderId"] != null) dgvReports.Columns["OrderId"]!.HeaderText = "Mã vận đơn";
            if (dgvReports.Columns["IssueType"] != null) dgvReports.Columns["IssueType"]!.HeaderText = "Loại sự cố";
            if (dgvReports.Columns["Status"] != null) dgvReports.Columns["Status"]!.HeaderText = "Trạng thái";
            if (dgvReports.Columns["Description"] != null) dgvReports.Columns["Description"]!.HeaderText = "Mô tả";
            if (dgvReports.Columns["CreatedAt"] != null) dgvReports.Columns["CreatedAt"]!.HeaderText = "Ngày ghi nhận";
        }

        private void AddButtonColumn(string name, string text)
        {
            if (!dgvReports.Columns.Contains(name))
            {
                dgvReports.Columns.Add(new DataGridViewButtonColumn { Name = name, Text = text, UseColumnTextForButtonValue = true });
            }
        }

        private void DgvReports_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string id = dgvReports.Rows[e.RowIndex].Cells["ReportId"].Value?.ToString() ?? string.Empty;
            string columnName = dgvReports.Columns[e.ColumnIndex].Name;
            if (columnName == "btnInvestigating")
            {
                DependencyContainer.GetProblemReportService().UpdateResolutionStatus(id, ResolutionStatus.Investigating);
                LoadData();
            }
            else if (columnName == "btnResolved")
            {
                DependencyContainer.GetProblemReportService().UpdateResolutionStatus(id, ResolutionStatus.Resolved);
                LoadData();
            }
            else if (columnName == "btnPrint")
            {
                string document = DependencyContainer.GetProblemReportService().GenerateProblemReportDocument(id);
                DocumentPrintHelper.PrintText("Biên bản sự cố " + id, document, this);
            }
        }

        private sealed class ProblemReportRow
        {
            public string ReportId { get; set; } = string.Empty;
            public string OrderId { get; set; } = string.Empty;
            public string IssueType { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string CreatedAt { get; set; } = string.Empty;
        }
    }
}
