using Back_end.Repositories.Interfaces;

namespace Back_end.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool Disposing)
        {
            if (this.disposed)
            {
                if (Disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            IRepository<T> repository = new Repository<T>(_context);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
