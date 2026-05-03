using Clothub.Application.Auth.Interfaces;
using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

  namespace Clothub.Persistence.Repositories;

  public class UsuarioRepository : Repository<ClothubDbContext, Usuario>, IUsuarioRepository
  {
      public UsuarioRepository(ClothubDbContext context) : base(context) { }

      public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken)
      {
          return await _context.Usuarios.AnyAsync(u => u.Email == email.ToLowerInvariant(), cancellationToken);
      }
  }