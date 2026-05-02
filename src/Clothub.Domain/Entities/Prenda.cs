using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Prenda
{
    private Prenda() { }

    public Guid Id { get; private set; }
    public Guid TiendaId { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? Marca { get; private set; }
    public string TipoPrenda { get; private set; } = string.Empty;
    public string Talla { get; private set; } = string.Empty;
    public decimal? MedidaCintura { get; private set; }
    public decimal? MedidaLargo { get; private set; }
    public decimal? MedidaAncho { get; private set; }
    public CondicionPrenda Condicion { get; private set; }
    public string? Descripcion { get; private set; }
    public decimal Precio { get; private set; }
    public EstadoPrenda Estado { get; private set; }
    public int Vistas { get; private set; }
    public DateTime FechaPublicacion { get; private set; }

    public Tienda Tienda { get; private set; } = null!;
    public ICollection<FotoPrenda> Fotos { get; private set; } = [];
    public ICollection<EtiquetaPrenda> Etiquetas { get; private set; } = [];
    public ICollection<Pedido> Pedidos { get; private set; } = [];
    public ICollection<Apartado> Apartados { get; private set; } = [];

    public static Prenda Crear(Guid tiendaId, string nombre, string? marca, string tipoPrenda,
        string talla, CondicionPrenda condicion, decimal precio, string? descripcion,
        decimal? medidaCintura = null, decimal? medidaLargo = null, decimal? medidaAncho = null)
    {
        return new Prenda
        {
            Id = Guid.NewGuid(),
            TiendaId = tiendaId,
            Nombre = nombre,
            Marca = marca,
            TipoPrenda = tipoPrenda,
            Talla = talla,
            Condicion = condicion,
            Precio = precio,
            Descripcion = descripcion,
            MedidaCintura = medidaCintura,
            MedidaLargo = medidaLargo,
            MedidaAncho = medidaAncho,
            Estado = EstadoPrenda.Disponible,
            Vistas = 0,
            FechaPublicacion = DateTime.UtcNow
        };
    }

    public void Actualizar(string nombre, string? marca, string tipoPrenda, string talla,
        CondicionPrenda condicion, decimal precio, string? descripcion,
        decimal? medidaCintura, decimal? medidaLargo, decimal? medidaAncho)
    {
        Nombre = nombre;
        Marca = marca;
        TipoPrenda = tipoPrenda;
        Talla = talla;
        Condicion = condicion;
        Precio = precio;
        Descripcion = descripcion;
        MedidaCintura = medidaCintura;
        MedidaLargo = medidaLargo;
        MedidaAncho = medidaAncho;
    }

    public void Pausar() => Estado = EstadoPrenda.Pausada;

    public void Activar() => Estado = EstadoPrenda.Disponible;

    public void Apartar() => Estado = EstadoPrenda.Apartada;

    public void Vender() => Estado = EstadoPrenda.Vendida;

    public void RegistrarVista() => Vistas++;
}
