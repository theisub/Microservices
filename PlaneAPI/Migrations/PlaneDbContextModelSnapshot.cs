﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlaneAPI.Model;

namespace PlaneAPI.Migrations
{
    [DbContext(typeof(PlaneDbContext))]
    partial class PlaneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlaneAPI.Model.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OutCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaneCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<bool>("Transit")
                        .HasColumnType("bit");

                    b.Property<int>("TravelTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Planes");
                });
#pragma warning restore 612, 618
        }
    }
}
