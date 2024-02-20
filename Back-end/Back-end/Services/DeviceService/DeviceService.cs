using Back_end.DTOS.Device;
using Back_end.DTOS.Room;
using Back_end.Models;
using Back_end.Repositories.Interfaces;

namespace Back_end.Services.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        #region Create 
        public async Task<DeviceViewDto> AddRoom(DeviceCreateDto deviceCreateDto)
        { 
            if (int.TryParse(deviceCreateDto.RoomId, out int roomid) )
            {
                var result = _unitOfWork.Repository<Room>().GetById(roomid);

                if (result == null || (result.IsDeleted == true))
                {
                    return new DeviceViewDto { massage = "RoomId IS Not Exist" };
                }
                int count = _unitOfWork.Repository<Device>().Count(r => r.RoomId == roomid && r.IsDeleted == false);
                // compare 
                if (count == result.NumberOfDevices)
                {
                    return new DeviceViewDto { massageBadRequst = $"Number of Device must not exceed {result.NumberOfDevices} in this room " };
                }
                // mapping 
                Device device = new Device();
                device.RoomId = roomid;
                device.EnergyType = deviceCreateDto.EnergyType;
                // save to db 
                _unitOfWork.Repository<Device>().Add(device);
                _unitOfWork.Save();
                return new DeviceViewDto { Id = device.Id, EnergyType = device.EnergyType };

            }
            return new DeviceViewDto { massageBadRequst = "Values is not coorect" };

        }
        #endregion
    }
}
