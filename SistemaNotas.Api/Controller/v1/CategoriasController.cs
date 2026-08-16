using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SistemaNotas.Api.Extensions; 
using Microsoft.AspNetCore.Authorization;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Categorias;

namespace SistemaNotas.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize] 
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaAnclaService _categoriaService;

    public CategoriasController(ICategoriaAnclaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    /// <summary>
    /// Obtiene solo las categorías activas
    /// </summary>
    [HttpGet("activas")]
    public async Task<IActionResult> ObtenerActivas(CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        var response = await _categoriaService.ObtenerCategoriasActivasAsync(usuarioId, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Obtiene el catálogo completo, incluyendo las inactivas
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas(CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        var response = await _categoriaService.ObtenerCategoriasAsync(usuarioId, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Crea una nueva categoría en el catálogo.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCategoriaAnclaDto request, CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        var response = await _categoriaService.CrearCategoriaAsync(request, usuarioId, cancellationToken);
        
        return Created(string.Empty, response);
    }

    /// <summary>
    /// Actualiza los datos de una categoría.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        [FromRoute] Guid id, 
        [FromBody] ActualizarCategoriaDto request, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        await _categoriaService.ActualizarCategoriaAsync(usuarioId, id, request, cancellationToken);
        
        return NoContent();
    }

    /// <summary>
    /// Deshabilita (Soft Delete) una categoría para que ya no salga en las opciones.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        Guid usuarioId = User.GetUsuarioId();
        await _categoriaService.EliminarCategoriaAsync(usuarioId, id, cancellationToken);
        
        return NoContent();
    }
}