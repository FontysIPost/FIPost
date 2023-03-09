using api_gateway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.ServiceModels
{
    public class PackageServiceModel
    {
        public Guid Id { get; set; }
        public string ReceiverId { get; set; }
        public Guid CollectionPointId { get; set; }
        public string Sender { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public bool RouteFinished { get; set; }
        public virtual ICollection<TicketServiceModel> Tickets { get; set; }

    }
}
