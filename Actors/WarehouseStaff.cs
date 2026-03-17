namespace Cuoi_ky_OOP.Models.Actors
{
    public class WarehouseStaff
    {
        public string StaffID { get; set; }
        public string FullName { get; set; }

        public WarehouseStaff(string staffId, string fullname)
        {
            StaffID = staffId;
            FullName = fullname;
        }
    }
}