namespace Clothub.API.Auth;

public record RestablecerPasswordRequest(string Token, string NuevaPassword);
