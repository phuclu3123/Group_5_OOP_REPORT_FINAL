using System;

namespace Logistics.Core.DTOs
{
    public class MaintenanceDTO
    {
        public string VehicleId { get; set; } = string.Empty;
        public string LogId { get; set; } = string.Empty;
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ServiceProvider { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime NextDueDate { get; set; }
        public bool IsDue { get; set; }
        public string VehicleStatus { get; set; } = string.Empty;
    }
}
