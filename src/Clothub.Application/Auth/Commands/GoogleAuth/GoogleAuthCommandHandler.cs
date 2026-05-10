using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using Clothub.Domain.Entities;
using Clothub.Domain.Enums;
using MediatR;

namespace Clothub.Application.Auth.Commands.GoogleAuth;

public class GoogleAuthCommandHandler : IRequestHandler<GoogleAuthCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public GoogleAuthCommandHandler(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);

        if (usuario is not null && usuario.ProveedorAuth == ProveedorAuth.Local)
            throw new InvalidOperationException("Esta cuenta usa email y contraseña. Iniciá sesión con ese método.");

        if (usuario is null)
        {
            usuario = Usuario.CrearConGoogle(request.Nombre, request.Apellidos, request.Email, request.GoogleId);
            await _usuarioRepository.AgregarAsync(usuario, cancellationToken);
        }

        return _jwtService.GenerarToken(usuario);
    }
}
