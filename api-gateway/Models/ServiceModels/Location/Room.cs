using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.ServiceModels.Location
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Building Building { get; set; }
    }
}
