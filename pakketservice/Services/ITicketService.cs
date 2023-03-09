using PakketService.Database.Datamodels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Services
{
    public interface ITicketService
    {
        Task<TicketResponse> AddAsync(TicketRequest request);
    }
}
