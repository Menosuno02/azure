using Newtonsoft.Json;

namespace AirportsNugetALL.Models
{
    public class Airport
    {
        [JsonProperty("@odata.id")]
        public string ODataId { get; set; }
        [JsonProperty("@odata.editLink")]
        public string ODataEditLink { get; set; }
        [JsonProperty("IcaoCode")]
        public string Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IataCode")]
        public string IataCode { get; set; }
        [JsonProperty("Location")]
        public Location Location { get; set; }
    }
}
