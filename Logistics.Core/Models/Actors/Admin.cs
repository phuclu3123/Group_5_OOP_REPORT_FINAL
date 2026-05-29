using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Actors
{
    public class Admin : Staff
    {
        public string AdminCode { get; private set; }
        
        // Thuộc tính để tính lương
        public decimal ManagementAllowance { get; private set; }

        public Admin(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, string accountId,
                     string staffId, string department, decimal baseSalary, DateTime joinDate,
                     string adminCode, decimal managementAllowance = 3000000m)
            : base(id, fullName, phoneNumber, email, homeAddress, birthDay, gender, accountId, staffId, Role.Admin, department, baseSalary, joinDate)
        {
            AdminCode = adminCode;
            ManagementAllowance = managementAllowance;
        }

        protected Admin() : base()
        {
            AdminCode = string.Empty;
        }

        // Constructor cho ISerializable (Deserialization)
        protected Admin(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            AdminCode = info.GetString("AdminCode") ?? string.Empty;
            ManagementAllowance = info.GetDecimal("ManagementAllowance");
        }

        // Phương thức ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AdminCode", AdminCode);
            info.AddValue("ManagementAllowance", ManagementAllowance);
        }

        // ===== POLYMORPHISM: Tính lương Admin =====
        // Lương = BaseSalary + phụ cấp quản lý
        public override decimal CalculateSalary()
        {
            return BaseSalary + ManagementAllowance;
        }

        public override string GetSalaryBreakdown()
        {
            return $"[Lương Admin] {FullName}\n" +
                   $"  Lương cơ bản:    {BaseSalary:N0} VND\n" +
                   $"  Phụ cấp quản lý: {ManagementAllowance:N0} VND\n" +
                   $"  -------------------------\n" +
                   $"  TỔNG LƯƠNG:      {CalculateSalary():N0} VND";
        }

        // ===== ADMIN METHODS =====

        public void UpdateManagementAllowance(decimal newAllowance)
        {
            ManagementAllowance = newAllowance;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + "\n" +
                   $"[Admin Details]\n" +
                   $"  Admin Code: {AdminCode}";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
