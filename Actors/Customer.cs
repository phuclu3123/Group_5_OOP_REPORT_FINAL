using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class Customer : Person
    {
        public CustomerType CustomerType { get; private set; }
        public int LoyaltyPoints { get; private set; }
        public GeoPoint DefaultLocation { get; private set; }
        public decimal CreditLimit { get; private set; }

        public Customer(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                        CustomerType customerType, GeoPoint defaultLocation, decimal creditLimit)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId)
        {
            CustomerType = customerType;
            DefaultLocation = defaultLocation;
            CreditLimit = creditLimit;
            LoyaltyPoints = 0;
        }

        // Constructor khong tham so cho XML serialization
        public Customer() : base() { }

        // Cong diem tich luy
        public void AddLoyaltyPoints(int points)
        {
            LoyaltyPoints += points;
        }

        // Tru diem tich luy
        public bool RedeemLoyaltyPoints(int points)
        {
            if (points <= LoyaltyPoints)
            {
                LoyaltyPoints -= points;
                return true;
            }
            return false;
        }

        // Cap nhat loai khach hang
        public void UpdateCustomerType(CustomerType newType)
        {
            CustomerType = newType;
        }

        // Cap nhat han muc tin dung
        public void UpdateCreditLimit(decimal newLimit)
        {
            CreditLimit = newLimit;
        }

        // Cap nhat vi tri mac dinh
        public void UpdateDefaultLocation(GeoPoint newLocation)
        {
            DefaultLocation = newLocation;
        }

        // Kiem tra khach hang VIP
        public bool IsVIP()
        {
            return CustomerType == CustomerType.VIP;
        }

        // Lay thong tin khach hang
        public override string GetInfo()
        {
            return "[Customer] ID: " + Id + " | Name: " + FullName + "\n" +
                   "  Phone: " + PhoneNumber + " | Email: " + Email + "\n" +
                   "  Address: " + Address + "\n" +
                   "  Type: " + CustomerType + " | Points: " + LoyaltyPoints + "\n" +
                   "  Credit Limit: " + CreditLimit.ToString("N0") + " VND | Location: " + DefaultLocation;
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
