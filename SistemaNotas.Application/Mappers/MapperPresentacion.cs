using SistemaNotas.Domain.Entities;
using SistemaNotas.Application.Dtos.Peticion.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;

namespace SistemaNotas.Application.Mappers
{
  public static class MapperPresentacion
  {
    public static Presentacion MapToEntity(this CrearPresentacionDto dto, Guid usuarioId)
    {
      return new Presentacion
      {
        Id = Guid.NewGuid(),
        UsuarioId = usuarioId,
        Titulo = dto.Titulo,
        FechaExposicion = dto.FechaExposicion,
        Audiencia = dto.Audiencia
      };
    }

    public static void UpdateEntity(this Presentacion presentacion, ActualizarPresentacionDto actualizarDto)
    {
      if (!string.IsNullOrWhiteSpace(actualizarDto.Titulo))
      {
        presentacion.Titulo = actualizarDto.Titulo;
      }

      presentacion.Audiencia = actualizarDto.Audiencia;

      presentacion.FechaExposicion = actualizarDto.FechaExposicion;
    }

    public static PresentacionResponseDto MapToDto(this Presentacion presentacion)
    {
      return new PresentacionResponseDto
      {
        Id = presentacion.Id,
        UsuarioId = presentacion.UsuarioId,
        Titulo = presentacion.Titulo,
        Audiencia = presentacion.Audiencia,
        FechaExposicion = presentacion.FechaExposicion,
        CreatedAt = presentacion.CreatedAt,

        Secciones = presentacion.Secciones != null 
                        ? presentacion.Secciones.Select(s => s.MapToDto()).OrderBy(s => s.Orden).ToList() 
                        : new List<SeccionResponseDto>()
      };
    }

    public static IReadOnlyList<PresentacionResponseDto> MapToDto(this IEnumerable<Presentacion>? presentaciones)
    {
      return presentaciones?.Select(MapToDto).ToList() ??
        new List<PresentacionResponseDto>();
    }
  }
}
