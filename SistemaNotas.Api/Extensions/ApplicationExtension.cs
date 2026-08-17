using SistemaNotas.Application.Services;
using SistemaNotas.Application.Interfaces;

namespace SistemaNotas.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAnclaService, AnclaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoriaAnclaService, CategoriaAnclaService>();
        services.AddScoped<IPresentacionService, PresentacionService>();
        services.AddScoped<IRetrospectivaService, RetrospectivaService>();
        services.AddScoped<ISeccionService, SeccionService>();

        return services;
    }
}