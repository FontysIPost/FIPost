using LocatieService.Database.Datamodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LocatieService.Database.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Name").HasMaxLength(255);
            builder.Property<Guid>("BuildingId").IsRequired();
        }
    }
}
