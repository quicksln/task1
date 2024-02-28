using QS.Task1.APIChecker.Models;
using System;
using System.Threading.Tasks;

namespace QS.Task1.APIChecker.Interfaces
{
  /// <summary>
    /// Represents an interface for an API client.
    /// </summary>
    public interface IAPIClient
    {
        /// <summary>
        /// Gets a random API response.
        /// </summary>
        /// <param name="apiuri">The URI of the API.</param>
        /// <returns>A tuple containing the API response and the file name.</returns>
        Task<(APIResponse Result, string File)> GetRandomAPI(string apiuri);
    }
}
