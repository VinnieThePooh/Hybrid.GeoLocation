using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.GeoLocation.DataAccess.Configurations
{
    public class CountryBlockGeoDataConfiguration : IEntityTypeConfiguration<CountryBlockGeoData>
    {
        public void Configure(EntityTypeBuilder<CountryBlockGeoData> builder)
        {
            builder.ToTable("CountryBlocks");
            builder.HasKey(x => x.GeoNameId);
        }
    }
}
