using Back_end.DTOS.Room;

namespace Back_end.DTOS.Device
{
    public class DevicesWithRoomDto
    {
        public int Id { get; set; }
        public string EnergyType { get; set; } = string.Empty;
        
        public RoomViewDto room { get; set; }

    }
}
