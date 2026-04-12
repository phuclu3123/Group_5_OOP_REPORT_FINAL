using System;
using System.Collections.Generic;
using Logistic.Core.Models.Common;
using Logistic.Core.Interfaces;

namespace Logistic.Core.Models.Business
{
    // ============================================================
    // Lop Vehicle dai dien cho phuong tien van chuyen trong he thong logistics.
    // Implement ITrackable de theo doi trang thai va vi tri phuong tien.
    // Ho tro kiem tra suc chua (capacity check) truoc khi nhan don hang.
    // ============================================================
    public class Vehicle : ITrackable
    {
        // ===== PROPERTIES =====

        // Ma dinh danh phuong tien
        public string VehicleID { get; private set; }

        // Loai phuong tien (Motorbike, Van, Truck,...)
        public VehicleType VehicleType { get; private set; }

        // Suc chua toi da ve khoi luong (kg)
        public double MaxWeightCapacity { get; private set; }

        // Suc chua toi da ve the tich (m3)
        public double MaxVolumeCapacity { get; private set; }

        // Kich thuoc tong the cua phuong tien (DxRxC)
        public string Dimensions { get; private set; }

        // So km da chay (odometer)
        public double CurrentOdometer { get; private set; }

        // Phuong tien co dong lanh khong
        public bool IsRefrigerated { get; private set; }

        // Muc nhien lieu hien tai (0-100%)
        public double FuelLevel { get; private set; }

        // Trang thai hien tai cua phuong tien
        public VehicleStatus Status { get; private set; }

        // Association: 1 Vehicle co the duoc phan cong cho nhieu Driver lai (1..*)
        public List<Actors.Driver> AllowedDrivers { get; private set; }

        // Aggregation: Quan he Whole-Part. Engine co the ton tai doc lap voi Vehicle
        public Infrastructure.Engine VehicleEngine { get; private set; }

        // Danh sach ma don hang dang duoc cho tren xe
        public List<string> AssignedOrderIds { get; private set; }

        // Tong khoi luong hang dang cho tren xe (kg)
        public double CurrentLoadWeight { get; private set; }

        // Tong the tich hang dang cho tren xe (m3)
        public double CurrentLoadVolume { get; private set; }

        // ===== CONSTRUCTORS =====

        public Vehicle(string vehicleId, VehicleType vehicleType, double maxWeightCapacity,
                       double maxVolumeCapacity, string dimensions, bool isRefrigerated)
        {
            VehicleID = vehicleId;
            VehicleType = vehicleType;
            MaxWeightCapacity = maxWeightCapacity;
            MaxVolumeCapacity = maxVolumeCapacity;
            Dimensions = dimensions;
            IsRefrigerated = isRefrigerated;
            CurrentOdometer = 0;
            FuelLevel = 100;
            Status = VehicleStatus.Ready;
            AllowedDrivers = new List<Actors.Driver>();
            AssignedOrderIds = new List<string>();
            CurrentLoadWeight = 0;
            CurrentLoadVolume = 0;
        }

        // Constructor khong tham so cho serialization
        public Vehicle()
        {
            VehicleID = "";
            Dimensions = "";
            AllowedDrivers = new List<Actors.Driver>();
            AssignedOrderIds = new List<string>();
        }

        // ===== ASSOCIATION: Phan cong tai xe =====

        // Them tai xe vao danh sach duoc phep lai xe nay
        public void AddAllowedDriver(Actors.Driver driver)
        {
            if (driver == null)
            {
                return;
            }
            // Kiem tra trung lap truoc khi them
            for (int i = 0; i < AllowedDrivers.Count; i++)
            {
                if (AllowedDrivers[i].StaffID == driver.StaffID)
                {
                    return;
                }
            }
            AllowedDrivers.Add(driver);
        }

        // ===== AGGREGATION: Gan dong co =====

        // Gan dong co vao phuong tien (Aggregation - Part co the ton tai doc lap)
        public void InstallEngine(Infrastructure.Engine engine)
        {
            VehicleEngine = engine;
        }

        // ===== ITrackable =====

        // Lay trang thai hien tai cua phuong tien
        public string GetCurrentStatus()
        {
            return Status.ToString();
        }

        // Lay thong tin theo doi chi tiet cua phuong tien
        public string GetTrackingInfo()
        {
            string refrigeratedText = "No";
            if (IsRefrigerated)
            {
                refrigeratedText = "Yes";
            }
            return "[Tracking Vehicle] " + VehicleID + " (" + VehicleType + ")\n" +
                   "  Status: " + Status + " | Fuel: " + FuelLevel + "%" + "\n" +
                   "  Odometer: " + CurrentOdometer + "km | Refrigerated: " + refrigeratedText + "\n" +
                   "  Load: " + CurrentLoadWeight + "/" + MaxWeightCapacity + "kg" +
                   " | Volume: " + CurrentLoadVolume + "/" + MaxVolumeCapacity + "m3" +
                   " | Orders: " + AssignedOrderIds.Count;
        }

        // ===== CAPACITY CHECK METHODS =====

        // Kiem tra phuong tien co du suc chua cho khoi luong va the tich chi dinh
        public bool HasCapacityFor(double weight, double volume)
        {
            double remainingWeight = MaxWeightCapacity - CurrentLoadWeight;
            double remainingVolume = MaxVolumeCapacity - CurrentLoadVolume;
            return weight <= remainingWeight && volume <= remainingVolume;
        }

        // Lay khoi luong con lai co the cho
        public double GetRemainingWeight()
        {
            return MaxWeightCapacity - CurrentLoadWeight;
        }

        // Lay the tich con lai co the cho
        public double GetRemainingVolume()
        {
            return MaxVolumeCapacity - CurrentLoadVolume;
        }

        // Nap don hang len xe - kiem tra suc chua truoc khi nap
        public bool LoadOrder(string orderId, double weight, double volume)
        {
            if (!HasCapacityFor(weight, volume))
            {
                return false;
            }
            // Kiem tra don hang da duoc nap chua
            for (int i = 0; i < AssignedOrderIds.Count; i++)
            {
                if (AssignedOrderIds[i] == orderId)
                {
                    return false;
                }
            }
            AssignedOrderIds.Add(orderId);
            CurrentLoadWeight += weight;
            CurrentLoadVolume += volume;
            return true;
        }

        // Do don hang khoi xe
        public bool UnloadOrder(string orderId, double weight, double volume)
        {
            bool found = false;
            for (int i = 0; i < AssignedOrderIds.Count; i++)
            {
                if (AssignedOrderIds[i] == orderId)
                {
                    AssignedOrderIds.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return false;
            }
            CurrentLoadWeight -= weight;
            CurrentLoadVolume -= volume;
            // Dam bao khong bi am
            if (CurrentLoadWeight < 0)
            {
                CurrentLoadWeight = 0;
            }
            if (CurrentLoadVolume < 0)
            {
                CurrentLoadVolume = 0;
            }
            return true;
        }

        // Do toan bo hang khoi xe
        public void UnloadAll()
        {
            AssignedOrderIds.Clear();
            CurrentLoadWeight = 0;
            CurrentLoadVolume = 0;
        }

        // ===== VEHICLE METHODS =====

        // Cap nhat trang thai phuong tien
        public void UpdateStatus(VehicleStatus newStatus)
        {
            Status = newStatus;
        }

        // Cap nhat muc nhien lieu (gioi han 0-100%)
        public void UpdateFuelLevel(double newLevel)
        {
            if (newLevel < 0)
            {
                FuelLevel = 0;
            }
            else if (newLevel > 100)
            {
                FuelLevel = 100;
            }
            else
            {
                FuelLevel = newLevel;
            }
        }

        // Cap nhat so km da chay
        public void UpdateOdometer(double km)
        {
            if (km > 0)
            {
                CurrentOdometer += km;
            }
        }

        // Kiem tra phuong tien co the cho khoi luong chi dinh khong
        public bool CanCarry(double weight)
        {
            return weight <= MaxWeightCapacity && Status == VehicleStatus.Ready;
        }

        // Kiem tra phuong tien co san sang khong
        public bool IsAvailable()
        {
            return Status == VehicleStatus.Ready;
        }

        // Gui phuong tien di bao tri
        public void SendToMaintenance()
        {
            Status = VehicleStatus.Maintenance;
        }

        // Hoan tat bao tri, phuong tien san sang
        public void CompleteMaintenance()
        {
            Status = VehicleStatus.Ready;
        }

        // Lay thong tin chi tiet phuong tien
        public string GetVehicleInfo()
        {
            string refrigeratedText = "No";
            if (IsRefrigerated)
            {
                refrigeratedText = "Yes";
            }
            return "[Vehicle] ID: " + VehicleID + " | Type: " + VehicleType + "\n" +
                   "  Max Weight: " + MaxWeightCapacity + "kg | Max Volume: " + MaxVolumeCapacity + "m3 | Dim: " + Dimensions + "\n" +
                   "  Current Load: " + CurrentLoadWeight + "kg / " + CurrentLoadVolume + "m3\n" +
                   "  Odometer: " + CurrentOdometer + "km | Fuel: " + FuelLevel + "%\n" +
                   "  Refrigerated: " + refrigeratedText + " | Status: " + Status + "\n" +
                   "  Assigned Orders: " + AssignedOrderIds.Count;
        }

        public override string ToString()
        {
            return GetVehicleInfo();
        }
    }
}
