using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries.ApplianceDtos;
using Back_end.DTOs.Cassandra_quries.Home_OverallDtos;
using Back_end.Models;
using Cassandra.Mapping;

namespace Back_end.DAOs.Implementations
{
    public class ApplianceDAO : IApplianceDAO
    {
        protected readonly Cassandra.ISession session;
        protected readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public ApplianceDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            ICassandraDAO cassandraDAO = new CassandraDAO(_configuration.GetValue<string>("CassandraConfiguration:cassandraNodes", "127.0.0.1"), _configuration.GetValue<string>("CassandraConfiguration:Keyspace", "big_data"));
            session = cassandraDAO.GetSession();

            mapper = new Mapper(session);

        }

        public async Task<IEnumerable<Appliance>> getAppliance()
        {

            string cql = "SELECT * FROM Appliance ;";
            try
            {

                return await mapper.FetchAsync<Appliance>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null;
            }
        }
        public async Task<Appliance> getLastAppliance(int applianceid, string energytype)
        {
            IEnumerable<String> lastend = await mapper.FetchAsync<String>($"select max(end) from appliance where applianceid = {applianceid} and energytype = '{energytype}'  ALLOW FILTERING ;");

            string cql = $"SELECT * FROM appliance where end = '{lastend.FirstOrDefault()}'  and energytype = '{energytype}' limit 1  ALLOW FILTERING ;";
            try
            {

                IEnumerable<Appliance> lastappliance = await mapper.FetchAsync<Appliance>(cql);
                return lastappliance.FirstOrDefault();

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<String>> getApplianceStartDates(string energytype, int applianceid)
        {

            string cql = $"select start from appliance where applianceid = {applianceid} and energytype = '{energytype}' ALLOW FILTERING ;";
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

        public async Task<IEnumerable<String>> getApplianceEndDates(ApplianceS_DateDto s_DateDto, int applianceid)
        {

            string cql = $"select end from appliance where applianceid = {applianceid} and energytype = '{s_DateDto.energyType}' and end > '{s_DateDto.startDate}' ALLOW FILTERING ;";
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

        public async Task<IEnumerable<Double>> getApplianceconsumption(ApplianceDatesDto datesDto, int applianceid)
        {

            string cql = $"select  applianceconsumption from appliance where applianceid = {applianceid} and energytype = '{datesDto.energyType}' and end > '{datesDto.startDate}'and end <= '{datesDto.endDate}' ALLOW FILTERING ;";
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

