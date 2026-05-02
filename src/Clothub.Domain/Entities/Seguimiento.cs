namespace Clothub.Domain.Entities;

public class Seguimiento
{
    private Seguimiento() { }

    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid? TiendaId { get; private set; }
    public Guid? EtiquetaId { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Usuario { get; private set; } = null!;
    public Tienda? Tienda { get; private set; }
    public Etiqueta? Etiqueta { get; private set; }

    public static Seguimiento SeguirTienda(Guid usuarioId, Guid tiendaId)
    {
        return new Seguimiento
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            TiendaId = tiendaId,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public static Seguimiento SeguirEtiqueta(Guid usuarioId, Guid etiquetaId)
    {
        return new Seguimiento
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            EtiquetaId = etiquetaId,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public bool EsSeguimientoDeTienda => TiendaId.HasValue;
    public bool EsSeguimientoDeEtiqueta => EtiquetaId.HasValue;
}
