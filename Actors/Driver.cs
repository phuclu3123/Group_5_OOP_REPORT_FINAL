using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class Driver : Staff
    {
        public string LicenseNumber { get; private set; }
        public string LicenseType { get; private set; }
        public DriverStatus DriverStatus { get; set; }
        public GeoPoint CurrentLocation { get; set; }

        public Driver(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                      string staffId, string department, decimal baseSalary, DateTime joinDate,
                      string licenseNumber, string licenseType)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.Driver, department, baseSalary, joinDate)
        {
            LicenseNumber = licenseNumber;
            LicenseType = licenseType;
            DriverStatus = DriverStatus.Available;
            CurrentLocation = new GeoPoint(0, 0); // Default 0,0
        }
    }
}
