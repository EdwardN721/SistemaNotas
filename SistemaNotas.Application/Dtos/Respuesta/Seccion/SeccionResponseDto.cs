using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Application.Dtos.Respuesta.Seccion;

public record SeccionResponseDto
{
    public Guid Id { get; init; }
    public Guid PresentacionId { get; init; }
    public int Orden { get; init; }
    public string TituloSeccion { get; init; } = string.Empty;
    public int? MinutosEstimados { get; init; }
    
    public IReadOnlyList<AnclaResponseDto> Anclas { get; init; } = new List<AnclaResponseDto>();
}