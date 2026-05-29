using System;

namespace Logistics.Core.Models.Account
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        
        // Security Q&A for password reset
        public string SecurityQuestion { get; set; } = string.Empty;
        public string SecurityAnswerHash { get; set; } = string.Empty;
        
        public Actors.Person? Person { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool MustChangePassword { get; set; } = false;
        public string AvatarPath { get; set; } = string.Empty;
    }
}
