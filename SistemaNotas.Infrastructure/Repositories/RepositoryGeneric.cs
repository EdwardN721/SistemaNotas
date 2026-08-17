using System.Linq.Expressions;
using SistemaNotas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Infrastructure.Data;

namespace SistemaNotas.Infrastructure.Repositories
{
  public class RepositoryGeneric<T>(NotasDbContext context) : IRepositoryGeneric<T> where T : EntityBase
  {
    protected readonly NotasDbContext _context = context;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = false, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();
        
        if (disableTracking) query = query.AsNoTracking();
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate, 
        bool disableTracking = false,
        CancellationToken cancellationToken = default, 
        params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
      await _context.Set<T>().AddAsync(entity, cancellationToken);
    }
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate, 
        bool disableTracking = false,
        CancellationToken cancellationToken = default, 
        params string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
    }
  }
}
