using System;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Actors
{
    public abstract class Person
    {
        public string Id { get; protected set; }
        public string FullName { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Email { get; protected set; }
        public string Address { get; protected set; }
        public DateTime BirthDay { get; protected set; }
        public Gender Gender { get; protected set; }
        public string AccountID { get; protected set; }

        protected Person(string id, string fullName, string phoneNumber, string email, string address, DateTime birthDay, Gender gender, string accountId)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            BirthDay = birthDay;
            Gender = gender;
            AccountID = accountId;
        }

        // Constructor khong tham so cho XML serialization
        protected Person() { }

        // Cap nhat so dien thoai
        public void UpdatePhoneNumber(string newPhone)
        {
            PhoneNumber = newPhone;
        }

        // Cap nhat email
        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }

        // Cap nhat dia chi
        public void UpdateAddress(string newAddress)
        {
            Address = newAddress;
        }

        // Tinh tuoi
        public int GetAge()
        {
            int age = DateTime.Now.Year - BirthDay.Year;
            if (DateTime.Now.DayOfYear < BirthDay.DayOfYear)
            {
                age--;
            }
            return age;
        }

        // Lay thong tin co ban
        public virtual string GetInfo()
        {
            return "[Person] ID: " + Id + " | Name: " + FullName + "\n" +
                   "  Phone: " + PhoneNumber + " | Email: " + Email + "\n" +
                   "  Address: " + Address + "\n" +
                   "  Birthday: " + BirthDay.ToString("dd/MM/yyyy") + " | Age: " + GetAge() + " | Gender: " + Gender;
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}
