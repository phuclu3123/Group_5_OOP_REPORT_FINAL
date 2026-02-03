using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cuoi_ky_OOP.Models.Business
{
    public enum ServiceType { Standard, Express, Overnight }
    public enum OrderStatus { Pending, Confirmed, Shipped, Delivered, Cancelled }

    public class Order
    {
        private string TrackingNumber { get; set; } = string.Empty;
        private string SenderID { get; set; } = string.Empty;
        private string ReceiverID { get; set; } = string.Empty;
        private string PickupAddress { get; set; } = string.Empty;
        private string DeliveryAddress { get; set; } = string.Empty;
        private List<Package> Packages { get; set; } = new List<Package>();
        private double TotalWeight { get; set; }
        private decimal TotalCost { get; set; }
        private ServiceType ServiceType { get; set; }
        private OrderStatus CurrentStatus { get; set; }
        public Order() {}
    }
}