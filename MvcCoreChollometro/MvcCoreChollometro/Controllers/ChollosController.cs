using Microsoft.AspNetCore.Mvc;
using MvcCoreChollometro.Models;
using MvcCoreChollometro.Repositories;

namespace MvcCoreChollometro.Controllers
{
    public class ChollosController : Controller
    {
        private RepositoryChollos repo;

        public ChollosController(RepositoryChollos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Chollo> chollos =
                await this.repo.GetChollosAsync();
            ViewData["ELEMENTOS"] = chollos.Count();
            return View(chollos);
        }
    }
}
