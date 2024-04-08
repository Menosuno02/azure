using NugetApiModels.Models;
using System.Net.Http.Headers;

namespace MvcApliClientMultiplesRoutes.Services
{
    public class ServiceEmpleados
    {
        private MediaTypeWithQualityHeaderValue header;
        private string ApiUrlEmpleados;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrlEmpleados =
                configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/empleados";
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "api/empleados/GetOficios";
            List<string> oficios =
                await this.CallApiAsync<List<string>>(request);
            return oficios;
        }

        public async Task<List<Empleado>> GetEmpleadosOficio
            (string oficio)
        {
            string request = "api/empleados/empleadospficio/" + oficio;
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            string request = "api/empleados/" + idEmpleado;
            Empleado empleado =
                await this.CallApiAsync<Empleado>(request);
            return empleado;
        }

        // Método genérico para la petición a todos los GET de la API
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrlEmpleados);
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
    }
}
