using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv.MapConfiguration;
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

            var entryPaths = await DownloadAndExtractZip(zipUrl, csvLanguage);

            foreach (var path in entryPaths)
            {
                using (var reader = new StreamReader(path))
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
        }       
        

        public Task UpdateCities(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            if (string.IsNullOrEmpty(zipUrl))
                throw new ArgumentException(zipUrl);

            Directory.CreateDirectory(DownloadDirectory);
            

            return Task.CompletedTask;

        }


        private Task<string[]> DownloadAndExtractZip(string zipUrl, CsvLanguage language = CsvLanguage.NotDefined)
        {
            return Task.Run(() => {

                string pathToDownload = string.Empty;

                using (var webClient = new WebClient())
                {
                    var fileName = zipUrl.Split('/').Last();
                    pathToDownload = Path.Combine(DownloadDirectory, fileName);
                    webClient.DownloadFile(new Uri(zipUrl), pathToDownload);
                }

                var langCode = CsvLanguageMapper.Map[language];

                string entryPath = string.Empty;

                using (var stream = File.OpenRead(pathToDownload))
                using (var zipArchive = new ZipArchive(stream))
                {
                    if (language != CsvLanguage.NotDefined)
                    {
                        var entry = zipArchive.Entries.Single(x => x.Name.Contains(langCode));
                        entryPath = Path.Combine(DownloadDirectory, entry.Name);

                        // remove old version of extracted csv
                        File.Delete(entryPath);

                        entry.ExtractToFile(entryPath);
                        return new string[] { entryPath };
                    }

                    // blocks data are extracted

                    var entries = zipArchive.Entries.Where(x => x.Name.Contains("IPv4") || x.Name.Contains("IPv6")).ToList();

                    var entriePaths = new List<string>();

                    entries.ForEach(entry => {
                        var enPath = Path.Combine(DownloadDirectory, entry.Name);

                        // delete old if exists
                        File.Delete(enPath);
                        entry.ExtractToFile(enPath);
                        entriePaths.Add(enPath);
                    });

                    return entriePaths.ToArray();                
                }
            });
        }


        public async Task UpdateCountryBlocks(string zipUrl)
        {
            if (string.IsNullOrEmpty(zipUrl))
                throw new ArgumentException(zipUrl);

            Directory.CreateDirectory(DownloadDirectory);

            var extractedPaths = await DownloadAndExtractZip(zipUrl);

            int counter = 0;

            foreach (var path in extractedPaths)
            {
                using (var reader = new StreamReader(path))
                using (var csvReader = new CsvReader(reader))
                {
                    if (counter == 0)
                    {
                        csvReader.Configuration.RegisterClassMap<CountryBlockConfiguration>();
                        context.CountryBlocks.RemoveRange(context.CountryBlocks); 
                        await context.SaveChangesAsync();
                        
                    }
                    else
                    {
                        csvReader.Configuration.RegisterClassMap<CountryBlockIPv6Configuration>();
                    }

                    var data = csvReader.GetRecords<CountryBlockGeoData>().ToList();


                    // IPv4 and IPv6 are considered as different objects
                    context.CountryBlocks.AddRange(data);
                    await context.SaveChangesAsync();
                }
                counter++;
            }            
        }

        public Task UpdateCityBlocks(string zipUrl, CsvLanguage language)
        {
            throw new NotImplementedException();
        }
    }
}
