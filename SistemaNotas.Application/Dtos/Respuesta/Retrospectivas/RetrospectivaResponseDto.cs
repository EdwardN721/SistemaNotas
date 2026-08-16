namespace SistemaNotas.Application.Dtos.Respuesta.Retrospectivas;

public record RetrospectivaResponseDto
{
    public Guid Id { get; init; }
    public Guid PresentacionId { get; init; }
    public int NivelNerviosismo { get; init; }
    public List<string>? MuletillasDetectadas { get; init; }
    public string? QueSalioBien { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}