using SistemaNotas.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

string corsPolicyName = "SistemaNotasCorsPolicy"; // Le damos un nombre a nuestra política

// Registramos el CORS
builder.Services.AddCorsConfiguration(builder.Configuration, corsPolicyName);

// Registramos nuestro servicio JWT
builder.Services.AddJwtAuthenticationConfig(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseCors(corsPolicyName);

// Activamos la barrera de seguridad en el pipeline HTTP 
app.UseAuthentication(); // "¿Quién eres?" (Valida el Token)
app.UseAuthorization();  // "¿Puedes pasar?" (Valida el Permiso)

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
