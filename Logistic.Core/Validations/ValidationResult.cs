using System.Collections.Generic;

namespace Logistic.Core.Validations
{
    // ============================================================
    // Lop chua ket qua kiem tra tinh hop le (Validation Result)
    // Encapsulation: Ket qua chi duoc thay doi thong qua AddError(),
    // khong the truc tiep sua IsValid hoac danh sach Errors tu ben ngoai.
    // ============================================================
    public class ValidationResult
    {
        // ===== PROPERTIES =====

        // Ket qua kiem tra: true neu hop le, false neu co loi
        public bool IsValid { get; private set; }

        // Danh sach cac loi duoc phat hien
        public List<string> Errors { get; private set; }

        // ===== CONSTRUCTOR =====

        // Khoi tao ket qua mac dinh la hop le (chua co loi)
        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        // ===== METHODS =====

        // Them mot loi vao danh sach va danh dau ket qua la khong hop le
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            IsValid = false;
        }

        // Lay tong hop ket qua kiem tra dang chuoi
        public string GetSummary()
        {
            if (IsValid)
            {
                return "[Validation] THANH CONG - Khong co loi.";
            }

            string summary = "[Validation] THAT BAI - Phat hien " + Errors.Count + " loi:\n";
            for (int i = 0; i < Errors.Count; i++)
            {
                summary += "  " + (i + 1) + ". " + Errors[i] + "\n";
            }
            return summary;
        }

        // Lay so luong loi
        public int GetErrorCount()
        {
            return Errors.Count;
        }

        // Gop ket qua tu mot ValidationResult khac vao ket qua hien tai
        public void Merge(ValidationResult other)
        {
            for (int i = 0; i < other.Errors.Count; i++)
            {
                AddError(other.Errors[i]);
            }
        }

        public override string ToString()
        {
            return GetSummary();
        }
    }
}
