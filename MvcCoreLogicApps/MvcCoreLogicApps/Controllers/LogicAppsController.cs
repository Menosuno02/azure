using Microsoft.AspNetCore.Mvc;
using MvcCoreLogicApps.Services;

namespace MvcCoreLogicApps.Controllers
{
    public class LogicAppsController : Controller
    {
        private ServiceLogicApps service;

        public LogicAppsController(ServiceLogicApps service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string asunto, string mensaje)
        {
            await this.service.SendEmailAsync(email, asunto, mensaje);
            ViewData["MENSAJE"] = "Procesando Email Logic Apps";
            return View();
        }
    }
}
