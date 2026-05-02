using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Tienda
{
    private Tienda() { }

    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? LogoUrl { get; private set; }
    public string? Descripcion { get; private set; }
    public string Provincia { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;
    public bool TelefonoVerificado { get; private set; }
    public string? Instagram { get; private set; }
    public string? WhatsApp { get; private set; }
    public string? ReglasDeLaTienda { get; private set; }
    public bool AceptaApartados { get; private set; }
    public decimal? PorcentajeApartado { get; private set; }
    public int? TiempoLimiteApartadoHoras { get; private set; }
    public PlanTienda Plan { get; private set; }
    public EstadoTienda Estado { get; private set; }
    public string? StripeAccountId { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Usuario { get; private set; } = null!;
    public ICollection<Prenda> Prendas { get; private set; } = [];
    public ICollection<Pedido> Pedidos { get; private set; } = [];
    public ICollection<Resena> Resenas { get; private set; } = [];
    public ICollection<Seguimiento> Seguimientos { get; private set; } = [];
    public ICollection<Reporte> Reportes { get; private set; } = [];

    public static Tienda Crear(Guid usuarioId, string nombre, string provincia, string telefono)
    {
        return new Tienda
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            Nombre = nombre,
            Provincia = provincia,
            Telefono = telefono,
            TelefonoVerificado = false,
            AceptaApartados = false,
            Plan = PlanTienda.Gratuito,
            Estado = EstadoTienda.Inactiva,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void ActivarConTelefono()
    {
        TelefonoVerificado = true;
        Estado = EstadoTienda.Activa;
    }

    public void Actualizar(string nombre, string? descripcion, string provincia,
        string? instagram, string? whatsApp, string? reglas)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Provincia = provincia;
        Instagram = instagram;
        WhatsApp = whatsApp;
        ReglasDeLaTienda = reglas;
    }

    public void ConfigurarApartados(bool acepta, decimal? porcentaje, int? tiempoLimiteHoras)
    {
        AceptaApartados = acepta;
        PorcentajeApartado = acepta ? porcentaje : null;
        TiempoLimiteApartadoHoras = acepta ? tiempoLimiteHoras : null;
    }

    public void AsignarLogo(string logoUrl) => LogoUrl = logoUrl;

    public void AsignarStripeAccount(string stripeAccountId) => StripeAccountId = stripeAccountId;

    public void ActualizarPlan(PlanTienda plan) => Plan = plan;

    public void Suspender() => Estado = EstadoTienda.Suspendida;

    public void Habilitar() => Estado = EstadoTienda.Activa;

    public bool PuedePublicarMasPrendas(int prendasActuales)
    {
        if (Plan == PlanTienda.Premium) return true;
        return prendasActuales < 20;
    }

    public decimal ObtenerPorcentajeComision() => Plan == PlanTienda.Premium ? 0.10m : 0.20m;
}
