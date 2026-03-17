using System;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class VehicleAssignment
    {
        public string AssignmentID { get; private set; }  
        public string DriverID { get; private set; }
        public string VehicleID { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; set; }
        public double StartOdometer { get; private set; }

        public VehicleAssignment(string assignmentId, string driverId, string vehicleId, DateTime startTime, double startOdometer)
        {
            AssignmentID = assignmentId;
            DriverID = driverId;
            VehicleID = vehicleId;
            StartTime = startTime;
            StartOdometer = startOdometer;
        }

        public void CompleteAssignment(DateTime endTime)
        {
            EndTime = endTime;
        }
    }
}
