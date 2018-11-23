using Hybrid.GeoLocation.DataAccess.GeoProviders;
using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.BusinessLogic.GeoProviders
{
    public class CountryGeoProvider : ICountryGeoProvider
    {
        public Task<CountryGeoData> GetGeoDataByIPv4(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<CountryGeoData> GetGeoDataByIPv6(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
