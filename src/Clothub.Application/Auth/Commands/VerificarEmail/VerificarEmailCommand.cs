using MediatR;

namespace Clothub.Application.Auth.Commands.VerificarEmail;

public record VerificarEmailCommand(string Email, string Codigo) : IRequest<string>;
