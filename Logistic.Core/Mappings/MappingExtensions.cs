using System;
using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Mappings
{
    // ============================================================
    // THAY THE AUTOMAPPER: Mapping thu cong giua Model va DTO
    // Su dung static methods de chuyen doi du lieu giua cac tang.
    // Kem theo InMemoryDataStore (Singleton) de luu du lieu in-memory.
    // ============================================================

    // ===== DTO CLASSES =====
    // Data Transfer Objects - doi tuong trung gian de truyen du lieu giua cac tang
    // Chi chua du lieu, khong chua logic xu ly

    // DTO cho Order (don hang)
    public class OrderDTO
    {
        public string TrackingNumber { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ServiceType { get; set; }
        public string CurrentStatus { get; set; }
        public double TotalWeight { get; set; }
        public decimal TotalCost { get; set; }
        public string CreatedDate { get; set; }
        public string AssignedDriverID { get; set; }
        public int PackageCount { get; set; }
        public int StatusHistoryCount { get; set; }

        public OrderDTO()
        {
            TrackingNumber = "";
            SenderID = "";
            ReceiverID = "";
            PickupAddress = "";
            DeliveryAddress = "";
            ServiceType = "";
            CurrentStatus = "";
            CreatedDate = "";
            AssignedDriverID = "";
        }
    }

    // DTO cho Vehicle (phuong tien)
    public class VehicleDTO
    {
        public string VehicleID { get; set; }
        public string VehicleType { get; set; }
        public double MaxWeightCapacity { get; set; }
        public double MaxVolumeCapacity { get; set; }
        public double CurrentLoadWeight { get; set; }
        public double CurrentLoadVolume { get; set; }
        public string Status { get; set; }
        public double FuelLevel { get; set; }
        public int AssignedOrderCount { get; set; }

        public VehicleDTO()
        {
            VehicleID = "";
            VehicleType = "";
            Status = "";
        }
    }

    // DTO cho Package (goi hang)
    public class PackageDTO
    {
        public string PackageID { get; set; }
        public string OrderID { get; set; }
        public string Description { get; set; }
        public double ActualWeight { get; set; }
        public string Dimensions { get; set; }
        public bool IsFragile { get; set; }

        public PackageDTO()
        {
            PackageID = "";
            OrderID = "";
            Description = "";
            Dimensions = "";
        }
    }

    // ============================================================
    // STATIC CLASS: Cac phuong thuc mapping thu cong (thay AutoMapper)
    // Chuyen doi giua Model <-> DTO bang cach gan tung property.
    // ============================================================
    public static class MappingExtensions
    {
        // ===== ORDER MAPPING =====

        // Chuyen doi Order Model sang OrderDTO
        public static OrderDTO ToDTO(Order order)
        {
            if (order == null)
            {
                return null;
            }
            OrderDTO dto = new OrderDTO();
            dto.TrackingNumber = order.TrackingNumber;
            dto.SenderID = order.SenderID;
            dto.ReceiverID = order.ReceiverID;
            dto.PickupAddress = order.PickupAddress;
            dto.DeliveryAddress = order.DeliveryAddress;
            dto.ServiceType = order.ServiceType.ToString();
            dto.CurrentStatus = order.CurrentStatus.ToString();
            dto.TotalWeight = order.TotalWeight;
            dto.TotalCost = order.TotalCost;
            dto.CreatedDate = order.CreatedDate.ToString("dd/MM/yyyy HH:mm");
            dto.AssignedDriverID = order.AssignedDriverID;
            dto.PackageCount = order.Packages.Count;
            dto.StatusHistoryCount = order.StatusHistories.Count;
            return dto;
        }

        // Chuyen doi danh sach Order sang danh sach OrderDTO
        public static List<OrderDTO> ToDTOList(List<Order> orders)
        {
            List<OrderDTO> dtoList = new List<OrderDTO>();
            for (int i = 0; i < orders.Count; i++)
            {
                dtoList.Add(ToDTO(orders[i]));
            }
            return dtoList;
        }

        // Cap nhat Order tu OrderDTO (chi cap nhat cac truong cho phep)
        public static void UpdateFromDTO(Order order, OrderDTO dto)
        {
            if (order == null || dto == null)
            {
                return;
            }
            // Chi cho phep cap nhat dia chi giao hang (cac truong khac dung method rieng)
            if (!string.IsNullOrEmpty(dto.DeliveryAddress))
            {
                order.UpdateDeliveryAddress(dto.DeliveryAddress);
            }
        }

        // ===== VEHICLE MAPPING =====

        // Chuyen doi Vehicle Model sang VehicleDTO
        public static VehicleDTO ToDTO(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return null;
            }
            VehicleDTO dto = new VehicleDTO();
            dto.VehicleID = vehicle.VehicleID;
            dto.VehicleType = vehicle.VehicleType.ToString();
            dto.MaxWeightCapacity = vehicle.MaxWeightCapacity;
            dto.MaxVolumeCapacity = vehicle.MaxVolumeCapacity;
            dto.CurrentLoadWeight = vehicle.CurrentLoadWeight;
            dto.CurrentLoadVolume = vehicle.CurrentLoadVolume;
            dto.Status = vehicle.GetCurrentStatus();
            dto.FuelLevel = vehicle.FuelLevel;
            dto.AssignedOrderCount = vehicle.AssignedOrderIds.Count;
            return dto;
        }

        // Chuyen doi danh sach Vehicle sang danh sach VehicleDTO
        public static List<VehicleDTO> ToDTOList(List<Vehicle> vehicles)
        {
            List<VehicleDTO> dtoList = new List<VehicleDTO>();
            for (int i = 0; i < vehicles.Count; i++)
            {
                dtoList.Add(ToDTO(vehicles[i]));
            }
            return dtoList;
        }

        // ===== PACKAGE MAPPING =====

        // Chuyen doi Package Model sang PackageDTO
        public static PackageDTO ToDTO(Package package)
        {
            if (package == null)
            {
                return null;
            }
            PackageDTO dto = new PackageDTO();
            dto.PackageID = package.PackageID;
            dto.OrderID = package.OrderID;
            dto.Description = package.Description;
            dto.ActualWeight = package.ActualWeight;
            dto.Dimensions = package.Dimensions;
            dto.IsFragile = package.IsFragile;
            return dto;
        }

        // Chuyen doi danh sach Package sang danh sach PackageDTO
        public static List<PackageDTO> ToDTOList(List<Package> packages)
        {
            List<PackageDTO> dtoList = new List<PackageDTO>();
            for (int i = 0; i < packages.Count; i++)
            {
                dtoList.Add(ToDTO(packages[i]));
            }
            return dtoList;
        }
    }

    // ============================================================
    // SINGLETON PATTERN: Luu tru du lieu in-memory
    // Dam bao chi co 1 instance duy nhat trong toan bo ung dung.
    // Dong vai tro nhu cache/store de cac Service truy cap chung.
    // ============================================================
    public class InMemoryDataStore
    {
        // ===== SINGLETON =====

        // Instance duy nhat (private static)
        private static InMemoryDataStore _instance;

        // Lock object de dam bao thread-safe khi khoi tao
        private static readonly object _lock = new object();

        // Cac kho du lieu in-memory
        private List<Order> _orders;
        private List<Vehicle> _vehicles;
        private List<Package> _packages;

        // Constructor private: khong cho phep tao tu ben ngoai
        private InMemoryDataStore()
        {
            _orders = new List<Order>();
            _vehicles = new List<Vehicle>();
            _packages = new List<Package>();
        }

        // Lay instance duy nhat (Singleton)
        // Su dung double-check locking de dam bao thread-safe
        public static InMemoryDataStore GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new InMemoryDataStore();
                    }
                }
            }
            return _instance;
        }

        // ===== ORDER OPERATIONS =====

        // Them don hang vao kho
        public void AddOrder(Order order)
        {
            if (order == null)
            {
                return;
            }
            // Kiem tra trung lap truoc khi them
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].TrackingNumber == order.TrackingNumber)
                {
                    return;
                }
            }
            _orders.Add(order);
        }

        // Tim don hang theo ma tracking
        public Order GetOrderByTracking(string trackingNumber)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].TrackingNumber == trackingNumber)
                {
                    return _orders[i];
                }
            }
            return null;
        }

        // Lay toan bo don hang
        public List<Order> GetAllOrders()
        {
            // Tra ve ban sao de bao ve du lieu goc (Encapsulation)
            List<Order> copy = new List<Order>();
            for (int i = 0; i < _orders.Count; i++)
            {
                copy.Add(_orders[i]);
            }
            return copy;
        }

        // Xoa don hang theo ma tracking
        public bool RemoveOrder(string trackingNumber)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].TrackingNumber == trackingNumber)
                {
                    _orders.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // Lay so luong don hang
        public int GetOrderCount()
        {
            return _orders.Count;
        }

        // ===== VEHICLE OPERATIONS =====

        // Them phuong tien vao kho
        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return;
            }
            for (int i = 0; i < _vehicles.Count; i++)
            {
                if (_vehicles[i].VehicleID == vehicle.VehicleID)
                {
                    return;
                }
            }
            _vehicles.Add(vehicle);
        }

        // Tim phuong tien theo ID
        public Vehicle GetVehicleById(string vehicleId)
        {
            for (int i = 0; i < _vehicles.Count; i++)
            {
                if (_vehicles[i].VehicleID == vehicleId)
                {
                    return _vehicles[i];
                }
            }
            return null;
        }

        // Lay toan bo phuong tien
        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> copy = new List<Vehicle>();
            for (int i = 0; i < _vehicles.Count; i++)
            {
                copy.Add(_vehicles[i]);
            }
            return copy;
        }

        // Lay so luong phuong tien
        public int GetVehicleCount()
        {
            return _vehicles.Count;
        }

        // ===== PACKAGE OPERATIONS =====

        // Them goi hang vao kho
        public void AddPackage(Package package)
        {
            if (package == null)
            {
                return;
            }
            _packages.Add(package);
        }

        // Lay toan bo goi hang
        public List<Package> GetAllPackages()
        {
            List<Package> copy = new List<Package>();
            for (int i = 0; i < _packages.Count; i++)
            {
                copy.Add(_packages[i]);
            }
            return copy;
        }

        // ===== UTILITY =====

        // Xoa toan bo du lieu (dung cho testing)
        public void ClearAll()
        {
            _orders.Clear();
            _vehicles.Clear();
            _packages.Clear();
        }
    }
}
