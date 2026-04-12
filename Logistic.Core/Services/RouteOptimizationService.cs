using System;
using System.Collections.Generic;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu toi uu tuyen duong (Route Optimization Service)
    // Tinh khoang cach giua cac diem bang cong thuc Haversine,
    // uoc luong thoi gian giao hang, va sap xep tuyen duong toi uu.
    // ============================================================
    public class RouteOptimizationService
    {
        // ===== HANG SO =====

        // Ban kinh Trai Dat (km) - dung cho cong thuc Haversine
        private const double EARTH_RADIUS_KM = 6371.0;

        // Toc do di chuyen trung binh (km/h) - dung de uoc luong thoi gian
        private const double AVERAGE_SPEED_KMH = 40.0;

        // Thoi gian dung chan trung binh moi diem (phut)
        private const double STOP_TIME_MINUTES = 10.0;

        // ===== METHODS =====

        // Tinh khoang cach giua 2 diem tren ban do bang cong thuc Haversine
        // Cong thuc nay tinh khoang cach duong tron lon tren mat cau
        public double CalculateDistance(GeoPoint point1, GeoPoint point2)
        {
            // Chuyen do sang radian
            double lat1Rad = DegreesToRadians(point1.Latitude);
            double lat2Rad = DegreesToRadians(point2.Latitude);
            double deltaLat = DegreesToRadians(point2.Latitude - point1.Latitude);
            double deltaLon = DegreesToRadians(point2.Longitude - point1.Longitude);

            // Cong thuc Haversine
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EARTH_RADIUS_KM * c;
            return Math.Round(distance, 2);
        }

        // Uoc luong thoi gian giao hang (phut)
        // Tinh toan: thoi gian di chuyen + thoi gian dung chan
        public double EstimateDeliveryTime(GeoPoint origin, GeoPoint destination)
        {
            double distance = CalculateDistance(origin, destination);

            // Thoi gian di chuyen (phut)
            double travelTimeMinutes = (distance / AVERAGE_SPEED_KMH) * 60;

            // Tong thoi gian = di chuyen + dung chan
            double totalMinutes = travelTimeMinutes + STOP_TIME_MINUTES;
            return Math.Round(totalMinutes, 1);
        }

        // Sap xep tuyen duong toi uu (Nearest Neighbor Algorithm)
        // Bat dau tu diem xuat phat, luon chon diem gan nhat chua di qua
        public List<GeoPoint> OptimizeRoute(GeoPoint startPoint, List<GeoPoint> deliveryPoints)
        {
            List<GeoPoint> optimizedRoute = new List<GeoPoint>();
            List<GeoPoint> remainingPoints = new List<GeoPoint>();

            // Copy danh sach diem giao hang
            for (int i = 0; i < deliveryPoints.Count; i++)
            {
                remainingPoints.Add(deliveryPoints[i]);
            }

            GeoPoint currentPoint = startPoint;
            optimizedRoute.Add(currentPoint);

            // Lap cho den khi het diem can giao
            while (remainingPoints.Count > 0)
            {
                // Tim diem gan nhat voi vi tri hien tai
                int nearestIndex = 0;
                double nearestDistance = CalculateDistance(currentPoint, remainingPoints[0]);

                for (int i = 1; i < remainingPoints.Count; i++)
                {
                    double distance = CalculateDistance(currentPoint, remainingPoints[i]);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestIndex = i;
                    }
                }

                // Di chuyen den diem gan nhat
                currentPoint = remainingPoints[nearestIndex];
                optimizedRoute.Add(currentPoint);
                remainingPoints.RemoveAt(nearestIndex);
            }

            return optimizedRoute;
        }

        // Tinh tong khoang cach cua toan bo tuyen duong
        public double CalculateTotalRouteDistance(List<GeoPoint> route)
        {
            double totalDistance = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                totalDistance += CalculateDistance(route[i], route[i + 1]);
            }
            return Math.Round(totalDistance, 2);
        }

        // Uoc luong tong thoi gian cho toan bo tuyen duong (phut)
        public double EstimateTotalRouteTime(List<GeoPoint> route)
        {
            double totalDistance = CalculateTotalRouteDistance(route);
            // Thoi gian di chuyen + thoi gian dung chan tai moi diem
            double travelTime = (totalDistance / AVERAGE_SPEED_KMH) * 60;
            double stopTime = (route.Count - 1) * STOP_TIME_MINUTES;
            return Math.Round(travelTime + stopTime, 1);
        }

        // Tao bao cao tuyen duong chi tiet
        public string GenerateRouteReport(GeoPoint startPoint, List<GeoPoint> deliveryPoints)
        {
            List<GeoPoint> optimizedRoute = OptimizeRoute(startPoint, deliveryPoints);
            double totalDistance = CalculateTotalRouteDistance(optimizedRoute);
            double totalTime = EstimateTotalRouteTime(optimizedRoute);

            string report = "========== BAO CAO TUYEN DUONG ==========\n";
            report += "  Diem xuat phat: " + startPoint.ToString() + "\n";
            report += "  So diem giao: " + deliveryPoints.Count + "\n";
            report += "  Tong khoang cach: " + totalDistance + " km\n";
            report += "  Uoc luong thoi gian: " + totalTime + " phut\n";
            report += "  Chi tiet tuyen duong:\n";

            for (int i = 0; i < optimizedRoute.Count; i++)
            {
                report += "    [" + (i + 1) + "] " + optimizedRoute[i].ToString();
                if (i < optimizedRoute.Count - 1)
                {
                    double segmentDistance = CalculateDistance(optimizedRoute[i], optimizedRoute[i + 1]);
                    report += " -> " + segmentDistance + " km";
                }
                report += "\n";
            }
            report += "==========================================";
            return report;
        }

        // ===== PRIVATE HELPERS =====

        // Chuyen doi tu do sang radian
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
