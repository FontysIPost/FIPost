using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.RequestModels
{
    public class TicketRequestModel
    {
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string CompletedByPersonId { get; set; }
        [Required]
        public Guid PackageId { get; set; }

        public string ReceivedByPersonId { get; set; }
    }
}
