namespace SistemaNotas.Api.Extensions
{
  public static class CorsServiceExtensions
  {
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration, string policyName)
    {
      string[] allowedOrigins = configuration.GetSection("CorsSettings:AllowedOrigins")
          .Get<string[]>()
              ?? Array.Empty<string>();

      services.AddCors(options =>
      {
        options.AddPolicy(policyName, builder =>
        {
          builder.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
        });
      });

      return services;
    }
  }
}
