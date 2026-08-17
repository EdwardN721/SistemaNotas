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
            migrationBuilder.CreateTable(
                name: "CategoriasAncla",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasAncla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presentaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaExposicion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Audiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presentaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retrospectivas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresentacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NivelNerviosismo = table.Column<int>(type: "int", nullable: false),
                    MuletillasDetectadas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueSalioBien = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retrospectivas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retrospectivas_Presentaciones_PresentacionId",
                        column: x => x.PresentacionId,
                        principalTable: "Presentaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresentacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    TituloSeccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinutosEstimados = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secciones_Presentaciones_PresentacionId",
                        column: x => x.PresentacionId,
                        principalTable: "Presentaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anclas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeccionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    ConceptoClave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecordatorioVisual = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anclas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anclas_CategoriasAncla_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasAncla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anclas_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 8, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ed@mymail.com", false, "Eduardo Campos", "$2a$11$e7K4Vom.uK7M2wYQWb1nIe05g8l27S.U0fU98t29vE4F02j8pQ1iC", null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Anclas_CategoriaId",
                table: "Anclas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anclas_SeccionId",
                table: "Anclas",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Presentaciones_UsuarioId",
                table: "Presentaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Retrospectivas_PresentacionId",
                table: "Retrospectivas",
                column: "PresentacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_PresentacionId",
                table: "Secciones",
                column: "PresentacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anclas");

            migrationBuilder.DropTable(
                name: "Retrospectivas");

            migrationBuilder.DropTable(
                name: "CategoriasAncla");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Presentaciones");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
