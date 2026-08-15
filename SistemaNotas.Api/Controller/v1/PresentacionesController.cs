using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Presentacion;

namespace SistemaNotas.Api.Controllers;

/// <summary>
/// Controlador para la gestión de Presentaciones (Temas a exponer).
/// </summary>
[Authorize] 
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PresentacionesController : ControllerBase
{
    private readonly IPresentacionService _presentacionService;

    public PresentacionesController(IPresentacionService presentacionService)
    {
        _presentacionService = presentacionService;
    }

    /// <summary>
    /// Crea una nueva presentación en el sistema.
    /// </summary>
    /// <param name="request">Datos de la presentación a crear.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>La presentación recién creada con su ID generado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PresentacionResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearPresentacion(
        [FromBody] CrearPresentacionDto request, 
        CancellationToken cancellationToken)
    {
        PresentacionResponseDto response = await _presentacionService.CrearPresentacionAsync(request, cancellationToken);
        
        // Retorna HTTP 201 (Created) e indica en los Headers la URL para consultar el nuevo recurso
        return CreatedAtAction(nameof(ObtenerPorId), new { id = response.Id }, response);
    }

    /// <summary>
    /// Obtiene el listado de todas las presentaciones activas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de presentaciones.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PresentacionResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodas(CancellationToken cancellationToken)
    {
        IReadOnlyList<PresentacionResponseDto> response = await _presentacionService.ObtenerTodasPresentacionesAsync(cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Obtiene el detalle de una presentación específica por su ID.
    /// </summary>
    /// <param name="id">Identificador único (GUID) de la presentación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Datos de la presentación encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PresentacionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        // Si no existe, el servicio lanzará KeyNotFoundException y el GlobalExceptionHandler 
        // se encargará de devolver un HTTP 404 estructurado automáticamente.
        PresentacionResponseDto response = await _presentacionService.ObtenerPresentacionPorIdAsync(id, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza la información principal de una presentación existente.
    /// </summary>
    /// <param name="id">Identificador único (GUID) de la presentación.</param>
    /// <param name="request">Nuevos datos a actualizar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Respuesta vacía indicando éxito (HTTP 204).</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(
        Guid id, 
        [FromBody] ActualizarPresentacionDto request, 
        CancellationToken cancellationToken)
    {
        await _presentacionService.ActualizarPresentacionAsync(id, request, cancellationToken);
        
        return NoContent();
    }

    /// <summary>
    /// Elimina una presentación de forma lógica (Soft Delete).
    /// </summary>
    /// <param name="id">Identificador único (GUID) de la presentación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Respuesta vacía indicando éxito (HTTP 204).</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(Guid id, CancellationToken cancellationToken)
    {
        await _presentacionService.EliminarPresentacionAsync(id, cancellationToken);
        return NoContent();
    }
}