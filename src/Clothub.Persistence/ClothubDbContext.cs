using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clothub.Persistence;

public class ClothubDbContext(DbContextOptions<ClothubDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Tienda> Tiendas => Set<Tienda>();
    public DbSet<Prenda> Prendas => Set<Prenda>();
    public DbSet<FotoPrenda> FotosPrenda => Set<FotoPrenda>();
    public DbSet<Etiqueta> Etiquetas => Set<Etiqueta>();
    public DbSet<EtiquetaPrenda> EtiquetasPrenda => Set<EtiquetaPrenda>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Apartado> Apartados => Set<Apartado>();
    public DbSet<Resena> Resenas => Set<Resena>();
    public DbSet<Seguimiento> Seguimientos => Set<Seguimiento>();
    public DbSet<Reporte> Reportes => Set<Reporte>();
    public DbSet<Suscripcion> Suscripciones => Set<Suscripcion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
