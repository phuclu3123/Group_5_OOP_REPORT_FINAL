using Logistics.Core.Services.Interfaces;
using System;

namespace Logistics.Core.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        public event Action<string, string>? OnNotificationReceived;

        public void Notify(string title, string message)
        {
            OnNotificationReceived?.Invoke(title, message);
        }
    }
}
