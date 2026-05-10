using Clothub.Application.Auth.Interfaces;
using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

  namespace Clothub.Persistence.Repositories;

  public class UsuarioRepository : Repository<ClothubDbContext, Usuario>, IUsuarioRepository
  {
    // Constructor
    public UsuarioRepository(ClothubDbContext context) : base(context) { }

    //Verificar si el email ya existe
    public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
    }

    //Obtener por email y password
    public async Task<Usuario?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
    }

    public async Task ActualizarAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
  }