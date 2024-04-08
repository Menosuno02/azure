using AppChollometroWebJob.Data;
using AppChollometroWebJob.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

string connectionString =
    @"Data Source=sqlalopez2825.database.windows.net;Initial Catalog=AZURETAJAMAR;Persist Security Info=True;User ID=adminsql;Password=Admin123;Encrypt=False;Trust Server Certificate=True";

// Necesitamos utilizar inyección de dependecias
// Para ello debemos crear un provider
var provider = new ServiceCollection()
    .AddTransient<RepositoryChollometro>()
    .AddDbContext<ChollometroContext>
    (options => options.UseSqlServer(connectionString))
    .BuildServiceProvider();

// Mediante el proveedor, ya podemos recuperar nuestro
// repositorio para recuperar el método Populate
RepositoryChollometro repo = provider
    .GetService<RepositoryChollometro>();
await repo.PopulateChollosAzureAsync();