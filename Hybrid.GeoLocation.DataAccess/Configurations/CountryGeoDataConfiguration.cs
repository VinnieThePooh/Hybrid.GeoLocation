using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hybrid.GeoLocation.DataAccess.Configurations
{
    public class CountryGeoDataConfiguration : IEntityTypeConfiguration<CountryGeoData>
    {
        public void Configure(EntityTypeBuilder<CountryGeoData> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(x => x.GeoNameId);
            builder.HasIndex(x => x.CountryISOCode).IsUnique();
            builder.HasMany(x => x.CityGeoData)
                        .WithOne(x => x.CountryGeoData).IsRequired()
                        .HasForeignKey(x => x.CountryISOCode)
                        .HasPrincipalKey(x => x.CountryISOCode)
                        .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CityBlockGeoData).WithOne(x => x.CountryGeoData).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CountryBlockGeoData).WithOne(x => x.CountryGeoData).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
