using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcCoreMicrosoftAD.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlLocal");
string appID = builder.Configuration.GetValue<string>
    ("Authentication:Microsoft:ApplicationID");
string secretKey = builder.Configuration.GetValue<string>
    ("Authentication:Microsoft:SecretKey");
builder.Services.AddDbContext<ApplicationContext>
    (options => options.UseSqlServer(connectionString));
// Indicamos que utilizaremos un usuario IdentityUser
// dentro de nuestra app y que lo administrará nuestro context
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationContext>();
// Si queremos utilizar distintos proveedores es aquí
// dónde los iremos dando de alta
builder.Services.AddAuthentication()
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = appID;
        options.ClientSecret = secretKey;
    });

// Como vamos a utilizar rutas personalizadas
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false);

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

app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute
    (name: "default",
    template: "{controller=Home}/{action=Index}/{id?}");
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
