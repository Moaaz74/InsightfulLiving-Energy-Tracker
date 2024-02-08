using Back_end.Kafka.EventProcessor.Implementations;
using Back_end.Kafka.EventProcessor.Interfaces;
using Back_end.Kafka.Services;
using Back_end.Models;
using Back_end.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Back_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IEventConsumer, KafkaConsumer>();
            builder.Services.AddHostedService<KafkaConsumerService>();
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
