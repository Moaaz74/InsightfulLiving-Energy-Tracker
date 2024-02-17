using Cassandra;

namespace Back_end.DAOs.Interfaces
{
    public interface ICassandraDAO : IDisposable
    {
        Cassandra.ISession GetSession();

    }
}
