using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Apartado
{
    private Apartado() { }

    public Guid Id { get; private set; }
    public Guid CompradorId { get; private set; }
    public Guid PrendaId { get; private set; }
    public decimal MontoPagado { get; private set; }
    public decimal MontoRestante { get; private set; }
    public DateTime FechaLimite { get; private set; }
    public EstadoApartado Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Comprador { get; private set; } = null!;
    public Prenda Prenda { get; private set; } = null!;

    public static Apartado Crear(Guid compradorId, Guid prendaId, decimal precioTotal,
        decimal porcentajeAdelanto, int tiempoLimiteHoras)
    {
        var montoPagado = Math.Round(precioTotal * (porcentajeAdelanto / 100), 2);

        return new Apartado
        {
            Id = Guid.NewGuid(),
            CompradorId = compradorId,
            PrendaId = prendaId,
            MontoPagado = montoPagado,
            MontoRestante = precioTotal - montoPagado,
            FechaLimite = DateTime.UtcNow.AddHours(tiempoLimiteHoras),
            Estado = EstadoApartado.Activo,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void Completar() => Estado = EstadoApartado.Completado;

    public void Cancelar() => Estado = EstadoApartado.Cancelado;

    public bool EstaVencido() => DateTime.UtcNow > FechaLimite && Estado == EstadoApartado.Activo;
}
