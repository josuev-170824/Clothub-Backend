using Clothub.Application.Auth.Interfaces;
using Clothub.Domain.Entities;
using MediatR;

namespace Clothub.Application.Auth.Commands.RegisterWithEmail;

public class RegisterWithEmailCommandHandler : IRequestHandler<RegisterWithEmailCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public RegisterWithEmailCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(RegisterWithEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _usuarioRepository.ExisteEmailAsync(request.Email, cancellationToken))
            throw new InvalidOperationException("El email ya está registrado.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var usuario = Usuario.CrearConEmail(request.Nombre, request.Apellidos, request.Email, passwordHash);

        await _usuarioRepository.AgregarAsync(usuario, cancellationToken);

        return usuario.Id;
    }
}
