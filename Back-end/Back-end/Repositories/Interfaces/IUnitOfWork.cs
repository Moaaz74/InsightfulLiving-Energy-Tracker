namespace Back_end.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork
        {
            IRepository<T> Repository<T>() where T : class;

            void Save();
        }
    }
}
