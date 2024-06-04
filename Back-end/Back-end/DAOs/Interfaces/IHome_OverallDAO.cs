using Back_end.DTOs.Cassandra_quries;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.DTOS.Cassandra_quries.Home_OverallDtos;
using Back_end.Models;
using Cassandra;

namespace Back_end.DAOs.Interfaces
{
    public interface IHome_OverallDAO
    {
        Task<IEnumerable<Home_Overall>> getHome(int homeid);
        Task<Home_Overall> getLastHome(int homeid, string energytype);
        Task<IEnumerable<String>> getHomeStartDates(string energytype,int homeid);
        Task<IEnumerable<String>> getHomeEndDates(Home_OverallS_DateDto s_DateDto, int homeid);
        Task<IEnumerable<HomeConsumptionDto>> getHomeconsumption(HomeDatesDto datesDto, int homeid);
    }
}
