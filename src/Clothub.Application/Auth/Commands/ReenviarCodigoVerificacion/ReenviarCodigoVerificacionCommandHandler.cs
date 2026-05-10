using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using MediatR;

namespace Clothub.Application.Auth.Commands.ReenviarCodigoVerificacion;

public class ReenviarCodigoVerificacionCommandHandler : IRequestHandler<ReenviarCodigoVerificacionCommand>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;

    public ReenviarCodigoVerificacionCommandHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task Handle(ReenviarCodigoVerificacionCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);

        // Respuesta siempre genérica para no revelar si el email existe o no
        if (usuario is null || usuario.EmailVerificado)
            return;

        if (usuario.FechaExpiracionTokenVerificacion is not null && usuario.FechaExpiracionTokenVerificacion > DateTime.UtcNow.AddMinutes(14))
            return;

        usuario.RegenerarCodigoVerificacion();
        await _usuarioRepository.ActualizarAsync(cancellationToken);

        await _emailService.EnviarCodigoVerificacionAsync(
            usuario.Email, usuario.Nombre, usuario.TokenVerificacionEmail!, cancellationToken);
    }
}
