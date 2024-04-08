using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCustomersALL.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public string IdCustomer { get; set; }
        [JsonProperty("contactName")]
        public string Name { get; set; }
        [JsonProperty("companyName")]
        public string Company { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
