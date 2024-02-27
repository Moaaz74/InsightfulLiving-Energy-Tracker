using Back_end.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Home> Homes { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Device> Devices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                     .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.IsPasswordChanged)
                .HasDefaultValue(false);

            modelBuilder.Entity<ApplicationUser>()
               .Property(u => u.IsDeleted)
               .HasDefaultValue(false);

            modelBuilder.Entity<UserConnection>()
                .HasKey(u => new { u.UserId, u.ConnectionId });

            base.OnModelCreating(modelBuilder);
        }

    }
}
