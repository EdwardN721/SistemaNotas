namespace SistemaNotas.Application.Dtos.Peticion.Anclas;

public record CrearAnclaDto
{
    public Guid SeccionId { get; init; }
    public Guid CategoriaId { get; init; }
    public int Orden { get; init; }
    public string ConceptoClave { get; init; } = string.Empty;
    public bool RecordatorioVisual { get; init; }
}
