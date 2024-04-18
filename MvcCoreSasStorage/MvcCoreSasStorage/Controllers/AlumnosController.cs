using Microsoft.AspNetCore.Mvc;
using MvcCoreSasStorage.Models;
using MvcCoreSasStorage.Services;

namespace MvcCoreSasStorage.Controllers
{
    public class AlumnosController : Controller
    {
        private ServiceAzureAlumnos service;

        public AlumnosController(ServiceAzureAlumnos service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string curso)
        {
            List<Alumno> alumnos = await this.service.GetAlumnosAsync(curso);
            return View(alumnos);
        }
    }
}
