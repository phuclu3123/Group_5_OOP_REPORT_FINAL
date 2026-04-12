using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable

namespace Logistic.Core.Models.Business
{
    // Danh dau class co the duoc serialize
    [Serializable]
    public class Package : ISerializable
    {
        public string PackageID { get; private set; }
        public string OrderID { get; private set; }
        public string Description { get; private set; }
        public double ActualWeight { get; private set; }
        public string Dimensions { get; private set; }
        public double VolumeWeight { get; private set; }
        public bool IsFragile { get; private set; }

        public Package(string packageId, string orderId, string description,
                       double actualWeight, string dimensions, bool isFragile)
        {
            PackageID = packageId;
            OrderID = orderId;
            Description = description;
            ActualWeight = actualWeight;
            Dimensions = dimensions;
            IsFragile = isFragile;
            VolumeWeight = CalculateVolumeWeight();
        }

        // Constructor khong tham so cho serialization
        public Package() { }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong Package tu SerializationInfo khi doc tu file
        protected Package(SerializationInfo info, StreamingContext context)
        {
            PackageID = info.GetString("PackageID") ?? "";
            OrderID = info.GetString("OrderID") ?? "";
            Description = info.GetString("Description") ?? "";
            ActualWeight = info.GetDouble("ActualWeight");
            Dimensions = info.GetString("Dimensions") ?? "";
            VolumeWeight = info.GetDouble("VolumeWeight");
            IsFragile = info.GetBoolean("IsFragile");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua Package vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PackageID", PackageID);
            info.AddValue("OrderID", OrderID);
            info.AddValue("Description", Description);
            info.AddValue("ActualWeight", ActualWeight);
            info.AddValue("Dimensions", Dimensions);
            info.AddValue("VolumeWeight", VolumeWeight);
            info.AddValue("IsFragile", IsFragile);
        }

        // Tinh khoi luong quy doi tu kich thuoc (DxRxC cm / 5000)
        public double CalculateVolumeWeight()
        {
            try
            {
                string[] parts = Dimensions.Split('x');
                if (parts.Length == 3)
                {
                    double length = double.Parse(parts[0].Trim());
                    double width = double.Parse(parts[1].Trim());
                    double height = double.Parse(parts[2].Trim());
                    VolumeWeight = (length * width * height) / 5000.0;
                    return VolumeWeight;
                }
            }
            catch (Exception)
            {
                // Khong parse duoc kich thuoc
            }
            VolumeWeight = 0;
            return 0;
        }

        // Lay khoi luong tinh cuoc (lon hon giua thuc te va quy doi)
        public double GetChargeableWeight()
        {
            if (ActualWeight > VolumeWeight)
            {
                return ActualWeight;
            }
            return VolumeWeight;
        }

        // Cap nhat mo ta
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        // Cap nhat khoi luong thuc te
        public void UpdateActualWeight(double newWeight)
        {
            ActualWeight = newWeight;
        }

        // Lay thong tin goi hang
        public string GetPackageInfo()
        {
            string fragileText = "No";
            if (IsFragile)
            {
                fragileText = "Yes";
            }
            return "[Package] ID: " + PackageID + " | Order: " + OrderID + "\n" +
                   "  Desc: " + Description + " | Weight: " + ActualWeight + "kg | Vol: " + VolumeWeight + "kg\n" +
                   "  Dimensions: " + Dimensions + " | Fragile: " + fragileText;
        }

        public override string ToString()
        {
            return GetPackageInfo();
        }
    }
}