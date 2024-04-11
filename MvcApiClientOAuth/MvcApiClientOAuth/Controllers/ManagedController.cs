using Microsoft.AspNetCore.Mvc;
using MvcApiClientOAuth.Models;
using MvcApiClientOAuth.Services;

namespace MvcApiClientOAuth.Controllers
{
    public class ManagedController : Controller
    {
        private ServiceApiEmpleados service;

        public ManagedController(ServiceApiEmpleados service)
        {
            this.service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            string token = await this.service
                .GetTokenAsync(model.UserName, model.Password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            else
            {
                ViewData["MENSAJE"] = "Ya tienes tu token";
                HttpContext.Session.SetString("TOKEN", token);
            }
            return View();
        }
    }
}
