using AirportsNugetALL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirportsNugetALL.Services
{
    public class ServiceAirports
    {
        public async Task<AirportList> GetAirportListAsync()
        {
            WebClient client = new WebClient();
            client.Headers["content-type"] = "application/json";
            string url = "https://services.odata.org/V4/(S(2esholowikwyef30ogqjzbvf))/TripPinServiceRW/Airports";
            string dataJson = await client.DownloadStringTaskAsync(url);
            AirportList airports = JsonConvert.DeserializeObject<AirportList>(dataJson);
            return airports;
        }
    }
}
