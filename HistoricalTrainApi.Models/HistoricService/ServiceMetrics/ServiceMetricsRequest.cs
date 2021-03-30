using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
{
    public class ServiceMetricsRequest
    {
        [JsonProperty("from_loc")]
        public string FromLocation { get; set; }

        [JsonProperty("to_loc")]
        public string ToLocation { get; set; }

        [JsonProperty("from_time")]
        public string FromTime { get; set; }

        [JsonProperty("to_time")]
        public string ToTime { get; set; }

        [JsonProperty("from_date")]
        public string FromDate { get; set; }

        [JsonProperty("to_date")]
        public string ToDate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("days")]
        public Days Days { get; set; }
    }
}