using System.Linq.Expressions;
using SistemaNotas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Infrastructure.Data;

namespace SistemaNotas.Infrastructure.Repository
{
  public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : EntityBase
  {
    protected readonly NotasDbContext _context;

    public RepositoryGeneric(NotasDbContext context)
    {
      _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      // FindAsync recibe un arreglo de llaves primarias y luego el token
      return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
      return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
      await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
    }
  }
}
