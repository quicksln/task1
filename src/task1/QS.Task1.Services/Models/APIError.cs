using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace QS.Task1.Services.Models
{
    /// <summary>
    /// AI Error Response details to store in Azure Table Storage
    /// </summary>
    public class APIError : ITableEntity
    {
        public APIError()
        {
            PartitionKey = "APIError";
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTimeOffset.UtcNow;
        }
        public string Error { get; set; }

        public DateTime CreatedAt { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
