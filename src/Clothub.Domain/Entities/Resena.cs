namespace Clothub.Domain.Entities;

public class Resena
{
    private Resena() { }

    public Guid Id { get; private set; }
    public Guid CompradorId { get; private set; }
    public Guid TiendaId { get; private set; }
    public Guid PedidoId { get; private set; }
    public int Calificacion { get; private set; }
    public string? Comentario { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Comprador { get; private set; } = null!;
    public Tienda Tienda { get; private set; } = null!;
    public Pedido Pedido { get; private set; } = null!;

    public static Resena Crear(Guid compradorId, Guid tiendaId, Guid pedidoId,
        int calificacion, string? comentario)
    {
        if (calificacion < 1 || calificacion > 5)
            throw new ArgumentOutOfRangeException(nameof(calificacion), "La calificación debe estar entre 1 y 5.");

        return new Resena
        {
            Id = Guid.NewGuid(),
            CompradorId = compradorId,
            TiendaId = tiendaId,
            PedidoId = pedidoId,
            Calificacion = calificacion,
            Comentario = comentario,
            FechaCreacion = DateTime.UtcNow
        };
    }
}
