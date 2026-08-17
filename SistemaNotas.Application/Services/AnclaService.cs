using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Anclas;
using SistemaNotas.Application.Dtos.Respuesta.Anclas;

namespace SistemaNotas.Application.Services;

public class AnclaService(IUnitOfWork unitOfWork, ILogger<AnclaService> logger) : IAnclaService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<AnclaService> _logger = logger;

    public async Task<AnclaResponseDto> CrearAnclaAsync(CrearAnclaDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Seccion? seccion = await _unitOfWork.Secciones.FirstOrDefaultAsync(
            s => s.Id == dto.SeccionId && s.Presentacion != null && s.Presentacion.UsuarioId == usuarioId, 
            true, 
            cancellationToken,
            "Presentacion"
        );
        
        if (seccion is null)
        {
            _logger.LogWarning("Intento no autorizado o sección inexistente: {SeccionId}", dto.SeccionId);
            throw new KeyNotFoundException("La sección indicada no existe o no tienes permisos.");
        }

        int siguienteOrden = dto.Orden;
        var anclasExistentes = await _unitOfWork.Anclas.GetAsync(a => a.SeccionId == dto.SeccionId, true, cancellationToken);
        
        if (dto.Orden <= 0 || anclasExistentes.Any(a => a.Orden == dto.Orden))
        {
            siguienteOrden = anclasExistentes.Any() ? anclasExistentes.Max(a => a.Orden) + 1 : 1;
        }

        Ancla nuevaAncla = dto.MapToEntity();
        nuevaAncla.Orden = siguienteOrden;

        await _unitOfWork.Anclas.AddAsync(nuevaAncla, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        await ReordenarAnclasAsync(dto.SeccionId, cancellationToken);

        _logger.LogInformation("Ancla creada con éxito en la sección {SeccionId} con orden {Orden}.", dto.SeccionId, siguienteOrden);

        var anclaCompleta = await _unitOfWork.Anclas.FirstOrDefaultAsync(a => a.Id == nuevaAncla.Id, true, cancellationToken, "Categoria");
        return anclaCompleta!.MapToDto();
    }

    public async Task<IReadOnlyList<AnclaResponseDto>> ObtenerPorSeccionIdAsync(Guid seccionId, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        bool tienePermiso = await _unitOfWork.Secciones.AnyAsync(
            s => s.Id == seccionId && s.Presentacion != null && s.Presentacion.UsuarioId == usuarioId, 
            cancellationToken);

        if (!tienePermiso)
            throw new KeyNotFoundException("El elemento solicitado no éxiste.");

        var anclas = await _unitOfWork.Anclas.GetAsync(
            a => a.SeccionId == seccionId,
            true, 
            cancellationToken,
            "Categoria" 
        );

        return anclas.OrderBy(a => a.Orden).Select(a => a.MapToDto()).ToList();
    }

    public async Task<AnclaResponseDto> ObtenerAnclaPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);
        return ancla.MapToDto();
    }

    public async Task ActualizarAnclaAsync(Guid id, Guid usuarioId, ActualizarAnclaDto actualizarDto, CancellationToken cancellationToken = default)
    {
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);

        ancla.UpdateEntity(actualizarDto);

        _unitOfWork.Anclas.Update(ancla);
        await _unitOfWork.CommitAsync(cancellationToken);

        await ReordenarAnclasAsync(ancla.SeccionId, cancellationToken);

        _logger.LogInformation("Ancla {Id} actualizada correctamente.", id);
    }

    public async Task EliminarAnclaAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        Ancla ancla = await ObtenerYValidarAnclaAsync(id, usuarioId, cancellationToken);
        Guid seccionId = ancla.SeccionId;

        _unitOfWork.Anclas.Delete(ancla);
        await _unitOfWork.CommitAsync(cancellationToken);

        await ReordenarAnclasAsync(seccionId, cancellationToken);

        _logger.LogWarning("Ancla {Id} eliminada.", id);
    }

    #region MetodosPrivados
    
    private async Task<Ancla> ObtenerYValidarAnclaAsync(Guid anclaId, Guid usuarioId, CancellationToken cancellationToken)
    {
        Ancla? ancla = await _unitOfWork.Anclas.FirstOrDefaultAsync(
            a => a.Id == anclaId && a.Seccion != null && a.Seccion.Presentacion != null && a.Seccion.Presentacion.UsuarioId == usuarioId, 
            true,
            cancellationToken,
            "Seccion", "Seccion.Presentacion", "Categoria"
        );
        
        if (ancla is null)
            throw new KeyNotFoundException($"El ancla no existe o no tienes permisos.");

        return ancla;
    }

    private async Task ReordenarAnclasAsync(Guid seccionId, CancellationToken cancellationToken)
    {
        var anclas = await _unitOfWork.Anclas.GetAsync(a => a.SeccionId == seccionId, true, cancellationToken);
        var anclasOrdenadas = anclas.OrderBy(a => a.Orden).ToList();

        for (int i = 0; i < anclasOrdenadas.Count; i++)
        {
            anclasOrdenadas[i].Orden = i + 1; 
            _unitOfWork.Anclas.Update(anclasOrdenadas[i]);
        }

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    #endregion
}