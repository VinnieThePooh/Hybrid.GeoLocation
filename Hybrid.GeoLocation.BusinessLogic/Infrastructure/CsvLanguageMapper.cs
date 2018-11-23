using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;
using System.Collections.Generic;

namespace Hybrid.GeoLocation.BusinessLogic.Infrastructure
{
    public static class CsvLanguageMapper
    {
        private static Dictionary<CsvLanguage, string> Map = new Dictionary<CsvLanguage, string>
        {
            { CsvLanguage.English, "en" },
            { CsvLanguage.Russian, "ru" }
        };
    }
}
