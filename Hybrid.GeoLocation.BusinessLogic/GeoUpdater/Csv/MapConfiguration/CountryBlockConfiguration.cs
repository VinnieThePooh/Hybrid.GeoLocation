using CsvHelper.Configuration;
using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv.MapConfiguration
{
    public class CountryBlockConfiguration: ClassMap<CountryBlockGeoData>
    {
        public CountryBlockConfiguration()
        {
            Map(x => x.Network).Name("network");
            Map(x => x.GeoNameId).Name("geoname_id");
            Map(x => x.RegisteredCountryGeoNameId).Name("registered_country_geoname_id");
            Map(x => x.RepresentedCountryGeoNameId).Name("represented_country_geoname_id");
            Map(x => x.IsAnonymousProxy).Name("is_anonymous_proxy");
            Map(x => x.IsSatelliteProvider).Name("is_satellite_provider");
        }
    }    
}
