using Logistics.Core.DataAccess.Interfaces;
using System.IO;

namespace Logistics.Core.DataAccess.Repositories
{
    // Factory Method Pattern - chi dung JSON (Newtonsoft.Json)
    public class RepositoryFactory
    {
        // Tao repository doc/ghi du lieu voi dinh dang JSON
        public static IRepository<T> CreateRepository<T>(string folderPath, string fileNameWithoutExt) where T : class
        {
            string fullPath = Path.Combine(folderPath, fileNameWithoutExt + ".json");
            return new JsonRepository<T>(fullPath);
        }
    }
}
