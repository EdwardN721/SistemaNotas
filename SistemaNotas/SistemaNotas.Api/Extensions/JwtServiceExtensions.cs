using System.Text;
using Microsoft.IdentityModel.Tokens;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SistemaNotas.Api.Extensions
{
  public static class JwtServiceExtensions
  {
    public static IServiceCollection AddJwtAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

      services.AddSingleton<IJwtProvider, JwtProvider>();

      IConfigurationSection jwtSettings = configuration.GetSection(JwtSettings.SectionName);

      string secretKey = jwtSettings["Secret"]
          ?? throw new ArgumentNullException("JWT Secret no existe en la configuración.");

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

          ValidateIssuer = true,
          ValidIssuer = jwtSettings["Issuer"],

          ValidateAudience = true,
          ValidAudience = jwtSettings["Audience"],

          ValidateLifetime = true,

          // .NET da 5 minutos "de vida" a un token expirado. 
          // En sistemas estrictos, lo forzamos a cero para que expire en el segundo exacto.
          ClockSkew = TimeSpan.Zero
        };
      });

      services.AddAuthorization();

      return services;
    }
  }
}