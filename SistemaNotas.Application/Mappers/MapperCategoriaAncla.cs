using SistemaNotas.Domain.Entities;
using SistemaNotas.Application.Dtos.Peticion.Categorias;
using SistemaNotas.Application.Dtos.Respuesta.Categorias;

namespace SistemaNotas.Application.Mappers;

public static class MapperCategoriaAncla
{
    public static CategoriaAncla MapToEntity(this CrearCategoriaAnclaDto dto)
    {
        return new CategoriaAncla
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            CodigoColor = dto.CodigoColor,
            Activo = true 
        };
    }

    public static void UpdateEntity(this CategoriaAncla categoriaAncla, ActualizarCategoriaDto actualizarAnclaDto)
    {
        if (!string.IsNullOrWhiteSpace(actualizarAnclaDto.Nombre))
        {
            categoriaAncla.Nombre = actualizarAnclaDto.Nombre;
        }

        if (!string.IsNullOrWhiteSpace(actualizarAnclaDto.CodigoColor))
        {
            categoriaAncla.CodigoColor = actualizarAnclaDto.CodigoColor;
        }
    }

    public static CategoriaAnclaResponseDto MapToDto(this CategoriaAncla categoria)
    {
        return new CategoriaAnclaResponseDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            CodigoColor = categoria.CodigoColor,
            Activo = categoria.Activo
        };
    }

    public static IReadOnlyList<CategoriaAnclaResponseDto> MapToDto(this IEnumerable<CategoriaAncla> categoriasAnclas)
    {
        return categoriasAnclas.Select(MapToDto).ToList()
            ?? new List<CategoriaAnclaResponseDto>();
    }
}