using System;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Security;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;

namespace Logistics.Core.Services.Implementations
{
    /// <summary>
    /// Triển khai dịch vụ quản lý phương tiện vận tải, bao gồm xác thực định dạng dữ liệu và phân quyền.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly IAuditService? _auditService;

        public VehicleService(VehicleRepository vehicleRepository, IAuditService? auditService = null)
        {
            _vehicleRepository = vehicleRepository;
            _auditService = auditService;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleRepository.GetAll();
        }

        public Vehicle GetVehicleById(string vehicleId)
        {
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                return null!;
            }
            return _vehicleRepository.GetById(vehicleId);
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Người dùng không có quyền thêm phương tiện.");
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Phương tiện không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.VehicleID))
            {
                throw new ArgumentException("Mã phương tiện không được để trống.");
            }

            if (_vehicleRepository.GetById(vehicle.VehicleID) != null)
            {
                throw new ArgumentException("Mã phương tiện đã tồn tại trên hệ thống.");
            }

            if (vehicle.MaxWeightCapacity <= 0)
            {
                throw new ArgumentException("Tải trọng tối đa của xe phải lớn hơn 0.");
            }

            if (vehicle.MaxVolumeCapacity <= 0)
            {
                throw new ArgumentException("Thể tích tối đa của xe phải lớn hơn 0.");
            }

            _vehicleRepository.Add(vehicle);

            string actor = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
            _auditService?.Log(actor, "VEHICLE_CREATED", "Vehicle", vehicle.VehicleID, "Thêm phương tiện mới qua BUS.");
            return true;
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Người dùng không có quyền chỉnh sửa thông tin phương tiện.");
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Phương tiện không được để trống.");
            }

            Vehicle existing = _vehicleRepository.GetById(vehicle.VehicleID);
            if (existing == null)
            {
                return false;
            }

            if (vehicle.MaxWeightCapacity <= 0)
            {
                throw new ArgumentException("Tải trọng tối đa của xe phải lớn hơn 0.");
            }

            if (vehicle.MaxVolumeCapacity <= 0)
            {
                throw new ArgumentException("Thể tích tối đa của xe phải lớn hơn 0.");
            }

            _vehicleRepository.Update(vehicle);

            string actor = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
            _auditService?.Log(actor, "VEHICLE_UPDATED", "Vehicle", vehicle.VehicleID, "Cập nhật phương tiện qua BUS.");
            return true;
        }

        public bool DeleteVehicle(string vehicleId)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Người dùng không có quyền xóa phương tiện.");
            }

            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                return false;
            }

            Vehicle existing = _vehicleRepository.GetById(vehicleId);
            if (existing == null)
            {
                return false;
            }

            _vehicleRepository.Delete(vehicleId);

            string actor = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
            _auditService?.Log(actor, "VEHICLE_DELETED", "Vehicle", vehicleId, "Xóa phương tiện qua BUS.");
            return true;
        }
    }
}
