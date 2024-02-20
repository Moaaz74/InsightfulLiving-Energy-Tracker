using Back_end.DTOS.Device;

namespace Back_end.Services.DeviceService
{
    public interface IDeviceService
    {
        Task<DeviceViewDto> AddRoom(DeviceCreateDto deviceCreateDto); 
    }
}
