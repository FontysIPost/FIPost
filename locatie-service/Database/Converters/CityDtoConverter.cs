using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using System.Collections.Generic;

namespace LocatieService.Database.Converters
{
    public class CityDtoConverter : IDtoConverter<City, CityRequest, CityResponse>
    {
        public City DtoToModel(CityRequest request)
        {
            return new City
            {
                Name = request.Name
            };
        }

        public CityResponse ModelToDto(City model)
        {
            return new CityResponse
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public List<CityResponse> ModelToDto(List<City> models)
        {
            List<CityResponse> responseDtos = new List<CityResponse>();

            foreach (City city in models)
            {
                responseDtos.Add(ModelToDto(city));
            }

            return responseDtos;
        }
    }
}
