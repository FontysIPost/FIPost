using LocatieService.Database.Converters;
using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Database.Datamodels.Dtos.Responses;
using System.Collections.Generic;

namespace LocatieService.Database.Converters
{
    public class BuildingDtoConverter : IDtoConverter<Building, BuildingRequest, BuildingResponse>
    {
        public Building DtoToModel(BuildingRequest request)
        {
            return new Building
            {
                Name = request.Name,
                Address = request.Address
            };
        }

        public BuildingResponse ModelToDto(Building model)
        {
            return new BuildingResponse
            {
                Id = model.Id,
                Name = model.Name,
                Address = new AddressResponse
                {
                    // City will be added in controller.
                    PostalCode = model.Address.PostalCode,
                    Street = model.Address.Street,
                    Number = model.Address.Number,
                    Addition = model.Address.Addition
                }
            };
        }

        public List<BuildingResponse> ModelToDto(List<Building> models)
        {
            List<BuildingResponse> responseDtos = new List<BuildingResponse>();

            foreach (Building building in models)
            {
                responseDtos.Add(ModelToDto(building));
            }

            return responseDtos;
        }
    }
}
