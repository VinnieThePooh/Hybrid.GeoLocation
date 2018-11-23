﻿// <auto-generated />
using Hybrid.GeoLocation.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hybrid.GeoLocation.API.Migrations
{
    [DbContext(typeof(GeoContext))]
    partial class GeoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CityBlockGeoData", b =>
                {
                    b.Property<int>("GeoNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccuracyRadius");

                    b.Property<bool>("IsAnonymousProxy");

                    b.Property<bool>("IsSatelliteProvider");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Network");

                    b.Property<string>("NetworkIPv6");

                    b.Property<string>("PostalCode");

                    b.Property<int>("RegisteredCountryGeoNameId");

                    b.Property<int>("RepresentedCountryGeoNameId");

                    b.HasKey("GeoNameId");

                    b.HasIndex("RegisteredCountryGeoNameId");

                    b.ToTable("CityBlocks");
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CityGeoData", b =>
                {
                    b.Property<int>("GeoNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<string>("ContinentCode");

                    b.Property<string>("ContinentName");

                    b.Property<int>("CountryGeoNameId");

                    b.Property<string>("CountryISOCode")
                        .IsRequired();

                    b.Property<string>("CountryName");

                    b.Property<bool>("IsInEuropeanUnion");

                    b.Property<string>("LocaleCode");

                    b.Property<string>("MetroCode");

                    b.Property<string>("Subdivision2ISOCode");

                    b.Property<string>("Subdivision2Name");

                    b.Property<string>("SubdivisionISOCode");

                    b.Property<string>("SubdivisionName");

                    b.Property<string>("TimeZone");

                    b.HasKey("GeoNameId");

                    b.HasIndex("CountryISOCode");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CountryBlockGeoData", b =>
                {
                    b.Property<int>("GeoNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAnonymousProxy");

                    b.Property<bool>("IsSatelliteProvider");

                    b.Property<string>("Network");

                    b.Property<string>("NetworkIPv6");

                    b.Property<int>("RegisteredCountryGeoNameId");

                    b.Property<int>("RepresentedCountryGeoNameId");

                    b.HasKey("GeoNameId");

                    b.HasIndex("RegisteredCountryGeoNameId");

                    b.ToTable("CountryBlocks");
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CountryGeoData", b =>
                {
                    b.Property<int>("GeoNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContinentCode");

                    b.Property<string>("ContinentName");

                    b.Property<string>("CountryISOCode")
                        .IsRequired();

                    b.Property<string>("CountryName");

                    b.Property<bool>("IsInEuropeanUnion");

                    b.Property<string>("LocaleCode");

                    b.HasKey("GeoNameId");

                    b.HasIndex("CountryISOCode")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CityBlockGeoData", b =>
                {
                    b.HasOne("Hybrid.GeoLocation.Domain.Models.CountryGeoData", "CountryGeoData")
                        .WithMany("CityBlockGeoData")
                        .HasForeignKey("RegisteredCountryGeoNameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CityGeoData", b =>
                {
                    b.HasOne("Hybrid.GeoLocation.Domain.Models.CountryGeoData", "CountryGeoData")
                        .WithMany("CityGeoData")
                        .HasForeignKey("CountryISOCode")
                        .HasPrincipalKey("CountryISOCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Hybrid.GeoLocation.Domain.Models.CountryBlockGeoData", b =>
                {
                    b.HasOne("Hybrid.GeoLocation.Domain.Models.CountryGeoData", "CountryGeoData")
                        .WithMany("CountryBlockGeoData")
                        .HasForeignKey("RegisteredCountryGeoNameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
