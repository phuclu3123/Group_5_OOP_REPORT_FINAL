# Phân công nhiệm vụ thực hiện các tính chất hướng đối tượng (OOP)

Danh sách dưởi đây trình bày chi tiết về việc áp dụng 4 tính chất OOP (Kế thừa, Đóng gói, Đa hình, Trừu tượng) vào các module của hệ thống dựa trên sự phân công của các thành viên.

---

## 1. Lữ Phúc - Module `Actors` (Quản lý Con người & Nhân sự)

**Lữ Phúc** chịu trách nhiệm thiết kế hệ thống phân cấp các thực thể tham gia vào hệ thống (Khách hàng, Nhân sự).

*   **Tính kế thừa (Inheritance):** 
    *   Thiết lập lớp cơ sở `Person` chứa các thông tin cá nhân cơ bản dùng chung. 
    *   Xây dựng các lớp `Customer` và `Staff` kế thừa trực tiếp từ `Person`. 
    *   Thực hiện kế thừa đa cấp (multilevel inheritance): Từ `Staff`, tiếp tục phân rã thành các vai trò cụ thể như `Driver` (Tài xế), `Dispatcher` (Điều phối viên) và `WarehouseStaff` (Nhân viên kho).
*   **Tính trừu tượng (Abstraction):** 
    *   Định nghĩa `Person` và `Staff` là các lớp trừu tượng (`abstract`). Ngăn chặn việc tạo ra các thực thể không rõ vai trò (không thể tạo đối tượng nhân viên chung chung mà phải cụ thể là tài xế hoặc nhân viên kho).
*   **Tính đa hình (Polymorphism):**
    *   Thực hiện Overriding (Ghi đè) phương thức trừu tượng `CalculateSalary()` và `GetSalaryBreakdown()` tại từng lớp nhân viên con (`Driver`, `Dispatcher`...), giúp tính lương đúng đặc thù từng vai trò.
    *   Cài đặt interface `ITrackable` trên đối tượng `Driver` để đồng bộ hành vi theo dõi định vị chung của hệ thống.
*   **Tính đóng gói (Encapsulation):**
    *   Gom nhóm dữ liệu và hành vi liên quan: `Driver` tự quản lý số chuyến giao hàng `DeliveryCount` thông qua hành vi `RecordDelivery()`, không cho phép bên ngoài gán trực tiếp (`private set`).

---

## 2. Hoàng Anh - Module `Business` (Quản lý Luồng Nghiệp vụ & Đơn hàng)

**Hoàng Anh** chịu trách nhiệm xây dựng cốt lõi luồng luân chuyển hàng hóa và các giao dịch kinh tế.

*   **Tính đóng gói (Encapsulation):**
    *   Che giấu cấu trúc nội bộ: Trong lớp `Order`, danh sách các kiện hàng (`Package`) được theo dõi dưới dạng private setter. Mọi thay đổi đều bắt buộc thông qua `AddPackage()` và `RemovePackage()`.
    *   Ẩn giấu logic tính toán: Phương thức `CalculateTotalWeight()` và `CalculateTotalCost()` tự động hoạt động bên trong để bảo toàn sự nhất quán của Dữ liệu tổng, không phụ thuộc vào lớp ngoài can thiệp.
*   **Tính đa hình (Polymorphism):**
    *   Triển khai Interface linh hoạt: Lớp `Order` cài đặt mạnh mẽ cả 2 interfaces là `ITrackable` (theo dõi hành trình đơn) và `IReportable` (sản sinh báo cáo thông qua `GenerateReport()`).
    *   `Package` cũng triển khai `ITrackable`, tạo nên sự đa hình cho phép hệ thống tra cứu được trạng thái của từng món hàng nhỏ lẫn toàn bộ đơn lớn.

---

## 3. Thanh Trúc - Module `Infrastructure` (Quản lý Cơ sở vật chất & Hạ tầng)

**Thanh Trúc** chịu trách nhiệm lập trình việc cấp phát và giám sát các trang thiết bị cấu thành Logistics (Xe cộ, Nhà kho...).

*   **Tính đóng gói (Encapsulation):**
    *   Quản lý an toàn vòng đời tài sản: Trong `Vehicle`, các yếu tố sống còn của xe như `FuelLevel` (Mức nhiên liệu) và `Status` (Trạng thái) được đóng gói. Việc tiêu hao nhiên liệu hay cập nhật Odometer (số km) được điều chỉnh qua hàm bảo vệ riêng (`UpdateFuelLevel`, `UpdateOdometer`) nhằm đề phòng dữ liệu âm hoặc vượt quá tải trọng `CanCarry()`.
    *   Cấu trúc phân vùng `Warehouse` và `WarehouseLocation` được che giấu kỹ thuật sắp xếp vị trí lưu kho.
*   **Tính đa hình (Polymorphism):**
    *   Tái sử dụng giao kèo Interface: Lớp `Vehicle` cài đặt `ITrackable`. Điều này cho phép một dịch vụ điều phối trung tâm in ra vị trí/trạng thái của cả `Vehicle` (do Trúc làm) và `Driver` (do Phúc làm) bằng cùng một cú pháp mà không cần mã rẽ nhánh.
    *   Ghi đè `ToString()` chuyên biệt cho từng trang thiết bị để xuất thông tin nhanh.
