﻿namespace Logistics.Core.Models.Interfaces
{
    // Interface theo doi - ap dung cho cac doi tuong co the track trang thai va vi tri
    public interface ITrackable
    {
        // Lay trang thai hien tai
        string GetCurrentStatus();

        // Lay thong tin theo doi chi tiet
        string GetTrackingInfo();
    }
}
