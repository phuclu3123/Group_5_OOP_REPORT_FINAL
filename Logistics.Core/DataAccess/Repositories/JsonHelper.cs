using Logistics.Core.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace Logistics.Core.DataAccess.Repositories
{
    /// <summary>
    /// Thread-safe JSON serialization/deserialization helper using ReaderWriterLockSlim.
    /// </summary>
    public static class JsonHelper
    {
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            SerializationBinder = LogisticsSerializationBinder.Instance
        };

        public static List<T> ReadAll<T>(string filePath)
        {
            _lock.EnterReadLock();
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }

                List<T>? result = JsonConvert.DeserializeObject<List<T>>(json, _settings);
                return result != null ? result : new List<T>();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public static void WriteAll<T>(string filePath, List<T> data)
        {
            _lock.EnterWriteLock();
            try
            {
                string json = JsonConvert.SerializeObject(data, _settings);
                SafeWriteText(filePath, json);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public static void EnsureDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static T ReadSingle<T>(string filePath) where T : new()
        {
            _lock.EnterReadLock();
            try
            {
                if (!File.Exists(filePath))
                {
                    return new T();
                }

                string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new T();
                }

                T? result = JsonConvert.DeserializeObject<T>(json, _settings);
                return result != null ? result : new T();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public static void WriteSingle<T>(string filePath, T data)
        {
            _lock.EnterWriteLock();
            try
            {
                string json = JsonConvert.SerializeObject(data, _settings);
                SafeWriteText(filePath, json);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private static void SafeWriteText(string filePath, string content)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string tempPath = filePath + ".tmp";
            string backupPath = filePath + ".bak";
            File.WriteAllText(tempPath, content, System.Text.Encoding.UTF8);

            if (File.Exists(filePath))
            {
                File.Copy(filePath, backupPath, true);
            }

            File.Copy(tempPath, filePath, true);
            File.Delete(tempPath);
        }
    }
}
