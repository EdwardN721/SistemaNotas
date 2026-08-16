using Microsoft.Extensions.Logging;
using SistemaNotas.Application.Dtos.Peticion.Seccion;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;
using SistemaNotas.Application.DTOs;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Interfaces;

namespace SistemaNotas.Application.Services;

public class SeccionService : ISeccionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SeccionService> _logger;

    public SeccionService(IUnitOfWork unitOfWork, ILogger<SeccionService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<SeccionResponseDto> CrearSeccionAsync(CrearSeccionDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == dto.PresentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
        {
            _logger.LogWarning("Intento no autorizado de crear sección. Presentacion: {Id}, Usuario: {UsuarioId}", dto.PresentacionId, usuarioId);
            throw new KeyNotFoundException("La presentación no existe o no tienes permisos para modificarla.");
        }

        Seccion nuevaSeccion = dto.MapToEntity();

        await _unitOfWork.Secciones.AddAsync(nuevaSeccion, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Sección '{Titulo}' creada con éxito.", nuevaSeccion.TituloSeccion);

        return nuevaSeccion.MapToDto();
    }

    public async Task<IReadOnlyList<SeccionResponseDto>> ObtenerPorPresentacionIdAsync(Guid presentacionId, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == presentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new KeyNotFoundException("La presentación no existe o no tienes permisos para verla.");

        // Obtenemos las secciones (Aún sin Anclas, lo agregaremos en el siguiente paso)
        var secciones = await _unitOfWork.Secciones.GetAsync(s => s.PresentacionId == presentacionId, cancellationToken);

        return secciones.MapToDto();
    }

    public async Task<SeccionResponseDto> ObtenerSeccionPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion seccion = await ObtenerSeccion(id, usuarioId, cancellationToken);

        return seccion.MapToDto();
    }

    public async Task ActualizarSeccionAsync(Guid id, Guid usuarioId, ActualizarSeccionDto dto, CancellationToken cancellationToken = default)
    {
        Seccion seccion = await ObtenerSeccion(id, usuarioId, cancellationToken);

        seccion.UpdateEntity(dto);

        _unitOfWork.Secciones.Update(seccion);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("Sección '{Titulo}' actualizada con éxito.", seccion.TituloSeccion);
    }

    public async Task EliminarSeccionAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion seccion = await ObtenerSeccion(id, usuarioId, cancellationToken);

        _unitOfWork.Secciones.Delete(seccion);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogWarning("Sección '{Titulo}' eliminada.", seccion.TituloSeccion);
    }

    #region MetodosPrivados

    private async Task<Seccion> ObtenerSeccion(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion? seccion = await _unitOfWork.Secciones.GetByIdAsync(id, cancellationToken);
        if (seccion is null || !(await _unitOfWork.Presentaciones.AnyAsync(p => p.Id == seccion.PresentacionId && p.UsuarioId == usuarioId, cancellationToken)))
        {
            _logger.LogWarning("La sección no existe o no tienes permisos para verla: {Id}", id);
            throw new KeyNotFoundException("La sección no existe o no tienes permisos para verla.");
        }

        return seccion;
    }

    #endregion
}