using Microsoft.AspNetCore.Mvc;
using MvcDockersComics.Models;
using MvcDockersComics.Repositories;

namespace MvcDockersComics.Controllers
{
    public class ComicsController : Controller
    {
        private RepositoryComics repo;

        public ComicsController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await this.repo.GetComicsAsync();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comic comic)
        {
            await this.repo.InsertComic(comic.Nombre, comic.Imagen);
            return RedirectToAction("Index");
        }
    }
}
