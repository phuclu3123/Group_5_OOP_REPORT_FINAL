using System;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class Dispatcher : Staff
    {
        public string ManagedRegion { get; private set; }

        public Dispatcher(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                          string staffId, string department, decimal baseSalary, DateTime joinDate,
                          string managedRegion)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.Dispatcher, department, baseSalary, joinDate)
        {
            ManagedRegion = managedRegion;
        }
    }
}
