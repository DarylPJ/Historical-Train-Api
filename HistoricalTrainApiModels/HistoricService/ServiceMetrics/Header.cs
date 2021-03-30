using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceMetrics
{
    public class Header
    {
        [JsonProperty("from_location")]
        public string FromLocation { get; set; }

        [JsonProperty("to_location")]
        public string ToLocation { get; set; }
    }
}
