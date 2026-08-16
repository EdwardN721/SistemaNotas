using SistemaNotas.Application.Dtos.Peticion.Retrospectivas;
using SistemaNotas.Application.Dtos.Respuesta.Retrospectivas;

namespace SistemaNotas.Application.Interfaces;

public interface IRetrospectivaService
{
    /// <summary>
    /// Crea la retrospectiva de una presentacion
    /// </summary>
    /// <param name="dto">Datos para crear la retrospectiva</param>
    /// <param name="usuarioId">Id del usuario que creo la presentacion</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    Task<RetrospectivaResponseDto> CrearRetrospectivaAsync(CrearRetrospectivaDto dto, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene la retrospectiva por Id
    /// </summary>
    /// <param name="id">Id de la retrospectiva</param>
    /// <param name="usuarioId">Id del usuario que busca</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    Task<RetrospectivaResponseDto> ObtenerRetrospectivaPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Obtiene la retrospectiva de una presentacion por su id
    /// </summary>
    /// <param name="presentacionId">Id de la presentacion</param>
    /// <param name="usuarioId">Id del usuario que busca</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    Task<RetrospectivaResponseDto?> ObtenerRetrospectivaPorPresentacionIdAsync(Guid presentacionId, Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Actualizar retrospectiva
    /// </summary>
    /// <param name="id">Id de la retrospectiva</param>
    /// <param name="usuarioId">Id del usuario que actualiza</param>
    /// <param name="dto">Informacion para actualizar</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    Task ActualizarRetrospectivaAsync(Guid id, Guid usuarioId, ActualizarRetrospectivaDto dto, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Eliminar una retrospectiva
    /// </summary>
    /// <param name="id">Id de la retrospectiva a eliminar</param>
    /// <param name="usuarioId">Id del usuario que elimina</param>
    /// <param name="cancellationToken">Token de cancelacion</param>
    Task EliminarRetrospectivaAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default);
}