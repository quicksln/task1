using QS.Task1.APIChecker.Interfaces;
using QS.Task1.APIChecker.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace QS.Task1.APIChecker.Configuration
{
    /// <summary>
    /// API Client class to execute calls to external API
    /// </summary>
    public class APIClient : IAPIClient
    {
        private readonly HttpClient _client;

        public APIClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets a random API response.
        /// </summary>
        /// <param name="apiuri">The URI of the API.</param>
        /// <returns>A tuple containing the API response and the file name.</returns>
        public async Task<(APIResponse Result, string File)> GetRandomAPI(string apiuri)
        {
            var response = await _client.GetAsync(apiuri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<APIResponse>(content);
                return (Result: result, File: content);
            }

            return (Result: null, File: null);
        }
    }
}
