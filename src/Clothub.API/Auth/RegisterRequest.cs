namespace Clothub.API.Auth;

public record RegisterRequest(string Nombre, string Apellidos, string Email, string Password);