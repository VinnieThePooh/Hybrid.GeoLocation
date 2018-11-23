using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;
using Hybrid.GeoLocation.BusinessLogic.Infrastructure;
using Hybrid.GeoLocation.DataAccess;
using Hybrid.GeoLocation.Domain.Models;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv
{

    // todo: refactor
    public class GeoLiteCsvUpdater : IGeoLiteCsvUpdater
    {
        public GeoLiteCsvUpdater(GeoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DownloadDirectory = Path.Combine(appDirectory, "Data");          

        }

        public string DownloadDirectory { get;  }        

        private const string citiesFileNameRu = "";
        private const string countriesFileNameRu = "";
        private readonly GeoContext context;       

        
        // todo: refactor
        public async Task UpdateCountries(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            if (string.IsNullOrEmpty(zipUrl))
                throw new ArgumentException(zipUrl);

            Directory.CreateDirectory(DownloadDirectory);

            string pathToDownload = string.Empty;

            await Task.Run(() => {

                using (var webClient = new WebClient())
                {
                    var fileName = zipUrl.Split('/').Last();
                    pathToDownload = Path.Combine(DownloadDirectory, fileName);
                    webClient.DownloadFile(new Uri(zipUrl), pathToDownload);
                }
            });

            var langCode = CsvLanguageMapper.Map[csvLanguage];

            string entryPath = string.Empty;

            using (var stream = File.OpenRead(pathToDownload))
            using (var zipArchive = new ZipArchive(stream))
            {
                var entry = zipArchive.Entries.Single(x => x.Name.Contains(langCode));
                entryPath = Path.Combine(DownloadDirectory, entry.Name);

                // remove old version of extracted csv
                File.Delete(entryPath);

                entry.ExtractToFile(entryPath);
            }            

            using (var reader = new StreamReader(entryPath))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.RegisterClassMap<CountryClassMapConfiguration>();
                var data = csvReader.GetRecords<CountryGeoData>().ToList();
                var filtered = data.Where(x => !string.IsNullOrEmpty(x.CountryISOCode) && !string.IsNullOrEmpty(x.CountryName)).ToList();

                // very plain update logic is used
                // old data are removed then new data are inserted
                // thanks god table is very small
                context.Countries.RemoveRange(context.Countries);
                await context.SaveChangesAsync();

                context.Countries.AddRange(filtered);
                await context.SaveChangesAsync();
            }            
        }       
        

        public Task UpdateCities(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            if (string.IsNullOrEmpty(zipUrl))
                throw new ArgumentException(zipUrl);

            Directory.CreateDirectory(DownloadDirectory);
            

            return Task.CompletedTask;

        }
    }
}
