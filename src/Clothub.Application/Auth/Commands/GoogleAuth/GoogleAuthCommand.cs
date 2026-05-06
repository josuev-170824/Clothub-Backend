using MediatR;

namespace Clothub.Application.Auth.Commands.GoogleAuth;

public record GoogleAuthCommand(
    string GoogleId,
    string Email,
    string Nombre,
    string Apellidos
) : IRequest<string>;
