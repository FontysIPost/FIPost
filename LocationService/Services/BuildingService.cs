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
    public class BuildingService : IBuildingService
    {
        private readonly LocatieContext _context;
        private readonly IDtoConverter<Building, BuildingRequest, BuildingResponse> _converter;

        public BuildingService(LocatieContext context, IDtoConverter<Building, BuildingRequest, BuildingResponse> converter)
        {
            _context = context;
            _converter = converter;
        }


        public async Task<BuildingResponse> AddAsync(BuildingRequest request)
        {
            Building building = _converter.DtoToModel(request);

            if (await IsDuplicateAsync(building))
            {
                throw new DuplicateException("This building already exists.");
            }

            await _context.AddAsync(building);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(building);
        }

        public async Task<List<BuildingResponse>> GetAllAsync()
        {
            List<Building> buildings = await _context.Buildings.Where(e => e.IsActive).ToListAsync();
            List<BuildingResponse> responses = new List<BuildingResponse>();

            // Add cities:
            foreach (Building building in buildings)
            {
                BuildingResponse response = _converter.ModelToDto(building);
                response.Address.City = await _context.Cities.FirstOrDefaultAsync(e => e.Id == building.Address.CityId);

                responses.Add(response);
            }

            return responses;
        }

        public async Task<BuildingResponse> GetByIdAsync(Guid id)
        {
            Building building = await _context.Buildings.FirstOrDefaultAsync(e => e.Id == id);

            if (building == null)
            {
                throw new NotFoundException($"Building with id {id} not found.");
            }

            return await CreateResponseAsync(building);
        }

        public async Task<BuildingResponse> GetByNameAsync(string name)
        {
            Building building = await _context.Buildings.FirstOrDefaultAsync(e => e.Name == name);

            if (building == null)
            {
                throw new NotFoundException($"Building with name {name} not found.");
            }

            return await CreateResponseAsync(building);
        }

        public async Task<BuildingResponse> UpdateAsync(Guid id, BuildingRequest request)
        {
            Building building = _converter.DtoToModel(request);
            building.Id = id;

            if (!await _context.Buildings.AnyAsync(b => b.Id == id))
            {
                throw new NotFoundException($"Building with id {id} not found.");
            }

            if (await IsDuplicateAsync(building, id))
            {
                throw new DuplicateException("This building already exists.");
            }

            _context.Update(building);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(building);
        }

        public async Task<BuildingResponse> DeleteAsync(Guid id)
        {
            Building building = await _context.Buildings.FirstOrDefaultAsync(e => e.Id == id);

            if (building == null)
            {
                throw new NotFoundException($"Building with id {id} not found.");
            }

            // Set inactive:
            building.IsActive = false;

            // Get all rooms from this building:
            List<Room> rooms = await _context.Rooms.Where(e => e.BuildingId == id).ToListAsync();

            // Set all rooms to inactive:
            foreach (Room room in rooms)
            {
                room.IsActive = false;
                // Update record:
                _context.Update(room);
                await _context.SaveChangesAsync();
            }

            // Update record:
            _context.Update(building);
            await _context.SaveChangesAsync();

            return await CreateResponseAsync(building);
        }

        private async Task<bool> IsDuplicateAsync(Building building, Guid? id = null)
        {
            if (id == null)
            {
                return await _context.Buildings.AnyAsync(
                    e => e.Name == building.Name
                    && e.Address.PostalCode == building.Address.PostalCode
                    && e.Address.Street == building.Address.Street
                    && e.Address.Number == building.Address.Number
                    && e.IsActive);
            }
            else
            {
                return await _context.Buildings.AnyAsync(
                    e => e.Name == building.Name
                    && e.Address.PostalCode == building.Address.PostalCode
                    && e.Address.Street == building.Address.Street
                    && e.Address.Number == building.Address.Number
                    && e.Address.Addition == building.Address.Addition
                    && e.IsActive && e.Id != id);
            }
        }

        private async Task<BuildingResponse> CreateResponseAsync(Building building)
        {
            BuildingResponse response = _converter.ModelToDto(building);
            response.Address.City = await _context.Cities.FirstOrDefaultAsync(e => e.Id == building.Address.CityId);

            return response;
        }
    }
}
