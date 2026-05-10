using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using Clothub.Domain.Entities;
using MediatR;

namespace Clothub.Application.Auth.Commands.RegisterWithEmail;

public class RegisterWithEmailCommandHandler : IRequestHandler<RegisterWithEmailCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public RegisterWithEmailCommandHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(RegisterWithEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _usuarioRepository.ExisteEmailAsync(request.Email, cancellationToken))
            throw new InvalidOperationException("El email ya está registrado.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var usuario = Usuario.CrearConEmail(request.Nombre, request.Apellidos, request.Email, passwordHash);

        await _usuarioRepository.AgregarAsync(usuario, cancellationToken);

        await _emailService.EnviarCodigoVerificacionAsync(
            usuario.Email, usuario.Nombre, usuario.TokenVerificacionEmail!, cancellationToken);

        return usuario.Id;
    }
}
