using Back_end.DTOs.Cassandra_quries;
using Back_end.Models;
using Cassandra;

namespace Back_end.DAOs.Interfaces
{
    public interface IHome_OverallDAO
    {
        Task<IEnumerable<Home_Overall>> getHome();
    }
}
