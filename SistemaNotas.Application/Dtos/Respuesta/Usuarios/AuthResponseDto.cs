namespace SistemaNotas.Application.DTOs.Respuesta.Usuarios;

public record AuthResponseDto
{
    public string Token { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}