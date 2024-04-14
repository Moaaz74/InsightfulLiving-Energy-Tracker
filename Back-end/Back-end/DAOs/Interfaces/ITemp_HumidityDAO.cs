using Back_end.Models;

namespace Back_end.DAOs.Interfaces
{
    public interface ITemp_HumidityDAO
    {
        Task<IEnumerable<Temp_Humidity>> getTemp_Humidity();
        Task<Temp_Humidity> getLastTemp_Humidity(int roomid);
    }
}
