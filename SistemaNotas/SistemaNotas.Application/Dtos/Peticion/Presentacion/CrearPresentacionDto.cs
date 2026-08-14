namespace SistemaNotas.Application.Dtos.Peticion.Presentacion
{
  public record CrearPresentacionDto
  {
    public string Titulo { get; init; } = string.Empty;
    public string? Audicencia {  get; init; }
    public DateTime? FechaExposicion { get; init; }
  }
}
