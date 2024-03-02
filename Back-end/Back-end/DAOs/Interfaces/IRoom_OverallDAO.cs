using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.DTOs.Cassandra_quries.Room_OverallDtos;
using Back_end.Models;

namespace Back_end.DAOs.Interfaces
{
    public interface IRoom_OverallDAO
    {
        Task<Room_Overall> getLastRoom(int roomid);
        Task<IEnumerable<String>> getRoomStartDates(string energytype, int roomid);
        Task<IEnumerable<String>> getRoomEndDates(Room_OverallS_DateDto s_DateDto, int roomid);
        Task<IEnumerable<Double>> getRoomconsumption(RoomDatesDto datesDto, int roomid);
    }
}
