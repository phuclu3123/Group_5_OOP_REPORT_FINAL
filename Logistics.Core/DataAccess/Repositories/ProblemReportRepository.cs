using System.Collections.Generic;
using Logistics.Core.Models.Business;

namespace Logistics.Core.DataAccess.Repositories
{
    public class ProblemReportRepository : JsonRepository<ProblemReport>
    {
        public ProblemReportRepository(string filePath) : base(filePath)
        {
        }

        public override ProblemReport GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].ReportID == id)
                {
                    return _items[i];
                }
            }

            return null!;
        }

        public List<ProblemReport> FindByOrder(string orderId)
        {
            List<ProblemReport> result = new List<ProblemReport>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].OrderID == orderId)
                {
                    result.Add(_items[i]);
                }
            }

            return result;
        }

        public override void Update(ProblemReport entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ReportID == entity.ReportID)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }
    }
}
