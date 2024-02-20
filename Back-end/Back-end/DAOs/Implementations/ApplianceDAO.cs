using Back_end.DAOs.Interfaces;
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
    }

}

