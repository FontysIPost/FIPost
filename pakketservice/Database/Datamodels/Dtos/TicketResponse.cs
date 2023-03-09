using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Database.Datamodels.Dtos
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public double FinishedAt { get; set; }
        public string CompletedByPersonId { get; set; }
        public string ReceivedByPersonId { get; set; }
    }
}
