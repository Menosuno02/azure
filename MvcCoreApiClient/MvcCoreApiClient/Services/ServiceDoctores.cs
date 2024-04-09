using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiClient.Services
{
    public class ServiceDoctores
    {
        private string urlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDoctores(IConfiguration configuration)
        {
            this.urlApi = configuration.GetValue<string>("ApiUrls:ApiDoctores");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> CallApiAsync<T>(string request)
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

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "api/doctores";
            List<Doctor> data =
                await this.CallApiAsync<List<Doctor>>(request);
            return data;
        }

        public async Task<Doctor> FindDoctorAsync(int doctorno)
        {
            string request = "api/doctores/" + doctorno;
            Doctor data =
                await this.CallApiAsync<Doctor>(request);
            return data;
        }

        public async Task DeleteDoctorAsync(int doctorno)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores/" + doctorno;
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task InsertDoctorAsync
            (int hospitalCod, int doctorNo, string apellido,
            string especialidad, int salario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Doctor doctor = new Doctor
                {
                    HospitalCod = hospitalCod,
                    DoctorNo = doctorNo,
                    Apellido = apellido,
                    Especialidad = especialidad,
                    Salario = salario
                };
                string json = JsonConvert.SerializeObject(doctor);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDoctorAsync
            (int hospitalCod, int doctorNo, string apellido,
            string especialidad, int salario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/doctores";
                client.BaseAddress = new Uri(urlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Doctor doctor = new Doctor
                {
                    HospitalCod = hospitalCod,
                    DoctorNo = doctorNo,
                    Apellido = apellido,
                    Especialidad = especialidad,
                    Salario = salario
                };
                string json = JsonConvert.SerializeObject(doctor);
                StringContent context = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, context);
            }
        }
    }
}
