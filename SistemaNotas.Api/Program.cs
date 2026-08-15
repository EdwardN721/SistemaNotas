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

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{

}
app.UseCors(corsPolicyName);
app.UseHttpsRedirection();
app.UseAuthentication(); // "¿Quién eres?" (Valida el Token)
app.UseAuthorization();  // "¿Puedes pasar?" (Valida el Permiso)
app.MapControllers();

app.Run();