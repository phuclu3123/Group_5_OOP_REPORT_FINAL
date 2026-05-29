# Class Person.cs
- Các thuộc tính cơ bản của một người 
    1. Id của căn cước công dân
    2. Họ tên đầy đủ
    3. Số điện thoại
    4. Email
    5. Ngày sinh
    6. Giới tính
    7. Địa chỉ nhà (sử dụng lớp Address đã tạo)

- Constructor - method_Ise
    1. Constructor có tham số để khởi tạo đầy đủ thông tin của một người
    2. Constructor không tham số cho JSON serialization và các lớp con
    3. Phương thức ISerializable (Serialization)
 
- basic method 
    1. Cập nhật số điện thoại
    2. Cập nhật email
    3. Cập nhật địa chỉ
    4. Tính tuổi
    5. Phương thức hiển thị thông tin cơ bản của một người
    6. Override ToString() để hiển thị thông tin chi tiết của một người
 
# Class staff - kế thừa person 

