using System;
using System.Collections.Generic;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Business
{
    public class Order : ITrackable, IReportable
    {
        public string TrackingNumber { get; private set; }
        public string SenderID { get; private set; }
        public string ReceiverID { get; private set; }
        public string PickupAddress { get; private set; }
        public string DeliveryAddress { get; private set; }
        public List<Package> Packages { get; private set; }
        public double TotalWeight { get; private set; }
        public decimal TotalCost { get; private set; }
        public ServiceType ServiceType { get; private set; }
        public OrderStatus CurrentStatus { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string AssignedDriverID { get; private set; }

        public Order(string trackingNumber, string senderId, string receiverId,
                     string pickupAddress, string deliveryAddress, ServiceType serviceType)
        {
            TrackingNumber = trackingNumber;
            SenderID = senderId;
            ReceiverID = receiverId;
            PickupAddress = pickupAddress;
            DeliveryAddress = deliveryAddress;
            ServiceType = serviceType;
            Packages = new List<Package>();
            TotalWeight = 0;
            TotalCost = 0;
            CurrentStatus = OrderStatus.Pending;
            CreatedDate = DateTime.Now;
            AssignedDriverID = "";
        }

        // Constructor khong tham so cho XML serialization
        public Order()
        {
            Packages = new List<Package>();
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            return CurrentStatus.ToString();
        }

        public string GetTrackingInfo()
        {
            string driverInfo = "Chua gan";
            if (AssignedDriverID != "")
            {
                driverInfo = AssignedDriverID;
            }
            return "[Tracking] " + TrackingNumber + "\n" +
                   "  Status: " + CurrentStatus + " | Driver: " + driverInfo + "\n" +
                   "  From: " + PickupAddress + "\n" +
                   "  To: " + DeliveryAddress;
        }

        // ===== IReportable =====
        public string GenerateReport()
        {
            string report = "========== BAO CAO DON HANG ==========\n" +
                            "  Tracking: " + TrackingNumber + "\n" +
                            "  Ngay tao: " + CreatedDate.ToString("dd/MM/yyyy HH:mm") + "\n" +
                            "  Nguoi gui: " + SenderID + " | Nguoi nhan: " + ReceiverID + "\n" +
                            "  Dia chi lay: " + PickupAddress + "\n" +
                            "  Dia chi giao: " + DeliveryAddress + "\n" +
                            "  Dich vu: " + ServiceType + " | Trang thai: " + CurrentStatus + "\n" +
                            "  So goi: " + Packages.Count + " | Tong KL: " + TotalWeight + "kg\n" +
                            "  Tong chi phi: " + TotalCost.ToString("N0") + " VND\n";
            if (AssignedDriverID != "")
            {
                report += "  Tai xe: " + AssignedDriverID + "\n";
            }
            report += "=======================================";
            return report;
        }

        // ===== ORDER METHODS =====

        // Them goi hang vao don
        public void AddPackage(Package package)
        {
            Packages.Add(package);
            CalculateTotalWeight();
        }

        // Xoa goi hang khoi don
        public bool RemovePackage(string packageId)
        {
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages[i].PackageID == packageId)
                {
                    Packages.RemoveAt(i);
                    CalculateTotalWeight();
                    return true;
                }
            }
            return false;
        }

        // Tinh tong khoi luong cac goi hang
        public void CalculateTotalWeight()
        {
            double total = 0;
            for (int i = 0; i < Packages.Count; i++)
            {
                total += Packages[i].ActualWeight;
            }
            TotalWeight = total;
        }

        // Cap nhat trang thai don hang
        public void UpdateStatus(OrderStatus newStatus)
        {
            CurrentStatus = newStatus;
        }

        // Cap nhat dia chi giao hang
        public void UpdateDeliveryAddress(string newAddress)
        {
            DeliveryAddress = newAddress;
        }

        // Gan tai xe cho don hang
        public void AssignDriver(string driverId)
        {
            AssignedDriverID = driverId;
        }

        // Tinh chi phi don hang
        public void CalculateTotalCost(decimal costPerKg)
        {
            decimal baseRate = 1.0m;
            if (ServiceType == ServiceType.Express)
            {
                baseRate = 1.5m;
            }
            else if (ServiceType == ServiceType.Instant)
            {
                baseRate = 2.0m;
            }
            TotalCost = (decimal)TotalWeight * costPerKg * baseRate;
        }

        // Tim goi hang theo ID
        public Package FindPackage(string packageId)
        {
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages[i].PackageID == packageId)
                {
                    return Packages[i];
                }
            }
            return null;
        }

        // Lay so luong goi hang
        public int GetPackageCount()
        {
            return Packages.Count;
        }

        // Lay thong tin don hang
        public string GetOrderInfo()
        {
            return "[Order] Tracking: " + TrackingNumber + " | Sender: " + SenderID + " | Receiver: " + ReceiverID + "\n" +
                   "  From: " + PickupAddress + " -> To: " + DeliveryAddress + "\n" +
                   "  Packages: " + Packages.Count + " | Weight: " + TotalWeight + "kg | Cost: " + TotalCost.ToString("N0") + " VND\n" +
                   "  Service: " + ServiceType + " | Status: " + CurrentStatus;
        }

        public override string ToString()
        {
            return GetOrderInfo();
        }
    }
}