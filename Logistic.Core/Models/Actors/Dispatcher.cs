using System;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Actors
{
    public class Dispatcher : Staff
    {
        public string ManagedRegion { get; private set; }

        // Thuoc tinh de tinh luong
        public decimal RegionAllowance { get; private set; }
        public decimal KpiBonus { get; private set; }

        public Dispatcher(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                          string staffId, string department, decimal baseSalary, DateTime joinDate,
                          string managedRegion)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.Dispatcher, department, baseSalary, joinDate)
        {
            ManagedRegion = managedRegion;
            RegionAllowance = 2000000m;
            KpiBonus = 0;
        }

        // Constructor khong tham so cho XML serialization
        public Dispatcher() : base() { }

        // ===== POLYMORPHISM: Tinh luong dieu phoi vien =====
        // Luong = BaseSalary + phu cap khu vuc + thuong KPI
        public override decimal CalculateSalary()
        {
            decimal totalSalary = BaseSalary + RegionAllowance + KpiBonus;
            return totalSalary;
        }

        public override string GetSalaryBreakdown()
        {
            return "[Luong Dieu Phoi] " + FullName + "\n" +
                   "  Luong co ban:     " + BaseSalary.ToString("N0") + " VND\n" +
                   "  Phu cap khu vuc:  " + RegionAllowance.ToString("N0") + " VND\n" +
                   "  Thuong KPI:       " + KpiBonus.ToString("N0") + " VND\n" +
                   "  -------------------------\n" +
                   "  TONG LUONG:       " + CalculateSalary().ToString("N0") + " VND";
        }

        // ===== DISPATCHER METHODS =====

        // Cap nhat phu cap khu vuc
        public void UpdateRegionAllowance(decimal newAllowance)
        {
            RegionAllowance = newAllowance;
        }

        // Cap nhat thuong KPI
        public void UpdateKpiBonus(decimal newBonus)
        {
            KpiBonus = newBonus;
        }

        // Cap nhat khu vuc quan ly
        public void UpdateManagedRegion(string newRegion)
        {
            ManagedRegion = newRegion;
        }

        // Lay thong tin dieu phoi vien
        public override string GetInfo()
        {
            return "[Dispatcher] ID: " + StaffID + " | Name: " + FullName + "\n" +
                   "  Region: " + ManagedRegion + "\n" +
                   "  Department: " + Department + " | Status: " + WorkStatus + "\n" +
                   "  Salary: " + BaseSalary.ToString("N0") + " VND";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
