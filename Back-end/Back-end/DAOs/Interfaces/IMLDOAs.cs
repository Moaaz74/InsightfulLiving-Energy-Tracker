

using Back_end.DTO;

namespace Back_end.DAOs.Interfaces
{
    public interface IMLDOAs
    {
        Task<List<Info>> getLastFire(int homeidd);
    }
}
