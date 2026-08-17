using SistemaNotas.Domain.Entities;
using SistemaNotas.Domain.Interfaces;
using SistemaNotas.Domain.Exceptions;
using SistemaNotas.Application.Mappers;
using SistemaNotas.Application.Interfaces;
using SistemaNotas.Application.Dtos.Peticion.Usuarios;
using SistemaNotas.Application.DTOs.Respuesta.Usuarios;

namespace SistemaNotas.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<AuthResponseDto> RegistrarAsync(RegistroRequestDto request, CancellationToken cancellationToken = default)
    {
        // 1. Validar si el correo ya existe
        bool correoExiste = await _unitOfWork.Usuarios.AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (correoExiste)
        {
            throw new BusinessRuleException("El correo ya está registrado.");
        }

        // 2. Encriptar la contraseña (Hash)
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. Crear el usuario
        Usuario nuevoUsuario = request.MapToEntity(passwordHash);

        await _unitOfWork.Usuarios.AddAsync(nuevoUsuario, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        // 4. Generar Token automáticamente al registrarse
        string token = _jwtProvider.GenerateToken(nuevoUsuario.Id, nuevoUsuario.Email, nuevoUsuario.Nombre);

        return nuevoUsuario.MapToDto(token); 
    }

  public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        // Buscar al usuario por correo
        Usuario? usuario = await _unitOfWork.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email, true, cancellationToken);

        // Validar que exista y que la contraseña coincida con el Hash
        if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
        {
            throw new UnauthorizedAccessException("Credenciales inválidas."); 
        }

        // 3. Generar el Token
        string token = _jwtProvider.GenerateToken(usuario.Id, usuario.Email, usuario.Nombre);

        return usuario.MapToDto(token);
    }
}