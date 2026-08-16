using SistemaNotas.Application.Dtos.Peticion.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Application.Interfaces;

public interface IAnclaService
{
    /// <summary>
    /// Crea una nueva ancla asociada a una sección específica. La sección debe pertenecer al usuario autenticado.
    /// </summary>
    /// <param name="dto">Los datos para crear la ancla</param>
    /// <param name="usuarioId">El ID del usuario autenticado</param>
    /// <param name="cancellationToken">El token de cancelación</param>
    /// <returns>La ancla creada</returns>
    Task<AnclaResponseDto> CrearAnclaAsync(CrearAnclaDto dto, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las anclas asociadas a una sección específica. La sección debe pertenecer al usuario autenticado.
    /// </summary>
    /// <param name="seccionId">El ID de la sección</param>
    /// <param name="usuarioId">El ID del usuario autenticado</param>
    /// <param name="cancellationToken">El token de cancelación</param>
    /// <returns>La lista de anclas obtenidas</returns>
    Task<IReadOnlyList<AnclaResponseDto>> ObtenerPorSeccionIdAsync(Guid seccionId, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una ancla específica por su ID. La ancla debe pertenecer a una sección que a su vez pertenece al usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la ancla</param>
    /// <param name="usuarioId">El ID del usuario autenticado</param>
    /// <param name="cancellationToken">El token de cancelación</param>
    /// <returns>La ancla obtenida</returns>
    Task<AnclaResponseDto> ObtenerAnclaPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza los datos de una ancla existente. La ancla debe pertenecer a una sección que a su vez pertenece al usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la ancla</param>
    /// <param name="usuarioId">El ID del usuario autenticado</param>
    /// <param name="actualizarDto">Los datos para actualizar la ancla</param>
    /// <param name="cancellationToken">El token de cancelación</param>
    Task ActualizarAnclaAsync(Guid id, Guid usuarioId, ActualizarAnclaDto actualizarDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina de manera lógica (Soft Delete) una ancla existente. La ancla debe pertenecer a una sección que a su vez pertenece al usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la ancla</param>
    /// <param name="usuarioId">El ID del usuario autenticado</param>
    /// <param name="cancellationToken">El token de cancelación</param>
    Task EliminarAnclaAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
}