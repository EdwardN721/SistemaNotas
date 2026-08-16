using SistemaNotas.Application.Dtos.Respuesta.Seccion;

namespace SistemaNotas.Application.Dtos.Respuesta.Presentacion;

public record PresentacionResponseDto
{
  public Guid Id { get; init; }
  public Guid UsuarioId { get; init; }
  public string Titulo { get; init; } = string.Empty;
  public string? Audiencia { get; init; }
  public DateTimeOffset? FechaExposicion { get; init; }
  public DateTimeOffset CreatedAt { get; init; }

  public IReadOnlyList<SeccionResponseDto> Secciones { get; init; } = new List<SeccionResponseDto>();
}

