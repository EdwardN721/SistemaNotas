using SistemaNotas.Application.Dtos.Peticion.Usuarios;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Application.Mappers
{
  public static class MapperUsuario  
  {
    public static Usuario MapToEntity(this RegistroRequestDto usuario, string passwordHash)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            PasswordHash = passwordHash
        };
    }
  }
}