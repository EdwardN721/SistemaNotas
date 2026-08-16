using SistemaNotas.Domain.Entities;
using Microsoft.Extensions.Logging;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Domain.Exceptions;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Presentacion;
using SistemaNotas.Application.Dtos.Respuesta.Presentacion;

namespace SistemaNotas.Application.Services
{
  public class PresentacionService : IPresentacionService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PresentacionService> _logger;

    public PresentacionService(IUnitOfWork unitOfWork, ILogger<PresentacionService> logger)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task<PresentacionResponseDto> CrearPresentacionAsync(CrearPresentacionDto dto, Guid usuarioId, CancellationToken cancellationToken = default)
    {
      _logger.LogInformation("Creando Presentacion: {Titulo}", dto.Titulo);
      Presentacion presentacion = dto.MapToEntity(usuarioId);

      await _unitOfWork.Presentaciones.AddAsync(presentacion, cancellationToken);
      await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogInformation("Presentación agregada {Titulo}", dto.Titulo);
      return presentacion.MapToDto();
    }

    public async Task<IReadOnlyList<PresentacionResponseDto>> ObtenerTodasPresentacionesAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
      IReadOnlyList<Presentacion> presentaciones = await _unitOfWork.Presentaciones.GetAsync(p => p.UsuarioId == usuarioId, cancellationToken);

      _logger.LogInformation("Registros obtenidos: {Contador}", presentaciones.Count());
      return presentaciones.MapToDto();
    }

    public async Task<PresentacionResponseDto> ObtenerPresentacionPorIdAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, usuarioId, cancellationToken);

      _logger.LogInformation("Presentación {Titulo} encontrada.", presentacion.Titulo);
      return presentacion.MapToDto();
    }

    public async Task ActualizarPresentacionAsync(Guid id, Guid usuarioId, ActualizarPresentacionDto dto, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, usuarioId, cancellationToken);

      presentacion.UpdateEntity(dto);

      _unitOfWork.Presentaciones.Update(presentacion);
      await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogInformation("Presentación {Titulo} actualizada.", presentacion.Titulo);
    }

    public async Task EliminarPresentacionAsync(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, usuarioId, cancellationToken);

      _unitOfWork.Presentaciones.Delete(presentacion);
    await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogWarning("Presentación {Titulo} eliminada.", presentacion.Titulo);
    }

    #region MetodosPrivados

    private async Task<Presentacion> ObtenerPresentacion(Guid id, Guid usuarioId, CancellationToken cancellationToken = default)
    {
      Presentacion? presentacion = await _unitOfWork.Presentaciones.FirstOrDefaultAsync(p => p.Id == id && p.UsuarioId == usuarioId, cancellationToken);
      if (presentacion is null)
      {
        _logger.LogWarning("La presentación no éxiste: {Id}", id);
        throw new NotFoundException("La presentación no éxiste.");
      }

      return presentacion;
    }

    #endregion
  }
}
