<div align="center">

# Group 5 - Logistic Management System
### Đồ án cuối kỳ: Lập trình Hướng đối tượng (OOP)

![Language](https://img.shields.io/badge/Language-C%23-blue)
![Platform](https://img.shields.io/badge/Platform-.NET%20Framework-purple)
![GUI](https://img.shields.io/badge/GUI-WinForms-green)
![Status](https://img.shields.io/badge/Status-Completed-success)

</div>

---

## 📑 Mục lục
1. [Giới thiệu](#-giới-thiệu)
2. [Thành viên thực hiện](#-thành-viên-thực-hiện)
3. [Công nghệ sử dụng](#-công-nghệ-sử-dụng)
4. [Các chức năng chính](#-các-chức-năng-chính)
5. [Áp dụng Hướng đối tượng](#-áp-dụng-hướng-đối-tượng)
6. [Cài đặt và Hướng dẫn](#-cài-đặt-và-hướng-dẫn)

---

## 📖 Giới thiệu
**Logistic Management System** là phần mềm quản lý vận chuyển hàng hóa được xây dựng nhằm tối ưu hóa quy trình logistic cho các công ty chuyển phát. Dự án tập trung vào việc áp dụng triệt để các nguyên lý của **Lập trình hướng đối tượng (OOP)** để xây dựng một hệ thống có cấu trúc chặt chẽ, dễ bảo trì và mở rộng.

Mục tiêu của phần mềm:
* Quản lý đơn hàng và quy trình vận chuyển.
* Theo dõi trạng thái giao nhận.
* Quản lý đội ngũ nhân viên và kho bãi.

---

## 👥 Thành viên thực hiện

| STT | Họ và tên | Mã số sinh viên | Vai trò (Dự kiến) |
|:---:|:---|:---|:---|
| 1 | **Lữ Võ Hoàng Phúc** | 31241025389 | Nhóm trưởng / Backend / Database |
| 2 | **Nguyễn Hoàng Anh** | 31241021759 | Frontend / UI Design |
| 3 | **Từ Trương Thanh Trúc** | 31241023563 | Tester / Documentation |

*(Lưu ý: Bạn hãy cập nhật thêm MSSV và vai trò cụ thể cho từng bạn nhé)*

---

## 🛠 Công nghệ sử dụng

* **Ngôn ngữ lập trình:** C#
* **Framework:** .NET (Windows Forms Application)
* **Cơ sở dữ liệu:** SQL Server
* **Công cụ phát triển:** Visual Studio 2022
* **Mô hình thiết kế:** 3-Tier Architecture (3 lớp) hoặc MVC (tùy thực tế dự án của bạn)

---

## 🚀 Các chức năng chính

### 1. Quản lý Đơn hàng (Orders)
* Tạo mới, sửa đổi và hủy đơn vận chuyển.
* Tính toán cước phí tự động dựa trên khối lượng và khoảng cách.
* Cập nhật trạng thái: *Đang xử lý -> Đang giao -> Đã giao / Hoàn trả*.

### 2. Quản lý Kho bãi (Warehousing)
* Nhập/Xuất hàng hóa.
* Kiểm soát tồn kho và vị trí lưu trữ.

### 3. Quản lý Nhân sự & Khách hàng
* Phân quyền quản trị viên (Admin) và nhân viên (Staff).
* Lưu trữ thông tin khách hàng và lịch sử giao dịch.

### 4. Báo cáo & Thống kê
* Thống kê doanh thu theo tháng/quý.
* Báo cáo hiệu suất giao hàng.

---

## 🧩 Áp dụng Hướng đối tượng (OOP Principles)

Dự án minh họa rõ nét 4 tính chất cơ bản của OOP:

1.  **Tính đóng gói (Encapsulation):** Dữ liệu của các đối tượng (Đơn hàng, Nhân viên) được bảo vệ qua các property và phương thức `private/public`.
2.  **Tính kế thừa (Inheritance):** Xây dựng các lớp cơ sở (ví dụ: `Person`) cho `Employee` và `Customer` kế thừa để tái sử dụng mã nguồn.
3.  **Tính đa hình (Polymorphism):** Xử lý các phương thức tính lương hoặc tính phí vận chuyển khác nhau tùy theo loại hình (Giao nhanh, Giao tiết kiệm).
4.  **Tính trừu tượng (Abstraction):** Sử dụng `Interface` hoặc `Abstract Class` để định nghĩa các hành vi chung cho hệ thống.

---

## 💻 Cài đặt và Hướng dẫn

1.  **Clone repository:**
    ```bash
    git clone [https://github.com/username/Group7_Logistic_OOP.git](https://github.com/username/Group7_Logistic_OOP.git)
    ```
2.  **Cấu hình Database:**
    * Mở SQL Server Management Studio.
    * Chạy script `database.sql` trong thư mục `Database`.
    * Cập nhật `ConnectionString` trong file `App.config`.
3.  **Chạy ứng dụng:**
    * Mở solution bằng Visual Studio.
    * Nhấn `F5` để Build và Run.

---

<div align="center">

**Group 7 - OOP Final Project**
<br>
*Cảm ơn thầy cô và các bạn đã quan tâm!*

</div>
