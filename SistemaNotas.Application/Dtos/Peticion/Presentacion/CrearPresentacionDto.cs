namespace SistemaNotas.Application.Dtos.Peticion.Presentacion
{
  public record CrearPresentacionDto
  {
    public string Titulo { get; init; } = string.Empty;
    public string? Audiencia {  get; init; }
    public DateTimeOffset? FechaExposicion { get; init; }
  }
}
