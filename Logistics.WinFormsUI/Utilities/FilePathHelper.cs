using System.IO;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class FilePathHelper
    {
        private static string _dataDirectory = string.Empty;

        public static void Initialize(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        public static string GetDataDirectory()
        {
            return _dataDirectory;
        }

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(_dataDirectory, fileName);
        }

        public static string GetJsonFilePath(string jsonFileName)
        {
            return Path.Combine(_dataDirectory, jsonFileName);
        }

        public static string GetApplicationDirectory()
        {
            return Application.StartupPath;
        }

        public static string GetDefaultDataDirectory()
        {
            return Path.Combine(Application.StartupPath, "Data");
        }
    }
}
