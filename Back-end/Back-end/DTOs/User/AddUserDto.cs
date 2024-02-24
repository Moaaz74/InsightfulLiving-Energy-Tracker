using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public class AddUserDto
    {
        [Required(ErrorMessage = "Please Enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter valid Email Address")]
        public string Email { get; set; }

        public string PasswordHash { get; set; } = "Moa@@z_1234";

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Phone(ErrorMessage = "Please Enter valid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}