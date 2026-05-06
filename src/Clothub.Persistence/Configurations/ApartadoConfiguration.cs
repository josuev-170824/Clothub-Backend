using Clothub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clothub.Persistence.Configurations;

public class ApartadoConfiguration : IEntityTypeConfiguration<Apartado>
{
    public void Configure(EntityTypeBuilder<Apartado> builder)
    {
        builder.ToTable("apartados");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id");

        builder.Property(a => a.CompradorId)
            .HasColumnName("comprador_id")
            .IsRequired();

        builder.Property(a => a.PrendaId)
            .HasColumnName("prenda_id")
            .IsRequired();

        builder.Property(a => a.MontoPagado)
            .HasColumnName("monto_pagado")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(a => a.MontoRestante)
            .HasColumnName("monto_restante")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(a => a.FechaLimite)
            .HasColumnName("fecha_limite")
            .IsRequired();

        builder.Property(a => a.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        builder.HasOne(a => a.Comprador)
            .WithMany(u => u.Apartados)
            .HasForeignKey(a => a.CompradorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Prenda)
            .WithMany(p => p.Apartados)
            .HasForeignKey(a => a.PrendaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Solo puede haber un apartado activo por prenda a la vez
        builder.HasIndex(a => new { a.PrendaId, a.Estado })
            .HasFilter("estado = 'Activo'")
            .IsUnique();
    }
}
