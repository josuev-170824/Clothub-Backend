using Clothub.Domain.Entities;

namespace Clothub.Application.Auth.Interfaces;

public interface IUsuarioRepository
{
    //Verificar si el email ya existe
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken);
    //Agregar usuario
    Task AgregarAsync(Usuario usuario, CancellationToken cancellationToken);
    //Obtener por email y password
    Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken);
    //Guardar cambios sobre una entidad ya trackeada
    Task ActualizarAsync(CancellationToken cancellationToken);
}
