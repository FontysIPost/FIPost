using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Database.Datamodels
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public double FinishedAt { get; set; }
        public string CompletedByPersonId { get; set; }
        public string ReceivedByPersonId { get; set; }

        public Guid PackageId { get; set; }
    }
}
