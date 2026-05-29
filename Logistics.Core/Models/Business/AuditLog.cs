using System;

namespace Logistics.Core.Models.Business
{
    public class AuditLog
    {
        public string AuditLogId { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ActorUsername { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
