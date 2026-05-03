using MediatR;

namespace Clothub.Application.Auth.Commands.RegisterWithEmail;

public record RegisterWithEmailCommand(string Nombre, string Apellidos, string Email, string Password) : IRequest<Guid>;