using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    public interface IProblemReportService
    {
        ProblemReport CreateReport(string orderId, IssueType issueType, string description);
        bool UpdateResolutionStatus(string reportId, ResolutionStatus status);
        List<ProblemReport> GetReportsByOrder(string orderId);
        List<ProblemReport> GetAllReports();
        string GenerateProblemReportDocument(string reportId);
    }
}
