using MvcApiClientOAuth.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiClientOAuth.Services
{
    public class ServiceApiEmpleados
    {
        private string UrlApiEmpleados;
        private MediaTypeWithQualityHeaderValue Header;
        private IHttpContextAccessor HttpContextAccessor;

        public ServiceApiEmpleados
            (IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            this.UrlApiEmpleados = configuration.GetValue<string>
                ("ApiUrls:ApiEmpleados");
            this.Header = new MediaTypeWithQualityHeaderValue
                ("application/json");
            this.HttpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTokenAsync
            (string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Auth/Login";
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };
                string jsonData = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent
                    (jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);
                    string token = keys.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
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

        // Tendremos un método genérico que recibirá el request
        // y el token
        private async Task<T> CallApiAsync<T>
            (string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiEmpleados);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
                    ("Authorization", "bearer " + token);
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

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/Empleados";
            List<Empleado> empleados = await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync
            (int idEmpleado)
        {
            string request = "api/Empleados/" + idEmpleado;
            Empleado empleado = await
                this.CallApiAsync<Empleado>(request);
            return empleado;
        }

        public async Task<Empleado> GetPerfilEmpleadoAsync()
        {
            string token =
                this.HttpContextAccessor.HttpContext.User
                .FindFirst(x => x.Type == "TOKEN").Value;
            string request = "api/empleados/perfilempleado";
            Empleado empleado =
                await this.CallApiAsync<Empleado>(request, token);
            return empleado;
        }

        public async Task<List<Empleado>>
            GetCompisTrabajoAsync()
        {
            string token = this.HttpContextAccessor.HttpContext.User
                .FindFirst(x => x.Type == "TOKEN").Value;
            string request = "api/empleados/compiscurro";
            List<Empleado> compis = await this.CallApiAsync<List<Empleado>>(request, token);
            return compis;
        }

        
    }
}
