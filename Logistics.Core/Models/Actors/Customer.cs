using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Actors
{
    public class Customer : Person
    {
        /*  
        I. Các thuộc tính cơ bản của một khách hàng
        1. AccountID (mã tài khoản liên kết với khách hàng, có thể null nếu là khách vãng lai)
        2. CustomerType (loại khách hàng: Regular, VIP)
        3. LoyaltyPoints (điểm tích lũy)
        4. DefaultLocation (vị trí mặc định của khách hàng, sử dụng GeoPoint)
        5. CreditLimit (hạn mức tín dụng cho khách hàng)
        */
        public string? AccountID { get; private set; } 
        
        public CustomerType CustomerType { get; private set; }
        public int LoyaltyPoints { get; private set; }
        public GeoPoint DefaultLocation { get; private set; }
        public decimal CreditLimit { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một khách hàng
        public Customer(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, 
                        CustomerType customerType, GeoPoint defaultLocation, decimal creditLimit, string? accountId = null)
            : base(id, fullName, phoneNumber, email, birthDay, gender, homeAddress) 
        {
            AccountID = accountId; // Gán AccountID tại class con
            CustomerType = customerType;
            DefaultLocation = defaultLocation;
            CreditLimit = creditLimit;
            LoyaltyPoints = 0;
            IsActive = true;
        }

        // Constructor không tham số cho JSON serialization và các lớp con
        protected Customer() : base() { }

        // Constructor cho ISerializable (Deserialization)
        protected Customer(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            AccountID = info.GetString("AccountID"); // Không cần ?? string.Empty vì có thể null
            CustomerType = (CustomerType)info.GetValue("CustomerType", typeof(CustomerType))!;
            LoyaltyPoints = info.GetInt32("LoyaltyPoints");
            DefaultLocation = (GeoPoint)info.GetValue("DefaultLocation", typeof(GeoPoint))!;
            CreditLimit = info.GetDecimal("CreditLimit");
            try
            {
                IsActive = info.GetBoolean("IsActive");
            }
            catch
            {
                IsActive = true;
            }
        }

        // Phương thức ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AccountID", AccountID);
            info.AddValue("CustomerType", CustomerType);
            info.AddValue("LoyaltyPoints", LoyaltyPoints);
            info.AddValue("DefaultLocation", DefaultLocation);
            info.AddValue("CreditLimit", CreditLimit);
            info.AddValue("IsActive", IsActive);
        }

        /* ===== CUSTOMER METHODS =====
            1. Thêm điểm tích lũy
            2. Đổi điểm tích lũy lấy ưu đãi
            3. Cập nhật loại khách hàng
            4. Cập nhật hạn mức tín dụng
            5. Cập nhật vị trí mặc định
            6. Kiểm tra xem khách hàng có phải là VIP hay không
        */
        public void AddLoyaltyPoints(int points)
        {
            LoyaltyPoints += points;
            if (LoyaltyPoints < 0)
            {
                LoyaltyPoints = 0;
            }
        }

        public void SetLoyaltyPoints(int points)
        {
            LoyaltyPoints = points < 0 ? 0 : points;
        }

        public bool RedeemLoyaltyPoints(int points)
        {
            if (points <= LoyaltyPoints)
            {
                LoyaltyPoints -= points;
                return true;
            }
            return false;
        }

        public void UpdateCustomerType(CustomerType newType)
        {
            CustomerType = newType;
        }

        public void UpdateCreditLimit(decimal newLimit)
        {
            CreditLimit = newLimit;
        }

        public void UpdateDefaultLocation(GeoPoint newLocation)
        {
            DefaultLocation = newLocation;
        }

        public void UpdateAccountId(string accountId)
        {
            AccountID = accountId;
        }

        public bool IsVIP()
        {
            return CustomerType == CustomerType.VIP;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Reactivate()
        {
            IsActive = true;
        }

        // Lấy thông tin khách hàng
        public override string GetInfo()
        {
            // Xử lý hiển thị AccountID nếu là khách vãng lai
            string accountDisplay = string.IsNullOrEmpty(AccountID) ? "Guest (No Account)" : AccountID;

            // Gọi base.GetInfo() để lấy Name, Phone, Address...
            return base.GetInfo() + "\n" +
                   $"[Customer Details] Account ID: {accountDisplay}\n" +
                   $"  Type: {CustomerType} | Loyalty Points: {LoyaltyPoints}\n" +
                   $"  Credit Limit: {CreditLimit:N0} VND | Default Location: {DefaultLocation}";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
