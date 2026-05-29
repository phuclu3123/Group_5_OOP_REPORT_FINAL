using System;

namespace Logistics.Core.Models.Infrastructure
{
    /// <summary>
    /// Luu mot lan bao tri cua phuong tien: ngay bao tri, chi phi,
    /// nha cung cap dich vu va moc bao tri tiep theo.
    /// </summary>
    public class MaintenanceLog
    {
        public string LogID { get; set; }
        public string VehicleID { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ServiceProvider { get; set; }
        public DateTime NextDueDate { get; set; }

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

        // Constructor khong tham so bat buoc cho Newtonsoft.Json.
        public MaintenanceLog()
        {
            LogID = string.Empty;
            VehicleID = string.Empty;
            Description = string.Empty;
            ServiceProvider = string.Empty;
        }

        // Cap nhat chi phi bao tri sau khi co hoa don chinh thuc.
        public void UpdateCost(decimal newCost)
        {
            Cost = newCost;
        }

        // Cap nhat moc bao tri tiep theo theo khuyen nghi ky thuat.
        public void UpdateNextDueDate(DateTime newDate)
        {
            NextDueDate = newDate;
        }

        // True khi da toi han bao tri hoac qua han.
        public bool IsDue()
        {
            return DateTime.Now >= NextDueDate;
        }

        // Chuoi hien thi ngan gon cho man hinh bao tri/bao cao.
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

