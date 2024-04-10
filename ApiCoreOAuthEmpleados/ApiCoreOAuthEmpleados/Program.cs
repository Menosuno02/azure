using ApiCoreOAuthEmpleados.Data;
using ApiCoreOAuthEmpleados.Helpers;
using ApiCoreOAuthEmpleados.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Creamos una instancia del Helper
HelperActionServicesOAuth helper =
    new HelperActionServicesOAuth(builder.Configuration);
// Esta instancia debemos incluirla dentro de nuestra
// aplicación solamente una vez, para que todo lo que
// hemos creado dentro no se genere de nuevo
builder.Services
    .AddSingleton<HelperActionServicesOAuth>(helper);
// Habilitamos los servicios de Authentication que
// hemos creado en el helper con Action<>
builder.Services.AddAuthentication
    (helper.GetAuthenticateSchema())
    .AddJwtBearer(helper.GetJwtBearerOptions());

// Add services to the container.

string connectionString =
    builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryEmpleados>();
builder.Services.AddDbContext<HospitalContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Oauth Empleados",
        Description = "API con token de seguridad"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "API OAuth Empleados");
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
