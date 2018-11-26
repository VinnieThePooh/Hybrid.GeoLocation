using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv;
using Hybrid.GeoLocation.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;

namespace Hybrid.GeoLocation.Tests
{
    class Program
    {
        private static  readonly string ConnectionString = "User ID=ryan; Password=ryanPass12; Host=localhost; Port=5432; Database=Hybrid.Geo; Pooling=true;";
        private static DbContextOptionsBuilder<GeoContext> optionsBuilder = new DbContextOptionsBuilder<GeoContext>();

        static Program()
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }


        static void Main(string[] args)
        {
            try
            {
                MainAsync().Wait();
            }
            catch (Exception exc)
            {
                Console.WriteLine();
                Console.WriteLine(exc.ToString());      
            }
            Console.ReadKey(true);
        }


        static async Task MainAsync()
        {
            //await TestCountriesUpdate();
            await TestCountryBlocksUpdate();
        }



        static async Task TestCountriesUpdate()
        {
            using (var context = new GeoContext(optionsBuilder.Options))
            {
                var updater = new GeoLiteCsvUpdater(context);

                var zipUrl = "http://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip";

                Console.Write("Updating Countries table with russian-based data..");
                await updater.UpdateCountries(zipUrl, CsvLanguage.Russian);
                Console.WriteLine("completed.");

                Console.Write("Updating Countries table with english-based data..");
                await updater.UpdateCountries(zipUrl, CsvLanguage.English);
                Console.WriteLine("completed.");
            }
        }


        static async Task TestCountryBlocksUpdate()
        {
            using (var context = new GeoContext(optionsBuilder.Options))
            {
                var updater = new GeoLiteCsvUpdater(context);

                var zipUrl = "http://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip";

                Console.Write("Updating country blocks with russian-based data..");
                await updater.UpdateCountryBlocks(zipUrl, CsvLanguage.Russian);
                Console.WriteLine("completed.");

                Console.Write("Updating country blocks with english-based data..");
                await updater.UpdateCountryBlocks(zipUrl, CsvLanguage.English);
                Console.WriteLine("completed.");
            }

        }

    }
}
