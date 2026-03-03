using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class ShipmentLog : ITrackable
    {
        //Properties
        public string LogID { get; private set; }
        public string VehicleID { get; private set; }
        public DateTime ServiceDate { get; private set; }
        public decimal Cost { get; private set; }
        public string Description { get; private set; }
        public string ServiceProvider { get; private set; }
        public DateTime NextDueDate { get; private set; }
        //Constructor có tham số
        public ShipmentLog(string logId, string vehicleId, DateTime serviceDate,
                              decimal cost, string description, string serviceProvider, DateTime nextDueDate)
        {
            LogID = logId;
            VehicleID = vehicleId;
            ServiceDate = serviceDate;
            Cost = cost;
            Description = description;
            ServiceProvider = serviceProvider;
            NextDueDate = nextDueDate;

            if (string.IsNullOrWhiteSpace(logId))
                throw new ArgumentException("LogID không được rỗng.");

            if (string.IsNullOrWhiteSpace(vehicleId))
                throw new ArgumentException("VehicleID không được rỗng.");

            if (cost < 0)
                throw new ArgumentException("Chi phí không được âm.");

            if (nextDueDate < serviceDate)
                throw new ArgumentException("NextDueDate phải lớn hơn hoặc bằng ServiceDate.");

        }


        // Constructor không tham số (phục vụ serialization)
        public ShipmentLog() { }

        // Cập nhật chi phí
        public void UpdateCost(decimal newCost)
        {
            if (newCost < 0)
                throw new ArgumentException("Chi phí không được âm.");

            Cost = newCost;
        }

        // Cập nhật ngày bảo trì tiếp theo
        public void UpdateNextDueDate(DateTime newDate)
        {
            if (newDate < ServiceDate)
                throw new ArgumentException("NextDueDate phải lớn hơn ServiceDate.");

            NextDueDate = newDate;
        }

        // Kiểm tra đã đến hạn bảo trì chưa
        public bool IsDue() => DateTime.Now >= NextDueDate;

        // Lấy thông tin log
        public string GetLogInfo()
        {
            return $"[MaintenanceLog] ID: {LogID} | Vehicle: {VehicleID}\n" +
                   $"  Service: {ServiceDate:dd/MM/yyyy} | Cost: {Cost:N0} VND\n" +
                   $"  Provider: {ServiceProvider} | Next Due: {NextDueDate:dd/MM/yyyy}\n" +
                   $"  Description: {Description}";
        }

        public override string ToString() => GetLogInfo();
    }
}