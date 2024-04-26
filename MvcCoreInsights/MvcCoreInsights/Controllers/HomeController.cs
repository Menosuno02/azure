using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using MvcCoreInsights.Models;
using System.Diagnostics;

namespace MvcCoreInsights.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TelemetryClient telemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            this.telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, int cantidad)
        {
            ViewData["MENSAJE"] = "Su donativo de " + cantidad + "€ ha sido aceptado. Muchas gracias Sr/Sra " + usuario;
            // CREAMOS UN EVENTO PARA EMPEZAR A 
            // GUARDAR DATOS
            this.telemetryClient.TrackEvent("DonativosRequest");
            // PODEMOS CREAR RESUMEN DE LAS MÉTRICAS
            MetricTelemetry metric = new MetricTelemetry();
            metric.Name = "Donativos";
            metric.Sum = cantidad;
            this.telemetryClient.TrackMetric(metric);
            // TAMBIÉN PODEMOS CREAR TRAZAS, QUE NO DEJAN
            // DE SER MENSAJES DE SEGUIMIENTO Y QUE
            // CONTIENEN UN NIVEL
            string mensaje = "";
            SeverityLevel level;
            if (cantidad == 0)
                level = SeverityLevel.Error;
            else if (cantidad < 5)
                level = SeverityLevel.Critical;
            else if (cantidad <= 20)
                level = SeverityLevel.Warning;
            else
                level = SeverityLevel.Information;
            mensaje = usuario + " " + cantidad + "€";
            TraceTelemetry traza = new TraceTelemetry(mensaje, level);
            this.telemetryClient.TrackTrace(traza);
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
