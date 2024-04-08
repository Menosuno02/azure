using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Models
{
    public class Location
    {
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("City")]
        public City City { get; set; }
        [JsonProperty("Loc")]
        public Coordinates Coordinates { get; set; }
    }
}
