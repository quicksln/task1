using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace QS.Task2.Infrastructure.Models
{
    public class CurrentUnits
    {
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("interval")]
        public string? Interval { get; set; }

        [JsonPropertyName("temperature_2m")]
        public string? Temperature2m { get; set; }

        [JsonPropertyName("cloud_cover")]
        public string? CloudCover { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public string? WindSpeed10m { get; set; }
    }
}
