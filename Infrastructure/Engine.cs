using System;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class Engine
    {
        public string EngineID { get; private set; }
        public string Manufacturer { get; private set; }
        public double HorsePower { get; private set; }

        public Engine(string engineId, string manufacturer, double horsePower)
        {
            EngineID = engineId;
            Manufacturer = manufacturer;
            HorsePower = horsePower;
        }

        // For XML serialization
        public Engine()
        {
            EngineID = null!;
            Manufacturer = null!;
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
