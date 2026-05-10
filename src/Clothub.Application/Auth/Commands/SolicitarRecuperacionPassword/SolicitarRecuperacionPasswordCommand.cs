using MediatR;

namespace Clothub.Application.Auth.Commands.SolicitarRecuperacionPassword;

public record SolicitarRecuperacionPasswordCommand(string Email, string FrontendUrl) : IRequest;
