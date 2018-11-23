using Hybrid.GeoLocation.Domain.Models;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.DataAccess.Providers
{
    public interface ICityGeoProvider
    {        
        /// <param name="ipAddress">IPv4 or IPv6</param>        
        Task<CityGeoData> GetCityGeoData(string ipAddress);
        
        /// <param name="ipAddress">IPv4 or IPv6</param>        
        Task<CityBlockGeoData> GetCityBlockGeoData(string ipAddress);
    }
}
