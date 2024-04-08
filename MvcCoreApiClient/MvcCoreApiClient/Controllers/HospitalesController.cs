using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClient.Models;
using MvcCoreApiClient.Services;

namespace MvcCoreApiClient.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceHospital service;

        public HospitalesController(ServiceHospital service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Servidor()
        {
            List<Hospital> hospitales = await this.service.GetHospitalesAsync();
            return View(hospitales);
        }

        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital = await
                this.service.FindHospitalAsync(id);
            return View(hospital);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cliente()
        {
            return View();
        }
    }
}
