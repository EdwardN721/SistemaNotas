using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaNotas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CategoriasAncla",
                columns: new[] { "Id", "Activo", "CodigoColor", "CreatedAt", "IsDeleted", "Nombre", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("cc111111-1111-1111-1111-111111111111"), true, "#3498db", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Concepto Clave", null },
                    { new Guid("cc222222-2222-2222-2222-222222222222"), true, "#e74c3c", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Demo / Código en Vivo", null },
                    { new Guid("cc333333-3333-3333-3333-333333333333"), true, "#f1c40f", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Pregunta a Audiencia", null },
                    { new Guid("cc444444-4444-4444-4444-444444444444"), true, "#2ecc71", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Alerta de Tiempo", null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Nombre", "PasswordHash", "UpdatedAt" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ed@mymail.com", false, "Eduardo Campos", "$2a$11$H5...hash_simulado_seguro...", null });

            migrationBuilder.InsertData(
                table: "Presentaciones",
                columns: new[] { "Id", "Audiencia", "CreatedAt", "FechaExposicion", "IsDeleted", "Titulo", "UpdatedAt", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("aa111111-1111-1111-1111-111111111111"), "Trainees", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 8, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Aprender C# desde cero: Fundamentos", null, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("aa222222-2222-2222-2222-222222222222"), "Frontend Devs", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 9, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Angular con Signals y Standalone", null, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("aa333333-3333-3333-3333-333333333333"), "Arquitectos", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Implementación de LlaveMX en Gobierno", null, new Guid("11111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Retrospectivas",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "MuletillasDetectadas", "NivelNerviosismo", "PresentacionId", "QueSalioBien", "UpdatedAt" },
                values: new object[] { new Guid("ee100001-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "[\"este\",\"entonces\"]", 3, new Guid("aa111111-1111-1111-1111-111111111111"), "La interacción con los juniors al explicar Clean Architecture fue muy fluida.", null });

            migrationBuilder.InsertData(
                table: "Secciones",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "MinutosEstimados", "Orden", "PresentacionId", "TituloSeccion", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("bb100001-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 15, 1, new Guid("aa111111-1111-1111-1111-111111111111"), "Introducción al CLR", null },
                    { new Guid("bb300001-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 15, 1, new Guid("aa333333-3333-3333-3333-333333333333"), "Marco Normativo OIDC", null },
                    { new Guid("bb300002-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 30, 2, new Guid("aa333333-3333-3333-3333-333333333333"), "Login y Tokens en .NET Core", null }
                });

            migrationBuilder.InsertData(
                table: "Anclas",
                columns: new[] { "Id", "CategoriaId", "ConceptoClave", "CreatedAt", "IsDeleted", "Orden", "RecordatorioVisual", "SeccionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("ff100001-1111-1111-1111-111111111111"), new Guid("cc111111-1111-1111-1111-111111111111"), "Diferencias en memoria: Stack vs Heap", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 1, false, new Guid("bb100001-1111-1111-1111-111111111111"), null },
                    { new Guid("ff300001-3333-3333-3333-333333333333"), new Guid("cc111111-1111-1111-1111-111111111111"), "Enfatizar interoperabilidad entre dependencias", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 1, false, new Guid("bb300001-3333-3333-3333-333333333333"), null },
                    { new Guid("ff300002-3333-3333-3333-333333333333"), new Guid("cc222222-2222-2222-2222-222222222222"), "Abrir Postman para mostrar el intercambio del JWT", new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 1, true, new Guid("bb300002-3333-3333-3333-333333333333"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Anclas",
                keyColumn: "Id",
                keyValue: new Guid("ff100001-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Anclas",
                keyColumn: "Id",
                keyValue: new Guid("ff300001-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Anclas",
                keyColumn: "Id",
                keyValue: new Guid("ff300002-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "CategoriasAncla",
                keyColumn: "Id",
                keyValue: new Guid("cc333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "CategoriasAncla",
                keyColumn: "Id",
                keyValue: new Guid("cc444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Presentaciones",
                keyColumn: "Id",
                keyValue: new Guid("aa222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Retrospectivas",
                keyColumn: "Id",
                keyValue: new Guid("ee100001-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "CategoriasAncla",
                keyColumn: "Id",
                keyValue: new Guid("cc111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "CategoriasAncla",
                keyColumn: "Id",
                keyValue: new Guid("cc222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Secciones",
                keyColumn: "Id",
                keyValue: new Guid("bb100001-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Secciones",
                keyColumn: "Id",
                keyValue: new Guid("bb300001-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Secciones",
                keyColumn: "Id",
                keyValue: new Guid("bb300002-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Presentaciones",
                keyColumn: "Id",
                keyValue: new Guid("aa111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Presentaciones",
                keyColumn: "Id",
                keyValue: new Guid("aa333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
