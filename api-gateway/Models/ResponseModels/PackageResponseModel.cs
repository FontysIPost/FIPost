using api_gateway.Models.ServiceModels;
using api_gateway.Models.ServiceModels.Location;
using api_gateway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.ResponseModels
{
    public class PackageResponseModel
    {
        public Guid Id { get; set; }
        public PersonServiceModel Receiver { get; set; }
        public Room CollectionPoint { get; set; }
        public string Sender { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public bool RouteFinished { get; set; }
        public virtual ICollection<TicketResponseModel> Tickets { get; set; }

        public PackageResponseModel(Guid id, string sender, Room collectionPoint, PersonServiceModel receiver, string name,
            Status status, bool routeFinished, ICollection<TicketResponseModel> tickets)
        {
            Id = id;
            Receiver = receiver;
            CollectionPoint = collectionPoint;
            Sender = sender;
            Name = name;
            Status = status;
            RouteFinished = routeFinished;
            Tickets = tickets;
        }

        public PackageResponseModel()
        {

        }
    }
}
