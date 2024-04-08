using ProyectoNugetCoches;

Console.WriteLine("Hellou");

Garaje g = new Garaje();
List<Coche> cars = g.GetCoches();
foreach (Coche car in cars)
{
    Console.WriteLine(car.Marca + " " + car.Modelo);
}
Console.WriteLine("Fin del programa");