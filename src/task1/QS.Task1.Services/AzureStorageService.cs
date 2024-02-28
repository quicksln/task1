using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QS.Task1.Services.Interfaces;
using QS.Task1.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QS.Task1.Services
{
    /// <summary>
    /// Azure Storage Service responsible for saving API 
    /// response to Blob Storage and Table Storage
    /// </summary>
    public class AzureStorageService : IAzureStorageService
    {
        private readonly ILogger<AzureStorageService> _logger;
        private readonly IConfiguration _configuration;
        public AzureStorageService(ILogger<AzureStorageService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Saves the API response to Azure Blob Storage.
        /// </summary>
        /// <param name="fileName">The name of the file to be created in Blob Storage.</param>
        /// <param name="content">The content to be written to the file.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while saving the API response to Blob Storage.</exception>
        public async Task SaveAPIResponseToBlobStorage(string fileName, string content)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(_configuration["APIChecker-BlobStorageConnectionString"]);
                var containerClient = blobServiceClient.GetBlobContainerClient(_configuration["APIChecker-BlobContainerName"]);

                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(fileName);
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    await blobClient.UploadAsync(stream, true);
                }

            } catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the API response to Blob Storage");
                throw;
            }   
        }

        /// <summary>
        /// Adds the API response to Azure Table Storage.
        /// </summary>
        /// <param name="apiEntry">The APIEntry object to be added to Table Storage.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="RequestFailedException">Thrown when a request to Table Storage fails.</exception>
        /// <exception cref="Exception">Thrown when an error occurs while adding the API response to Table Storage.</exception>
        public async Task AddAPIResponseToTableStorage(APIEntry apiEntry)
        {
            try
            {
                TableClient tableClient = new TableClient(_configuration["APIChecker-TableConnectionString"], _configuration["APIChecker-TableName"]);

                await tableClient.CreateIfNotExistsAsync();

                // Add the APIEntry to the table.
                apiEntry.CreatedAt = DateTime.UtcNow;
                await tableClient.AddEntityAsync<APIEntry>(apiEntry);

            } catch(RequestFailedException rex)
            {
                _logger.LogError(rex, "Request failed error occurred while adding the API response to Table Storage");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the API response to Table Storage");
                throw;
            }
        }

        /// <summary>
        /// Adds an API error response to Azure Table Storage.
        /// </summary>
        /// <param name="error">The error message to be added to Table Storage.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="RequestFailedException">Thrown when a request to Table Storage fails.</exception>
        /// <exception cref="Exception">Thrown when an error occurs while adding the API error response to Table Storage.</exception>
        public async Task AddAPIErrorResponseToTableStorage(string error)
        {
            try
            {
                TableClient tableClient = new TableClient(_configuration["APIChecker-TableConnectionString"], _configuration["APIChecker-TableErrorName"]);

                await tableClient.CreateIfNotExistsAsync();

                // Add the APIEntry to the table.
                await tableClient.AddEntityAsync<APIError>(new APIError() {  CreatedAt = DateTime.UtcNow, Error = error });

            }
            catch (RequestFailedException rex)
            {
                _logger.LogError(rex, "Request failed error occurred while adding the API response to Table Storage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the API response to Table Storage");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of API response files from Azure Blob Storage that were created within a specified date range.
        /// </summary>
        /// <param name="from">The start date of the range.</param>
        /// <param name="to">The end date of the range.</param>
        /// <returns>A list of file names that were created within the specified date range.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the list of API response files from Blob Storage.</exception>
        public async Task<IEnumerable<string>> GetAPIResponseBlobFilesList(DateTime from, DateTime to)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(_configuration["APIChecker-BlobStorageConnectionString"]);
                var containerClient = blobServiceClient.GetBlobContainerClient(_configuration["APIChecker-BlobContainerName"]);

                var blobFiles = new List<string>();

                if (containerClient.Exists())
                {
                    await foreach (var blobItem in containerClient.GetBlobsAsync())
                    {
                        if (blobItem.Properties.CreatedOn >= from && blobItem.Properties.CreatedOn <= to)
                        {
                            blobFiles.Add(blobItem.Name);
                        }
                    }
                }

                return blobFiles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the list of API response files from Blob Storage");
                throw;
            }
        }

        /// <summary>
        /// Downloads an API response file from Azure Blob Storage.
        /// </summary>
        /// <param name="fileName">The name of the file to be downloaded from Blob Storage.</param>
        /// <returns>A Stream representing the downloaded file.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while downloading the file from Blob Storage.</exception>
        public async Task<MemoryStream> DownloadAPIResponseBlobFile(string fileName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(_configuration["APIChecker-BlobStorageConnectionString"]);
                var containerClient = blobServiceClient.GetBlobContainerClient(_configuration["APIChecker-BlobContainerName"]);

                if (containerClient.Exists())
                {
                    var blobClient = containerClient.GetBlobClient(fileName);

                    MemoryStream fileStream = new MemoryStream();
                    await blobClient.DownloadToAsync(fileStream);
                    fileStream.Position = 0;

                    return fileStream;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the list of API response files from Blob Storage");
                throw;
            }
        }
    }
}
