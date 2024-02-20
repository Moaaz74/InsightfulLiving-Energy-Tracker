using Back_end.DAOs.Interfaces;
using Back_end.DTOs.Cassandra_quries;
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
   
               return  await mapper.FetchAsync<Home_Overall>(cql);
             
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
