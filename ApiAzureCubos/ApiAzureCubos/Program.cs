using NSwag.Generation.Processors.Security;
using NSwag;
using ApiAzureCubos.Helpers;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Azure;
using ApiAzureCubos.Repositories;
using ApiAzureCubos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
        (builder.Configuration.GetSection("KeyVault"));
});


SecretClient secretClient = builder.Services.BuildServiceProvider()
    .GetService<SecretClient>();
KeyVaultSecret secretConnectionString = await secretClient.GetSecretAsync("ConnectionString");
string connectionString = secretConnectionString.Value;

HelperActionServicesOAuth helper =
    new HelperActionServicesOAuth(secretClient);
builder.Services
    .AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication
    (helper.GetAuthenticateSchema())
    .AddJwtBearer(helper.GetJwtBearerOptions());

builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Cubos";
    document.Description = "Api Cubos Examen";
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
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "API v1"
    );
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
