using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.DTOs.Cassandra_quries.Room_OverallDtos;
using Back_end.DTOS.Cassandra_quries.Room_OverallDtos;
using Back_end.Models;
using Cassandra;
using Cassandra.Mapping;

namespace Back_end.DAOs.Implementations
{
    public class Room_OverallDAO : IRoom_OverallDAO

    {
        protected readonly Cassandra.ISession session;
        protected readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public Room_OverallDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            ICassandraDAO cassandraDAO = new CassandraDAO(_configuration.GetValue<string>("CassandraConfiguration:cassandraNodes", "127.0.0.1"), _configuration.GetValue<string>("CassandraConfiguration:Keyspace", "big_data"));
            session = cassandraDAO.GetSession();

            mapper = new Mapper(session);

        }

        public async Task<IEnumerable<Room_Overall>> getRoom()
        {

            string cql = "SELECT * FROM Room_Overall ;";
            try
            {

                return await mapper.FetchAsync<Room_Overall>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null;
            }
        }


        public async Task<Room_Overall> getLastRoom(int roomid, string energytype)
        {
            IEnumerable<String> lastend = await mapper.FetchAsync<String>($"select max(end) from room_overall where roomid = {roomid} and energytype = '{energytype}' ALLOW FILTERING ;");
            string cql = $"SELECT * FROM room_Overall where end = '{lastend.FirstOrDefault()}' and energytype = '{energytype}' limit 1  ALLOW FILTERING ;";
            try
            {

                IEnumerable<Room_Overall> lastroom =  await mapper.FetchAsync<Room_Overall>(cql);
                return lastroom.FirstOrDefault();
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; 
            }
        }


        public async Task<IEnumerable<String>> getRoomStartDates(string energytype, int roomid)
        {

            string cql = $"select start from room_overall where roomid = {roomid} and energytype = '{energytype}' ALLOW FILTERING ;";
            try
            {

                return await mapper.FetchAsync<String>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; // or throw the exception, depending on your requirements
            }
        }

        public async Task<IEnumerable<String>> getRoomEndDates(Room_OverallS_DateDto s_DateDto, int roomid)
        {

            string cql = $"select end from room_overall where roomid = {roomid} and energytype = '{s_DateDto.energyType}' and end > '{s_DateDto.startDate}' ALLOW FILTERING ;";
            try
            {

                return await mapper.FetchAsync<String>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; // or throw the exception, depending on your requirements
            }
        }

        public async Task<IEnumerable<RoomConsumptionDto>> getRoomconsumption(RoomDatesDto datesDto, int roomid)
        {

            string cql = $"select start, end , roomconsumption from room_overall where roomid = {roomid} and energytype = '{datesDto.energyType}' and end > '{datesDto.startDate}'and end <= '{datesDto.endDate}' ALLOW FILTERING ;";
            try
            {

                return await mapper.FetchAsync<RoomConsumptionDto>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; // or throw the exception, depending on your requirements
            }
        }
    }
}
