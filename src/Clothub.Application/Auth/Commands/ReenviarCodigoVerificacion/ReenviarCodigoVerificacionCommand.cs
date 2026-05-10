using MediatR;

namespace Clothub.Application.Auth.Commands.ReenviarCodigoVerificacion;

public record ReenviarCodigoVerificacionCommand(string Email) : IRequest;
