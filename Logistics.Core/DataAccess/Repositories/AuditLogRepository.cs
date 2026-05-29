using Logistics.Core.Models.Business;

namespace Logistics.Core.DataAccess.Repositories
{
    public class AuditLogRepository : JsonRepository<AuditLog>
    {
        public AuditLogRepository(string filePath) : base(filePath)
        {
        }

        public override AuditLog GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].AuditLogId == id)
                {
                    return _items[i];
                }
            }

            return null!;
        }
    }
}
