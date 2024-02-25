using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOs
{
    public class GetUserDto
    {
        [Display(Name = "User Name")]
        public string Username { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
