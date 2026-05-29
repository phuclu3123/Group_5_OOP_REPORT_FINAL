﻿using System.Collections.Generic;

namespace Logistics.Core.DataAccess.Interfaces
{
    // Interface chung cho tat ca Repository - ap dung Generic Pattern
    // T la kieu doi tuong bat ky (Customer, Order, Vehicle, ...)
    public interface IRepository<T>
    {
        // Lay tat ca ban ghi
        List<T> GetAll();

        // Lay ban ghi theo ID
        T GetById(string id);

        // Them ban ghi moi
        void Add(T entity);

        // Cap nhat ban ghi
        void Update(T entity);

        // Xoa ban ghi theo ID
        void Delete(string id);

        // Luu thay doi xuong file JSON
        void SaveChanges();

        // Tai lai du lieu tu file JSON
        void LoadData();
    }
}

