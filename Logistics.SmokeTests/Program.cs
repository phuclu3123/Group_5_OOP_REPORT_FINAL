using System;
using System.Collections.Generic;
using System.IO;
using Logistics.Core.Configuration;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Implementations;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;

namespace Logistics.SmokeTests
{
    internal static class Program
    {
        private static int _failures;

        private static void Main()
        {
            string winFormsDataDir = @"C:\Users\ADMIN\OneDrive\Máy tính\OOP_huong_doi_tuong_C#\Cuoi_ky_OOP\Logistics.WinFormsUI\bin\Debug\net10.0-windows\Data";
            Console.WriteLine("UI DIAGNOSTIC FROM WINFORMS DIRECTORY:");
            try
            {
                VehicleRepository repo = new VehicleRepository(Path.Combine(winFormsDataDir, JsonConstants.VehiclesFile));
                var testVehicles = repo.GetAll();
                Console.WriteLine("LOADED VEHICLES COUNT: " + testVehicles.Count);
                for (int i = 0; i < testVehicles.Count; i++)
                {
                    Vehicle veh = testVehicles[i];
                    Console.WriteLine("Vehicle " + veh.VehicleID + " has " + (veh.MaintenanceHistory?.Count ?? 0) + " logs.");
                    if (veh.MaintenanceHistory != null)
                    {
                        for (int j = 0; j < veh.MaintenanceHistory.Count; j++)
                        {
                            var log = veh.MaintenanceHistory[j];
                            Console.WriteLine("  Log: ID=" + log.LogID + ", Cost=" + log.Cost + ", Desc=" + log.Description + ", Provider=" + log.ServiceProvider + ", Date=" + log.ServiceDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR LOADING WINFORMS DATA: " + ex);
            }

            string dataDirectory = Path.Combine(Path.GetTempPath(), "LogisticsSmoke_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            Directory.CreateDirectory(dataDirectory);

            BusinessRules rules = new BusinessRules();
            DataSeeder.Seed(dataDirectory);
            CheckOrderMigration(dataDirectory);

            UserRepository userRepository = new UserRepository(Path.Combine(dataDirectory, JsonConstants.UsersFile));
            OrderRepository orderRepository = new OrderRepository(Path.Combine(dataDirectory, JsonConstants.OrdersFile));
            VehicleRepository vehicleRepository = new VehicleRepository(Path.Combine(dataDirectory, JsonConstants.VehiclesFile));
            DriverRepository driverRepository = new DriverRepository(Path.Combine(dataDirectory, JsonConstants.DriversFile));
            DispatcherRepository dispatcherRepository = new DispatcherRepository(Path.Combine(dataDirectory, JsonConstants.DispatchersFile));
            WarehouseStaffRepository warehouseStaffRepository = new WarehouseStaffRepository(Path.Combine(dataDirectory, JsonConstants.WarehouseStaffFile));
            AdminRepository adminRepository = new AdminRepository(Path.Combine(dataDirectory, JsonConstants.AdminsFile));
            CustomerRepository customerRepository = new CustomerRepository(Path.Combine(dataDirectory, JsonConstants.CustomersFile));
            WarehouseRepository warehouseRepository = new WarehouseRepository(Path.Combine(dataDirectory, JsonConstants.WarehousesFile));
            TransactionRepository transactionRepository = new TransactionRepository(Path.Combine(dataDirectory, JsonConstants.TransactionsFile));
            ProblemReportRepository problemReportRepository = new ProblemReportRepository(Path.Combine(dataDirectory, JsonConstants.ProblemReportsFile));
            DeliveryTripRepository deliveryTripRepository = new DeliveryTripRepository(Path.Combine(dataDirectory, JsonConstants.DeliveryTripsFile));
            AuditLogRepository auditLogRepository = new AuditLogRepository(Path.Combine(dataDirectory, JsonConstants.AuditLogsFile));
            WarehouseInventoryLogRepository inventoryLogRepository = new WarehouseInventoryLogRepository(Path.Combine(dataDirectory, JsonConstants.WarehouseInventoryLogsFile));

            IAuditService auditService = new AuditService(auditLogRepository);
            AuthService authService = new AuthService(userRepository, auditService);
            User? admin = authService.Login("admin", "Admin@123");
            Check(admin != null, "Dang nhap admin seed");
            if (admin == null)
            {
                Finish(dataDirectory);
                return;
            }

            SessionManager.Login(admin);

            OrderService orderService = new OrderService(orderRepository, customerRepository, driverRepository, vehicleRepository, rules);
            WarehouseService warehouseService = new WarehouseService(warehouseRepository, new NotificationService(), inventoryLogRepository, orderRepository);
            DispatchService dispatchService = new DispatchService(vehicleRepository, orderRepository, driverRepository, deliveryTripRepository, new NotificationService());
            DeliveryService deliveryService = new DeliveryService(driverRepository, vehicleRepository, orderRepository, deliveryTripRepository, transactionRepository, customerRepository, rules);
            TransactionService transactionService = new TransactionService(transactionRepository, orderRepository, customerRepository);
            ProblemReportService problemReportService = new ProblemReportService(problemReportRepository, orderRepository);
            ReportService reportService = new ReportService(orderRepository, driverRepository, dispatcherRepository, warehouseStaffRepository, adminRepository, vehicleRepository, customerRepository, warehouseRepository, rules);
            RouteOptimizationService routeOptimizationService = new RouteOptimizationService(driverRepository, vehicleRepository);
            CustomerService customerService = new CustomerService(customerRepository, auditService, rules);
            StaffManagementService staffManagementService = new StaffManagementService(driverRepository, dispatcherRepository, warehouseStaffRepository, userRepository, auditService);

            RunColdChainDeliveryFlow(orderService, warehouseService, dispatchService, deliveryService, transactionService, reportService, routeOptimizationService, customerRepository);
            RunProblemReportFlow(orderService, problemReportService);
            RunCreditLimitFlow(orderService, transactionService);
            RunPackageEdgeCases(orderService);
            RunStatusGuardFlow(orderService, dispatchService, deliveryService);
            RunDataValidationRules(customerService, staffManagementService);
            RunAuthRules(authService);

            Finish(dataDirectory);
        }

        private static void CheckOrderMigration(string dataDirectory)
        {
            string badOrderPath = Path.Combine(dataDirectory, "blank_orders.json");
            List<Order> blankOrders = new List<Order>();
            blankOrders.Add(new Order());
            blankOrders.Add(new Order());
            JsonHelper.WriteAll<Order>(badOrderPath, blankOrders);

            OrderRepository repository = new OrderRepository(badOrderPath);
            List<Order> migratedOrders = repository.GetAll();
            Check(migratedOrders.Count > 0 && !string.IsNullOrWhiteSpace(migratedOrders[0].TrackingNumber), "Migration thay the order rong bang du lieu hop le");
        }

        private static void RunColdChainDeliveryFlow(OrderService orderService, WarehouseService warehouseService, DispatchService dispatchService, DeliveryService deliveryService, TransactionService transactionService, ReportService reportService, RouteOptimizationService routeOptimizationService, CustomerRepository customerRepository)
        {
            Order order = orderService.CreateOrder("CUST001", "CUST002", "10 Pasteur, Quan 1, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Express);
            Package package = orderService.CreatePackage(order.TrackingNumber, "Mau y te can giu mat", 6.5, "40x30x20", true, 2500000m, "Y te", "Giu mat 2-8C");

            Check(orderService.AddPackageToOrder(order.TrackingNumber, package), "Them kien hang vao don moi");
            decimal cost = orderService.CalculateOrderCost(order.TrackingNumber);
            Check(cost > 0, "Tinh cuoc theo cau hinh BusinessRules");

            Order loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            Package? loadedPackage = loadedOrder.FindPackage(package.PackageID);
            Check(loadedPackage != null, "Tai lai kien hang tu repository");
            if (loadedPackage == null)
            {
                return;
            }

            Check(warehouseService.StorePackage("W004", loadedPackage, "COLD-A1"), "Nhap kho lanh co vi tri ke");
            loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            loadedPackage = loadedOrder.FindPackage(package.PackageID);
            Check(loadedOrder.CurrentStatus == OrderStatus.ArrivedAtWarehouse, "Don hang cap nhat trang thai da nhap kho");
            Check(loadedPackage != null && loadedPackage.IsCurrentlyInWarehouse("W004"), "Kien hang luu kho hien tai");

            Check(warehouseService.ReleasePackage("W004", loadedPackage!), "Xuat kho dung kien hang");
            loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            Check(loadedOrder.CurrentStatus == OrderStatus.ReadyForDispatch, "Don hang san sang dieu phoi sau xuat kho");

            string suggestion = routeOptimizationService.SuggestDriverAndVehicle(loadedOrder);
            Check(suggestion.Contains("VEH004"), "Goi y xe lanh cho hang y te/giu mat");

            List<string> orderIds = new List<string>();
            orderIds.Add(order.TrackingNumber);
            DeliveryTripDTO trip = dispatchService.CreateTripDispatch("VEH004", "DRV0001", orderIds);
            Check(!string.IsNullOrWhiteSpace(trip.TripID), "Tao chuyen giao co xe va tai xe");

            Check(deliveryService.CompleteDelivery(order.TrackingNumber), "Hoan thanh giao hang");
            loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            Check(loadedOrder.CurrentStatus == OrderStatus.Delivered, "Don hang sang Delivered");

            Transaction payment = transactionService.CreatePayment(order.TrackingNumber, loadedOrder.TotalCost, PaymentMethod.Banking);
            Check(payment.Status == TransactionStatus.Completed, "Ghi nhan thanh toan Banking");

            string invoice = reportService.GenerateInvoice(order.TrackingNumber);
            Check(invoice.Contains(order.TrackingNumber) && invoice.Contains("TONG THANH TOAN"), "Tao hoa don van chuyen");

            Logistics.Core.Models.Actors.Customer customer = customerRepository.GetById("CUST001");
            Check(customer != null && customer.LoyaltyPoints > 0, "Cong diem khach hang sau giao thanh cong");
        }

        private static void RunProblemReportFlow(OrderService orderService, ProblemReportService problemReportService)
        {
            Order order = orderService.CreateOrder("CUST002", "CUST003", "25 Ly Tu Trong, Quan 1, TP.HCM", "88 Cach Mang Thang 8, Quan 3, TP.HCM", ServiceType.Standard);
            Package package = orderService.CreatePackage(order.TrackingNumber, "May quet ma vach", 2.2, "35x25x15", true, 4500000m, "Dien tu", "Can xu ly nhe");
            orderService.AddPackageToOrder(order.TrackingNumber, package);
            orderService.CalculateOrderCost(order.TrackingNumber);

            ProblemReport report = problemReportService.CreateReport(order.TrackingNumber, IssueType.Damaged, "Vo man hinh khi kiem hang.");
            Order loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            Package? loadedPackage = loadedOrder.FindPackage(package.PackageID);
            Check(!string.IsNullOrWhiteSpace(report.ReportID), "Lap bien ban su co");
            Check(loadedOrder.CurrentStatus == OrderStatus.Failed, "Su co hu hong chuyen don sang Failed terminal");
            Check(loadedPackage != null && loadedPackage.Status == PackageStatus.Damaged, "Kien hang duoc danh dau hu hong");
        }

        private static void RunCreditLimitFlow(OrderService orderService, TransactionService transactionService)
        {
            Order order = orderService.CreateOrder("CUST003", "CUST001", "88 Cach Mang Thang 8, Quan 3, TP.HCM", "10 Pasteur, Quan 1, TP.HCM", ServiceType.Express);
            Package package = orderService.CreatePackage(order.TrackingNumber, "Lo hang gia tri lon", 100, "120x80x80", false, 60000000m, "Vat tu", "");
            orderService.AddPackageToOrder(order.TrackingNumber, package);
            orderService.CalculateOrderCost(order.TrackingNumber);

            bool rejected = false;
            try
            {
                transactionService.CreatePayment(order.TrackingNumber, 6000000m, PaymentMethod.Credit);
            }
            catch (InvalidOperationException)
            {
                rejected = true;
            }

            Check(rejected, "Chan thanh toan cong no vuot han muc Standard");
        }

        private static void RunPackageEdgeCases(OrderService orderService)
        {
            Order order = orderService.CreateOrder("CUST001", "CUST002", "10 Pasteur, Quan 1, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Standard);
            Package package = orderService.CreatePackage(order.TrackingNumber, "Thiet bi dien tu", 3.5, "30X20X15", false, 3500000m, "dien tu", "");

            Check(package.VolumeWeight > 0, "Tinh khoi luong quy doi voi kich thuoc dung chu X hoa");
            Check(package.CheckIfFragile(), "Nhan dien hang dien tu de vo khong phan biet hoa thuong");
            Check(orderService.AddPackageToOrder(order.TrackingNumber, package), "Them kien hang voi kich thuoc dung chu X hoa");

            bool updated = orderService.UpdatePackage(order.TrackingNumber, package.PackageID, "Du lieu sai", -1, "10x10x10", false, 0, "General", "");
            Order loadedOrder = orderService.GetOrderById(order.TrackingNumber);
            Package? loadedPackage = loadedOrder.FindPackage(package.PackageID);

            Check(!updated, "Tu choi cap nhat kien hang khong hop le");
            Check(loadedPackage != null && loadedPackage.ActualWeight == 3.5 && loadedPackage.Description == "Thiet bi dien tu", "Cap nhat khong hop le khong lam thay doi kien hang cu");
        }

        private static void RunStatusGuardFlow(OrderService orderService, DispatchService dispatchService, DeliveryService deliveryService)
        {
            Order order = orderService.CreateOrder("CUST001", "CUST002", "10 Pasteur, Quan 1, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Standard);
            Package package = orderService.CreatePackage(order.TrackingNumber, "Ho so giao nhanh", 1.2, "20x15x5", false, 100000m, "Tai lieu", "");
            orderService.AddPackageToOrder(order.TrackingNumber, package);
            orderService.CalculateOrderCost(order.TrackingNumber);

            List<string> orderIds = new List<string>();
            orderIds.Add(order.TrackingNumber);
            DeliveryTripDTO trip = dispatchService.CreateTripDispatch("VEH004", "DRV0001", orderIds);
            Check(!string.IsNullOrWhiteSpace(trip.TripID), "Tao chuyen de kiem tra trang thai OutForDelivery");
            Check(orderService.UpdateOrderStatus(order.TrackingNumber, OrderStatus.OutForDelivery), "Chuyen don dang van chuyen sang OutForDelivery");
            Check(deliveryService.CompleteDelivery(order.TrackingNumber), "Hoan thanh don hang o trang thai OutForDelivery");

            Order deliveredOrder = orderService.GetOrderById(order.TrackingNumber);
            Check(deliveredOrder.CurrentStatus == OrderStatus.Delivered, "Don OutForDelivery sang Delivered");
            Check(!dispatchService.AssignVehicleToOrder("VEH004", order.TrackingNumber), "Khong dieu phoi lai don da ket thuc");
        }

        private static void RunAuthRules(AuthService authService)
        {
            Check(!authService.ResetPassword("admin", "weak"), "Chan reset mat khau yeu o service");

            User weakUser = new User
            {
                Username = "weak_customer",
                PasswordHash = "weak",
                Role = UserRole.Customer,
                SecurityQuestion = "Color?",
                SecurityAnswerHash = PasswordHasher.HashPassword("blue")
            };
            Check(!authService.Register(weakUser), "Chan dang ky mat khau yeu o service");
        }

        private static void RunDataValidationRules(CustomerService customerService, StaffManagementService staffManagementService)
        {
            Address address = new Address("1 Nguyen Hue", "Ben Nghe", "Quan 1", "TP.HCM", "700000", "Viet Nam");

            bool invalidCustomerRejected = false;
            try
            {
                customerService.AddCustomer("A", "123", "bad-email", address, CustomerType.Standard, -1);
            }
            catch (ArgumentException)
            {
                invalidCustomerRejected = true;
            }

            Check(invalidCustomerRejected, "Chan tao khach hang sai du lieu");
            Check(!customerService.UpdateCustomerPolicy("CUST001", CustomerType.VIP, -1), "Chan cap nhat han muc tin dung am");

            bool invalidDriverRejected = false;
            try
            {
                staffManagementService.AddDriver(
                    "Le",
                    "123",
                    "bad-email",
                    address,
                    DateTime.Now.AddYears(-17),
                    Gender.Male,
                    "driver_bad",
                    "Transport",
                    -1,
                    DateTime.Now,
                    "",
                    "",
                    DateTime.Now.AddDays(-1));
            }
            catch (ArgumentException)
            {
                invalidDriverRejected = true;
            }

            Check(invalidDriverRejected, "Chan tao tai xe sai du lieu");
            Check(!staffManagementService.UpdateBaseSalary("DRV0001", "Driver", -1), "Chan cap nhat luong am");
            Check(!staffManagementService.UpdateDispatcherKpi("DSP0001", -1), "Chan KPI dieu phoi am");
            Check(!staffManagementService.UpdateWarehouseStaffDetails("WHS0001", "", "Ca ngay"), "Chan thong tin nhan vien kho thieu ma kho");
        }

        private static void Check(bool condition, string name)
        {
            if (condition)
            {
                Console.WriteLine("[PASS] " + name);
                return;
            }

            _failures++;
            Console.WriteLine("[FAIL] " + name);
        }

        private static void Finish(string dataDirectory)
        {
            Console.WriteLine("Data: " + dataDirectory);
            if (_failures > 0)
            {
                Console.WriteLine("Smoke test failed: " + _failures);
                Environment.Exit(1);
            }

            Console.WriteLine("Smoke test passed.");
        }
    }
}
