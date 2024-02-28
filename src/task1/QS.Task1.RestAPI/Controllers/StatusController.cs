using Microsoft.AspNetCore.Mvc;
using QS.Task1.Services;
using QS.Task1.Services.Interfaces;

namespace QS.Task1.RestAPI.Controllers
{
    /// <summary>
    /// The StatusController is responsible for handling HTTP requests related to the status of the application.
    /// It provides endpoints for retrieving log files from Azure Storage Service based on a provided date range,
    /// and for downloading a file from Azure Storage Service based on a provided file name.
    /// </summary>
    public class StatusController : Controller
    {
        private readonly IAzureStorageService _azureStorageService;
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger, IAzureStorageService azureStorageService)
        {
            _logger = logger;
            _azureStorageService = azureStorageService;
        }

        /// <summary>
        /// API call retrieves log files from Azure Storage Service based on the provided date range.
        /// </summary>
        /// <param name="from">The start date of the range.</param>
        /// <param name="to">The end date of the range.</param>
        /// <returns>A list of log files if successful, or an error message if an exception occurs.</returns>
        [HttpGet($"/{nameof(StatusController.GetLogsFiles)}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<IEnumerable<string>>> GetLogsFiles(DateTime from, DateTime to)
        {
            try
            {
                var result = await _azureStorageService.GetAPIResponseBlobFilesList(from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"API Error ({nameof(StatusController)}.GetLogsFiles) : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API call downloads a file from Azure Storage Service based on the provided file name.
        /// </summary>
        /// <param name="fileName">The name of the file to download.</param>
        /// <returns>A Stream of the file if successful, or an error message if an exception occurs.</returns>
        [HttpGet($"/{nameof(StatusController.DownloadFile)}")]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName))
                {
                    return BadRequest("File name is required");
                }

                var result = await _azureStorageService.DownloadAPIResponseBlobFile(fileName);

                return File(result, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"API Error ({nameof(StatusController)}.GetLogsFiles) : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
