using System;
using Logistics.Core.Configuration;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Services.Implementations;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Utilities
{
    /// <summary>
    /// Simple Service Locator / Dependency Container.
    /// Initializes and wires all repositories and services (Pure DI).
    /// </summary>
    public static class DependencyContainer
    {
        private static string _dataDirectory = string.Empty;

        // ─── Repositories ────────────────────────────────────────────────────
        private static UserRepository _userRepository = null!;
        private static OrderRepository _orderRepository = null!;
        private static VehicleRepository _vehicleRepository = null!;
        private static DriverRepository _driverRepository = null!;
        private static DispatcherRepository _dispatcherRepository = null!;
        private static WarehouseStaffRepository _warehouseStaffRepository = null!;
        private static WarehouseRepository _warehouseRepository = null!;
        private static CustomerRepository _customerRepository = null!;
        private static AdminRepository _adminRepository = null!;
        private static TransactionRepository _transactionRepository = null!;
        private static ProblemReportRepository _problemReportRepository = null!;
        private static DeliveryTripRepository _deliveryTripRepository = null!;
        private static AuditLogRepository _auditLogRepository = null!;
        private static WarehouseInventoryLogRepository _warehouseInventoryLogRepository = null!;

        // ─── Services ────────────────────────────────────────────────────────
        private static IAuthService _authService = null!;
        private static IOrderService _orderService = null!;
        private static IDeliveryService _deliveryService = null!;
        private static IDispatchService _dispatchService = null!;
        private static IWarehouseService _warehouseService = null!;
        private static IReportService _reportService = null!;
        private static IRouteOptimizationService _routeOptimizationService = null!;
        private static IStaffManagementService _staffManagementService = null!;
        private static INotificationService _notificationService = null!;
        private static ICustomerService _customerService = null!;
        private static IMaintenanceService _maintenanceService = null!;
        private static ITransactionService _transactionService = null!;
        private static IProblemReportService _problemReportService = null!;
        private static IAccountManagementService _accountManagementService = null!;
        private static IAuditService _auditService = null!;
        private static IVehicleService _vehicleService = null!;

        public static void Initialize(string dataDirectory)
        {
            _dataDirectory = dataDirectory;

            // Seed data if JSON files don't exist
            DataSeeder.Seed(_dataDirectory);

            // ─── Init Repositories ───────────────────────────────────────────
            _userRepository          = new UserRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.UsersFile));
            _orderRepository         = new OrderRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.OrdersFile));
            _vehicleRepository       = new VehicleRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.VehiclesFile));
            _driverRepository        = new DriverRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.DriversFile));
            _dispatcherRepository    = new DispatcherRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.DispatchersFile));
            _warehouseStaffRepository= new WarehouseStaffRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.WarehouseStaffFile));
            _warehouseRepository     = new WarehouseRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.WarehousesFile));
            _warehouseRepository.SeedData(); // Thêm dữ liệu mẫu
            _customerRepository      = new CustomerRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.CustomersFile));
            _adminRepository         = new AdminRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.AdminsFile));
            _transactionRepository   = new TransactionRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.TransactionsFile));
            _problemReportRepository = new ProblemReportRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.ProblemReportsFile));
            _deliveryTripRepository  = new DeliveryTripRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.DeliveryTripsFile));
            _auditLogRepository      = new AuditLogRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.AuditLogsFile));
            _warehouseInventoryLogRepository = new WarehouseInventoryLogRepository(System.IO.Path.Combine(_dataDirectory, JsonConstants.WarehouseInventoryLogsFile));

            // ─── Init Services ───────────────────────────────────────────────
            BusinessRules businessRules = AppBusinessSettings.Load().ToBusinessRules();
            _auditService            = new AuditService(_auditLogRepository);
            _authService             = new AuthService(_userRepository, _auditService);
            _orderService            = new OrderService(_orderRepository, _customerRepository, _driverRepository, _vehicleRepository, businessRules);
            _deliveryService         = new DeliveryService(_driverRepository, _vehicleRepository, _orderRepository, _deliveryTripRepository, _transactionRepository, _customerRepository, businessRules);
            _notificationService     = new NotificationService();
            _dispatchService         = new DispatchService(_vehicleRepository, _orderRepository, _driverRepository, _deliveryTripRepository, _notificationService);
            _warehouseService        = new WarehouseService(_warehouseRepository, _notificationService, _warehouseInventoryLogRepository, _orderRepository);
            _reportService           = new ReportService(
                                           _orderRepository,
                                           _driverRepository,
                                           _dispatcherRepository,
                                           _warehouseStaffRepository,
                                           _adminRepository,
                                           _vehicleRepository,
                                           _customerRepository,
                                           _warehouseRepository,
                                           businessRules);
            _routeOptimizationService= new RouteOptimizationService(_driverRepository, _vehicleRepository);
            _staffManagementService  = new StaffManagementService(
                                           _driverRepository,
                                           _dispatcherRepository,
                                           _warehouseStaffRepository,
                                           _userRepository,
                                           _auditService);
            _customerService         = new CustomerService(_customerRepository, _auditService, businessRules);
            _maintenanceService      = new MaintenanceService(_vehicleRepository);
            _transactionService      = new TransactionService(_transactionRepository, _orderRepository, _customerRepository);
            _problemReportService    = new ProblemReportService(_problemReportRepository, _orderRepository);
            _vehicleService          = new VehicleService(_vehicleRepository, _auditService);
            _accountManagementService= new AccountManagementService(
                _userRepository, 
                _auditService,
                _driverRepository,
                _dispatcherRepository,
                _warehouseStaffRepository);
        }

        // ─── Service Getters ─────────────────────────────────────────────────
        public static IAuthService GetAuthService()
        {
            return _authService;
        }

        public static IOrderService GetOrderService()
        {
            return _orderService;
        }

        public static IDeliveryService GetDeliveryService()
        {
            return _deliveryService;
        }

        public static IDispatchService GetDispatchService()
        {
            return _dispatchService;
        }

        public static IWarehouseService GetWarehouseService()
        {
            return _warehouseService;
        }

        public static IReportService GetReportService()
        {
            return _reportService;
        }

        public static IRouteOptimizationService GetRouteOptimizationService()
        {
            return _routeOptimizationService;
        }

        public static IStaffManagementService GetStaffManagementService()
        {
            return _staffManagementService;
        }

        public static INotificationService GetNotificationService()
        {
            return _notificationService;
        }

        public static ICustomerService GetCustomerService()
        {
            return _customerService;
        }

        public static IMaintenanceService GetMaintenanceService()
        {
            return _maintenanceService;
        }

        public static ITransactionService GetTransactionService()
        {
            return _transactionService;
        }

        public static IProblemReportService GetProblemReportService()
        {
            return _problemReportService;
        }

        public static IAccountManagementService GetAccountManagementService()
        {
            return _accountManagementService;
        }

        public static IAuditService GetAuditService()
        {
            return _auditService;
        }

        public static IVehicleService GetVehicleService()
        {
            return _vehicleService;
        }

        // ─── Repository Getters (used directly by some forms) ────────────────
        public static UserRepository GetUserRepository()
        {
            return _userRepository;
        }

        public static OrderRepository GetOrderRepository()
        {
            return _orderRepository;
        }

        public static VehicleRepository GetVehicleRepository()
        {
            return _vehicleRepository;
        }

        public static DriverRepository GetDriverRepository()
        {
            return _driverRepository;
        }

        public static DispatcherRepository GetDispatcherRepository()
        {
            return _dispatcherRepository;
        }

        public static WarehouseStaffRepository GetWarehouseStaffRepository()
        {
            return _warehouseStaffRepository;
        }

        public static WarehouseRepository GetWarehouseRepository()
        {
            return _warehouseRepository;
        }

        public static CustomerRepository GetCustomerRepository()
        {
            return _customerRepository;
        }

        public static WarehouseInventoryLogRepository GetWarehouseInventoryLogRepository()
        {
            return _warehouseInventoryLogRepository;
        }
    }
}
