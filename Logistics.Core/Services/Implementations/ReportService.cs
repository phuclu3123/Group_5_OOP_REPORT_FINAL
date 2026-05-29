using System;
using System.Collections.Generic;
using System.Text;
using Logistics.Core.Configuration;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    /// <summary>
    /// Service tạo báo cáo và thống kê cho Dashboard.
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly DriverRepository _driverRepo;
        private readonly DispatcherRepository _dispatcherRepo;
        private readonly WarehouseStaffRepository _warehouseRepoStaff;
        private readonly AdminRepository _adminRepo;
        private readonly VehicleRepository _vehicleRepo;
        private readonly CustomerRepository _customerRepo;
        private readonly WarehouseRepository _warehouseRepo;
        private readonly BusinessRules _businessRules;

        public ReportService(
            IRepository<Order> orderRepository,
            DriverRepository driverRepo,
            DispatcherRepository dispatcherRepo,
            WarehouseStaffRepository warehouseRepoStaff,
            AdminRepository adminRepo,
            VehicleRepository vehicleRepo,
            CustomerRepository customerRepo,
            WarehouseRepository warehouseRepo,
            BusinessRules? businessRules = null)
        {
            _orderRepository = orderRepository;
            _driverRepo      = driverRepo;
            _dispatcherRepo  = dispatcherRepo;
            _warehouseRepoStaff = warehouseRepoStaff;
            _adminRepo       = adminRepo;
            _vehicleRepo     = vehicleRepo;
            _customerRepo    = customerRepo;
            _warehouseRepo   = warehouseRepo;
            _businessRules   = businessRules ?? new BusinessRules();
        }

        // ─── HOÁ ĐƠN ─────────────────────────────────────────────────────────
        public string GenerateInvoice(string orderId)
        {
            List<Order> orders = _orderRepository.GetAll();
            Order? order = null;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null && orders[i].TrackingNumber == orderId)
                {
                    order = orders[i];
                    break;
                }
            }

            if (order == null) return "Không tìm thấy đơn hàng: " + orderId;

            if (UseProfessionalInvoice())
            {
                return BuildProfessionalInvoice(order);
            }

            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("CONG TY TNHH LOGISTICS SYSTEM");
            invoice.AppendLine("Dia chi: 01 Nguyen Van Cu, Quan 5, TP.HCM");
            invoice.AppendLine("Dien thoai: 028 0000 0000    Email: support@logistics.local");
            invoice.AppendLine("Ma so thue: 0312345678");
            invoice.AppendLine("============================================================");
            invoice.AppendLine("                    HOA DON DICH VU VAN CHUYEN");
            invoice.AppendLine("============================================================");
            invoice.AppendLine("So hoa don: INV-" + order.TrackingNumber);
            invoice.AppendLine("Ma van don: " + order.TrackingNumber);
            invoice.AppendLine("Ngay lap: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            invoice.AppendLine("Ngay tao don: " + order.CreatedDate.ToString("dd/MM/yyyy HH:mm"));
            invoice.AppendLine();
            invoice.AppendLine("BEN GUI");
            invoice.AppendLine("  Ma khach hang: " + order.SenderID);
            if (order.Route != null && order.Route.PickupAddress != null)
            {
                invoice.AppendLine("  Dia chi lay hang: " + order.Route.PickupAddress.ToString());
            }
            invoice.AppendLine();
            invoice.AppendLine("BEN NHAN");
            invoice.AppendLine("  Ma khach hang: " + order.ReceiverID);
            if (order.Route != null && order.Route.DeliveryAddress != null)
            {
                invoice.AppendLine("  Dia chi giao hang: " + order.Route.DeliveryAddress.ToString());
            }
            invoice.AppendLine();

            double totalWeight = 0;
            if (order.Packages != null)
            {
                invoice.AppendLine("CHI TIET KIEN HANG");
                invoice.AppendLine("------------------------------------------------------------");
                for (int i = 0; i < order.Packages.Count; i++)
                {
                    if (order.Packages[i] != null)
                    {
                        invoice.AppendLine("  " + (i + 1) + ". " + order.Packages[i].Description);
                        invoice.AppendLine("     Khoi luong: " + order.Packages[i].ActualWeight + " kg");
                        invoice.AppendLine("     Gia tri khai bao: " + order.Packages[i].Value.ToString("N0") + " VND");
                        totalWeight += order.Packages[i].ActualWeight;
                    }
                }
            }

            invoice.AppendLine("------------------------------------------------------------");
            invoice.AppendLine("Loai dich vu: " + order.ServiceType);
            invoice.AppendLine("Tong khoi luong tinh cuoc: " + totalWeight.ToString("N2") + " kg");
            invoice.AppendLine("Cuoc van chuyen: " + order.TotalCost.ToString("N0") + " VND");
            invoice.AppendLine("Thue VAT: 0 VND");
            invoice.AppendLine("TONG THANH TOAN: " + order.TotalCost.ToString("N0") + " VND");
            invoice.AppendLine("Trang thai don: " + order.CurrentStatus.ToString());
            invoice.AppendLine();
            invoice.AppendLine("Dieu khoan: Hang hoa duoc van chuyen theo thong tin khach hang cung cap.");
            invoice.AppendLine("Khach hang vui long kiem tra tinh trang kien hang khi nhan.");
            invoice.AppendLine();
            invoice.AppendLine("Nguoi lap phieu                 Khach hang                 Ke toan");
            invoice.AppendLine();
            invoice.AppendLine("................          ................          ................");
            invoice.AppendLine("============================================================");

            return invoice.ToString();
        }

        private static bool UseProfessionalInvoice()
        {
            return true;
        }

        private string BuildProfessionalInvoice(Order order)
        {
            Models.Actors.Customer sender = _customerRepo.GetById(order.SenderID);
            Models.Actors.Customer receiver = _customerRepo.GetById(order.ReceiverID);
            CustomerType customerType = sender != null ? sender.CustomerType : CustomerType.Standard;
            decimal discountRate = _businessRules.GetDiscountRate(customerType);
            decimal discountAmount = _businessRules.CalculateDiscount(order.TotalCost, customerType);
            decimal vatAmount = _businessRules.CalculateVat(order.TotalCost - discountAmount);
            decimal payableAmount = order.TotalCost - discountAmount + vatAmount;
            int earnedPoints = _businessRules.CalculateEarnedPoints(payableAmount, customerType);
            double totalWeight = 0;

            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("CONG TY TNHH LOGISTICS SYSTEM".PadRight(48) + "MST: 0312345678");
            invoice.AppendLine("Dia chi: 01 Nguyen Van Cu, Quan 5, TP.HCM");
            invoice.AppendLine("Dien thoai: 028 0000 0000 | Email: support@logistics.local");
            invoice.AppendLine("Website: logistics-system.local");
            invoice.AppendLine(new string('=', 86));
            invoice.AppendLine(CenterText("HOA DON DICH VU VAN CHUYEN", 86));
            invoice.AppendLine(CenterText("INVOICE / TRANSPORT SERVICE BILL", 86));
            invoice.AppendLine(new string('=', 86));
            invoice.AppendLine("So hoa don : INV-" + order.TrackingNumber);
            invoice.AppendLine("Ma van don : " + order.TrackingNumber);
            invoice.AppendLine("Ngay lap   : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            invoice.AppendLine("Ngay tao   : " + order.CreatedDate.ToString("dd/MM/yyyy HH:mm"));
            invoice.AppendLine();
            invoice.AppendLine("BEN GUI / SHIPPER");
            invoice.AppendLine("  Ma khach hang : " + order.SenderID);
            invoice.AppendLine("  Ten khach hang: " + (sender != null ? sender.FullName : "Khach hang chua khai bao"));
            invoice.AppendLine("  Hang khach    : " + customerType + " | Uu dai: " + discountRate.ToString("P0"));
            invoice.AppendLine("BEN NHAN / CONSIGNEE");
            invoice.AppendLine("  Ma khach hang : " + order.ReceiverID);
            invoice.AppendLine("  Ten khach hang: " + (receiver != null ? receiver.FullName : "Khach hang chua khai bao"));
            invoice.AppendLine();
            invoice.AppendLine("CHI TIET KIEN HANG");
            invoice.AppendLine(new string('-', 86));
            invoice.AppendLine("STT  Ma kien".PadRight(20) + "Mo ta".PadRight(28) + "KL(kg)".PadLeft(10) + "Gia tri".PadLeft(18));
            invoice.AppendLine(new string('-', 86));
            if (order.Packages != null)
            {
                for (int i = 0; i < order.Packages.Count; i++)
                {
                    if (order.Packages[i] != null)
                    {
                        string description = order.Packages[i].Description;
                        if (description.Length > 25) description = description.Substring(0, 25);
                        invoice.AppendLine((i + 1).ToString().PadRight(5) +
                                           order.Packages[i].PackageID.PadRight(15) +
                                           description.PadRight(28) +
                                           order.Packages[i].ActualWeight.ToString("N2").PadLeft(10) +
                                           order.Packages[i].Value.ToString("N0").PadLeft(18));
                        totalWeight += order.Packages[i].ActualWeight;
                    }
                }
            }

            invoice.AppendLine(new string('-', 86));
            invoice.AppendLine("Loai dich vu".PadRight(34) + ": " + order.ServiceType);
            invoice.AppendLine("Tong khoi luong tinh cuoc".PadRight(34) + ": " + totalWeight.ToString("N2") + " kg");
            invoice.AppendLine("Cuoc van chuyen".PadRight(34) + ": " + order.TotalCost.ToString("N0").PadLeft(16) + " VND");
            invoice.AppendLine(("Chiet khau " + discountRate.ToString("P0")).PadRight(34) + ": " + discountAmount.ToString("N0").PadLeft(16) + " VND");
            invoice.AppendLine("VAT 8%".PadRight(34) + ": " + vatAmount.ToString("N0").PadLeft(16) + " VND");
            invoice.AppendLine(new string('-', 86));
            invoice.AppendLine("TONG THANH TOAN".PadRight(34) + ": " + payableAmount.ToString("N0").PadLeft(16) + " VND");
            invoice.AppendLine("Diem du kien cong".PadRight(34) + ": " + earnedPoints.ToString("N0") + " diem");
            invoice.AppendLine();
            invoice.AppendLine("Nguoi lap phieu".PadRight(28) + "Khach hang".PadRight(28) + "Ke toan");
            invoice.AppendLine();
            invoice.AppendLine("................".PadRight(28) + "................".PadRight(28) + "................");
            invoice.AppendLine(new string('=', 86));
            return invoice.ToString();
        }

        private static string CenterText(string text, int width)
        {
            if (text.Length >= width) return text;
            int left = (width - text.Length) / 2;
            return new string(' ', left) + text;
        }

        // ─── THỐNG KÊ THÁNG ─────────────────────────────────────────────────
        /// <summary>
        /// Trả về thống kê tổng hợp theo tháng bao gồm đơn hàng và tài nguyên.
        /// </summary>
        public DashboardStatisticsDTO GetMonthlyStatistics(int year, int month)
        {
            List<Order> allOrders = _orderRepository.GetAll();

            int totalOrders   = 0;
            int pending       = 0;
            int inTransit     = 0;
            int delivered     = 0;
            int cancelled     = 0;
            int failed        = 0;
            int returned      = 0;
            decimal totalRevenue = 0;

            for (int i = 0; i < allOrders.Count; i++)
            {
                Order o = allOrders[i];
                if (o == null) continue;
                if (o.CreatedDate.Year != year || o.CreatedDate.Month != month) continue;

                totalOrders++;
                totalRevenue += o.TotalCost;

                switch (o.CurrentStatus)
                {
                    case OrderStatus.Pending:    pending++;   break;
                    case OrderStatus.InTransit:  inTransit++; break;
                    case OrderStatus.Delivered:  delivered++; break;
                    case OrderStatus.Cancelled:  cancelled++; break;
                    case OrderStatus.Failed:     failed++;    break;
                    case OrderStatus.Returned:   returned++;  break;
                }
            }

            // Thống kê tài nguyên
            List<Models.Actors.Driver> allDrivers = _driverRepo.GetAll();
            int availableDrivers = 0;
            for (int i = 0; i < allDrivers.Count; i++)
            {
                if (allDrivers[i] != null && allDrivers[i].IsAvailable())
                    availableDrivers++;
            }

            List<Models.Infrastructure.Vehicle> allVehicles = _vehicleRepo.GetAll();
            int availableVehicles = 0;
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] != null && allVehicles[i].IsAvailable())
                    availableVehicles++;
            }

            DashboardStatisticsDTO stats = new DashboardStatisticsDTO();
            stats.Year             = year;
            stats.Month            = month;
            stats.TotalOrders      = totalOrders;
            stats.PendingOrders    = pending;
            stats.InTransitOrders  = inTransit;
            stats.DeliveredOrders  = delivered;
            stats.CancelledOrders  = cancelled;
            stats.FailedOrders     = failed;
            stats.ReturnedOrders   = returned;
            stats.TotalRevenue     = totalRevenue;
            stats.SuccessRate      = totalOrders > 0 ? ((decimal)delivered / totalOrders) * 100m : 0;
            stats.TotalDrivers     = allDrivers.Count;
            stats.TotalStaff       = allDrivers.Count + _dispatcherRepo.Count() + _warehouseRepoStaff.Count() + _adminRepo.Count();
            stats.AvailableDrivers = availableDrivers;
            stats.TotalVehicles    = allVehicles.Count;
            stats.AvailableVehicles = availableVehicles;
            stats.TotalCustomers   = _customerRepo.Count();
            stats.TotalWarehouses  = _warehouseRepo.Count();

            return stats;
        }

        // ─── THỐNG KÊ TOÀN BỘ (không lọc tháng) ─────────────────────────────
        /// <summary>
        /// Thống kê tổng hợp toàn bộ dữ liệu (dùng cho Dashboard tổng quan).
        /// </summary>
        public DashboardStatisticsDTO GetOverallStatistics()
        {
            return GetAllOrderStatistics(); // Gọi đúng method nội bộ thay vì GetMonthlyStatistics(0, 0)
        }

        // Override để bỏ qua year/month filter khi cần thống kê toàn bộ
        private DashboardStatisticsDTO GetAllOrderStatistics()
        {
            List<Order> allOrders = _orderRepository.GetAll();

            int delivered = 0;
            int failed    = 0;
            int pending   = 0;
            int inTransit = 0;
            int cancelled = 0;
            int returned  = 0;
            decimal totalRevenue = 0;

            for (int i = 0; i < allOrders.Count; i++)
            {
                Order o = allOrders[i];
                if (o == null) continue;
                totalRevenue += o.TotalCost;
                switch (o.CurrentStatus)
                {
                    case OrderStatus.Pending:    pending++;   break;
                    case OrderStatus.InTransit:  inTransit++; break;
                    case OrderStatus.Delivered:  delivered++; break;
                    case OrderStatus.Cancelled:  cancelled++; break;
                    case OrderStatus.Failed:     failed++;    break;
                    case OrderStatus.Returned:   returned++;  break;
                }
            }

            List<Models.Actors.Driver> allDrivers = _driverRepo.GetAll();
            int availableDrivers = 0;
            for (int i = 0; i < allDrivers.Count; i++)
            {
                if (allDrivers[i] != null && allDrivers[i].IsAvailable())
                    availableDrivers++;
            }

            List<Models.Infrastructure.Vehicle> allVehicles = _vehicleRepo.GetAll();
            int availableVehicles = 0;
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] != null && allVehicles[i].IsAvailable())
                    availableVehicles++;
            }

            DashboardStatisticsDTO stats = new DashboardStatisticsDTO();
            stats.TotalOrders       = allOrders.Count;
            stats.PendingOrders     = pending;
            stats.InTransitOrders   = inTransit;
            stats.DeliveredOrders   = delivered;
            stats.CancelledOrders   = cancelled;
            stats.FailedOrders      = failed;
            stats.ReturnedOrders    = returned;
            stats.TotalRevenue      = totalRevenue;
            stats.SuccessRate       = allOrders.Count > 0 ? ((decimal)delivered / allOrders.Count) * 100m : 0;
            stats.TotalDrivers      = allDrivers.Count;
            stats.TotalStaff        = allDrivers.Count + _dispatcherRepo.Count() + _warehouseRepoStaff.Count() + _adminRepo.Count();
            stats.AvailableDrivers  = availableDrivers;
            stats.TotalVehicles     = allVehicles.Count;
            stats.AvailableVehicles = availableVehicles;
            stats.TotalCustomers    = _customerRepo.Count();
            stats.TotalWarehouses   = _warehouseRepo.Count();

            return stats;
        }
    }
}
