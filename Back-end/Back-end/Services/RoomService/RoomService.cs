using Back_end.DTOS.Home;
using Back_end.DTOS.Room;
using Back_end.Models;
using Back_end.Repositories.Interfaces;

namespace Back_end.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Create 
        public async Task<RoomViewDto> AddRoom(RoomCreateDto roomCreateDto)
        {
            //cheack Home ID ... 
            if (int.TryParse(roomCreateDto.HomeId, out int homeid) && (int.TryParse(roomCreateDto.NumberOfDevices, out int numberOfDevices)))
            {
                var result = _unitOfWork.Repository<Home>().GetById(homeid);

                if (result == null || (result.IsDeleted == true))
                {
                    return new RoomViewDto { massage = "HomeId IS Not Exist" };
                }
                //  count of room in home ...
                int count = _unitOfWork.Repository<Room>().Count(r => r.HomeId == homeid && r.IsDeleted==false);
                // compare 
                
                if (count >= result.NumberOfRooms)
                {
                    return new RoomViewDto { massageBadRequst = $"Number of room must not exceed{result.NumberOfRooms}" };

                }

                // mapping 
                Room room = new Room();
                room.NumberOfDevices = numberOfDevices;
                room.HomeId = homeid;

                // save to db 
                _unitOfWork.Repository<Room>().Add(room);
                _unitOfWork.Save();
                return new RoomViewDto { Id = room.Id, HomeId = room.HomeId, NumberOfDevices = room.NumberOfDevices };

            }
            return new RoomViewDto { massageBadRequst = "Values is not coorect" };

        }
        #endregion

        #region Update
        public async Task<RoomViewDto> UpdateRoom(RoomUpdateDto roomUpdateDto, int Id)
        {
            //cheack id of room 
            var room = _unitOfWork.Repository<Room>().GetById(Id);
            if (room == null || (room.IsDeleted==true))
            {
                return new RoomViewDto { massage = "Room Is Not Exist" };   
            }
            // Home ID ... 
            if (int.TryParse(roomUpdateDto.HomeId, out int homeid)&& (int.TryParse(roomUpdateDto.NumberOfDevices, out int numberOfDevices)))
            {
                var result = _unitOfWork.Repository<Home>().GetById(homeid);

                if (result == null || (result.IsDeleted == true))
                {
                    return new RoomViewDto { massage = "Home IS Not Exist" };
                }
                if (homeid != room.HomeId)
                {
                    //  count of room in home ...
                    int count = _unitOfWork.Repository<Room>().Count(r => r.HomeId == homeid && r.IsDeleted == false);
                    // compare 
                    if (count == result.NumberOfRooms)
                    {
                        return new RoomViewDto { massageBadRequst = $"Number of room must not exceed in this home {result.NumberOfRooms}" };
                    }
                }
                if (numberOfDevices!= room.NumberOfDevices)
                {
                    int countDevice = _unitOfWork.Repository<Device>().Count(r => r.RoomId == room.Id && r.IsDeleted == false);
                    if (countDevice > numberOfDevices)
                    {
                        return new RoomViewDto { massageBadRequst = $"this room have  {countDevice} aready" };

                    }

                }
                room.NumberOfDevices = numberOfDevices;
                room.HomeId = homeid;
                _unitOfWork.Save();

                return new RoomViewDto { Id = room.Id, HomeId = room.HomeId, NumberOfDevices = room.NumberOfDevices };
            }

            return new RoomViewDto { massageBadRequst = "Values is not correct" };

        }
        #endregion

        #region delete

        public async Task<string> RemoveRoom(int Id)
        {
            var room = await _unitOfWork.Repository<Room>().FindAsync(h => h.Id == Id, new[] {"devices"});
            if (room == null || (room.IsDeleted == true))
            {
                return "Room Is Not Exist";
            }
            room.IsDeleted = true;
            foreach (var device in room.devices)
            {
                device.IsDeleted = true;    
            }

            _unitOfWork.Save();
            return "";


        }
        #endregion

        #region GetHomeWithRoomswithDivice
        public async Task<RoomWithDevicesAndHome?> GetRoomWithDevicesAndHome(int Id)
        {
            var room = await _unitOfWork.Repository<Room>().FindAsync(h => h.Id == Id, new[] { "devices", "home" });
            if (room == null || (room.IsDeleted == true))
            {
                return null;
            }

            return new RoomWithDevicesAndHome
            {
                Id = room.Id,
                NumberOfDevices = room.NumberOfDevices,
                devices = room.devices.Where(r => r.IsDeleted == false).Select(d => new DTOS.Device.DeviceViewDto { EnergyType = d.EnergyType, Id = d.Id }).ToList(),
                home =  new HomeViewDto { Id = room.home.Id, NumberOfRooms = room.home.NumberOfRooms, UserId = room.home.UserId }
            };

                

        }
        #endregion

        #region GET Room 
        public async Task<RoomViewDto?> ViewRoom(int Id)
        {
            var room = _unitOfWork.Repository<Room>().GetById(Id);
            if ((room == null || (room.IsDeleted == true)))
            {
                return null;
            }
            return new RoomViewDto
            {
                Id = room.Id,
                NumberOfDevices = room.NumberOfDevices,
                HomeId = room.HomeId
            };

        }
        #endregion

        #region Get all Rooms Deleted or not 
        public async Task<List<RoomViewDto>> ViewsRoom()
        {
            var rooms = _unitOfWork.Repository<Room>().GetAll();
            if (rooms == null)
            {
                return null;
            }
            return rooms.Select(h => new RoomViewDto
            {
                Id = h.Id,
                NumberOfDevices= h.NumberOfDevices,
                HomeId = h.HomeId,
            }).ToList();
        }
        #endregion

        public async Task<List<RoomViewDto>?> ViewsRoomNotDelete()
        {
            var rooms = _unitOfWork.Repository<Room>().GetAll(h => h.IsDeleted == false);
            if (rooms == null)
            {
                return null;
            }
            return rooms.Select(h => new RoomViewDto
            {
                Id = h.Id,
                NumberOfDevices= h.NumberOfDevices,
                HomeId = h.HomeId,
                
            }).ToList();
        }
        public async Task<List<RoomViewDto>?> ViewsRoomDelete()
        {
            var rooms = _unitOfWork.Repository<Room>().GetAll(h => h.IsDeleted == true);
            if (rooms == null)
            {
                return null;
            }

            return rooms.Select(h => new RoomViewDto
            {
                Id = h.Id,
                NumberOfDevices= h.NumberOfDevices,
                HomeId = h.HomeId,
                
            }).ToList();

        }
    }


}

