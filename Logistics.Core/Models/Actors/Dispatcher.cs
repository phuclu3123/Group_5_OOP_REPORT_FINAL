using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Actors
{
    public class Dispatcher : Staff
    {   
        /*
        I. Các thuộc tính cơ bản của một điều phối viên
        1. ManagedRegion (khu vực quản lý)
        2. RegionAllowance (phụ cấp khu vực)
        3. KpiBonus (thưởng KPI hàng tháng)
        */

        public string ManagedRegion { get; private set; }

        public decimal RegionAllowance { get; private set; }
        public decimal KpiBonus { get; private set; }

        // 1. Constructor có tham số để khởi tạo đầy đủ thông tin của một điều phối viên
        public Dispatcher(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, string accountId,
                          string staffId, string department, decimal baseSalary, DateTime joinDate,
                          string managedRegion)
            : base(id, fullName, phoneNumber, email, homeAddress, birthDay, gender, accountId, staffId, Role.Dispatcher, department, baseSalary, joinDate)
        {
            ManagedRegion = managedRegion;
            RegionAllowance = 2000000m;
            KpiBonus = 0; // Khởi tạo KPI ban đầu bằng 0
        }

        // 2. Constructor không tham số cho JSON serialization và các lớp con
        protected Dispatcher() : base()
        {
            ManagedRegion = string.Empty;
        }

        // Constructor cho ISerializable (Deserialization)
        protected Dispatcher(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ManagedRegion = info.GetString("ManagedRegion") ?? string.Empty; // 3. Xử lý null an toàn
            RegionAllowance = info.GetDecimal("RegionAllowance");
            KpiBonus = info.GetDecimal("KpiBonus");
        }

        // Phương thức ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ManagedRegion", ManagedRegion);
            info.AddValue("RegionAllowance", RegionAllowance);
            info.AddValue("KpiBonus", KpiBonus);
        }

        /*
        II. Các phương thức của một điều phối viên
        1. Cập nhật phụ cấp khu vực
        2. Cập nhật thưởng KPI
        3. Cập nhật khu vực quản lý
        4. Lấy thông tin chi tiết của một điều phối viên
        5. Override ToString() để hiển thị thông tin chi tiết của một điều phối viên
        */
        public override decimal CalculateSalary()
        {
            return BaseSalary + RegionAllowance + KpiBonus;
        }

        public override string GetSalaryBreakdown()
        {
            return $"[Lương Điều Phối] {FullName}\n" +
                   $"  Lương cơ bản:    {BaseSalary:N0} VND\n" +
                   $"  Phụ cấp khu vực: {RegionAllowance:N0} VND\n" +
                   $"  Thưởng KPI:      {KpiBonus:N0} VND\n" +
                   $"  -------------------------\n" +
                   $"  TỔNG LƯƠNG:      {CalculateSalary():N0} VND";
        }

        public void UpdateRegionAllowance(decimal newAllowance)
        {
            RegionAllowance = newAllowance;
        }

        public void UpdateKpiBonus(decimal newBonus)
        {
            KpiBonus = newBonus;
        }

        public void UpdateManagedRegion(string newRegion)
        {
            ManagedRegion = newRegion;
        }

        // Lấy thông tin điều phối viên
        public override string GetInfo()
        {
            // 5. Gọi base.GetInfo() để lấy thông tin cá nhân và thông tin Staff cơ bản
            // Không cần in lại Department/Salary vì lớp Staff đã lo việc đó.
            return base.GetInfo() + "\n" +
                   $"[Dispatcher Details]\n" +
                   $"  Managed Region: {ManagedRegion}";
        }

        // Override ToString() để hiển thị thông tin chi tiết của một điều phối viên
        public override string ToString()
        {
            return GetInfo();
        }
    }
}
