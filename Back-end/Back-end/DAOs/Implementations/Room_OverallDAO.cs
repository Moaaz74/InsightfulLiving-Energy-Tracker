using Back_end.DAOs.Interfaces;
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
    
}
}
