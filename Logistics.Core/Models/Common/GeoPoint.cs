﻿using System;

namespace Logistics.Core.Models.Common
{
    public struct GeoPoint
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public GeoPoint(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
}
