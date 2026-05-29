using System;

namespace Logistics.Core.Services.Interfaces
{
    public interface INotificationService
    {
        event Action<string, string> OnNotificationReceived; // (Title, Message)
        void Notify(string title, string message);
    }
}
