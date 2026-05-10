using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using MediatR;

namespace Clothub.Application.Auth.Commands.LoginWithEmail;

public class LoginWithEmailCommandHandler : IRequestHandler<LoginWithEmailCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public LoginWithEmailCommandHandler(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(LoginWithEmailCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);

        if (usuario is null)
            throw new InvalidOperationException("Credenciales inválidas.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            throw new InvalidOperationException("Credenciales inválidas.");

        if (!usuario.EmailVerificado)
            throw new InvalidOperationException("Debés verificar tu email antes de iniciar sesión.");

        return _jwtService.GenerarToken(usuario);
    }
}
