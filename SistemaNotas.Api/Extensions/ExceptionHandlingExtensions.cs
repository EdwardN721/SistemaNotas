using SistemaNotas.Api.Handlers;

namespace SistemaNotas.Api.Extensions
{
  public static class ExceptionHandlingExtensions
  {
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
      services.AddProblemDetails();
      
      services.AddExceptionHandler<GlobalExceptionHandler>();

      return services;
    }
  }
}
