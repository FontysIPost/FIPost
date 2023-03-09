using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Services
{
    public interface IRoomService
    {
        Task<RoomResponse> AddAsync(RoomRequest request);
        Task<List<RoomResponse>> GetAllAsync();
        Task<RoomResponse> GetByIdAsync(Guid id);
        Task<RoomResponse> GetByNameAsync(string name);
        Task<RoomResponse> UpdateAsync(Guid id, RoomRequest request);
        Task<RoomResponse> DeleteRoomAsync(Guid id);
    }
}
