using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_end.Models
{
    public class ApplicationUser : IdentityUser
    {
            public int Id { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Range(20, 60)]
            public int Age { get; set; }

            [Required]
            [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Name Must be only characters")]
            [Display(Name = "User Name")]
            public string Name { get; set; }

            public Gender Gender { get; set; }

            public bool IsActive { get; set; }

            [ForeignKey("HomeId")]
            public int HomeId { get; set; }


        }

        public enum Gender
        { Male = 0, Femal = 1 }
    }
