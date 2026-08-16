namespace SistemaNotas.Application.Dtos.Respuesta.Anclas;


public record AnclaResponseDto
{
    public Guid Id { get; init; }
    public Guid SeccionId { get; init; }
    public Guid CategoriaId { get; init; }
    public int Orden { get; init; }
    public string ConceptoClave { get; init; } = string.Empty;
    public bool RecordatorioVisual { get; init; }
}