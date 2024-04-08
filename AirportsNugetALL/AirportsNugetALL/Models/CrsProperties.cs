using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Models
{
    public class CrsProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
