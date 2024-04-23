using Microsoft.Azure.Cosmos;
using MvcCosmosAzure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration
    .GetConnectionString("CosmosDb");
string databaseName = builder.Configuration
    .GetValue<string>("CochesCosmosDb:Database");
string containerName = builder.Configuration
    .GetValue<string>("CochesCosmosDb:Container");
// CREAMOS EL CLIENTE DE COSMOS
CosmosClient client = new CosmosClient(connectionString);
Container container = client
    .GetContainer(databaseName, containerName);
builder.Services.AddSingleton<CosmosClient>(x => client);
builder.Services.AddTransient<Container>(x => container);
builder.Services.AddTransient<ServiceCosmosDb>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
