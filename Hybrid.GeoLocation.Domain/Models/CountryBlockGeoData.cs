using System.ComponentModel.DataAnnotations.Schema;

namespace Hybrid.GeoLocation.Domain.Models
{
    public class CountryBlockGeoData
    {
        public int GeoNameId { get; set; }

        public string Network { get; set; }

        public string NetworkIPv6 { get; set; }

        public int RegisteredCountryGeoNameId { get; set; }

        public int RepresentedCountryGeoNameId { get; set; }

        public bool IsAnonymousProxy { get; set; }

        public bool IsSatelliteProvider { get; set; }

        [ForeignKey(nameof(RegisteredCountryGeoNameId))]
        public CountryGeoData CountryGeoData { get; set; }

        public static CountryBlockGeoData EmptyGeoData => null;
    }
}
