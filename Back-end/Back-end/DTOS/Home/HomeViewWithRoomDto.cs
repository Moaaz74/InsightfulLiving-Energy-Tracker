using Back_end.Models;
using System.Collections.Generic;
using Back_end.DTOS.Room;
namespace Back_end.DTOS.Home
{
    public class HomeViewWithRoomDto
    {
        public int Id { get; set; }
        public int NumberOfRooms { get; set; }
        public HomeUserDto User { get; set; }
        public List<DTOS.Room.RoomHomeViewDto> Rooms { get; set; }  //= new List<RoomViewDto> { new RoomViewDto() };
    }
}
