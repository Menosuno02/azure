using ApiNetCoreHospitales.Data;
using ApiNetCoreHospitales.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryHospitales>();
builder.Services.AddDbContext<HospitalContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// A este m�todo le podemos indicar varias opciones
// Desde el t�tulo del API hasta quien lo ha hecho
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Hospitales de viernes",
        Description = "API realizada el viernes 05/04/2024",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Alejo Tajamar 2024",
            Email = "prueba@mail.com"
        }
    });
});

var app = builder.Build();

app.UseSwagger();
// Esto tiene que ver con el comportamiento de la p�gina Swagger
app.UseSwaggerUI(options =>
{
    // Indicamos d�nde est� el endpoint de Open API
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "API v1"
    );
    // Indicamos que index ser� la p�gina principal de nuestro API
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
