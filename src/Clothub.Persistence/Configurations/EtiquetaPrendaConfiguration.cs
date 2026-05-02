using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class EtiquetaPrendaConfiguration : IEntityTypeConfiguration<EtiquetaPrenda>
{
    public void Configure(EntityTypeBuilder<EtiquetaPrenda> builder)
    {
        builder.ToTable("etiquetas_prenda");

        builder.HasKey(ep => new { ep.PrendaId, ep.EtiquetaId });

        builder.Property(ep => ep.PrendaId)
            .HasColumnName("prenda_id");

        builder.Property(ep => ep.EtiquetaId)
            .HasColumnName("etiqueta_id");

        builder.HasOne(ep => ep.Prenda)
            .WithMany(p => p.Etiquetas)
            .HasForeignKey(ep => ep.PrendaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ep => ep.Etiqueta)
            .WithMany(e => e.Prendas)
            .HasForeignKey(ep => ep.EtiquetaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
