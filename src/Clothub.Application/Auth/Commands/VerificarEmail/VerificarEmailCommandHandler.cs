using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using MediatR;

namespace Clothub.Application.Auth.Commands.VerificarEmail;

public class VerificarEmailCommandHandler : IRequestHandler<VerificarEmailCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public VerificarEmailCommandHandler(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(VerificarEmailCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);

        if (usuario is null)
            throw new InvalidOperationException("Usuario no encontrado.");

        if (usuario.EmailVerificado)
            throw new InvalidOperationException("El email ya fue verificado.");

        if (usuario.FechaExpiracionTokenVerificacion is null || usuario.FechaExpiracionTokenVerificacion < DateTime.UtcNow)
            throw new InvalidOperationException("El código ha expirado. Solicitá uno nuevo.");

        if (usuario.TokenVerificacionEmail != request.Codigo)
            throw new InvalidOperationException("El código es incorrecto.");

        usuario.VerificarEmail();
        await _usuarioRepository.ActualizarAsync(cancellationToken);

        return _jwtService.GenerarToken(usuario);
    }
}
