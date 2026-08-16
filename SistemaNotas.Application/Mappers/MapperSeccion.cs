using SistemaNotas.Application.Dtos.Peticion.Seccion;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Application.Mappers;

public static class MapperSeccion
{
    public static Seccion MapToEntity(this CrearSeccionDto dto)
    {
        return new Seccion
        {
            Id = Guid.NewGuid(),
            PresentacionId = dto.PresentacionId,
            Orden = dto.Orden,
            TituloSeccion = dto.TituloSeccion,
            MinutosEstimados = dto.MinutosEstimados
        };
    }

    public static void UpdateEntity(this Seccion seccion, ActualizarSeccionDto dto)
    {
        if (dto.PresentacionId != Guid.Empty)
        {
            seccion.PresentacionId = dto.PresentacionId;
        }

        if (!string.IsNullOrWhiteSpace(dto.TituloSeccion))
        {
            seccion.TituloSeccion = dto.TituloSeccion;
        }

        if (dto.Orden.HasValue)
        {
            seccion.Orden = dto.Orden.Value;
        }

        seccion.MinutosEstimados = dto.MinutosEstimados;
    }

    public static SeccionResponseDto MapToDto(this Seccion seccion)
    {
        return new SeccionResponseDto
        {
            Id = seccion.Id,
            PresentacionId = seccion.PresentacionId,
            Orden = seccion.Orden,
            TituloSeccion = seccion.TituloSeccion,
            MinutosEstimados = seccion.MinutosEstimados,
            Anclas = seccion.Anclas != null
                     ? seccion.Anclas.Select(a => a.MapToDto()).ToList()
                     : new List<AnclaResponseDto>()
        };
    }

    public static IReadOnlyList<SeccionResponseDto> MapToDto(this IEnumerable<Seccion> secciones)
    {
        return secciones?.Select(MapToDto).ToList()
            ?? new List<SeccionResponseDto>();
    }
}