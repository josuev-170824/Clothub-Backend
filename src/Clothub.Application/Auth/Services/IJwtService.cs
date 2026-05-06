using Clothub.Domain.Entities;

namespace Clothub.Application.Auth.Services;

public interface IJwtService
{
    string GenerarToken(Usuario usuario);
}
