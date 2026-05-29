// DTO nhẹ cho thông tin User — dùng trong form quản lý tài khoản (không lộ PasswordHash)
using System;

namespace Logistics.Core.DTOs
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu tài khoản người dùng (UserDTO).
    /// Sử dụng ở tầng giao diện WinForms để hiển thị danh sách tài khoản mà không làm lộ các thông tin nhạy cảm (như PasswordHash, PasswordSalt).
    /// </summary>
    public class UserDTO
    {
        /// <summary> Mã định danh duy nhất của tài khoản. </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary> Tên đăng nhập của tài khoản. </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary> Vai trò phân quyền trong hệ thống (Admin, Driver, Dispatcher, WarehouseStaff, Customer). </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary> Mã định danh cá nhân liên kết (nếu có). </summary>
        public string LinkedPersonId { get; set; } = string.Empty;

        /// <summary> Họ tên đầy đủ của nhân viên hoặc khách hàng liên kết. </summary>
        public string LinkedPersonName { get; set; } = string.Empty;

        /// <summary> Trạng thái hoạt động của tài khoản (true: kích hoạt, false: bị khóa). </summary>
        public bool IsActive { get; set; }

        /// <summary> Đánh dấu tài khoản bắt buộc phải đổi mật khẩu ở lần đăng nhập tiếp theo. </summary>
        public bool MustChangePassword { get; set; }

        /// <summary> Ngày tạo tài khoản. </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary> Đường dẫn tệp ảnh đại diện của người dùng. </summary>
        public string AvatarPath { get; set; } = string.Empty;
    }
}
