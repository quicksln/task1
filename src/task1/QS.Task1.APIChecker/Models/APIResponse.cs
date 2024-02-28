using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace QS.Task1.APIChecker.Models
{
    /// <summary>
    /// API Response container
    /// </summary>
    public class APIResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("entries")]
        public List<Entry> Entries { get; set; }
    }
}
