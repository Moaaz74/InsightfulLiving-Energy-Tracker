using System.Text.Json.Serialization;

namespace Back_end.Models
{
    public class Home
    {
        public int Id { get; set; }

        public string UserId { get; set; } =  string.Empty;

        public  ApplicationUser User { get; set; } = default!;

        public int NumberOfRooms { get; set; }

        [JsonIgnore]
        public virtual List<Room> Rooms { get; set; }  = new List<Room>();
        public bool IsDeleted { get; set; }










    }
}
