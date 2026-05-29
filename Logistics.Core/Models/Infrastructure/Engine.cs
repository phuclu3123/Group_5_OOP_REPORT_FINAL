using System;

namespace Logistics.Core.Models.Infrastructure
{
    /// <summary>
    /// Thong tin dong co gan voi phuong tien.
    /// Engine duoc tach rieng de minh hoa quan he aggregation voi Vehicle.
    /// </summary>
    public class Engine
    {
        public string EngineID { get; set; }
        public string Manufacturer { get; set; }
        public double HorsePower { get; set; }

        public Engine(string engineId, string manufacturer, double horsePower)
        {
            EngineID = engineId;
            Manufacturer = manufacturer;
            HorsePower = horsePower;
        }

        // Constructor khong tham so bat buoc cho Newtonsoft.Json.
        public Engine() 
        { 
            EngineID = string.Empty;
            Manufacturer = string.Empty;
        }

        public string GetEngineInfo()
        {
            return "[Engine] ID: " + EngineID + " | Manufacturer: " + Manufacturer + " | HP: " + HorsePower;
        }

        public override string ToString()
        {
            return GetEngineInfo();
        }
    }
}

