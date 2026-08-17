using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Exceptions;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Retrospectivas;
using SistemaNotas.Application.Dtos.Respuesta.Retrospectivas;

namespace SistemaNotas.Application.Services;

public class RetrospectivaService : IRetrospectivaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RetrospectivaService> _logger;

    public RetrospectivaService(IUnitOfWork unitOfWork, ILogger<RetrospectivaService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RetrospectivaResponseDto> CrearRetrospectivaAsync(CrearRetrospectivaDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // 1. Validar que la Presentación exista y pertenezca al usuario
        var presentacion = await _unitOfWork.Presentaciones.FirstOrDefaultAsync(
            p => p.Id == dto.PresentacionId && p.UsuarioId == usuarioId, 
            true, cancellationToken);

        if (presentacion is null)
        {
            throw new NotFoundException("La presentación no existe o no tienes permisos.");
        }

        // 2. Validar que no exista ya una retrospectiva 
        bool existe = await _unitOfWork.Retrospectivas.AnyAsync(r => r.PresentacionId == dto.PresentacionId, cancellationToken);
        if (existe)
        {
            throw new BusinessRuleException("Esta presentación ya tiene una retrospectiva registrada.");
        }

        Retrospectiva nuevaRetrospectiva = dto.MapToEntity();

        await _unitOfWork.Retrospectivas.AddAsync(nuevaRetrospectiva, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Retrospectiva creada para la presentación {PresentacionId}.", dto.PresentacionId);

        return nuevaRetrospectiva.MapToDto();
    }

    public async Task<RetrospectivaResponseDto> ObtenerRetrospectivaPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Retrospectiva retrospectiva = await ObtenerYValidarRetrospectivaAsync(id, usuarioId, cancellationToken);
        return retrospectiva.MapToDto();
    }

    public async Task<RetrospectivaResponseDto?> ObtenerRetrospectivaPorPresentacionIdAsync(Guid presentacionId, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // Validar propiedad de la presentación
        bool esDuenio = await _unitOfWork.Presentaciones.AnyAsync(p => p.Id == presentacionId && p.UsuarioId == usuarioId, cancellationToken);
        if (!esDuenio)
        {
            throw new UnauthorizedAccessException("La presentación no existe o no tienes permisos.");
        }

        Retrospectiva? retrospectiva = await _unitOfWork.Retrospectivas.FirstOrDefaultAsync(r => r.PresentacionId == presentacionId, true, cancellationToken);
        
        return retrospectiva?.MapToDto(); 
    }

    public async Task ActualizarRetrospectivaAsync(Guid id, Guid usuarioId, ActualizarRetrospectivaDto dto, CancellationToken cancellationToken = default)
    {
        Retrospectiva retrospectiva = await ObtenerYValidarRetrospectivaAsync(id, usuarioId, cancellationToken);

        retrospectiva.UpdateEntity(dto);

        _unitOfWork.Retrospectivas.Update(retrospectiva);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Retrospectiva {Id} actualizada.", id);
    }

    public async Task EliminarRetrospectivaAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Retrospectiva retrospectiva = await ObtenerYValidarRetrospectivaAsync(id, usuarioId, cancellationToken);

        _unitOfWork.Retrospectivas.Delete(retrospectiva);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogWarning("Retrospectiva {Id} eliminada físicamente.", id);
    }

    #region MetodosPrivados

    private async Task<Retrospectiva> ObtenerYValidarRetrospectivaAsync(Guid retrospectivaId, Guid usuarioId, CancellationToken cancellationToken)
    {
        Retrospectiva? retrospectiva = await _unitOfWork.Retrospectivas.FirstOrDefaultAsync(r => r.Id == retrospectivaId, true, cancellationToken);
        
        if (retrospectiva is null)
            throw new NotFoundException("La retrospectiva no fue encontrada.");

        bool esDuenio = await _unitOfWork.Presentaciones.AnyAsync(p => p.Id == retrospectiva.PresentacionId && p.UsuarioId == usuarioId, cancellationToken);
        if (!esDuenio)
            throw new UnauthorizedAccessException("No tienes permisos para acceder a esta retrospectiva.");

        return retrospectiva;
    }

    #endregion
}