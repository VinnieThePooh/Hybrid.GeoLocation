using Hybrid.GeoLocation.DataAccess.Providers;
using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.BusinessLogic.GeoProviders
{
    public class CityGeoProvider : ICityGeoProvider
    {
        public Task<CityBlockGeoData> GetCityBlockGeoData(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<CityGeoData> GetCityGeoData(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
