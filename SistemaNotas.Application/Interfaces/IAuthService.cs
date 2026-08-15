using SistemaNotas.Application.Dtos.Peticion.Usuarios;
using SistemaNotas.Application.DTOs.Respuesta.Usuarios;

namespace SistemaNotas.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Registra un nuevo usuario en el sistema y genera un token de autenticación.
    /// </summary>
    /// <param name="request">La solicitud de registro del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    Task<AuthResponseDto> RegistrarAsync(RegistroRequestDto request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Autentica a un usuario en el sistema y genera un token de autenticación.
    /// </summary>
    /// <param name="request">La solicitud de inicio de sesión del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}