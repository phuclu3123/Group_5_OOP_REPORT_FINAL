using Logistics.Core.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Logistics.Core.DataAccess.Repositories
{
    /// <summary>
    /// Lớp cơ sở (Base Class) cho kho lưu trữ dữ liệu dạng JSON.
    /// Triển khai các phương thức cơ bản của mẫu thiết kế Repository (DAL) dùng Newtonsoft.Json.
    /// </summary>
    /// <typeparam name="T">Kiểu thực thể dữ liệu cần quản lý.</typeparam>
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        /// <summary> Danh sách dữ liệu thực thể được nạp vào bộ nhớ tạm (RAM). </summary>
        protected List<T> _items;

        /// <summary> Đường dẫn tuyệt đối dẫn đến tệp lưu trữ JSON. </summary>
        protected string _filePath;

        /// <summary> Đối tượng khóa dùng để khóa tệp khi đọc ghi đa luồng (Thread-safety). </summary>
        private static readonly object _fileLock = new object();

        /// <summary> Cấu hình tuần tự hóa và giải tuần tự hóa của Newtonsoft.Json. </summary>
        private JsonSerializerSettings _options;

        /// <summary> Đánh dấu trạng thái nạp dữ liệu thất bại từ lần trước. </summary>
        private bool _dataLoadFailed;

        /// <summary> Nội dung thông báo lỗi khi nạp dữ liệu thất bại. </summary>
        private string _loadErrorMessage;

        /// <summary>
        /// Khởi tạo Repository và tự động nạp dữ liệu từ tệp JSON.
        /// </summary>
        /// <param name="filePath">Đường dẫn tệp JSON dữ liệu.</param>
        public JsonRepository(string filePath)
        {
            _filePath = filePath;
            _items = new List<T>();
            _options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = LogisticsSerializationBinder.Instance
            };
            _dataLoadFailed = false;
            _loadErrorMessage = string.Empty;

            string? directory = Path.GetDirectoryName(_filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            LoadData();
        }

        /// <summary>
        /// Lấy toàn bộ danh sách thực thể từ bộ nhớ tạm.
        /// </summary>
        public List<T> GetAll()
        {
            return new List<T>(_items);
        }

        /// <summary>
        /// Tìm kiếm thực thể theo mã định danh (ID). Phương thức ảo để lớp con ghi đè.
        /// </summary>
        public virtual T GetById(string id)
        {
            return null!; // Sẽ được ghi đè ở các Repository con cụ thể
        }

        /// <summary>
        /// Thử tìm kiếm thực thể theo ID.
        /// </summary>
        public virtual bool TryGetById(string id, out T? entity)
        {
            entity = GetById(id);
            return entity != null;
        }

        /// <summary>
        /// Thêm mới một thực thể vào bộ nhớ tạm và ghi đè xuống tệp JSON.
        /// </summary>
        public void Add(T entity)
        {
            _items.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// Cập nhật thực thể. Phương thức ảo để lớp con ghi đè.
        /// </summary>
        public virtual void Update(T entity)
        {
            // Được ghi đè ở lớp kế thừa
        }

        /// <summary>
        /// Xóa thực thể theo ID. Phương thức ảo để lớp con ghi đè.
        /// </summary>
        public virtual void Delete(string id)
        {
        }

        /// <summary>
        /// Ghi đồng bộ dữ liệu hiện tại xuống tệp JSON.
        /// Sử dụng cơ chế ghi an toàn (Safe Write): viết ra file tạm .tmp, sao lưu file cũ sang .bak rồi mới thay thế file chính.
        /// </summary>
        public void SaveChanges()
        {
            lock (_fileLock)
            {
                if (_dataLoadFailed)
                {
                    throw new IOException("Khong the luu du lieu vi lan doc JSON truoc do bi loi: " + _loadErrorMessage);
                }

                try
                {
                    string jsonString = JsonConvert.SerializeObject(_items, _options);
                    SafeWriteText(_filePath, jsonString);
                }
                catch (Exception ex)
                {
                    throw new IOException("Loi khi luu file JSON: " + _filePath, ex);
                }
            }
        }

        public void LoadData()
        {
            lock (_fileLock)
            {
                _dataLoadFailed = false;
                _loadErrorMessage = string.Empty;

                if (!File.Exists(_filePath))
                {
                    _items = new List<T>();
                    return;
                }

                try
                {
                    string jsonString = File.ReadAllText(_filePath, System.Text.Encoding.UTF8);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        List<T>? result = JsonConvert.DeserializeObject<List<T>>(jsonString, _options);
                        if (result != null)
                        {
                            _items = result;
                        }
                        else
                        {
                            _items = new List<T>();
                        }
                    }
                    else
                    {
                        _items = new List<T>();
                    }
                }
                catch (Exception ex)
                {
                    if (TryLoadBackup())
                    {
                        return;
                    }

                    PreserveCorruptFile();
                    _items = new List<T>();
                    _dataLoadFailed = true;
                    _loadErrorMessage = ex.Message;
                }
            }
        }

        public int Count()
        {
            return _items.Count;
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

        private bool TryLoadBackup()
        {
            string backupPath = _filePath + ".bak";
            if (!File.Exists(backupPath))
            {
                return false;
            }

            try
            {
                string jsonString = File.ReadAllText(backupPath, System.Text.Encoding.UTF8);
                List<T>? result = JsonConvert.DeserializeObject<List<T>>(jsonString, _options);
                if (result == null)
                {
                    return false;
                }

                _items = result;
                _dataLoadFailed = false;
                _loadErrorMessage = string.Empty;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void PreserveCorruptFile()
        {
            if (!File.Exists(_filePath))
            {
                return;
            }

            try
            {
                string corruptPath = _filePath + ".corrupt." + DateTime.Now.ToString("yyyyMMddHHmmss");
                File.Copy(_filePath, corruptPath, false);
            }
            catch
            {
                // Khong duoc ghi de file loi neu khong tao duoc ban copy bao toan.
            }
        }
    }
}
