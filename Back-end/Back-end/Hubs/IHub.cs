namespace Back_end.Hubs
{
    public interface IHub
    {
        Task SendMessage(string message);
    }
}
