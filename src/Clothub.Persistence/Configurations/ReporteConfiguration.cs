using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class ReporteConfiguration : IEntityTypeConfiguration<Reporte>
{
    public void Configure(EntityTypeBuilder<Reporte> builder)
    {
        builder.ToTable("reportes");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder.Property(r => r.CompradorId)
            .HasColumnName("comprador_id")
            .IsRequired();

        builder.Property(r => r.TiendaId)
            .HasColumnName("tienda_id")
            .IsRequired();

        builder.Property(r => r.Motivo)
            .HasColumnName("motivo")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(r => r.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        builder.HasOne(r => r.Comprador)
            .WithMany(u => u.Reportes)
            .HasForeignKey(r => r.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Tienda)
            .WithMany(t => t.Reportes)
            .HasForeignKey(r => r.TiendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.TiendaId, r.Estado });
        builder.HasIndex(r => r.FechaCreacion);
    }
}
