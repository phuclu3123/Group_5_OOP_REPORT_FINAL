﻿using System;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Common
{
    public class Address
    {
        public string? StreetAndNumber { get; set; }     // sá»‘ nhÃ , tÃªn Ä‘Æ°á»ng
        public string? Ward { get; set; }                // phÆ°á»ng, xÃ£
        public string? District {get; set;}              // quáº­n, huyá»‡n
        public string? City { get; set; }                // ThÃ nh phá»‘
        public string? ZipCode { get; set; }             // MÃ£ bÆ°u Ä‘iá»‡n
        public string? Country { get; set; }             // Quá»‘c gia 

        // Tá»a Ä‘á»™ Ä‘á»‹a lÃ½ (latitude, longitude) - tÃ¹y chá»n Ä‘á»ƒ há»— trá»£ cÃ¡c tÃ­nh nÄƒng Ä‘á»‹nh vá»‹, báº£n Ä‘á»“ vÃ  tá»‘i Æ°u hÃ³a lá»™ trÃ¬nh
        public GeoPoint? Location { get; set; }

        // Constructor Ä‘áº§y Ä‘á»§ tham sá»‘
        public Address(string streetAndNumber, string ward, string district, string city, string zipCode, string country, GeoPoint? location = null)
        {
            StreetAndNumber = streetAndNumber;
            Ward = ward;
            District = district;
            City = city;
            ZipCode = zipCode;
            Country = country;
            Location = location;
        }

        // Constructor khong tham so cho JSON serialization
        protected Address() { }
        
        // PhÆ°Æ¡ng thá»©c ToString Ä‘á»ƒ hiá»ƒn thá»‹ Ä‘á»‹a chá»‰ dÆ°á»›i dáº¡ng chuá»—i
        public override string ToString()
        {
            return $"{StreetAndNumber}, {Ward}, {District}, {City}, {Country} (Zip: {ZipCode})".TrimEnd(',', ' ');
        }
    }
}
