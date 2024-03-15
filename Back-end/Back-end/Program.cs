using Back_end.DAOs.Implementations;
using Back_end.DAOs.Interfaces;
using Back_end.Models;
using Back_end.Repositories;
using Back_end.Repositories.Implementations;
using Back_end.Repositories.Interfaces;
using Back_end.Services;
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
            var configuration = builder.Configuration;
            // Add services to the container.
            Console.WriteLine($"Kafka Broker: {configuration["KafkaConfig:Broker"]}");
            Console.WriteLine($"Kafka Topic: {configuration["KafkaConfig:Topic"]}");
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

            builder.Services.AddScoped<IHome_OverallDAO, Home_OverallDAO>();
            builder.Services.AddScoped<IRoom_OverallDAO, Room_OverallDAO>();
            builder.Services.AddScoped<IApplianceDAO, ApplianceDAO>();
            builder.Services.AddScoped<ITemp_HumidityDAO, Temp_HumidityDAO>();
            builder.Services.AddScoped<IJwtService , JwtService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IDeviceService,DeviceService>();

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
