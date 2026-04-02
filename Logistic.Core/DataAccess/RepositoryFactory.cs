using System;

namespace Cuoi_ky_OOP.Models.DataAccess
{
    /// <summary>
    /// Factory Method Pattern: Tu dong tao va tra ve Repository phu hop
    /// He thong se tu dong biet luc nao nen tra ve JsonRepository,
    /// luc nao nen tra ve XmlRepository dua vao tham so truyen vao
    /// </summary>
    public static class RepositoryFactory
    {
        /// <summary>
        /// Enum dinh nghia cac loai repository duoc ho tro
        /// </summary>
        public enum RepositoryType
        {
            Json, // Luu tru duoi dinh dang JSON (.json)
            Xml   // Luu tru duoi dinh dang XML (.xml)
        }

        /// <summary>
        /// Factory Method: Tao va tra ve instance cua Repository tuong ung
        /// Su dung Generic <T> de ho tro bat ky kieu Model nao (Order, Package, ...)
        /// </summary>
        /// <typeparam name="T">Kieu doi tuong can luu tru</typeparam>
        /// <param name="type">Loai repository muon tao (Json hoac Xml)</param>
        /// <returns>Instance cua IRepository<T> tuong ung</returns>
        public static IRepository<T> CreateRepository<T>(RepositoryType type)
        {
            // Dung switch de quyet dinh tao loai repository nao
            switch (type)
            {
                case RepositoryType.Json:
                    // Tra ve kho luu tru JSON (su dung Newtonsoft.Json)
                    return new JsonRepository<T>();

                case RepositoryType.Xml:
                    // Tra ve kho luu tru XML (su dung Newtonsoft.Json chuyen doi JSON <-> XML)
                    return new XmlRepository<T>();

                default:
                    // Nem loi neu loai repository khong hop le
                    throw new ArgumentException("Loai repository khong hop le: " + type);
            }
        }
    }
}
