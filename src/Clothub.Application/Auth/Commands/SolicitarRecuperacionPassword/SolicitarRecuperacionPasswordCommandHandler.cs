using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using Clothub.Domain.Enums;
using MediatR;

namespace Clothub.Application.Auth.Commands.SolicitarRecuperacionPassword;

public class SolicitarRecuperacionPasswordCommandHandler : IRequestHandler<SolicitarRecuperacionPasswordCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public SolicitarRecuperacionPasswordCommandHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task Handle(SolicitarRecuperacionPasswordCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);

        // Respuesta siempre genérica — no revelar si el email existe, está verificado o es de Google
        if (usuario is null || !usuario.EmailVerificado || usuario.ProveedorAuth != ProveedorAuth.Local)
            return;

        usuario.GenerarTokenRecuperacion();
        await _usuarioRepository.ActualizarAsync(cancellationToken);

        var link = $"{request.FrontendUrl}/reset-password#token={usuario.TokenRecuperacionPassword}";
        await _emailService.EnviarEmailRecuperacionPasswordAsync(usuario.Email, usuario.Nombre, link, cancellationToken);
    }
}
