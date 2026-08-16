using SistemaNotas.Application.Dtos.Peticion.Categorias;
using SistemaNotas.Application.Dtos.Respuesta.Categorias;

namespace SistemaNotas.Application.Interfaces;

public interface ICategoriaAnclaService
{
    /// <summary>
    /// Obtiene la lista de categorías que están activas.
    /// </summary>
    /// <param name="usuarioId">El ID del usuario que solicita (para auditoría o futuro filtrado)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de categorías activas</returns>
    Task<IReadOnlyList<CategoriaAnclaResponseDto>> ObtenerCategoriasActivasAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Crea una nueva categoría.
    /// </summary>
    /// <param name="dto">Datos de la categoría a crear</param>
    /// <param name="usuarioId">El ID del usuario que la crea</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La categoría creada</returns>
    Task<CategoriaAnclaResponseDto> CrearCategoriaAsync(CrearCategoriaAnclaDto dto, Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las categorías (activas e inactivas).
    /// </summary>
    /// <param name="usuarioId">El ID del usuario que solicita</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista completa de categorías</returns>
    Task<IReadOnlyList<CategoriaAnclaResponseDto>> ObtenerCategoriasAsync(Guid usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza la información de una categoría existente.
    /// </summary>
    /// <param name="usuarioId">El ID del usuario que actualiza</param>
    /// <param name="categoriaId">ID de la categoría a actualizar</param>
    /// <param name="actualizarCategoriaDto">Datos nuevos</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    Task ActualizarCategoriaAsync(Guid usuarioId, Guid categoriaId, ActualizarCategoriaDto actualizarCategoriaDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina de forma lógica o física una categoría.
    /// </summary>
    /// <param name="usuarioId">El ID del usuario que la elimina</param>
    /// <param name="categoriaId">ID de la categoría</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    Task EliminarCategoriaAsync(Guid usuarioId, Guid categoriaId, CancellationToken cancellationToken = default);
}