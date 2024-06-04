using Back_end.DTOs.Cassandra_quries.ApplianceDtos;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.DTOS.Cassandra_quries.ApplianceDtos;
using Back_end.Models;

namespace Back_end.DAOs.Interfaces
{
    public interface IApplianceDAO
    {
        Task<IEnumerable<Appliance>> getAppliance();
        Task<Appliance> getLastAppliance(int applianceid, string energytype);
        Task<IEnumerable<String>> getApplianceStartDates(string energytype, int applianceid);
        Task<IEnumerable<String>> getApplianceEndDates(ApplianceS_DateDto s_DateDto, int applianceid);
        Task<IEnumerable<ApplianceConsumptionDto>> getApplianceconsumption(ApplianceDatesDto datesDto, int applianceid);
    }
}
