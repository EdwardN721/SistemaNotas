namespace SistemaNotas.Application.Dtos.Respuesta.Categorias;

public record CategoriaAnclaResponseDto
{
    public Guid Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? CodigoColor { get; init; }
    public bool Activo { get; init; }
}