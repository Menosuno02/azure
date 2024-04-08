using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Models
{
    public class City
    {
        [JsonProperty("CountryRegion")]
        public string CountryRegion { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Region")]
        public string Region { get; set; }
    }
}
