using Cassandra;

namespace Back_end.DAOs.Interfaces
{
    public interface ICassandraDAO
    {
        Cassandra.ISession GetSession();

    }
}
