using PakketService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PakketService.Services
{
    public interface IPackageService
    {
        Task<PackageResponse> AddAsync(PackageRequest request);
        Task<List<PackageResponse>> GetAllAsync();
        Task<PackageResponse> GetByIdAsync(Guid id);
        Task<PackageResponse> UpdateAsync(Guid id, PackageRequest request);
    }
}
