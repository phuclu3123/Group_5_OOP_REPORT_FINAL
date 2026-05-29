using System;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.Core.Validations;

namespace Logistics.Core.Services.Implementations
{
    /// <summary>
    /// Triển khai IStaffManagementService — quản lý CRUD cho Driver, Dispatcher, WarehouseStaff.
    /// </summary>
    public class StaffManagementService : IStaffManagementService
    {
        private readonly DriverRepository _driverRepo;
        private readonly DispatcherRepository _dispatcherRepo;
        private readonly WarehouseStaffRepository _warehouseStaffRepo;
        private readonly UserRepository? _userRepository;
        private readonly IAuditService? _auditService;

        // Dùng cho việc tự động sinh ID
        private int _driverCounter;
        private int _dispatcherCounter;
        private int _warehouseStaffCounter;

        public StaffManagementService(
            DriverRepository driverRepo,
            DispatcherRepository dispatcherRepo,
            WarehouseStaffRepository warehouseStaffRepo,
            UserRepository? userRepository = null,
            IAuditService? auditService = null)
        {
            _driverRepo         = driverRepo;
            _dispatcherRepo     = dispatcherRepo;
            _warehouseStaffRepo = warehouseStaffRepo;
            _userRepository     = userRepository;
            _auditService       = auditService;

            _driverCounter         = _driverRepo.Count();
            _dispatcherCounter     = _dispatcherRepo.Count();
            _warehouseStaffCounter = _warehouseStaffRepo.Count();
        }

        // ─────────────────────────────────────────────────────────────────────
        // ID Generators
        // ─────────────────────────────────────────────────────────────────────
        private string GenerateDriverId()
        {
            _driverCounter++;
            return "DRV" + _driverCounter.ToString("D4");
        }

        private string GenerateDispatcherId()
        {
            _dispatcherCounter++;
            return "DSP" + _dispatcherCounter.ToString("D4");
        }

        private string GenerateWarehouseStaffId()
        {
            _warehouseStaffCounter++;
            return "WHS" + _warehouseStaffCounter.ToString("D4");
        }

        private string GeneratePersonId(string prefix)
        {
            return prefix + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        // ─────────────────────────────────────────────────────────────────────
        // DRIVER
        // ─────────────────────────────────────────────────────────────────────
        public List<Driver> GetAllDrivers()
        {
            return _driverRepo.GetAll();
        }

        public Driver GetDriverById(string staffId)
        {
            return _driverRepo.GetById(staffId);
        }

        public Driver AddDriver(string fullName, string phone, string email, Address homeAddress,
                                DateTime birthDay, Gender gender, string accountId,
                                string department, decimal baseSalary, DateTime joinDate,
                                string licenseNumber, string licenseType, DateTime licenseExpiryDate)
        {
            string personId = GeneratePersonId("P");
            string staffId  = GenerateDriverId();

            Driver driver = new Driver(
                personId, fullName, phone, email, homeAddress, birthDay, gender, accountId,
                staffId, department, baseSalary, joinDate,
                licenseNumber, licenseType, licenseExpiryDate
            );

            EnsureValidDriver(driver);
            _driverRepo.Add(driver);
            _auditService?.Log(GetActor(), "STAFF_CREATED", "Driver", driver.StaffID, "Driver created: " + driver.FullName);
            return driver;
        }

        public bool UpdateDriverInfo(string staffId, string phone, string email, decimal baseSalary)
        {
            Driver driver = _driverRepo.GetById(staffId);
            if (driver == null) return false;

            Driver candidate = new Driver(
                driver.Id,
                driver.FullName,
                phone,
                email,
                driver.HomeAddress,
                driver.BirthDay,
                driver.Gender,
                driver.AccountID,
                driver.StaffID,
                driver.Department,
                baseSalary,
                driver.JoinDate,
                driver.LicenseNumber,
                driver.LicenseType,
                driver.LicenseExpiryDate);

            if (!IsValidDriver(candidate))
            {
                return false;
            }

            driver.UpdatePhoneNumber(phone);
            driver.UpdateEmail(email);
            driver.UpdateBaseSalary(baseSalary);
            _driverRepo.Update(driver);
            _auditService?.Log(GetActor(), "STAFF_UPDATED", "Driver", driver.StaffID, "Driver info updated.");
            return true;
        }

        public bool UpdateDriverStatus(string staffId, DriverStatus newStatus)
        {
            Driver driver = _driverRepo.GetById(staffId);
            if (driver == null) return false;

            driver.UpdateDriverStatus(newStatus);
            _driverRepo.Update(driver);
            return true;
        }

        public bool DeleteDriver(string staffId)
        {
            Driver driver = _driverRepo.GetById(staffId);
            if (driver == null) return false;

            driver.Resign();
            _driverRepo.Update(driver);
            DeactivateLinkedAccount(driver.AccountID);
            _auditService?.Log(GetActor(), "STAFF_RESIGNED", "Driver", driver.StaffID, "Driver soft-deleted/resigned.");
            return true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // DISPATCHER
        // ─────────────────────────────────────────────────────────────────────
        public List<Dispatcher> GetAllDispatchers()
        {
            return _dispatcherRepo.GetAll();
        }

        public Dispatcher GetDispatcherById(string staffId)
        {
            return _dispatcherRepo.GetById(staffId);
        }

        public Dispatcher AddDispatcher(string fullName, string phone, string email, Address homeAddress,
                                         DateTime birthDay, Gender gender, string accountId,
                                         string department, decimal baseSalary, DateTime joinDate,
                                         string managedRegion)
        {
            string personId = GeneratePersonId("P");
            string staffId  = GenerateDispatcherId();

            Dispatcher dispatcher = new Dispatcher(
                personId, fullName, phone, email, homeAddress, birthDay, gender, accountId,
                staffId, department, baseSalary, joinDate,
                managedRegion
            );

            EnsureValidDispatcher(dispatcher);
            _dispatcherRepo.Add(dispatcher);
            _auditService?.Log(GetActor(), "STAFF_CREATED", "Dispatcher", dispatcher.StaffID, "Dispatcher created: " + dispatcher.FullName);
            return dispatcher;
        }

        public bool UpdateDispatcherRegion(string staffId, string newRegion)
        {
            Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
            if (dispatcher == null) return false;
            if (string.IsNullOrWhiteSpace(newRegion)) return false;

            dispatcher.UpdateManagedRegion(newRegion);
            _dispatcherRepo.Update(dispatcher);
            return true;
        }

        public bool UpdateDispatcherKpi(string staffId, decimal kpiBonus)
        {
            Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
            if (dispatcher == null) return false;
            if (kpiBonus < 0) return false;

            dispatcher.UpdateKpiBonus(kpiBonus);
            _dispatcherRepo.Update(dispatcher);
            return true;
        }

        public bool DeleteDispatcher(string staffId)
        {
            Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
            if (dispatcher == null) return false;

            dispatcher.Resign();
            _dispatcherRepo.Update(dispatcher);
            DeactivateLinkedAccount(dispatcher.AccountID);
            _auditService?.Log(GetActor(), "STAFF_RESIGNED", "Dispatcher", dispatcher.StaffID, "Dispatcher soft-deleted/resigned.");
            return true;
        }

        // ─────────────────────────────────────────────────────────────────────
        // WAREHOUSE STAFF
        // ─────────────────────────────────────────────────────────────────────
        public List<WarehouseStaff> GetAllWarehouseStaff()
        {
            return _warehouseStaffRepo.GetAll();
        }

        public WarehouseStaff GetWarehouseStaffById(string staffId)
        {
            return _warehouseStaffRepo.GetById(staffId);
        }

        public WarehouseStaff AddWarehouseStaff(string fullName, string phone, string email, Address homeAddress,
                                                  DateTime birthDay, Gender gender, string accountId,
                                                  string department, decimal baseSalary, DateTime joinDate,
                                                  string warehouseId, string shift)
        {
            string personId = GeneratePersonId("P");
            string staffId  = GenerateWarehouseStaffId();

            WarehouseStaff staff = new WarehouseStaff(
                personId, fullName, phone, email, homeAddress, birthDay, gender, accountId,
                staffId, department, baseSalary, joinDate,
                warehouseId, shift
            );

            EnsureValidWarehouseStaff(staff);
            _warehouseStaffRepo.Add(staff);
            _auditService?.Log(GetActor(), "STAFF_CREATED", "WarehouseStaff", staff.StaffID, "Warehouse staff created: " + staff.FullName);
            return staff;
        }

        public bool UpdateWarehouseStaffShift(string staffId, string newShift)
        {
            WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
            if (staff == null) return false;
            if (string.IsNullOrWhiteSpace(newShift)) return false;

            staff.UpdateShift(newShift);
            _warehouseStaffRepo.Update(staff);
            return true;
        }

        public bool TransferWarehouseStaff(string staffId, string newWarehouseId)
        {
            WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
            if (staff == null) return false;
            if (string.IsNullOrWhiteSpace(newWarehouseId)) return false;

            staff.TransferWarehouse(newWarehouseId);
            _warehouseStaffRepo.Update(staff);
            return true;
        }

        public bool UpdateWarehouseStaffDetails(string staffId, string warehouseId, string shift)
        {
            WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
            if (staff == null) return false;
            if (string.IsNullOrWhiteSpace(warehouseId) || string.IsNullOrWhiteSpace(shift)) return false;

            staff.TransferWarehouse(warehouseId);
            staff.UpdateShift(shift);
            _warehouseStaffRepo.Update(staff);
            return true;
        }

        public bool DeleteWarehouseStaff(string staffId)
        {
            WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
            if (staff == null) return false;

            staff.Resign();
            _warehouseStaffRepo.Update(staff);
            DeactivateLinkedAccount(staff.AccountID);
            _auditService?.Log(GetActor(), "STAFF_RESIGNED", "WarehouseStaff", staff.StaffID, "Warehouse staff soft-deleted/resigned.");
            return true;
        }

        private static string GetActor()
        {
            return SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
        }

        private void DeactivateLinkedAccount(string accountId)
        {
            if (_userRepository == null || string.IsNullOrWhiteSpace(accountId))
            {
                return;
            }

            User user = _userRepository.FindByUsername(accountId);
            if (user == null)
            {
                return;
            }

            user.IsActive = false;
            _userRepository.Update(user);
        }

        // ─────────────────────────────────────────────────────────────────────
        // CHUNG
        // ─────────────────────────────────────────────────────────────────────
        public bool UpdateWorkStatus(string staffId, string role, WorkStatus newStatus)
        {
            if (role == "Driver")
            {
                Driver driver = _driverRepo.GetById(staffId);
                if (driver == null) return false;
                driver.UpdateWorkStatus(newStatus);
                _driverRepo.Update(driver);
                return true;
            }
            if (role == "Dispatcher")
            {
                Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
                if (dispatcher == null) return false;
                dispatcher.UpdateWorkStatus(newStatus);
                _dispatcherRepo.Update(dispatcher);
                return true;
            }
            if (role == "WarehouseStaff")
            {
                WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
                if (staff == null) return false;
                staff.UpdateWorkStatus(newStatus);
                _warehouseStaffRepo.Update(staff);
                return true;
            }
            return false;
        }

        public bool UpdateBaseSalary(string staffId, string role, decimal newSalary)
        {
            if (newSalary < 0)
            {
                return false;
            }

            if (role == "Driver")
            {
                Driver driver = _driverRepo.GetById(staffId);
                if (driver == null) return false;
                driver.UpdateBaseSalary(newSalary);
                _driverRepo.Update(driver);
                return true;
            }
            if (role == "Dispatcher")
            {
                Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
                if (dispatcher == null) return false;
                dispatcher.UpdateBaseSalary(newSalary);
                _dispatcherRepo.Update(dispatcher);
                return true;
            }
            if (role == "WarehouseStaff")
            {
                WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
                if (staff == null) return false;
                staff.UpdateBaseSalary(newSalary);
                _warehouseStaffRepo.Update(staff);
                return true;
            }
            return false;
        }

        public bool UpdateAccountId(string staffId, string role, string accountId)
        {
            if (role == "Driver")
            {
                Driver driver = _driverRepo.GetById(staffId);
                if (driver == null) return false;
                driver.UpdateAccountId(accountId);
                _driverRepo.Update(driver);
                return true;
            }
            if (role == "Dispatcher")
            {
                Dispatcher dispatcher = _dispatcherRepo.GetById(staffId);
                if (dispatcher == null) return false;
                dispatcher.UpdateAccountId(accountId);
                _dispatcherRepo.Update(dispatcher);
                return true;
            }
            if (role == "WarehouseStaff")
            {
                WarehouseStaff staff = _warehouseStaffRepo.GetById(staffId);
                if (staff == null) return false;
                staff.UpdateAccountId(accountId);
                _warehouseStaffRepo.Update(staff);
                return true;
            }
            return false;
        }

        private static bool IsValidDriver(Driver driver)
        {
            DriverValidator validator = new DriverValidator();
            ValidationResult validation = validator.Validate(driver);
            return validation.IsValid;
        }

        private static void EnsureValidDriver(Driver driver)
        {
            DriverValidator validator = new DriverValidator();
            ValidationResult validation = validator.Validate(driver);
            if (!validation.IsValid)
            {
                throw new ArgumentException(BuildValidationMessage("Driver", validation));
            }
        }

        private static void EnsureValidDispatcher(Dispatcher dispatcher)
        {
            DispatcherValidator validator = new DispatcherValidator();
            ValidationResult validation = validator.Validate(dispatcher);
            if (!validation.IsValid)
            {
                throw new ArgumentException(BuildValidationMessage("Dispatcher", validation));
            }
        }

        private static void EnsureValidWarehouseStaff(WarehouseStaff staff)
        {
            StaffValidator validator = new StaffValidator();
            ValidationResult validation = validator.Validate(staff);
            if (!validation.IsValid)
            {
                throw new ArgumentException(BuildValidationMessage("Warehouse staff", validation));
            }
        }

        private static string BuildValidationMessage(string entityName, ValidationResult validation)
        {
            return entityName + " data is invalid: " + string.Join(" ", validation.Errors);
        }
    }
}
