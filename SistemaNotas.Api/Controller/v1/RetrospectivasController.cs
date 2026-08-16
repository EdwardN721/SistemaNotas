using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaNotas.Api.Extensions; 
using SistemaNotas.Application.Dtos.Peticion.Retrospectivas;
using SistemaNotas.Application.Interfaces;

namespace SistemaNotas.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/[Controller]")]
public class RetrospectivasController : ControllerBase
{
    private readonly IRetrospectivaService _retrospectivaService;

    public RetrospectivasController(IRetrospectivaService retrospectivaService)
    {
        _retrospectivaService = retrospectivaService;
    }

    /// <summary>
    /// Crea la evaluación final para una presentación.
    /// </summary>
    [HttpPost("presentaciones/{presentacionId:guid}/retrospectivas")]
    public async Task<IActionResult> Crear(
        [FromRoute] Guid presentacionId, 
        [FromBody] CrearRetrospectivaDto request, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        
        var dtoConIdCorrecto = request with { PresentacionId = presentacionId };
        
        var response = await _retrospectivaService.CrearRetrospectivaAsync(dtoConIdCorrecto, usuarioId, cancellationToken);
        
        return CreatedAtAction(nameof(ObtenerPorId), new { id = response.Id, version = "1.0" }, response);
    }

    /// <summary>
    /// Obtiene la retrospectiva asociada a una presentación (si existe).
    /// </summary>
    [HttpGet("presentaciones/{presentacionId:guid}/retrospectivas")]
    public async Task<IActionResult> ObtenerPorPresentacion(
        [FromRoute] Guid presentacionId, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        var response = await _retrospectivaService.ObtenerRetrospectivaPorPresentacionIdAsync(presentacionId, usuarioId, cancellationToken);
        
        if (response is null) return NoContent();

        return Ok(response);
    }

    /// <summary>
    /// Obtiene una retrospectiva directamente por su ID.
    /// </summary>
    [HttpGet("retrospectivas/{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        var response = await _retrospectivaService.ObtenerRetrospectivaPorIdAsync(id, usuarioId, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza la evaluación.
    /// </summary>
    [HttpPut("retrospectivas/{id:guid}")]
    public async Task<IActionResult> Actualizar(
        [FromRoute] Guid id, 
        [FromBody] ActualizarRetrospectivaDto request, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        await _retrospectivaService.ActualizarRetrospectivaAsync(id, usuarioId, request, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Elimina la evaluación.
    /// </summary>
    [HttpDelete("retrospectivas/{id:guid}")]
    public async Task<IActionResult> Eliminar(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        await _retrospectivaService.EliminarRetrospectivaAsync(id, usuarioId, cancellationToken);
        return NoContent();
    }
}