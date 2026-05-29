using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;

namespace Logistics.Core.Models.Business
{
    public class Order : ITrackable, IReportable, ISerializable
    {   
        /*
        I. Các thuộc tính cơ bản của một đơn hàng
        1. TrackingNumber (mã vận đơn)
        2. SenderID (ID người gửi)
        3. ReceiverID (ID người nhận)
        4. DeliveryRoute (tuyến giao hàng - sử dụng lớp DeliveryRoute đã tạo)
        5. OrderDetails (danh sách chi tiết đơn hàng, sử dụng lớp OrderDetail đã tạo)
        6. StatusHistories (lịch sử trạng thái của đơn hàng, có thể là một danh sách các OrderStatusHistory)
        7. Packages (danh sách các gói hàng trong đơn, sử dụng lớp Package đã tạo)
        8. TotalWeight (tổng khối lượng của đơn hàng, tự động tính dựa trên các gói hàng)
        9. TotalCost (tổng chi phí của đơn hàng, có thể tính dựa trên khối lượng và loại dịch vụ)
        10. ServiceType (loại dịch vụ: Standard, Express, SameDay)
        11. CurrentStatus (trạng thái hiện tại của đơn hàng: Pending, InTransit, Delivered, Cancelled)
        12. CreatedDate (ngày tạo đơn hàng)
        13. AssignedDriverID (ID tài xế được giao cho đơn hàng)
        14. AssignedVehicleID (ID phương tiện được giao cho đơn hàng)
        */
        public string TrackingNumber { get; private set; }
        public string SenderID { get; private set; }
        public string ReceiverID { get; private set; }
        // Composition: DeliveryRoute duoc tao va quan ly hoan toan boi Order (Part phu thuoc chat che vao Whole)
        public DeliveryRoute Route { get; private set; }
        // Composition 2: Order Contains OrderDetail (Whole-Part, Part cannot exist without Whole)
        public List<OrderDetail> Details { get; private set; }

        public List<OrderStatusHistory> StatusHistories { get; private set; }

        // Observer Pattern: Su kien phat ra khi trang thai thay doi
        public event OrderStatusChangedEventHandler? OnStatusChanged;

        public List<Package> Packages { get; private set; }
        public double TotalWeight { get; private set; }
        public decimal TotalCost { get; private set; }
        public ServiceType ServiceType { get; private set; }
        public OrderStatus CurrentStatus { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string AssignedDriverID { get; private set; }
        public string AssignedVehicleID { get; private set; }
        public decimal CodAmount { get; private set; }
        public CodStatus CodStatus { get; private set; }
        public int FailureCount { get; private set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một đơn hàng
        public Order(string trackingNumber, string senderId, string receiverId,
                     string pickupAddress, string deliveryAddress, ServiceType serviceType)
        {
            TrackingNumber = trackingNumber;
            SenderID = senderId;
            ReceiverID = receiverId;
            Route = new DeliveryRoute(pickupAddress, deliveryAddress); // Khoi tao part tu ben trong whole
            ServiceType = serviceType;
            Packages = new List<Package>();
            Details = new List<OrderDetail>(); // Composition
            StatusHistories = new List<OrderStatusHistory>();
            TotalWeight = 0;
            TotalCost = 0;
            CurrentStatus = OrderStatus.Pending;
            CreatedDate = DateTime.Now;
            AssignedDriverID = "";
            AssignedVehicleID = "";
            CodAmount = 0;
            CodStatus = CodStatus.None;
            FailureCount = 0;
        }

        // Constructor khong tham so cho JSON serialization
        public Order()
        {
            Packages = new List<Package>();
            Details = new List<OrderDetail>();
            StatusHistories = new List<OrderStatusHistory>();
            TrackingNumber = string.Empty;
            SenderID = string.Empty;
            ReceiverID = string.Empty;
            AssignedDriverID = string.Empty;
            AssignedVehicleID = string.Empty;
            Route = new DeliveryRoute("", "");
            CodAmount = 0;
            CodStatus = CodStatus.None;
            FailureCount = 0;
        }

        // Constructor cho ISerializable (Deserialization)
        protected Order(SerializationInfo info, StreamingContext context)
        {
            TrackingNumber = info.GetString("TrackingNumber") ?? string.Empty;
            SenderID = info.GetString("SenderID") ?? string.Empty;
            ReceiverID = info.GetString("ReceiverID") ?? string.Empty;
            Route = (DeliveryRoute)info.GetValue("Route", typeof(DeliveryRoute))!;
            Details = (List<OrderDetail>)info.GetValue("Details", typeof(List<OrderDetail>))!;
            Packages = (List<Package>)info.GetValue("Packages", typeof(List<Package>))!;
            StatusHistories = (List<OrderStatusHistory>)(info.GetValue("StatusHistories", typeof(List<OrderStatusHistory>)) ?? new List<OrderStatusHistory>());
            TotalWeight = info.GetDouble("TotalWeight");
            TotalCost = info.GetDecimal("TotalCost");
            ServiceType = (ServiceType)info.GetValue("ServiceType", typeof(ServiceType))!;
            CurrentStatus = (OrderStatus)info.GetValue("CurrentStatus", typeof(OrderStatus))!;
            CreatedDate = info.GetDateTime("CreatedDate");
            AssignedDriverID = info.GetString("AssignedDriverID") ?? string.Empty;
            AssignedVehicleID = info.GetString("AssignedVehicleID") ?? string.Empty;
            try
            {
                CodAmount = info.GetDecimal("CodAmount");
                CodStatus = (CodStatus)info.GetValue("CodStatus", typeof(CodStatus))!;
                FailureCount = info.GetInt32("FailureCount");
            }
            catch (Exception)
            {
                CodAmount = 0;
                CodStatus = CodStatus.None;
                FailureCount = 0;
            }
        }

        // Phuong thuc ISerializable (Serialization)
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TrackingNumber", TrackingNumber);
            info.AddValue("SenderID", SenderID);
            info.AddValue("ReceiverID", ReceiverID);
            info.AddValue("Route", Route);
            info.AddValue("Details", Details);
            info.AddValue("Packages", Packages);
            info.AddValue("StatusHistories", StatusHistories);
            info.AddValue("TotalWeight", TotalWeight);
            info.AddValue("TotalCost", TotalCost);
            info.AddValue("ServiceType", ServiceType);
            info.AddValue("CurrentStatus", CurrentStatus);
            info.AddValue("CreatedDate", CreatedDate);
            info.AddValue("AssignedDriverID", AssignedDriverID);
            info.AddValue("AssignedVehicleID", AssignedVehicleID);
            info.AddValue("CodAmount", CodAmount);
            info.AddValue("CodStatus", CodStatus);
            info.AddValue("FailureCount", FailureCount);
        }

        // ===== ITrackable =====
        /*
        II. Các phương thức của một đơn hàng
        1. Cập nhật trạng thái đơn hàng (kèm theo mô tả và người thay đổi)
        2. Cập nhật địa chỉ giao hàng (tự động cập nhật tuyến đường)
        3. Gán tài xế cho đơn hàng
        4. Gán phương tiện cho đơn hàng
        5. Tính chi phí đơn hàng dựa trên khối lượng và loại dịch vụ (sử dụng Strategy Pattern)
        6. Tìm kiếm gói hàng trong đơn theo ID
        7. Lấy số lượng gói hàng trong đơn
        8. Lấy thông tin chi tiết của đơn hàng
        9. Override ToString() để hiển thị thông tin chi tiết của đơn hàng
        */
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
                   "  Route: " + Route.ToString();
        }

        // ===== IReportable =====
        public string GenerateReport()
        {
            string report = "========== BAO CAO DON HANG ==========\n" +
                            "  Tracking: " + TrackingNumber + "\n" +
                            "  Ngay tao: " + CreatedDate.ToString("dd/MM/yyyy HH:mm") + "\n" +
                            "  Nguoi gui: " + SenderID + " | Nguoi nhan: " + ReceiverID + "\n" +
                            "  Tuyen duong: " + Route.ToString() + "\n" +
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

        // Method showing Composition: instantiating the part INSIDE the whole
        public void AddDetail(string productName, int quantity, decimal unitPrice)
        {
            string detailId = TrackingNumber + "_DTL" + (Details.Count + 1);
            // Khoi tao truc tiep ben trong Order. Part is completely managed by Whole
            OrderDetail detail = new OrderDetail(detailId, productName, quantity, unitPrice);
            Details.Add(detail);
        }

        // Them goi hang vao don
        public void AddPackage(Package package)
        {
            if (package == null)
            {
                return;
            }

            package.MarkWaitingPickup();
            Packages.Add(package);
            CalculateTotalWeight();
        }

        public bool IsBlankRecord()
        {
            bool hasIdentity = !string.IsNullOrWhiteSpace(TrackingNumber) ||
                               !string.IsNullOrWhiteSpace(SenderID) ||
                               !string.IsNullOrWhiteSpace(ReceiverID);
            bool hasPackages = Packages != null && Packages.Count > 0;
            return !hasIdentity && !hasPackages && CreatedDate == DateTime.MinValue;
        }

        public bool IsStructurallyValid()
        {
            return !string.IsNullOrWhiteSpace(TrackingNumber) &&
                   !string.IsNullOrWhiteSpace(SenderID) &&
                   !string.IsNullOrWhiteSpace(ReceiverID) &&
                   CreatedDate != DateTime.MinValue &&
                   Packages != null &&
                   Packages.Count > 0;
        }

        public void EnsureRuntimeDefaults()
        {
            if (TrackingNumber == null)
            {
                TrackingNumber = string.Empty;
            }

            if (SenderID == null)
            {
                SenderID = string.Empty;
            }

            if (ReceiverID == null)
            {
                ReceiverID = string.Empty;
            }

            if (Route == null)
            {
                Route = new DeliveryRoute(string.Empty, string.Empty);
            }

            if (Details == null)
            {
                Details = new List<OrderDetail>();
            }

            if (Packages == null)
            {
                Packages = new List<Package>();
            }

            if (StatusHistories == null)
            {
                StatusHistories = new List<OrderStatusHistory>();
            }

            if (AssignedDriverID == null)
            {
                AssignedDriverID = string.Empty;
            }

            if (AssignedVehicleID == null)
            {
                AssignedVehicleID = string.Empty;
            }

            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages[i] != null)
                {
                    Packages[i].EnsureRuntimeDefaults();
                }
            }

            CalculateTotalWeight();
            SynchronizePackageStatuses(CurrentStatus, CurrentStatus);
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

        // Tinh the tich vat ly thuc te (m3) cua don hang
        public double GetTotalVolume()
        {
            double total = 0;
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages[i] != null)
                {
                    total += Packages[i].GetPhysicalVolume();
                }
            }
            return total;
        }

        // Tang so lan giao that bai
        public void IncrementFailure()
        {
            FailureCount++;
        }

        // Cap nhat trang thai COD
        public void UpdateCodStatus(CodStatus newStatus)
        {
            CodStatus = newStatus;
        }

        // Thiet lap thong tin COD
        public void SetCodDetails(decimal amount, CodStatus status)
        {
            CodAmount = amount;
            CodStatus = status;
        }

        // Cap nhat trang thai don hang
        public void ChangeStatus(OrderStatus newStatus, string description = "", string changedBy = "System")
        {
            OrderStatus requestedStatus = newStatus;
            if (newStatus == OrderStatus.DeliveryAttemptFailed)
            {
                IncrementFailure();
                if (FailureCount < 3)
                {
                    newStatus = OrderStatus.Pending;
                    description = (string.IsNullOrWhiteSpace(description) ? "" : description + " | ") + "Giao that bai lan " + FailureCount + ". Chuyen ve cho giao lai.";
                }
                else
                {
                    newStatus = OrderStatus.Returned;
                    description = (string.IsNullOrWhiteSpace(description) ? "" : description + " | ") + "Giao that bai lan " + FailureCount + ". Chuyen thanh hang hoan tra.";
                    
                    if (Route != null)
                    {
                        string temp = Route.PickupAddress;
                        Route.UpdateDeliveryAddress(temp);
                    }
                    TotalCost = TotalCost * 1.5m;
                }
            }
            else if (newStatus == OrderStatus.Failed)
            {
                if (Route != null)
                {
                    string temp = Route.PickupAddress;
                    Route.UpdateDeliveryAddress(temp);
                }
            }
            else if (newStatus == OrderStatus.Returned)
            {
                if (Route != null)
                {
                    string temp = Route.PickupAddress;
                    Route.UpdateDeliveryAddress(temp);
                }
                TotalCost = TotalCost * 1.5m;
            }

            if (CurrentStatus != newStatus)
            {
                OrderStatus oldStatus = CurrentStatus;
                
                OrderStatusHistory historyItem = new OrderStatusHistory();
                historyItem.PreviousStatus = oldStatus;
                historyItem.NewStatus = newStatus;
                historyItem.ChangedAt = DateTime.Now;
                historyItem.Location = this.Route != null ? this.Route.ToString() : "Unknown";
                historyItem.Description = description;
                historyItem.ChangedBy = changedBy;
                
                StatusHistories.Add(historyItem);
                
                CurrentStatus = newStatus;
                SynchronizePackageStatuses(requestedStatus, newStatus);
                
                // Trigger su kien (Notify Observers)
                if (OnStatusChanged != null)
                {
                    OnStatusChanged(this, oldStatus, newStatus);
                }
            }
        }

        public bool CanTransitionTo(OrderStatus newStatus)
        {
            return CanTransition(CurrentStatus, newStatus);
        }

        public static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if (currentStatus == newStatus)
            {
                return true;
            }

            if (currentStatus == OrderStatus.Delivered ||
                currentStatus == OrderStatus.Cancelled ||
                currentStatus == OrderStatus.Failed ||
                currentStatus == OrderStatus.Returned)
            {
                return false;
            }

            if (currentStatus == OrderStatus.Pending)
            {
                return newStatus == OrderStatus.WaitingPickup ||
                       newStatus == OrderStatus.PickedUp ||
                       newStatus == OrderStatus.ArrivedAtWarehouse ||
                       newStatus == OrderStatus.Sorting ||
                       newStatus == OrderStatus.ReadyForDispatch ||
                       newStatus == OrderStatus.InTransit ||
                       newStatus == OrderStatus.Cancelled ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.WaitingPickup)
            {
                return newStatus == OrderStatus.PickedUp ||
                       newStatus == OrderStatus.Cancelled ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.PickedUp)
            {
                return newStatus == OrderStatus.ArrivedAtWarehouse ||
                       newStatus == OrderStatus.Sorting ||
                       newStatus == OrderStatus.ReadyForDispatch ||
                       newStatus == OrderStatus.InTransit ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.ArrivedAtWarehouse)
            {
                return newStatus == OrderStatus.Sorting ||
                       newStatus == OrderStatus.ReadyForDispatch ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.Sorting)
            {
                return newStatus == OrderStatus.ReadyForDispatch ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.ReadyForDispatch)
            {
                return newStatus == OrderStatus.InTransit ||
                       newStatus == OrderStatus.OutForDelivery ||
                       newStatus == OrderStatus.Cancelled ||
                       newStatus == OrderStatus.Failed;
            }

            if (currentStatus == OrderStatus.InTransit)
            {
                return newStatus == OrderStatus.OutForDelivery ||
                       newStatus == OrderStatus.Delivered ||
                       newStatus == OrderStatus.Failed ||
                       newStatus == OrderStatus.DeliveryAttemptFailed ||
                       newStatus == OrderStatus.Returning ||
                       newStatus == OrderStatus.Returned;
            }

            if (currentStatus == OrderStatus.OutForDelivery)
            {
                return newStatus == OrderStatus.Delivered ||
                       newStatus == OrderStatus.Failed ||
                       newStatus == OrderStatus.DeliveryAttemptFailed ||
                       newStatus == OrderStatus.Returning ||
                       newStatus == OrderStatus.Returned;
            }

            if (currentStatus == OrderStatus.DeliveryAttemptFailed)
            {
                return newStatus == OrderStatus.ReadyForDispatch ||
                       newStatus == OrderStatus.OutForDelivery ||
                       newStatus == OrderStatus.Returning ||
                       newStatus == OrderStatus.Returned;
            }

            if (currentStatus == OrderStatus.Returning)
            {
                return newStatus == OrderStatus.Returned ||
                       newStatus == OrderStatus.InTransit;
            }

            return false;
        }

        private void SynchronizePackageStatuses(OrderStatus requestedStatus, OrderStatus effectiveStatus)
        {
            if (Packages == null)
            {
                return;
            }

            for (int i = 0; i < Packages.Count; i++)
            {
                Package package = Packages[i];
                if (package == null)
                {
                    continue;
                }

                if (requestedStatus == OrderStatus.DeliveryAttemptFailed || effectiveStatus == OrderStatus.DeliveryAttemptFailed)
                {
                    package.MarkFailedDelivery();
                    continue;
                }

                switch (effectiveStatus)
                {
                    case OrderStatus.Pending:
                    case OrderStatus.WaitingPickup:
                        package.MarkWaitingPickup();
                        break;
                    case OrderStatus.PickedUp:
                        package.MarkPickedUp();
                        break;
                    case OrderStatus.Sorting:
                        package.MarkSorting();
                        break;
                    case OrderStatus.ReadyForDispatch:
                        package.CheckOutWarehouse();
                        break;
                    case OrderStatus.InTransit:
                    case OrderStatus.OutForDelivery:
                        package.MarkInTransit();
                        break;
                    case OrderStatus.Delivered:
                        package.MarkDelivered();
                        break;
                    case OrderStatus.Returning:
                        package.MarkReturning();
                        break;
                    case OrderStatus.Returned:
                        package.MarkReturned();
                        break;
                }
            }
        }

        // Cap nhat dia chi giao hang
        public void UpdateDeliveryAddress(string newAddress)
        {
            Route.UpdateDeliveryAddress(newAddress);
        }

        // Gan tai xe cho don hang
        public void AssignDriver(string driverId)
        {
            AssignedDriverID = driverId;
        }

        // Gan phuong tien cho don hang
        public void AssignVehicle(string vehicleId)
        {
            AssignedVehicleID = vehicleId;
        }

        // Tinh chi phi don hang dung Strategy Pattern
        public void CalculateTotalCost(Services.Interfaces.IShippingFeeStrategy feeStrategy, decimal baseRatePerKg)
        {
            if (feeStrategy != null)
            {
                TotalCost = feeStrategy.CalculateFee(this, baseRatePerKg);
            }
        }

        // Tim goi hang theo ID
        public Package? FindPackage(string packageId)
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
                   "  Route: " + Route.ToString() + "\n" +
                   "  Packages: " + Packages.Count + " | OrderDetails: " + Details.Count + " | Weight: " + TotalWeight + "kg | Cost: " + TotalCost.ToString("N0") + " VND\n" +
                   "  Service: " + ServiceType + " | Status: " + CurrentStatus;
        }

        // Override ToString() để hiển thị thông tin chi tiết của đơn hàng
        public override string ToString()
        {
            return GetOrderInfo();
        }
    }
}

