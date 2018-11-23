using System.ComponentModel.DataAnnotations.Schema;

namespace Hybrid.GeoLocation.Domain.Models
{
    public class CityGeoData
    {
        public int GeoNameId { get; set; }
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }        
        public string CountryISOCode { get; set; }
        public string CountryName { get; set; }
        public string SubdivisionISOCode { get; set; }
        public string SubdivisionName { get; set; }
        public string Subdivision2ISOCode { get; set; }
        public string Subdivision2Name { get; set; }
        public string CityName { get; set; }
        public string MetroCode { get; set; }
        public string TimeZone { get; set; }
        public bool IsInEuropeanUnion { get; set; }

        // extra field - no present in csv
        public int CountryGeoNameId { get; set; }
        
        public CountryGeoData CountryGeoData { get; set; }

        public static CityGeoData EmptyGeoData => null; 
    }
}
