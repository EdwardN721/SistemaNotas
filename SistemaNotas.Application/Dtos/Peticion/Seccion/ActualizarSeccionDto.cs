namespace SistemaNotas.Application.Dtos.Peticion.Seccion;

public record ActualizarSeccionDto
{
    public Guid PresentacionId { get; init; }
    public int? Orden { get; init; }
    public string? TituloSeccion { get; init; } = string.Empty;
    public int? MinutosEstimados { get; init; }
}
