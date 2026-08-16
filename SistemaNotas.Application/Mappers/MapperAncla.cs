using SistemaNotas.Domain.Entities;
using SistemaNotas.Application.Dtos.Peticion.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Application.Mappers;

public static class MapperAncla
{
    public static Ancla MapToEntity(this CrearAnclaDto dto)
    {
        return new Ancla
        {
            Id = Guid.NewGuid(),
            SeccionId = dto.SeccionId,
            CategoriaId = dto.CategoriaId,
            Orden = dto.Orden,
            ConceptoClave = dto.ConceptoClave,
            RecordatorioVisual = dto.RecordatorioVisual
        };
    }

    public static void UpdateEntity(this Ancla ancla, ActualizarAnclaDto actualizarAnclaDto)
    {
        if (actualizarAnclaDto.CategoriaId != Guid.Empty)
        {
            ancla.CategoriaId = actualizarAnclaDto.CategoriaId;
        }

        if (actualizarAnclaDto.Orden.HasValue)
        {
            ancla.Orden = actualizarAnclaDto.Orden.Value;
        }

        ancla.ConceptoClave = actualizarAnclaDto.ConceptoClave ?? ancla.ConceptoClave;

        ancla.RecordatorioVisual = actualizarAnclaDto.RecordatorioVisual ?? ancla.RecordatorioVisual;
    }

    public static AnclaResponseDto MapToDto(this Ancla ancla)
    {
        return new AnclaResponseDto
        {
            Id = ancla.Id,
            SeccionId = ancla.SeccionId,
            CategoriaId = ancla.CategoriaId,
            Orden = ancla.Orden,
            ConceptoClave = ancla.ConceptoClave,
            RecordatorioVisual = ancla.RecordatorioVisual
        };
    }

    public static IReadOnlyList<AnclaResponseDto> MapToDto(this IEnumerable<Ancla> anclas)
    {
        return anclas?.Select(MapToDto).ToList()
            ?? new List<AnclaResponseDto>();
    }
}