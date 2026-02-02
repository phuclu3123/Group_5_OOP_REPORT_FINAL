using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class WarehouseLocation
    {
        public string LocationCode { get; set; }
        public bool IsOccupied { get; set; }

        public WarehouseLocation(string locationCode)
        {
            LocationCode = locationCode;
            IsOccupied = false;
        }
    }
}