using System;
using System.Collections.Generic;
using System.IO;

namespace Logistics.Core.Utilities
{
    /// <summary>
    /// Helper class to load and access environment variables from a .env file.
    /// Implements lazy loading for "real-time" access without manual initialization.
    /// </summary>
    public static class EnvHelper
    {
        private static Dictionary<string, string>? _envData;
        private static readonly object _lock = new object();
        private const string EnvFileName = ".env";

        /// <summary>
        /// Gets a value from the .env file. Loads the file if not already loaded.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <param name="defaultValue">The value to return if the key is not found.</param>
        /// <returns>The value associated with the key, or defaultValue.</returns>
        public static string Get(string key, string defaultValue = "")
        {
            EnsureLoaded();
            Dictionary<string, string> envData = _envData ?? new Dictionary<string, string>();
            if (envData.TryGetValue(key, out string? value))
            {
                return value;
            }
            return defaultValue;
        }

        private static void EnsureLoaded()
        {
            if (_envData != null) return;

            lock (_lock)
            {
                if (_envData != null) return;

                _envData = new Dictionary<string, string>();
                
                // Try to find the .env file in common locations (Execution folder or Parent folders for dev)
                string? filePath = FindEnvFile();
                
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    return;
                }

                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;

                        int eqIndex = line.IndexOf('=');
                        if (eqIndex > 0)
                        {
                            string k = line.Substring(0, eqIndex).Trim();
                            string v = line.Substring(eqIndex + 1).Trim();
                            
                            // Remove quotes if present
                            if (v.StartsWith("\"") && v.EndsWith("\""))
                                v = v.Substring(1, v.Length - 2);
                            
                            if (!_envData.ContainsKey(k))
                                _envData.Add(k, v);
                        }
                    }
                }
                catch (Exception)
                {
                    // Fail silently, defaults will be used
                }
            }
        }

        private static string? FindEnvFile()
        {
            // 1. Check current directory (Bin folder usually)
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(currentDir, EnvFileName);
            if (File.Exists(path)) return path;

            // 2. Check parent directories (useful for development)
            string dir = currentDir;
            for (int i = 0; i < 4; i++) // Search up to 4 levels
            {
                DirectoryInfo? parent = Directory.GetParent(dir);
                if (parent == null) break;
                
                dir = parent.FullName;
                path = Path.Combine(dir, EnvFileName);
                if (File.Exists(path)) return path;
                
                // Check if we can find Logistics.WinFormsUI folder specifically
                string uiPath = Path.Combine(dir, "Logistics.WinFormsUI", EnvFileName);
                if (File.Exists(uiPath)) return uiPath;
            }

            return null;
        }
    }
}
