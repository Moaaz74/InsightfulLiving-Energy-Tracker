using Back_end.DTOS.Room;

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
    }
}
