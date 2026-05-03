using MediatR;

namespace Clothub.Application.Auth.Commands.LoginWithEmail;

public record LoginWithEmailCommand(string Email, string Password) : IRequest<string>;