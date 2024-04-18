using Azure.Data.Tables;
using MvcCoreSasStorage.Models;
using Newtonsoft.Json.Linq;
using NuGet.ProjectModel;
using System.Net;

namespace MvcCoreSasStorage.Services
{
    public class ServiceAzureAlumnos
    {
        // NECESITAMOS LA TABLA ALUMNOS
        private TableClient tablaAlumnos;

        // NECESITAMOS LA URI DE ACCESO AL TOKEN
        // QUE ESTARÁ EN CONFIGURATION
        private string UrlApiToken;

        public ServiceAzureAlumnos(IConfiguration configuration)
        {
            this.UrlApiToken = configuration.GetValue<string>
                ("ApiUrls:ApiTokenSaS");
        }

        // NECESITAMOS UN MÉTODO PARA LEER EL TOKEN
        public async Task<string> GetTokenAsync(string curso)
        {
            using (WebClient client = new WebClient())
            {
                string request = "token/" + curso;
                client.Headers["content-type"] = "application/json";
                Uri uri = new Uri(this.UrlApiToken + request);
                string data = await client.DownloadStringTaskAsync(uri);
                JObject objetoJson = JObject.Parse(data);
                string token = objetoJson.GetValue("token").ToString();
                return token;
            }
        }

        // MÉTODO PARA RECUPERAR LOS ALUMNOS A PARTIR DEL TOKEN
        public async Task<List<Alumno>> GetAlumnosAsync(string curso)
        {
            string token = await this.GetTokenAsync(curso);
            // CREAMOS UN URI CON EL TOKEN
            Uri uriToken = new Uri(token);
            // PARA ACCEDER AL RECURSO, SIMPLEMENTE CREAMOS
            // EL RECURSO CON SU URI
            this.tablaAlumnos = new TableClient(uriToken);
            List<Alumno> alumnos = new List<Alumno>();
            // REALIZAMOS UNA CONSULTA CON FILTER PARA RECUPERAR
            // TODOS LOS ALUMNOS
            var query = this.tablaAlumnos.QueryAsync<Alumno>
                (filter: "");
            await foreach (var item in query)
            {
                alumnos.Add(item);
            }
            return alumnos;
        }
    }
}
