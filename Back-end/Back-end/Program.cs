using Back_end.DAOs.Implementations;
using Back_end.DAOs.Interfaces;
using Back_end.Kafka.EventProcessor.Implementations;
using Back_end.Kafka.EventProcessor.Interfaces;
using Back_end.Kafka.Services;
using Back_end.Models;
using Back_end.Repositories;
using Back_end.Repositories.Implementations;
using Back_end.Repositories.Interfaces;
using Back_end.Services.DeviceService;
using Back_end.Services.HomeService;
using Back_end.Services.RoomService;
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

            builder.Services.AddSingleton<ICassandraDAO>(provider =>
            {
                var contactPoint = builder.Configuration["CassandraConfiguration:cassandraNodes"];
                var keyspace = builder.Configuration["CassandraConfiguration:Keyspace"];
                return new CassandraDAO(contactPoint , keyspace);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
            });

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IEventConsumer, KafkaConsumer>();
            builder.Services.AddHostedService<KafkaConsumerService>();
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IDeviceService,DeviceService>();
            var app = builder.Build();

            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
