using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Driver (domain model) và DriverDTO (display model).
    /// </summary>
    public static class DriverMappingExtensions
    {
        // Driver → DriverDTO (dùng cho DataGridView, Form display)
        public static DriverDTO ToDTO(this Driver driver)
        {
            if (driver == null) return new DriverDTO();

            DriverDTO dto = new DriverDTO();
            dto.DriverId       = driver.StaffID;
            dto.FullName       = driver.FullName;
            dto.Phone          = driver.PhoneNumber;
            dto.Email          = driver.Email;
            dto.LicenseNumber  = driver.LicenseNumber;
            dto.LicenseType    = driver.LicenseType;
            dto.Status         = driver.DriverStatus.ToString();
            dto.WorkStatus     = driver.WorkStatus.ToString();
            dto.Department     = driver.Department;
            dto.DeliveryCount  = driver.DeliveryCount;
            dto.TotalSalary    = driver.CalculateSalary();

            return dto;
        }
    }
}
