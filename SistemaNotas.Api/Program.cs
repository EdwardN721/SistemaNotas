using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SistemaNotas.Api.Extensions;
using SistemaNotas.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
string corsPolicyName = "SistemaNotasCorsPolicy"; // Le damos un nombre a nuestra política

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Agregar Base de datos
builder.Services.AddDbContextConfig(builder.Configuration);

// Agregar UnitOfWork
builder.Services.AddUnitOfWorkConfig();

// Agregar HandleExceptiom
builder.Services.AddGlobalExceptionHandler();

// Registramos el CORS
builder.Services.AddCorsConfiguration(builder.Configuration, corsPolicyName);

// Registramos nuestro servicio JWT
builder.Services.AddJwtAuthenticationConfig(builder.Configuration);

// Registramos la versión de la API y la documentación
builder.Services.AddApiVersioningAndDocs();

// Registramos los servicios de la capa de aplicación
builder.Services.AddServicesConfiguration();

var app = builder.Build();

// SECCIÓN DE MIGRACIÓN AUTOMÁTICA Y SEED
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Obtenemos el DbContext de nuestra aplicación
        var context = services.GetRequiredService<NotasDbContext>();
        
        // Ejecuta todas las migraciones pendientes y aplica el Seed de Datos
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al migrar o inicializar la base de datos.");
    }
}


// Aplicamos las migraciones pendientes al iniciar la aplicación
app.ApplyPendingMigrations();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // Mapea el JSON de OpenAPI
    app.MapOpenApi(); 
    
    // Mapea la interfaz visual de Scalar
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Sistema Notas API")
               .WithTheme(ScalarTheme.DeepSpace) 
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseCors(corsPolicyName);
app.UseHttpsRedirection();
app.UseAuthentication(); // "¿Quién eres?" (Valida el Token)
app.UseAuthorization();  // "¿Puedes pasar?" (Valida el Permiso)
app.MapControllers();

app.Run();