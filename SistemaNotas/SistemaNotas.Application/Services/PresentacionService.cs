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

    public async Task<PresentacionResponseDto> CrearPresentacionAsync(CrearPresentacionDto dto, CancellationToken cancellationToken = default)
    {
      _logger.LogInformation("Creando Presentacion: {Titulo}", dto.Titulo);
      Presentacion presentacion = dto.MapToEntity();

      await _unitOfWork.Presentaciones.AddAsync(presentacion, cancellationToken);
      await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogInformation("Presentación agregada {Titulo}", dto.Titulo);
      return presentacion.MapToDto();
    }

    public async Task<IReadOnlyList<PresentacionResponseDto>> ObtenerTodasPresentacionesAsync(CancellationToken cancellationToken = default)
    {
      IReadOnlyList<Presentacion> presentaciones = await _unitOfWork.Presentaciones.GetAllAsync(cancellationToken);

      _logger.LogInformation("Registros obtenidos: {Contador}", presentaciones.Count());
      return presentaciones.MapToDto();
    }

    public async Task<PresentacionResponseDto> ObtenerPresentacionPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, cancellationToken);

      _logger.LogInformation("Presentación {Titulo} encontrada.", presentacion.Titulo);
      return presentacion.MapToDto();
    }

    public async Task ActualizarPresentacionAsync(Guid id, ActualizarPresentacionDto dto, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, cancellationToken);

      presentacion.UpdateEntity(dto);

      _unitOfWork.Presentaciones.Update(presentacion);
      await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogInformation("Presentación {Titulo} actualizada.", presentacion.Titulo);
    }

    public async Task EliminarPresentacionAsync(Guid id, CancellationToken cancellationToken = default)
    {
      Presentacion presentacion = await ObtenerPresentacion(id, cancellationToken);
      
      _unitOfWork.Presentaciones.Delete(presentacion);
    await _unitOfWork.CommitAsync(cancellationToken);

      _logger.LogWarning("Presentación {Titulo} eliminada.", presentacion.Titulo);
    }

    #region MetodosPrivados

    private async Task<Presentacion> ObtenerPresentacion(Guid id, CancellationToken cancellationToken = default)
    {
      Presentacion? presentacion = await _unitOfWork.Presentaciones.GetByIdAsync(id, cancellationToken);

      if (presentacion is null)
      {
        _logger.LogWarning("La presentación no éxiste:");
        throw new NotFoundException("La presentación no éxiste.");
      }

      return presentacion;
    }

    #endregion
  }
}
