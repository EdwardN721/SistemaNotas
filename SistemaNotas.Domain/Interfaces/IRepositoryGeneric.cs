using SistemaNotas.Domain.Entities;
using System.Linq.Expressions;

namespace SistemaNotas.Domain.Interfaces
{
  public interface IRepositoryGeneric<T> where T : EntityBase
  {
    /// <summary>
    /// Obtener información por el id.
    /// </summary>
    /// <param name="id">Id del objeto a buscar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Objeto de la base de datos.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los objetos de la base de datos.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de solo lectura.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de objetos que cumplan con la condición
    /// </summary>
    /// <param name="predicate">Condición o parametros a definir.</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    /// <returns>Lista de objetos delimitados.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Agrega una entidad a la base de datos
    /// </summary>
    /// <param name="entity">Información a agregar.</param>
    /// <param name="cancellationToken">Token de cancelacion.</param>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si existe algún objeto que cumpla con la condición especificada.
    /// </summary>
    /// <param name="predicate">Condición o parametros a definir.</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    /// <returns>Regresa true si existe al menos un objeto que cumpla con la condición, false en caso contrario.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el primer objeto que cumpla con la condición especificada o null si no existe ninguno.
    /// </summary>
    /// <param name="predicate">Condición o parametros a definir.</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    /// <returns>Regresa el primer objeto que cumpla con la condición o null si no existe ninguno.</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    void Update(T entity);
    void Delete(T entity);
  }
}
