namespace Clothub.Domain.Entities;

public class EtiquetaPrenda
{
    private EtiquetaPrenda() { }

    public Guid PrendaId { get; private set; }
    public Guid EtiquetaId { get; private set; }

    public Prenda Prenda { get; private set; } = null!;
    public Etiqueta Etiqueta { get; private set; } = null!;

    public static EtiquetaPrenda Crear(Guid prendaId, Guid etiquetaId)
    {
        return new EtiquetaPrenda
        {
            PrendaId = prendaId,
            EtiquetaId = etiquetaId
        };
    }
}
