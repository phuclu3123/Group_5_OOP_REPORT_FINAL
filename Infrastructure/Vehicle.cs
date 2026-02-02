using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public abstract class Vehicle
    {
        public string VehicleId { get; set; }
        public string LicensePlate { get; set; }

        public double MaxLoad { get; set; }        // maxLoad
        public double CargoVolume { get; set; }    // cargoVolume
        public double CurrentOdometer { get; set; } // km đã chạy

        protected Vehicle(
            string vehicleId,
            string licensePlate,
            double maxLoad,
            double cargoVolume,
            double currentOdometer)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            MaxLoad = maxLoad;
            CargoVolume = cargoVolume;
            CurrentOdometer = currentOdometer;
        }

        public bool IsMaintenanceNeeded()
        {
            return false;
        }

        public abstract double CalculateShippingCost(double distance);
    }
}