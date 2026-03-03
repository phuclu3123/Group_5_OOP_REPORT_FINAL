using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class Warehouse : ITrackable
    {
        public string WarehouseId { get; set; }
        public double Capacity { get; set; }
        public double CurrentLoad { get; set; }

        public List<WarehouseLocation> Locations { get; set; }

        public Warehouse(string warehouseId, double capacity)
        {
            WarehouseId = warehouseId;
            Capacity = capacity;
            CurrentLoad = 0;
            Locations = new List<WarehouseLocation>();
        }
    }
}