namespace Logistic.Core.Validations
{
    // ============================================================
    // Generic Interface Validator
    // Dinh nghia hop dong kiem tra tinh hop le cho bat ky doi tuong nao.
    // Ap dung Abstraction: cac lop con tu quyet dinh logic validate.
    // ============================================================
    // Typeparam T: Kieu du lieu cua doi tuong can validate
    public interface IValidator<T>
    {
        // Thuc hien kiem tra tinh hop le cua doi tuong
        // Tra ve ValidationResult chua ket qua va danh sach loi (neu co)
        ValidationResult Validate(T entity);
    }
}
