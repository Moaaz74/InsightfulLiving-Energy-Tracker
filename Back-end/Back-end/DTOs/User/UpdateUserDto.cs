namespace Back_end.DTOs
{
    public class UpdateUserDto
    {
        public string UserId { get;}
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

    } 
}