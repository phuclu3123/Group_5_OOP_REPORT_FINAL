namespace Logistic.Core.Interfaces
{
    /// <summary>
    /// Giao dien Generic Repository (Kho luu tru chung)
    /// Dinh nghia cac thao tac co ban: Luu (Save) va Doc (Load) du lieu
    /// </summary>
    /// <typeparam name="T">Kieu du lieu cua doi tuong can luu tru</typeparam>
    
    public interface IRepository<T>
    {
        /// <summary>
        /// Luu danh sach doi tuong vao file tai duong dan chi dinh
        /// </summary>
        /// <param name="filePath">Duong dan file luu tru (VD: "orders.json")</param>
        /// <param name="data">Danh sach doi tuong can luu</param>
        void Save(string filePath, List<T> data);

        /// <summary>
        /// Doc danh sach doi tuong tu file tai duong dan chi dinh
        /// </summary>
        /// <param name="filePath">Duong dan file can doc</param>
        /// <returns>Danh sach doi tuong duoc phuc hoi tu file</returns>
        List<T> Load(string filePath);
    }
}
