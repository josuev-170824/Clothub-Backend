using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
{
    public void Configure(EntityTypeBuilder<Prenda> builder)
    {
        builder.ToTable("prendas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.TiendaId)
            .HasColumnName("tienda_id")
            .IsRequired();

        builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Marca)
            .HasColumnName("marca")
            .HasMaxLength(100);

        builder.Property(p => p.TipoPrenda)
            .HasColumnName("tipo_prenda")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Talla)
            .HasColumnName("talla")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.MedidaCintura)
            .HasColumnName("medida_cintura")
            .HasPrecision(6, 2);

        builder.Property(p => p.MedidaLargo)
            .HasColumnName("medida_largo")
            .HasPrecision(6, 2);

        builder.Property(p => p.MedidaAncho)
            .HasColumnName("medida_ancho")
            .HasPrecision(6, 2);

        builder.Property(p => p.Condicion)
            .HasColumnName("condicion")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(1000);

        builder.Property(p => p.Precio)
            .HasColumnName("precio")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Vistas)
            .HasColumnName("vistas")
            .HasDefaultValue(0);

        builder.Property(p => p.FechaPublicacion)
            .HasColumnName("fecha_publicacion")
            .IsRequired();

        builder.HasOne(p => p.Tienda)
            .WithMany(t => t.Prendas)
            .HasForeignKey(p => p.TiendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.TiendaId);
        builder.HasIndex(p => p.Estado);
        builder.HasIndex(p => p.FechaPublicacion);
    }
}
