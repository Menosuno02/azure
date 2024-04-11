using Microsoft.AspNetCore.Mvc;
using MvcApiClientOAuth.Models;
using MvcApiClientOAuth.Services;

namespace MvcApiClientOAuth.Controllers
{

    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleados service;

        public EmpleadosController(ServiceApiEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados =
                await this.service.GetEmpleadosAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Tendremos el token en Session
            string token = HttpContext.Session.GetString("TOKEN");
            if (token == null)
            {
                ViewData["MENSAJE"] = "Debe validarse en Login";
                return View();
            }
            else
            {
                Empleado empleado =
                await this.service.FindEmpleadoAsync(id, token);
                return View(empleado);
            }
        }
    }
}
