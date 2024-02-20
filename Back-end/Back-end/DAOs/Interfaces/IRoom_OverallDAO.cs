using Back_end.Models;

namespace Back_end.DAOs.Interfaces
{
    public interface IRoom_OverallDAO
    {
        Task<IEnumerable<Room_Overall>> getRoom();
    }
}
