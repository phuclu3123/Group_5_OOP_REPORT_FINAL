using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace Cuoi_ky_OOP.Models.Business
{
    public enum IssueType { Damage, LostPackage, DelayedDelivery, Other }
    public enum ResolutionStatus { Open, InProgress, Resolved, Rejected }

    public class ProblemReport
    {
        private string ReportID { get; set; } = string.Empty;
        private string OrderID { get; set; } = string.Empty;
        private string Description { get; set; } = string.Empty;
        private IssueType IssueType { get; set; }
        private List<string> EvidenceImages { get; set; } = new List<string>();
        private ResolutionStatus ResolutionStatus { get; set; }
        public ProblemReport() {}
    }
}