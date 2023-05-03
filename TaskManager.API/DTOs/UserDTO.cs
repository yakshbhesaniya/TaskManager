namespace TaskManager.API.DTOs
{
    public class UserDTO
    {
        public string UserId { get; set; } //Getting id .
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
