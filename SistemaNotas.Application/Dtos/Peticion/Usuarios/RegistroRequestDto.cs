namespace SistemaNotas.Application.Dtos.Peticion.Usuarios;
public record RegistroRequestDto
{
    public string Nombre { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}