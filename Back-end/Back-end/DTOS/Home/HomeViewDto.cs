using Back_end.Models;
using System.Text.Json.Serialization;

namespace Back_end.DTOS.Home
{
    public class HomeViewDto
    {
        public int Id { get; set; }

        public string UserId { get; set; } 

      
        public int NumberOfRooms { get; set; }
        [JsonIgnore]
        public string? Massage { get; set; }


        [JsonIgnore]
        public string? NotFoundMassage { get; set; }


    }
}
