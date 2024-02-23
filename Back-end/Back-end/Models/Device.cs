using System.ComponentModel.DataAnnotations;

namespace Back_end.Models
{
    public class Device
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; } = default!;


        [MaxLength(50)]
        public string EnergyType { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
