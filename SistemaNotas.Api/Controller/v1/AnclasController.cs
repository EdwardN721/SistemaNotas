using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SistemaNotas.Api.Extensions; 
using Microsoft.AspNetCore.Authorization;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/secciones/")]
[Authorize]
public class AnclasController : ControllerBase
{
    private readonly IAnclaService _anclaService;

    public AnclasController(IAnclaService anclaService)
    {
        _anclaService = anclaService;
    }

    /// <summary>
    /// Crea una nueva ancla dentro de una sección específica.
    /// </summary>
    [HttpPost("{seccionId:guid}/anclas")]
    public async Task<IActionResult> Crear(
        [FromRoute] Guid seccionId, 
        [FromBody] CrearAnclaDto request, 
        CancellationToken cancellationToken)
    {
        // 1. Extraemos de forma segura el ID del token JWT
        Guid usuarioId = User.GetUsuarioId();

        // 2. Sobrescribimos el SeccionId del DTO con el de la URL por consistencia
        var dtoConIdCorrecto = request with { SeccionId = seccionId };

        // 3. Ejecutamos el servicio
        AnclaResponseDto response = await _anclaService.CrearAnclaAsync(dtoConIdCorrecto, usuarioId, cancellationToken);
        
        // 4. Retornamos 201 Created apuntando a la ruta de "ObtenerPorId"
        return CreatedAtAction(nameof(ObtenerPorId), new { id = response.Id, version = "1.0" }, response);
    }

    /// <summary>
    /// Obtiene todas las anclas de una sección.
    /// </summary>
    [HttpGet("{seccionId:guid}/anclas")]
    public async Task<IActionResult> ObtenerPorSeccion(
        [FromRoute] Guid seccionId, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        var response = await _anclaService.ObtenerPorSeccionIdAsync(seccionId, usuarioId, cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Obtiene un ancla específica por su ID único.
    /// </summary>
    [HttpGet("anclas/{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        AnclaResponseDto response = await _anclaService.ObtenerAnclaPorIdAsync(id, usuarioId, cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Actualiza los datos de un ancla existente.
    /// </summary>
    [HttpPut("anclas/{id:guid}")]
    public async Task<IActionResult> Actualizar(
        [FromRoute] Guid id, 
        [FromBody] ActualizarAnclaDto request, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        await _anclaService.ActualizarAnclaAsync(id, usuarioId, request, cancellationToken);
        
        // 204 No Content es el estándar al actualizar sin devolver cuerpo
        return NoContent(); 
    }

    /// <summary>
    /// Elimina un ancla de la base de datos.
    /// </summary>
    [HttpDelete("anclas/{id:guid}")]
    public async Task<IActionResult> Eliminar(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        await _anclaService.EliminarAnclaAsync(id, usuarioId, cancellationToken);
        
        return NoContent();
    }
}