using SistemaNotas.Application.Dtos.Peticion.Seccion;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;

namespace SistemaNotas.Application.Interfaces;

public interface ISeccionService
{
    /// <summary>
    /// Crea una nueva sección asociada a una presentación específica para un usuario dado.
    /// </summary>
    /// <param name="dto">El DTO con los datos para crear la sección.</param>
    /// <param name="usuarioId">El ID del usuario que crea la sección.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>El DTO con los datos de la sección creada.</returns>
    Task<SeccionResponseDto> CrearSeccionAsync(CrearSeccionDto dto, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene todas las secciones asociadas a una presentación específica para un usuario dado.
    /// </summary>
    /// <param name="presentacionId">El ID de la presentación.</param>
    /// <param name="usuarioId">El ID del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Una lista de DTOs con los datos de las secciones obtenidas.</returns>
    Task<IReadOnlyList<SeccionResponseDto>> ObtenerPorPresentacionIdAsync(Guid presentacionId, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene una sección específica por su ID para un usuario dado.
    /// </summary>
    /// <param name="id">El ID de la sección.</param>
    /// <param name="usuarioId">El ID del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>El DTO con los datos de la sección obtenida.</returns>
    Task<SeccionResponseDto> ObtenerSeccionPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Actualiza una sección específica por su ID para un usuario dado.
    /// </summary>
    /// <param name="id">El ID de la sección.</param>
    /// <param name="usuarioId">El ID del usuario.</param>
    /// <param name="dto">El DTO con los datos para actualizar la sección.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    Task ActualizarSeccionAsync(Guid id, Guid usuarioId, ActualizarSeccionDto dto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Elimina una sección específica por su ID para un usuario dado.
    /// </summary>
    /// <param name="id">El ID de la sección.</param>
    /// <param name="usuarioId">El ID del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    Task EliminarSeccionAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
}