using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOS.Device
{
    public class DeviceViewDto
    {
        public int Id { get; set; }

        
        public string EnergyType { get; set; } = string.Empty;


    }
}
