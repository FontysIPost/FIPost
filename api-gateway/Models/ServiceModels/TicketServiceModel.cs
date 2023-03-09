using api_gateway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.ServiceModels
{
    public class TicketServiceModel
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public double FinishedAt { get; set; }
        public string CompletedByPersonId { get; set; }
        public string ReceivedByPersonId { get; set; }

    }
}
