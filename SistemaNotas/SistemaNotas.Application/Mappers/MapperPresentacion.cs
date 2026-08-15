using SistemaNotas.Domain.Entities;
using SistemaNotas.Application.Dtos.Peticion.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Presentacion;

namespace SistemaNotas.Application.Mappers
{
  public static class MapperPresentacion
  {
    public static Presentacion MapToEntity(this CrearPresentacionDto dto)
    {
      return new Presentacion
      {
        Id = Guid.NewGuid(),
        Titulo = dto.Titulo,
        FechaExposicion = dto.FechaExposicion,
        Audiencia = dto.Audicencia
      };
    }

    public static void UpdateEntity(this Presentacion presentacion, ActualizarPresentacionDto actualizarDto)
    {
      presentacion.Titulo = actualizarDto.Titulo;
      presentacion.Audiencia = actualizarDto.Audicencia;
      presentacion.FechaExposicion = actualizarDto.FechaExposicion;
    }

    public static PresentacionResponseDto MapToDto(this Presentacion presentacion)
    {
      return new PresentacionResponseDto
      {
        Id = presentacion.Id,
        Titulo = presentacion.Titulo,
        Audiencia = presentacion.Audiencia,
        FechaExposicion = presentacion.FechaExposicion,
        CreatedAt = presentacion.CreatedAt,
      };
    }

    public static IReadOnlyList<PresentacionResponseDto> MapToDto(this IEnumerable<Presentacion>? presentaciones)
    {
      return presentaciones?.Select(MapToDto).ToList() ?? 
        new List<PresentacionResponseDto>();
    }
  }
}
