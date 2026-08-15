using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace SistemaNotas.Infrastructure.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly NotasDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    private IRepositoryGeneric<Usuario>? _usuario;
    private IRepositoryGeneric<Presentacion>? _presentacion;
    private IRepositoryGeneric<Seccion>? _seccion;
    private IRepositoryGeneric<Ancla>? _ancla;
    private IRepositoryGeneric<Retrospectiva>? _retrospectiva;
    private IRepositoryGeneric<CategoriaAncla>? _categoriaAncla;

    public UnitOfWork(NotasDbContext context)
    {
      _context = context;
    }

    public IRepositoryGeneric<Usuario> Usuarios
    {
      get { return _usuario ??= new RepositoryGeneric<Usuario>(_context); }
    }

    public IRepositoryGeneric<Presentacion> Presentaciones {
      get { return _presentacion ??= new RepositoryGeneric<Presentacion>(_context); }  
    }

    public IRepositoryGeneric<Seccion> Secciones
    {
      get { return _seccion ??= new RepositoryGeneric<Seccion>(_context); }
    }

    public IRepositoryGeneric<Ancla> Anclas
    {
      get { return _ancla ??= new RepositoryGeneric<Ancla>(_context); }
    }

    public IRepositoryGeneric<Retrospectiva> Retrospectivas
    {
      get { return _retrospectiva ??= new RepositoryGeneric<Retrospectiva>(_context); }
    }

    public IRepositoryGeneric<CategoriaAncla> CategoriasAncla
    {
      get { return _categoriaAncla ??= new RepositoryGeneric<CategoriaAncla>(_context); }
    }

    public async Task<int> CommitAsync(CancellationToken cancellation = default)
    {
      return await _context.SaveChangesAsync(cancellation);
    }

    #region ManejoDeTransacciones

    public async Task BeginTransactionAsync(CancellationToken cancellation = default)
    {
      if (_currentTransaction != null)
      {
        throw new InvalidOperationException("Ya existe una transacción en curso.");
      }

      _currentTransaction = await _context.Database.BeginTransactionAsync(cancellation);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellation = default)
    {
      try
      {
        await CommitAsync(cancellation);

        if (_currentTransaction !=null)
        {
          await _currentTransaction.CommitAsync(cancellation);
        } 
      } catch
      {
        await RollbackTransactionAsync(cancellation);
        throw;
      } finally
      {
        if (_currentTransaction != null)
        {
          _currentTransaction.Dispose();
          _currentTransaction = null;
        }
      }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellation = default)
    {
      try
      {
        if (_currentTransaction != null)
        {
          await _currentTransaction.RollbackAsync(cancellation);
        }
      }
      finally
      {
        _currentTransaction?.Dispose();
        _currentTransaction = null;
      }
    }
    #endregion

    public void Dispose()
    {
      _currentTransaction?.Dispose();
      _context.Dispose();
      GC.SuppressFinalize(this);
    }
  }
}
