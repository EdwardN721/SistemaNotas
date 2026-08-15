using Microsoft.EntityFrameworkCore;
using SistemaNotas.Infrastructure.Data;

namespace SistemaNotas.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyPendingMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<NotasDbContext>();
            
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate(); 
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error aplicando las migraciones a la base de datos.");
        }
    }
}