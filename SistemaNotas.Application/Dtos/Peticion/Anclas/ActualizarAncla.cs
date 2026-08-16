namespace SistemaNotas.Application.Dtos.Peticion.Anclas;

public record ActualizarAnclaDto
{
    public Guid SeccionId { get; init; }
    public Guid CategoriaId { get; init; }
    public int? Orden { get; init; }
    public string? ConceptoClave { get; init; }
    public bool? RecordatorioVisual { get; init; }
}
