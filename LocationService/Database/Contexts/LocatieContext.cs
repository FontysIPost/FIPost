using LocatieService.Database.Configurations;
using LocatieService.Database.Datamodels;
using Microsoft.EntityFrameworkCore;

namespace LocatieService.Database.Contexts
{
    public class LocatieContext : DbContext
    {
        public LocatieContext(DbContextOptions<LocatieContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BuildingConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
