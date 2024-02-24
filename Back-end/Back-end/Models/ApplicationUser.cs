using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Back_end.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Home> homes { get; set; } = new List<Home>();

        [Column]
        public bool IsPasswordChanged { get; set; }

        public bool IsDeleted { get; set; }
     
    }

   
}
