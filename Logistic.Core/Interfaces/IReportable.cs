namespace Logistic.Core.Interfaces
{
    // Interface bao cao - ap dung cho cac doi tuong co the tao bao cao tong hop
    public interface IReportable
    {
        // Tao bao cao chi tiet
        string GenerateReport();
    }
}
