namespace SistemaNotas.Application.Dtos.Peticion.Categorias;

public record CrearCategoriaAnclaDto
{
    public string Nombre { get; init; } = string.Empty;
    public string? CodigoColor { get; init; }
}