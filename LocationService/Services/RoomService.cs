using LocatieService.Database.Contexts;
using LocatieService.Database.Converters;
using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocatieService.Services
{
    public class RoomService : IRoomService
    {
        private readonly LocatieContext _context;
        private readonly IDtoConverter<Room, RoomRequest, RoomResponse> _converter;
        private readonly IDtoConverter<Building, BuildingRequest, BuildingResponse> _buildingConverter;

        public RoomService(LocatieContext context,
            IDtoConverter<Room, RoomRequest, RoomResponse> converter,
            IDtoConverter<Building, BuildingRequest, BuildingResponse> buildingConverter)
        {
            _context = context;
            _converter = converter;
            _buildingConverter = buildingConverter;
        }

        public async Task<RoomResponse> AddAsync(RoomRequest request)
        {
            Room room = _converter.DtoToModel(request);

            if (await IsDuplicateAsync(room))
            {
                throw new DuplicateException("This room already exists.");
            }

            await _context.AddAsync(room);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(room);
        }

        public async Task<List<RoomResponse>> GetAllAsync()
        {
            List<Room> rooms = await _context.Rooms.Where(e => e.IsActive).ToListAsync();
            List<RoomResponse> responses = new List<RoomResponse>();

            // Add buildings:
            foreach (Room room in rooms)
            {
                responses.Add(await CreateResponseAsync(room));
            }

            return responses;
        }

        public async Task<RoomResponse> GetByIdAsync(Guid id)
        {
            Room room = await _context.Rooms.FirstOrDefaultAsync(e => e.Id == id);

            if (room == null)
            {
                throw new NotFoundException($"Room with id {id} not found.");
            }

            return await CreateResponseAsync(room);
        }

        public async Task<RoomResponse> GetByNameAsync(string name)
        {
            Room room = await _context.Rooms.FirstOrDefaultAsync(e => e.Name == name);

            if (room == null)
            {
                throw new NotFoundException($"Room with name {name} not found.");
            }

            return await CreateResponseAsync(room);
        }

        public async Task<RoomResponse> UpdateAsync(Guid id, RoomRequest request)
        {
            Room room = _converter.DtoToModel(request);
            room.Id = id;

            if (!await _context.Rooms.AnyAsync(r => r.Id == id))
            {
                throw new DuplicateException($"Room with id {id} not found.");
            }

            if (await IsDuplicateAsync(room, id))
            {
                throw new DuplicateException("This room already exists.");
            }

            _context.Update(room);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(room);
        }

        public async Task<RoomResponse> DeleteRoomAsync(Guid id)
        {
            Room room = await _context.Rooms.FirstOrDefaultAsync(e => e.Id == id);

            if (room == null)
            {
                throw new NotFoundException($"Room with id {id} not found.");
            }

            // Set inactive:
            room.IsActive = false;

            // Update record:
            _context.Update(room);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(room);
        }

        private async Task<bool> IsDuplicateAsync(Room room, Guid? id = null)
        {
            if (id == null)
            {
                return await _context.Rooms.AnyAsync(e => e.BuildingId == room.BuildingId
                    && e.Name == room.Name
                    && e.IsActive);
            }
            else
            {
                return await _context.Rooms.AnyAsync(e => e.BuildingId == room.BuildingId
                    && e.Name == room.Name
                    && e.IsActive && e.Id != id);
            }
        }

        private async Task<RoomResponse> CreateResponseAsync(Room room)
        {
            RoomResponse response = _converter.ModelToDto(room);
            Building building = await _context.Buildings.FirstOrDefaultAsync(e => e.Id == room.BuildingId); // Get building model.
            BuildingResponse buildingResponse = _buildingConverter.ModelToDto(building); // Insert building to model.
            buildingResponse.Address.City = await _context.Cities.FirstOrDefaultAsync(e => e.Id == building.Address.CityId); // Insert city into address.
            response.Building = buildingResponse;

            return response;
        }
    }
}
