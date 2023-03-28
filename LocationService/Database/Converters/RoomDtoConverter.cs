using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;

namespace LocatieService.Database.Converters
{
    public class RoomDtoConverter : IDtoConverter<Room, RoomRequest, RoomResponse>
    {
        public Room DtoToModel(RoomRequest request)
        {
            return new Room
            {
                Name = request.Name,
                BuildingId = request.BuildingId
            };
        }

        public RoomResponse ModelToDto(Room model)
        {
            return new RoomResponse
            {
                Id = model.Id,
                Name = model.Name,
                // Building wordt in controller opgehaald.
            };
        }

        public List<RoomResponse> ModelToDto(List<Room> models)
        {
            List<RoomResponse> responseDtos = new List<RoomResponse>();

            foreach (Room room in models)
            {
                responseDtos.Add(ModelToDto(room));
            }

            return responseDtos;
        }
    }
}
