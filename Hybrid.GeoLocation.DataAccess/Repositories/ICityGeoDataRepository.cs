using Hybrid.GeoLocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.DataAccess.Repositories
{
    public interface ICityGeoDataRepository
    {
        Task<List<CityGeoData>> GetAllCitiesGeoData();
        Task<CityGeoData> GetSingleCityByFilter(Expression<Func<CountryGeoData, bool>> filter);
        Task<List<CityGeoData>> GetCitiesByFilter(Expression<Func<CountryGeoData, bool>> filter);
    }
}
