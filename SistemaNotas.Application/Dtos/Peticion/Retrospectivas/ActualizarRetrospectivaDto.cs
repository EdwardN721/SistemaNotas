namespace SistemaNotas.Application.Dtos.Peticion.Retrospectivas;

public record ActualizarRetrospectivaDto
{
    public int? NivelNerviosismo { get; init; }
    public List<string>? MuletillasDetectadas { get; init; }
    public string? QueSalioBien { get; init; }
}