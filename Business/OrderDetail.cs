using System;

namespace Cuoi_ky_OOP.Models.Business
{
    // COMPOSITION (Strong "Has-A") voi Order
    // OrderDetail CHI duoc tao ben trong Order thong qua AddDetail()
    // Khong the ton tai neu khong co Order (Sinh cung sinh, diet cung diet)
    public class OrderDetail
    {
        public string DetailID { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal SubTotal { get; private set; }

        // Constructor internal: chi cho phep tao tu ben trong assembly (Order.AddDetail)
        internal OrderDetail(string detailId, string productName, int quantity, decimal unitPrice)
        {
            DetailID = detailId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            SubTotal = quantity * unitPrice;
        }

        // Constructor khong tham so cho XML serialization
        internal OrderDetail()
        {
            DetailID = "";
            ProductName = "";
            Quantity = 0;
            UnitPrice = 0;
            SubTotal = 0;
        }

        // Cap nhat so luong
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            SubTotal = Quantity * UnitPrice;
        }

        // Lay thong tin chi tiet don hang
        public string GetDetailInfo()
        {
            return "[Detail] ID: " + DetailID + " | " + ProductName +
                   " | SL: " + Quantity + " x " + UnitPrice.ToString("N0") +
                   " = " + SubTotal.ToString("N0") + " VND";
        }

        public override string ToString()
        {
            return GetDetailInfo();
        }
    }
}
