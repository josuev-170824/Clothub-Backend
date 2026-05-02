using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class FotoPrendaConfiguration : IEntityTypeConfiguration<FotoPrenda>
{
    public void Configure(EntityTypeBuilder<FotoPrenda> builder)
    {
        builder.ToTable("fotos_prenda");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("id");

        builder.Property(f => f.PrendaId)
            .HasColumnName("prenda_id")
            .IsRequired();

        builder.Property(f => f.Url)
            .HasColumnName("url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(f => f.Orden)
            .HasColumnName("orden")
            .IsRequired();

        builder.HasOne(f => f.Prenda)
            .WithMany(p => p.Fotos)
            .HasForeignKey(f => f.PrendaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Garantiza que el orden (1-3) sea único por prenda
        builder.HasIndex(f => new { f.PrendaId, f.Orden })
            .IsUnique();
    }
}
