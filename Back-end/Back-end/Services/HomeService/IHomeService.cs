using Back_end.DTOS.Home;

namespace Back_end.Services.HomeService
{
    public interface IHomeService
    {
        Task<HomeViewDto> AddHome(HomeCreateDto homeCreateDto);
        Task<HomeViewDto> UpdateHome(HomeUpdateDto homeUpdateDto, int Id);
        Task<HomeViewWithRoomDto?> GetHomeWithRooms(int Id);

        Task<HomeViewDto?> ViewHome(int Id);
        Task<List<HomeViewsDto>> ViewsHome();

        Task<string?> RemoveHome(int Id);

        Task<List<HomeViewsDto>?> ViewsHomeNotDelete();

        Task<List<HomeViewsDto>?> ViewsHomeDelete(); 
    }
}
