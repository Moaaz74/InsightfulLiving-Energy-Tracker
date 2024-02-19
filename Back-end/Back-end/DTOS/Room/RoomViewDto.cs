using Back_end.Models;
using System.Text.Json.Serialization;

namespace Back_end.DTOS.Room
{
    public class RoomViewDto
    {
        public int Id { get; set; }

        
        public int NumberOfDevices { get; set; }

        public int HomeId { get; set; }

        [JsonIgnore]
        public string massage {  get; set; }

        
    }
}
