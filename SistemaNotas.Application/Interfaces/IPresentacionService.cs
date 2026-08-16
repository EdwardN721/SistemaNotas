using SistemaNotas.Application.Dtos.Peticion.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Presentacion;

namespace SistemaNotas.Application.Interfaces
{
  public interface IPresentacionService
  {
    /// <summary>
    /// Crea una nueva presentación.
    /// </summary>
    /// <param name="dto">Información de la presentación a crear.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <param name="usuarioId">Id del usuario que crea la presentación.</param>
    /// <returns>Presentación creada con su Id asignado.</returns>
    Task<PresentacionResponseDto> CrearPresentacionAsync(CrearPresentacionDto dto, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las presentaciones registradas activas.
    /// </summary>
    /// <param name="usuarioId">Id del usuario del que se obtendrán las presentaciones.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de presentaciones de solo lectura.</returns>
    Task<IReadOnlyList<PresentacionResponseDto>> ObtenerTodasPresentacionesAsync(Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una presentación por su identificador único.
    /// </summary>
    /// <param name="id">Id de la presentación.</param>
    /// <param name="usuarioId">Id del usuario al que pertenece la presentación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Información de la presentación.</returns>
    Task<PresentacionResponseDto> ObtenerPresentacionPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza los datos de una presentación existente.
    /// </summary>
    /// <param name="id">Id de la presentación a actualizar.</param>
    /// <param name="usuarioId">Id del usuario al que pertenece la presentación.</param>
    /// <param name="actualizarDto">Información de la presentación a actualizar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task ActualizarPresentacionAsync(Guid id, Guid usuarioId, ActualizarPresentacionDto actualizarDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina de manera lógica (Soft Delete) una presentación.
    /// </summary>
    /// <param name="id">Id de la presentación a eliminar.</param>
    /// <param name="usuarioId">Id del usuario al que pertenece la presentación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task EliminarPresentacionAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
  }
}
