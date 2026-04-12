using System;
using System.Collections.Generic;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable
using Logistic.Core.Models.Common;
using Logistic.Core.Interfaces;

namespace Logistic.Core.Models.Business
{
    // Danh dau class co the duoc serialize
    [Serializable]
    public class ProblemReport : IReportable, ISerializable
    {
        public string ReportID { get; private set; }
        public string OrderID { get; private set; }
        public string Description { get; private set; }
        public IssueType IssueType { get; private set; }
        public List<string> EvidenceImages { get; private set; }
        public ResolutionStatus ResolutionStatus { get; private set; }
        public DateTime CreatedDate { get; private set; }

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

        // Constructor khong tham so cho serialization
        public ProblemReport()
        {
            EvidenceImages = new List<string>();
        }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong ProblemReport tu SerializationInfo khi doc tu file
        protected ProblemReport(SerializationInfo info, StreamingContext context)
        {
            ReportID = info.GetString("ReportID") ?? "";
            OrderID = info.GetString("OrderID") ?? "";
            Description = info.GetString("Description") ?? "";

            // Phuc hoi enum bang ep kieu (cast) tu GetValue
            IssueType = (IssueType)info.GetValue("IssueType", typeof(IssueType));
            ResolutionStatus = (ResolutionStatus)info.GetValue("ResolutionStatus", typeof(ResolutionStatus));

            // Phuc hoi danh sach bang chung
            EvidenceImages = (List<string>)info.GetValue("EvidenceImages", typeof(List<string>)) ?? new List<string>();

            CreatedDate = info.GetDateTime("CreatedDate");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua ProblemReport vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ReportID", ReportID);
            info.AddValue("OrderID", OrderID);
            info.AddValue("Description", Description);
            info.AddValue("IssueType", IssueType);              // Ghi enum
            info.AddValue("ResolutionStatus", ResolutionStatus); // Ghi enum
            info.AddValue("EvidenceImages", EvidenceImages);     // Ghi List<string>
            info.AddValue("CreatedDate", CreatedDate);           // Ghi DateTime
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