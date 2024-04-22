using MvcApiManagement.Models;
using System.Net.Http.Headers;
using System.Web;

namespace MvcApiManagement.Services
{
    public class ServiceApiManagement
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiEmpleados;
        private string UrlApiDepartamentos;

        public ServiceApiManagement(IConfiguration configuration)
        {
            this.Header = new MediaTypeWithQualityHeaderValue
                ("application/json");
            this.UrlApiEmpleados = configuration.GetValue<string>
                ("ApiUrls:ApiEmpleados");
            this.UrlApiDepartamentos =
                configuration.GetValue<string>
                ("ApiUrls:ApiDepartamentos");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //DEBEMOS ENVIAR UNA CADENA VACIA AL FINAL DEL 
                //REQUEST
                var queryString =
                    HttpUtility.ParseQueryString(string.Empty);
                string request = "data?" + queryString;
                //NO SE UTILIZA BASEADDRESS
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //LA PETICION SE REALIZA EN CONJUNTO, ES DECIR, 
                //URL + REQUEST
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApiEmpleados + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> data =
                        await response.Content.ReadAsAsync
                        <List<Empleado>>();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        //METODO CON SUSCRIPCION
        public async Task<List<Departamento>> GetDepartamentosAsync(string suscripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                //DEBEMOS ENVIAR UNA CADENA VACIA AL FINAL DEL 
                //REQUEST
                var queryString =
                    HttpUtility.ParseQueryString(string.Empty);
                string request = "api/departamentos?" + queryString;
                //NO SE UTILIZA BASEADDRESS
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //DEBEMOS AÑADIR LA SUSCRIPCION MEDIANTE UNA KEY 
                client.DefaultRequestHeaders.Add
                    ("Ocp-Apim-Subscription-Key", suscripcion);
                //LA PETICION SE REALIZA EN CONJUNTO, ES DECIR, 
                //URL + REQUEST
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApiDepartamentos
                    + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Departamento> data =
                        await response.Content.ReadAsAsync
                        <List<Departamento>>();
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
