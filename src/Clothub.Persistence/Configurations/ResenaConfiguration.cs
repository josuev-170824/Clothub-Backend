using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class ResenaConfiguration : IEntityTypeConfiguration<Resena>
{
    public void Configure(EntityTypeBuilder<Resena> builder)
    {
        builder.ToTable("resenas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder.Property(r => r.CompradorId)
            .HasColumnName("comprador_id")
            .IsRequired();

        builder.Property(r => r.TiendaId)
            .HasColumnName("tienda_id")
            .IsRequired();

        builder.Property(r => r.PedidoId)
            .HasColumnName("pedido_id")
            .IsRequired();

        builder.Property(r => r.Calificacion)
            .HasColumnName("calificacion")
            .IsRequired();

        builder.Property(r => r.Comentario)
            .HasColumnName("comentario")
            .HasMaxLength(1000);

        builder.Property(r => r.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        // Una compra solo permite una reseña (RF-48)
        builder.HasIndex(r => r.PedidoId)
            .IsUnique();

        builder.HasOne(r => r.Comprador)
            .WithMany(u => u.Resenas)
            .HasForeignKey(r => r.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Tienda)
            .WithMany(t => t.Resenas)
            .HasForeignKey(r => r.TiendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Pedido)
            .WithOne(p => p.Resena)
            .HasForeignKey<Resena>(r => r.PedidoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
