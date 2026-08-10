using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Infrastructure.Configurations;

public class PresentacionConfiguration : IEntityTypeConfiguration<Presentacion>
{
    public void Configure(EntityTypeBuilder<Presentacion> builder)
    {
        builder.ToTable("Presentaciones");

        builder.HasKey(p => p.Id);
        builder.HasQueryFilter(p => !p.IsDeleted);

        // Relacion uno a muchos: Una presentacion puede tener muchas secciones
        builder.HasMany(p => p.Secciones)
            .WithOne(s => s.Presentacion)
            .HasForeignKey(s => s.PresentacionId)
            .OnDelete(DeleteBehavior.Restrict); 

        // Relacion uno a uno: Una presentacion puede tener una retrospectiva
        builder.HasOne(p => p.Retrospectiva)
            .WithOne(r => r.Presentacion)
            .HasForeignKey<Retrospectiva>(r => r.PresentacionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}