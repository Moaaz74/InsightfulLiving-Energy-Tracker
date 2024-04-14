using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public class UpdateUserDto
    {

        [Required(ErrorMessage = "Please Enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Phone(ErrorMessage = "Please Enter valid Phone Number")]
        public string PhoneNumber { get; set; }

        public bool isPasswordChanged { get; set; }

    } 
}