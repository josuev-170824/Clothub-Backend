using Clothub.Domain.Entities;

namespace Clothub.Application.Auth.Interfaces;

public interface IUsuarioRepository
{
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken);
    Task AgregarAsync(Usuario usuario, CancellationToken cancellationToken);
}
