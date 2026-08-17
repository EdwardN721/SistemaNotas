using Microsoft.EntityFrameworkCore;
using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // ==========================================================
        // 1. GUIDs HEXADECIMALES VÁLIDOS (Solo 0-9 y A-F)
        // ==========================================================
        var usuarioId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Categorías (Prefijo CC)
        var catConceptoId = Guid.Parse("CC111111-1111-1111-1111-111111111111");
        var catDemoId = Guid.Parse("CC222222-2222-2222-2222-222222222222");
        var catPreguntaId = Guid.Parse("CC333333-3333-3333-3333-333333333333");
        var catTiempoId = Guid.Parse("CC444444-4444-4444-4444-444444444444");

        // Presentaciones (Prefijo AA)
        var presCsharpId = Guid.Parse("AA111111-1111-1111-1111-111111111111");
        var presAngularId = Guid.Parse("AA222222-2222-2222-2222-222222222222");
        var presLlaveMxId = Guid.Parse("AA333333-3333-3333-3333-333333333333");

        // Secciones (Prefijo BB)
        var secCsharp1Id = Guid.Parse("BB100001-1111-1111-1111-111111111111");
        var secLlave1Id = Guid.Parse("BB300001-3333-3333-3333-333333333333");
        var secLlave2Id = Guid.Parse("BB300002-3333-3333-3333-333333333333");

        // Retrospectiva (Prefijo EE)
        var retroCsharpId = Guid.Parse("EE100001-1111-1111-1111-111111111111");

        var fechaActual = new DateTimeOffset(2026, 8, 16, 12, 0, 0, TimeSpan.Zero);

        // ==========================================================
        // 2. USUARIO
        // ==========================================================
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = usuarioId,
                Nombre = "Eduardo Campos",
                Email = "ed@mymail.com",
                PasswordHash = "$2a$11$e7K4Vom.uK7M2wYQWb1nIe05g8l27S.U0fU98t29vE4F02j8pQ1iC",
                CreatedAt = fechaActual
            }
        );

        // ==========================================================
        // 3. CATEGORÍAS
        // ==========================================================
        modelBuilder.Entity<CategoriaAncla>().HasData(
            new CategoriaAncla { Id = catConceptoId, Nombre = "Concepto Clave", CodigoColor = "#3498db", Activo = true, CreatedAt = fechaActual },
            new CategoriaAncla { Id = catDemoId, Nombre = "Demo / Código en Vivo", CodigoColor = "#e74c3c", Activo = true, CreatedAt = fechaActual },
            new CategoriaAncla { Id = catPreguntaId, Nombre = "Pregunta a Audiencia", CodigoColor = "#f1c40f", Activo = true, CreatedAt = fechaActual },
            new CategoriaAncla { Id = catTiempoId, Nombre = "Alerta de Tiempo", CodigoColor = "#2ecc71", Activo = true, CreatedAt = fechaActual }
        );

        // ==========================================================
        // 4. PRESENTACIONES
        // ==========================================================
        modelBuilder.Entity<Presentacion>().HasData(
            new Presentacion { Id = presCsharpId, UsuarioId = usuarioId, Titulo = "Aprender C# desde cero: Fundamentos", Audiencia = "Trainees", FechaExposicion = fechaActual.AddDays(10), CreatedAt = fechaActual },
            new Presentacion { Id = presAngularId, UsuarioId = usuarioId, Titulo = "Angular con Signals y Standalone", Audiencia = "Frontend Devs", FechaExposicion = fechaActual.AddDays(20), CreatedAt = fechaActual },
            new Presentacion { Id = presLlaveMxId, UsuarioId = usuarioId, Titulo = "Implementación de LlaveMX en Gobierno", Audiencia = "Arquitectos", FechaExposicion = fechaActual.AddDays(30), CreatedAt = fechaActual }
        );

        // ==========================================================
        // 5. SECCIONES
        // ==========================================================
        modelBuilder.Entity<Seccion>().HasData(
            new Seccion { Id = secCsharp1Id, PresentacionId = presCsharpId, Orden = 1, TituloSeccion = "Introducción al CLR", MinutosEstimados = 15, CreatedAt = fechaActual },
            new Seccion { Id = secLlave1Id, PresentacionId = presLlaveMxId, Orden = 1, TituloSeccion = "Marco Normativo OIDC", MinutosEstimados = 15, CreatedAt = fechaActual },
            new Seccion { Id = secLlave2Id, PresentacionId = presLlaveMxId, Orden = 2, TituloSeccion = "Login y Tokens en .NET Core", MinutosEstimados = 30, CreatedAt = fechaActual }
        );

        // ==========================================================
        // 6. ANCLAS
        // ==========================================================
        modelBuilder.Entity<Ancla>().HasData(
            new Ancla { Id = Guid.Parse("FF100001-1111-1111-1111-111111111111"), SeccionId = secCsharp1Id, CategoriaId = catConceptoId, Orden = 1, ConceptoClave = "Diferencias en memoria: Stack vs Heap", RecordatorioVisual = false, CreatedAt = fechaActual },
            new Ancla { Id = Guid.Parse("FF300001-3333-3333-3333-333333333333"), SeccionId = secLlave1Id, CategoriaId = catConceptoId, Orden = 1, ConceptoClave = "Enfatizar interoperabilidad entre dependencias", RecordatorioVisual = false, CreatedAt = fechaActual },
            new Ancla { Id = Guid.Parse("FF300002-3333-3333-3333-333333333333"), SeccionId = secLlave2Id, CategoriaId = catDemoId, Orden = 1, ConceptoClave = "Abrir Postman para mostrar el intercambio del JWT", RecordatorioVisual = true, CreatedAt = fechaActual }
        );

        // ==========================================================
        // 7. RETROSPECTIVAS
        // ==========================================================
        modelBuilder.Entity<Retrospectiva>().HasData(
            new Retrospectiva 
            { 
                Id = retroCsharpId, 
                PresentacionId = presCsharpId, 
                NivelNerviosismo = 3, 
                MuletillasDetectadas = new List<string> { "este", "entonces" }, 
                QueSalioBien = "La interacción con los juniors al explicar Clean Architecture fue muy fluida.", 
                CreatedAt = fechaActual 
            }
        );
    }
}