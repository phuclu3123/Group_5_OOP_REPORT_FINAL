using System;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Actors
{
    public class WarehouseStaff : Staff
    {
        public string WarehouseID { get; private set; }
        public string Shift { get; private set; }

        // Thuoc tinh de tinh luong
        public decimal ShiftAllowance { get; private set; }
        public decimal HeavyDutyAllowance { get; private set; }

        public WarehouseStaff(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                              string staffId, string department, decimal baseSalary, DateTime joinDate,
                              string warehouseId, string shift)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.WarehouseManager, department, baseSalary, joinDate)
        {
            WarehouseID = warehouseId;
            Shift = shift;
            HeavyDutyAllowance = 500000m;

            // Phu cap ca dem cao hon ca ngay
            if (shift == "Ca dem" || shift == "Ca toi")
            {
                ShiftAllowance = 1500000m;
            }
            else
            {
                ShiftAllowance = 500000m;
            }
        }

        // Constructor khong tham so cho XML serialization
        public WarehouseStaff() : base() { }

        // ===== POLYMORPHISM: Tinh luong nhan vien kho =====
        // Luong = BaseSalary + phu cap ca + phu cap nang nhoc
        public override decimal CalculateSalary()
        {
            decimal totalSalary = BaseSalary + ShiftAllowance + HeavyDutyAllowance;
            return totalSalary;
        }

        public override string GetSalaryBreakdown()
        {
            return "[Luong NV Kho] " + FullName + "\n" +
                   "  Luong co ban:       " + BaseSalary.ToString("N0") + " VND\n" +
                   "  Phu cap ca (" + Shift + "): " + ShiftAllowance.ToString("N0") + " VND\n" +
                   "  Phu cap nang nhoc:  " + HeavyDutyAllowance.ToString("N0") + " VND\n" +
                   "  -------------------------\n" +
                   "  TONG LUONG:         " + CalculateSalary().ToString("N0") + " VND";
        }

        // ===== WAREHOUSE STAFF METHODS =====

        // Cap nhat ca lam viec (tu dong cap nhat phu cap)
        public void UpdateShift(string newShift)
        {
            Shift = newShift;
            if (newShift == "Ca dem" || newShift == "Ca toi")
            {
                ShiftAllowance = 1500000m;
            }
            else
            {
                ShiftAllowance = 500000m;
            }
        }

        // Cap nhat phu cap nang nhoc
        public void UpdateHeavyDutyAllowance(decimal newAllowance)
        {
            HeavyDutyAllowance = newAllowance;
        }

        // Chuyen kho
        public void TransferWarehouse(string newWarehouseId)
        {
            WarehouseID = newWarehouseId;
        }

        // Lay thong tin nhan vien kho
        public override string GetInfo()
        {
            return "[WarehouseStaff] ID: " + StaffID + " | Name: " + FullName + "\n" +
                   "  Warehouse: " + WarehouseID + " | Shift: " + Shift + "\n" +
                   "  Department: " + Department + " | Status: " + WorkStatus + "\n" +
                   "  Salary: " + BaseSalary.ToString("N0") + " VND";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
