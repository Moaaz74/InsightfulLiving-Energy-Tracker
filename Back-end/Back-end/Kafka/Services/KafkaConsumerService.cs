
using Back_end.Hubs;
using Back_end.Kafka.EventProcessor.Interfaces;
using Back_end.Models;
using Back_end.Repositories;
using Back_end.Services;
using Microsoft.AspNetCore.SignalR;

namespace Back_end.Kafka.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IHubContext<MainDashboardHub, IHub> _hubContext;
        private IUserConnectionService _userConnectionService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumerService(IEventConsumer consumer, ILogger<KafkaConsumerService> logger, IHubContext<MainDashboardHub, IHub> hubContext, IUserConnectionService userConnectionsService, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _consumer = consumer;
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
            _userConnectionService = userConnectionsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer Service Started");


            while (!stoppingToken.IsCancellationRequested)
            {
                string message = _consumer.ReadMessage();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    // Log
                    _logger.LogInformation($"Info: New Message : {message}");

                    // Send to Hub

                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }

        private async Task SendMessages(string message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                _userConnectionService = scope.ServiceProvider.GetRequiredService<UserConnectionService>();
                //List<UserConnection> userConnections = (List<UserConnection>)_userConnectionService.GetAll();

                //if (userConnections is not null && userConnections.Count > 0)
                //{
                //    List<string> connections = new List<string>();
                //    foreach (UserConnection userConnection in userConnections)
                //        connections.Add(userConnection.ConnectionId);

                //    await _hubContext.Clients.Clients(connections).SendMessage(Use, orderRequest);
                //}
                return;
                
            }
        }
    }
}
