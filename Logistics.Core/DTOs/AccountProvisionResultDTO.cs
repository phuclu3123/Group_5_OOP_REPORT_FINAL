namespace Logistics.Core.DTOs
{
    public class AccountProvisionResultDTO
    {
        public bool Success { get; set; }
        public string Username { get; set; } = string.Empty;
        public string TemporaryPassword { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public UserDTO? Account { get; set; }
    }
}
