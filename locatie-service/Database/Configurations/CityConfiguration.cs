using LocatieService.Database.Datamodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocatieService.Database.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Name").HasMaxLength(255);
        }
    }
}
