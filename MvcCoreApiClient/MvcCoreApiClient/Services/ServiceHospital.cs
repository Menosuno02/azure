using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospital
    {
        // Clase para indicar el formato que estamos consumiendo
        private MediaTypeWithQualityHeaderValue header;

        // Nuestra URL del servicio
        private string ApiUrl;

        public ServiceHospital(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>
                ("ApiUrls:ApiHospitales");
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<Hospital> FindHospitalAsync
            (int idhospital)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales/" + idhospital;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Si las propiedades del JSON y del model
                    // se llaman igual, utilizamos directamente
                    // la serialización del response
                    Hospital data =
                        await response.Content.ReadAsAsync<Hospital>();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        // Creamos un método asíncrono para leer los hospitales
        public async Task<List<Hospital>>
            GetHospitalesAsync()
        {
            // Utilizamos la clase HttpClient para las peticiones
            using (HttpClient client = new HttpClient())
            {
                // Necesitamos la petición
                string request = "api/hospitales";
                // Indicamos la url base de nuestro servicio
                client.BaseAddress = new Uri(this.ApiUrl);
                // Debemos limpiar las cabeceras en cada petición, por si
                // en algún momento las mezclamos y nos da error
                client.DefaultRequestHeaders.Clear();
                // Indicamos el tipo de consutla que vamos a consumir
                client.DefaultRequestHeaders.Accept.Add(this.header);
                // Realizamos la petición y almacenamos los resultados
                // en una respuesta
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // Primero vamos a realizar la petición usando
                    // NewtonSoft, para que tengamos un ejemplo si las
                    // propiedades del JSON y el model fuesen distintas
                    string json =
                        await response.Content.ReadAsStringAsync();
                    List<Hospital> data =
                        JsonConvert.DeserializeObject<List<Hospital>>
                        (json);
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
