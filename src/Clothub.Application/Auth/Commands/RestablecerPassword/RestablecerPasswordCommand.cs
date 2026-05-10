using MediatR;

namespace Clothub.Application.Auth.Commands.RestablecerPassword;

public record RestablecerPasswordCommand(string Token, string NuevaPassword) : IRequest;
