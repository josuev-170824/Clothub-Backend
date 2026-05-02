using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Reporte
{
    private Reporte() { }

    public Guid Id { get; private set; }
    public Guid CompradorId { get; private set; }
    public Guid TiendaId { get; private set; }
    public string Motivo { get; private set; } = string.Empty;
    public EstadoReporte Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Comprador { get; private set; } = null!;
    public Tienda Tienda { get; private set; } = null!;

    public static Reporte Crear(Guid compradorId, Guid tiendaId, string motivo)
    {
        return new Reporte
        {
            Id = Guid.NewGuid(),
            CompradorId = compradorId,
            TiendaId = tiendaId,
            Motivo = motivo,
            Estado = EstadoReporte.Pendiente,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void MarcarComoRevisado() => Estado = EstadoReporte.Revisado;
}
