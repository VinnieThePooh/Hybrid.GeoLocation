using CsvHelper.Configuration;
using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv
{
    public class CountryConfiguration: ClassMap<CountryGeoData>
    {
        public CountryConfiguration()
        {
            Map(m => m.GeoNameId).Name("geoname_id");
            Map(m => m.LocaleCode).Name("locale_code");
            Map(m => m.ContinentCode).Name("continent_code");
            Map(m => m.ContinentName).Name("continent_name");
            Map(m => m.CountryISOCode).Name("country_iso_code");
            Map(m => m.CountryName).Name("country_name");
            Map(m => m.IsInEuropeanUnion).Name("is_in_european_union");
        }
    }
}
