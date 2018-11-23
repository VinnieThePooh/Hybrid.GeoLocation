using System;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;
using Hybrid.GeoLocation.BusinessLogic.Infrastructure;
using Hybrid.GeoLocation.DataAccess;
using Hybrid.GeoLocation.DataAccess.Extensions;
using Hybrid.GeoLocation.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
                var connection = context.Database.GetDbConnection();
                using (var command = connection.CreateCommand())
                {                
                    connection.Open();

                    context.Countries.RemoveRange(context.Countries);
                    await context.SaveChangesAsync();

                    //DropForeingKeysAtCountriesTable(connection, command);

                    //var tableName = "Countries";
                    //command.CommandText = $"truncate table {tableName.WrapWithQuotes()}";
                    //command.ExecuteNonQuery();                    

                    //RecreateForeignKeysAtCountriesTable(connection, command);
                    

                    await context.Countries.AddRangeAsync(filtered);
                    await context.SaveChangesAsync();
                    connection.Close();
                }                
            }            
        }
        
        private void DropForeingKeysAtCountriesTable(DbConnection connection, DbCommand command)
        {
            var fkName = "FK_Cities_Countries_CountryISOCode";
            command.CommandText = $"alter table {"Cities".WrapWithQuotes()} drop constraint {fkName.WrapWithQuotes()}";
            command.ExecuteNonQuery();

            fkName = "FK_CityBlocks_Countries_RegisteredCountryGeoNameId";
            command.CommandText = $"alter table {"CityBlocks".WrapWithQuotes()} drop constraint {fkName.WrapWithQuotes()}";
            command.ExecuteNonQuery();

            fkName = "FK_CountryBlocks_Countries_RegisteredCountryGeoNameId";
            command.CommandText = $"alter table {"CountryBlocks".WrapWithQuotes()} drop constraint {fkName.WrapWithQuotes()}";
            command.ExecuteNonQuery();            
        }

        private void RecreateForeignKeysAtCountriesTable(DbConnection connection, DbCommand command)
        {            
            var fkName = "FK_Cities_Countries_CountryISOCode";
            command.CommandText = $"alter table {"Cities".WrapWithQuotes()} add constraint {fkName.WrapWithQuotes()} FOREIGN KEY(\"CountryISOCode\")" +
            "REFERENCES public.\"Countries\" (\"CountryISOCode\") MATCH SIMPLE ON UPDATE NO ACTION ON DELETE SET NULL;";
            command.ExecuteNonQuery();

            fkName = "FK_CityBlocks_Countries_RegisteredCountryGeoNameId";
            command.CommandText = $"alter table {"CityBlocks".WrapWithQuotes()} add constraint {fkName.WrapWithQuotes()} FOREIGN KEY (\"RegisteredCountryGeoNameId\")" +
            "REFERENCES public.\"Countries\" (\"GeoNameId\") MATCH SIMPLE ON UPDATE NO ACTION ON DELETE SET NULL";
            command.ExecuteNonQuery();

            fkName = "FK_CountryBlocks_Countries_RegisteredCountryGeoNameId";
            command.CommandText = $"alter table {"CountryBlocks".WrapWithQuotes()} add constraint {fkName.WrapWithQuotes()} FOREIGN KEY (\"RegisteredCountryGeoNameId\")" +
            "REFERENCES public.\"Countries\" (\"GeoNameId\") MATCH SIMPLE ON UPDATE NO ACTION ON DELETE SET NULL;";
            command.ExecuteNonQuery();            
        }

        public Task UpdateCities(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            //throw new NotImplementedException();

            Directory.CreateDirectory(DownloadDirectory);

            return Task.CompletedTask;

        }
    }
}
