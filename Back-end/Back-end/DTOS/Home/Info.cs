using System.Text.Json.Serialization;

namespace Back_end.DTO
{
    public class Info
    {
        [JsonIgnore]
        public Guid id { get; set; }
        public int homeId { get; set; }
        [JsonIgnore]
        public string isRead { get; set; }
        public int roomId { get; set; }
        public DateTime datetime { get; set; }
        public string Massage { get; set; } = "Fire Detected!";
    }

}

