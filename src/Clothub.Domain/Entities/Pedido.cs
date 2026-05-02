using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Pedido
{
    private Pedido() { }

    public Guid Id { get; private set; }
    public Guid CompradorId { get; private set; }
    public Guid TiendaId { get; private set; }
    public Guid PrendaId { get; private set; }
    public decimal MontoTotal { get; private set; }
    public decimal MontoVendedor { get; private set; }
    public decimal Comision { get; private set; }
    public TipoEnvio TipoEnvio { get; private set; }
    public decimal CostoEnvio { get; private set; }
    public string Provincia { get; private set; } = string.Empty;
    public string Canton { get; private set; } = string.Empty;
    public string Distrito { get; private set; } = string.Empty;
    public string DireccionExacta { get; private set; } = string.Empty;
    public string NombreDestinatario { get; private set; } = string.Empty;
    public string TelefonoDestinatario { get; private set; } = string.Empty;
    public EstadoPedido Estado { get; private set; }
    public string? CodigoRastreo { get; private set; }
    public string? StripePaymentIntentId { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Usuario Comprador { get; private set; } = null!;
    public Tienda Tienda { get; private set; } = null!;
    public Prenda Prenda { get; private set; } = null!;
    public Resena? Resena { get; private set; }

    public static Pedido Crear(Guid compradorId, Guid tiendaId, Guid prendaId,
        decimal precioBase, decimal costoEnvio, decimal porcentajeComision,
        TipoEnvio tipoEnvio, string provincia, string canton, string distrito,
        string direccionExacta, string nombreDestinatario, string telefonoDestinatario)
    {
        var comision = precioBase * porcentajeComision;
        var montoVendedor = precioBase - comision;

        return new Pedido
        {
            Id = Guid.NewGuid(),
            CompradorId = compradorId,
            TiendaId = tiendaId,
            PrendaId = prendaId,
            MontoTotal = precioBase + costoEnvio,
            MontoVendedor = montoVendedor,
            Comision = comision,
            CostoEnvio = costoEnvio,
            TipoEnvio = tipoEnvio,
            Provincia = provincia,
            Canton = canton,
            Distrito = distrito,
            DireccionExacta = direccionExacta,
            NombreDestinatario = nombreDestinatario,
            TelefonoDestinatario = telefonoDestinatario,
            Estado = EstadoPedido.Pendiente,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void AsignarStripePaymentIntent(string paymentIntentId) =>
        StripePaymentIntentId = paymentIntentId;

    public void MarcarComoPagado() => Estado = EstadoPedido.Pagado;

    public void MarcarComoEnviado(string codigoRastreo)
    {
        Estado = EstadoPedido.Enviado;
        CodigoRastreo = codigoRastreo;
    }

    public void MarcarComoEntregado() => Estado = EstadoPedido.Entregado;
}
