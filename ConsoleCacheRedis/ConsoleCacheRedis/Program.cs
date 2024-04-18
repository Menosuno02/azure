using ConsoleCacheRedis;

Console.WriteLine("Test Cache Redis");

ServiceCacheRedis service = new ServiceCacheRedis();
List<Producto> favoritos =
    await service.GetProductosFavoritosAsync();

if (favoritos == null)
{
    Console.WriteLine("No tenemos favoritos");
}
else
{
    int i = 1;
    foreach (Producto prod in favoritos)
    {
        Console.WriteLine(i + " - " + prod.Nombre);
        i++;
    }
}
Console.WriteLine("Fin del programa");