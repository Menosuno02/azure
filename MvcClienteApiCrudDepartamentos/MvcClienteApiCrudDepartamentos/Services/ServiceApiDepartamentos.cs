using MvcClienteApiCrudDepartamentos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcClienteApiCrudDepartamentos.Services
{
    public class ServiceApiDepartamentos
    {
        private string urlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiDepartamentos(IConfiguration configuration)
        {
            this.urlApi = configuration.GetValue<string>
                ("ApiUrls:ApiDepartamentos");
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>>
            GetDepartamentosAsync()
        {
            string request = "api/departamentos";
            List<Departamento> data =
                await this.CallApiAsync<List<Departamento>>(request);
            return data;
        }

        public async Task<Departamento>
            FindDepartamentoAsync(int deptno)
        {
            string request = "api/departamentos/" + deptno;
            Departamento data =
                await this.CallApiAsync<Departamento>(request);
            return data;
        }

        // Los métodos de acción se pueden hacer genéricos
        // cuando recibimos objetos
        // Pero si los parámetros van por URL, no
        // suelen ser genéricos
        public async Task DeleteDepartamentoAsync(int deptno)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos/" + deptno;
                client.BaseAddress = new Uri(this.urlApi);
                client.DefaultRequestHeaders.Clear();
                // No enviamos headers porque no recibe ni
                // devuelve nada
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
                // Podríamos devovler que es lo que ha sucedido
                // return response.StatusCode;
            }
        }

        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.urlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                // Instanciamos model
                Departamento dept = new Departamento
                {
                    DeptNo = id,
                    Nombre = nombre,
                    Localidad = localidad
                };
                // Convertimos nuestro objeto a JSON
                string json = JsonConvert.SerializeObject(dept);
                // Para enviar datos (Data) al servicio debemos
                // utilizar la clase StringContent que nos pedirá
                // los datos, us encoding y el tipo de formato
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.urlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Departamento dept = new Departamento
                {
                    DeptNo = id,
                    Nombre = nombre,
                    Localidad = localidad
                };
                string json = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
    }
}
