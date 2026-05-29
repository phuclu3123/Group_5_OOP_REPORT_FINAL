using System;
using System.Collections.Generic;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    /// <summary>
    /// Service quản lý nhân sự: thêm, sửa, xóa Driver / Dispatcher / WarehouseStaff.
    /// </summary>
    public interface IStaffManagementService
    {
        // ─── Driver ──────────────────────────────────────────────────────────
        List<Driver> GetAllDrivers();
        Driver GetDriverById(string staffId);
        Driver AddDriver(string fullName, string phone, string email, Address homeAddress,
                         DateTime birthDay, Gender gender, string accountId,
                         string department, decimal baseSalary, DateTime joinDate,
                         string licenseNumber, string licenseType, DateTime licenseExpiryDate);
        bool UpdateDriverInfo(string staffId, string phone, string email, decimal baseSalary);
        bool UpdateDriverStatus(string staffId, DriverStatus newStatus);
        bool DeleteDriver(string staffId);

        // ─── Dispatcher ──────────────────────────────────────────────────────
        List<Dispatcher> GetAllDispatchers();
        Dispatcher GetDispatcherById(string staffId);
        Dispatcher AddDispatcher(string fullName, string phone, string email, Address homeAddress,
                                  DateTime birthDay, Gender gender, string accountId,
                                  string department, decimal baseSalary, DateTime joinDate,
                                  string managedRegion);
        bool UpdateDispatcherRegion(string staffId, string newRegion);
        bool UpdateDispatcherKpi(string staffId, decimal kpiBonus);
        bool DeleteDispatcher(string staffId);

        // ─── WarehouseStaff ───────────────────────────────────────────────────
        List<WarehouseStaff> GetAllWarehouseStaff();
        WarehouseStaff GetWarehouseStaffById(string staffId);
        WarehouseStaff AddWarehouseStaff(string fullName, string phone, string email, Address homeAddress,
                                          DateTime birthDay, Gender gender, string accountId,
                                          string department, decimal baseSalary, DateTime joinDate,
                                          string warehouseId, string shift);
        bool UpdateWarehouseStaffShift(string staffId, string newShift);
        bool TransferWarehouseStaff(string staffId, string newWarehouseId);
        bool UpdateWarehouseStaffDetails(string staffId, string warehouseId, string shift);
        bool DeleteWarehouseStaff(string staffId);

        // ─── Chung ────────────────────────────────────────────────────────────
        bool UpdateWorkStatus(string staffId, string role, WorkStatus newStatus);
        bool UpdateBaseSalary(string staffId, string role, decimal newSalary);
        bool UpdateAccountId(string staffId, string role, string accountId);
    }
}
