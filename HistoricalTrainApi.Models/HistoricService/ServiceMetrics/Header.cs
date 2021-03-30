using Newtonsoft.Json;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
{
    public class Header
    {
        [JsonProperty("from_location")]
        public string FromLocation { get; set; }

        [JsonProperty("to_location")]
        public string ToLocation { get; set; }
    }
}
