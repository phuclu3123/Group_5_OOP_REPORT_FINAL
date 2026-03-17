using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class Driver : Staff, ITrackable
    {
        public string LicenseNumber { get; private set; }
        public string LicenseType { get; private set; }
        public DriverStatus DriverStatus { get; private set; }
        public GeoPoint CurrentLocation { get; private set; }
        
        // Association: 1 Driver co the lai nhieu Vehicle (1..*)
        public List<Infrastructure.Vehicle> AssignedVehicles { get; private set; }

        // Thuoc tinh de tinh luong
        public int DeliveryCount { get; private set; }
        public decimal BonusPerDelivery { get; private set; }
        public decimal FuelAllowance { get; private set; }

        public Driver(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                      string staffId, string department, decimal baseSalary, DateTime joinDate,
                      string licenseNumber, string licenseType)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.Driver, department, baseSalary, joinDate)
        {
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
            DriverStatus = DriverStatus.Available;
            CurrentLocation = new GeoPoint(0, 0);
            AssignedVehicles = new List<Infrastructure.Vehicle>();
            DeliveryCount = 0;
            BonusPerDelivery = 50000m;
            FuelAllowance = 1500000m;
        }

        public void AddAssignedVehicle(Infrastructure.Vehicle vehicle)
        {
            if (vehicle != null && !AssignedVehicles.Contains(vehicle))
            {
                AssignedVehicles.Add(vehicle);
            }
        }

        // Constructor khong tham so cho XML serialization
        public Driver() : base() { }

        // ===== POLYMORPHISM: Tinh luong tai xe =====
        // Luong = BaseSalary + (so chuyen x don gia/chuyen) + phu cap xang
        public override decimal CalculateSalary()
        {
            decimal deliveryBonus = DeliveryCount * BonusPerDelivery;
            decimal totalSalary = BaseSalary + deliveryBonus + FuelAllowance;
            return totalSalary;
        }

        public override string GetSalaryBreakdown()
        {
            decimal deliveryBonus = DeliveryCount * BonusPerDelivery;
            return "[Luong Tai Xe] " + FullName + "\n" +
                   "  Luong co ban:    " + BaseSalary.ToString("N0") + " VND\n" +
                   "  Thuong giao hang: " + DeliveryCount + " chuyen x " + BonusPerDelivery.ToString("N0") + " = " + deliveryBonus.ToString("N0") + " VND\n" +
                   "  Phu cap xang:    " + FuelAllowance.ToString("N0") + " VND\n" +
                   "  -------------------------\n" +
                   "  TONG LUONG:      " + CalculateSalary().ToString("N0") + " VND";
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            return DriverStatus.ToString();
        }

        public string GetTrackingInfo()
        {
            return "[Tracking Driver] " + FullName + " (" + StaffID + ")\n" +
                   "  Status: " + DriverStatus + " | Location: " + CurrentLocation;
        }

        // ===== DRIVER METHODS =====

        // Ghi nhan hoan thanh 1 chuyen giao hang
        public void RecordDelivery()
        {
            DeliveryCount++;
        }

        // Reset so chuyen (dau thang moi)
        public void ResetDeliveryCount()
        {
            DeliveryCount = 0;
        }

        // Cap nhat don gia thuong moi chuyen
        public void UpdateBonusPerDelivery(decimal newBonus)
        {
            BonusPerDelivery = newBonus;
        }

        // Cap nhat phu cap xang
        public void UpdateFuelAllowance(decimal newAllowance)
        {
            FuelAllowance = newAllowance;
        }

        // Cap nhat trang thai tai xe
        public void UpdateDriverStatus(DriverStatus newStatus)
        {
            DriverStatus = newStatus;
        }

        // Cap nhat vi tri hien tai
        public void UpdateCurrentLocation(GeoPoint newLocation)
        {
            CurrentLocation = newLocation;
        }

        // Bat dau chuyen giao hang
        public void StartDelivery()
        {
            DriverStatus = DriverStatus.Busy;
        }

        // Hoan thanh chuyen giao hang
        public void CompleteDelivery()
        {
            DriverStatus = DriverStatus.Available;
            RecordDelivery();
        }

        // Het ca lam viec
        public void GoOffDuty()
        {
            DriverStatus = DriverStatus.OffDuty;
        }

        // Kiem tra tai xe co san sang khong
        public bool IsAvailable()
        {
            return DriverStatus == DriverStatus.Available;
        }

        // Lay thong tin tai xe
        public override string GetInfo()
        {
            return "[Driver] ID: " + StaffID + " | Name: " + FullName + "\n" +
                   "  License: " + LicenseNumber + " (" + LicenseType + ")\n" +
                   "  Driver Status: " + DriverStatus + " | Location: " + CurrentLocation + "\n" +
                   "  Deliveries: " + DeliveryCount + " | Department: " + Department;
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
