using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedis.Models;
using MvcCoreCacheRedis.Repositories;
using MvcCoreCacheRedis.Services;

namespace MvcCoreCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;

        public ProductosController(RepositoryProductos repo, ServiceCacheRedis service)
        {
            this.repo = repo;
            this.service = service;
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {

            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }

        public async Task<IActionResult> Favoritos()
        {
            List<Producto> productos = await this.service.GetProductosFavoritosAsync();
            return View(productos);
        }

        public async Task<IActionResult> SeleccionarFavorito(int idproducto)
        {
            // RECUPERAMOS EL PRODUCTO DEL REPOSITORY
            Producto producto = this.repo.FindProducto(idproducto);
            await this.service.AddProductoFavoritoAsync(producto);
            ViewData["MENSAJE"] = "Producto favorito almacenado";
            return RedirectToAction("Details", new { id = idproducto });
        }

        public async Task<IActionResult> DeleteFavorito(int idproducto)
        {
            await this.service.DeleteProductoFavoritoAsync(idproducto);
            return RedirectToAction("Favoritos");
        }
    }
}
