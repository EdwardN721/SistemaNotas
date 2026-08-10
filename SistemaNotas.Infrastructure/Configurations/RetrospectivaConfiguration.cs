using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Infrastructure.Configurations;

public class RetrospectivaConfiguration : IEntityTypeConfiguration<Retrospectiva>
{
    public void Configure(EntityTypeBuilder<Retrospectiva> builder)
    {
        builder.ToTable("Retrospectivas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.NivelNerviosismo)
            .IsRequired();

        builder.Property(r => r.QueSalioBien)
            .HasMaxLength(500);

        builder.Property(r => r.MuletillasDetectadas)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
            )
            .HasColumnType("nvarchar(max)");

        // Filtro global para Soft Delete: Las consultas ignorarán los borrados por defecto
        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}