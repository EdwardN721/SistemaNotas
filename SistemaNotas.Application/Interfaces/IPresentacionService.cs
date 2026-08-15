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
    /// <returns>Presentación creada con su Id asignado.</returns>
    Task<PresentacionResponseDto> CrearPresentacionAsync(CrearPresentacionDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las presentaciones registradas activas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de presentaciones de solo lectura.</returns>
    Task<IReadOnlyList<PresentacionResponseDto>> ObtenerTodasPresentacionesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una presentación por su identificador único.
    /// </summary>
    /// <param name="id">Id de la presentación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Información de la presentación.</returns>
    Task<PresentacionResponseDto> ObtenerPresentacionPorIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza los datos de una presentación existente.
    /// </summary>
    /// <param name="id">Id de la presentación a actualizar.</param>
    /// <param name="actualizarDto">Información de la presentación a actualizar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task ActualizarPresentacionAsync(Guid id, ActualizarPresentacionDto actualizarDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina de manera lógica (Soft Delete) una presentación.
    /// </summary>
    /// <param name="id">Id de la presentación a eliminar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    Task EliminarPresentacionAsync(Guid id, CancellationToken cancellationToken = default);
  }
}
