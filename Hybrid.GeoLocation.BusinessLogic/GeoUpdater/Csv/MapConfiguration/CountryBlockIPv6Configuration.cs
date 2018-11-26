using CsvHelper.Configuration;
using Hybrid.GeoLocation.Domain.Models;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv.MapConfiguration
{
    public class CountryBlockIPv6Configuration : ClassMap<CountryBlockGeoData>
    {
        public CountryBlockIPv6Configuration()
        {
            Map(x => x.NetworkIPv6).Name("network");
            Map(x => x.GeoNameId).Name("geoname_id");
            Map(x => x.RegisteredCountryGeoNameId).Name("registered_country_geoname_id");
            Map(x => x.RepresentedCountryGeoNameId).Name("represented_country_geoname_id");
            Map(x => x.IsAnonymousProxy).Name("is_anonymous_proxy");
            Map(x => x.IsSatelliteProvider).Name("is_satellite_provider");
        }
    }
}
