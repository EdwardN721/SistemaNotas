using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaNotas.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SistemaNotas.Infrastructure.Authentication
{
  public class JwtProvider : IJwtProvider
  {
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IOptions<JwtSettings> jwtOptions)
    {
      _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string email, string nombre)
    {
      // Definimos los Claims (La información pública que viaja dentro del token)
      Claim[] claims = new[]
      {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Sub (Subject) = El ID del usuario
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Name, nombre),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Jti = ID único del token
        };

      // Creamos la llave simétrica con el secreto
      SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

      // Definimos el algoritmo de firma (HMAC SHA-256 es el estándar de la industria)
      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      // Construimos el token
      JwtSecurityToken token = new JwtSecurityToken(
          issuer: _jwtSettings.Issuer,
          audience: _jwtSettings.Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
