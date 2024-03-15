using Back_end.DTOS.Room;
using Back_end.Models;

namespace Back_end.Services.RoomService
{
    public interface IRoomService
    {
        Task<RoomViewDto> AddRoom(RoomCreateDto roomCreateDto);
        Task<RoomViewDto> UpdateRoom(RoomUpdateDto roomUpdateDto, int Id);
        Task<string> RemoveRoom(int Id);
        Task<RoomWithDevicesAndHome?> GetRoomWithDevicesAndHome(int Id);
        Task<RoomViewDto?> ViewRoom(int Id);
        Task<List<RoomViewDto>> ViewsRoom();
        Task<List<RoomViewDto>?> ViewsRoomDelete();
        Task<List<RoomViewDto>?> ViewsRoomNotDelete();

        Task<List<int>> GetIdsOfRooms();

        RoomViewDto GetRoomByType(string RoomType);
    }
}
