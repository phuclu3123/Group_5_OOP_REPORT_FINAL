# 📘 Tài Liệu Phân Tích & Thiết Kế Hệ Thống Logistics

Tài liệu này cung cấp cái nhìn chuyên sâu về quyết định thiết kế, áp dụng các nguyên lý OOP và giải thích chi tiết từng mối quan hệ trong biểu đồ lớp.

---

## 1. Bảng Giải Thích Ký Hiệu Visual (Visual Legend)

Để đọc hiểu biểu đồ Class Diagram, bạn cần nắm rõ ý nghĩa của các mũi tên và ký hiệu sau:

| Hình Ảnh Ký Hiệu | Tên Gọi (Tiếng Anh) | Ý Nghĩa & Cách Dùng Trong Đồ Án |
| :---: | :--- | :--- |
| **Tam giác rỗng** <br> ($\triangle$) | **Inheritance** (Kế thừa) | **"Là một" (IS-A)**. Lớp con kế thừa toàn bộ đặc điểm lớp cha. <br> *VD: `Driver` là một `Staff`.* |
| **Hình thoi đen** <br> ($\diamondsuit$) | **Composition** (Hợp thành) | **"Là một phần không thể tách rời"**. Nếu lớp cha hủy, lớp con cũng hủy theo. <br> *VD: `Order` hủy thì `Package` bên trong cũng mất.* |
| **Mũi tên hở** <br> ($\rightarrow$) | **Association** (Liên kết) | **"Có mối quan hệ với"**. 2 đối tượng biết nhau nhưng sống chết độc lập. <br> *VD: `Order` chứa thông tin `Customer`.* |
| **Nét đứt** <br> ($--$) | **Link** (Kết nối) | Nối một *Class* vào một *Association* (Mối quan hệ) để thêm thuộc tính cho mối quan hệ đó. <br> *VD: Nối `VehicleAssignment` vào giữa `Driver` và `Vehicle`.* |
| *Chữ nghiêng* | **Abstract Class** | Lớp trừu tượng, không thể tạo đối tượng (instance) từ nó. Chỉ dùng làm cha. |

---

## 2. Các Nguyên Lý Thiết Kế Được Áp Dụng (Design Principles)

### 1.1. Tính Kế Thừa (Inheritance) & DRY (Don't Repeat Yourself)
*   **Vấn đề**: `Customer`, `Driver`, `Dispatcher` đều có `Name`, `Phone`, `Email`. Nếu khai báo lặp lại ở từng lớp sẽ vi phạm nguyên tắc DRY.
*   **Giải pháp**: Tách các thuộc tính chung ra lớp cha trừu tượng `Person`.
    *   Lớp `Staff` kế thừa `Person` để thêm các thuộc tính đặc thù của nhân viên (`Salary`, `Department`), tách biệt hoàn toàn logic quản lý nhân sự với quản lý khách hàng.
    *   **Lợi ích**: Khi cần sửa logic validate số điện thoại, chỉ cần sửa tại `Person`.

### 1.2. Tính Trừu Tượng (Abstraction)
*   Lớp `Person` và `Staff` được đánh dấu là **Abstract**.
*   **Lý do**: Trong thực tế nghiệp vụ, không có đối tượng nào chỉ là "người" hay "nhân viên" chung chung. Họ phải đóng một vai trò cụ thể (Tài xế, Thủ kho, hay Khách hàng).
*   **Code**: Việc khởi tạo `new Person()` sẽ bị cấm (compile-time error), ngăn chặn dữ liệu rác.

### 1.3. Tính Đóng Gói (Encapsulation)
*   Tất cả thuộc tính quan trọng (`Balance`, `Salary`, `Status`) đều để `private` hoặc `protected`.
*   Truy cập dữ liệu phải thông qua các phương thức (Properties/Methods) để đảm bảo tính toàn vẹn (ví dụ: không thể sét `Salary < 0`).

---

## 2. Phân Tích Quan Hệ Giữa Các Lớp (Relationships Breakdown)

Đây là phần quan trọng nhất để hiểu "câu chuyện" của hệ thống qua biểu đồ.

### 2.1. Composition: Mối quan hệ Sống-Còn (Strong Ownership)
Ký hiệu: Hình thoi đen ($\diamondsuit$)
*   **`Order` $\diamondsuit$-- `Package`**:
    *   *Ý nghĩa*: Một Đơn hàng được cấu thành từ các Gói hàng.
    *   *Tại sao?*: Nếu xóa Đơn hàng, các Gói hàng bên trong không còn giá trị định danh hay nghiệp vụ nào cả. Chúng phụ thuộc hoàn toàn vào vòng đời của Đơn hàng.
*   **`Order` $\diamondsuit$-- `Transaction`**:
    *   *Ý nghĩa*: Giao dịch thanh toán là một phần nội tại của đơn hàng.
    *   *Logic*: Một giao dịch không thể tồn tại lơ lửng nếu không thuộc về đơn hàng nào.
*   **`Vehicle` $\diamondsuit$-- `MaintenanceLog`**:
    *   *Ý nghĩa*: Nhật ký bảo trì chỉ có ý nghĩa khi gắn liền với chiếc xe đó. Nếu xe bị bán hoặc hủy, nhật ký cũ chỉ là dữ liệu lưu trữ đi kèm hồ sơ xe.

### 2.2. Association: Mối quan hệ Hợp tác (Using/Knowing)
Ký hiệu: Mũi tên hở ($\rightarrow$)
*   **`Order` $\rightarrow$ `Customer` (Sender/Receiver)**:
    *   *Ý nghĩa*: Đơn hàng "biết" ai là người gửi.
    *   *Tại sao không phải Composition?*: Vì nếu hủy Đơn hàng, Khách hàng **vẫn tồn tại**. Khách hàng độc lập với Đơn hàng.
*   **`WarehouseStaff` $\rightarrow$ `Warehouse` (Works At)**:
    *   *Ý nghĩa*: Nhân viên làm việc tại Kho.
    *   *Logic*: Nhân viên có thể chuyển công tác sang kho khác. Kho bị đóng cửa thì nhân viên chuyển đi nơi khác, chứ không bị "xóa sổ".

### 2.3. Association Class: Mối quan hệ Phức tạp N-N
Ký hiệu: Đường nét đứt nối vào giữa liên kết ($--\rightarrow$)
*   **`VehicleAssignment`** (Giữa `Driver` và `Vehicle`):
    *   **Bài toán**: Một tài xế lái nhiều xe khác nhau trong tháng. Một chiếc xe được lái bởi nhiều tài xế (ca sáng/chiều). Đây là quan hệ nhiều-nhiều (N-N).
    *   **Thực tế nghiệp vụ**: Ta cần biết *CHÍNH XÁC* ông `A` lái xe `B` vào **khung giờ nào** và **đồng hồ công-tơ-mét** lúc nhận xe là bao nhiêu (để tính xăng xe tiêu hao).
    *   **Giải pháp**: Không thể để thuộc tính `StartTime` vào `Driver` (vì driver lái nhiều lần), cũng không thể để vào `Vehicle`. Phải tách ra lớp trung gian `VehicleAssignment`.

---

## 3. Chi Tiết Vai Trò Các Lớp (Class Responsibilities)

### Nhóm Quản Lý Cơ Sở Vật Chất (Infrastructure)
1.  **`Vehicle`**: Không chỉ là phương tiện, nó là một "Smart Asset".
    *   Quản lý `CurrentOdometer`: Tự động cảnh báo bảo trì khi đến ngưỡng km.
    *   Quản lý `CargoVolume`: Kiểm tra xem xe có chứa nổi kiện hàng kích thước lớn không (3D Bin Packing problem).
2.  **`Warehouse`**:
    *   Là một node trong đồ thị vận chuyển.
    *   Chứa `WarehouseLocation`: Quản lý vị trí chính xác (Bin/Slot) của hàng trong kho, hỗ trợ quy trình Pick-Pack (Lấy hàng - Đóng gói).

### Nhóm Nghiệp Vụ Cốt Lõi (Core Business)
1.  **`Order`**: Aggregate Root (Gốc của mọi nghiệp vụ).
    *   Tính toán `TotalCost` dựa trên `Packages`.
    *   Quản lý trạng thái (`Status State Machine`) để đảm bảo quy trình đúng (Không thể nhảy từ `Pending` sang `Delivered` mà không qua `InTransit`).
2.  **`ShipmentLog`**: Hộp đen ghi lại dấu vết (`Audit Trail`).
    *   Mọi thay đổi trạng thái đơn hàng đều sinh ra một Log. Giúp tra cứu lịch sử khi có khiếu nại (VD: Tại sao hàng nằm ở kho A quá 3 ngày?).
3.  **`ProblemReport`**: Xử lý ngoại lệ.
    *   Khi hàng vỡ/mất, hệ thống không chỉ đổi status đơn hàng mà phải sinh ra một `Report` để bộ phận CSKH xử lý bồi thường.

---

## 4. Kịch Bản Nghiệp Vụ (Scenario Walkthrough)

Để chứng minh thiết kế này hoạt động, hãy xem xét luồng đi của một đơn hàng:

1.  **Khởi tạo**: Khách hàng (`Customer`) tạo `Order`. Hệ thống tạo các `Package` bên trong (Composition). Status = `Pending`.
2.  **Điều phối**: `Dispatcher` tìm tài xế.
3.  **Gán xe**: Tài xế (`Driver`) nhận một `Vehicle`. Hệ thống tạo ra một bản ghi `VehicleAssignment` với `StartTime = Now`, `StartOdometer = Vehicle.CurrentOdometer`.
4.  **Vận chuyển**:
    *   Xe đến kho lấy hàng -> Tạo `ShipmentLog`.
    *   Tài xế cập nhật `Vehicle.CurrentLocation` liên tục.
5.  **Sự cố**: Nếu xe hỏng -> Cập nhật `Vehicle.Status = Broken` -> Tạo `MaintenanceLog`. Đơn hàng chuyển sang xe khác.
6.  **Hoàn tất**: Giao hàng thành công -> `Transaction.Status = Completed`. `Driver` trả xe -> Cập nhật `VehicleAssignment.EndTime` và `VehicleAssignment.EndOdometer`.

---
*Tài liệu này dùng kèm với bản vẽ kỹ thuật Class_Diagram.tex để bảo vệ đồ án.*
