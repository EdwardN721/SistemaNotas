using Scalar.AspNetCore;
using SistemaNotas.Api.Extensions;

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

var app = builder.Build();

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