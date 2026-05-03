using Microsoft.EntityFrameworkCore;

namespace Clothub.Persistence;

public abstract class Repository<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    protected readonly TContext _context;

    protected Repository(TContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
