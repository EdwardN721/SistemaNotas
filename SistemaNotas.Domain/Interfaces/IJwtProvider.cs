namespace SistemaNotas.Domain.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string email, string nombre);
}