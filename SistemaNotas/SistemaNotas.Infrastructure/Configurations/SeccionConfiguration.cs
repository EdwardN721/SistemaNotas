using SistemaNotas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaNotas.Infrastructure.Configurations;

public class SeccionConfiguration : IEntityTypeConfiguration<Seccion>
{
    public void Configure(EntityTypeBuilder<Seccion> builder)
    {
        builder.ToTable("Secciones");
        builder.HasKey(s => s.Id);
        builder.HasQueryFilter(s => !s.IsDeleted);

        // Relación uno a Muchos: Una Sección tiene muchas Anclas
        builder.HasMany(s => s.Anclas)
               .WithOne(a => a.Seccion)
               .HasForeignKey(a => a.SeccionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}