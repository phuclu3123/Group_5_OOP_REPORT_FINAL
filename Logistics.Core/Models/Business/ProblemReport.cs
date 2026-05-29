using System;
using System.Collections.Generic;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;

namespace Logistics.Core.Models.Business
{
    public class ProblemReport : IReportable
    {
        public string ReportID { get; set; }
        public string OrderID { get; set; }
        public string Description { get; set; }
        public IssueType IssueType { get; set; }
        public List<string> EvidenceImages { get; set; }
        public ResolutionStatus ResolutionStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProblemReport(string reportId, string orderId, IssueType issueType, string description)
        {
            ReportID = reportId;
            OrderID = orderId;
            IssueType = issueType;
            Description = description;
            EvidenceImages = new List<string>();
            ResolutionStatus = ResolutionStatus.Open;
            CreatedDate = DateTime.Now;
        }

        // Constructor khong tham so cho JSON serialization
        public ProblemReport()
        {
            ReportID = string.Empty;
            OrderID = string.Empty;
            Description = string.Empty;
            EvidenceImages = new List<string>();
        }

        // ===== IReportable =====
        public string GenerateReport()
        {
            string evidenceList = "";
            for (int i = 0; i < EvidenceImages.Count; i++)
            {
                evidenceList += "    - " + EvidenceImages[i] + "\n";
            }
            return "========== BAO CAO SU CO ==========\n" +
                   "  Report ID: " + ReportID + "\n" +
                   "  Order: " + OrderID + "\n" +
                   "  Loai: " + IssueType + " | Trang thai: " + ResolutionStatus + "\n" +
                   "  Mo ta: " + Description + "\n" +
                   "  Ngay tao: " + CreatedDate.ToString("dd/MM/yyyy") + "\n" +
                   "  Bang chung (" + EvidenceImages.Count + "):\n" + evidenceList +
                   "====================================";
        }

        // Cap nhat trang thai xu ly
        public void UpdateResolutionStatus(ResolutionStatus newStatus)
        {
            ResolutionStatus = newStatus;
        }

        // Them anh bang chung
        public void AddEvidence(string imageUrl)
        {
            EvidenceImages.Add(imageUrl);
        }

        // Xoa anh bang chung
        public bool RemoveEvidence(string imageUrl)
        {
            return EvidenceImages.Remove(imageUrl);
        }

        // Cap nhat mo ta
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        // Danh dau da giai quyet
        public void Resolve()
        {
            ResolutionStatus = ResolutionStatus.Resolved;
        }

        // Lay thong tin bao cao
        public string GetReportInfo()
        {
            return "[Report] ID: " + ReportID + " | Order: " + OrderID + "\n" +
                   "  Type: " + IssueType + " | Status: " + ResolutionStatus + "\n" +
                   "  Description: " + Description + "\n" +
                   "  Evidence: " + EvidenceImages.Count + " image(s) | Created: " + CreatedDate.ToString("dd/MM/yyyy");
        }

        public override string ToString()
        {
            return GetReportInfo();
        }
    }
}
