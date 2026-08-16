using SistemaNotas.Domain.Entities;
using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Exceptions;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Application.Services;

public class AnclaService : IAnclaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AnclaService> _logger;

    public AnclaService(IUnitOfWork unitOfWork, ILogger<AnclaService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AnclaResponseDto> CrearAnclaAsync(CrearAnclaDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // 1. Obtener la sección para saber a qué presentación pertenece
        Seccion? seccion = await _unitOfWork.Secciones.FirstOrDefaultAsync(s => s.Id == dto.SeccionId, cancellationToken);
        
        if (seccion is null)
        {
            _logger.LogWarning("Sección no encontrada: {SeccionId}", dto.SeccionId);
            throw new NotFoundException("La sección indicada no existe.");
        }

        // 2. Validar que la presentación de esta sección pertenezca al usuario
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == seccion.PresentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
        {
            _logger.LogWarning("Intento no autorizado de crear ancla. Seccion: {SeccionId}, Usuario: {UsuarioId}", dto.SeccionId, usuarioId);
            throw new NotFoundException("No existe la presentación.");
        }

        // 3. Mapear y guardar el Ancla
        Ancla nuevaAncla = dto.MapToEntity();

        await _unitOfWork.Anclas.AddAsync(nuevaAncla, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Ancla creada con éxito en la sección {SeccionId}.", dto.SeccionId);

        return nuevaAncla.MapToDto();
    }

    public async Task<IReadOnlyList<AnclaResponseDto>> ObtenerPorSeccionIdAsync(Guid seccionId, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // 1. Validar la sección y sus permisos
        Seccion? seccion = await _unitOfWork.Secciones.FirstOrDefaultAsync(s => s.Id == seccionId, cancellationToken);
        
        if (seccion is null)
            throw new NotFoundException("La sección indicada no existe.");

        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == seccion.PresentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new NotFoundException("No tienes permisos para acceder a esta sección.");

        // 2. Traer las anclas asociadas
        var anclas = await _unitOfWork.Anclas.GetAsync(a => a.SeccionId == seccionId, cancellationToken);

        // Mapear la lista de anclas a DTOs
        return anclas.Select(a => a.MapToDto()).ToList();
    }

    public async Task<AnclaResponseDto> ObtenerAnclaPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);
        return ancla.MapToDto();
    }

    public async Task ActualizarAnclaAsync(Guid id, Guid usuarioId, ActualizarAnclaDto actualizarDto, CancellationToken cancellationToken = default)
    {
        // 1. Buscamos y validamos que le pertenezca al usuario
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);

        // 2. Actualizamos las propiedades
        
        ancla.UpdateEntity(actualizarDto);

        // 3. Guardar cambios
        _unitOfWork.Anclas.Update(ancla);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Ancla {Id} actualizada correctamente.", id);
    }

    public async Task EliminarAnclaAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // 1. Buscamos y validamos
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);

        _unitOfWork.Anclas.Delete(ancla);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogWarning("Ancla {Id} eliminada.", id);
    }

    #region MetodosPrivados
    
    private async Task<Ancla> ObtenerYValidarAnclaAsync(Guid anclaId, Guid usuarioId, CancellationToken cancellationToken)
    {
        // 1. Buscar el ancla
        Ancla? ancla = await _unitOfWork.Anclas.FirstOrDefaultAsync(a => a.Id == anclaId, cancellationToken);
        
        if (ancla is null)
            throw new NotFoundException($"El ancla con ID {anclaId} no existe.");

        // 2. Buscar a qué sección pertenece
        Seccion? seccion = await _unitOfWork.Secciones.FirstOrDefaultAsync(s => s.Id == ancla.SeccionId, cancellationToken);
        
        if (seccion is null)
            throw new NotFoundException("La sección asociada al ancla no existe.");

        // 3. Validar si la presentación le pertenece al usuario
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == seccion.PresentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new NotFoundException("No tienes permisos para acceder a esta ancla.");

        return ancla;
    }

    #endregion
}