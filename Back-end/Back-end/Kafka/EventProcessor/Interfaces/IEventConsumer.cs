namespace Back_end.Kafka.EventProcessor.Interfaces
{
    public interface IEventConsumer
    {
        string ReadMessage();
    }
}
