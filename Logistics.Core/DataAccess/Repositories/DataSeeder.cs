using System;
using System.Collections.Generic;
using System.IO;
using Logistics.Core.Configuration;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Implementations;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;

namespace Logistics.Core.DataAccess.Repositories
{
    /// <summary>
    /// Seeds initial JSON data files nếu chưa tồn tại.
    /// Tự động tạo tài khoản admin/admin123 và dữ liệu mẫu để test UI ngay.
    /// </summary>
    public static class DataSeeder
    {
        public static void Seed(string dataDirectory)
        {
            JsonHelper.EnsureDirectory(dataDirectory);



            SeedUsers(dataDirectory);
            SeedAdmins(dataDirectory);
            SeedDrivers(dataDirectory);
            SeedDispatchers(dataDirectory);
            SeedWarehouseStaff(dataDirectory);
            SeedCustomers(dataDirectory);
            SeedVehicles(dataDirectory);
            SeedWarehouses(dataDirectory);
            SeedOrders(dataDirectory);
            SeedTransactions(dataDirectory);
            SeedProblemReports(dataDirectory);
            SeedDeliveryTrips(dataDirectory);
            SeedWarehouseInventoryLogs(dataDirectory);
            SeedAuditLogs(dataDirectory);
        }

        // ─── USERS ────────────────────────────────────────────────────────────
        private static void SeedUsers(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.UsersFile);
            if (System.IO.File.Exists(filePath))
            {
                List<User> existingUsers = JsonHelper.ReadAll<User>(filePath);
                EnsureTestAccounts(existingUsers);
                JsonHelper.WriteAll<User>(filePath, existingUsers);
                return;
            }

            List<User> defaultUsers = new List<User>();

            User admin = CreateSeedAdminUser();
            defaultUsers.Add(admin);

            User driverUser = CreateSeedDriverUser("driver", "Driver@123", "DRV0001", "Nguyễn Văn An");
            defaultUsers.Add(driverUser);

            User dispatcherUser = CreateSeedDispatcherUser();
            defaultUsers.Add(dispatcherUser);

            User tester = CreateSeedTesterUser();
            defaultUsers.Add(tester);

            EnsureTestAccounts(defaultUsers);

            JsonHelper.WriteAll<User>(filePath, defaultUsers);
        }

        private static void EnsureTestAccounts(List<User> users)
        {
            UpsertSeedUser(users, CreateSeedAdminUser());
            UpsertSeedUser(users, CreateSeedTesterUser());
            UpsertSeedUser(users, CreateSeedDriverUser("driver", "Driver@123", "DRV0001", "Nguyễn Văn An"));
            UpsertSeedUser(users, CreateSeedDriverUser("driver02", "Driver02@123", "DRV0002", "Trần Thị Bình"));
            UpsertSeedUser(users, CreateSeedDriverUser("driver03", "Driver03@123", "DRV0003", "Lê Văn Cường"));
            UpsertSeedUser(users, CreateSeedDriverUser("driver04", "Driver04@123", "DRV0004", "Đỗ Quốc Huy"));
            UpsertSeedUser(users, CreateSeedDriverUser("driver05", "Driver05@123", "DRV0005", "Nguyễn Minh Khang"));
            UpsertSeedUser(users, CreateSeedDispatcherUser());
            UpsertSeedUser(users, CreateSeedDispatcherUser("dispatcher02", "Dispatch02@123", "DSP0002", "Võ Thị Thanh", "Miền Trung"));
            UpsertSeedUser(users, CreateSeedWarehouseUser());
            UpsertSeedUser(users, CreateSeedWarehouseUser("ws02", "Warehouse02@123", "WHS0002", "Nguyễn Thị Lan", "WH003"));
            UpsertSeedUser(users, CreateSeedWarehouseUser("ws03", "Warehouse03@123", "WHS0003", "Bùi Văn Sơn", "WH002"));
            UpsertSeedUser(users, CreateSeedCustomerUser());
            UpsertSeedUser(users, CreateSeedCustomerUser("customer02", "Customer02@123"));
        }

        private static void UpsertSeedUser(List<User> users, User seed)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (string.Equals(users[i].Username, seed.Username, StringComparison.OrdinalIgnoreCase))
                {
                    if (users[i].Person == null)
                    {
                        users[i].Person = seed.Person;
                    }

                    return;
                }
            }

            users.Add(seed);
        }

        private static User CreateSeedAdminUser()
        {
            User user = new User();
            user.Username = EnvHelper.Get("INITIAL_ADMIN_USERNAME", "admin");
            user.PasswordHash = PasswordHasher.HashPassword(EnvHelper.Get("INITIAL_ADMIN_PASSWORD", "Admin@123"));
            user.Role = UserRole.Admin;
            user.SecurityQuestion = EnvHelper.Get("INITIAL_ADMIN_SECURITY_QUESTION", "What is your favorite color?");
            user.SecurityAnswerHash = PasswordHasher.HashPassword(EnvHelper.Get("INITIAL_ADMIN_SECURITY_ANSWER", "blue").Trim().ToLower());
            user.MustChangePassword = false;
            user.Person = new Dispatcher(
                "P_ADM001", "Nguyễn Hoàng Nam", "0900000000", "admin@logistics.vn",
                new Address("1 Lý Tự Trọng", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1985, 1, 1), Gender.Male, user.Username,
                "ADM001", "Ban Quản Trị", 50_000_000m, DateTime.Now, "Toàn quốc"
            );
            return user;
        }

        private static User CreateSeedTesterUser()
        {
            User user = new User();
            user.Username = "tester";
            user.PasswordHash = PasswordHasher.HashPassword("Tester@123");
            user.Role = UserRole.Admin;
            user.SecurityQuestion = "What is your favorite color?";
            user.SecurityAnswerHash = PasswordHasher.HashPassword("blue");
            user.AvatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "tester_avatar.png");
            user.Person = new Dispatcher(
                "P_ADM002", "Phạm Thu Hà", "0900000001", "ha.admin@logistics.vn",
                new Address("27 Nguyễn Đình Chiểu", "Phường Đa Kao", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1987, 9, 18), Gender.Female, user.Username,
                "ADM002", "Điều Hành Vận Hành", 32_000_000m, DateTime.Now, "Toàn quốc"
            );
            return user;
        }

        private static User CreateSeedDriverUser(string username, string password, string staffId, string fullName)
        {
            User user = new User();
            user.Username = username;
            user.PasswordHash = PasswordHasher.HashPassword(password);
            user.Role = UserRole.Driver;
            user.SecurityQuestion = "What is your pet's name?";
            user.SecurityAnswerHash = PasswordHasher.HashPassword("lucky");
            user.Person = new Driver(
                "P_" + staffId, fullName, "0901234567", username + "@logistics.vn",
                new Address("12 Lê Lợi", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1990, 5, 15), Gender.Male, username,
                staffId, "Vận chuyển", 8_000_000m, new DateTime(2022, 3, 1),
                "83B-12345", "B2", new DateTime(2027, 3, 1));
            return user;
        }

        private static User CreateSeedDispatcherUser()
        {
            return CreateSeedDispatcherUser("dispatcher01", "Dispatch@123", "DSP0001", "Phạm Minh Tuấn", "TP.HCM - Miền Nam");
        }

        private static User CreateSeedDispatcherUser(string username, string password, string staffId, string fullName, string region)
        {
            User user = new User();
            user.Username = username;
            user.PasswordHash = PasswordHasher.HashPassword(password);
            user.Role = UserRole.Dispatcher;
            user.SecurityQuestion = "What city were you born in?";
            user.SecurityAnswerHash = PasswordHasher.HashPassword("hanoi");
            user.Person = new Dispatcher(
                "P_" + staffId, fullName, "0934567890", username + "@logistics.vn",
                new Address("99 Đinh Tiên Hoàng", "Phường 3", "Bình Thạnh", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1985, 4, 12), Gender.Male, user.Username,
                staffId, "Điều phối", 12_000_000m, new DateTime(2020, 2, 1),
                region);
            return user;
        }

        private static User CreateSeedWarehouseUser()
        {
            return CreateSeedWarehouseUser("ws01", "Warehouse@123", "WHS0001", "Hoàng Thị Dung", "WH001");
        }

        private static User CreateSeedWarehouseUser(string username, string password, string staffId, string fullName, string warehouseId)
        {
            User user = new User();
            user.Username = username;
            user.PasswordHash = PasswordHasher.HashPassword(password);
            user.Role = UserRole.WarehouseStaff;
            user.SecurityQuestion = "What city were you born in?";
            user.SecurityAnswerHash = PasswordHasher.HashPassword("hcm");
            user.Person = new WarehouseStaff(
                "P_" + staffId, fullName, "0945678901", username + "@logistics.vn",
                new Address("500 Đại Lộ Bình Dương", "Phường Lái Thiêu", "Thị xã Thuận An", "Bình Dương", "820000", "Việt Nam"),
                new DateTime(1992, 7, 8), Gender.Female, user.Username,
                staffId, "Kho vận", 7_000_000m, new DateTime(2021, 9, 1),
                warehouseId, "Ca ngày");
            return user;
        }

        private static User CreateSeedCustomerUser()
        {
            return CreateSeedCustomerUser("customer01", "Customer@123");
        }

        private static User CreateSeedCustomerUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.PasswordHash = PasswordHasher.HashPassword(password);
            user.Role = UserRole.Customer;
            user.SecurityQuestion = "What is your favorite color?";
            user.SecurityAnswerHash = PasswordHasher.HashPassword("blue");
            return user;
        }

        // ─── ADMINS ──────────────────────────────────────────────────────────
        private static void SeedAdmins(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.AdminsFile);
            List<Admin> admins = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Admin>(filePath)
                : new List<Admin>();
            bool changed = false;

            AddAdminIfMissing(admins, new Admin(
                "P_ADM001", "Nguyễn Hoàng Nam", "0900000000", "admin@logistics.vn",
                new Address("1 Lý Tự Trọng", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1985, 1, 1), Gender.Male, "admin",
                "ADM001", "Ban Quản Trị", 50_000_000m, new DateTime(2018, 1, 1),
                "ADM-SYSTEM", 8_000_000m), ref changed);

            AddAdminIfMissing(admins, new Admin(
                "P_ADM002", "Phạm Thu Hà", "0900000001", "ha.admin@logistics.vn",
                new Address("27 Nguyễn Đình Chiểu", "Phường Đa Kao", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1987, 9, 18), Gender.Female, "tester",
                "ADM002", "Điều Hành Vận Hành", 32_000_000m, new DateTime(2019, 6, 10),
                "ADM-OPS", 5_000_000m), ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Admin>(filePath, admins);
            }
        }

        // ─── DRIVERS ──────────────────────────────────────────────────────────
        private static void SeedDrivers(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.DriversFile);
            List<Driver> drivers = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Driver>(filePath)
                : new List<Driver>();
            bool changed = false;

            Address addr1 = new Address("12 Lê Lợi", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam");
            Driver d1 = new Driver(
                "P_DRV001", "Nguyễn Văn An", "0901234567", "an.driver@logistics.vn",
                addr1, new DateTime(1990, 5, 15), Gender.Male, "driver",
                "DRV0001", "Vận chuyển", 8_000_000m, new DateTime(2022, 3, 1),
                "83B-12345", "B2", new DateTime(2027, 3, 1)
            );
            d1.UpdateCurrentLocation(new GeoPoint(10.7769, 106.7009));
            d1.RecordDelivery();
            d1.RecordDelivery();
            AddDriverIfMissing(drivers, d1, ref changed);

            Address addr2 = new Address("45 Nguyễn Huệ", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam");
            Driver d2 = new Driver(
                "P_DRV002", "Trần Thị Bình", "0912345678", "binh.driver@logistics.vn",
                addr2, new DateTime(1988, 8, 20), Gender.Female, "driver02",
                "DRV0002", "Vận chuyển", 9_000_000m, new DateTime(2021, 6, 15),
                "51B-67890", "C", new DateTime(2026, 6, 15)
            );
            d2.UpdateCurrentLocation(new GeoPoint(10.8231, 106.6297));
            d2.RecordDelivery();
            AddDriverIfMissing(drivers, d2, ref changed);

            Address addr3 = new Address("78 Trần Hưng Đạo", "Phường 7", "Quận 5", "TP.HCM", "700000", "Việt Nam");
            Driver d3 = new Driver(
                "P_DRV003", "Lê Văn Cường", "0923456789", "cuong.driver@logistics.vn",
                addr3, new DateTime(1995, 11, 30), Gender.Male, "driver03",
                "DRV0003", "Vận chuyển", 7_500_000m, new DateTime(2023, 1, 10),
                "51B-11111", "B2", new DateTime(2028, 1, 10)
            );
            d3.UpdateCurrentLocation(new GeoPoint(10.7944, 106.6436));
            AddDriverIfMissing(drivers, d3, ref changed);

            Driver d4 = new Driver(
                "P_DRV004", "Đỗ Quốc Huy", "0938123456", "huy.driver@logistics.vn",
                new Address("18 Võ Văn Kiệt", "Cầu Ông Lãnh", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1989, 2, 9), Gender.Male, "driver04",
                "DRV0004", "Vận chuyển liên tỉnh", 10_500_000m, new DateTime(2020, 11, 5),
                "50H-77889", "C", new DateTime(2029, 11, 5));
            d4.UpdateCurrentLocation(new GeoPoint(11.9404, 108.4583));
            d4.UpdateDriverStatus(DriverStatus.Busy);
            d4.RecordDelivery();
            d4.RecordDelivery();
            d4.RecordDelivery();
            AddDriverIfMissing(drivers, d4, ref changed);

            Driver d5 = new Driver(
                "P_DRV005", "Nguyễn Minh Khang", "0966123456", "khang.driver@logistics.vn",
                new Address("32 Nguyễn Văn Linh", "Tân Phong", "Quận 7", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1994, 12, 2), Gender.Male, "driver05",
                "DRV0005", "Giao nội thành", 7_800_000m, new DateTime(2024, 4, 1),
                "59K-45678", "B2", new DateTime(2028, 4, 1));
            d5.UpdateDriverStatus(DriverStatus.OffDuty);
            d5.AddAccidentRecord("Va quet nhe, da lap bien ban va dao tao lai quy trinh an toan.");
            AddDriverIfMissing(drivers, d5, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Driver>(filePath, drivers);
            }
        }

        // ─── DISPATCHERS ──────────────────────────────────────────────────────
        private static void SeedDispatchers(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.DispatchersFile);
            List<Dispatcher> dispatchers = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Dispatcher>(filePath)
                : new List<Dispatcher>();
            bool changed = false;

            Address addr1 = new Address("99 Đinh Tiên Hoàng", "Phường 3", "Bình Thạnh", "TP.HCM", "700000", "Việt Nam");
            Dispatcher disp1 = new Dispatcher(
                "P_DSP001", "Phạm Minh Tuấn", "0934567890", "tuan.dispatch@logistics.vn",
                addr1, new DateTime(1985, 4, 12), Gender.Male, "dispatcher01",
                "DSP0001", "Điều phối", 12_000_000m, new DateTime(2020, 2, 1),
                "TP.HCM - Miền Nam"
            );
            disp1.UpdateKpiBonus(1_500_000m);
            AddDispatcherIfMissing(dispatchers, disp1, ref changed);

            Dispatcher disp2 = new Dispatcher(
                "P_DSP002", "Võ Thị Thanh", "0934567891", "thanh.dispatch@logistics.vn",
                new Address("12 Phan Chu Trinh", "Hải Châu 1", "Hải Châu", "Đà Nẵng", "550000", "Việt Nam"),
                new DateTime(1991, 10, 3), Gender.Female, "dispatcher02",
                "DSP0002", "Điều phối", 11_500_000m, new DateTime(2021, 8, 16),
                "Miền Trung"
            );
            disp2.UpdateRegionAllowance(1_800_000m);
            AddDispatcherIfMissing(dispatchers, disp2, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Dispatcher>(filePath, dispatchers);
            }
        }

        // ─── WAREHOUSE STAFF ──────────────────────────────────────────────────
        private static void SeedWarehouseStaff(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.WarehouseStaffFile);
            List<WarehouseStaff> staffList = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<WarehouseStaff>(filePath)
                : new List<WarehouseStaff>();
            bool changed = false;

            Address addr1 = new Address("500 Bình Dương", "Lái Thiêu", "Thuận An", "Bình Dương", "820000", "Việt Nam");
            WarehouseStaff ws1 = new WarehouseStaff(
                "P_WHS001", "Hoàng Thị Dung", "0945678901", "dung.warehouse@logistics.vn",
                addr1, new DateTime(1992, 7, 8), Gender.Female, "ws01",
                "WHS0001", "Kho vận", 7_000_000m, new DateTime(2021, 9, 1),
                "WH001", "Ca ngày"
            );
            AddWarehouseStaffIfMissing(staffList, ws1, ref changed);

            WarehouseStaff ws2 = new WarehouseStaff(
                "P_WHS002", "Nguyễn Thị Lan", "0945678902", "lan.warehouse@logistics.vn",
                new Address("Khu Công Nghệ Cao", "Long Thạnh Mỹ", "Thủ Đức", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1994, 3, 22), Gender.Female, "ws02",
                "WHS0002", "Kho vận", 7_200_000m, new DateTime(2022, 5, 12),
                "WH003", "Ca tối"
            );
            AddWarehouseStaffIfMissing(staffList, ws2, ref changed);

            WarehouseStaff ws3 = new WarehouseStaff(
                "P_WHS003", "Bùi Văn Sơn", "0945678903", "son.warehouse@logistics.vn",
                new Address("Lô 12 KCN Hòa Cầm", "Hòa Thọ Tây", "Cẩm Lệ", "Đà Nẵng", "550000", "Việt Nam"),
                new DateTime(1990, 1, 6), Gender.Male, "ws03",
                "WHS0003", "Kho vận", 7_800_000m, new DateTime(2020, 10, 20),
                "WH002", "Ca đêm"
            );
            AddWarehouseStaffIfMissing(staffList, ws3, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<WarehouseStaff>(filePath, staffList);
            }
        }

        // ─── CUSTOMERS ────────────────────────────────────────────────────────
        private static void SeedCustomers(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.CustomersFile);
            List<Customer> customers = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Customer>(filePath)
                : new List<Customer>();
            bool changed = false;

            Address addr1 = new Address("10 Pasteur", "Phường Nguyễn Thái Bình", "Quận 1", "TP.HCM", "700000", "Việt Nam");
            Customer c1 = new Customer(
                "CUST001", "Công ty TNHH ABC", "0281234567", "contact@abc.vn",
                addr1, new DateTime(1980, 3, 15), Gender.Other,
                CustomerType.Enterprise, new GeoPoint(10.7769, 106.7009), 50_000_000m, "customer01"
            );
            c1.AddLoyaltyPoints(1450);
            AddCustomerIfMissing(customers, c1, ref changed);

            Address addr2 = new Address("25 Lý Tự Trọng", "Phường Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam");
            Customer c2 = new Customer(
                "CUST002", "Nguyễn Thị Mai", "0907654321", "mai.nguyen@gmail.com",
                addr2, new DateTime(1993, 11, 25), Gender.Female,
                CustomerType.VIP, new GeoPoint(10.7833, 106.6997), 10_000_000m, "ACC_CUST002"
            );
            c2.AddLoyaltyPoints(620);
            AddCustomerIfMissing(customers, c2, ref changed);

            Address addr3 = new Address("88 Cách Mạng Tháng 8", "Phường 6", "Quận 3", "TP.HCM", "700000", "Việt Nam");
            Customer c3 = new Customer(
                "CUST003", "Trần Văn Khoa", "0971234567", "khoa.tran@email.com",
                addr3, new DateTime(2000, 6, 5), Gender.Male,
                CustomerType.Standard, new GeoPoint(10.7721, 106.6886), 2_000_000m
            );
            c3.AddLoyaltyPoints(90);
            AddCustomerIfMissing(customers, c3, ref changed);

            Customer c4 = new Customer(
                "CUST004", "Công ty Cổ phần Nông sản Mekong", "0908123456", "ops@mekongagri.vn",
                new Address("182 Nguyễn Văn Cừ", "An Hòa", "Ninh Kiều", "Cần Thơ", "900000", "Việt Nam"),
                new DateTime(1982, 7, 12), Gender.Other,
                CustomerType.Enterprise, new GeoPoint(10.0452, 105.7469), 80_000_000m, "customer02"
            );
            c4.AddLoyaltyPoints(1830);
            AddCustomerIfMissing(customers, c4, ref changed);

            Customer c5 = new Customer(
                "CUST005", "Cửa hàng Điện máy An Phát", "0918123456", "sales@anphat-elec.vn",
                new Address("34 Lê Duẩn", "Bến Nghé", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1990, 8, 8), Gender.Other,
                CustomerType.VIP, new GeoPoint(10.7810, 106.7020), 15_000_000m
            );
            c5.AddLoyaltyPoints(480);
            AddCustomerIfMissing(customers, c5, ref changed);

            Customer c6 = new Customer(
                "CUST006", "Phòng khám Minh Tâm", "0928123456", "logistics@minhtamclinic.vn",
                new Address("19 Nguyễn Hữu Cảnh", "Phường 22", "Bình Thạnh", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1988, 2, 25), Gender.Other,
                CustomerType.Standard, new GeoPoint(10.7916, 106.7219), 5_000_000m
            );
            c6.AddLoyaltyPoints(210);
            AddCustomerIfMissing(customers, c6, ref changed);

            Customer c7 = new Customer(
                "CUST007", "Nhà sách Hải Đăng", "0938123456", "contact@haidangbooks.vn",
                new Address("11 Nguyễn Thị Minh Khai", "Đa Kao", "Quận 1", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1996, 4, 19), Gender.Other,
                CustomerType.Standard, new GeoPoint(10.7871, 106.7052), 3_000_000m
            );
            c7.AddLoyaltyPoints(75);
            AddCustomerIfMissing(customers, c7, ref changed);

            Customer c8 = new Customer(
                "CUST008", "Công ty Cổ phần Vận tải Biển Đông", "0288888888", "contact@biendong.vn",
                new Address("102 Nguyễn Tất Thành", "Phường 12", "Quận 4", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1985, 12, 10), Gender.Other,
                CustomerType.Enterprise, new GeoPoint(10.7629, 106.7089), 120_000_000m
            );
            c8.AddLoyaltyPoints(3120);
            AddCustomerIfMissing(customers, c8, ref changed);

            Customer c9 = new Customer(
                "CUST009", "Phạm Minh Hoàng", "0909999999", "hoang.pham@gmail.com",
                new Address("456 Lê Hồng Phong", "Phường 1", "Quận 10", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1992, 4, 3), Gender.Male,
                CustomerType.Standard, new GeoPoint(10.7719, 106.6719), 3_000_000m
            );
            c9.AddLoyaltyPoints(150);
            AddCustomerIfMissing(customers, c9, ref changed);

            Customer c10 = new Customer(
                "CUST010", "Công ty Cổ phần Sữa Việt Nam (Vinamilk)", "02854155555", "customercare@vinamilk.com.vn",
                new Address("10 Tân Trào", "Phường Tân Phú", "Quận 7", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1976, 8, 20), Gender.Other,
                CustomerType.Enterprise, new GeoPoint(10.7294, 106.7214), 250_000_000m
            );
            c10.AddLoyaltyPoints(8500);
            AddCustomerIfMissing(customers, c10, ref changed);

            Customer c11 = new Customer(
                "CUST011", "Trần Thu Trang", "0911223344", "trang.tran@outlook.com",
                new Address("789 Lạc Long Quân", "Phường 10", "Tân Bình", "TP.HCM", "700000", "Việt Nam"),
                new DateTime(1995, 2, 28), Gender.Female,
                CustomerType.VIP, new GeoPoint(10.7936, 106.6375), 18_000_000m
            );
            c11.AddLoyaltyPoints(720);
            AddCustomerIfMissing(customers, c11, ref changed);

            Customer c12 = new Customer(
                "CUST012", "Cửa hàng Thời trang Blue", "0987654321", "info@bluestore.vn",
                new Address("12 Hàng Bạc", "Phường Hàng Bạc", "Hoàn Kiếm", "Hà Nội", "100000", "Việt Nam"),
                new DateTime(2002, 10, 15), Gender.Female,
                CustomerType.Standard, new GeoPoint(21.0336, 105.8524), 5_000_000m
            );
            c12.AddLoyaltyPoints(280);
            AddCustomerIfMissing(customers, c12, ref changed);

            EnsureCustomerAccount(customers, "CUST001", "customer01", ref changed);
            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Customer>(filePath, customers);
            }
        }

        // ─── VEHICLES ─────────────────────────────────────────────────────────
        private static void SeedVehicles(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.VehiclesFile);
            List<Vehicle> vehicles = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Vehicle>(filePath)
                : new List<Vehicle>();
            bool changed = false;

            Vehicle v1 = new Vehicle("VEH001", VehicleType.Motorbike, 30, 0.1, "50x40x30 cm", false);
            v1.InstallEngine(new Engine("ENG001", "Honda", 13));
            v1.UpdateFuelLevel(82);
            v1.UpdateOdometer(18420);
            AddMaintenance(v1, "MTN-VEH001-001", DateTime.Now.AddMonths(-3), 150_000m, "Thay dầu nhớt động cơ và bộ lọc dầu", "Garage xe máy Tiến Đạt", DateTime.Now.AddMonths(-1));
            AddMaintenance(v1, "MTN-VEH001-002", DateTime.Now.AddMonths(-1), 350_000m, "Thay xích, má phanh trước sau và bugi", "Garage xe máy Tiến Đạt", DateTime.Now.AddMonths(2));
            AddMaintenance(v1, "MTN-VEH001-003", DateTime.Now.AddDays(-5), 600_000m, "Bảo dưỡng định kỳ toàn bộ xe", "Trung tâm Honda Head Sơn Cát", DateTime.Now.AddMonths(3));
            AddVehicleIfMissing(vehicles, v1, ref changed);

            Vehicle v2 = new Vehicle("VEH002", VehicleType.Van, 500, 5, "200x150x145 cm", false);
            v2.InstallEngine(new Engine("ENG002", "Toyota", 165));
            v2.UpdateFuelLevel(67);
            v2.UpdateOdometer(62300);
            AddMaintenance(v2, "MTN-VEH002-001", DateTime.Now.AddMonths(-4), 1_200_000m, "Thay nhớt máy, lọc nhớt và lọc gió", "Toyota Hùng Vương", DateTime.Now.AddMonths(-1));
            AddMaintenance(v2, "MTN-VEH002-002", DateTime.Now.AddMonths(-2), 3_800_000m, "Cân chỉnh góc đặt bánh xe, thay 2 lốp trước", "Mâm lốp Hồng Cường", DateTime.Now.AddMonths(1));
            AddMaintenance(v2, "MTN-VEH002-003", DateTime.Now.AddDays(-10), 900_000m, "Kiểm tra hệ thống điều hòa và bổ sung gas lạnh", "Toyota Hùng Vương", DateTime.Now.AddMonths(2));
            AddVehicleIfMissing(vehicles, v2, ref changed);

            Vehicle v3 = new Vehicle("VEH003", VehicleType.Truck_1Ton, 1000, 10, "350x180x160 cm", false);
            v3.InstallEngine(new Engine("ENG003", "Isuzu", 190));
            v3.UpdateFuelLevel(74);
            v3.UpdateOdometer(91450);
            AddMaintenance(v3, "MTN-VEH003-001", DateTime.Now.AddMonths(-3), 1_800_000m, "Thay dầu cầu, dầu số và bảo dưỡng phanh", "Isuzu Vân Nam", DateTime.Now.AddMonths(1));
            AddMaintenance(v3, "MTN-VEH003-002", DateTime.Now.AddMonths(-1), 1_500_000m, "Bảo dưỡng củ đề, máy phát và hệ thống điện", "Garage chuyên điện ô tô Khánh", DateTime.Now.AddMonths(2));
            AddMaintenance(v3, "MTN-VEH003-003", DateTime.Now.AddDays(-12), 2_200_000m, "Bảo dưỡng hệ thống treo và thay cao su nhíp", "Isuzu Vân Nam", DateTime.Now.AddDays(18));
            AddVehicleIfMissing(vehicles, v3, ref changed);

            Vehicle v4 = new Vehicle("VEH004", VehicleType.ColdStorageTruck, 800, 8, "300x180x160 cm", true);
            v4.InstallEngine(new Engine("ENG004", "Hyundai", 175));
            v4.UpdateFuelLevel(91);
            v4.UpdateOdometer(54210);
            AddMaintenance(v4, "MTN-VEH004-001", DateTime.Now.AddMonths(-2), 1_400_000m, "Vệ sinh dàn lạnh và kiểm tra cảm biến nhiệt", "Cơ điện lạnh ô tô Minh Phú", DateTime.Now.AddMonths(1));
            AddMaintenance(v4, "MTN-VEH004-002", DateTime.Now.AddMonths(-1), 8_500_000m, "Thay thế lốc nén lạnh hệ thống thùng đông", "Cơ điện lạnh ô tô Minh Phú", DateTime.Now.AddMonths(2));
            AddMaintenance(v4, "MTN-VEH004-003", DateTime.Now.AddDays(-3), 2_300_000m, "Bảo dưỡng định kỳ động cơ xe tải", "Hyundai Trường Chinh", DateTime.Now.AddMonths(3));
            AddVehicleIfMissing(vehicles, v4, ref changed);

            Vehicle v5 = new Vehicle("VEH005", VehicleType.Container_40ft, 24000, 67, "1200x235x239 cm", false);
            v5.InstallEngine(new Engine("ENG005", "Hino", 380));
            v5.UpdateFuelLevel(58);
            v5.UpdateOdometer(128900);
            v5.UpdateStatus(VehicleStatus.InTransit);
            AddMaintenance(v5, "MTN-VEH005-001", DateTime.Now.AddMonths(-3), 4_200_000m, "Bảo dưỡng hệ thống phanh khí nén đầu kéo", "Hino Trường Long", DateTime.Now.AddMonths(1));
            AddMaintenance(v5, "MTN-VEH005-002", DateTime.Now.AddMonths(-1), 3_500_000m, "Thay bộ lọc tách nước và vệ sinh kim phun", "Hino Trường Long", DateTime.Now.AddMonths(2));
            AddMaintenance(v5, "MTN-VEH005-003", DateTime.Now.AddDays(-2), 9_800_000m, "Đại tu hệ thống lái và thay rotuyn", "Garage ô tô tải chuyên dụng", DateTime.Now.AddMonths(3));
            AddVehicleIfMissing(vehicles, v5, ref changed);

            Vehicle v6 = new Vehicle("VEH006", VehicleType.Truck_1Ton, 950, 9, "330x175x155 cm", false);
            v6.InstallEngine(new Engine("ENG006", "Thaco", 160));
            v6.UpdateFuelLevel(24);
            v6.UpdateOdometer(73210);
            v6.SendToMaintenance();
            AddMaintenance(v6, "MTN-VEH006-001", DateTime.Now.AddMonths(-4), 1_100_000m, "Thay dầu động cơ, lọc dầu định kỳ", "Thaco An Lạc", DateTime.Now.AddMonths(-1));
            AddMaintenance(v6, "MTN-VEH006-002", DateTime.Now.AddMonths(-2), 4_500_000m, "Bảo dưỡng ly hợp, thay lá côn và bàn ép", "Thaco An Lạc", DateTime.Now.AddDays(-5));
            AddVehicleIfMissing(vehicles, v6, ref changed);

            Vehicle v7 = new Vehicle("VEH007", VehicleType.Van, 650, 6.5, "240x160x150 cm", false);
            v7.InstallEngine(new Engine("ENG007", "Ford", 170));
            v7.UpdateFuelLevel(49);
            v7.UpdateOdometer(38400);
            AddMaintenance(v7, "MTN-VEH007-001", DateTime.Now.AddMonths(-2), 800_000m, "Kiểm tra bình ắc quy và hệ thống khởi động", "Ford Phổ Quang", DateTime.Now.AddMonths(1));
            AddMaintenance(v7, "MTN-VEH007-002", DateTime.Now.AddMonths(-1), 1_300_000m, "Thay nhớt động cơ và căn chỉnh phanh", "Ford Phổ Quang", DateTime.Now.AddMonths(2));
            AddVehicleIfMissing(vehicles, v7, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Vehicle>(filePath, vehicles);
            }
        }

        private static void SeedWarehouses(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.WarehousesFile);
            List<Warehouse> warehouses = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Warehouse>(filePath)
                : new List<Warehouse>();
            bool changed = false;

            Warehouse w1 = new Warehouse("W001", "Kho Trung Tâm Miền Bắc", "Số 1 Duy Tân, Cầu Giấy, Hà Nội", 
                new GeoPoint(21.0285, 105.8542), WarehouseType.FulfillmentCenter, 5000, "08:00 - 20:00", null);
            w1.AddUsedCapacity(1265);
            AddWarehouseIfMissing(warehouses, w1, ref changed);
            
            Warehouse w2 = new Warehouse("W002", "Trạm Trung Chuyển Đà Nẵng", "Lô 12 KCN Hòa Cầm, Đà Nẵng", 
                new GeoPoint(16.0544, 108.2022), WarehouseType.TransshipmentPoint, 2000, "24/7", null);
            w2.AddUsedCapacity(730);
            AddWarehouseIfMissing(warehouses, w2, ref changed);
            
            Warehouse w3 = new Warehouse("W003", "Kho Phân Loại Miền Nam", "Khu Công Nghệ Cao, Quận 9, TP.HCM", 
                new GeoPoint(10.8231, 106.6297), WarehouseType.SortingCenter, 8000, "06:00 - 22:00", null);
            w3.AddUsedCapacity(3560);
            AddWarehouseIfMissing(warehouses, w3, ref changed);
            
            Warehouse w4 = new Warehouse("W004", "Kho Lạnh Tân Bình", "150 Trường Chinh, Tân Bình, TP.HCM", 
                new GeoPoint(10.7944, 106.6436), WarehouseType.ColdStorage, 1500, "08:00 - 18:00", null);
            w4.AddUsedCapacity(960);
            AddWarehouseIfMissing(warehouses, w4, ref changed);

            Warehouse w5 = new Warehouse("W005", "Điểm Trung Chuyển Tây Nguyên", "KCN Phú Hội, Đức Trọng, Lâm Đồng",
                new GeoPoint(11.7356, 108.3731), WarehouseType.TransitPoint, 1800, "07:00 - 19:00", null);
            w5.AddUsedCapacity(410);
            AddWarehouseIfMissing(warehouses, w5, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Warehouse>(filePath, warehouses);
            }
        }

        // ─── ORDERS ───────────────────────────────────────────────────────────
        private static void SeedOrders(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.OrdersFile);
            List<Order> orders = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Order>(filePath)
                : new List<Order>();
            bool changed = false;
            List<Order> sampleOrders = BuildSampleOrders();

            for (int i = 0; i < sampleOrders.Count; i++)
            {
                AddOrderIfMissing(orders, sampleOrders[i], ref changed);
            }

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Order>(filePath, orders);
            }
        }

        public static bool HasInvalidOrders(List<Order> orders)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                if (order == null ||
                    string.IsNullOrWhiteSpace(order.TrackingNumber) ||
                    string.IsNullOrWhiteSpace(order.SenderID) ||
                    string.IsNullOrWhiteSpace(order.ReceiverID) ||
                    order.CreatedDate == DateTime.MinValue ||
                    order.Packages == null ||
                    order.Packages.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<Order> BuildSampleOrders()
        {
            List<Order> orders = new List<Order>();

            orders.Add(CreateSampleOrder("TN202605180001", "CUST001", "CUST002", "10 Pasteur, Quan 1, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Standard, "Ho so hop dong", 2.4, "35x25x8", false, 1200000m, "Tai lieu", "", OrderStatus.Pending, "", ""));
            orders.Add(CreateSampleOrder("TN202605180002", "CUST002", "CUST003", "25 Ly Tu Trong, Quan 1, TP.HCM", "88 Cach Mang Thang 8, Quan 3, TP.HCM", ServiceType.Express, "Linh kien dien tu", 8.7, "50x40x30", true, 6800000m, "Dien tu", "Can xu ly nhe", OrderStatus.InTransit, "DRV0001", "VEH002"));
            orders.Add(CreateSampleOrder("TN202605180003", "CUST003", "CUST001", "88 Cach Mang Thang 8, Quan 3, TP.HCM", "10 Pasteur, Quan 1, TP.HCM", ServiceType.Instant, "Hang thoi trang", 4.2, "45x30x22", false, 2400000m, "Thoi trang", "", OrderStatus.Delivered, "DRV0002", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605180004", "CUST001", "CUST003", "Kho Trung Tam Mien Bac", "88 Cach Mang Thang 8, Quan 3, TP.HCM", ServiceType.Standard, "Vat tu san xuat", 42.5, "90x60x50", false, 5200000m, "Vat tu", "", OrderStatus.Cancelled, "", ""));
            orders.Add(CreateSampleOrder("TN202605180005", "CUST002", "CUST001", "25 Ly Tu Trong, Quan 1, TP.HCM", "Kho Lanh Tan Binh", ServiceType.Express, "Thuc pham dong goi", 18.3, "70x45x35", false, 3100000m, "Thuc pham", "Giu mat", OrderStatus.Failed, "DRV0003", "VEH004"));
            orders.Add(CreateSampleOrder("TN202605180006", "CUST003", "CUST002", "88 Cach Mang Thang 8, Quan 3, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Standard, "Sach va tai lieu", 6.1, "42x32x20", false, 900000m, "Tai lieu", "", OrderStatus.Pending, "", "VEH003"));
            orders.Add(CreateSampleOrder("TN202605180007", "CUST001", "CUST002", "10 Pasteur, Quan 1, TP.HCM", "KCN Hoa Cam, Da Nang", ServiceType.Express, "May quet ma vach", 12.8, "60x45x40", true, 7600000m, "Dien tu", "Hang gia tri cao", OrderStatus.InTransit, "DRV0001", "VEH003"));
            orders.Add(CreateSampleOrder("TN202605180008", "CUST002", "CUST003", "Kho Phan Loai Mien Nam", "88 Cach Mang Thang 8, Quan 3, TP.HCM", ServiceType.Instant, "Mau xet nghiem dong goi", 3.5, "30x24x18", true, 1500000m, "Y te", "Uu tien giao nhanh", OrderStatus.Delivered, "DRV0002", "VEH004"));
            orders.Add(CreateSampleOrder("TN202605180009", "CUST003", "CUST001", "88 Cach Mang Thang 8, Quan 3, TP.HCM", "10 Pasteur, Quan 1, TP.HCM", ServiceType.Standard, "Phu kien van phong", 9.4, "55x35x25", false, 1850000m, "Van phong", "", OrderStatus.Pending, "", ""));
            orders.Add(CreateSampleOrder("TN202605180010", "CUST001", "CUST003", "10 Pasteur, Quan 1, TP.HCM", "Binh Duong, Thuan An", ServiceType.Express, "Linh kien bao hanh", 15.6, "65x45x32", true, 4300000m, "Dien tu", "Can doi chieu bien ban", OrderStatus.Failed, "DRV0003", "VEH002"));
            orders.Add(CreateSampleOrder("TN202605180011", "CUST002", "CUST001", "25 Ly Tu Trong, Quan 1, TP.HCM", "10 Pasteur, Quan 1, TP.HCM", ServiceType.Standard, "Qua tang doanh nghiep", 7.9, "50x35x28", true, 2800000m, "Qua tang", "Khong chong nang", OrderStatus.Delivered, "DRV0001", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605180012", "CUST003", "CUST002", "88 Cach Mang Thang 8, Quan 3, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Instant, "Ho so dau thau", 1.8, "32x24x6", false, 600000m, "Tai lieu", "Giao trong ngay", OrderStatus.InTransit, "DRV0002", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605190013", "CUST004", "CUST005", "182 Nguyen Van Cu, Can Tho", "34 Le Duan, Quan 1, TP.HCM", ServiceType.Express, "Trai cay dong thung", 28.5, "80x55x45", false, 5600000m, "Thuc pham", "Giu mat, giao trong 24h", OrderStatus.WaitingPickup, "", "VEH004"));
            orders.Add(CreateSampleOrder("TN202605190014", "CUST005", "CUST006", "34 Le Duan, Quan 1, TP.HCM", "19 Nguyen Huu Canh, Binh Thanh, TP.HCM", ServiceType.Instant, "Thiet bi sieu am cam tay", 6.4, "48x32x24", true, 22000000m, "Y te", "Hang gia tri cao", OrderStatus.PickedUp, "DRV0005", "VEH007"));
            orders.Add(CreateSampleOrder("TN202605190015", "CUST006", "CUST004", "19 Nguyen Huu Canh, Binh Thanh, TP.HCM", "182 Nguyen Van Cu, Can Tho", ServiceType.Standard, "Vat tu phong kham", 18.9, "70x45x40", false, 3600000m, "Y te", "Kiem dem khi nhap kho", OrderStatus.ArrivedAtWarehouse, "", "VEH003"));
            orders.Add(CreateSampleOrder("TN202605190016", "CUST007", "CUST003", "11 Nguyen Thi Minh Khai, Quan 1, TP.HCM", "88 Cach Mang Thang 8, Quan 3, TP.HCM", ServiceType.Standard, "Sach giao khoa va tap chi", 31.2, "85x60x50", false, 1800000m, "Sach", "Tranh am uot", OrderStatus.Sorting, "", ""));
            orders.Add(CreateSampleOrder("TN202605190017", "CUST004", "CUST001", "KCN Phu Hoi, Lam Dong", "10 Pasteur, Quan 1, TP.HCM", ServiceType.Express, "Ca phe rang xay dong goi", 46.8, "100x70x60", false, 12500000m, "Thuc pham", "Khong de gan mui hoa chat", OrderStatus.ReadyForDispatch, "", "VEH005"));
            orders.Add(CreateSampleOrder("TN202605190018", "CUST001", "CUST004", "10 Pasteur, Quan 1, TP.HCM", "182 Nguyen Van Cu, Can Tho", ServiceType.Standard, "May bom cong nghiep", 180.5, "150x90x80", false, 45000000m, "Thiet bi cong nghiep", "Can xe nang khi boc do", OrderStatus.OutForDelivery, "DRV0004", "VEH005"));
            orders.Add(CreateSampleOrder("TN202605190019", "CUST005", "CUST007", "34 Le Duan, Quan 1, TP.HCM", "11 Nguyen Thi Minh Khai, Quan 1, TP.HCM", ServiceType.Instant, "Laptop ban giao bao hanh", 4.1, "45x32x12", true, 18500000m, "Dien tu", "Yeu cau ky nhan tai quay", OrderStatus.Returning, "DRV0003", "VEH002"));
            orders.Add(CreateSampleOrder("TN202605190020", "CUST006", "CUST002", "19 Nguyen Huu Canh, Binh Thanh, TP.HCM", "25 Ly Tu Trong, Quan 1, TP.HCM", ServiceType.Express, "Mau xet nghiem bi huy lich", 2.7, "30x22x18", true, 900000m, "Y te", "Hoan ve kho lanh neu qua gio", OrderStatus.Returned, "DRV0002", "VEH004"));
            orders.Add(CreateSampleOrder("TN202605190021", "CUST007", "CUST005", "11 Nguyen Thi Minh Khai, Quan 1, TP.HCM", "34 Le Duan, Quan 1, TP.HCM", ServiceType.Standard, "Ke trung bay sach", 72.4, "120x80x60", false, 7200000m, "Noi that", "Giao gio hanh chinh", OrderStatus.Cancelled, "", ""));
            orders.Add(CreateSampleOrder("TN202605190022", "CUST004", "CUST006", "182 Nguyen Van Cu, Can Tho", "19 Nguyen Huu Canh, Binh Thanh, TP.HCM", ServiceType.Express, "Thuc pham dong goi loi tem", 16.6, "65x40x35", false, 2400000m, "Thuc pham", "Can doi chieu nhiet do", OrderStatus.Failed, "DRV0004", "VEH004"));

            orders.Add(CreateSampleOrder("TN202605200023", "CUST008", "CUST010", "102 Nguyễn Tất Thành, Quận 4, TP.HCM", "10 Tân Trào, Quận 7, TP.HCM", ServiceType.Standard, "Thiết bị định vị tàu biển", 450.0, "120x80x100", false, 150000000m, "Thiết bị", "Cẩn thận rỉ sét", OrderStatus.Pending, "", ""));
            orders.Add(CreateSampleOrder("TN202605200024", "CUST009", "CUST001", "456 Lê Hồng Phong, Quận 10, TP.HCM", "10 Pasteur, Quận 1, TP.HCM", ServiceType.Express, "Hộp trang sức đá quý", 0.5, "15x15x10", true, 25000000m, "Trang sức", "Giao tận tay ký nhận", OrderStatus.InTransit, "DRV0002", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605200025", "CUST010", "CUST004", "10 Tân Trào, Quận 7, TP.HCM", "182 Nguyễn Văn Cừ, Cần Thơ", ServiceType.Standard, "Thùng sữa tươi Vinamilk", 1200.0, "Pallet sữa lớn", false, 32000000m, "Thực phẩm", "Tránh ánh nắng trực tiếp", OrderStatus.ArrivedAtWarehouse, "", "VEH003"));
            orders.Add(CreateSampleOrder("TN202605200026", "CUST011", "CUST012", "789 Lạc Long Quân, Tân Bình, TP.HCM", "12 Hàng Bạc, Hoàn Kiếm, Hà Nội", ServiceType.Instant, "Váy đầm thiết kế cao cấp", 1.5, "30x25x5", false, 3500000m, "Thời trang", "Giao trước 17h chiều", OrderStatus.Delivered, "DRV0001", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605200027", "CUST012", "CUST002", "12 Hàng Bạc, Hoàn Kiếm, Hà Nội", "25 Lý Tự Trọng, Quận 1, TP.HCM", ServiceType.Express, "Kiện áo sơ mi nam Blue", 12.0, "40x30x20", false, 4800000m, "Thời trang", "Kèm hóa đơn VAT", OrderStatus.PickedUp, "DRV0003", "VEH002"));
            orders.Add(CreateSampleOrder("TN202605200028", "CUST001", "CUST008", "10 Pasteur, Quận 1, TP.HCM", "102 Nguyễn Tất Thành, Quận 4, TP.HCM", ServiceType.Standard, "Hồ sơ kỹ thuật tàu biển", 3.2, "35x25x12", false, 500000m, "Tài liệu", "", OrderStatus.Pending, "", ""));
            orders.Add(CreateSampleOrder("TN202605200029", "CUST002", "CUST009", "25 Lý Tự Trọng, Quận 1, TP.HCM", "456 Lê Hồng Phong, Quận 10, TP.HCM", ServiceType.Express, "Mỹ phẩm thảo dược organic", 8.4, "35x25x20", true, 6200000m, "Mỹ phẩm", "Không đè vật nặng", OrderStatus.InTransit, "DRV0005", "VEH002"));
            orders.Add(CreateSampleOrder("TN202605200030", "CUST003", "CUST011", "88 Cách Mạng Tháng 8, Quận 3, TP.HCM", "789 Lạc Long Quân, Tân Bình, TP.HCM", ServiceType.Standard, "Bộ ấm trà gốm sứ Bát Tràng", 5.6, "40x30x30", true, 1800000m, "Gốm sứ", "Hàng dễ vỡ xin nhẹ tay", OrderStatus.Sorting, "", ""));
            orders.Add(CreateSampleOrder("TN202605210031", "CUST008", "CUST003", "102 Nguyễn Tất Thành, Quận 4, TP.HCM", "88 Cách Mạng Tháng 8, Quận 3, TP.HCM", ServiceType.Express, "Phụ tùng máy bơm tàu thủy", 85.0, "60x60x50", false, 18000000m, "Thiết bị", "Cần xe nâng hỗ trợ nâng", OrderStatus.ReadyForDispatch, "", "VEH003"));
            orders.Add(CreateSampleOrder("TN202605210032", "CUST010", "CUST011", "10 Tân Trào, Quận 7, TP.HCM", "789 Lạc Long Quân, Tân Bình, TP.HCM", ServiceType.Express, "Thùng sữa chua uống tiệt trùng", 2500.0, "2 Pallet lớn", false, 65000000m, "Thực phẩm", "Bảo quản mát liên tục", OrderStatus.InTransit, "DRV0004", "VEH005"));
            orders.Add(CreateSampleOrder("TN202605210033", "CUST009", "CUST012", "456 Lê Hồng Phong, Quận 10, TP.HCM", "12 Hàng Bạc, Hoàn Kiếm, Hà Nội", ServiceType.Instant, "Bánh kem chúc mừng khai trương", 2.5, "30x30x25", true, 800000m, "Thực phẩm", "Giao nhẹ tay giữ thăng bằng", OrderStatus.Delivered, "DRV0003", "VEH004"));
            orders.Add(CreateSampleOrder("TN202605210034", "CUST011", "CUST010", "789 Lạc Long Quân, Tân Bình, TP.HCM", "10 Tân Trào, Quận 7, TP.HCM", ServiceType.Standard, "Hợp đồng kinh tế bản gốc", 0.2, "30x21x1", false, 100000m, "Tài liệu", "Giao chuyển phát tận tay", OrderStatus.Pending, "", ""));
            orders.Add(CreateSampleOrder("TN202605210035", "CUST012", "CUST009", "12 Hàng Bạc, Hoàn Kiếm, Hà Nội", "456 Lê Hồng Phong, Quận 10, TP.HCM", ServiceType.Express, "Kiện giày thể thao nam nữ", 14.5, "50x40x30", false, 9500000m, "Thời trang", "", OrderStatus.PickedUp, "DRV0005", "VEH007"));
            orders.Add(CreateSampleOrder("TN202605210036", "CUST008", "CUST001", "102 Nguyễn Tất Thành, Quận 4, TP.HCM", "10 Pasteur, Quận 1, TP.HCM", ServiceType.Standard, "Bản đồ luồng hàng hải", 1.8, "80x10x10", false, 1200000m, "Tài liệu", "", OrderStatus.ArrivedAtWarehouse, "", "VEH001"));
            orders.Add(CreateSampleOrder("TN202605210037", "CUST002", "CUST010", "25 Lý Tự Trọng, Quận 1, TP.HCM", "10 Tân Trào, Quận 7, TP.HCM", ServiceType.Instant, "Giấy tờ thông quan lô hàng sữa", 0.5, "30x22x2", false, 200000m, "Tài liệu", "Yêu cầu giao gấp trước giờ ngọ", OrderStatus.Delivered, "DRV0001", "VEH001"));

            return orders;
        }

        private static Order CreateSampleOrder(string trackingNumber, string senderId, string receiverId, string pickupAddress, string deliveryAddress, ServiceType serviceType, string packageDescription, double weight, string dimensions, bool fragile, decimal value, string category, string handlingInstructions, OrderStatus status, string driverId, string vehicleId)
        {
            Order order = new Order(trackingNumber, senderId, receiverId, pickupAddress, deliveryAddress, serviceType);
            Package package = new Package(trackingNumber + "-PKG001", trackingNumber, packageDescription, weight, dimensions, fragile, value, category, handlingInstructions);
            order.AddPackage(package);
            order.AddDetail(packageDescription, 1, value);

            BusinessRules businessRules = new BusinessRules();
            IShippingFeeStrategy strategy = serviceType == ServiceType.Standard
                ? new StandardShippingFeeStrategy()
                : new ExpressShippingFeeStrategy(businessRules);
            order.CalculateTotalCost(strategy, businessRules.GetRatePerKg(serviceType));

            if (value >= 2_000_000m)
            {
                order.SetCodDetails(value, CodStatus.Pending);
            }

            if (!string.IsNullOrWhiteSpace(vehicleId))
            {
                order.AssignVehicle(vehicleId);
            }

            if (!string.IsNullOrWhiteSpace(driverId))
            {
                order.AssignDriver(driverId);
            }

            ApplySampleOrderStatus(order, package, status);

            return order;
        }

        private static void ApplySampleOrderStatus(Order order, Package package, OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.WaitingPickup)
            {
                return;
            }

            if (status == OrderStatus.PickedUp)
            {
                order.ChangeStatus(OrderStatus.PickedUp, "Tai xe da nhan hang tu nguoi gui", "Seeder");
                return;
            }

            if (status == OrderStatus.ArrivedAtWarehouse)
            {
                order.ChangeStatus(OrderStatus.PickedUp, "Tai xe da nhan hang tu nguoi gui", "Seeder");
                order.ChangeStatus(OrderStatus.ArrivedAtWarehouse, "Hang da ve kho trung chuyen", "Seeder");
                package.CheckInWarehouse("W003", "IN-A2-05");
                return;
            }

            if (status == OrderStatus.Sorting)
            {
                order.ChangeStatus(OrderStatus.PickedUp, "Tai xe da nhan hang tu nguoi gui", "Seeder");
                order.ChangeStatus(OrderStatus.ArrivedAtWarehouse, "Hang da ve kho trung chuyen", "Seeder");
                order.ChangeStatus(OrderStatus.Sorting, "Dang phan loai theo tuyen giao", "Seeder");
                package.CheckInWarehouse("W003", "SORT-B1-03");
                package.MarkSorting();
                return;
            }

            if (status == OrderStatus.ReadyForDispatch)
            {
                order.ChangeStatus(OrderStatus.PickedUp, "Tai xe da nhan hang tu nguoi gui", "Seeder");
                order.ChangeStatus(OrderStatus.ArrivedAtWarehouse, "Hang da ve kho trung chuyen", "Seeder");
                order.ChangeStatus(OrderStatus.Sorting, "Dang phan loai theo tuyen giao", "Seeder");
                order.ChangeStatus(OrderStatus.ReadyForDispatch, "Da xuat kho cho dieu phoi", "Seeder");
                return;
            }

            if (status == OrderStatus.InTransit)
            {
                order.ChangeStatus(OrderStatus.InTransit, "Da dieu phoi giao hang", "Seeder");
                return;
            }

            if (status == OrderStatus.OutForDelivery)
            {
                order.ChangeStatus(OrderStatus.ReadyForDispatch, "Da xuat kho cho dieu phoi", "Seeder");
                order.ChangeStatus(OrderStatus.OutForDelivery, "Tai xe dang giao den nguoi nhan", "Seeder");
                return;
            }

            if (status == OrderStatus.Delivered)
            {
                order.ChangeStatus(OrderStatus.InTransit, "Da dieu phoi giao hang", "Seeder");
                order.ChangeStatus(OrderStatus.Delivered, "Da giao thanh cong", "Seeder");
                if (order.CodStatus == CodStatus.Pending)
                {
                    order.UpdateCodStatus(CodStatus.Settled);
                }
                return;
            }

            if (status == OrderStatus.Cancelled)
            {
                order.ChangeStatus(OrderStatus.Cancelled, "Khach hang huy don truoc khi lay hang", "Seeder");
                return;
            }

            if (status == OrderStatus.Returning)
            {
                order.ChangeStatus(OrderStatus.InTransit, "Da dieu phoi giao hang", "Seeder");
                order.ChangeStatus(OrderStatus.Returning, "Nguoi nhan tu choi nhan hang", "Seeder");
                return;
            }

            if (status == OrderStatus.Returned)
            {
                order.ChangeStatus(OrderStatus.InTransit, "Da dieu phoi giao hang", "Seeder");
                order.ChangeStatus(OrderStatus.Returning, "Nguoi nhan tu choi nhan hang", "Seeder");
                order.ChangeStatus(OrderStatus.Returned, "Hang da hoan ve nguoi gui", "Seeder");
                return;
            }

            if (status == OrderStatus.Failed)
            {
                order.ChangeStatus(OrderStatus.InTransit, "Da dieu phoi giao hang", "Seeder");
                order.ChangeStatus(OrderStatus.Failed, "Su co giao hang can xu ly", "Seeder");
            }
        }

        private static void SeedTransactions(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.TransactionsFile);
            List<Transaction> transactions = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<Transaction>(filePath)
                : new List<Transaction>();
            List<Order> orders = ReadSeedOrders(dataDirectory);
            bool changed = false;

            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605180001", "TN202605180003", GetOrderCost(orders, "TN202605180003", 96_000m), PaymentMethod.Banking, TransactionStatus.Completed), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605180002", "TN202605180008", GetOrderCost(orders, "TN202605180008", 132_000m), PaymentMethod.EWallet, TransactionStatus.Completed), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605180003", "TN202605180011", GetOrderCost(orders, "TN202605180011", 158_000m), PaymentMethod.COD, TransactionStatus.Completed), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605180004", "TN202605180010", GetOrderCost(orders, "TN202605180010", 420_000m), PaymentMethod.COD, TransactionStatus.Failed), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605190005", "TN202605190017", GetOrderCost(orders, "TN202605190017", 820_000m), PaymentMethod.Credit, TransactionStatus.Pending), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605190006", "TN202605190018", GetOrderCost(orders, "TN202605190018", 1_450_000m), PaymentMethod.Banking, TransactionStatus.Pending), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605190007", "TN202605190020", GetOrderCost(orders, "TN202605190020", 120_000m), PaymentMethod.EWallet, TransactionStatus.Refunded), ref changed);
            AddTransactionIfMissing(transactions, CreateSeedTransaction("TXN202605190008", "TN202605190021", GetOrderCost(orders, "TN202605190021", 360_000m), PaymentMethod.Credit, TransactionStatus.Refunded), ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<Transaction>(filePath, transactions);
            }
        }

        private static void SeedProblemReports(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.ProblemReportsFile);
            List<ProblemReport> reports = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<ProblemReport>(filePath)
                : new List<ProblemReport>();
            bool changed = false;

            ProblemReport report1 = new ProblemReport("RPT202605180001", "TN202605180005", IssueType.Damaged, "Thung hang bi mop canh khi giao ve kho lanh Tan Binh.");
            report1.AddEvidence("evidence/problem_reports/RPT202605180001_thung_mop.jpg");
            report1.UpdateResolutionStatus(ResolutionStatus.Investigating);
            AddProblemReportIfMissing(reports, report1, ref changed);

            ProblemReport report2 = new ProblemReport("RPT202605180002", "TN202605180010", IssueType.Delay, "Don hang tre do ket xe khu vuc Binh Duong, khach yeu cau doi chieu bien ban.");
            report2.AddEvidence("evidence/problem_reports/RPT202605180002_bien_ban_tre.pdf");
            AddProblemReportIfMissing(reports, report2, ref changed);

            ProblemReport report3 = new ProblemReport("RPT202605190003", "TN202605190020", IssueType.WrongAddress, "Dia chi phong kham thay doi lich nhan, hang da hoan ve kho lanh theo quy trinh.");
            report3.AddEvidence("evidence/problem_reports/RPT202605190003_xac_nhan_khach.pdf");
            report3.Resolve();
            AddProblemReportIfMissing(reports, report3, ref changed);

            ProblemReport report4 = new ProblemReport("RPT202605190004", "TN202605190022", IssueType.Damaged, "Lo thuc pham dong goi loi tem niem phong, can kiem dem va lap bien ban voi nguoi gui.");
            report4.AddEvidence("evidence/problem_reports/RPT202605190004_tem_loi.jpg");
            report4.UpdateResolutionStatus(ResolutionStatus.Investigating);
            AddProblemReportIfMissing(reports, report4, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<ProblemReport>(filePath, reports);
            }
        }

        private static void SeedDeliveryTrips(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.DeliveryTripsFile);
            List<DeliveryTrip> trips = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<DeliveryTrip>(filePath)
                : new List<DeliveryTrip>();
            bool changed = false;

            DeliveryTrip trip1 = new DeliveryTrip("TRIP202605180001", "VEH001", "DRV0002");
            trip1.AddOrder("TN202605180003");
            trip1.AddOrder("TN202605180011");
            trip1.Start();
            trip1.Complete();
            AddDeliveryTripIfMissing(trips, trip1, ref changed);

            DeliveryTrip trip2 = new DeliveryTrip("TRIP202605180002", "VEH004", "DRV0002");
            trip2.AddOrder("TN202605180008");
            trip2.Start();
            trip2.Complete();
            AddDeliveryTripIfMissing(trips, trip2, ref changed);

            DeliveryTrip trip3 = new DeliveryTrip("TRIP202605190003", "VEH005", "DRV0004");
            trip3.AddOrder("TN202605190018");
            trip3.AddOrder("TN202605190022");
            trip3.Start();
            AddDeliveryTripIfMissing(trips, trip3, ref changed);

            DeliveryTrip trip4 = new DeliveryTrip("TRIP202605190004", "VEH003", "DRV0003");
            trip4.AddOrder("TN202605190017");
            AddDeliveryTripIfMissing(trips, trip4, ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<DeliveryTrip>(filePath, trips);
            }
        }

        private static void SeedWarehouseInventoryLogs(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.WarehouseInventoryLogsFile);
            List<WarehouseInventoryLog> logs = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<WarehouseInventoryLog>(filePath)
                : new List<WarehouseInventoryLog>();
            bool changed = false;

            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605180001", "W004", "TN202605180008-PKG001", "TN202605180008", "COLD-A1-02", InventoryTransactionType.CheckIn, 3.5, "WHS0002", "Nhap kho lanh mau xet nghiem truoc khi giao nhanh."), ref changed);
            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605180002", "W004", "TN202605180008-PKG001", "TN202605180008", "COLD-A1-02", InventoryTransactionType.CheckOut, 3.5, "WHS0002", "Xuat kho lanh giao cho tai xe DRV0002."), ref changed);
            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605180003", "W003", "TN202605190015-PKG001", "TN202605190015", "IN-A2-05", InventoryTransactionType.CheckIn, 18.9, "WHS0002", "Nhap kho phan loai vat tu phong kham."), ref changed);
            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605180004", "W003", "TN202605190016-PKG001", "TN202605190016", "SORT-B1-03", InventoryTransactionType.CheckIn, 31.2, "WHS0001", "Nhap khu vuc phan loai sach va tap chi."), ref changed);
            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605190005", "W005", "TN202605190017-PKG001", "TN202605190017", "TG-C2-01", InventoryTransactionType.CheckOut, 46.8, "WHS0003", "Xuat hang ca phe rang xay ve TP.HCM."), ref changed);
            AddInventoryLogIfMissing(logs, new WarehouseInventoryLog("INV202605190006", "W004", "TN202605190020-PKG001", "TN202605190020", "COLD-R1-07", InventoryTransactionType.CheckIn, 2.7, "WHS0002", "Hoan ve kho lanh do khach huy lich nhan."), ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<WarehouseInventoryLog>(filePath, logs);
            }
        }

        private static void SeedAuditLogs(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.AuditLogsFile);
            List<AuditLog> logs = System.IO.File.Exists(filePath)
                ? JsonHelper.ReadAll<AuditLog>(filePath)
                : new List<AuditLog>();
            bool changed = false;

            AddAuditLogIfMissing(logs, CreateAuditLog("AUD202605180001", new DateTime(2026, 5, 18, 8, 15, 0), "admin", "SeedData", "System", "DataSeeder", "Khoi tao du lieu van hanh mau cho demo nghiep vu."), ref changed);
            AddAuditLogIfMissing(logs, CreateAuditLog("AUD202605180002", new DateTime(2026, 5, 18, 9, 5, 0), "dispatcher01", "CreateTrip", "DeliveryTrip", "TRIP202605180001", "Tao chuyen giao noi thanh cho 2 don da giao thanh cong."), ref changed);
            AddAuditLogIfMissing(logs, CreateAuditLog("AUD202605180003", new DateTime(2026, 5, 18, 10, 30, 0), "ws02", "CheckIn", "WarehouseInventoryLog", "INV202605180003", "Nhap kho phan loai vat tu phong kham."), ref changed);
            AddAuditLogIfMissing(logs, CreateAuditLog("AUD202605180004", new DateTime(2026, 5, 18, 14, 10, 0), "driver02", "CompleteDelivery", "Order", "TN202605180008", "Hoan thanh giao don mau xet nghiem dong goi."), ref changed);
            AddAuditLogIfMissing(logs, CreateAuditLog("AUD202605190005", new DateTime(2026, 5, 19, 11, 20, 0), "dispatcher02", "CreateProblemReport", "ProblemReport", "RPT202605190004", "Lap bien ban loi tem niem phong lo thuc pham."), ref changed);

            if (changed || !System.IO.File.Exists(filePath))
            {
                JsonHelper.WriteAll<AuditLog>(filePath, logs);
            }
        }

        private static List<Order> ReadSeedOrders(string dataDirectory)
        {
            string filePath = System.IO.Path.Combine(dataDirectory, JsonConstants.OrdersFile);
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Order>();
            }

            return JsonHelper.ReadAll<Order>(filePath);
        }

        private static decimal GetOrderCost(List<Order> orders, string trackingNumber, decimal fallback)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null && string.Equals(orders[i].TrackingNumber, trackingNumber, StringComparison.OrdinalIgnoreCase))
                {
                    if (orders[i].TotalCost > 0)
                    {
                        return orders[i].TotalCost;
                    }
                }
            }

            return fallback;
        }

        private static Transaction CreateSeedTransaction(string transactionId, string orderId, decimal amount, PaymentMethod paymentMethod, TransactionStatus status)
        {
            Transaction transaction = new Transaction(transactionId, orderId, amount, paymentMethod);
            if (status == TransactionStatus.Completed)
            {
                transaction.CompleteTransaction();
            }
            else if (status == TransactionStatus.Failed)
            {
                transaction.FailTransaction();
            }
            else if (status == TransactionStatus.Refunded)
            {
                transaction.CompleteTransaction();
                transaction.RefundTransaction();
            }

            return transaction;
        }

        private static AuditLog CreateAuditLog(string auditLogId, DateTime createdAt, string actorUsername, string action, string entityType, string entityId, string detail)
        {
            AuditLog log = new AuditLog();
            log.AuditLogId = auditLogId;
            log.CreatedAt = createdAt;
            log.ActorUsername = actorUsername;
            log.Action = action;
            log.EntityType = entityType;
            log.EntityId = entityId;
            log.Detail = detail;
            return log;
        }

        private static void AddAdminIfMissing(List<Admin> admins, Admin admin, ref bool changed)
        {
            if (admin == null)
            {
                return;
            }

            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i] != null && string.Equals(admins[i].StaffID, admin.StaffID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            admins.Add(admin);
            changed = true;
        }

        private static void AddDriverIfMissing(List<Driver> drivers, Driver driver, ref bool changed)
        {
            if (driver == null)
            {
                return;
            }

            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i] != null && string.Equals(drivers[i].StaffID, driver.StaffID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            drivers.Add(driver);
            changed = true;
        }

        private static void AddDispatcherIfMissing(List<Dispatcher> dispatchers, Dispatcher dispatcher, ref bool changed)
        {
            if (dispatcher == null)
            {
                return;
            }

            for (int i = 0; i < dispatchers.Count; i++)
            {
                if (dispatchers[i] != null && string.Equals(dispatchers[i].StaffID, dispatcher.StaffID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            dispatchers.Add(dispatcher);
            changed = true;
        }

        private static void AddWarehouseStaffIfMissing(List<WarehouseStaff> staffList, WarehouseStaff staff, ref bool changed)
        {
            if (staff == null)
            {
                return;
            }

            for (int i = 0; i < staffList.Count; i++)
            {
                if (staffList[i] != null && string.Equals(staffList[i].StaffID, staff.StaffID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            staffList.Add(staff);
            changed = true;
        }

        private static void AddCustomerIfMissing(List<Customer> customers, Customer customer, ref bool changed)
        {
            if (customer == null)
            {
                return;
            }

            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i] != null && string.Equals(customers[i].Id, customer.Id, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            customers.Add(customer);
            changed = true;
        }

        private static void EnsureCustomerAccount(List<Customer> customers, string customerId, string accountId, ref bool changed)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i] != null && string.Equals(customers[i].Id, customerId, StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.Equals(customers[i].AccountID, accountId, StringComparison.OrdinalIgnoreCase))
                    {
                        customers[i].UpdateAccountId(accountId);
                        changed = true;
                    }
                    return;
                }
            }
        }

        private static void AddVehicleIfMissing(List<Vehicle> vehicles, Vehicle vehicle, ref bool changed)
        {
            if (vehicle == null)
            {
                return;
            }

            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i] != null && string.Equals(vehicles[i].VehicleID, vehicle.VehicleID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            vehicles.Add(vehicle);
            changed = true;
        }

        private static void AddMaintenance(Vehicle vehicle, string logId, DateTime serviceDate, decimal cost, string description, string serviceProvider, DateTime nextDueDate)
        {
            if (vehicle == null)
            {
                return;
            }

            vehicle.AddMaintenanceLog(new MaintenanceLog(logId, vehicle.VehicleID, serviceDate, cost, description, serviceProvider, nextDueDate));
        }

        private static void AddWarehouseIfMissing(List<Warehouse> warehouses, Warehouse warehouse, ref bool changed)
        {
            if (warehouse == null)
            {
                return;
            }

            for (int i = 0; i < warehouses.Count; i++)
            {
                if (warehouses[i] != null && string.Equals(warehouses[i].WarehouseID, warehouse.WarehouseID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            warehouses.Add(warehouse);
            changed = true;
        }

        private static void AddOrderIfMissing(List<Order> orders, Order order, ref bool changed)
        {
            if (order == null)
            {
                return;
            }

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null && string.Equals(orders[i].TrackingNumber, order.TrackingNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            orders.Add(order);
            changed = true;
        }

        private static void AddTransactionIfMissing(List<Transaction> transactions, Transaction transaction, ref bool changed)
        {
            if (transaction == null)
            {
                return;
            }

            for (int i = 0; i < transactions.Count; i++)
            {
                if (transactions[i] != null && string.Equals(transactions[i].TransactionID, transaction.TransactionID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            transactions.Add(transaction);
            changed = true;
        }

        private static void AddProblemReportIfMissing(List<ProblemReport> reports, ProblemReport report, ref bool changed)
        {
            if (report == null)
            {
                return;
            }

            for (int i = 0; i < reports.Count; i++)
            {
                if (reports[i] != null && string.Equals(reports[i].ReportID, report.ReportID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            reports.Add(report);
            changed = true;
        }

        private static void AddDeliveryTripIfMissing(List<DeliveryTrip> trips, DeliveryTrip trip, ref bool changed)
        {
            if (trip == null)
            {
                return;
            }

            for (int i = 0; i < trips.Count; i++)
            {
                if (trips[i] != null && string.Equals(trips[i].TripID, trip.TripID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            trips.Add(trip);
            changed = true;
        }

        private static void AddInventoryLogIfMissing(List<WarehouseInventoryLog> logs, WarehouseInventoryLog log, ref bool changed)
        {
            if (log == null)
            {
                return;
            }

            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i] != null && string.Equals(logs[i].LogID, log.LogID, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            logs.Add(log);
            changed = true;
        }

        private static void AddAuditLogIfMissing(List<AuditLog> logs, AuditLog log, ref bool changed)
        {
            if (log == null)
            {
                return;
            }

            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i] != null && string.Equals(logs[i].AuditLogId, log.AuditLogId, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            logs.Add(log);
            changed = true;
        }
    }
}
