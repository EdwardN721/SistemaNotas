using Asp.Versioning;
using Microsoft.OpenApi; 

namespace SistemaNotas.Api.Extensions;

public static class ApiDocsServiceExtensions
{
    public static IServiceCollection AddApiVersioningAndDocs(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true; 
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                // En .NET 10, la información del documento usa las nuevas clases base
                document.Info = new OpenApiInfo
                {
                    Title = "Sistema Notas API",
                    Version = "v1",
                    Description = "API para el Sistema de Anclaje Progresivo"
                };

                var components = document.Components ??= new OpenApiComponents();
                
                // 💡 SOLUCIÓN: Usamos la interfaz explícita IOpenApiSecurityScheme
                components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                // Definimos el candado para que Scalar lo renderice
                components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Ingresa tu token JWT para probar los endpoints seguros."
                };

                return Task.CompletedTask;
            });
        });

        return services;
    }
}