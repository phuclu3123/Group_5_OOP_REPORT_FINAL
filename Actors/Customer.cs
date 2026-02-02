using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class Customer : Person
    {
        public CustomerType CustomerType { get; private set; }
        public int LoyaltyPoints { get; set; }
        public GeoPoint DefaultLocation { get; set; }
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

        public void AddLoyaltyPoints(int points)
        {
            LoyaltyPoints += points;
        }
    }
}
