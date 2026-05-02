using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class SeguimientoConfiguration : IEntityTypeConfiguration<Seguimiento>
{
    public void Configure(EntityTypeBuilder<Seguimiento> builder)
    {
        builder.ToTable("seguimientos");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id");

        builder.Property(s => s.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(s => s.TiendaId)
            .HasColumnName("tienda_id");

        builder.Property(s => s.EtiquetaId)
            .HasColumnName("etiqueta_id");

        builder.Property(s => s.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        // Un usuario solo puede seguir la misma tienda una vez
        builder.HasIndex(s => new { s.UsuarioId, s.TiendaId })
            .IsUnique()
            .HasFilter("tienda_id IS NOT NULL");

        // Un usuario solo puede seguir la misma etiqueta una vez
        builder.HasIndex(s => new { s.UsuarioId, s.EtiquetaId })
            .IsUnique()
            .HasFilter("etiqueta_id IS NOT NULL");

        builder.HasOne(s => s.Usuario)
            .WithMany(u => u.Seguimientos)
            .HasForeignKey(s => s.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Tienda)
            .WithMany(t => t.Seguimientos)
            .HasForeignKey(s => s.TiendaId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(s => s.Etiqueta)
            .WithMany(e => e.Seguimientos)
            .HasForeignKey(s => s.EtiquetaId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}
