using Back_end.DAOs.Interfaces;
using Back_end.DTO;
using Cassandra;
using Cassandra.Mapping;


namespace Back_end.DAOs.Implementations
{
    public class MlDOAs : IMLDOAs
    {
        protected readonly Cassandra.ISession session;
        protected readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public MlDOAs(IConfiguration configuration)
        {
            _configuration = configuration;
            ICassandraDAO cassandraDAO = new CassandraDAO(_configuration.GetValue("CassandraConfiguration:cassandraNodes", "127.0.0.1"), _configuration.GetValue("CassandraConfiguration:Keyspace", "big_data"));
            session = cassandraDAO.GetSession();
            mapper = new Mapper(session);

        }


        public async Task<List<Info>> getLastFire(int homeid)
        {

            string cql = $"SELECT * FROM your_table_name WHERE homeId = {homeid} AND isRead='0'  ALLOW FILTERING ;";
            IEnumerable<Info> info = await mapper.FetchAsync<Info>(cql);

            if (info == null || !info.Any())
            {
                return null;
            }
            List<Info> photoInfos = info.Where(p => p.isRead == "0").ToList();

            foreach (var photo in photoInfos)
            {
                var updateQuery = new SimpleStatement($"UPDATE your_table_name SET isRead = '1' WHERE homeId = {homeid}  AND id = {photo.id} ;");
                await session.ExecuteAsync(updateQuery);
            }
            return photoInfos.ToList();


        }
    }
}
