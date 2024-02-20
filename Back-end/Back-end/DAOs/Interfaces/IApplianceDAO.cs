using Back_end.Models;

namespace Back_end.DAOs.Interfaces
{
    public interface IApplianceDAO
    {
        Task<IEnumerable<Appliance>> getAppliance();
    }
}
