using System.Collections.Generic;

namespace Logistics.Core.Validations
{
    public class ValidationResult
    {
        // Thuộc tính kiểm tra xem có hợp lệ không (True nếu không có lỗi nào)
        public bool IsValid
        {
            get { return Errors.Count == 0; }
        }
        
        // Danh sách chứa các thông báo lỗi
        public List<string> Errors { get; } = new List<string>();

        // Hàm thêm lỗi
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}