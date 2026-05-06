using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Suscripcion
{
    private Suscripcion() { }

    public Guid Id { get; private set; }
    public Guid TiendaId { get; private set; }
    public EstadoSuscripcion Estado { get; private set; }
    public DateTime FechaInicioPrueba { get; private set; }
    public DateTime FechaFinPrueba { get; private set; }
    public DateTime? FechaInicioSuscripcion { get; private set; }
    public DateTime? FechaVencimiento { get; private set; }
    public string? OnvoSubscriptionId { get; private set; }

    public Tienda Tienda { get; private set; } = null!;

    public static Suscripcion IniciarPrueba(Guid tiendaId)
    {
        var ahora = DateTime.UtcNow;
        return new Suscripcion
        {
            Id = Guid.NewGuid(),
            TiendaId = tiendaId,
            Estado = EstadoSuscripcion.Prueba,
            FechaInicioPrueba = ahora,
            FechaFinPrueba = ahora.AddDays(30)
        };
    }

    public void Activar(string onvoSubscriptionId, DateTime fechaVencimiento)
    {
        Estado = EstadoSuscripcion.Activa;
        FechaInicioSuscripcion = DateTime.UtcNow;
        FechaVencimiento = fechaVencimiento;
        OnvoSubscriptionId = onvoSubscriptionId;
    }

    public void Renovar(DateTime nuevaFechaVencimiento) => FechaVencimiento = nuevaFechaVencimiento;

    public void Vencer() => Estado = EstadoSuscripcion.Vencida;

    public void Cancelar() => Estado = EstadoSuscripcion.Cancelada;

    public bool EstaActiva() =>
        Estado == EstadoSuscripcion.Activa || Estado == EstadoSuscripcion.Prueba;

    public bool PruebaVencida() =>
        Estado == EstadoSuscripcion.Prueba && DateTime.UtcNow > FechaFinPrueba;
}
