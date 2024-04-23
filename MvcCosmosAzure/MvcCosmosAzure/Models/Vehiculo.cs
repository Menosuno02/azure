using Newtonsoft.Json;

namespace MvcCosmosAzure.Models
{
    public class Vehiculo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        // Nuestra clase Motor que será la clase dinámica
        public Motor Motor { get; set; }
    }
}
