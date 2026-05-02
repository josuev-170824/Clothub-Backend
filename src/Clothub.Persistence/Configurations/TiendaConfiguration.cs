using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class TiendaConfiguration : IEntityTypeConfiguration<Tienda>
{
    public void Configure(EntityTypeBuilder<Tienda> builder)
    {
        builder.ToTable("tiendas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id");

        builder.Property(t => t.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(t => t.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.LogoUrl)
            .HasColumnName("logo_url")
            .HasMaxLength(500);

        builder.Property(t => t.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(1000);

        builder.Property(t => t.Provincia)
            .HasColumnName("provincia")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Telefono)
            .HasColumnName("telefono")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.TelefonoVerificado)
            .HasColumnName("telefono_verificado")
            .HasDefaultValue(false);

        builder.Property(t => t.Instagram)
            .HasColumnName("instagram")
            .HasMaxLength(100);

        builder.Property(t => t.WhatsApp)
            .HasColumnName("whatsapp")
            .HasMaxLength(20);

        builder.Property(t => t.ReglasDeLaTienda)
            .HasColumnName("reglas_de_la_tienda")
            .HasMaxLength(2000);

        builder.Property(t => t.AceptaApartados)
            .HasColumnName("acepta_apartados")
            .HasDefaultValue(false);

        builder.Property(t => t.PorcentajeApartado)
            .HasColumnName("porcentaje_apartado")
            .HasPrecision(5, 2);

        builder.Property(t => t.TiempoLimiteApartadoHoras)
            .HasColumnName("tiempo_limite_apartado_horas");

        builder.Property(t => t.Plan)
            .HasColumnName("plan")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.StripeAccountId)
            .HasColumnName("stripe_account_id")
            .HasMaxLength(100);

        builder.Property(t => t.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        builder.HasOne(t => t.Usuario)
            .WithOne(u => u.Tienda)
            .HasForeignKey<Tienda>(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
