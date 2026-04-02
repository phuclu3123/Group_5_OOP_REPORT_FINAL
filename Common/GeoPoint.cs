using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable

namespace Cuoi_ky_OOP.Models.Common
{
    // Danh dau struct co the duoc serialize
    [Serializable]
    public struct GeoPoint : ISerializable
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong GeoPoint tu SerializationInfo khi doc tu file
        // Luu y: Struct dung public thay vi protected
        public GeoPoint(SerializationInfo info, StreamingContext context)
        {
            Latitude = info.GetDouble("Latitude");
            Longitude = info.GetDouble("Longitude");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua GeoPoint vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Latitude", Latitude);
            info.AddValue("Longitude", Longitude);
        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
}
