using System;
using System.Collections.Generic;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable
using Logistic.Core.Models.Common;
using Logistic.Core.Interfaces;

namespace Logistic.Core.Models.Business
{
    // Danh dau class co the duoc serialize (bat buoc khi implement ISerializable)
    [Serializable]
    public class Order : ITrackable, IReportable, ISerializable
    {
        // ===== PROPERTIES =====
        public string TrackingNumber { get; private set; }
        public string SenderID { get; private set; }
        public string ReceiverID { get; private set; }
        public string PickupAddress { get; private set; }
        public string DeliveryAddress { get; private set; }

        // Aggregation (Hop thanh): Packages duoc tao ben ngoai, sau do dung AddPackage() them vao
        public List<Package> Packages { get; private set; }

        // Composition (Hop thanh 1-Nhieu): OrderDetail CHI duoc tao ben trong Order
        // OrderDetail khong the ton tai doc lap, sinh cung sinh, diet cung diet voi Order
        public List<OrderDetail> Details { get; private set; }

        public double TotalWeight { get; private set; }
        public decimal TotalCost { get; private set; }
        public ServiceType ServiceType { get; private set; }
        public OrderStatus CurrentStatus { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string AssignedDriverID { get; private set; }

        // Composition (1-Nhieu): StatusHistories CHI duoc tao ben trong Order
        // Ghi lai lich su thay doi trang thai (Timeline Tracking)
        public List<OrderStatusHistory> StatusHistories { get; private set; }

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
            Details = new List<OrderDetail>();
            StatusHistories = new List<OrderStatusHistory>();
            TotalWeight = 0;
            TotalCost = 0;
            CurrentStatus = OrderStatus.Pending;
            CreatedDate = DateTime.Now;
            AssignedDriverID = "";
        }

        // Constructor khong tham so cho serialization
        public Order()
        {
            Packages = new List<Package>();
            Details = new List<OrderDetail>();
            StatusHistories = new List<OrderStatusHistory>();
        }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Constructor nay duoc goi khi deserialization, doc du lieu tu SerializationInfo
        // va gan nguoc lai vao tung property cua doi tuong
        protected Order(SerializationInfo info, StreamingContext context)
        {
            // Phuc hoi cac property kieu string
            TrackingNumber = info.GetString("TrackingNumber") ?? "";
            SenderID = info.GetString("SenderID") ?? "";
            ReceiverID = info.GetString("ReceiverID") ?? "";
            PickupAddress = info.GetString("PickupAddress") ?? "";
            DeliveryAddress = info.GetString("DeliveryAddress") ?? "";
            AssignedDriverID = info.GetString("AssignedDriverID") ?? "";

            // Phuc hoi cac collection (List) bang GetValue voi typeof
            Packages = (List<Package>)info.GetValue("Packages", typeof(List<Package>)) ?? new List<Package>();
            Details = (List<OrderDetail>)info.GetValue("Details", typeof(List<OrderDetail>)) ?? new List<OrderDetail>();
            StatusHistories = (List<OrderStatusHistory>)info.GetValue("StatusHistories", typeof(List<OrderStatusHistory>)) ?? new List<OrderStatusHistory>();

            // Phuc hoi cac property kieu so
            TotalWeight = info.GetDouble("TotalWeight");
            TotalCost = info.GetDecimal("TotalCost");

            // Phuc hoi cac property kieu enum bang ep kieu (cast)
            ServiceType = (ServiceType)info.GetValue("ServiceType", typeof(ServiceType));
            CurrentStatus = (OrderStatus)info.GetValue("CurrentStatus", typeof(OrderStatus));

            // Phuc hoi property kieu DateTime
            CreatedDate = info.GetDateTime("CreatedDate");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Method bat buoc cua ISerializable, ghi toan bo property vao SerializationInfo
        // de luu tru hoac truyen di
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Ghi cac property kieu string
            info.AddValue("TrackingNumber", TrackingNumber);
            info.AddValue("SenderID", SenderID);
            info.AddValue("ReceiverID", ReceiverID);
            info.AddValue("PickupAddress", PickupAddress);
            info.AddValue("DeliveryAddress", DeliveryAddress);
            info.AddValue("AssignedDriverID", AssignedDriverID);

            // Ghi cac collection (List)
            info.AddValue("Packages", Packages);
            info.AddValue("Details", Details);
            info.AddValue("StatusHistories", StatusHistories);

            // Ghi cac property kieu so
            info.AddValue("TotalWeight", TotalWeight);
            info.AddValue("TotalCost", TotalCost);

            // Ghi cac property kieu enum
            info.AddValue("ServiceType", ServiceType);
            info.AddValue("CurrentStatus", CurrentStatus);

            // Ghi property kieu DateTime
            info.AddValue("CreatedDate", CreatedDate);
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
                            "  Chi tiet don hang: " + Details.Count + " dong\n";
            for (int i = 0; i < Details.Count; i++)
            {
                report += "    " + Details[i].GetDetailInfo() + "\n";
            }
            report += "  Tong chi phi: " + TotalCost.ToString("N0") + " VND\n";
            if (AssignedDriverID != "")
            {
                report += "  Tai xe: " + AssignedDriverID + "\n";
            }
            report += "=======================================";
            return report;
        }

        // ===== ORDER METHODS =====

        // Them goi hang vao don hang (Aggregation)
        public void AddPackage(Package package)
        {
            Packages.Add(package);
            CalculateTotalWeight();
        }

        // ===== COMPOSITION: Tao OrderDetail ben trong Order =====
        // OrderDetail CHI duoc sinh ra bang tu khoa 'new' ben trong ham nay
        // Khong the tao OrderDetail tu ben ngoai Order
        public OrderDetail AddDetail(string detailId, string productName, int quantity, decimal unitPrice)
        {
            OrderDetail detail = new OrderDetail(detailId, productName, quantity, unitPrice);
            Details.Add(detail);
            return detail;
        }

        // Xoa chi tiet don hang theo ID
        public bool RemoveDetail(string detailId)
        {
            for (int i = 0; i < Details.Count; i++)
            {
                if (Details[i].DetailID == detailId)
                {
                    Details.RemoveAt(i);
                    return true;
                }
            }
            return false;
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

        // Cap nhat trang thai don hang va tu dong ghi lai lich su thay doi
        public void UpdateStatus(OrderStatus newStatus)
        {
            OrderStatus oldStatus = CurrentStatus;
            CurrentStatus = newStatus;
            // Tu dong ghi lich su khi trang thai thay doi
            AddStatusHistory(oldStatus, newStatus, "", "Cap nhat trang thai tu dong");
        }

        // Cap nhat trang thai don hang voi thong tin nguoi thay doi
        public void UpdateStatus(OrderStatus newStatus, string changedBy, string note)
        {
            OrderStatus oldStatus = CurrentStatus;
            CurrentStatus = newStatus;
            AddStatusHistory(oldStatus, newStatus, changedBy, note);
        }

        // ===== COMPOSITION: Tao OrderStatusHistory ben trong Order =====
        // OrderStatusHistory CHI duoc sinh ra ben trong ham nay
        // Khong the tao OrderStatusHistory tu ben ngoai Order
        public OrderStatusHistory AddStatusHistory(OrderStatus oldStatus, OrderStatus newStatus,
                                                   string changedBy, string note)
        {
            string historyId = "HST-" + TrackingNumber + "-" + (StatusHistories.Count + 1);
            OrderStatusHistory history = new OrderStatusHistory(
                historyId, TrackingNumber, oldStatus, newStatus, changedBy, note);
            StatusHistories.Add(history);
            return history;
        }

        // Lay toan bo lich su thay doi trang thai
        public string GetStatusTimeline()
        {
            string timeline = "========== LICH SU TRANG THAI ==========\n";
            timeline += "  Don hang: " + TrackingNumber + "\n";
            for (int i = 0; i < StatusHistories.Count; i++)
            {
                timeline += "  [" + (i + 1) + "] " + StatusHistories[i].GetHistoryInfo() + "\n";
            }
            timeline += "========================================";
            return timeline;
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
                   "  Packages: " + Packages.Count + " | Details: " + Details.Count + " | Weight: " + TotalWeight + "kg | Cost: " + TotalCost.ToString("N0") + " VND\n" +
                   "  Service: " + ServiceType + " | Status: " + CurrentStatus;
        }

        public override string ToString()
        {
            return GetOrderInfo();
        }
    }
}