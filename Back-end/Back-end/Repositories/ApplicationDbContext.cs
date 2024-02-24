using Back_end.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.IsPasswordChanged)
                .HasDefaultValue(false);

            modelBuilder.Entity<ApplicationUser>()
               .Property(u => u.IsDeleted)
               .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
