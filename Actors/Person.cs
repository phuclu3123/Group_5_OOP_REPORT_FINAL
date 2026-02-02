using System;
using Cuoi_ky_OOP.Models.Enums;

namespace Cuoi_ky_OOP.Models.Actors
{
    public abstract class Person
    {
        // Properties with protected setters to respect encapsulation while allowing read access
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
    }
}
