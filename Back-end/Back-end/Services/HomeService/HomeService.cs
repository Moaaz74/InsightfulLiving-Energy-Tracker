using Back_end.DTOS.Home;
using Back_end.Models;
using Back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Back_end.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly UserManager<ApplicationUser> _manager;
        public HomeService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> manager)
        {
            _unitOfWork = unitOfWork;
            _manager = manager;
        }

        #region Create 
        public async Task<HomeViewDto> AddHome(HomeCreateDto homeCreateDto)
        {
            //user validation
            var result = await _manager.FindByIdAsync(homeCreateDto.UserId);
            if (result == null)
            {
                return new HomeViewDto { NotFoundMassage = "UserID Is Not Found" };
            }

            // mapping 
            Home home = new Home()
            {
                UserId = homeCreateDto.UserId,
            };
            if (int.TryParse(homeCreateDto.NumberOfRooms, out int numberOfRooms))
            {
                home.NumberOfRooms = numberOfRooms;
            }
            // save to db 
            _unitOfWork.Repository<Home>().Add(home);
            _unitOfWork.Save();
            return new HomeViewDto { Id = home.Id, UserId = home.UserId, NumberOfRooms = home.NumberOfRooms };

        }
        #endregion

        #region Update
        public async Task<HomeViewDto> UpdateHome(HomeUpdateDto homeUpdateDto, int Id)
        {
            Home home = _unitOfWork.Repository<Home>().GetById(Id);
            if (home == null || (home.IsDeleted == true))
            {
                return new HomeViewDto { NotFoundMassage = "Home Is Not Exist" };
            }
            //user validation
            var result = await _manager.FindByIdAsync(homeUpdateDto.UserId);
            if (result == null)
            {
                return new HomeViewDto { NotFoundMassage = "UserID Is Not Exist" };
            }

            if (int.TryParse(homeUpdateDto.NumberOfRooms, out int numberOfRooms))
            {
                // Will be Reviewed
                if (numberOfRooms != home.NumberOfRooms)
                {
                    int count = _unitOfWork.Repository<Room>().Count(r => r.HomeId == home.Id && r.IsDeleted == false);
                    if (count > numberOfRooms)
                    {
                        return new HomeViewDto { Massage = $"Home have {count} Room aready " };
                    }
                }
                home.NumberOfRooms = numberOfRooms;
                home.UserId = homeUpdateDto.UserId;
                await _unitOfWork.Repository<Home>().UpdateAsync(home);
                _unitOfWork.Save();
                return new HomeViewDto { Id = home.Id, NumberOfRooms = home.NumberOfRooms, UserId = home.UserId };
            }
            return new HomeViewDto { Massage = " Invalid Data " };

        }
        #endregion

        #region GetHomeWithRooms
        public async Task<HomeViewWithRoomDto?> GetHomeWithRooms(int Id)
        {
            var home = await _unitOfWork.Repository<Home>().FindAsync(h => h.Id == Id, new[] { "Rooms", "User" });
            if (home == null || (home.IsDeleted == true))
            {
                return null;
            }

            return new HomeViewWithRoomDto
            {
                Id = home.Id,
                NumberOfRooms = home.NumberOfRooms,
                User = new HomeUserDto { Id = home.User.Id, Name = home.User.UserName, Email = home.User.Email },
                Rooms = home.Rooms.Where(r => r.IsDeleted == false).Select(R => new DTOS.Room.RoomHomeViewDto
                {
                    Id = R.Id,
                    NumberOfDevices = R.NumberOfDevices

                }).ToList()
            };

        }
        #endregion

        public async Task<HomeViewDto?> ViewHome(int Id)
        {
            var home = _unitOfWork.Repository<Home>().GetById(Id);
            if ((home == null || (home.IsDeleted == true)))
            {
                return null;
            }
            return new HomeViewDto
            {
                Id = home.Id,
                NumberOfRooms = home.NumberOfRooms,
                UserId = home.UserId
            };

        }

        public async Task<List<HomeViewsDto>> ViewsHome()
        {
            var homes = _unitOfWork.Repository<Home>().GetAll();
            if (homes == null)
            {
                return null;
            }


            return homes.Select(h => new HomeViewsDto
            {
                Id = h.Id,
                NumberOfRooms = h.NumberOfRooms,
                UserId = h.UserId
            }).ToList();


        }


        public async Task<string?> RemoveHome(int Id)
        {
            var home = await _unitOfWork.Repository<Home>().FindAsync(h => h.Id == Id, new[] { "Rooms" });
            if (home == null || (home.IsDeleted == true))
            {
                return "Home Is Not Exist";
            }

            home.IsDeleted = true;
            foreach (var room in home.Rooms)
            {
                room.IsDeleted = true;
                var roomR = await _unitOfWork.Repository<Room>().FindAsync(r => r.Id == room.Id, new[] { "devices" });
                foreach (var device in roomR.devices)
                {
                    device.IsDeleted = true;
                }
            }

            _unitOfWork.Save();
            return "";

        }


        public async Task<List<HomeViewsDto>?> ViewsHomeNotDelete()
        {
            var homes = _unitOfWork.Repository<Home>().GetAll(h => h.IsDeleted == false);
            if (homes == null)
            {
                return null;
            }


            return homes.Select(h => new HomeViewsDto
            {
                Id = h.Id,
                NumberOfRooms = h.NumberOfRooms,
                UserId = h.UserId
            }).ToList();


        }


        public async Task<List<HomeViewsDto>?> ViewsHomeDelete()
        {
            var homes = _unitOfWork.Repository<Home>().GetAll(h => h.IsDeleted == true);
            if (homes == null)
            {
                return null;
            }


            return homes.Select(h => new HomeViewsDto
            {
                Id = h.Id,
                NumberOfRooms = h.NumberOfRooms,
                UserId = h.UserId
            }).ToList();


        }



        public async Task<List<int>> GetIdsOfHomes()
        {
            var homes = _unitOfWork.Repository<Home>().GetAll(h => h.IsDeleted == false);
            if (homes == null)
            {
                return null;
            }
            return homes.Select(h => h.Id).ToList();
        }

        public async Task<MlDto> GetHomeEmailAndPhone(int Id)
        {
            var home = await _unitOfWork.Repository<Home>().FindAsync(h => h.Id == Id, new[] { "User" });
            return new MlDto { Email = home.User.Email, PhoneNumber = home.User.PhoneNumber };
        }

        public Home GetHomeByUserId(string UserId)
        {
            return _unitOfWork.Repository<Home>().GetAll(filter: h => h.UserId == UserId).ToList().FirstOrDefault<Home>();
        }

        public Home GetHomeByRoom(Room Room)
        {
            return _unitOfWork.Repository<Home>().GetAll(filter: h => h.Rooms.Contains(Room), IncludeProperties: "Rooms").FirstOrDefault<Home>();
        }
    }

}

