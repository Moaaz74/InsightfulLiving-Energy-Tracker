using System.Linq.Expressions;

namespace Back_end.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string IncludeProperties = "");

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        void Add(T entity);

        Task<T> AddAsync(T entity);

        void Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Delete(T entity);

        Task<T> DeleteAsync(T entity);
    }
}
