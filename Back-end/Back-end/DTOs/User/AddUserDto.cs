namespace Back_end.DTOs
{
    public class AddUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = "12345678";
        public string PhoneNumber { get; set; }
    }
}