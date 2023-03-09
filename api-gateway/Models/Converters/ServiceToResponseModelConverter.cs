using api_gateway.Models.ResponseModels;
using api_gateway.Models.ServiceModels;
using api_gateway.Models.ServiceModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_gateway.Models.Converters
{
    /// <summary>
    /// Transfers incoming objects from microservices into response objects for API endpoints
    /// </summary>
    public static class ServiceToResponseModelConverter
    {
        public static PackageResponseModel ConvertPackage(PackageServiceModel serviceModel)
        {
            return new PackageResponseModel(
                serviceModel.Id,
                serviceModel.Sender,
                null,
                null,
                serviceModel.Name,
                serviceModel.Status,
                serviceModel.RouteFinished,
                null
                );
        }

        public static PackageResponseModel ConvertPackage(PackageServiceModel serviceModel, ICollection<PersonServiceModel> personServiceModels, ICollection<Room> rooms)
        {
            PersonServiceModel receiver = null;
            Room collectionPoint = null;
            if (personServiceModels != null)
            {
                receiver = personServiceModels.FirstOrDefault(p => p.Id == serviceModel.ReceiverId);
            }
            if (rooms != null)
            {
                collectionPoint = rooms.FirstOrDefault(r => r.Id == serviceModel.CollectionPointId);
            }

            return new PackageResponseModel(
                serviceModel.Id,
                serviceModel.Sender,
                collectionPoint,
                receiver,
                serviceModel.Name,
                serviceModel.Status,
                serviceModel.RouteFinished,
                ConvertTickets(serviceModel.Tickets, personServiceModels, rooms)
                );
        }

        public static TicketResponseModel ConvertTicket(TicketServiceModel serviceModel, PersonServiceModel completedBy,
            Room location, PersonServiceModel receivedBy = null)
        {
            return new TicketResponseModel(
                serviceModel.Id,
                location,
                serviceModel.FinishedAt,
                completedBy != null ? completedBy.Name : "",
                receivedBy != null ? receivedBy.Name : ""
                );
        }

        public static TicketResponseModel ConvertTicket(TicketServiceModel serviceModel)
        {
            return new TicketResponseModel(
                serviceModel.Id,
                null,
                serviceModel.FinishedAt,
                null,
                null
                );
        }

        public static ICollection<TicketResponseModel> ConvertTickets(ICollection<TicketServiceModel> serviceModels, ICollection<PersonServiceModel> personServiceModels,
            ICollection<Room> rooms)
        {
            List<TicketResponseModel> responseModels = new List<TicketResponseModel>();

            // if the list is null assume empty and return an empty response model list
            if (serviceModels == null)
            {
                return responseModels;
            }

            PersonServiceModel completedBy = null;
            PersonServiceModel receivedBy = null;
            Room location = null;
            foreach (var serviceModel in serviceModels)
            {
                if (personServiceModels != null)
                {
                    completedBy = personServiceModels.FirstOrDefault(p => p.Id == serviceModel.CompletedByPersonId);
                    receivedBy = personServiceModels.FirstOrDefault(p => p.Id == serviceModel.ReceivedByPersonId);
                }
                if (rooms != null)
                {
                    location = rooms.FirstOrDefault(r => r.Id == serviceModel.LocationId);
                }

                TicketResponseModel responseModel = ConvertTicket(serviceModel, completedBy, location, receivedBy);
                responseModels.Add(responseModel);
            }

            return responseModels;
        }

        public static ICollection<PackageResponseModel> ConvertPackages(ICollection<PackageServiceModel> packageServiceModels,
            ICollection<PersonServiceModel> personServiceModels, ICollection<Room> rooms)
        {
            List<PackageResponseModel> responseModels = new List<PackageResponseModel>();

            foreach (var serviceModel in packageServiceModels)
            {
                PackageResponseModel responseModel = ConvertPackage(serviceModel, personServiceModels, rooms);
                responseModels.Add(responseModel);
            }
            return responseModels;
        }
    }
}
