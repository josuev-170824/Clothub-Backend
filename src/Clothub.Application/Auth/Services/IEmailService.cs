namespace Clothub.Application.Auth.Services;

public interface IEmailService
{
    Task EnviarCodigoVerificacionAsync(string email, string nombre, string codigo, CancellationToken cancellationToken);
}
