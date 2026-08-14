using SistemaNotas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaNotas.Infrastructure.Configurations;

public class AnclaConfiguration : IEntityTypeConfiguration<Ancla>
{
    public void Configure(EntityTypeBuilder<Ancla> builder)
    {
        builder.ToTable("Anclas");
        builder.HasKey(a => a.Id);
        builder.HasQueryFilter(a => !a.IsDeleted);

        builder.Property(a => a.ConceptoClave)
                .HasMaxLength(100).IsRequired();

        // Relación con CategoriaAncla (Muchos a uno)
        builder.HasOne(a => a.Categoria)
               .WithMany(c => c.Anclas)
               .HasForeignKey(a => a.CategoriaId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}