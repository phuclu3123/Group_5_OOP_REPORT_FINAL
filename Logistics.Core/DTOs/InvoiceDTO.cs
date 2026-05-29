using System;
using System.Collections.Generic;

namespace Logistics.Core.DTOs
{
    /// <summary>
    /// DTO dùng để hiển thị và in hóa đơn đơn hàng trên FrmInvoice.
    /// </summary>
    public class InvoiceDTO
    {
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string PickupAddress { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<InvoiceLineDTO> Lines { get; set; } = new List<InvoiceLineDTO>();
        public double TotalWeightKg { get; set; }
        public decimal ShippingFee { get; set; }

        // Chuỗi văn bản hóa đơn đã định dạng (từ ReportService.GenerateInvoice)
        public string FormattedText { get; set; } = string.Empty;
    }

    /// <summary>
    /// Từng dòng trong hóa đơn (chi tiết gói hàng).
    /// </summary>
    public class InvoiceLineDTO
    {
        public string PackageId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double WeightKg { get; set; }
        public double VolumeWeight { get; set; }
        public string PackageType { get; set; } = string.Empty;
    }
}
