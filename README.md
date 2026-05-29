# 🚚 HỆ THỐNG QUẢN LÝ LOGISTICS (LOGISTICS MANAGEMENT SYSTEM)

[![C#](https://img.shields.io/badge/C%23-12.0-blue.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET Core](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download)
[![WinForms](https://img.shields.io/badge/UI-Windows%20Forms-orange.svg)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![JSON Serialization](https://img.shields.io/badge/Data-Newtonsoft.Json-green.svg)](https://www.newtonsoft.com/json)

> **Đồ án cuối kỳ môn Lập trình hướng đối tượng (OOP)**  
> **Nhóm thực hiện**: Nhóm 5 (Group 5)  
> **Giảng viên hướng dẫn**: TS. Đặng Ngọc Hoàng Thành

---

## 📌 Tổng Quan Dự Án

Dự án là một **Hệ thống Quản lý Logistics** toàn diện, được thiết kế và phát triển dựa trên ngôn ngữ **C#** và nền tảng **Windows Forms (WinForms)**. Hệ thống tập trung tối đa vào việc áp dụng các nguyên lý lập trình hướng đối tượng (OOP) nâng cao và các mẫu thiết kế (Design Patterns) phổ biến trong thực tế, thay vì sử dụng các framework ORM hay cơ sở dữ liệu quan hệ truyền thống. 

Đặc biệt, toàn bộ dữ liệu của hệ thống được lưu trữ dưới dạng các tập tin JSON thông qua kỹ thuật **Serialization và Deserialization** tự phát triển, đảm bảo tính toàn vẹn dữ liệu, cơ chế backup an toàn và khôi phục khi gặp sự cố.

---

## 📂 Cơ Cấu Tổ Chức Thư Mục & Project

Dự án bao gồm 3 project chính trong Solution `LogisticsSystem.slnx`:

```text
Cuoi_ky_OOP/
├── Logistics.Core/             # Logic nghiệp vụ, Models, Services, Repositories
│   ├── Configuration/          # Cấu hình hệ thống
│   ├── DataAccess/             # Xử lý lưu trữ JSON (Repository Pattern)
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Exceptions/             # Các lớp ngoại lệ tự định nghĩa
│   ├── Mappings/               # Ánh xạ giữa Model và DTO
│   ├── Models/                 # Các thực thể nghiệp vụ (Entities, Interfaces, Enums)
│   ├── Security/               # Bảo mật, phân quyền, mã hóa mật khẩu
│   ├── Services/               # Nghiệp vụ ứng dụng (Business Services)
│   ├── Utilities/              # Tiện ích bổ trợ (Validation, Logging)
│   └── Validations/            # Kiểm tra ràng buộc dữ liệu
├── Logistics.WinFormsUI/       # Giao diện người dùng Windows Forms
│   ├── Forms/                  # Giao diện màn hình chức năng
│   ├── UserControls/           # Thành phần UI tái sử dụng
│   ├── Helpers/                # Tiện ích UI (Style, định dạng)
│   └── Program.cs              # Điểm khởi chạy ứng dụng
└── Logistics.SmokeTests/       # Kịch bản kiểm thử tự động (Unit / Smoke Tests)
```

---

## 🛠️ Công Nghệ Sử Dụng

*   **Ngôn ngữ lập trình:** C# (Explicitly typed, cam kết không sử dụng từ khóa `var`, không dùng `LINQ` hay `Lambda` để tập trung tối đa vào tư duy OOP thuần túy).
*   **Giao diện:** Windows Forms (WinForms UI) trực quan, phân quyền rõ ràng theo vai trò người dùng.
*   **Lưu trữ dữ liệu:** Serialization JSON (Sử dụng thư viện `Newtonsoft.Json`).
*   **Kiểm thử:** Unit Testing trong project `Logistics.SmokeTests`.

---

## 💎 Áp Dụng Rõ Ràng 4 Tính Chất Cốt Lõi Của OOP

Đồ án được thiết kế tỉ mỉ nhằm thể hiện rõ nét 4 đặc tính quan trọng của Lập trình Hướng đối tượng:

### 1. Đóng gói (Encapsulation)
*   Tất cả các thuộc tính quan trọng trong các lớp model (như `Order.CurrentStatus`, `Package.Status`, `Vehicle.Status`, `Staff.BaseSalary`) đều sử dụng getter công khai (`public`) nhưng setter bị giới hạn (`private set` hoặc `protected set`).
*   Việc thay đổi trạng thái đối tượng bắt buộc phải thông qua các phương thức nghiệp vụ tương ứng (ví dụ: `Order.ChangeStatus()`, `Package.CheckInWarehouse()`, `Vehicle.SendToMaintenance()`), giúp kiểm soát dữ liệu chặt chẽ và ngăn ngừa trạng thái bất hợp lệ.

### 2. Trừu tượng (Abstraction)
*   **Abstract Class:** 
    *   `Person`: Lớp cơ sở trừu tượng chứa các thông tin cá nhân cơ bản.
    *   `Staff`: Lớp cơ sở trừu tượng đại diện cho toàn bộ nhân sự hệ thống.
*   **Interfaces:** Định nghĩa hợp đồng hành vi chung:
    *   `ITrackable`: Chuẩn hóa hành vi theo dõi trạng thái đối với Tài xế, Đơn hàng, Phương tiện, Kho bãi, Thiết bị.
    *   `IReportable`: Chuẩn hóa chức năng xuất báo cáo của Đơn hàng, Kho bãi, Sự cố.
    *   `ISalaryCalculable`: Ràng buộc hành vi tính lương và bảng kê chi tiết lương của nhân sự.
    *   `IRepository<T>`: Trừu tượng hóa việc truy xuất dữ liệu từ các tệp tin lưu trữ.
    *   `IShippingFeeStrategy`: Định nghĩa thuật toán tính cước phí vận chuyển.

### 3. Kế thừa (Inheritance)
*   **Phân cấp Tác nhân:**
    ```text
    Person (Abstract)
    ├── Customer (Khách hàng)
    └── Staff (Nhân sự - Abstract)
        ├── Admin (Quản trị viên)
        ├── Driver (Tài xế)
        ├── Dispatcher (Điều phối viên)
        └── WarehouseStaff (Nhân viên kho)
    ```
*   Giúp tái sử dụng mã nguồn hiệu quả, đồng thời tổ chức cấu trúc dữ liệu khoa học.

### 4. Đa hình (Polymorphism)
*   **Đa hình thông qua Ghi đè (Override):** Phương thức `CalculateSalary()` và `GetSalaryBreakdown()` của lớp `Staff` được override bởi mỗi lớp con (`Admin`, `Driver`, `Dispatcher`, `WarehouseStaff`) để áp dụng công thức tính lương và phụ cấp riêng biệt.
*   **Đa hình thông qua Interface:** Thuật toán tính cước vận chuyển linh hoạt theo chiến lược (`IShippingFeeStrategy`), có thể thay đổi giữa `StandardShippingFeeStrategy` và `ExpressShippingFeeStrategy` vào thời điểm runtime.

---

## 🎨 Các Mẫu Thiết Kế (Design Patterns) Được Áp Dụng

| Pattern | Thành Phần Áp Dụng | Vai Trò & Mục Đích |
| :--- | :--- | :--- |
| **Repository Pattern** | `IRepository<T>`, `JsonRepository<T>` | Tách biệt logic truy xuất dữ liệu (đọc/ghi file JSON) ra khỏi các lớp dịch vụ nghiệp vụ (Services) và giao diện (UI). |
| **Strategy Pattern** | `IShippingFeeStrategy` và các lớp triển khai | Thay đổi linh hoạt chiến lược tính phí giao hàng (Standard vs. Express) mà không làm ảnh hưởng đến mã nguồn lớp `Order`. |
| **Factory Method** | `RepositoryFactory.CreateRepository<T>()` | Đóng gói logic khởi tạo các Repository cụ thể tại một điểm duy nhất, giúp dễ dàng nâng cấp hệ thống lưu trữ sau này. |
| **Observer Pattern** | `Order.OnStatusChanged` (Event & Delegate) | Gửi thông báo tự động và ghi nhật ký hành trình (`ShipmentLog`) ngay khi trạng thái của đơn hàng thay đổi. |
---

## 💾 Cơ Chế Serialization JSON & Bảo Vệ Dữ Liệu An Toàn

Do không sử dụng SQL Server hay các RDBMS khác, hệ thống đã xây dựng một cơ chế lưu trữ File JSON an toàn:
1.  **Ghi file giao dịch an toàn (Transaction-safe writing):** Khi cập nhật dữ liệu, repository ghi vào một file tạm `.tmp`. Nếu ghi thành công, file cũ sẽ được đổi tên thành `.bak` (Backup), sau đó file `.tmp` được đổi tên thành file dữ liệu chính thức. Điều này giúp ngăn ngừa mất dữ liệu nếu ứng dụng bị crash hay mất điện đột ngột trong lúc ghi.
2.  **Tự động khôi phục (Auto Recovery):** Nếu file dữ liệu chính bị hỏng (corrupt), hệ thống sẽ cố gắng đọc và khôi phục từ file backup `.bak`. Nếu thất bại, file lỗi được lưu lại dưới đuôi `.corrupt` để kỹ thuật viên phân tích.
3.  **Khóa đồng bộ (File Locking):** Sử dụng `_fileLock` để đảm bảo an toàn đa luồng (thread-safety), tránh tình trạng nhiều tiến trình ghi đè lên file cùng một lúc.
4.  **Bảo mật Deserialization:** Triển khai `LogisticsSerializationBinder` để giới hạn các kiểu dữ liệu được phép deserialization khi cấu hình `TypeNameHandling.Auto`, ngăn chặn các lỗ hổng bảo mật tấn công từ xa qua file dữ liệu.

---

## 👥 Thành Viên Nhóm 5 & Phân Công Nhiệm Vụ

Hệ thống được thiết kế với phân quyền và phân công nhiệm vụ rõ ràng:

1.  **Thành viên 1**: Phát triển lõi nghiệp vụ Actors (`Person`, `Staff`, `Admin`, `Driver`, `Customer`), Security và hệ thống tính lương (`ISalaryCalculable`), Thiết kế và triển khai cơ chế lưu trữ dữ liệu an toàn `JsonRepository<T>` và Repository Factory.
2.  **Thành viên 2**: Phát triển lõi vận chuyển (`Order`, `Package`, `DeliveryRoute`, `DeliveryTrip`) và các chiến lược tính phí (`IShippingFeeStrategy`).
3.  **Thành viên 3**: Phát triển lõi cơ sở hạ tầng (`Warehouse`, `WarehouseLocation`, `Equipment`, `Vehicle`, `Engine`, `MaintenanceLog`), Xây dựng giao diện Windows Forms (`Logistics.WinFormsUI`), tích hợp phân quyền chức năng và viết kịch bản kiểm thử (`Logistics.SmokeTests`).

## 🚀 Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Yêu cầu hệ thống
*   Hệ điều hành Windows (hỗ trợ WinForms).
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) trở lên.
*   Visual Studio 2022 hoặc VS Code (có cài đặt C# Dev Kit).

### Các bước thực hiện

1.  **Clone dự án về máy:**
    ```bash
    git clone https://github.com/phuclu3123/Group_5_OOP_REPORT_FINAL.git
    cd Group_5_OOP_REPORT_FINAL
    ```

2.  **Phục hồi các gói NuGet cần thiết:**
    ```bash
    dotnet restore
    ```

3.  **Chạy kịch bản kiểm thử tự động (Smoke Tests):**
    ```bash
    dotnet test Logistics.SmokeTests/Logistics.SmokeTests.csproj
    ```

4.  **Khởi chạy ứng dụng WinForms:**
    ```bash
    dotnet run --project Logistics.WinFormsUI/Logistics.WinFormsUI.csproj
    ```

