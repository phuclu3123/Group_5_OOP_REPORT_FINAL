using System.Collections.Generic;
using Logistics.Core.Models.Business;

namespace Logistics.Core.Services.Interfaces
{
    public interface IAuditService
    {
        void Log(string actorUsername, string action, string entityType, string entityId, string detail);
        List<AuditLog> GetRecentLogs(int maxCount);
    }
}
