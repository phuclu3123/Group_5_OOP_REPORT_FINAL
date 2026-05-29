using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;

namespace Logistics.Core.Models.Actors
{
    public class Driver : Staff, ITrackable
    {   
        /*
        I. Các thuộc tính cơ bản của một tài xế
        1. LicenseNumber (số bằng lái)
        2. LicenseType (loại bằng lái: A1, A2, B1, B2, C)
        3. LicenseExpiryDate (ngày hết hạn bằng lái)
        4. DriverStatus (trạng thái tài xế: Available, Busy, OffDuty)
        5. CurrentLocation (vị trí hiện tại của tài xế, sử dụng GeoPoint)
        6. LastLocationUpdate (thời điểm cập nhật vị trí cuối cùng)
        7. AccidentHistory (lịch sử tai nạn, có thể là một danh sách các chuỗi mô tả tai nạn)
        8. AssignedVehicles (danh sách các xe được giao cho tài xế, sử dụng lớp Vehicle đã tạo)
        9. DeliveryCount (số lượng giao hàng đã hoàn thành trong tháng, dùng để tính thưởng)
        10. BonusPerDelivery (thưởng cho mỗi giao hàng hoàn thành)
        11. FuelAllowance (phụ cấp xăng hàng tháng)
        */
        [JsonProperty]
        public string LicenseNumber { get; private set; }
        [JsonProperty]
        public string LicenseType { get; private set; }
        [JsonProperty]
        public DateTime LicenseExpiryDate { get; private set; }
        [JsonProperty]
        public DriverStatus DriverStatus { get; private set; }
        [JsonProperty]
        public GeoPoint CurrentLocation { get; private set; }
        [JsonProperty]
        public DateTime LastLocationUpdate { get; private set; }
        
        public List<string> AccidentHistory { get; private set; }
        public int AccidentCount
        {
            get { return AccidentHistory?.Count ?? 0; }
        }
        
        // Association: 1 Driver có thể lái nhiều Vehicle (1..*)
        [JsonProperty]
        public List<Infrastructure.Vehicle> AssignedVehicles { get; private set; }

        // Thuộc tính để tính lương
        public int DeliveryCount { get; private set; }
        public decimal BonusPerDelivery { get; private set; }
        public decimal FuelAllowance { get; private set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một tài xế
        public Driver(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, string accountId,
                      string staffId, string department, decimal baseSalary, DateTime joinDate,
                      string licenseNumber, string licenseType, DateTime licenseExpiryDate)
            : base(id, fullName, phoneNumber, email, homeAddress, birthDay, gender, accountId, staffId, Role.Driver, department, baseSalary, joinDate)
        {
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
            LicenseExpiryDate = licenseExpiryDate;
            DriverStatus = DriverStatus.Available;
            CurrentLocation = new GeoPoint(0, 0); // Tọa độ mặc định ban đầu
            LastLocationUpdate = DateTime.Now;
            AssignedVehicles = new List<Infrastructure.Vehicle>();
            AccidentHistory = new List<string>();
            DeliveryCount = 0;
            BonusPerDelivery = 50000m;
            FuelAllowance = 1500000m;
        }

        // Phương thức để thêm xe vào danh sách AssignedVehicles
        public void AddAssignedVehicle(Infrastructure.Vehicle vehicle)
        {
            if (vehicle != null && !AssignedVehicles.Contains(vehicle))
            {
                AssignedVehicles.Add(vehicle);
            }
        }

        // Constructor không tham số cho JSON serialization và các lớp con
        protected Driver() : base()
        {
            LicenseNumber = string.Empty;
            LicenseType = string.Empty;
            CurrentLocation = new GeoPoint(0, 0);
            AssignedVehicles = new List<Infrastructure.Vehicle>();
            AccidentHistory = new List<string>();
        }

        // Constructor cho ISerializable (Deserialization)
        protected Driver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            LicenseNumber = info.GetString("LicenseNumber") ?? string.Empty;
            LicenseType = info.GetString("LicenseType") ?? string.Empty;
            LicenseExpiryDate = info.GetDateTime("LicenseExpiryDate");
            DriverStatus = (DriverStatus)info.GetValue("DriverStatus", typeof(DriverStatus))!;
            CurrentLocation = (GeoPoint)info.GetValue("CurrentLocation", typeof(GeoPoint))!;
            LastLocationUpdate = info.GetDateTime("LastLocationUpdate");
            AssignedVehicles = (List<Infrastructure.Vehicle>)(info.GetValue("AssignedVehicles", typeof(List<Infrastructure.Vehicle>)) ?? new List<Infrastructure.Vehicle>());
            AccidentHistory = (List<string>)(info.GetValue("AccidentHistory", typeof(List<string>)) ?? new List<string>());
            DeliveryCount = info.GetInt32("DeliveryCount");
            BonusPerDelivery = info.GetDecimal("BonusPerDelivery");
            FuelAllowance = info.GetDecimal("FuelAllowance");
        }

        // Phương thức ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("LicenseNumber", LicenseNumber);
            info.AddValue("LicenseType", LicenseType);
            info.AddValue("LicenseExpiryDate", LicenseExpiryDate);
            info.AddValue("DriverStatus", DriverStatus);
            info.AddValue("CurrentLocation", CurrentLocation);
            info.AddValue("LastLocationUpdate", LastLocationUpdate);
            info.AddValue("AssignedVehicles", AssignedVehicles);
            info.AddValue("AccidentHistory", AccidentHistory);
            info.AddValue("DeliveryCount", DeliveryCount);
            info.AddValue("BonusPerDelivery", BonusPerDelivery);
            info.AddValue("FuelAllowance", FuelAllowance);
        }

        /*
        II. Các phương thức của một tài xế
        1. Cập nhật trạng thái tài xế
        2. Cập nhật vị trí hiện tại
        3. Thêm bản ghi tai nạn
        4. Tính điểm hiệu suất dựa trên số lượng giao hàng và tai nạn
        5. Kiểm tra xem bằng lái còn hiệu lực hay không
        6. Bắt đầu giao hàng (chuyển trạng thái thành Busy)
        7. Hoàn thành giao hàng (chuyển trạng thái thành Available và tăng DeliveryCount)
        8. Đi làm muộn (chuyển trạng thái thành OffDuty)
        9. Kiểm tra xem tài xế có đang sẵn sàng nhận giao hàng hay không
        10. Lấy thông tin chi tiết của một tài xế
        11. Override ToString() để hiển thị thông tin chi tiết của một tài xế
        */

        // ===== POLYMORPHISM: Tính lương tài xế =====
        public override decimal CalculateSalary()
        {
            decimal deliveryBonus = DeliveryCount * BonusPerDelivery;
            return BaseSalary + deliveryBonus + FuelAllowance;
        }

        public override string GetSalaryBreakdown()
        {
            decimal deliveryBonus = DeliveryCount * BonusPerDelivery;
            // Áp dụng String Interpolation
            return $"[Lương Tài Xế] {FullName}\n" +
                   $"  Lương cơ bản:     {BaseSalary:N0} VND\n" +
                   $"  Thưởng giao hàng: {DeliveryCount} chuyến x {BonusPerDelivery:N0} = {deliveryBonus:N0} VND\n" +
                   $"  Phụ cấp xăng:     {FuelAllowance:N0} VND\n" +
                   $"  -------------------------\n" +
                   $"  TỔNG LƯƠNG:       {CalculateSalary():N0} VND";
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            return DriverStatus.ToString();
        }

        public string GetTrackingInfo()
        {
            return $"[Tracking Driver] {FullName} ({StaffID})\n" +
                   $"  Status: {DriverStatus} | Location: {CurrentLocation}";
        }

        // ===== DRIVER METHODS =====

        public void RecordDelivery() { DeliveryCount++; }
        public void ResetDeliveryCount() { DeliveryCount = 0; }
        public void UpdateBonusPerDelivery(decimal newBonus) { BonusPerDelivery = newBonus; }
        public void UpdateFuelAllowance(decimal newAllowance) { FuelAllowance = newAllowance; }
        public void UpdateDriverStatus(DriverStatus newStatus) { DriverStatus = newStatus; }
        public void UpdateCurrentLocation(GeoPoint newLocation)
        {
            CurrentLocation = newLocation;
            LastLocationUpdate = DateTime.Now;
        }
        
        public bool IsLicenseValid()
        {
            return LicenseExpiryDate > DateTime.Now;
        }

        public decimal GetPerformanceRating()
        {
            if (DeliveryCount == 0 && AccidentCount == 0) return 0;
            return (DeliveryCount - AccidentCount * 5) / (decimal)Math.Max(1, DeliveryCount);
        }
        
        public void AddAccidentRecord(string record)
        {
            if (AccidentHistory != null)
            {
                AccidentHistory.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] {record}");
            }
        }

        public void StartDelivery() { DriverStatus = DriverStatus.Busy; }
        
        public void CompleteDelivery()
        {
            DriverStatus = DriverStatus.Available;
            RecordDelivery();
        }
        
        public void GoOffDuty() { DriverStatus = DriverStatus.OffDuty; }
        public bool IsAvailable() { return DriverStatus == DriverStatus.Available; }

        public override string GetInfo()
        {
            // Gọi base.GetInfo() để lấy chuỗi thông tin của Person và Staff, sau đó cộng thêm đặc thù của Driver
            return base.GetInfo() + "\n" +
                   $"[Driver Details] License: {LicenseNumber} ({LicenseType})\n" +
                   $"  Driver Status: {DriverStatus} | Location: {CurrentLocation}\n" +
                   $"  Deliveries: {DeliveryCount}";
        }

        // 11. Override ToString() để hiển thị thông tin chi tiết của một tài xế
        public override string ToString()
        {
            return GetInfo();
        }
    }
}
