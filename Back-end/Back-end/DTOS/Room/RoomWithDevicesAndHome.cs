using Back_end.DTOS.Device;
using Back_end.DTOS.Home;
using Back_end.Models;

namespace Back_end.DTOS.Room
{
    public class RoomWithDevicesAndHome
    {
        public int Id { get; set; }


        public HomeViewDto home { get; set; } = default!;

        public int NumberOfDevices { get; set; }

        public List<DeviceViewDto> devices { get; set; }

       
    }
}
