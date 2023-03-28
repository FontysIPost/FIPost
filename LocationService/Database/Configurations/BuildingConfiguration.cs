using LocatieService.Database.Datamodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocatieService.Database.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("buildings");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Name").HasMaxLength(255);

            builder.OwnsOne(x => x.Address);
        }
    }
}
