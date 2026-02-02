using System;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public abstract class Staff : Person
    {
        public string StaffID { get; protected set; }
        public Role Role { get; protected set; }
        public string Department { get; protected set; }
        public WorkStatus WorkStatus { get; protected set; }
        public decimal BaseSalary { get; protected set; }
        public DateTime JoinDate { get; protected set; }

        protected Staff(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                        string staffId, Role role, string department, decimal baseSalary, DateTime joinDate)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId)
        {
            StaffID = staffId;
            Role = role;
            Department = department;
            WorkStatus = WorkStatus.Active; // Default to Active
            BaseSalary = baseSalary;
            JoinDate = joinDate;
        }
    }
}
