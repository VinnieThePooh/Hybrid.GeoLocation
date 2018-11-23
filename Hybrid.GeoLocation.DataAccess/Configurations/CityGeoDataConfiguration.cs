using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hybrid.GeoLocation.DataAccess.Configurations
{
    public class CityGeoDataConfiguration : IEntityTypeConfiguration<CityGeoData>
    {
        public void Configure(EntityTypeBuilder<CityGeoData> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(x => x.GeoNameId);
        }
    }
}
