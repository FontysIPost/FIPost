using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Services
{
    public interface IBuildingService
    {
        Task<BuildingResponse> AddAsync(BuildingRequest request);
        Task<List<BuildingResponse>> GetAllAsync();
        Task<BuildingResponse> GetByIdAsync(Guid id);
        Task<BuildingResponse> GetByNameAsync(string name);
        Task<BuildingResponse> UpdateAsync(Guid id, BuildingRequest request);
        Task<BuildingResponse> DeleteAsync(Guid id);
    }
}
