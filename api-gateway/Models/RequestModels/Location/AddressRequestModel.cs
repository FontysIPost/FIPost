using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.RequestModels.Location
{
    public class AddressRequestModel
    {
        public Guid CityId { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Addition { get; set; }
    }
}
