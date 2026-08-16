using SistemaNotas.Application.Services;
using SistemaNotas.Application.Interfaces;

namespace SistemaNotas.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IPresentacionService, PresentacionService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}