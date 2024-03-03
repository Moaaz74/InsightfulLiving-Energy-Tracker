using Back_end.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Back_end.Repositories.Implementations
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        private bool disposed = false;

        public Repository(ApplicationDbContext ApplicationDbContext)
        {
            _context = ApplicationDbContext;
            dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
            return entity;
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

        public IEnumerable<T> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string IncludeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
            if (orderBy != null)
                return orderBy(query).ToList();

            return query.ToList();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T GetByFilter(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }


        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

    }
}
