using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Enums;
using Hybrid.GeoLocation.DataAccess;

namespace Hybrid.GeoLocation.BusinessLogic.GeoUpdater.Csv
{
    public class GeoLiteUpdater : IGeoLiteCsvUpdater
    {
        public GeoLiteUpdater(GeoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DownloadDirectory = Path.Combine(appDirectory, "Data");            
        }

        public string DownloadDirectory { get;  }        

        private const string citiesFileNameRu = "";
        private const string countriesFileNameRu = "";
        private readonly GeoContext context;       

        

        public async Task UpdateCountries(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            if (string.IsNullOrEmpty(zipUrl))
                throw new ArgumentException(zipUrl);           

            string pathToDownload;

            await Task.Run(() => {

                using (var webClient = new WebClient())
                {
                    var fileName = zipUrl.Split('/').Last();
                    pathToDownload = Path.Combine(DownloadDirectory, fileName);
                    webClient.DownloadFile(new Uri(zipUrl), pathToDownload);
                }
            });




                      
        }

        public Task UpdateCities(string zipUrl, CsvLanguage csvLanguage = CsvLanguage.Russian)
        {
            throw new NotImplementedException();
        }
    }
}
