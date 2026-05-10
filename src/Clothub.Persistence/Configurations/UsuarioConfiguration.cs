using Clothub.Domain.Entities;
using Clothub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Apellidos)
            .HasColumnName("apellidos")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(255);

        builder.Property(u => u.EmailVerificado)
            .HasColumnName("email_verificado")
            .HasDefaultValue(false);

        builder.Property(u => u.TokenVerificacionEmail)
            .HasColumnName("token_verificacion_email")
            .HasMaxLength(100);

        builder.Property(u => u.FechaExpiracionTokenVerificacion)
            .HasColumnName("fecha_expiracion_token_verificacion");

        builder.Property(u => u.IntentosFallidosVerificacion)
            .HasColumnName("intentos_fallidos_verificacion")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(u => u.TokenRecuperacionPassword)
            .HasColumnName("token_recuperacion_password")
            .HasMaxLength(64);

        builder.Property(u => u.FechaExpiracionTokenRecuperacion)
            .HasColumnName("fecha_expiracion_token_recuperacion");

        builder.Property(u => u.ProveedorAuth)
            .HasColumnName("proveedor_auth")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.GoogleId)
            .HasColumnName("google_id")
            .HasMaxLength(100);

        builder.HasIndex(u => u.GoogleId)
            .IsUnique()
            .HasFilter("google_id IS NOT NULL");

        builder.Property(u => u.Rol)
            .HasColumnName("rol")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.FechaRegistro)
            .HasColumnName("fecha_registro")
            .IsRequired();
    }
}
