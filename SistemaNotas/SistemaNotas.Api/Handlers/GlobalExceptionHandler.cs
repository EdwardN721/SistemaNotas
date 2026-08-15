using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaNotas.Api.Handlers
{
  public class GlobalExceptionHandler : IExceptionHandler
  {
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) { _logger = logger; }

    public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext, 
      Exception exception, 
      CancellationToken cancellationToken)
    {
      // Registramos el error internamente
      _logger.LogError(exception, "Ocurrió una excepción no controlada: {Message}", exception.Message);

      // Preparar la respuesta estandar
      ProblemDetails problemDetails = new ProblemDetails
      {
        Instance = httpContext.Request.Path,
        Type = exception.GetType().Name,
      };

      // Mapeamos la excepción a un código HTTP correcto
      switch (exception)
      {
        // Errores de Base de Datos (ej. Violación de llaves, Constraint fails)
        case DbUpdateException:
          problemDetails.Status = StatusCodes.Status409Conflict;
          problemDetails.Title = "Conflicto en la base de datos";
          problemDetails.Detail = "Ocurrió un error al intentar guardar los cambios. Puede deberse a un registro duplicado o datos inválidos.";
          break;

        // Registros no encontrados (cuando intentes buscar un Guid que no existe)
        case KeyNotFoundException:
          problemDetails.Status = StatusCodes.Status404NotFound;
          problemDetails.Title = "Recurso no encontrado";
          problemDetails.Detail = "El identificador proporcionado no existe en nuestros registros.";
          break;

        // Error genérico (Atrapa todo lo demás, ej. NullReferenceException)
        default:
          problemDetails.Status = StatusCodes.Status500InternalServerError;
          problemDetails.Title = "Error interno del servidor";
          problemDetails.Detail = "Ocurrió un error inesperado. Por favor, contacte a soporte si el problema persiste.";
          break;
      }

      // Escribimos la respuesta
      httpContext.Response.StatusCode = problemDetails.Status.Value;
      await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

      // Devolver 'true' le dice a .NET que ya manejamos el error y detenga la propagación.
      return true;
    }
  }
}
