using SistemaNotas.Application.Services;
using SistemaNotas.Application.Interfaces;

namespace SistemaNotas.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IPresentacionService, PresentacionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPresentacionService, PresentacionService>();
        services.AddScoped<IAnclaService, AnclaService>();
        services.AddScoped<ISeccionService, SeccionService>();

        return services;
    }
}