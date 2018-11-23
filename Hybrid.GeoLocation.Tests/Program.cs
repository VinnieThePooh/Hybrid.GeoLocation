using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv;
using Hybrid.GeoLocation.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Hybrid.GeoLocation.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            Console.ReadKey(true);
        }


        static async Task MainAsync()
        {
            await TestGeoUpdater();
        }



        static async Task TestGeoUpdater()
        {
            var conString = "User ID=ryan; Password=ryanPass12; Host=localhost; Port=5432; Database=Hybrid.Geo; Pooling=true;";

            var optionsBuilder = new DbContextOptionsBuilder<GeoContext>();
            optionsBuilder.UseNpgsql(conString);


            using (var context = new GeoContext(optionsBuilder.Options))
            {
                var updater = new GeoLiteCsvUpdater(context);

                var zipUrl = "http://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip";

                await updater.UpdateCountries(zipUrl, BusinessLogic.GeoUpdater.Enums.CsvLanguage.English);
            }

        }

    }
}
