using Back_end.DAOs.Interfaces;
using Back_end.Models;
using Cassandra.Mapping;

namespace Back_end.DAOs.Implementations
{
    public class Temp_HumidityDAO : ITemp_HumidityDAO
    {

        protected readonly Cassandra.ISession session;
        protected readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public Temp_HumidityDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            ICassandraDAO cassandraDAO = new CassandraDAO(_configuration.GetValue<string>("CassandraConfiguration:cassandraNodes", "127.0.0.1"), _configuration.GetValue<string>("CassandraConfiguration:Keyspace", "big_data"));
            session = cassandraDAO.GetSession();

            mapper = new Mapper(session);

        }

        public async Task<IEnumerable<Temp_Humidity>> getTemp_Humidity()
        {

            string cql = "SELECT * FROM temp_humidity ;";
            try
            {

                return await mapper.FetchAsync<Temp_Humidity>(cql);

            }

            catch (Exception ex)
            {
                // Handle any exceptions that occurred during query execution
                Console.WriteLine($"Error executing query: {ex.Message}");
                return null;
            }
        }

    }
}

