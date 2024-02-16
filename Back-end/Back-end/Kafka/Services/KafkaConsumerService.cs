
using Back_end.Kafka.EventProcessor.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Back_end.Kafka.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly ILogger<KafkaConsumerService> _logger;
        //private readonly IHubContext<KafkaHub, IKafkaHub> _hubContext;
        //private IUserConnectionsService _userConnectionsService;
        //private IUserDataService _userDataService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumerService(IEventConsumer consumer, ILogger<KafkaConsumerService> logger, /*IHubContext<KafkaHub, IKafkaHub> hubContext*/ /*, IUserConnectionsService userConnectionsService, IUserDataService userDataService,*/ IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _consumer = consumer;
            //_hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
            //_userConnectionsService = userConnectionsService;
            //_userDataService = userDataService;
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


                    // Deserialize

                    
                    // Send to Hub

                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }
    }
}
