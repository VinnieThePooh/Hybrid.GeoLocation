using System.ComponentModel.DataAnnotations.Schema;

namespace Hybrid.GeoLocation.Domain.Models
{
    public class CityBlockGeoData
    {
        public int GeoNameId { get; set; }

        public string Network { get; set; }

        public string NetworkIPv6 { get; set; }
        
        public int RegisteredCountryGeoNameId { get; set; }

        public int RepresentedCountryGeoNameId { get; set; }

        public bool IsAnonymousProxy { get; set; }

        public bool IsSatelliteProvider { get; set; }

        public string PostalCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int AccuracyRadius { get; set; }

        [ForeignKey(nameof(RegisteredCountryGeoNameId))]
        public CountryGeoData CountryGeoData { get; set; }

        public static CityBlockGeoData EmptyGeoData => null;
    }
}
