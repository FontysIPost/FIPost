using Microsoft.EntityFrameworkCore;
using PakketService.Database.Contexts;
using PakketService.Database.Converters;
using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using PakketService.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Services
{
    public class PackageService : IPackageService
    {
        private readonly PackageServiceContext _context;
        private readonly IDtoConverter<Package, PackageRequest, PackageResponse> _converter;
        private readonly IDtoConverter<Ticket, TicketRequest, TicketResponse> _ticketConverter;

        public PackageService(PackageServiceContext context, IDtoConverter<Package, PackageRequest, PackageResponse> converter,
            IDtoConverter<Ticket, TicketRequest, TicketResponse> ticketConverter)
        {
            _context = context;
            _converter = converter;
            _ticketConverter = ticketConverter;
        }

        public async Task<PackageResponse> AddAsync(PackageRequest request)
        {
            Package package = _converter.DtoToModel(request);

            package.Tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        CompletedByPersonId = request.CreatedByPersonId,
                        LocationId = request.CreatedAtLocationId,
                        FinishedAt = DateTimeOffset.Now.ToUnixTimeSeconds()
                    }
                };

            await _context.AddAsync(package);
            await _context.SaveChangesAsync();

            return _converter.ModelToDto(package);
        }

        public async Task<List<PackageResponse>> GetAllAsync()
        {
            List<PackageResponse> packages = _converter.ModelToDto(await _context.Package.ToListAsync());

            foreach (PackageResponse package in packages)
            {
                package.Tickets = _ticketConverter.ModelToDto(await _context.Ticket.Where(t => t.PackageId == package.Id).ToListAsync());
            }

            return packages;
        }

        public async Task<PackageResponse> GetByIdAsync(Guid id)
        {
            PackageResponse package = _converter.ModelToDto(await _context.Package.FirstOrDefaultAsync(e => e.Id == id));

            if (package == null)
            {
                throw new NotFoundException($"Package with id {id} not found.");
            }

            package.Tickets = _ticketConverter.ModelToDto(await _context.Ticket.Where(t => t.PackageId == id).ToListAsync());

            return package;
        }

        public async Task<PackageResponse> UpdateAsync(Guid id, PackageRequest request)
        {
            Package package = _converter.DtoToModel(request);
            package.Id = id;

            if (!await _context.Package.AnyAsync(b => b.Id == id))
            {
                throw new NotFoundException($"Building with id {id} not found.");
            }

            _context.Update(package);
            await _context.SaveChangesAsync();

            return _converter.ModelToDto(package);
        }
    }
}
