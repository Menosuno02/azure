using ApiAzureStorageTokens.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ServiceSasToken>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Aquí se mapean los métodos necesarios
app.MapGet("/testapi", () =>
{
    return "Testing Minimal Api";
});

// Queremos un método para generar el token y
// que reciba un curso
// Lógicamente no podemos utilizar las palabras [action]
// o [controller]
// Necesitamos el service y tenemos dos formas de
// recuperarlo dentro del método buscando
// el servicio con Services utilizando la inyección
app.MapGet("/token/{curso}", (string curso,
    ServiceSasToken service) =>
{
    string token = service.GenerateToken(curso);
    return new { token = token };
});







var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
