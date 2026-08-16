using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaNotas.Api.Extensions;
using SistemaNotas.Application.Dtos.Peticion.Seccion;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;
using SistemaNotas.Application.DTOs;
using SistemaNotas.Application.Interfaces;

namespace SistemaNotas.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/presentaciones/{presentacionId:guid}/[controller]")]
[Authorize] 
public class SeccionesController : ControllerBase
{
    private readonly ISeccionService _seccionService;

    public SeccionesController(ISeccionService seccionService)
    {
        _seccionService = seccionService;
    }

    /// <summary>
    /// Crea una nueva sección asociada a una presentación específica. La presentación debe pertenecer al usuario autenticado.
    /// </summary>
    /// <param name="presentacionId">El ID de la presentación a la que se asociará la nueva sección.</param>
    /// <param name="request">Los datos para crear la nueva sección.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>La sección creada.</returns>
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromRoute] Guid presentacionId, 
        [FromBody] CrearSeccionDto request, 
        CancellationToken cancellationToken)
    {
        // 1. Extraemos el usuario de forma segura
        Guid usuarioId = User.GetUsuarioId();

        // 2. Aseguramos que el ID de la ruta coincida con el DTO por consistencia
        var dtoConIdCorrecto = request with { PresentacionId = presentacionId };

        // 3. Ejecutamos la lógica de negocio
        SeccionResponseDto response = await _seccionService.CrearSeccionAsync(dtoConIdCorrecto, usuarioId, cancellationToken);
        
        return Created(string.Empty, response);
    }


    /// <summary>
    /// Obtiene todas las secciones asociadas a una presentación específica. La presentación debe pertenecer al usuario autenticado.
    /// </summary>
    /// <param name="presentacionId">El ID de la presentación de la que se obtendrán las secciones.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Una lista de las secciones asociadas a la presentación.</returns>
    [HttpGet]
    public async Task<IActionResult> ObtenerPorPresentacion(
        [FromRoute] Guid presentacionId, 
        CancellationToken cancellationToken)
    {
        // 1. Extraemos el usuario de forma segura
        Guid usuarioId = User.GetUsuarioId();

        // 2. Traemos las secciones (el servicio ya valida que la presentación sea suya)
        var response = await _seccionService.ObtenerPorPresentacionIdAsync(presentacionId, usuarioId, cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Obtiene una sección específica por su ID. La sección debe pertenecer a una presentación del usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la sección a obtener.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>La sección solicitada.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        // 1. Extraemos el usuario de forma segura
        Guid usuarioId = User.GetUsuarioId();

        // 2. Traemos la sección (el servicio ya valida que la presentación sea suya)
        var response = await _seccionService.ObtenerSeccionPorIdAsync(id, usuarioId, cancellationToken);
        
        return Ok(response);
    }

    /// <summary>
    /// Actualiza una sección específica por su ID. La sección debe pertenecer a una presentación del usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la sección a actualizar.</param>
    /// <param name="request">Los datos para actualizar la sección.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        [FromRoute] Guid id,
        [FromBody] ActualizarSeccionDto request,
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        await _seccionService.ActualizarSeccionAsync(id, usuarioId, request, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Elimina una sección específica por su ID. La sección debe pertenecer a una presentación del usuario autenticado.
    /// </summary>
    /// <param name="id">El ID de la sección a eliminar.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();

        await _seccionService.EliminarSeccionAsync(id, usuarioId, cancellationToken);

        return NoContent();
    }
}