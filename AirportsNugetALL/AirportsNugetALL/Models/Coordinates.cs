using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Models
{
    public class Coordinates
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("coordinates")]
        public double[] Coordinate { get; set; }
        [JsonProperty("crs")]
        public Crs Crs { get; set; }
    }
}
