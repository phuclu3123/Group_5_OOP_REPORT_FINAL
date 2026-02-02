using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class Equipment
    {
        public string EquipmentID { get; set; }
        public string Type { get; set; }
        public string WarehouseID { get; set; }
        public string Status { get; set; }

        public Equipment(
            string equipmentID,
            string type,
            string warehouseID,
            string status)
        {
            EquipmentID = equipmentID;
            Type = type;
            WarehouseID = warehouseID;
            Status = status;
        }
    }
}