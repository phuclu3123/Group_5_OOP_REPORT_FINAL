using System;
using System.Globalization;
using System.Text;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Business
{
    public class Package
    {
        public string PackageID { get; private set; }
        public string OrderID { get; private set; }
        public string Description { get; private set; }
        public double ActualWeight { get; private set; }
        public string Dimensions { get; private set; }
        public double VolumeWeight { get; private set; }
        public bool IsFragile { get; private set; }
        public decimal Value { get; private set; }
        public string ItemCategory { get; private set; }
        public string HandlingInstructions { get; private set; }
        public PackageStatus Status { get; private set; }
        public string CurrentWarehouseID { get; private set; }
        public string CurrentShelfLocation { get; private set; }
        public DateTime? LastScannedAt { get; private set; }

        public Package(string packageId, string orderId, string description,
                       double actualWeight, string dimensions, bool isFragile,
                       decimal value = 0, string itemCategory = "General", string handlingInstructions = "")
        {
            PackageID = packageId;
            OrderID = orderId;
            Description = description;
            ActualWeight = actualWeight;
            Dimensions = dimensions;
            IsFragile = isFragile;
            Value = value;
            ItemCategory = itemCategory;
            HandlingInstructions = handlingInstructions;
            Status = PackageStatus.Created;
            CurrentWarehouseID = string.Empty;
            CurrentShelfLocation = string.Empty;
            LastScannedAt = null;
            VolumeWeight = CalculateVolumeWeight();
        }

        // Constructor khong tham so cho JSON serialization
        public Package() 
        {
            PackageID = string.Empty;
            OrderID = string.Empty;
            Description = string.Empty;
            Dimensions = string.Empty;
            ItemCategory = string.Empty;
            HandlingInstructions = string.Empty;
            CurrentWarehouseID = string.Empty;
            CurrentShelfLocation = string.Empty;
        }

        // Tinh khoi luong quy doi tu kich thuoc (DxRxC cm / 5000)
        public double CalculateVolumeWeight()
        {
            double length;
            double width;
            double height;
            if (TryParseDimensions(Dimensions, out length, out width, out height))
            {
                VolumeWeight = (length * width * height) / 5000.0;
                return VolumeWeight;
            }

            VolumeWeight = 0;
            return 0;
        }

        // Tinh the tich vat ly thuc te (m3) tu kich thuoc cm (DxRxC)
        public double GetPhysicalVolume()
        {
            double length;
            double width;
            double height;
            if (TryParseDimensions(Dimensions, out length, out width, out height))
            {
                return (length * width * height) / 1000000.0;
            }

            return 0.0;
        }

        private static bool TryParseDimensions(string dimensions, out double length, out double width, out double height)
        {
            length = 0;
            width = 0;
            height = 0;

            if (string.IsNullOrWhiteSpace(dimensions))
            {
                return false;
            }

            string[] parts = dimensions.Split(new char[] { 'x', 'X' });
            if (parts.Length != 3)
            {
                return false;
            }

            bool canParseLength = double.TryParse(parts[0].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out length);
            bool canParseWidth = double.TryParse(parts[1].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out width);
            bool canParseHeight = double.TryParse(parts[2].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out height);
            return canParseLength && canParseWidth && canParseHeight && length > 0 && width > 0 && height > 0;
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

        public void UpdatePackageInfo(string description, double actualWeight, string dimensions, bool isFragile, decimal value, string itemCategory, string handlingInstructions)
        {
            Description = description;
            ActualWeight = actualWeight;
            Dimensions = dimensions;
            IsFragile = isFragile;
            Value = value;
            ItemCategory = itemCategory;
            HandlingInstructions = handlingInstructions;
            CalculateVolumeWeight();
        }

        public void MarkWaitingPickup()
        {
            if (Status == PackageStatus.Created)
            {
                Status = PackageStatus.WaitingPickup;
            }
        }

        public void MarkPickedUp()
        {
            Status = PackageStatus.PickedUp;
            LastScannedAt = DateTime.Now;
        }

        public void CheckInWarehouse(string warehouseId, string shelfLocation)
        {
            CurrentWarehouseID = warehouseId ?? string.Empty;
            CurrentShelfLocation = shelfLocation ?? string.Empty;
            Status = PackageStatus.InWarehouse;
            LastScannedAt = DateTime.Now;
        }

        public void MarkSorting()
        {
            Status = PackageStatus.Sorting;
            LastScannedAt = DateTime.Now;
        }

        public void CheckOutWarehouse()
        {
            CurrentWarehouseID = string.Empty;
            CurrentShelfLocation = string.Empty;
            Status = PackageStatus.LoadedForDelivery;
            LastScannedAt = DateTime.Now;
        }

        public void MarkInTransit()
        {
            Status = PackageStatus.InTransit;
            LastScannedAt = DateTime.Now;
        }

        public void MarkDelivered()
        {
            Status = PackageStatus.Delivered;
            CurrentWarehouseID = string.Empty;
            CurrentShelfLocation = string.Empty;
            LastScannedAt = DateTime.Now;
        }

        public void MarkFailedDelivery()
        {
            if (Status == PackageStatus.Lost || Status == PackageStatus.Damaged)
            {
                return;
            }

            Status = PackageStatus.FailedDelivery;
            LastScannedAt = DateTime.Now;
        }

        public void MarkReturning()
        {
            Status = PackageStatus.Returning;
            LastScannedAt = DateTime.Now;
        }

        public void MarkReturned()
        {
            Status = PackageStatus.Returned;
            LastScannedAt = DateTime.Now;
        }

        public void MarkLost()
        {
            Status = PackageStatus.Lost;
            LastScannedAt = DateTime.Now;
        }

        public void MarkDamaged()
        {
            Status = PackageStatus.Damaged;
            LastScannedAt = DateTime.Now;
        }

        public bool IsCurrentlyInWarehouse(string warehouseId)
        {
            return Status == PackageStatus.InWarehouse &&
                   string.Equals(CurrentWarehouseID, warehouseId, StringComparison.OrdinalIgnoreCase);
        }

        public void EnsureRuntimeDefaults()
        {
            if (PackageID == null)
            {
                PackageID = string.Empty;
            }

            if (OrderID == null)
            {
                OrderID = string.Empty;
            }

            if (Description == null)
            {
                Description = string.Empty;
            }

            if (Dimensions == null)
            {
                Dimensions = string.Empty;
            }

            if (ItemCategory == null)
            {
                ItemCategory = string.Empty;
            }

            if (HandlingInstructions == null)
            {
                HandlingInstructions = string.Empty;
            }

            if (CurrentWarehouseID == null)
            {
                CurrentWarehouseID = string.Empty;
            }

            if (CurrentShelfLocation == null)
            {
                CurrentShelfLocation = string.Empty;
            }

            CalculateVolumeWeight();
        }

        // Kiem tra hang de vo
        private bool IsFragileCategoryItem()
        {
            string normalizedCategory = NormalizeCategory(ItemCategory);
            if (normalizedCategory == string.Empty)
            {
                return false;
            }

            string[] fragileCategories = new string[] { "glass", "ceramic", "electronics", "wine", "thuy tinh", "gom su", "dien tu", "ruou" };
            for (int i = 0; i < fragileCategories.Length; i++)
            {
                if (normalizedCategory.Contains(fragileCategories[i], StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        private static string NormalizeCategory(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string normalized = value.Trim().Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < normalized.Length; i++)
            {
                char current = normalized[i];
                if (current == 'đ' || current == 'Đ')
                {
                    builder.Append('d');
                    continue;
                }

                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(current);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(char.ToLowerInvariant(current));
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        public bool CheckIfFragile()
        {
            return IsFragile || Value > 5000000 || IsFragileCategoryItem();
        }

        // Lay thong tin goi hang
        public string GetPackageInfo()
        {
            string fragileText = "No";
            if (CheckIfFragile())
            {
                fragileText = "Yes";
            }
            return "[Package] ID: " + PackageID + " | Order: " + OrderID + "\n" +
                   "  Desc: " + Description + " | Weight: " + ActualWeight + "kg | Vol: " + VolumeWeight + "kg\n" +
                   "  Dimensions: " + Dimensions + " | Category: " + ItemCategory + " | Value: " + Value.ToString("N0") + " VND\n" +
                   "  Fragile: " + fragileText + " | Handling: " + HandlingInstructions + "\n" +
                   "  Status: " + Status + " | Warehouse: " + CurrentWarehouseID + " | Shelf: " + CurrentShelfLocation;
        }

        public override string ToString()
        {
            return GetPackageInfo();
        }
    }
}
