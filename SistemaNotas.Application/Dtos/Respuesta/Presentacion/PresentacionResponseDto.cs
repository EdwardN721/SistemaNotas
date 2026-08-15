namespace SistemaNotas.Application.Dtos.Respuesta.Presentacion
{
  public record PresentacionResponseDto
  {
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Audiencia { get; set; }
    public DateTime? FechaExposicion { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
  }
}
