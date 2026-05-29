using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Actors
{
    public class WarehouseStaff : Staff
    {   
        /*
        I. Các thuộc tính cơ bản của một nhân viên kho
        1. WarehouseID (mã kho làm việc)
        2. Shift (ca làm việc: Ca ngày, Ca đêm, Ca tối)
        3. ShiftAllowance (phụ cấp ca)
        4. HeavyDutyAllowance (phụ cấp nặng nhọc)
        */
        public string WarehouseID { get; private set; }
        public string Shift { get; private set; }

        // Thuộc tính để tính lương
        public decimal ShiftAllowance { get; private set; }
        public decimal HeavyDutyAllowance { get; private set; }

        public WarehouseStaff(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, string accountId,
                              string staffId, string department, decimal baseSalary, DateTime joinDate,
                              string warehouseId, string shift)
            : base(id, fullName, phoneNumber, email, homeAddress, birthDay, gender, accountId, staffId, Role.WarehouseManager, department, baseSalary, joinDate)
        {
            WarehouseID = warehouseId;
            Shift = shift;
            HeavyDutyAllowance = 500000m;

            // Phụ cấp ca đêm cao hơn ca ngày
            if (shift == "Ca dem" || shift == "Ca toi")
            {
                ShiftAllowance = 1500000m;
            }
            else
            {
                ShiftAllowance = 500000m;
            }
        }

        // 2. Constructor không tham số cho JSON serialization và các lớp con
        protected WarehouseStaff() : base()
        {
            WarehouseID = string.Empty;
            Shift = string.Empty;
        }

        // Constructor cho ISerializable (Deserialization)
        protected WarehouseStaff(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            WarehouseID = info.GetString("WarehouseID") ?? string.Empty; // 5. Thêm xử lý Null
            Shift = info.GetString("Shift") ?? string.Empty;
            ShiftAllowance = info.GetDecimal("ShiftAllowance");
            HeavyDutyAllowance = info.GetDecimal("HeavyDutyAllowance");
        }

        // Phương thức ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("WarehouseID", WarehouseID);
            info.AddValue("Shift", Shift);
            info.AddValue("ShiftAllowance", ShiftAllowance);
            info.AddValue("HeavyDutyAllowance", HeavyDutyAllowance);
        }

        /*
        II. Các phương thức của một nhân viên kho
        1. Cập nhật ca làm việc (tự động cập nhật phụ cấp)
        2. Cập nhật phụ cấp nặng nhọc
        3. Chuyển kho
        4. Lấy thông tin nhân viên kho
        5. Override ToString() để hiển thị thông tin chi tiết của một nhân viên kho
        */

        // ===== POLYMORPHISM: Tính lương nhân viên kho =====
        // Lương = BaseSalary + phụ cấp ca + phụ cấp nặng nhọc
        public override decimal CalculateSalary()
        {
            return BaseSalary + ShiftAllowance + HeavyDutyAllowance;
        }

        public override string GetSalaryBreakdown()
        {
            // 3. Sử dụng String Interpolation
            return $"[Lương NV Kho] {FullName}\n" +
                   $"  Lương cơ bản:         {BaseSalary:N0} VND\n" +
                   $"  Phụ cấp ca ({Shift}): {ShiftAllowance:N0} VND\n" +
                   $"  Phụ cấp nặng nhọc:    {HeavyDutyAllowance:N0} VND\n" +
                   $"  -------------------------\n" +
                   $"  TỔNG LƯƠNG:           {CalculateSalary():N0} VND";
        }

        // ===== WAREHOUSE STAFF METHODS =====

        // Cập nhật ca làm việc (tự động cập nhật phụ cấp)
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

        public void UpdateHeavyDutyAllowance(decimal newAllowance)
        {
            HeavyDutyAllowance = newAllowance;
        }

        // Chuyển kho
        public void TransferWarehouse(string newWarehouseId)
        {
            WarehouseID = newWarehouseId;
        }

        // Lấy thông tin nhân viên kho
        public override string GetInfo()
        {
            // 2. Gọi base.GetInfo() để kế thừa thông tin từ Person và Staff
            return base.GetInfo() + "\n" +
                   $"[Warehouse Staff Details]\n" +
                   $"  Warehouse ID: {WarehouseID} | Shift: {Shift}";
        }

        // Override ToString() để hiển thị thông tin chi tiết của một nhân viên kho
        public override string ToString()
        {
            return GetInfo();
        }
    }
}
