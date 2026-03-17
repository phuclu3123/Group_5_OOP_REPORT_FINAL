using System;

namespace Cuoi_ky_OOP.Models.Business
{
    // COMPOSITION (Strong "Has-A") voi Order
    // DeliveryRoute chi duoc tao ben trong Order, khong the ton tai doc lap
    // Khi Order bi huy -> DeliveryRoute cung bi huy (Sinh cung sinh, diet cung diet)
    public class DeliveryRoute
    {
        public string PickupAddress { get; private set; }
        public string DeliveryAddress { get; private set; }
        public double EstimatedDistanceKm { get; private set; }

        // Constructor internal: chi cho phep tao tu ben trong assembly (Order)
        internal DeliveryRoute(string pickupAddress, string deliveryAddress)
        {
            PickupAddress = pickupAddress;
            DeliveryAddress = deliveryAddress;
            EstimatedDistanceKm = EstimateDistance();
        }

        // Constructor khong tham so cho XML serialization
        internal DeliveryRoute()
        {
            PickupAddress = "";
            DeliveryAddress = "";
            EstimatedDistanceKm = 0;
        }

        // Uoc tinh khoang cach (mo phong)
        private double EstimateDistance()
        {
            // Mo phong: tinh khoang cach dua tren do dai chuoi dia chi
            int totalLength = PickupAddress.Length + DeliveryAddress.Length;
            return Math.Round(totalLength * 0.5, 1);
        }

        // Cap nhat dia chi giao hang
        public void UpdateDeliveryAddress(string newAddress)
        {
            DeliveryAddress = newAddress;
            EstimatedDistanceKm = EstimateDistance();
        }

        // Lay thong tin tuyen duong
        public string GetRouteInfo()
        {
            return "[Route] From: " + PickupAddress + " -> To: " + DeliveryAddress +
                   " | Distance: ~" + EstimatedDistanceKm + "km";
        }

        public override string ToString()
        {
            return GetRouteInfo();
        }
    }
}
