using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable

namespace Logistic.Core.Models.Business
{
    // COMPOSITION (Strong "Has-A") voi Order
    // OrderDetail CHI duoc tao ben trong Order thong qua AddDetail()
    // Khong the ton tai neu khong co Order (Sinh cung sinh, diet cung diet)
    // Danh dau class co the duoc serialize
    [Serializable]
    public class OrderDetail : ISerializable
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

        // Constructor khong tham so cho serialization
        internal OrderDetail()
        {
            DetailID = "";
            ProductName = "";
            Quantity = 0;
            UnitPrice = 0;
            SubTotal = 0;
        }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong OrderDetail tu SerializationInfo khi doc tu file
        protected OrderDetail(SerializationInfo info, StreamingContext context)
        {
            DetailID = info.GetString("DetailID") ?? "";
            ProductName = info.GetString("ProductName") ?? "";
            Quantity = info.GetInt32("Quantity");
            UnitPrice = info.GetDecimal("UnitPrice");
            SubTotal = info.GetDecimal("SubTotal");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua OrderDetail vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DetailID", DetailID);
            info.AddValue("ProductName", ProductName);
            info.AddValue("Quantity", Quantity);
            info.AddValue("UnitPrice", UnitPrice);
            info.AddValue("SubTotal", SubTotal);
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
