﻿using System;

namespace Logistics.Core.Models.Business
{
    public class DeliveryRoute
    {   
        /*
        I. Các thuộc tính cơ bản của một tuyến giao hàng
        1. PickupAddress (địa chỉ lấy hàng)
        2. DeliveryAddress (địa chỉ giao hàng)
        3. EstimatedDistanceKm (khoảng cách ước tính giữa pickup và delivery)
        */
        public string PickupAddress { get; private set; }
        public string DeliveryAddress { get; private set; }
        public double EstimatedDistanceKm { get; private set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một tuyến giao hàng
        public DeliveryRoute(string pickupAddress, string deliveryAddress)
        {
            PickupAddress = pickupAddress;
            DeliveryAddress = deliveryAddress;
            EstimatedDistanceKm = CalculateDistance(pickupAddress, deliveryAddress);
        }

        // Constructor khong tham so cho JSON serialization
        public DeliveryRoute()
        {
            PickupAddress = string.Empty;
            DeliveryAddress = string.Empty;
        }

        /*
        II. Các phương thức của một tuyến giao hàng
        1. Cập nhật địa chỉ giao hàng (tự động cập nhật khoảng cách)
        2. Phương thức giả lập tính khoảng cách giữa hai địa chỉ (có thể thay bằng API thực tế)
        3. Lấy thông tin chi tiết của một tuyến giao hàng
        4. Override ToString() để hiển thị thông tin chi tiết của một tuyến giao hàng
         */
        public void UpdateDeliveryAddress(string newAddress)
        {
            DeliveryAddress = newAddress;
            EstimatedDistanceKm = CalculateDistance(PickupAddress, newAddress);
        }

        // Phương thức giả lập tính khoảng cách giữa hai địa chỉ (có thể thay bằng API thực tế)
        private double CalculateDistance(string from, string to)
        {
            // DEMO LOGIC: Trả về khoảng cách giả định dựa trên độ dài chuỗi địa chỉ
            return 15.5;
        }

        // Override ToString() để hiển thị thông tin chi tiết của một tuyến giao hàng
        public override string ToString()
        {
            return PickupAddress + " -> " + DeliveryAddress + " (" + EstimatedDistanceKm + "km)";
        }
    }
}
