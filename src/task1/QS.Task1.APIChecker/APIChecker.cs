using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QS.Task1.APIChecker.Configuration;
using QS.Task1.APIChecker.Interfaces;
using QS.Task1.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Task1.APIChecker
{
    /// <summary>
    /// Azure function to check the API
    /// </summary>
    public class APIChecker
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorageService _azureStorageService;

        public APIChecker(IAPIClient apiClient, IConfiguration configuration, IAzureStorageService azureStorageService)
        {
            _apiClient = apiClient;
            _configuration = configuration;
            _azureStorageService = azureStorageService;
        }

        /// <summary>
        /// At one-minute intervals, retrieve data from external API, then record the outcome of each attempt, 
        /// successful or not, in a azure table and save the entire payload in a blob storage.
        /// </summary>
        /// <param name="myTimer"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("APIChecker")]
        public async Task Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
               var resposne = await _apiClient.GetRandomAPI(_configuration["APIChecker-URI"]);
                if(resposne.Result != null && !String.IsNullOrEmpty(resposne.File))
                {
                    var fileName = $"{Path.GetRandomFileName()}.json";
                    await _azureStorageService.SaveAPIResponseToBlobStorage(fileName, resposne.File);

                    foreach (var entry in resposne.Result.Entries)
                    {
                        var apiEntry = Mapper.Map(entry);
                        apiEntry.FileName = fileName;
                        await _azureStorageService.AddAPIResponseToTableStorage(apiEntry);
                    }
                }
            } catch (Exception ex) {
                log.LogError(ex, "An error occurred during API Check.");
                await _azureStorageService.AddAPIErrorResponseToTableStorage(ex.Message);
            }
        }
    }
}
