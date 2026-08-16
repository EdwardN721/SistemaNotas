using System.Security.Claims;

namespace SistemaNotas.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUsuarioId(this ClaimsPrincipal principal)
    {
        // En JWT, el NameIdentifier (o "sub") es el estándar para guardar el ID del usuario
        Claim? claim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? 
            principal.FindFirst("sub"); // En caso de que se use "sub" en lugar de NameIdentifier

        if (claim is null || !Guid.TryParse(claim.Value, out Guid usuarioId))
        {
            // Si alguien mandó un token malformado o sin ID, lo bloqueamos al instante
            throw new UnauthorizedAccessException("Token inválido o sin ID de usuario.");
        }

        return usuarioId;
    }
}