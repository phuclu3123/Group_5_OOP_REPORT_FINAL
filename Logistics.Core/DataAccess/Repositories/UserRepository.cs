using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.Models.Account;

namespace Logistics.Core.DataAccess.Repositories
{
    /// <summary>
    /// Kho lưu trữ tài khoản người dùng kế thừa từ JsonRepository.
    /// Quản lý dữ liệu người dùng lưu trong tệp JSON.
    /// </summary>
    public class UserRepository : JsonRepository<User>
    {
        public UserRepository(string filePath) : base(filePath)
        {
        }

        /// <summary>
        /// Tìm tài khoản người dùng theo tên đăng nhập (không phân biệt chữ hoa thường).
        /// </summary>
        public User FindByUsername(string username)
        {
            System.Collections.Generic.List<User> all = GetAll();
            foreach (User user in all)
            {
                if (string.Equals(user.Username, username, System.StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            return null!;
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại trong hệ thống chưa.
        /// </summary>
        public bool UsernameExists(string username)
        {
            return FindByUsername(username) != null;
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản người dùng hiện có.
        /// </summary>
        public override void Update(User entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].UserId == entity.UserId || string.Equals(_items[i].Username, entity.Username, System.StringComparison.OrdinalIgnoreCase))
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }

        /// <summary>
        /// Xóa tài khoản người dùng theo ID hoặc tên đăng nhập.
        /// </summary>
        public override void Delete(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].UserId == id || string.Equals(_items[i].Username, id, System.StringComparison.OrdinalIgnoreCase))
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }
    }
}
