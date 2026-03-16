using System;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class MaintenanceLog
    {
        public string LogID { get; private set; }
        public string VehicleID { get; private set; }
        public DateTime ServiceDate { get; private set; }
        public decimal Cost { get; private set; }
        public string Description { get; private set; }
        public string ServiceProvider { get; private set; }
        public DateTime NextDueDate { get; private set; }

        public MaintenanceLog(string logId, string vehicleId, DateTime serviceDate,
                              decimal cost, string description, string serviceProvider, DateTime nextDueDate)
        {
            LogID = logId;
            VehicleID = vehicleId;
            ServiceDate = serviceDate;
            Cost = cost;
            Description = description;
            ServiceProvider = serviceProvider;
            NextDueDate = nextDueDate;
        }

        // Constructor khong tham so cho XML serialization
        public MaintenanceLog() { }

        // Cap nhat chi phi
        public void UpdateCost(decimal newCost)
        {
            Cost = newCost;
        }

        // Cap nhat ngay bao tri tiep theo
        public void UpdateNextDueDate(DateTime newDate)
        {
            NextDueDate = newDate;
        }

        // Kiem tra da den han bao tri chua
        public bool IsDue()
        {
            return DateTime.Now >= NextDueDate;
        }

        // Lay thong tin log bao tri
        public string GetLogInfo()
        {
            return "[MaintenanceLog] ID: " + LogID + " | Vehicle: " + VehicleID + "\n" +
                   "  Service: " + ServiceDate.ToString("dd/MM/yyyy") + " | Cost: " + Cost.ToString("N0") + " VND\n" +
                   "  Provider: " + ServiceProvider + " | Next Due: " + NextDueDate.ToString("dd/MM/yyyy") + "\n" +
                   "  Description: " + Description;
        }

        public override string ToString()
        {
            return GetLogInfo();
        }
    }
}
