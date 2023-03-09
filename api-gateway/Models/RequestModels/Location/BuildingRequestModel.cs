using api_gateway.Models.ServiceModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.RequestModels.Location
{
    public class BuildingRequestModel
    {
        public string Name { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
