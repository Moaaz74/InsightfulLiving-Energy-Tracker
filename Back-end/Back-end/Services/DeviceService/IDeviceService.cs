using Back_end.DTOS.Device;

namespace Back_end.Services.DeviceService
{
    public interface IDeviceService
    {
        Task<DeviceViewDto> AddRoom(DeviceCreateDto deviceCreateDto);
        Task<DeviceViewDto> UpdateDevice(DeviceUpdateDto deviceUpdateDto, int Id);
        Task<string> RemoveDevice(int Id);
        Task<DevicesWithRoomDto?> GetDeviceWithRoom(int Id);
        Task<DeviceViewDto?> ViewDevice(int Id);
        Task<List<DeviceViewDto>> ViewsDevice();
        Task<List<DeviceViewDto>?> ViewsDeviceNotDelete();
        Task<List<DeviceViewDto>?> ViewsDeviceDelete(); 
    }
}
