using Back_end.DAOs.Interfaces;
using Cassandra;

namespace Back_end.DAOs.Implementations
{
    public class CassandraDAO : ICassandraDAO
    {
        private static Cluster Cluster;
        private static Cassandra.ISession Session;
        private readonly IConfiguration _configuration;

        public CassandraDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            SetCluster();
        }

        private void SetCluster()
        {
            if (Cluster == null)
            {
                Cluster = Connect();
            }
        }

        public Cassandra.ISession GetSession()
        {
            if (Cluster == null)
            {
                SetCluster();
                Session = Cluster.Connect(getAppSetting("Keyspace"));
            }
            else if (Session == null)
            {
                Session = Cluster.Connect(getAppSetting("Keyspace"));
            }

            return Session;
        }

        private Cluster Connect()
        {
            //string user = getAppSetting("cassandraUser");
            //string pwd = getAppSetting("cassandraPassword");
            string[] nodes = getAppSetting("cassandraNodes").Split(',');

            QueryOptions queryOptions = new QueryOptions().SetConsistencyLevel(ConsistencyLevel.One);


            Cluster cluster = Cluster.Builder()
            .AddContactPoints(nodes)
            //.WithCredentials(user, pwd)
            .WithQueryOptions(queryOptions)
            .Build();
            if (cluster is not null)
                Console.WriteLine("Connected successfully!!!");
            return cluster;
        }

        private string getAppSetting(string key)
        {
            return _configuration["CassandraConfiguration:" + key];
        }
    }
}
