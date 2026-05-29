using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Logistics.WinFormsUI
{
    public static class AppSettings
    {
        // JSON data directory (relative to exe location)
        public static string DataDirectory { get; private set; } = string.Empty;
        private const string SettingsFileName = "app.settings";

        // Application name
        public const string ApplicationName = "Logistics Management System";
        public const string Version = "1.0.0";

        // Default settings
        public const int DefaultFormWidth = 1200;
        public const int DefaultFormHeight = 750;
        public const bool RememberLastUser = false;

        public static void Initialize()
        {
            string persistedPath = LoadPersistedDataDirectory();
            bool usedDefaultPath = string.IsNullOrWhiteSpace(persistedPath);
            if (usedDefaultPath)
            {
                DataDirectory = Path.Combine(Application.StartupPath, "Data");
            }
            else
            {
                DataDirectory = persistedPath;
            }
            EnsureDataDirectory();
            if (usedDefaultPath)
            {
                PersistDataDirectory();
            }
        }

        public static void Initialize(string customDataDirectory)
        {
            DataDirectory = customDataDirectory;
            EnsureDataDirectory();
        }

        public static void SaveDataDirectory(string customDataDirectory)
        {
            Initialize(customDataDirectory);
            PersistDataDirectory();
        }

        private static string LoadPersistedDataDirectory()
        {
            string settingsPath = GetSettingsPath();
            try
            {
                if (!File.Exists(settingsPath))
                {
                    return string.Empty;
                }

                string persistedPath = File.ReadAllText(settingsPath, Encoding.UTF8).Trim();
                if (Directory.Exists(persistedPath))
                {
                    return persistedPath;
                }
            }
            catch (IOException)
            {
                return string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        private static string GetSettingsPath()
        {
            return Path.Combine(Application.StartupPath, SettingsFileName);
        }

        private static void EnsureDataDirectory()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        private static void PersistDataDirectory()
        {
            try
            {
                string settingsPath = GetSettingsPath();
                File.WriteAllText(settingsPath, DataDirectory, Encoding.UTF8);
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}
