using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.Models;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ISession = Cassandra.ISession;

namespace Back_end.DAOs.Implementations
{
    public class Home_OverallDAO : IHome_OverallDAO
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        private readonly IConfiguration _configuration;
       
        public Home_OverallDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            ICassandraDAO cassandraDAO = new CassandraDAO(_configuration.GetValue<string>("CassandraConfiguration:cassandraNodes", "127.0.0.1"), _configuration.GetValue<string>("CassandraConfiguration:Keyspace", "big_data"));
            session = cassandraDAO.GetSession();

            mapper = new Mapper(session);
            
        }
        public async Task<IEnumerable<Home_Overall>> getHome()
        {

            string cql = "SELECT * FROM Home_Overall ;";
            try
            {

                return await mapper.FetchAsync<Home_Overall>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; // or throw the exception, depending on your requirements
            }
        }

        public async Task<Home_Overall> getLastHome(int homeid,string energytype)
        {
            IEnumerable<String> lastend = await mapper.FetchAsync<String>($"select max(end) from home_overall where homeid = {homeid} and energytype = '{energytype}'  ALLOW FILTERING ;");

            string cql = $"SELECT * FROM Home_Overall where end = '{lastend.FirstOrDefault()}' and energytype = '{energytype}' limit 1  ALLOW FILTERING ;";
            try
            {

                IEnumerable<Home_Overall> lasthome = await mapper.FetchAsync<Home_Overall>(cql);
                return lasthome.FirstOrDefault();
             
            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null; // or throw the exception, depending on your requirements
            }
        }

        public async Task<IEnumerable<String>> getHomeStartDates(string energytype, int homeid)
        {

            string cql = $"select start from home_overall where homeid = {homeid} and energytype = '{energytype}' ALLOW FILTERING ;";
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

        public async Task<IEnumerable<String>> getHomeEndDates(Home_OverallS_DateDto s_DateDto, int homeid)
        {

            string cql = $"select end from home_overall where homeid = {homeid} and energytype = '{s_DateDto.energyType}' and end > '{s_DateDto.startDate}' ALLOW FILTERING ;";
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

        public async Task<IEnumerable<Double>> getHomeconsumption(HomeDatesDto datesDto, int homeid)
        {

            string cql = $"select  homeconsumption from home_overall where homeid = {homeid} and energytype = '{datesDto.energyType}' and end > '{datesDto.startDate}'and end <= '{datesDto.endDate}' ALLOW FILTERING ;";
            try
            {

                return await mapper.FetchAsync<Double>(cql);

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
