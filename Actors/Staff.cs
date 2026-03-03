using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Actors
{
    public abstract class Staff : Person, ISalaryCalculable
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
            WorkStatus = WorkStatus.Active;
            BaseSalary = baseSalary;
            JoinDate = joinDate;
        }

        // Constructor khong tham so cho XML serialization
        protected Staff() : base() { }

        // ===== ABSTRACT METHODS (Polymorphism) =====

        // Moi loai nhan vien tinh luong khac nhau
        public abstract decimal CalculateSalary();

        // Moi loai nhan vien co chi tiet luong khac nhau
        public abstract string GetSalaryBreakdown();

        // ===== COMMON METHODS =====

        // Cap nhat trang thai lam viec
        public void UpdateWorkStatus(WorkStatus newStatus)
        {
            WorkStatus = newStatus;
        }

        // Cap nhat luong co ban
        public void UpdateBaseSalary(decimal newSalary)
        {
            BaseSalary = newSalary;
        }

        // Cap nhat phong ban
        public void UpdateDepartment(string newDepartment)
        {
            Department = newDepartment;
        }

        // Nghi viec
        public void Resign()
        {
            WorkStatus = WorkStatus.Resigned;
        }

        // Nghi phep
        public void TakeLeave()
        {
            WorkStatus = WorkStatus.OnLeave;
        }

        // Quay lai lam viec
        public void ReturnToWork()
        {
            WorkStatus = WorkStatus.Active;
        }

        // Tinh so nam lam viec
        public int GetYearsOfService()
        {
            int years = DateTime.Now.Year - JoinDate.Year;
            if (DateTime.Now.DayOfYear < JoinDate.DayOfYear)
            {
                years--;
            }
            return years;
        }

        // Kiem tra nhan vien dang lam viec
        public bool IsActive()
        {
            return WorkStatus == WorkStatus.Active;
        }

        // Lay thong tin nhan vien
        public override string GetInfo()
        {
            return "[Staff] ID: " + StaffID + " | Name: " + FullName + "\n" +
                   "  Role: " + Role + " | Department: " + Department + "\n" +
                   "  Status: " + WorkStatus + " | Salary: " + BaseSalary.ToString("N0") + " VND\n" +
                   "  Join Date: " + JoinDate.ToString("dd/MM/yyyy") + " | Years: " + GetYearsOfService();
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
