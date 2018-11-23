using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.DataAccess.Repositories
{
    public interface ICountryGeoDataRepository
    {
        Task<CountryGeoData> GetAllCountryGeoData();
        Task<CountryGeoData> GetSingleCountryByFilter(Expression<Func<CountryGeoData, bool>> filter);
        Task<CountryGeoData> GetCountriesByFilter(Expression<Func<CountryGeoData, bool>> filter);
    }
}
