namespace SistemaNotas.Application.Dtos.Peticion.Retrospectivas;

public record CrearRetrospectivaDto
{
    public Guid PresentacionId { get; init; }
    public int NivelNerviosismo { get; init; } 
    public List<string>? MuletillasDetectadas { get; init; }
    public string? QueSalioBien { get; init; }
}

