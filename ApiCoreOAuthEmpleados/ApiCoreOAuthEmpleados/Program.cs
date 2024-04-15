using ApiCoreOAuthEmpleados.Data;
using ApiCoreOAuthEmpleados.Helpers;
using ApiCoreOAuthEmpleados.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors.Security;
using NSwag;

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
/*
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Oauth Empleados",
        Description = "API con token de seguridad"
    });
});
*/
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api OAuth Empleados";
    document.Description = "API con seguridad 2024";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(
    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});


var app = builder.Build();
app.UseOpenApi();
// app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/monokai_theme.css");
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "API OAuth Empleados");
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
