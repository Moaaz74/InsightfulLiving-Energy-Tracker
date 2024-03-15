using Back_end.DTOS.Device;
using Back_end.DTOS.Home;
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
            if (int.TryParse(deviceCreateDto.RoomId, out int roomid))
            {
                var result = _unitOfWork.Repository<Room>().GetById(roomid);

                if (result == null || (result.IsDeleted == true))
                {
                    return new DeviceViewDto { massage = "RoomId IS Not Exist" };
                }
                int count = _unitOfWork.Repository<Device>().Count(r => r.RoomId == roomid && r.IsDeleted == false);
                // compare 
                if (count >= result.NumberOfDevices)
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
                return new DeviceViewDto { Id = device.Id, EnergyType = device.EnergyType , RoomId = device.RoomId };

            }
            return new DeviceViewDto { massageBadRequst = "Values is not coorect" };

        }
        #endregion

        #region Update
        public async Task<DeviceViewDto> UpdateDevice(DeviceUpdateDto deviceUpdateDto, int Id)
        {
            var device = _unitOfWork.Repository<Device>().GetById(Id);
            if (device == null || (device.IsDeleted == true))
            {
                return new DeviceViewDto { massage = "Device Is Not Exist" };
            }
            if (int.TryParse(deviceUpdateDto.RoomId, out int roomid))
            {
                var result = _unitOfWork.Repository<Room>().GetById(roomid);

                if (result == null || (result.IsDeleted == true))
                {
                    return new DeviceViewDto { massage = "Room IS Not Exist" };
                }

                if (roomid != device.RoomId)
                {
                    //  count of room in home ...
                    int count = _unitOfWork.Repository<Device>().Count(r => r.RoomId == roomid && r.IsDeleted == false);
                    // compare 
                    if (count >= result.NumberOfDevices)
                    {
                        return new DeviceViewDto { massageBadRequst = $"Number of Device  must not exceed {result.NumberOfDevices} in this room" };

                    }
                }
                device.RoomId = roomid;
                device.EnergyType = deviceUpdateDto.EnergyType;
                _unitOfWork.Save();
                return new DeviceViewDto { Id = device.Id, EnergyType = device.EnergyType , RoomId=device.RoomId };
            }
            return new DeviceViewDto { massageBadRequst = "Values is not correct" };
        }
        #endregion

        #region delete

        public async Task<string> RemoveDevice(int Id)
        {
            var device = _unitOfWork.Repository<Device>().GetById(Id);
            if (device == null || (device.IsDeleted == true))
            {
                return "Devices Is Not Exist";
            }
            device.IsDeleted = true;
            _unitOfWork.Save();
            return "";
        }
        #endregion

        #region GetRoomswithDivice
        public async Task<DevicesWithRoomDto?> GetDeviceWithRoom(int Id)
        {
            var device = await _unitOfWork.Repository<Device>().FindAsync(h => h.Id == Id, new[] { "Room" });
            if (device == null || (device.IsDeleted == true))
            {
                return null;
            }

            return new DevicesWithRoomDto
            {
                Id = device.Id,
                EnergyType = device.EnergyType,
                room = new RoomViewDto { Id = device.Room.Id , HomeId = device.Room.Id , NumberOfDevices=device.Room.NumberOfDevices },
                
            };
        }
        #endregion

        #region GET Device 
        public async Task<DeviceViewDto?> ViewDevice(int Id)
        {
            var device = _unitOfWork.Repository<Device>().GetById(Id);
            if ((device == null || (device.IsDeleted == true)))
            {
                return null;
            }
            return new DeviceViewDto
            {
                Id = device.Id,
                EnergyType = device.EnergyType ,
                RoomId = device.RoomId,


            };

        }
        #endregion

        #region Get all Devices Deleted or not 
        public async Task<List<DeviceViewDto>> ViewsDevice()
        {
            var devices = _unitOfWork.Repository<Device>().GetAll();
            if (devices == null)
            {
                return null;
            }
            return devices.Select(h => new DeviceViewDto
            {
                Id = h.Id,
                EnergyType= h.EnergyType,
                RoomId = h.RoomId,


            }).ToList();
        }
        #endregion

        public async Task<List<DeviceViewDto>?> ViewsDeviceNotDelete()
        {
            var devices = _unitOfWork.Repository<Device>().GetAll(h => h.IsDeleted == false);
            if (devices == null)
            {
                return null;
            }
            return devices.Select(h => new DeviceViewDto
            {
                Id = h.Id,
                EnergyType=h.EnergyType,
                RoomId = h.RoomId,

            }).ToList();

        }


        public async Task<List<DeviceViewDto>?> ViewsDeviceDelete()
        {
            var devices = _unitOfWork.Repository<Device>().GetAll(h => h.IsDeleted == true);
            if (devices == null)
            {
                return null;
            }
            return devices.Select(h => new DeviceViewDto
            {
                Id = h.Id,
                EnergyType = h.EnergyType,
                RoomId = h.RoomId,


            }).ToList();


        }
    }
}
