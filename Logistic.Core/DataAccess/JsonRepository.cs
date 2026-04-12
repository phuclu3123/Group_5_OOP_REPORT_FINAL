using Newtonsoft.Json; // Su dung Newtonsoft.Json (JsonConvert) de serialize/deserialize
using System;
using System.Collections.Generic;
using System.IO;
using Logistic.Core.Interfaces; // IRepository<T> da chuyen sang Interfaces

namespace Logistic.Core.DataAccess
{
    /// <summary>
    /// Hien thuc hoa kho luu tru du lieu duoi dinh dang JSON
    /// Su dung thu vien Newtonsoft.Json (JsonConvert) de chuyen doi doi tuong <-> chuoi JSON
    /// </summary>
    /// <typeparam name="T">Kieu du lieu cua doi tuong can luu tru</typeparam>
    public class JsonRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Luu danh sach doi tuong vao file .json
        /// Buoc 1: Chuyen doi danh sach thanh chuoi JSON bang JsonConvert.SerializeObject
        /// Buoc 2: Ghi chuoi JSON vao file bang File.WriteAllText
        /// </summary>
        public void Save(string filePath, List<T> data)
        {
            // Chuyen doi doi tuong thanh chuoi JSON voi dinh dang dep (Indented)
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Ghi chuoi JSON vao file
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Doc danh sach doi tuong tu file .json
        /// Buoc 1: Doc toan bo noi dung file bang File.ReadAllText
        /// Buoc 2: Chuyen doi chuoi JSON nguoc lai thanh doi tuong bang JsonConvert.DeserializeObject
        /// </summary>
        public List<T> Load(string filePath)
        {
            // Kiem tra file co ton tai khong, neu khong tra ve danh sach rong
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            // Doc toan bo noi dung file JSON
            string json = File.ReadAllText(filePath);

            // Chuyen doi chuoi JSON nguoc lai thanh danh sach doi tuong
            List<T> result = JsonConvert.DeserializeObject<List<T>>(json);

            // Neu ket qua null (file rong hoac loi), tra ve danh sach rong
            return result ?? new List<T>();
        }
    }
}
