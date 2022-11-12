//paso 2

using System.Linq.Expressions;

namespace ComercioElectronico.Domain.Repository;

public interface IRepository<T> where T : class
{
    IUnitOfWork UnitOfWork { get; }

    IQueryable<T> GetAll(bool asNoTracking = true);

    Task<T> GetByIdAsync(int id);

    Task<T> AddAsync(T entity);

    Task UpdateAsync (T entity);

    void  Delete(T entity);

    IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
}
