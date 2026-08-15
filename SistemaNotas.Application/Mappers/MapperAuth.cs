using SistemaNotas.Application.DTOs.Respuesta.Usuarios;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Application.Mappers
{
  public static class MapperAuth   
  {
    public static AuthResponseDto MapToDto(this Usuario usuario, string token)
    {
        return new AuthResponseDto
        {
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Token = token
        };
    }
  }
}