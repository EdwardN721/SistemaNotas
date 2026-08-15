using System.Reflection;
using SistemaNotas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SistemaNotas.Infrastructure.Data;

public class NotasDbContext : DbContext
{
    public NotasDbContext(DbContextOptions<NotasDbContext> options) : base(options)
    { }

    public DbSet<Presentacion> Presentaciones => Set<Presentacion>();
    public DbSet<Seccion> Secciones => Set<Seccion>();
    public DbSet<Ancla> Anclas => Set<Ancla>();
    public DbSet<Retrospectiva> Retrospectivas => Set<Retrospectiva>();
    public DbSet<CategoriaAncla> CategoriasAncla => Set<CategoriaAncla>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esto busca automáticamente todas las clases que hereden de IEntityTypeConfiguration 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}