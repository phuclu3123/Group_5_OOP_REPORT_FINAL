using System;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class ProblemReportService : IProblemReportService
    {
        private readonly ProblemReportRepository _problemReportRepository;
        private readonly OrderRepository _orderRepository;
        private int _counter;

        public ProblemReportService(ProblemReportRepository problemReportRepository, OrderRepository orderRepository)
        {
            _problemReportRepository = problemReportRepository;
            _orderRepository = orderRepository;
            _counter = _problemReportRepository.Count();
        }

        public ProblemReport CreateReport(string orderId, IssueType issueType, string description)
        {
            Order order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new ArgumentException("Khong tim thay don hang " + orderId);
            }

            _counter++;
            string id = "PR" + DateTime.Now.ToString("yyyyMMdd") + _counter.ToString("0000");
            ProblemReport report = new ProblemReport(id, orderId, issueType, description);
            _problemReportRepository.Add(report);

            if (issueType == IssueType.Damaged || issueType == IssueType.Lost)
            {
                MarkImpactedPackages(order, issueType);
                order.ChangeStatus(OrderStatus.Failed, "Problem report " + id + ": " + issueType, "System");
                _orderRepository.Update(order);
                _orderRepository.SaveChanges();
            }
            else if (issueType == IssueType.WrongAddress)
            {
                order.ChangeStatus(OrderStatus.DeliveryAttemptFailed, "Problem report " + id + ": " + issueType, "System");
                _orderRepository.Update(order);
                _orderRepository.SaveChanges();
            }

            return report;
        }

        public bool UpdateResolutionStatus(string reportId, ResolutionStatus status)
        {
            ProblemReport report = _problemReportRepository.GetById(reportId);
            if (report == null) return false;
            report.UpdateResolutionStatus(status);
            _problemReportRepository.Update(report);
            return true;
        }

        public List<ProblemReport> GetReportsByOrder(string orderId)
        {
            return _problemReportRepository.FindByOrder(orderId);
        }

        public List<ProblemReport> GetAllReports()
        {
            return _problemReportRepository.GetAll();
        }

        public string GenerateProblemReportDocument(string reportId)
        {
            ProblemReport report = _problemReportRepository.GetById(reportId);
            if (report == null)
            {
                return "Khong tim thay bao cao su co: " + reportId;
            }

            return report.GenerateReport();
        }

        private static void MarkImpactedPackages(Order order, IssueType issueType)
        {
            if (order == null || order.Packages == null)
            {
                return;
            }

            for (int i = 0; i < order.Packages.Count; i++)
            {
                Package package = order.Packages[i];
                if (package == null)
                {
                    continue;
                }

                if (issueType == IssueType.Damaged)
                {
                    package.MarkDamaged();
                }
                else if (issueType == IssueType.Lost)
                {
                    package.MarkLost();
                }
            }
        }
    }
}
