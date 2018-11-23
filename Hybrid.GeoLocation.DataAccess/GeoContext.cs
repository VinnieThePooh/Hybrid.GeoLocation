using Hybrid.GeoLocation.DataAccess.Configurations;
using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Hybrid.GeoLocation.DataAccess
{
    public class GeoContext: DbContext
    {
        public GeoContext(DbContextOptions<GeoContext> options) : base(options)
        {
            Countries = Set<CountryGeoData>();
            Cities = Set<CityGeoData>();
            CityBlocks = Set<CityBlockGeoData>();
        }

        public DbSet<CountryGeoData> Countries { get; }
        public DbSet<CityGeoData> Cities { get; }
        public DbSet<CityBlockGeoData> CityBlocks { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CountryGeoDataConfiguration());
            modelBuilder.ApplyConfiguration(new CountryBlockGeoDataConfiguration());
            modelBuilder.ApplyConfiguration(new CityGeoDataConfiguration());
            modelBuilder.ApplyConfiguration(new CityBlockGeoDataConfiguration());
        }        
    }
}
