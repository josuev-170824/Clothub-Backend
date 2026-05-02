namespace Clothub.Domain.Entities;

public class FotoPrenda
{
    private FotoPrenda() { }

    public Guid Id { get; private set; }
    public Guid PrendaId { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public int Orden { get; private set; }

    public Prenda Prenda { get; private set; } = null!;

    public static FotoPrenda Crear(Guid prendaId, string url, int orden)
    {
        return new FotoPrenda
        {
            Id = Guid.NewGuid(),
            PrendaId = prendaId,
            Url = url,
            Orden = orden
        };
    }
}
