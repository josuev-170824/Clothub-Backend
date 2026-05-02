using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class EtiquetaConfiguration : IEntityTypeConfiguration<Etiqueta>
{
    public void Configure(EntityTypeBuilder<Etiqueta> builder)
    {
        builder.ToTable("etiquetas");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.Nombre)
            .IsUnique();
    }
}
