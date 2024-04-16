using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSasStorage.Helpers;
using MvcCoreSasStorage.Models;

namespace MvcCoreSasStorage.Controllers
{
    public class MigracionController : Controller
    {
        private HelperXml helperXml;

        public MigracionController(HelperXml helperXml)
        {
            this.helperXml = helperXml;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string algo)
        {
            string azureKeys = "UseDevelopmentStorage=true";
            TableServiceClient serviceClient = new TableServiceClient(azureKeys);
            TableClient tableClient = serviceClient.GetTableClient("alumnos");
            await tableClient.CreateIfNotExistsAsync();

            List<Alumno> alumnos = helperXml.GetAlumnosXml();
            foreach (Alumno alumno in alumnos)
            {
                await tableClient.AddEntityAsync<Alumno>(alumno);
            }
            ViewData["MENSAJE"] = "Migración completa en Azure";
            return View();
        }
    }
}
