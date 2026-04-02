using Newtonsoft.Json; // Su dung Newtonsoft.Json de chuyen doi JSON <-> XML
using System;
using System.Collections.Generic;
using System.IO;

namespace Cuoi_ky_OOP.Models.DataAccess
{
    /// <summary>
    /// Hien thuc hoa kho luu tru du lieu duoi dinh dang XML
    /// KHONG su dung System.Xml.Serialization
    /// Thay vao do, su dung Newtonsoft.Json de chuyen doi:
    ///   Doi tuong -> JSON -> XML (khi luu)
    ///   XML -> JSON -> Doi tuong (khi doc)
    /// </summary>
    /// <typeparam name="T">Kieu du lieu cua doi tuong can luu tru</typeparam>
    public class XmlRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Luu danh sach doi tuong vao file .xml
        /// Quy trinh: Doi tuong -> Serialize thanh JSON -> Chuyen JSON sang XML -> Ghi file
        /// </summary>
        public void Save(string filePath, List<T> data)
        {
            // Buoc 1: Chuyen doi danh sach doi tuong thanh chuoi JSON
            string json = JsonConvert.SerializeObject(data);

            // Buoc 2: Boc JSON vao mot root object de JsonConvert co the chuyen sang XML
            // Vi JSON array khong the chuyen truc tiep sang XML, can boc vao { "Items": [...] }
            string wrappedJson = "{\"Items\":" + json + "}";

            // Buoc 3: Chuyen JSON thanh XmlDocument bang Newtonsoft (KHONG dung System.Xml.Serialization)
            System.Xml.XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(wrappedJson, "Root");

            // Buoc 4: Ghi noi dung XML ra file
            if (xmlDoc != null)
            {
                // Tao XmlWriterSettings de dinh dang XML cho de doc
                System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                settings.Indent = true; // Them tab/space de de doc

                // Ghi XML vao file voi dinh dang dep
                using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(filePath, settings))
                {
                    xmlDoc.Save(writer);
                }
            }
        }

        /// <summary>
        /// Doc danh sach doi tuong tu file .xml
        /// Quy trinh: Doc file XML -> Chuyen XML sang JSON -> Deserialize JSON thanh doi tuong
        /// </summary>
        public List<T> Load(string filePath)
        {
            // Kiem tra file co ton tai khong
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            // Buoc 1: Doc noi dung file XML
            string xml = File.ReadAllText(filePath);

            // Buoc 2: Load XML vao XmlDocument
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            // Buoc 3: Chuyen XML nguoc lai thanh JSON bang Newtonsoft
            string json = JsonConvert.SerializeXmlNode(xmlDoc.DocumentElement, Newtonsoft.Json.Formatting.None, true);

            // Buoc 4: Doc gia tri cua key "Items" ra de lay mang JSON goc
            // Vi khi luu, ta da boc vao { "Items": [...] }
            var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(json);
            string itemsJson = jsonObj["Items"]?.ToString() ?? "[]";

            // Buoc 5: Deserialize JSON thanh danh sach doi tuong
            List<T> result = JsonConvert.DeserializeObject<List<T>>(itemsJson);

            return result ?? new List<T>();
        }
    }
}
