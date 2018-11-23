﻿using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;
using Hybrid.GeoLocation.DataAccess;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv
{
    public interface IGeoLiteCsvUpdater
    {
        Task UpdateCountries(string zipUrl, CsvLanguage language);
        Task UpdateCities(string zipUrl, CsvLanguage language);
    }
}
