using Microsoft.EntityFrameworkCore;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Infrastructure.Data;
using SistemaNotas.Infrastructure.Repository;
using SistemaNotas.Infrastructure.Interceptors;

namespace SistemaNotas.Api.Extensions
{
  public static class InfrastructureServiceExtensions
  {
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<AuditInterceptor>();

      services.AddDbContext<NotasDbContext>((sp, options) =>
      {
        AuditInterceptor? auditInterceptor = sp.GetRequiredService<AuditInterceptor>();

        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(auditInterceptor);
      });

      return services;
    }

    public static IServiceCollection AddUnitOfWorkConfig(this IServiceCollection services)
    {
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      return services;
    }
  }
}
