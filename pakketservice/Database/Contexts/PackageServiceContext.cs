using Microsoft.EntityFrameworkCore;
using PakketService.Database.Datamodels;

namespace PakketService.Database.Contexts
{
    public class PackageServiceContext : DbContext
    {
        public PackageServiceContext(DbContextOptions<PackageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Package { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

    }
}