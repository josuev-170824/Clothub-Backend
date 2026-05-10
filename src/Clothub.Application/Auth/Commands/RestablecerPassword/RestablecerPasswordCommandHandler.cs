using Clothub.Application.Auth.Interfaces;
using MediatR;

namespace Clothub.Application.Auth.Commands.RestablecerPassword;

public class RestablecerPasswordCommandHandler : IRequestHandler<RestablecerPasswordCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public RestablecerPasswordCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(RestablecerPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NuevaPassword.Length < 8)
            throw new InvalidOperationException("La contraseña debe tener al menos 8 caracteres.");

        var usuario = await _usuarioRepository.ObtenerPorTokenRecuperacionAsync(request.Token, cancellationToken);

        if (usuario is null)
            throw new InvalidOperationException("El link es inválido o ya fue usado.");

        if (usuario.FechaExpiracionTokenRecuperacion is null || usuario.FechaExpiracionTokenRecuperacion < DateTime.UtcNow)
            throw new InvalidOperationException("El link ha expirado. Solicitá uno nuevo.");

        if (BCrypt.Net.BCrypt.Verify(request.NuevaPassword, usuario.PasswordHash))
            throw new InvalidOperationException("La nueva contraseña no puede ser igual a la actual.");

        usuario.RestablecerPassword(BCrypt.Net.BCrypt.HashPassword(request.NuevaPassword, workFactor: 12));
        await _usuarioRepository.ActualizarAsync(cancellationToken);
    }
}
