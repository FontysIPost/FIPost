using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.ServiceModels.Location
{
    public class Address
    {
        public City City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Addition { get; set; }
    }
}
