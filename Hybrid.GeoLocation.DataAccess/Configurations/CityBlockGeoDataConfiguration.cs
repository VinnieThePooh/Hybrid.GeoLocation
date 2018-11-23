using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.GeoLocation.DataAccess.Configurations
{
    public class CityBlockGeoDataConfiguration : IEntityTypeConfiguration<CityBlockGeoData>
    {
        public void Configure(EntityTypeBuilder<CityBlockGeoData> builder)
        {
            builder.ToTable("CityBlocks");
            builder.HasKey(x => x.GeoNameId);
        }
    }
}
