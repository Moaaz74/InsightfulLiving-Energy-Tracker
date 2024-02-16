using Back_end.Kafka.EventProcessor.Interfaces;
using Confluent.Kafka;

namespace Back_end.Kafka.EventProcessor.Implementations
{
    public class KafkaConsumer : IEventConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IConfiguration _configuration;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _consumer = KafkaUtils.CreateConsumer(_configuration["KafkaConfig:Broker"], new List<string> { _configuration["KafkaConfig:Topic"] });
        }

        public string ReadMessage()
        {
            var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(0.5));
            if (consumeResult == null || string.IsNullOrWhiteSpace(consumeResult.Value))
            {
                return string.Empty;
            }
            else
            {
                return consumeResult.Value;
            }
        }
    }
}
