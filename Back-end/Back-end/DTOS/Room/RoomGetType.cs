using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOS.Room
{
    public class RoomGetType
    {
        [Required]
        public string Type { get; set; }
    }
}
