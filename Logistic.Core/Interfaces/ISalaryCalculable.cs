namespace Logistic.Core.Interfaces
{
    // Interface tinh luong - moi loai nhan vien tinh luong khac nhau (Polymorphism)
    public interface ISalaryCalculable
    {
        // Tinh tong luong
        decimal CalculateSalary();

        // Lay chi tiet cac khoan luong
        string GetSalaryBreakdown();
    }
}
