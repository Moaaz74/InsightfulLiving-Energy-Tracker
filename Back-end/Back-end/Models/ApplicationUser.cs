using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Back_end.Models
{
    public class ApplicationUser : IdentityUser
    {
        private readonly ILazyLoader _lazyLoader;

        public ApplicationUser()
        {
        }

        public ApplicationUser(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private Home _Home;

        public Home Home
        {
            get => _lazyLoader.Load(this, ref _Home);
            set => _Home = value;
        }

        public bool IsPasswordChanged { get; set; }

        public bool IsDeleted { get; set; }
     
    }

   
}
