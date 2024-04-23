using Microsoft.AspNetCore.Mvc;
using MvcCosmosAzure.Models;
using MvcCosmosAzure.Services;

namespace MvcCosmosAzure.Controllers
{
    public class CochesController : Controller
    {
        private ServiceCosmosDb service;

        public CochesController(ServiceCosmosDb service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string accion)
        {
            await this.service.CreateDatabaseAsync();
            ViewData["MENSAJE"] = "Recursos creados en Azure Cosmos";
            return View();
        }

        public async Task<IActionResult> Vehiculos()
        {
            List<Vehiculo> cars =
                await this.service.GetVehiculosAsync();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehiculo car, string existemotor)
        {
            if (existemotor == null)
            {
                car.Motor = null;
            }
            await this.service.InsertVehiculoAsync(car);
            return RedirectToAction("Vehiculos");
        }

        public async Task<IActionResult> Details(string id)
        {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteVehiculoAsync(id);
            return RedirectToAction("Vehiculos");
        }

        public async Task<IActionResult> Edit(string id)
        {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehiculo car)
        {
            await this.service.UpdateVehiculoAsync(car);
            return RedirectToAction("Vehiculos");
        }

        public IActionResult Buscador()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscador(string marca)
        {
            List<Vehiculo> cars =
                await this.service.GetVehiculosMarcaAsync(marca);
            return View(cars);
        }
    }
}
