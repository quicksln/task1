using QS.Task1.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task1.Services.Interfaces
{
    /// <summary>
    /// Interface for Azure Storage Service responsible for saving API 
    /// response to Blob Storage and Table Storage
    /// </summary>
    public interface IAzureStorageService
    {
        Task SaveAPIResponseToBlobStorage(string fileName, string content);
        
        Task AddAPIResponseToTableStorage(APIEntry apiEntry);

        Task AddAPIErrorResponseToTableStorage(string error);

        Task<IEnumerable<string>> GetAPIResponseBlobFilesList(DateTime from, DateTime to);

        Task<MemoryStream> DownloadAPIResponseBlobFile(string fileName);
    }
}
