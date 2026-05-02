using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.CompradorId)
            .HasColumnName("comprador_id")
            .IsRequired();

        builder.Property(p => p.TiendaId)
            .HasColumnName("tienda_id")
            .IsRequired();

        builder.Property(p => p.PrendaId)
            .HasColumnName("prenda_id")
            .IsRequired();

        builder.Property(p => p.MontoTotal)
            .HasColumnName("monto_total")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.MontoVendedor)
            .HasColumnName("monto_vendedor")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.Comision)
            .HasColumnName("comision")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.TipoEnvio)
            .HasColumnName("tipo_envio")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.CostoEnvio)
            .HasColumnName("costo_envio")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(p => p.Provincia)
            .HasColumnName("provincia")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Canton)
            .HasColumnName("canton")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Distrito)
            .HasColumnName("distrito")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.DireccionExacta)
            .HasColumnName("direccion_exacta")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.NombreDestinatario)
            .HasColumnName("nombre_destinatario")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.TelefonoDestinatario)
            .HasColumnName("telefono_destinatario")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.CodigoRastreo)
            .HasColumnName("codigo_rastreo")
            .HasMaxLength(100);

        builder.Property(p => p.StripePaymentIntentId)
            .HasColumnName("stripe_payment_intent_id")
            .HasMaxLength(100);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        builder.HasOne(p => p.Comprador)
            .WithMany(u => u.Pedidos)
            .HasForeignKey(p => p.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Tienda)
            .WithMany(t => t.Pedidos)
            .HasForeignKey(p => p.TiendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Prenda)
            .WithMany(pr => pr.Pedidos)
            .HasForeignKey(p => p.PrendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.CompradorId);
        builder.HasIndex(p => p.TiendaId);
        builder.HasIndex(p => p.Estado);
    }
}
