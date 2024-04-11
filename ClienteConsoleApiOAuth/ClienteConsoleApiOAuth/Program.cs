using ClienteConsoleApiOAuth.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

Console.WriteLine("API Client OAuth");
Console.WriteLine("Introduzca Apellido");
string apellido = Console.ReadLine();
Console.WriteLine("Introduzca Password");
string password = Console.ReadLine();
string respuesta = await GetTokenAsync(apellido, password);
Console.WriteLine(respuesta);
Console.WriteLine("Petición a Empleado");
string data = await FindEmpleadoAsync(respuesta);
Console.WriteLine(data);
Console.WriteLine("-------------------------");
Console.WriteLine("Fin de programa");

static async Task<string> GetTokenAsync(string user, string pass)
{
    string urlApi = "https://localhost:7213/";
    LoginModel model = new LoginModel
    {
        UserName = user,
        Password = pass
    };
    using (HttpClient client = new HttpClient())
    {
        client.BaseAddress = new Uri(urlApi);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // Convertimos nuestro model a JSON
        string jsonModel = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent
            (jsonModel, Encoding.UTF8, "application/json");
        string request = "api/Auth/Login";
        HttpResponseMessage response = await
            client.PostAsync(request, content);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            JObject keys = JObject.Parse(data);
            string token = keys.GetValue("response").ToString();
            return token;
        }
        else
        {
            return "Petición incorrecta: " + response.StatusCode;
        }
    }
}

// Creamos un método para realizar una petición con el token
// en el header mediante Authorization
static async Task<string> FindEmpleadoAsync(string token)
{
    string urlApi = "https://localhost:7213/";
    using (HttpClient client = new HttpClient())
    {
        string request = "api/Empleados/7839";
        client.BaseAddress = new Uri(urlApi);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // Debemos añadir a nuestro header Authorization
        client.DefaultRequestHeaders.Add
            ("Authorization", "bearer " + token);
        HttpResponseMessage response = await client.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }
        else
        {
            return "Error: " + response.StatusCode;
        }
    }
}
