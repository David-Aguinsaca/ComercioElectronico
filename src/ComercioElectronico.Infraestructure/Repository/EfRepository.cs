//paso 6

using System.Linq.Expressions;
using ComercioElectronico.Domain;
using ComercioElectronico.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ComercioElectronico.Infraestructure.Repository;

public class EfRepository<TEntity, TEntityId> : IRepository<TEntity, TEntityId> where TEntity : class
{
    private readonly ECommerceDbContext _context;

    public IUnitOfWork UnitOfWork => _context;


    public EfRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> GetByIdAsync(TEntityId id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

   
    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
    {
        if (asNoTracking)
            return _context.Set<TEntity>().AsNoTracking();
        else
            return _context.Set<TEntity>().AsQueryable();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {

        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async  Task UpdateAsync(TEntity entity)
    {
          _context.Update(entity);
        await _context.SaveChangesAsync();
        
        return;
    }

    public virtual void  Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
 
    }
 
    public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> queryable = GetAll();
        foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
        {
            queryable = queryable.Include<TEntity, object>(includeProperty);
        }

        return queryable;
    }
}

