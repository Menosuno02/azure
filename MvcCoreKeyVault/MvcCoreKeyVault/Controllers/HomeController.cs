using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using MvcCoreKeyVault.Models;
using System.Diagnostics;

namespace MvcCoreKeyVault.Controllers
{
    public class HomeController : Controller
    {
        // NECESITAMOS INYECTAR SECRETCLIENT
        private SecretClient secretClient;

        public HomeController(SecretClient secretClient)
        {
            this.secretClient = secretClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string secretkey)
        {
            KeyVaultSecret secret = await this.secretClient.GetSecretAsync(secretkey);
            ViewData["SECRETO"] = secret.Value;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
