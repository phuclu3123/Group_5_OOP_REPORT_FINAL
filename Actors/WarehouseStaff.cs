using System;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public class WarehouseStaff : Staff
    {
        public string WarehouseID { get; private set; }
        public string Shift { get; set; }

        public WarehouseStaff(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId,
                              string staffId, string department, decimal baseSalary, DateTime joinDate,
                              string warehouseId, string shift)
            : base(id, fullName, phoneNumber, email, address, birthDay, gender, accountId, staffId, Role.WarehouseManager, department, baseSalary, joinDate)
        {
            WarehouseID = warehouseId;
            Shift = shift;
        }
    }
}
