using Clothub.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clothub.API.Services;

public class LimpiezaCuentasZombieService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public LimpiezaCuentasZombieService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ClothubDbContext>();

            await db.Usuarios
                .Where(u => !u.EmailVerificado && u.FechaExpiracionTokenVerificacion < DateTime.UtcNow)
                .ExecuteDeleteAsync(stoppingToken);
        }
    }
}
