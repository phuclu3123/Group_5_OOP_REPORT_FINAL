using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cuoi_ky_OOP.Models.Business
{
    public class Package
    {
        private string PackageID { get; set; }
        private string OrderID { get; set; }
        private string Description { get; set; }
        private double ActualWeight { get; set; }
        private string Dimensions { get; set; }
        private double VolumeWeight { get; set; }
        private bool IsFragile { get; set; }
        public Package() {}
    }
}