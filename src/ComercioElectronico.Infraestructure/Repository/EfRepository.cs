//paso 6

using System.Linq.Expressions;
using ComercioElectronico.Domain;
using ComercioElectronico.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ComercioElectronico.Infraestructure.Repository;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly ECommerceDbContext _context;

    public IUnitOfWork UnitOfWork => _context;


    public EfRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

   
    public virtual IQueryable<T> GetAll(bool asNoTracking = true)
    {
        if (asNoTracking)
            return _context.Set<T>().AsNoTracking();
        else
            return _context.Set<T>().AsQueryable();
    }

    public virtual async Task<T> AddAsync(T entity)
    {

        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async  Task UpdateAsync(T entity)
    {
          _context.Update(entity);
        await _context.SaveChangesAsync();
        
        return;
    }

    public virtual void  Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
 
    }
 
    public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> queryable = GetAll();
        foreach (Expression<Func<T, object>> includeProperty in includeProperties)
        {
            queryable = queryable.Include<T, object>(includeProperty);
        }

        return queryable;
    }
}

