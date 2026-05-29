namespace Logistics.Core.Validations
{
    // Sử dụng Generics <T> để Interface này dùng được cho mọi Class
    public interface IValidator<T>
    {
        ValidationResult Validate(T entity);
    }
}