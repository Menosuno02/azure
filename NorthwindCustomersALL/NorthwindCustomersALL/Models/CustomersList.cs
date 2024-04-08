using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCustomersALL.Models
{
    public class CustomersList
    {
        [JsonProperty("results")]
        public List<Customer> Customers { get; set; }
    }
}
