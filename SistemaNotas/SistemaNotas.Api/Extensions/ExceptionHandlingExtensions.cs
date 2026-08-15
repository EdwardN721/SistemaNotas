using SistemaNotas.Api.Handlers;

namespace SistemaNotas.Api.Extensions
{
  public static class ExceptionHandlingExtensions
  {
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
      services.AddExceptionHandler<GlobalExceptionHandler>();

      services.AddProblemDetails();

      return services;
    }
  }
}
