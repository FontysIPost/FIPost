using LocatieService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Services
{
    public interface ICityService
    {
        Task<CityResponse> AddAsync(CityRequest request);
        Task<List<CityResponse>> GetAllAsync();
        Task<CityResponse> GetByIdAsync(Guid id);
        Task<CityResponse> GetByNameAsync(string name);
        Task<CityResponse> UpdateAsync(Guid id, CityRequest request);
        Task<CityResponse> DeleteAsync(Guid id);
    }
}
