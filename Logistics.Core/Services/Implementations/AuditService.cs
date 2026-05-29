using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Business;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class AuditService : IAuditService
    {
        private readonly AuditLogRepository _auditLogRepository;

        public AuditService(AuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public void Log(string actorUsername, string action, string entityType, string entityId, string detail)
        {
            AuditLog log = new AuditLog
            {
                ActorUsername = string.IsNullOrWhiteSpace(actorUsername) ? "system" : actorUsername,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                Detail = detail
            };

            _auditLogRepository.Add(log);
        }

        public List<AuditLog> GetRecentLogs(int maxCount)
        {
            List<AuditLog> all = _auditLogRepository.GetAll();
            List<AuditLog> result = new List<AuditLog>();
            for (int i = all.Count - 1; i >= 0 && result.Count < maxCount; i--)
            {
                result.Add(all[i]);
            }

            return result;
        }
    }
}
