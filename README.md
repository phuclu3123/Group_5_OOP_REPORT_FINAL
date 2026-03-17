# Nội dung OOP trong đồ án cuối kỳ

Hệ thống Logistics Management System được thiết kế bằng cách áp dụng chặt chẽ 4 nguyên lý cơ bản của Lập trình Hướng đối tượng (OOP), kết hợp với các mẫu thiết kế (Design Patterns) để đảm bảo tính linh hoạt, mở rộng và dễ bảo trì.

## 1. Tính kế thừa (Inheritance)
Tính kế thừa được áp dụng mạnh mẽ để biểu diễn cấu trúc phân cấp của các thực thể trong hệ thống, nhằm tái sử dụng mã nguồn và quản lý thông tin tập trung:
- **Lớp gốc `Person`:** Chứa các thuộc tính định danh cơ bản của một con người (`Id`, `FullName`, `PhoneNumber`, `Email`, `Address`, v.v.).
- **Phân nhánh cấp 1:**
  - `Customer` kế thừa `Person`, bổ sung các thông tin như `CustomerType`, `LoyaltyPoints`.
  - `Staff` kế thừa `Person`, mở rộng hệ thống nhân sự với `StaffID`, `Role`, `Department`, `BaseSalary`.
- **Kế thừa đa cấp ở `Staff`:** Từ `Staff`, hệ thống tiếp tục phân nhánh thành các vai trò nghiệp vụ thực tế:
  - `Driver` (Tài xế): Bổ sung `LicenseNumber`, `DriverStatus`, `DeliveryCount`, v.v.
  - `Dispatcher` (Nhân viên điều phối): Bổ sung khả năng quản lý khu vực `ManagedRegion`.
  - `WarehouseStaff` (Nhân viên kho): Bổ sung thông tin về ca làm việc `Shift`.

## 2. Tính đóng gói (Encapsulation)
Hệ thống sử dụng các Access Modifiers (Phạm vi truy cập) để bảo vệ trạng thái của đối tượng và đảm bảo tính toàn vẹn dữ liệu:
- **Private Setters (`private set`, `protected set`):** Các thuộc tính quan trọng trong `Staff`, `Order`, `Driver` không thể bị gán giá trị tùy tiện từ bên ngoài. Thay vào đó, chúng chỉ được thay đổi thông qua các phương thức công khai hợp lệ. 
  - Ví dụ: `Order` lưu trữ các `Package` trong một danh sách kín và sử dụng tính năng ẩn giấu để bảo vệ biến `TotalWeight`. Vị trí của tài xế chỉ có thể cập nhật qua `UpdateCurrentLocation()`, thay vì gán đè trực tiếp.
- **Ẩn giấu logic nghiệp vụ:** Thông qua việc sử dụng `private` và `protected`, logic lõi của các tính toán tài chính (lương, chi phí vận chuyển) được giữ kín và an toàn bên trong lớp tương ứng, hạn chế tối đa nguy cơ sửa đổi trái phép từ Service Layer.
- **Gom nhóm chức năng đặc trưng (Cohesion):** Tính đóng gói không chỉ là che giấu dữ liệu mà còn là việc ràng buộc chặt chẽ giữa *dữ liệu* (thuộc tính) và *hành vi* (phương thức) có liên quan mật thiết với nhau vào cùng một thực thể. Mỗi lớp trong hệ thống được thiết kế để tự quản lý một nhóm chức năng chuyên biệt.
  - Ví dụ: Lớp `Driver` đóng gói toàn bộ trạng thái của tài xế (`DriverStatus`, `CurrentLocation`, `DeliveryCount`). Thay vì để hệ thống bên ngoài can thiệp rời rạc, lớp `Driver` cung cấp các phương thức diễn đạt đúng nghiệp vụ thực tế như `StartDelivery()`, `CompleteDelivery()`, và `GoOffDuty()`. Chẳng hạn, khi gọi hàm `CompleteDelivery()`, bản thân đối tượng `Driver` sẽ tự biết phải đổi trạng thái về `Available` và cộng dồn số chuyến `RecordDelivery()`. Điều này giúp đối tượng tự chịu trách nhiệm quản lý vòng đời dữ liệu của chính nó, giảm thiểu sự phụ thuộc từ bên ngoài.

## 3. Tính đa hình (Polymorphism)
Tính đa hình được thể hiện triệt để thông qua việc các đối tượng chia sẻ chung Method Signatures để xử lý thông điệp một cách độc lập:
- **Cơ chế Overriding:**
  - Thông qua Interface `ISalaryCalculable`: Các lớp `Driver`, `Dispatcher`, `WarehouseStaff` cùng triển khai (implement) hàm `CalculateSalary()` và `GetSalaryBreakdown()`. Lương cố định của `Dispatcher` khác biệt hoàn toàn với hệ thống lương dựa trên số chuyến giao hàng của `Driver`. Khi gọi vòng lặp tính lương ở tầng dịch vụ, hệ thống không cần ép kiểu (casting) đối tượng mà vẫn thực thi chính xác biểu thức của từng thực thể.
- **Tính đa hình của Giao diện (`ITrackable`, `IReportable`):**
  - Cả `Driver`, `Order`, và `Package` đều implement phương thức `GetCurrentStatus()` và `GetTrackingInfo()`. Điều này tạo ra tính đa hình khi một dịch vụ trung tâm có thể in ra thông tin định vị của bất cứ hệ thống nào. Tương tự với việc xuất báo cáo qua `IReportable` ở thực thể `Order` và `ShipmentLog`.
- **Cơ chế Generic Repository (`IRepository<T>`):** Thiết kế Repository Pattern đem lại đa hình tĩnh để lưu trữ dữ liệu XML bất kì (`XmlRepository<T>`), hỗ trợ tự động Serialize/Deserialize tương thích và an toàn cho tất cả các đối tượng Model.

## 4. Tính trừu tượng (Abstraction)
Hệ thống ẩn đi những chi tiết phức tạp, chỉ hiển thị ra những khái niệm cần giao tiếp, thông qua Abstract Class và Interfaces:
- **Abstract Classes:** `Person` và `Staff` được thiết kế dưới dạng lớp trừu tượng (`abstract`). Lập trình viên không thể tạo trực tiếp một đối tượng Person trơn tru, mọi nhân sự tham gia hệ thống bắt buộc phải được định hình thành một vai trò tồn tại (`Driver`, `Dispatcher`). 
- **Interfaces chuyên biệt:** Hệ thống loại bỏ sư cồng kềnh qua việc chia nhỏ hành vi (ISP - Interface Segregation Principle):
  - `ISalaryCalculable`: Trừu tượng hóa cách tính bảng lương.
  - `ITrackable`: Định nghĩa bản thiết kế trừu tượng theo dõi vị trí động.
  - `IReportable`: Xây dựng khuôn mẫu trừu tượng tạo lập báo cáo.
  - `IRepository<T>`: Trừu tượng hóa lớp dữ liệu thao tác Data Access, tách biệt mã nguồn XML Repository với logic C# nền tảng của Service Context.

---

## 5. Tầm nhìn mở rộng & Khả năng vận dụng tương lai (Future Capabilities)
Dựa trên kiến trúc OOP lõi đầy đủ hiện tại, hệ thống sở hữu cấu trúc linh hoạt để tiếp tục mở rộng và tích hợp thêm nhiều Module sát với thực tế Logistics (đạt chuẩn Open/Closed Principle):

### 5.1. Hệ thống tính phí & Khuyến mãi động (Áp dụng Trừu tượng - Strategy Pattern)
*   **Vấn đề:** Chi phí vận chuyển (`TotalCost` trong `Order`) hiện đang bị đóng khung cơ bản. Khi kinh doanh đa vùng, giá có thể thay đổi theo ngày lễ, kích thước hàng hóa hoặc trợ giá.
*   **Giải pháp OOP:**
    *   Xây dựng Interface `IPricingStrategy` chứa hàm trừu tượng `CalculateFinalCost(Order order)`.
    *   Các lớp triển khai như: `StandardPricing`, `HolidayPricing`, `VIPDiscountPricing`.
    *   Điều này giúp Service chuyển đổi cơ chế linh động ở Runtime và loại bỏ việc kiểm tra nhánh `if-else` chằng chịt trong module `Order`.

### 5.2. Tự động hóa thông báo đa kênh (Áp dụng Đa hình & Observer Pattern)
*   **Vấn đề:** Cần lập trình gửi Event (tin nhắn báo đơn) mỗi khi kiện hàng (`ITrackable`) qua chặng hoặc giao xe đổi trạng thái.
*   **Giải pháp OOP:**
    *   Xây dựng Interface `INotificationSender` chung cấu trúc `Send(string message, string receiverId)`.
    *   Cấu tạo nhiều đối tượng con: `EmailSender`, `SMSSender`, `FirebasePushSender`.
    *   Logic của `OrderService` chỉ cần lặp qua Sender array và gọi hàm `Send()`. Toàn bộ liên kết hệ thống thư tín SMTP gốc được module Sender che giấu an toàn.

### 5.3. Xử lý sự cố logistics chuyên sâu (Áp dụng Kế thừa mở rộng)
*   **Vấn đề:** Thực tế việc xử lý lỗi (Exception) rất đa dạng. Xe tải thủng lốp (`MaintenanceLog`) có lối giải quyết khác với hàng hóa bị nhầm lẫn trong kho. 
*   **Giải pháp OOP:**
    *   Tái thiết kế model `ProblemReport` như một Abstract Base Class.
    *   Cấu trúc thành các nhánh: `VehicleBreakdownReport`, `DamagedGoodsClaim` (đi kèm logic hoàn tiển `ProcessRefund()`). Cấu trúc kế thừa sẽ giúp công ty tái sử dụng chung mã lỗi nhưng có các kịch bản báo cáo cụ thể (Override).

### 5.4. Giải pháp Logistic bên thứ ba (3PL) (Áp dụng Trừu tượng Đa hình - Adapter Pattern)
*   **Vấn đề:** Mùa vận chuyển cuối năm, để khắc phục thiếu hụt phương tiện nội bộ (`Vehicle`), hệ thống có thể trung chuyển kiện hàng cho đối tác: GHN, VNPost.
*   **Giải pháp OOP:**
    *   Tạo giao diện lõi `ICarrier` đại diện mọi đối tượng có khả năng vận tải hợp lệ.
    *   Cả đối tượng `Driver` (nhà xe nội) và cổng `VNPostAdapter` (API ngoài) cùng implement giao diện này. Cấp quản lý (`Dispatcher`) vì thế điều phối đối tác thoải mái hệt như xe nội bộ qua cùng một cú pháp điều vận dòng dữ liệu.

### 5.5. Thuật toán tối ưu hóa đa lộ trình (Áp dụng Đóng gói nghiệp vụ)
*   **Vấn đề:** Khâu `GeoPoint` hiện mới thiết kế trên mạng tĩnh 2 chiều. Hệ chuyên chở cần có lộ trình phân mảnh cho chuyến xe nhận 20 đơn hàng lẻ rải rác (bài toán TSP, VRP).
*   **Giải pháp OOP:**
    *   Thiết kế độc lập module thuật toán `RouteOptimizer` xử lý riêng một danh sách vị trí tọa độ.
    *   Che giấu (Encapsulate) độ nặng tính toán của A*, Dijkstra hoặc Heuristic bên trong lớp `private`, tầng Interface phía ngoài chỉ giao tiếp bằng một hàm Public API duy nhất để kết xuất đối tượng lộ trình `List<GeoPoint> OptimizeAndGetRoute()`.
