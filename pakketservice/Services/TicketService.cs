using Microsoft.EntityFrameworkCore;
using PakketService.Database.Contexts;
using PakketService.Database.Converters;
using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using PakketService.helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Services
{
    public class TicketService : ITicketService
    {
        private readonly PackageServiceContext _context;
        private readonly IDtoConverter<Ticket, TicketRequest, TicketResponse> _converter;

        public TicketService(PackageServiceContext context, IDtoConverter<Ticket, TicketRequest, TicketResponse> converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<TicketResponse> AddAsync(TicketRequest request)
        {
            Package package = await _context.Package.FirstOrDefaultAsync(p => p.Id == request.PackageId);
            if (package == null)
            {
                throw new NotFoundException($"Package with id {request.PackageId} not found.");
            }

            var a = _context.Ticket
                .Where(t => t.PackageId == request.PackageId)
                .OrderByDescending(t => t.FinishedAt).ToList();
            if (a.FirstOrDefault().LocationId == request.LocationId)
            {
                throw new NotFoundException($"Package with id {request.PackageId} cannot be deliverd to the same location.");
            }


            Ticket ticket = _converter.DtoToModel(request);
            ticket.FinishedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
            await _context.AddAsync(ticket);

            if (ticket.LocationId == package.CollectionPointId)
            {
                package.RouteFinished = true;
                _context.Update(package);
            }

            await _context.SaveChangesAsync();
            return _converter.ModelToDto(ticket);
        }
    }
}
