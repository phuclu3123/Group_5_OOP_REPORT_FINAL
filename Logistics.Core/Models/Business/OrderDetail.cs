﻿using System;

namespace Logistics.Core.Models.Business
{
    public class OrderDetail
    {   
        /*
        I. Các thuộc tính cơ bản của một chi tiết đơn hàng
        1. DetailID (mã chi tiết đơn hàng)
        2. ProductName (tên sản phẩm)
        3. Quantity (số lượng)
        4. UnitPrice (đơn giá)
        */
        public string DetailID { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một chi tiết đơn hàng
        public OrderDetail(string detailId, string productName, int quantity, decimal unitPrice)
        {
            DetailID = detailId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        // Constructor rong cho JSON serialization
        public OrderDetail()
        {
            DetailID = null!;
            ProductName = null!;
        }
        /*
        II. Các phương thức của một chi tiết đơn hàng
        1. Tính thành tiền (Quantity * UnitPrice)
        2. Lấy thông tin chi tiết của một chi tiết đơn hàng
        3. Override ToString() để hiển thị thông tin chi tiết của một chi tiết đơn hàng
        */
        public decimal GetSubTotal()
        {
            return Quantity * UnitPrice;
        }

        public string GetDetailInfo()
        {
            return $"[OrderDetail] {DetailID} - {ProductName} x {Quantity} = {GetSubTotal().ToString("N0")} VND";
        }

        public override string ToString()
        {
            return GetDetailInfo();
        }
    }
}

