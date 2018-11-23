using System.Collections.Generic;

namespace Hybrid.GeoLocation.Domain.Models
{
    public class CountryGeoData
    {
        public int GeoNameId { get; set; }
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryISOCode { get; set; }
        public string CountryName { get; set; }
        public bool IsInEuropeanUnion { get; set; }

        public List<CityBlockGeoData> CityBlockGeoData { get; set; } = new List<CityBlockGeoData>();
        public List<CityGeoData> CityGeoData { get; set; } = new List<CityGeoData>();
        public List<CountryBlockGeoData> CountryBlockGeoData { get; set; } = new List<CountryBlockGeoData>();

        public static CountryGeoData EmptyGeoData => null;
    }
}
