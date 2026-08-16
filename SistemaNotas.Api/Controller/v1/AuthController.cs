using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.DTOs.Respuesta.Usuarios;
using SistemaNotas.Application.Dtos.Peticion.Usuarios;

namespace SistemaNotas.Api.Controllers.v1;

/// <summary>
/// Controlador para la autenticación de usuarios, incluyendo registro e inicio de sesión.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    
    /// <summary>
    /// Registra un nuevo usuario y genera un token JWT automáticamente.
    /// </summary>
    /// <param name="request">Información del usuario a registrar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Información del usuario registrado y el token JWT</returns>
    [HttpPost("registrar")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar([FromBody] RegistroRequestDto request, CancellationToken cancellationToken)
    {
        AuthResponseDto response = await _authService.RegistrarAsync(request, cancellationToken);
        
        // Retornamos 201 Created (El estándar cuando se crea un recurso)
        return CreatedAtAction(nameof(Registrar), new { id = response.Nombre }, response);
    }

    /// <summary>
    /// Inicia sesión con un usuario existente y genera un token JWT.
    /// </summary>
    /// <param name="request">Información del usuario para iniciar sesión</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Información del usuario autenticado y el token JWT</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        AuthResponseDto response = await _authService.LoginAsync(request, cancellationToken);
        
        return Ok(response);
    }
}