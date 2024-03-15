using Back_end.DAOs.Interfaces;
using Cassandra;
using Microsoft.AspNetCore.Http;

namespace Back_end.DAOs.Implementations
{
    public class CassandraDAO : ICassandraDAO
    {
        private readonly Cluster _cluster;
        private readonly Cassandra.ISession _session;
        private readonly IConfiguration _configuration;
        private bool disposed = false;

        public CassandraDAO(string contactPoint , string keyspace)
        {
            _cluster = Cluster.Builder()
                         .AddContactPoint(contactPoint)
                         .WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.One))
                         .Build();
            _session = _cluster.Connect(keyspace);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _session.Dispose();
                    _cluster.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Cassandra.ISession GetSession()
        {
            return _session;
        }

    }
}
