using Confluent.Kafka;

namespace Back_end.Kafka.EventProcessor.Implementations
{
    public class KafkaUtils
    {
        public static IConsumer<string, string> CreateConsumer(string brokerList, List<string> topics)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                GroupId = "Sample-Consumer",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Latest
            };
            var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topics);
            return consumer;
        }
    }
}
