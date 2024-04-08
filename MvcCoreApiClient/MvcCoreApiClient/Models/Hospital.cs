using Newtonsoft.Json;

namespace MvcCoreApiClient.Models
{
    public class Hospital
    {
        // Esto solamente lo incluiremos si las
        // propiedades se llaman diferente al json
        [JsonProperty("idHospital")]
        public int IdHospital { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("direccion")]
        public string Direccion { get; set; }
        [JsonProperty("telefono")]
        public string Telefono { get; set; }
        [JsonProperty("camas")]
        public int Camas { get; set; }
    }
}
