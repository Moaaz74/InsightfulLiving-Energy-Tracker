using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Back_end.DTOS.Device
{
    public class DeviceViewDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        public string EnergyType { get; set; } = string.Empty;


        [JsonIgnore]
        public string massage { get; set; }

        [JsonIgnore]
        public string massageBadRequst { get; set; }


    }
}
