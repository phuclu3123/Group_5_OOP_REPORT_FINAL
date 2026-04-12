using System;
using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;
using Logistic.Core.Mappings;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu bao cao va thong ke (Report Service)
    // Tao hoa don chi tiet, thong ke theo ngay/thang,
    // phan tich doanh thu theo loai dich vu.
    // Su dung InMemoryDataStore (Singleton) de truy cap du lieu.
    // ============================================================
    public class ReportService
    {
        // ===== DEPENDENCIES =====

        // Kho du lieu in-memory (Singleton)
        private InMemoryDataStore _dataStore;

        // ===== CONSTRUCTOR =====
        public ReportService()
        {
            _dataStore = InMemoryDataStore.GetInstance();
        }

        // ===== INVOICE =====

        // Tao hoa don chi tiet cho don hang
        public string GenerateInvoice(string trackingNumber)
        {
            Order order = _dataStore.GetOrderByTracking(trackingNumber);
            if (order == null)
            {
                return "Khong tim thay don hang: " + trackingNumber;
            }

            string invoice = "";
            invoice += "╔══════════════════════════════════════╗\n";
            invoice += "║          HOA DON VAN CHUYEN          ║\n";
            invoice += "╠══════════════════════════════════════╣\n";
            invoice += "║  Ma tracking: " + PadRight(order.TrackingNumber, 22) + "║\n";
            invoice += "║  Ngay tao:    " + PadRight(order.CreatedDate.ToString("dd/MM/yyyy HH:mm"), 22) + "║\n";
            invoice += "╠══════════════════════════════════════╣\n";
            invoice += "║  NGUOI GUI                           ║\n";
            invoice += "║  ID: " + PadRight(order.SenderID, 31) + "║\n";
            invoice += "║  Dia chi: " + PadRight(TruncateString(order.PickupAddress, 26), 26) + "║\n";
            invoice += "╠══════════════════════════════════════╣\n";
            invoice += "║  NGUOI NHAN                          ║\n";
            invoice += "║  ID: " + PadRight(order.ReceiverID, 31) + "║\n";
            invoice += "║  Dia chi: " + PadRight(TruncateString(order.DeliveryAddress, 26), 26) + "║\n";
            invoice += "╠══════════════════════════════════════╣\n";
            invoice += "║  CHI TIET DON HANG                   ║\n";
            invoice += "║  Dich vu:    " + PadRight(order.ServiceType.ToString(), 23) + "║\n";
            invoice += "║  So goi:     " + PadRight(order.Packages.Count.ToString(), 23) + "║\n";
            invoice += "║  Tong KL:    " + PadRight(order.TotalWeight + " kg", 23) + "║\n";
            invoice += "║  Trang thai: " + PadRight(order.CurrentStatus.ToString(), 23) + "║\n";

            // In danh sach chi tiet don hang
            if (order.Details.Count > 0)
            {
                invoice += "╠══════════════════════════════════════╣\n";
                invoice += "║  DANH SACH SAN PHAM                  ║\n";
                for (int i = 0; i < order.Details.Count; i++)
                {
                    OrderDetail detail = order.Details[i];
                    string detailLine = "  " + detail.ProductName + " x" + detail.Quantity +
                                        " = " + detail.SubTotal.ToString("N0");
                    invoice += "║" + PadRight(detailLine, 38) + "║\n";
                }
            }

            invoice += "╠══════════════════════════════════════╣\n";
            invoice += "║  TONG CONG: " + PadRight(order.TotalCost.ToString("N0") + " VND", 24) + "║\n";

            if (!string.IsNullOrEmpty(order.AssignedDriverID))
            {
                invoice += "║  Tai xe:    " + PadRight(order.AssignedDriverID, 24) + "║\n";
            }

            invoice += "╚══════════════════════════════════════╝";
            return invoice;
        }

        // ===== STATISTICS =====

        // Thong ke theo ngay: so don, doanh thu, ty le giao thanh cong
        public string GetDailyStatistics(DateTime date)
        {
            List<Order> allOrders = _dataStore.GetAllOrders();
            int totalOrders = 0;
            int deliveredOrders = 0;
            int cancelledOrders = 0;
            decimal totalRevenue = 0;

            for (int i = 0; i < allOrders.Count; i++)
            {
                Order order = allOrders[i];
                if (order.CreatedDate.Date == date.Date)
                {
                    totalOrders++;
                    if (order.CurrentStatus == OrderStatus.Delivered)
                    {
                        deliveredOrders++;
                        totalRevenue += order.TotalCost;
                    }
                    else if (order.CurrentStatus == OrderStatus.Cancelled)
                    {
                        cancelledOrders++;
                    }
                }
            }

            // Tinh ty le giao thanh cong
            double successRate = 0;
            if (totalOrders > 0)
            {
                successRate = ((double)deliveredOrders / totalOrders) * 100;
            }

            string report = "========== THONG KE NGAY " + date.ToString("dd/MM/yyyy") + " ==========\n";
            report += "  Tong don hang:        " + totalOrders + "\n";
            report += "  Da giao thanh cong:   " + deliveredOrders + "\n";
            report += "  Da huy:               " + cancelledOrders + "\n";
            report += "  Dang xu ly:           " + (totalOrders - deliveredOrders - cancelledOrders) + "\n";
            report += "  Ty le thanh cong:     " + Math.Round(successRate, 1) + "%\n";
            report += "  Tong doanh thu:       " + totalRevenue.ToString("N0") + " VND\n";
            report += "================================================";
            return report;
        }

        // Thong ke theo thang
        public string GetMonthlyStatistics(int month, int year)
        {
            List<Order> allOrders = _dataStore.GetAllOrders();
            int totalOrders = 0;
            int deliveredOrders = 0;
            int cancelledOrders = 0;
            decimal totalRevenue = 0;

            for (int i = 0; i < allOrders.Count; i++)
            {
                Order order = allOrders[i];
                if (order.CreatedDate.Month == month && order.CreatedDate.Year == year)
                {
                    totalOrders++;
                    if (order.CurrentStatus == OrderStatus.Delivered)
                    {
                        deliveredOrders++;
                        totalRevenue += order.TotalCost;
                    }
                    else if (order.CurrentStatus == OrderStatus.Cancelled)
                    {
                        cancelledOrders++;
                    }
                }
            }

            double successRate = 0;
            if (totalOrders > 0)
            {
                successRate = ((double)deliveredOrders / totalOrders) * 100;
            }

            string report = "========== THONG KE THANG " + month + "/" + year + " ==========\n";
            report += "  Tong don hang:        " + totalOrders + "\n";
            report += "  Da giao thanh cong:   " + deliveredOrders + "\n";
            report += "  Da huy:               " + cancelledOrders + "\n";
            report += "  Dang xu ly:           " + (totalOrders - deliveredOrders - cancelledOrders) + "\n";
            report += "  Ty le thanh cong:     " + Math.Round(successRate, 1) + "%\n";
            report += "  Tong doanh thu:       " + totalRevenue.ToString("N0") + " VND\n";
            report += "================================================";
            return report;
        }

        // Thong ke doanh thu theo loai dich vu
        public string GetRevenueByServiceType()
        {
            List<Order> allOrders = _dataStore.GetAllOrders();

            // Dem theo loai dich vu
            int standardCount = 0;
            int expressCount = 0;
            int instantCount = 0;
            decimal standardRevenue = 0;
            decimal expressRevenue = 0;
            decimal instantRevenue = 0;

            for (int i = 0; i < allOrders.Count; i++)
            {
                Order order = allOrders[i];
                if (order.CurrentStatus == OrderStatus.Delivered)
                {
                    if (order.ServiceType == ServiceType.Standard)
                    {
                        standardCount++;
                        standardRevenue += order.TotalCost;
                    }
                    else if (order.ServiceType == ServiceType.Express)
                    {
                        expressCount++;
                        expressRevenue += order.TotalCost;
                    }
                    else if (order.ServiceType == ServiceType.Instant)
                    {
                        instantCount++;
                        instantRevenue += order.TotalCost;
                    }
                }
            }

            decimal totalRevenue = standardRevenue + expressRevenue + instantRevenue;

            string report = "========== DOANH THU THEO DICH VU ==========\n";
            report += "  Standard: " + standardCount + " don | " + standardRevenue.ToString("N0") + " VND\n";
            report += "  Express:  " + expressCount + " don | " + expressRevenue.ToString("N0") + " VND\n";
            report += "  Instant:  " + instantCount + " don | " + instantRevenue.ToString("N0") + " VND\n";
            report += "  -----------------------------------------\n";
            report += "  TONG:     " + (standardCount + expressCount + instantCount) + " don | " + totalRevenue.ToString("N0") + " VND\n";
            report += "=============================================";
            return report;
        }

        // ===== PRIVATE HELPERS =====

        // Them khoang trang phia sau chuoi de can le (pad right)
        private string PadRight(string text, int totalWidth)
        {
            if (text == null)
            {
                text = "";
            }
            if (text.Length >= totalWidth)
            {
                return text.Substring(0, totalWidth);
            }
            string padded = text;
            for (int i = text.Length; i < totalWidth; i++)
            {
                padded += " ";
            }
            return padded;
        }

        // Cat chuoi neu qua dai va them "..."
        private string TruncateString(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            if (text.Length <= maxLength)
            {
                return text;
            }
            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}
