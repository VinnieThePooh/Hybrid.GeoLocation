using Hybrid.GeoLocation.Domain.Models;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.DataAccess.GeoProviders
{
    public interface ICountryGeoProvider
    {
        Task<CountryGeoData> GetGeoDataByIPv4(string ipAddress);
        Task<CountryGeoData> GetGeoDataByIPv6(string ipAddress);
    }
}
