using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PakketService.Database.Converters
{
    public class TicketDtoConverter : IDtoConverter<Ticket, TicketRequest, TicketResponse>
    {
        public Ticket DtoToModel(TicketRequest request)
        {
            return new Ticket
            {
                LocationId = request.LocationId,
                CompletedByPersonId = request.CompletedByPersonId,
                ReceivedByPersonId = request.ReceivedByPersonId,
                PackageId = request.PackageId
            };
        }

        public TicketResponse ModelToDto(Ticket model)
        {
            return new TicketResponse
            {
                LocationId = model.LocationId,
                CompletedByPersonId = model.CompletedByPersonId,
                FinishedAt = model.FinishedAt,
                Id = model.Id,
                ReceivedByPersonId = model.ReceivedByPersonId
            };
        }

        public List<TicketResponse> ModelToDto(List<Ticket> models)
        {
            List<TicketResponse> responseDtos = new();

            foreach (Ticket ticket in models)
            {
                responseDtos.Add(ModelToDto(ticket));
            }

            responseDtos.Sort((x, y) => y.FinishedAt.CompareTo(x.FinishedAt));
            return responseDtos;
        }
    }
}
