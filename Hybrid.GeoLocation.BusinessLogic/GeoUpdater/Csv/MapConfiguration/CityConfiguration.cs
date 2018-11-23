using CsvHelper.Configuration;
using Hybrid.GeoLocation.Domain.Models;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv
{
    class CityConfiguration: ClassMap<CityGeoData>
    {
        public CityConfiguration()
        {
            Map(m => m.GeoNameId).Name("geoname_id");
            Map(m => m.LocaleCode).Name("locale_code");
            Map(m => m.ContinentCode).Name("continent_code");
            Map(m => m.ContinentName).Name("continent_name");
            Map(m => m.CountryISOCode).Name("country_iso_code");
            Map(m => m.CountryName).Name("country_name");
            Map(m => m.SubdivisionISOCode).Name("subdivision_1_iso_code");
            Map(m => m.SubdivisionName).Name("subdivision_1_name");
            Map(m => m.Subdivision2ISOCode).Name("subdivision_2_iso_code");
            Map(m => m.Subdivision2Name).Name("subdivision_2_name");
            Map(m => m.CityName).Name("city_name");
            Map(m => m.MetroCode).Name("metro_code");
            Map(m => m.TimeZone).Name("time_zone");
            Map(m => m.IsInEuropeanUnion).Name("is_in_european_union");
        }
    }
}
