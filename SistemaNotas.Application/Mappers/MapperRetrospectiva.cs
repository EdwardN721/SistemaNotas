using SistemaNotas.Application.Dtos.Peticion.Retrospectivas;
using SistemaNotas.Application.Dtos.Respuesta.Retrospectivas;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Application.Mappers;

public static class MapperRetrospectiva
{
    public static Retrospectiva MapToEntity(this CrearRetrospectivaDto dto)
    {
        return new Retrospectiva
        {
            Id = Guid.NewGuid(),
            PresentacionId = dto.PresentacionId,
            NivelNerviosismo = dto.NivelNerviosismo,
            MuletillasDetectadas = dto.MuletillasDetectadas ?? new List<string>(),
            QueSalioBien = dto.QueSalioBien
        };
    }

    public static void UpdateEntity(this Retrospectiva retrospectiva, ActualizarRetrospectivaDto dto)
    {
        if (dto.NivelNerviosismo.HasValue)
        {
            retrospectiva.NivelNerviosismo = dto.NivelNerviosismo.Value;
        }

        if (dto.MuletillasDetectadas != null)
        {
            retrospectiva.MuletillasDetectadas = dto.MuletillasDetectadas;
        }

        retrospectiva.QueSalioBien = dto.QueSalioBien ?? retrospectiva.QueSalioBien;
    }

    public static RetrospectivaResponseDto MapToDto(this Retrospectiva retrospectiva)
    {
        return new RetrospectivaResponseDto
        {
            Id = retrospectiva.Id,
            PresentacionId = retrospectiva.PresentacionId,
            NivelNerviosismo = retrospectiva.NivelNerviosismo,
            MuletillasDetectadas = retrospectiva.MuletillasDetectadas,
            QueSalioBien = retrospectiva.QueSalioBien,
            CreatedAt = retrospectiva.CreatedAt
        };
    }
}