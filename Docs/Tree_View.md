# Logistics System - Project Structure

## Project Root: `Cuoi_ky_OOP`

```
Cuoi_ky_OOP/
├── .sixth/
│   └── skills/
│       └── (empty folder)
├── bin/
│   └── Debug/
│       └── net10.0/
├── obj/
│   ├── Debug/
│   ├── Cuoi_ky_OOP.csproj.nuget.dgspec.json
│   ├── Cuoi_ky_OOP.csproj.nuget.g.props
│   ├── Cuoi_ky_OOP.csproj.nuget.g.targets
│   └── project.assets.json
├── scratch/
│   └── AddISerializable.cs
├── Logistics.Core/
│   ├── appsettings.json
│   ├── Logistics.Core.csproj
│   ├── bin/
│   │   └── Debug/
│   ├── obj/
│   │   ├── Debug/
│   │   ├── Logistics.Core.csproj.nuget.dgspec.json
│   │   ├── Logistics.Core.csproj.nuget.g.props
│   │   ├── Logistics.Core.csproj.nuget.g.targets
│   │   └── project.assets.json
│   ├── DataAccess/
│   │   ├── Interfaces/
│   │   │   └── IRepository.cs
│   │   ├── Repositories/
│   │   │   ├── CustomerRepository.cs
│   │   │   ├── DataSeeder.cs
│   │   │   ├── JsonConstants.cs
│   │   │   ├── JsonHelper.cs
│   │   │   ├── JsonRepository.cs
│   │   │   ├── OrderRepository.cs
│   │   │   ├── RepositoryFactory.cs
│   │   │   ├── StaffRepository.cs
│   │   │   ├── UserRepository.cs
│   │   │   ├── VehicleRepository.cs
│   │   │   ├── WarehouseRepository.cs
│   │   │   └── XmlRepository.cs
│   ├── DTOs/
│   │   ├── CustomerDTO.cs
│   │   ├── DriverDTO.cs
│   │   ├── OrderDTO.cs
│   │   ├── RouteDTO.cs
│   │   ├── VehicleDTO.cs
│   │   └── WarehouseDTO.cs
│   ├── Exceptions/
│   │   ├── DriverNotAvailableException.cs
│   │   ├── InsufficientCapacityException.cs
│   │   ├── InvalidAddressException.cs
│   │   ├── InvalidOrderException.cs
│   │   ├── InvalidPackageException.cs
│   │   ├── LogisticsException.cs
│   │   ├── OrderCancellationException.cs
│   │   ├── TransactionFailedException.cs
│   │   ├── ValidationException.cs
│   │   ├── VehicleNotAvailableException.cs
│   │   └── WarehouseCapacityExceededException.cs
│   ├── Mappings/
│   │   ├── CustomerMappingExtensions.cs
│   │   ├── DriverMappingExtensions.cs
│   │   ├── OrderMappingExtensions.cs
│   │   ├── VehicleMappingExtensions.cs
│   │   └── WarehouseMappingExtensions.cs
│   ├── Models/
│   │   ├── Account/
│   │   │   ├── LoginCredentials.cs
│   │   │   ├── User.cs
│   │   │   └── UserRole.cs
│   │   ├── Actors/
│   │   │   ├── Admin.cs
│   │   │   ├── Customer.cs
│   │   │   ├── Dispatcher.cs
│   │   │   ├── Driver.cs
│   │   │   ├── Person.cs
│   │   │   ├── Staff.cs
│   │   │   └── WarehouseStaff.cs
│   │   ├── Business/
│   │   │   ├── DeliveryRoute.cs
│   │   │   ├── Order.cs
│   │   │   ├── OrderDetail.cs
│   │   │   ├── OrderStatusChangedEventHandler.cs
│   │   │   ├── OrderStatusHistory.cs
│   │   │   ├── Package.cs
│   │   │   ├── ProblemReport.cs
│   │   │   ├── ShipmentLog.cs
│   │   │   └── Transaction.cs
│   │   ├── Common/
│   │   │   ├── Address.cs
│   │   │   ├── Constants.cs
│   │   │   ├── Enums.cs
│   │   │   └── GeoPoint.cs
│   │   ├── Infrastructure/
│   │   │   ├── Engine.cs
│   │   │   ├── Equipment.cs
│   │   │   ├── MaintenanceLog.cs
│   │   │   ├── Vehicle.cs
│   │   │   ├── VehicleAssignment.cs
│   │   │   ├── Warehouse.cs
│   │   │   └── WarehouseLocation.cs
│   │   └── Interfaces/
│   │       ├── IAuthService.cs
│   │       ├── IDeliveryService.cs
│   │       ├── IOrderService.cs
│   │       ├── IReportable.cs
│   │       ├── IRepository.cs
│   │       ├── ISalaryCalculable.cs
│   │       └── ITrackable.cs
│   ├── Security/
│   │   └── (empty folder)
│   ├── Services/
│   │   ├── Implementations/
│   │   │   ├── AuthenticationService.cs
│   │   │   ├── AuthService.cs
│   │   │   ├── DeliveryService.cs
│   │   │   ├── DispatchService.cs
│   │   │   ├── ExpressShippingFeeStrategy.cs
│   │   │   ├── OrderService.cs
│   │   │   ├── ReportService.cs
│   │   │   ├── RouteOptimizationService.cs
│   │   │   ├── StandardShippingFeeStrategy.cs
│   │   │   └── WarehouseService.cs
│   │   └── Interfaces/
│   │       └── IShippingFeeStrategy.cs
│   ├── Utilities/
│   │   ├── Constants.cs
│   │   ├── DateTimeHelper.cs
│   │   ├── PasswordHasher.cs
│   │   ├── SessionManager.cs
│   │   └── StringHelper.cs
│   └── Validations/
│       ├── AddressValidator.cs
│       ├── DriverValidator.cs
│       ├── IValidator.cs
│       ├── OrderValidator.cs
│       ├── PackageValidator.cs
│       ├── PersonValidator.cs
│       ├── StaffValidator.cs
│       └── ValidationResult.cs
├── Logistics.WinFormsUI/
│   ├── AppSettings.cs
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   ├── FrmLogin.cs
│   ├── Logistics.WinFormsUI.csproj
│   ├── Logistics.WinFormsUI.csproj.user
│   ├── Program.cs
│   ├── bin/
│   │   └── Debug/
│   ├── obj/
│   │   └── Debug/
│   ├── Data/
│   │   ├── admins.json
│   │   ├── customers.json
│   │   ├── drivers.json
│   │   ├── orders.json
│   │   ├── users.json
│   │   ├── vehicles.json
│   │   ├── warehouse.json
│   │   └── Default/
│   │       └── .gitkeep
│   ├── Extensions/
│   │   ├── ControlExtensions.cs
│   │   └── DataGridViewExtensions.cs
│   ├── Forms/
│   │   ├── FrmChangePassword.cs
│   │   ├── FrmDashboard.cs
│   │   ├── FrmDispatch.cs
│   │   ├── FrmDriver.cs
│   │   ├── FrmForgotPassword.cs
│   │   ├── FrmInvoice.cs
│   │   ├── FrmMain.cs
│   │   ├── FrmOrder.cs
│   │   ├── FrmRegister.cs
│   │   ├── FrmReport.cs
│   │   ├── FrmSettings.cs
│   │   ├── FrmSplash.cs
│   │   ├── FrmTracking.cs
│   │   ├── FrmVehicle.cs
│   │   └── FrmWarehouse.cs
│   ├── Resources/
│   │   ├── Icons/
│   │   │   └── .gitkeep
│   │   ├── Images/
│   │   │   └── .gitkeep
│   │   └── Styles/
│   ├── Styles/
│   │   ├── Colors.cs
│   │   ├── Fonts.cs
│   │   └── Themes.cs
│   ├── UserControls/
│   │   ├── ucDriverCard.cs
│   │   ├── ucOrderCard.cs
│   │   ├── ucOrderTimeline.cs
│   │   ├── ucSearchPanel.cs
│   │   ├── ucStatusBadge.cs
│   │   └── ucVehicleCard.cs
│   └── Utilities/
│       ├── DependencyContainer.cs
│       ├── ExportHelper.cs
│       ├── FilePathHelper.cs
│       ├── FormHelper.cs
│       └── UIHelper.cs
├── Class_Diagram.tex
├── LogisticsSystem.slnx
├── Test.cs
└── To_Chuc_Folder
```

## Tóm Tắt Cấu Trúc (Summary)

### **Logistics.Core** - Business Logic Layer
- **DataAccess**: Repository Pattern cho truy cập dữ liệu (JSON, XML)
- **DTOs**: Data Transfer Objects
- **Exceptions**: Custom exceptions cho hệ thống
- **Mappings**: Mapping extensions giữa models và DTOs
- **Models**: Domain models
  - **Account**: Quản lý tài khoản người dùng
  - **Actors**: Các vai trò (Admin, Driver, Customer, Staff, etc.)
  - **Business**: Logic kinh doanh (Order, Delivery, Package, etc.)
  - **Common**: Shared models (Address, Enums, Constants, etc.)
  - **Infrastructure**: Cơ sở hạ tầng (Vehicle, Warehouse, Equipment, etc.)
  - **Interfaces**: Các interface định nghĩa
- **Security**: Bảo mật (hiện tại trống)
- **Services**: Business services
  - **Implementations**: Các service chính (AuthService, OrderService, DeliveryService, etc.)
  - **Interfaces**: Interface IShippingFeeStrategy
- **Utilities**: Helper utilities (DateTimeHelper, PasswordHasher, SessionManager, etc.)
- **Validations**: Validators cho các models

### **Logistics.WinFormsUI** - Presentation Layer
- **Form1**: Main form (legacy)
- **FrmLogin**: Login form
- **Data**: JSON data files (admins, customers, drivers, orders, users, vehicles, warehouse)
- **Extensions**: Control extensions cho WinForms
- **Forms**: Giao diện chính
  - FrmDashboard, FrmOrder, FrmDriver, FrmVehicle, FrmWarehouse, etc.
  - FrmChangePassword, FrmForgotPassword, FrmRegister
  - FrmTracking, FrmReport, FrmDispatch, FrmInvoice, FrmSettings
- **Resources**: Icons, Images (placeholder folders)
- **Styles**: UI styling (Colors.cs, Fonts.cs, Themes.cs)
- **UserControls**: Tái sử dụng components
  - ucDriverCard, ucOrderCard, ucOrderTimeline, etc.
- **Utilities**: UI helpers

### Root Level Files
- **Class_Diagram.tex**: LaTeX class diagram
- **LogisticsSystem.slnx**: Solution file
- **Test.cs**: Test file
- **To_Chuc_Folder**: Folder tổ chức (file)

---

## File Counts

| Folder | File Count |
|--------|-----------|
| Logistics.Core | ~100 files |
| Logistics.WinFormsUI | ~50 files |
| **Total** | **~150+ files** |

---

| Layer | Category | Count |
|-------|----------|-------|
| Models | 22 files |
| Services | 11 files |
| DataAccess | 12 files |
| Utilities | 5 files |
| Validations | 8 files |
| Exceptions | 10 files |
| Forms | 15 files |
| UserControls | 6 files |

