// <auto-generated />
using LocatieService.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace LocatieService.Migrations
{
    [DbContext(typeof(LocatieContext))]
    [Migration("20210415103133_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LocatieService.Database.Datamodels.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("buildings");
                });

            modelBuilder.Entity("LocatieService.Database.Datamodels.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("LocatieService.Database.Datamodels.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("LocatieService.Database.Datamodels.Building", b =>
                {
                    b.OwnsOne("LocatieService.Database.Datamodels.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("BuildingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Addition")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("CityId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BuildingId");

                            b1.ToTable("buildings");

                            b1.WithOwner()
                                .HasForeignKey("BuildingId");
                        });

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}