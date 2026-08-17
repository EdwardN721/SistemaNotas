using SistemaNotas.Domain.Entities;
using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Domain.Exceptions;
using SistemaNotas.Application.Mappers; 
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Categorias;
using SistemaNotas.Application.Dtos.Respuesta.Categorias;

namespace SistemaNotas.Application.Services;

public class CategoriaAnclaService : ICategoriaAnclaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoriaAnclaService> _logger;

    public CategoriaAnclaService(IUnitOfWork unitOfWork, ILogger<CategoriaAnclaService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IReadOnlyList<CategoriaAnclaResponseDto>> ObtenerCategoriasActivasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Usuario {UsuarioId} consultando categorías activas.", usuarioId);

        var categorias = await _unitOfWork.CategoriasAncla.GetAsync(c => c.Activo, true, cancellationToken);
        
        return categorias.Select(c => c.MapToDto()).ToList();
    }

    public async Task<CategoriaAnclaResponseDto> CrearCategoriaAsync(CrearCategoriaAnclaDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Usuario {UsuarioId} intentando crear la categoría {Nombre}.", usuarioId, dto.Nombre);

        bool existe = await _unitOfWork.CategoriasAncla.AnyAsync(c => c.Nombre.ToLower() == dto.Nombre.ToLower(), cancellationToken);
        if (existe)
        {
            throw new BusinessRuleException($"Ya existe una categoría con el nombre '{dto.Nombre}'.");
        }

        CategoriaAncla nuevaCategoria = dto.MapToEntity();
        
        await _unitOfWork.CategoriasAncla.AddAsync(nuevaCategoria, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Categoría '{Nombre}' creada con éxito por el usuario {UsuarioId}.", nuevaCategoria.Nombre, usuarioId);

        return nuevaCategoria.MapToDto();
    }

    public async Task<IReadOnlyList<CategoriaAnclaResponseDto>> ObtenerCategoriasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Usuario {UsuarioId} consultando todo el catálogo de categorías.", usuarioId);

        var categorias = await _unitOfWork.CategoriasAncla.GetAllAsync(true, cancellationToken);
        
        return categorias.Select(c => c.MapToDto()).ToList();
    }

    public async Task ActualizarCategoriaAsync(Guid usuarioId, Guid categoriaId, ActualizarCategoriaDto actualizarCategoriaDto, CancellationToken cancellationToken = default)
    {
        CategoriaAncla categoria = await ObtenerYValidarCategoriaAsync(categoriaId, cancellationToken);

        categoria.UpdateEntity(actualizarCategoriaDto);

        _unitOfWork.CategoriasAncla.Update(categoria);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Categoría {CategoriaId} actualizada por el usuario {UsuarioId}.", categoriaId, usuarioId);
    }

    public async Task EliminarCategoriaAsync(Guid usuarioId, Guid categoriaId, CancellationToken cancellationToken = default)
    {
        CategoriaAncla categoria = await ObtenerYValidarCategoriaAsync(categoriaId, cancellationToken);

        categoria.Activo = false;
        
        _unitOfWork.CategoriasAncla.Update(categoria);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogWarning("Categoría {CategoriaId} deshabilitada por el usuario {UsuarioId}.", categoriaId, usuarioId);
    }

    #region MetodosPrivados

    private async Task<CategoriaAncla> ObtenerYValidarCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken)
    {
        CategoriaAncla? categoria = await _unitOfWork.CategoriasAncla.FirstOrDefaultAsync(c => c.Id == categoriaId, true, cancellationToken);
        
        if (categoria is null)
        {
            _logger.LogWarning("La categoría con ID {CategoriaId} no existe.", categoriaId);
            throw new NotFoundException($"La categoría con ID {categoriaId} no fue encontrada.");
        }

        return categoria;
    }

    #endregion
}