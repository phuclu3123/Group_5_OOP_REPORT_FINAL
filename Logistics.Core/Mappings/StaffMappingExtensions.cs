using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa các Staff classes và StaffDTO.
    /// Bao gồm: Driver → StaffDTO, Dispatcher → StaffDTO, WarehouseStaff → StaffDTO, Admin → StaffDTO.
    /// </summary>
    public static class StaffMappingExtensions
    {
        // Extension methods cho StaffDTO
        // ─── Driver → StaffDTO ───────────────────────────────────────────────
        public static StaffDTO ToStaffDTO(this Driver driver)
        {
            if (driver == null) return new StaffDTO();

            StaffDTO dto = new StaffDTO();
            dto.StaffId        = driver.StaffID;
            dto.PersonId       = driver.Id;
            dto.FullName       = driver.FullName;
            dto.Phone          = driver.PhoneNumber;
            dto.Email          = driver.Email;
            dto.Role           = EnumTranslator.TranslateRole("Driver");
            dto.Department     = driver.Department;
            dto.WorkStatus     = EnumTranslator.TranslateWorkStatus(driver.WorkStatus);
            dto.BaseSalary     = driver.BaseSalary;
            dto.TotalSalary    = driver.CalculateSalary();
            dto.JoinDate       = driver.JoinDate.ToString("dd/MM/yyyy");
            dto.YearsOfService = driver.GetYearsOfService();

            return dto;
        }

        // ─── Dispatcher → StaffDTO ───────────────────────────────────────────
        public static StaffDTO ToStaffDTO(this Dispatcher dispatcher)
        {
            if (dispatcher == null) return new StaffDTO();

            StaffDTO dto = new StaffDTO();
            dto.StaffId        = dispatcher.StaffID;
            dto.PersonId       = dispatcher.Id;
            dto.FullName       = dispatcher.FullName;
            dto.Phone          = dispatcher.PhoneNumber;
            dto.Email          = dispatcher.Email;
            dto.Role           = EnumTranslator.TranslateRole("Dispatcher");
            dto.Department     = dispatcher.Department;
            dto.WorkStatus     = EnumTranslator.TranslateWorkStatus(dispatcher.WorkStatus);
            dto.BaseSalary     = dispatcher.BaseSalary;
            dto.TotalSalary    = dispatcher.CalculateSalary();
            dto.JoinDate       = dispatcher.JoinDate.ToString("dd/MM/yyyy");
            dto.YearsOfService = dispatcher.GetYearsOfService();
            dto.ManagedRegion  = dispatcher.ManagedRegion;

            return dto;
        }

        // ─── WarehouseStaff → StaffDTO ───────────────────────────────────────
        public static StaffDTO ToStaffDTO(this WarehouseStaff staff)
        {
            if (staff == null) return new StaffDTO();

            StaffDTO dto = new StaffDTO();
            dto.StaffId        = staff.StaffID;
            dto.PersonId       = staff.Id;
            dto.FullName       = staff.FullName;
            dto.Phone          = staff.PhoneNumber;
            dto.Email          = staff.Email;
            dto.Role           = EnumTranslator.TranslateRole("WarehouseStaff");
            dto.Department     = staff.Department;
            dto.WorkStatus     = EnumTranslator.TranslateWorkStatus(staff.WorkStatus);
            dto.BaseSalary     = staff.BaseSalary;
            dto.TotalSalary    = staff.CalculateSalary();
            dto.JoinDate       = staff.JoinDate.ToString("dd/MM/yyyy");
            dto.YearsOfService = staff.GetYearsOfService();
            dto.WarehouseId    = staff.WarehouseID;
            dto.Shift          = staff.Shift;

            return dto;
        }

        // ─── Admin → StaffDTO ────────────────────────────────────────────────
        public static StaffDTO ToStaffDTO(this Admin admin)
        {
            if (admin == null) return new StaffDTO();

            StaffDTO dto = new StaffDTO();
            dto.StaffId        = admin.StaffID;
            dto.PersonId       = admin.Id;
            dto.FullName       = admin.FullName;
            dto.Phone          = admin.PhoneNumber;
            dto.Email          = admin.Email;
            dto.Role           = EnumTranslator.TranslateRole("Admin");
            dto.Department     = admin.Department;
            dto.WorkStatus     = EnumTranslator.TranslateWorkStatus(admin.WorkStatus);
            dto.BaseSalary     = admin.BaseSalary;
            dto.TotalSalary    = admin.CalculateSalary();
            dto.JoinDate       = admin.JoinDate.ToString("dd/MM/yyyy");
            dto.YearsOfService = admin.GetYearsOfService();
            dto.AdminCode      = admin.AdminCode;

            return dto;
        }
    }
}
