namespace Clothub.Domain.Entities;

public class Etiqueta
{
    private Etiqueta() { }

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;

    public ICollection<EtiquetaPrenda> Prendas { get; private set; } = [];
    public ICollection<Seguimiento> Seguimientos { get; private set; } = [];

    public static Etiqueta Crear(string nombre)
    {
        return new Etiqueta
        {
            Id = Guid.NewGuid(),
            Nombre = nombre.ToLowerInvariant().Trim()
        };
    }
}
