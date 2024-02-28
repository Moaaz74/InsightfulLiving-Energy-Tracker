using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back_end.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Home Home { get; set; }

        [Column]
        public bool IsPasswordChanged { get; set; }

        public bool IsDeleted { get; set; }
     
    }

   
}
