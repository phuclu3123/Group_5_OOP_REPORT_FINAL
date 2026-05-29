using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;
using Newtonsoft.Json;

namespace Logistics.Core.Models.Actors
{   
    public abstract class Person : ISerializable
    {   
        /*I. Các thuộc tính cơ bản của một người 
        1. Id của căn cước công dân
        2. Họ tên đầy đủ
        3. Số điện thoại
        4. Email
        5. Ngày sinh
        6. Giới tính
        7. Địa chỉ nhà (sử dụng lớp Address đã tạo)
        */
        
        [JsonProperty]
        public string Id { get; protected set; }
        [JsonProperty]
        public string FullName { get; protected set; }
        [JsonProperty]
        public string PhoneNumber { get; protected set; }
        [JsonProperty]
        public string Email { get; protected set; }
        [JsonProperty]
        public DateTime BirthDay { get; protected set; }
        [JsonProperty]
        public Gender Gender { get; protected set; }
        [JsonProperty]
        public Address HomeAddress { get; protected set; }

        // Constructor có tham số để khởi tạo đầy đủ thông tin của một người
        protected Person(string id, string fullName, string phoneNumber, string email, DateTime birthDay, Gender gender, Address homeAddress)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            BirthDay = birthDay;
            Gender = gender;
            HomeAddress = homeAddress;
        }

        // Constructor không tham số cho JSON serialization và các lớp con
        protected Person()
        {
            Id = string.Empty;
            FullName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            HomeAddress = null!;
        }
        
        // Constructor cho ISerializable (Deserialization)
        protected Person(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetString("Id") ?? string.Empty;
            FullName = info.GetString("FullName") ?? string.Empty;
            PhoneNumber = info.GetString("PhoneNumber") ?? string.Empty;
            Email = info.GetString("Email") ?? string.Empty;
            BirthDay = info.GetDateTime("BirthDay");
            Gender = (Gender)info.GetValue("Gender", typeof(Gender))!;
            HomeAddress = (Address)info.GetValue("HomeAddress", typeof(Address))!;
        }

        // Phương thức ISerializable (Serialization)
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("FullName", FullName);
            info.AddValue("PhoneNumber", PhoneNumber);
            info.AddValue("Email", Email);
            info.AddValue("BirthDay", BirthDay);
            info.AddValue("Gender", Gender);
            info.AddValue("HomeAddress", HomeAddress);
        }

        /* ===== COMMON METHODS FOR ALL PERSONS =====
            1. Cập nhật số điện thoại
            2. Cập nhật email
            3. Cập nhật địa chỉ
            4. Tính tuổi
            5. Phương thức hiển thị thông tin cơ bản của một người
            6. Override ToString() để hiển thị thông tin chi tiết của một người
        */

        public void UpdatePhoneNumber(string newPhone)
        {
            PhoneNumber = newPhone;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }

        public void UpdateAddress(Address newAddress)
        {
            HomeAddress = newAddress;
        }

        public int GetAge()
        {
            int age = DateTime.Now.Year - BirthDay.Year;
            if (DateTime.Now.DayOfYear < BirthDay.DayOfYear)
            {
                age--;
            }
            return age;
        }

        public virtual string GetInfo()
        {
            return $"[Person] ID: {Id} | Name: {FullName}\n" +
                   $"  Phone: {PhoneNumber} | Email: {Email}\n" +
                   $"  Home Address: {HomeAddress}\n" +
                   $"  Birthday: {BirthDay:dd/MM/yyyy} | Age: {GetAge()} | Gender: {Gender}";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
