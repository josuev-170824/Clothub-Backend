using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class SuscripcionConfiguration : IEntityTypeConfiguration<Suscripcion>
{
    public void Configure(EntityTypeBuilder<Suscripcion> builder)
    {
        builder.ToTable("suscripciones");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id");

        builder.Property(s => s.TiendaId)
            .HasColumnName("tienda_id")
            .IsRequired();

        builder.Property(s => s.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.FechaInicioPrueba)
            .HasColumnName("fecha_inicio_prueba")
            .IsRequired();

        builder.Property(s => s.FechaFinPrueba)
            .HasColumnName("fecha_fin_prueba")
            .IsRequired();

        builder.Property(s => s.FechaInicioSuscripcion)
            .HasColumnName("fecha_inicio_suscripcion");

        builder.Property(s => s.FechaVencimiento)
            .HasColumnName("fecha_vencimiento");

        builder.Property(s => s.OnvoSubscriptionId)
            .HasColumnName("onvo_subscription_id")
            .HasMaxLength(100);

        builder.HasOne(s => s.Tienda)
            .WithOne(t => t.Suscripcion)
            .HasForeignKey<Suscripcion>(s => s.TiendaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
