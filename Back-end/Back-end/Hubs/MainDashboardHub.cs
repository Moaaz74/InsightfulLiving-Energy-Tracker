using Back_end.Models;
using Back_end.Services;
using Back_end.Services.HomeService;
using Back_end.Services.RoomService;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Back_end.Hubs
{
    public class MainDashboardHub : Hub<IHub>
    {
        private readonly IUserConnectionService _userConnectionsService;
        private readonly IHomeService _homeService;
        private readonly IRoomService _roomService;
        // Cassandra Home Service

        public MainDashboardHub(IUserConnectionService userConnectionService , IHomeService homeService , IRoomService roomService)
        {
            _userConnectionsService = userConnectionService;
            _homeService =  homeService;
            _roomService = roomService;
        }

        public override Task OnConnectedAsync()
        {
            string UserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _userConnectionsService.Add(new UserConnection() { UserId = UserId, ConnectionId = Context.ConnectionId });
            List<int> RoomIDs = new List<int>();
            int HomeId = _homeService.GetHomeByUserId(UserId).Id;
            
            foreach(Room room in _roomService.GetRoomsByHomeId(HomeId))
            {
                if (room.Type == "LivingRoom")
                    RoomIDs.Add(room.Id);
            }


            // Call the method which retrieves the last raw added in the table (Homes , Rooms) Energy & (Temp&Humidity of Living Room)

            List<UserConnection> userConnections = (List<UserConnection>)_userConnectionsService.GetAll(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            List<string> connections = new List<string>();
            foreach (var connection in userConnections)
                connections.Add(connection.ConnectionId);

            //Clients.Clients(connections).SendMessage(JsonSerializer.Serialize(data)); //data will be retrieved by cassandra 

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {

            _userConnectionsService.Delete(new UserConnection() { ConnectionId = Context.ConnectionId  , UserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value });
            return base.OnDisconnectedAsync(exception);
        }

    }
}
