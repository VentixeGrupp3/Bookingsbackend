using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> AddAsync(TEntity entity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? filterBy = null, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> findBy, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> UpdateAsync(TEntity entity);
}

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _entities;


    public BaseRepository(DataContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        if (entity == default)
            return false;

        try
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _entities.FirstOrDefaultAsync(expression);

        if (entity == default)
            return false;

        try
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();

            return !await _entities.AnyAsync(expression);
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return false;
        }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _entities.AnyAsync(expression);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool orderByDescending = false,
        Expression<Func<TEntity, object>>? sortBy = null,
        Expression<Func<TEntity, bool>>? filterBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _entities;

        if (filterBy != null)
            query = query.Where(filterBy);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> findBy,
                                                params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _entities;

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(findBy);
        return entity ?? null!;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        if (entity == default)
            return false;

        try
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
