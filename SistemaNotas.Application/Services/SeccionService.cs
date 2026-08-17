using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Seccion;
using SistemaNotas.Application.Dtos.Respuesta.Seccion;

namespace SistemaNotas.Application.Services;

// Uso de Constructores Primarios (C# 12+)
public class SeccionService(IUnitOfWork unitOfWork, ILogger<SeccionService> logger) : ISeccionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<SeccionService> _logger = logger;

    public async Task<SeccionResponseDto> CrearSeccionAsync(CrearSeccionDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == dto.PresentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new KeyNotFoundException("La presentación no existe o no tienes permisos.");

        int siguienteOrden = dto.Orden;
        var seccionesExistentes = await _unitOfWork.Secciones.GetAsync(s => s.PresentacionId == dto.PresentacionId, true, cancellationToken);
        
        if (dto.Orden <= 0 || seccionesExistentes.Any(s => s.Orden == dto.Orden))
        {
            siguienteOrden = seccionesExistentes.Any() ? seccionesExistentes.Max(s => s.Orden) + 1 : 1;
        }

        Seccion nuevaSeccion = dto.MapToEntity();
        nuevaSeccion.Orden = siguienteOrden;

        await _unitOfWork.Secciones.AddAsync(nuevaSeccion, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        await ReordenarSeccionesAsync(dto.PresentacionId, cancellationToken);

        _logger.LogInformation("Sección '{Titulo}' creada con éxito en el orden {Orden}.", nuevaSeccion.TituloSeccion, siguienteOrden);

        return nuevaSeccion.MapToDto();
    }

    public async Task<IReadOnlyList<SeccionResponseDto>> ObtenerPorPresentacionIdAsync(Guid presentacionId, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        bool tienePermiso = await _unitOfWork.Presentaciones.AnyAsync(
            p => p.Id == presentacionId && p.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new KeyNotFoundException("No tienes permisos para verla.");

        // Se añade el Include para traer las Anclas anidadas
        var secciones = await _unitOfWork.Secciones.GetAsync(
            s => s.PresentacionId == presentacionId, 
            true,
            cancellationToken,
            "Anclas"
        );

        return secciones.OrderBy(s => s.Orden).MapToDto();
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

        await ReordenarSeccionesAsync(seccion.PresentacionId, cancellationToken);

        _logger.LogInformation("Sección '{Titulo}' actualizada con éxito.", seccion.TituloSeccion);
    }

    public async Task EliminarSeccionAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion seccion = await ObtenerSeccion(id, usuarioId, cancellationToken);
        Guid presentacionId = seccion.PresentacionId;

        _unitOfWork.Secciones.Delete(seccion);
        await _unitOfWork.CommitAsync(cancellationToken);

        await ReordenarSeccionesAsync(presentacionId, cancellationToken);

        _logger.LogWarning("Sección '{Titulo}' eliminada y orden recalculado.", seccion.TituloSeccion);
    }

    #region MetodosPrivados

    private async Task<Seccion> ObtenerSeccion(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion? seccion = await _unitOfWork.Secciones.FirstOrDefaultAsync(
            s => s.Id == id && s.Presentacion != null && s.Presentacion.UsuarioId == usuarioId, 
            true,
            cancellationToken,
            "Presentacion" 
        );

        if (seccion is null)
            throw new KeyNotFoundException("La sección no existe o no tienes permisos.");

        return seccion;
    }

    // Método utilitario para mantener el "Orden" limpio y secuencial 
    private async Task ReordenarSeccionesAsync(Guid presentacionId, CancellationToken cancellationToken)
    {
        var secciones = await _unitOfWork.Secciones.GetAsync(s => s.PresentacionId == presentacionId, true, cancellationToken);
        var seccionesOrdenadas = secciones.OrderBy(s => s.Orden).ToList();

        for (int i = 0; i < seccionesOrdenadas.Count; i++)
        {
            seccionesOrdenadas[i].Orden = i + 1; 
            _unitOfWork.Secciones.Update(seccionesOrdenadas[i]);
        }

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    #endregion
}