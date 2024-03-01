using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace QS.Task2.Infrastructure.Models
{
    public class Current
    {
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("interval")]
        public int? Interval { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double? Temperature2m { get; set; }

        [JsonPropertyName("cloud_cover")]
        public int? CloudCover { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double? WindSpeed10m { get; set; }
    }
}
