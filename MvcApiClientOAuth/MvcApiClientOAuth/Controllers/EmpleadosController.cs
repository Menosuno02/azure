using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcApiClientOAuth.Filters;
using MvcApiClientOAuth.Models;
using MvcApiClientOAuth.Services;
using System.Security.Claims;

namespace MvcApiClientOAuth.Controllers
{

    public class EmpleadosController : Controller
    {
        private ServiceApiEmpleados service;

        public EmpleadosController(ServiceApiEmpleados service)
        {
            this.service = service;
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados =
                await this.service.GetEmpleadosAsync();
            return View(empleados);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado =
                await this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }

        // Metodo en el API
        [AuthorizeEmpleados]
        public async Task<Empleado> Perfil()
        {
            // Este método estará protegido y debe recibir el token
            // Lo que debemos hacer es extraer el usuario del
            // propio token
            return null;
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> CompisCurro()
        {
            // Necesito el ID del departamento
            var data =
                HttpContext.User.FindFirst
                (x => x.Type == "IDDEPARTAMENTO").Value;
            int idDepartamento = int.Parse(data);
            Empleado empleado = await
                    this.service.FindCompisAsync(idDepartamento);
            return View(empleado);
        }
    }
}
