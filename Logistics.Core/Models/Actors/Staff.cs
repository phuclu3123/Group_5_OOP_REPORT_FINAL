﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;

namespace Logistics.Core.Models.Actors
{
    public abstract class Staff : Person, ISalaryCalculable
    {   
        /*
        I. CÃ¡c thuá»™c tÃ­nh cÆ¡ báº£n cá»§a má»™t nhÃ¢n viÃªn
        1. StaffID (mÃ£ nhÃ¢n viÃªn)
        2. Role (vai trÃ²: Admin, Driver, WarehouseStaff, dispatcher, administrativeStaff)
        3. Department (phÃ²ng ban)   
        4. WorkStatus (tráº¡ng thÃ¡i lÃ m viá»‡c: Active, OnLeave, Resigned)
        5. BaseSalary (lÆ°Æ¡ng cÆ¡ báº£n)
        6. JoinDate (ngÃ y vÃ o lÃ m)
        7. AccountID (mÃ£ tÃ i khoáº£n liÃªn káº¿t vá»›i nhÃ¢n viÃªn)
        */
        [JsonProperty]
        public string StaffID { get; protected set; }
        [JsonProperty]
        public Role Role { get; protected set; }
        [JsonProperty]
        public string Department { get; protected set; }
        [JsonProperty]
        public WorkStatus WorkStatus { get; protected set; }
        [JsonProperty]
        public decimal BaseSalary { get; protected set; }
        [JsonProperty]
        public DateTime JoinDate { get; protected set; }
        [JsonProperty] 
        public string AccountID { get; protected set; }

        // 1. Constructor cÃ³ tham sá»‘ Ä‘á»ƒ khá»Ÿi táº¡o Ä‘áº§y Ä‘á»§ thÃ´ng tin cá»§a má»™t nhÃ¢n viÃªn
        protected Staff(string id, string fullName, string phoneNumber, string email, Address homeAddress, DateTime birthDay, Gender gender, string accountId,
                        string staffId, Role role, string department, decimal baseSalary, DateTime joinDate)
            : base(id, fullName, phoneNumber, email, birthDay, gender, homeAddress) 
        {
            AccountID = accountId; // 2. Khá»Ÿi táº¡o AccountID trong constructor
            StaffID = staffId;
            Role = role;
            Department = department;
            WorkStatus = WorkStatus.Active;
            BaseSalary = baseSalary;
            JoinDate = joinDate;
        }

        // 2. Constructor khÃ´ng tham sá»‘ cho JSON serialization vÃ  cÃ¡c lá»›p con
        protected Staff() : base()
        {
            StaffID = string.Empty;
            Department = string.Empty;
            AccountID = string.Empty;
        }

        // Constructor cho ISerializable (Deserialization)
        protected Staff(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StaffID = info.GetString("StaffID") ?? string.Empty;
            AccountID = info.GetString("AccountID") ?? string.Empty; // 3. ThÃƒÂªm Ã„â€˜Ã¡Â»Âc AccountID
            Role = (Role)info.GetValue("Role", typeof(Role))!;
            Department = info.GetString("Department") ?? string.Empty;
            WorkStatus = (WorkStatus)info.GetValue("WorkStatus", typeof(WorkStatus))!;
            BaseSalary = info.GetDecimal("BaseSalary");
            JoinDate = info.GetDateTime("JoinDate");
        }

        // PhÆ°Æ¡ng thá»©c ISerializable (Serialization)
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context); // GÃ¡Â»Âi base Ã„â€˜Ã¡Â»Æ’ lÃ†Â°u cÃƒÂ¡c thuÃ¡Â»â„¢c tÃƒÂ­nh cÃ¡Â»Â§a Person
            info.AddValue("StaffID", StaffID);
            info.AddValue("AccountID", AccountID); // 3. ThÃƒÂªm lÃ†Â°u AccountID
            info.AddValue("Role", Role);
            info.AddValue("Department", Department);
            info.AddValue("WorkStatus", WorkStatus);
            info.AddValue("BaseSalary", BaseSalary);
            info.AddValue("JoinDate", JoinDate);
        }

        // ===== ABSTRACT METHODS (Polymorphism) =====

        // Má»—i loáº¡i nhÃ¢n viÃªn sáº½ cÃ³ cÃ¡ch tÃ­nh lÆ°Æ¡ng khÃ¡c nhau, nÃªn Ä‘á»ƒ phÆ°Æ¡ng thá»©c nÃ y lÃ  abstract Ä‘á»ƒ cÃ¡c lá»›p con pháº£i override
        public abstract decimal CalculateSalary();

        // PhÆ°Æ¡ng thá»©c nÃ y sáº½ tráº£ vá» chi tiáº¿t cÃ¡ch tÃ­nh lÆ°Æ¡ng, cÅ©ng Ä‘á»ƒ abstract Ä‘á»ƒ cÃ¡c lá»›p con implement theo cÃ¡ch riÃªng cá»§a mÃ¬nh
        public abstract string GetSalaryBreakdown();

        /* ===== COMMON METHODS =====
            1. Cáº­p nháº­t tráº¡ng thÃ¡i lÃ m viá»‡c (Active, OnLeave, Resigned)
            2. Cáº­p nháº­t lÆ°Æ¡ng cÆ¡ báº£n
            3. Cáº­p nháº­t phÃ²ng ban
            4. PhÆ°Æ¡ng thá»©c tá»« chá»©c (Resign)
            5. PhÆ°Æ¡ng thá»©c xin nghá»‰ phÃ©p (TakeLeave)
            6. PhÆ°Æ¡ng thá»©c trá»Ÿ láº¡i lÃ m viá»‡c (ReturnToWork)
            7. TÃ­nh sá»‘ nÄƒm cÃ´ng tÃ¡c
            8. Kiá»ƒm tra xem nhÃ¢n viÃªn cÃ³ Ä‘ang hoáº¡t Ä‘á»™ng hay khÃ´ng
        */
        public void UpdateWorkStatus(WorkStatus newStatus)
        {
            WorkStatus = newStatus;
        }

        public void UpdateBaseSalary(decimal newSalary)
        {
            BaseSalary = newSalary;
        }

        public void UpdateDepartment(string newDepartment)
        {
            Department = newDepartment;
        }

        public void UpdateAccountId(string accountId)
        {
            AccountID = accountId;
        }

        public void Resign()
        {
            WorkStatus = WorkStatus.Resigned;
        }

        public void TakeLeave()
        {
            WorkStatus = WorkStatus.OnLeave;
        }

        public void ReturnToWork()
        {
            WorkStatus = WorkStatus.Active;
        }

        public int GetYearsOfService()
        {
            int years = DateTime.Now.Year - JoinDate.Year;
            if (DateTime.Now.DayOfYear < JoinDate.DayOfYear)
            {
                years--;
            }
            return years;
        }

        public bool IsActive()
        {
            return WorkStatus == WorkStatus.Active;
        }

        // 4. PhÆ°Æ¡ng thá»©c hiá»ƒn thá»‹ thÃ´ng tin chi tiáº¿t cá»§a má»™t nhÃ¢n viÃªn, override tá»« Person Ä‘á»ƒ thÃªm thÃ´ng tin vá» Staff
        public override string GetInfo()
        {
            // Gá»i base.GetInfo() Ä‘á»ƒ láº¥y thÃ´ng tin cÆ¡ báº£n cá»§a má»™t ngÆ°á»i, sau Ä‘Ã³ thÃªm thÃ´ng tin vá» nhÃ¢n viÃªn
            return base.GetInfo() + "\n" +
                   $"[Staff Info] StaffID: {StaffID} | AccountID: {AccountID}\n" +
                   $"  Role: {Role} | Department: {Department} | Status: {WorkStatus}\n" +
                   $"  Join Date: {JoinDate:dd/MM/yyyy} | Years of Service: {GetYearsOfService()}\n" +
                   $"  Base Salary: {BaseSalary:N0} VND";
        }

        // Override ToString() Ä‘á»ƒ hiá»ƒn thá»‹ thÃ´ng tin chi tiáº¿t cá»§a má»™t nhÃ¢n viÃªn
        public override string ToString()
        {
            return GetInfo();
        }
    }
}
