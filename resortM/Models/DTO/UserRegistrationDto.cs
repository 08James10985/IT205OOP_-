namespace resortM.Models.DTO
{
    public class UserRegistrationDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Platform { get; set; }
        public string AdminRegistrationKey { get; set; } // Add this line
    }

}
