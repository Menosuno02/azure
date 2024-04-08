using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Models
{
    public class Crs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("properties")]
        public CrsProperties Properties { get; set; }
    }
}
